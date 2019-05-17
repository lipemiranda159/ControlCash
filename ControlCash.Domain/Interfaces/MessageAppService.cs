using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lime.Protocol;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ControlCash.Domain.Interfaces
{
    public class MessageAppService : IMessageAppService
    {
        public async Task SendMessageAsync(Message message)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Key", "Y29udHJvbGNhc2g6Y1NlYWJQbWdKUmVOUFo4c3lPR0Q=");

                var json = JsonConvert.SerializeObject(message.ToDocument());
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://msging.net/messages", content);
            }

        }
    }
}
