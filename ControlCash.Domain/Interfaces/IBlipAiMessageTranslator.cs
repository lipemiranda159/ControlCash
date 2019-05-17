using Api.Ai.Domain.DataTransferObject.Request;
using Api.Ai.Domain.DataTransferObject.Response;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Domain
{
    public interface IApiAiMessageTranslator
    {
        Task<IList<Document>> TranslateAsync(QueryResponse queryResponse);

        Task<QueryRequest> TranslateMessageAsync(string request);
    }
}
