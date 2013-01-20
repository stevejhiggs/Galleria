using Galleria.Services.FileStorage;
using Galleria.Services.FileStorage.Implementations;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc3;

namespace Galleria
{
    public class UnityConfig
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IFileStorageService, ChunkedLocalFileSystemStorageService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IChunkedFileStorageService, ChunkedLocalFileSystemStorageService>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}