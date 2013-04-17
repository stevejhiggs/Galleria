using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using System.Reflection;

namespace Galleria.Core.RavenDb.Session
{
    public class RavenDocumentStore
    {
        public static IDocumentStore Initialize(params Assembly[] assemblies)
        {
            var instance = new DocumentStore { ConnectionStringName = "RavenDB" };
            instance.Conventions.IdentityPartsSeparator = "-";
            instance.Initialize();

            Assembly callAssembly = Assembly.GetCallingAssembly();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            IndexCreation.CreateIndexes(callAssembly, instance);

            if (executingAssembly != callAssembly)
            {
                IndexCreation.CreateIndexes(executingAssembly, instance);
            }
            

            if (assemblies != null)
            {
                foreach (Assembly a in assemblies)
                {
                    IndexCreation.CreateIndexes(a, instance);
                }
            }

            return instance;
        }
    }
}