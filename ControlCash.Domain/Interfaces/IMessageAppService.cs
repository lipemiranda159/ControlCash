using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Domain.Interfaces
{
    public interface IMessageAppService
    {
        Task SendMessageAsync(Message message);
    }
}
