using System;
using System.Reflection;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Galleria.Core.RavenDb.Session
{
    public class RavenDocumentStore
    {
        private static IDocumentStore instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (instance == null)
                    throw new InvalidOperationException(
                      "IDocumentStore has not been initialized.");
                return instance;
            }
        }

        public static IDocumentStore Initialize(params Assembly[] assemblies)
        {
            instance = new DocumentStore { ConnectionStringName = "RavenDB" };
            instance.Conventions.IdentityPartsSeparator = "-";
            instance.Initialize();

            IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), instance);

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