using ControlCash.EF;
using Lime.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ControlCash.Bot.Controllers
{
    public class RememberController : ApiController
    {
        [HttpGet]
        [Route("Remember/do")]
        public async Task<HttpResponseMessage> ReceiveMessage()
        {
            var result = new HttpResponseMessage(HttpStatusCode.Accepted);
            using (var db = new ControlCashDbContext())
            {
                var wallets = await db.WalletRepository.GetAllAsync();
                foreach (var wallet in wallets)
                {
                    var users = await db.UserRepository.FindAllAsync(a => a.WalletId == wallet.WalletId);
                    foreach (var user in users)
                    {
                        var date = DateTime.Now.AddDays(-7);
                        var moviment = await db.MovimentHistoryRepository.FindAllAsync(a => a.Date >= date && a.UserId == user.UserId);
                        if (moviment.Count() == 0)
                        {
                            using (var client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Key", "Y29udHJvbGNhc2g6Y1NlYWJQbWdKUmVOUFo4c3lPR0Q=");

                                var message = new Message()
                                {
                                    Content = "Já faz um tempo que você não cadastra nada, isto está correto?",
                                    To = $"{user.UserIdentifier}@messenger.gw.msging.net",
                                    Id = Guid.NewGuid().ToString(),
                                    From = "controlcash@msging.net"
                                };

                                var json = JsonConvert.SerializeObject(message.ToDocument());
                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                var response = await client.PostAsync("https://msging.net/messages", content);
                            };

                        }
                    }
                }
            }

            return result;
        }
    }
}
