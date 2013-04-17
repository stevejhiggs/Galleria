using Galleria.Core.RavenDb.Session;
using Galleria.RavenDb.BaseControllers;
using Raven.Client;

namespace Galleria
{
    public static class RavenConfig
    { 
        public static void SetupRaven()
        {
            var documentStore = RavenDocumentStore.Initialize();
            RavenBaseController.DocumentStore = documentStore;
            RavenBaseApiController.DocumentStore = documentStore;
        }
    }
}