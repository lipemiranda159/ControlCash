using Api.Ai.ApplicationService.Factories;
using Api.Ai.Domain.Service.Factories;
using Api.Ai.Infrastructure.Factories;
using ControlCash.Domain;
using ControlCash.Domain.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;

namespace ControlCash.DI.Api
{
    public class ApiBootstrap
    {
        public static IContainerProvider RegisterContainer(HttpConfiguration config)
        {


            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.RegisterWebApiControllers(config);
            container.RegisterSingleton<IServiceProvider>(container);
            container.RegisterSingleton<IContainerProvider>(() => new ContainerProvider(container));
            container.Register<IApiAiMessageTranslator, BlipAiMessageTranslator>(Lifestyle.Singleton);
            container.Register<IApiAiAppServiceFactory, ApiAiAppServiceFactory>(Lifestyle.Singleton);
            container.Register<IHttpClientFactory, HttpClientFactory>(Lifestyle.Singleton);
            container.Register<IMessageAppService, MessageAppService>(Lifestyle.Singleton);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            return new ContainerProvider(container);
        }

    }
}
