using Galleria.Services.FileStorage;
using Galleria.Services.FileStorage.Implementations;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc3;

namespace Galleria
{
    public class UnityConfig
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc3.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            UnityServiceLocator locator = new UnityServiceLocator(container);  
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IFileStorageService, ChunkedLocalFileSystemStorageService>(new ContainerControlledLifetimeManager(), 
                                                                                                new InjectionConstructor(
                                                                                                    HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BlockPath"]), 
                                                                                                    HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MediaPath"]), 
                                                                                                    ConfigurationManager.AppSettings["BlockPath"], 
                                                                                                    ConfigurationManager.AppSettings["MediaPath"]
                                                                                                ));

            container.RegisterType<IChunkedFileStorageService, ChunkedLocalFileSystemStorageService>(new ContainerControlledLifetimeManager(),
                                                                                                        new InjectionConstructor(
                                                                                                            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BlockPath"]),
                                                                                                            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MediaPath"]),
                                                                                                            ConfigurationManager.AppSettings["BlockPath"],
                                                                                                            ConfigurationManager.AppSettings["MediaPath"]
                                                                                                        ));

            return container;
        }
    }
}