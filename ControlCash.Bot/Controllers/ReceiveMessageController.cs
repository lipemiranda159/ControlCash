using System;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using System.Diagnostics;
using Api.Ai.ApplicationService.Factories;
using Api.Ai.Csharp.Frameworks.Blip.Ai.Interfaces;
using Api.Ai.Domain.DataTransferObject.Request;
using Lime.Messaging.Contents;
using Api.Ai.Csharp.Frameworks.Blip.Ai.Extensions;
using System.Web.Http;
using ControlCash.Domain;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using ControlCash.Domain.Interfaces;
using ControlCash.EF;
using System.Text;
using ControlCash.Entities;

namespace ControlCash.Bot.Controllers
{
    public class ReceiveMessageController : ApiController
    {
        private readonly IApiAiMessageTranslator _blipAiMessageTranslator;
        private readonly IApiAiAppServiceFactory _apiAiAppServiceFactory;
        private readonly IMessageAppService _messageAppService;
        private string _urlApiAi;
        private string _apiKey;


        public ReceiveMessageController(IApiAiMessageTranslator blipAiMessageTranslator, IApiAiAppServiceFactory apiAiAppServiceFactory, IMessageAppService messageAppService)
        {
            _blipAiMessageTranslator = blipAiMessageTranslator;
            _apiAiAppServiceFactory = apiAiAppServiceFactory;
            _messageAppService = messageAppService;
            _urlApiAi = "https://api.api.ai/v1";
            _apiKey = "9a4815a6297a4bfe84a506a39d165613";


        }

        private async Task<string> ReturnMessage(LimitHistory limitHistory)
        {
            var result = string.Empty;

            if (limitHistory.Balance >= limitHistory.LimitValue)
            {
                result = "Você já gastou mais do que podia esse mês! 😢";

            }
            else
            {
                var percent = 100 - ((limitHistory.Balance * 100) / limitHistory.LimitValue);
                percent = Math.Round(percent);
                if (percent == 0)
                {
                    result = "Você acaba de atingir a cota do mês! 😢";
                }
                else
                {
                    result = $"Você está a {percent}% de atingir a cota!";
                }
            }
            return result;
        }

        [HttpPost]
        [Route("ReceiveMessage/Input")]
        public async Task<HttpResponseMessage> ReceiveMessage()
        {
            var result = new HttpResponseMessage(HttpStatusCode.Accepted);
            var request = await Request.Content.ReadAsStringAsync();
            var requestMessage = JsonConvert.DeserializeObject<Message>(request);
            var queryRequest = await _blipAiMessageTranslator.TranslateMessageAsync(request);

            var queryAppService = _apiAiAppServiceFactory.CreateQueryAppService(_urlApiAi,
                _apiKey);
            var queryResponse = await queryAppService.PostQueryAsync(queryRequest);

            if (queryResponse.Result.Action.Equals("savevalue"))
            {
                using (var db = new ControlCashDbContext())
                {
                    var userId = requestMessage.From.Name.Split('@')[0];

                    var user = db.UserRepository.Find(a => a.UserIdentifier.Equals(userId));

                    var value = decimal.Parse(queryResponse.Result.Parameters["number"].ToString());
                    if (user != null)
                    {
                        var wallet = db.WalletRepository.Find(a => a.WalletId == user.WalletId);
                        if (wallet != null)
                        {
                            if (queryResponse.Result.Contexts[0].Parameters["Type"].ToString().Equals("Entrada"))
                            {
                                wallet.Balance += value;
                                await db.SaveChanges();
                                db.MovimentHistoryRepository.Add(new Entities.MovimentHistory()
                                {
                                    Date = DateTime.Now,
                                    Description = queryResponse.Result.Contexts[0].Parameters["TagEntrada.original"].ToString(),
                                    UserId = user.UserId,
                                    Value = value
                                });
                                await db.SaveChanges();
                            }
                            else
                            {
                                wallet.Balance -= value;
                                await db.SaveChanges();
                                var tagSaida = queryResponse.Result.Contexts[0].Parameters["TagSaida.original"].ToString();
                                if (!string.IsNullOrEmpty(tagSaida))
                                {
                                    var limit = db.LimitRepository.Find(a => a.TagDescription.Equals(tagSaida));
                                    var limitHistory = db.LimitHistoryRepository.Find(a => a.Month == DateTime.Now.Month && a.Year == DateTime.Now.Year && a.TagDescription.Equals(tagSaida));
                                    if (limit != null)
                                    {
                                        var text = string.Empty;
                                        if (limitHistory != null)
                                        {
                                            limitHistory.Balance += value;
                                            await db.SaveChanges();
                                            var requestSendMessage = new Message()
                                            {
                                                Content = await ReturnMessage(limitHistory),
                                                From = requestMessage.To,
                                                Id = Guid.NewGuid().ToString(),
                                                To = requestMessage.From
                                            };
                                            await _messageAppService.SendMessageAsync(requestSendMessage);
                                        }
                                        else
                                        {
                                            limitHistory = new LimitHistory()
                                            {
                                                Balance = 0,
                                                LimitValue = limit.LimitValue,
                                                Month = (short)DateTime.Now.Month,
                                                Year = (short)DateTime.Now.Year,
                                                TagDescription = tagSaida
                                            };

                                            limitHistory.Balance += value;
                                            await db.SaveChanges();
                                            var requestSendMessage = new Message()
                                            {
                                                Content = await ReturnMessage(limitHistory),
                                                From = requestMessage.To,
                                                Id = Guid.NewGuid().ToString(),
                                                To = requestMessage.From
                                            };
                                            await _messageAppService.SendMessageAsync(requestSendMessage);

                                            db.LimitHistoryRepository.Add(limitHistory);
                                            await db.SaveChanges();
                                        }


                                    }

                                }
                                wallet.Balance -= value;
                                await db.SaveChanges();
                                db.MovimentHistoryRepository.Add(new Entities.MovimentHistory()
                                {
                                    Date = DateTime.Now,
                                    Description = queryResponse.Result.Contexts[0].Parameters["TagSaida.original"].ToString(),
                                    UserId = user.UserId,
                                    Value = value
                                });
                                await db.SaveChanges();
                            }
                        }
                        else
                        {
                            var requestSendMessage = new Message()
                            {
                                Content = "Desculpa mas não existe uma conta criada para você! Alguém entrará em contato com você para verificar!",
                                From = requestMessage.To,
                                Id = Guid.NewGuid().ToString(),
                                To = requestMessage.From
                            };
                            await _messageAppService.SendMessageAsync(requestSendMessage);
                        }
                    }
                    else
                    {
                        db.UserRepository.Add(new Entities.User()
                        {
                            UserIdentifier = userId,
                            Wallet = new Wallet() { Balance = 0 }
                        });
                        await db.SaveChanges();
                        var requestSendMessage = new Message()
                        {
                            Content = "Sua conta foi criada com sucesso!",
                            From = requestMessage.To,
                            Id = Guid.NewGuid().ToString(),
                            To = requestMessage.From
                        };
                        await _messageAppService.SendMessageAsync(requestSendMessage);
                    }
                }

            }

            var documents = await _blipAiMessageTranslator.TranslateAsync(queryResponse);

            foreach (var item in documents)
            {
                var requestSendMessage = new Message()
                {
                    Content = item,
                    From = requestMessage.To,
                    Id = Guid.NewGuid().ToString(),
                    To = requestMessage.From
                };
                await _messageAppService.SendMessageAsync(requestSendMessage);
                Thread.Sleep(500);
            }
            return result;
        }
    }
}
