using Galleria.Core.RavenDb.Session;
using Raven.Client;

namespace Galleria
{
    public static class RavenConfig
    { 
        public static void SetupRaven()
        {
            RavenDocumentStore.Initialize();
        }
    }
}