using ControlCash.Domain.Interfaces;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.DI
{
    public class ContainerProvider : IContainerProvider
    {
        private readonly Container _container;

        public ContainerProvider(Container container)
        {
            _container = container;
        }

        public T GetService<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            return _container.GetAllInstances<T>();
        }

        public IDisposable BeginScope()
        {
            return _container.BeginExecutionContextScope();
        }
        public object GetService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }
    }
}
