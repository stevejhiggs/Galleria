using System;
using Raven.Client;

namespace Galleria.Core.RavenDb.Session
{
    public interface IRavenDocumentSessionFactory
    {
        IDocumentSession GetSession(IDocumentStore documentStore);
    }
}
