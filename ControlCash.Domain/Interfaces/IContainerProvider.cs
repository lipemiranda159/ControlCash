using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Domain.Interfaces
{
    public interface IContainerProvider : IServiceProvider
    {
        T GetService<T>() where T : class;

        IEnumerable<T> GetServices<T>() where T : class;

        IDisposable BeginScope();
    }
}
