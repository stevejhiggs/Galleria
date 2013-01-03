using System;
using Raven.Client;

namespace Galleria.RavenDb.Session
{
    public interface IRavenDocumentSessionFactory
    {
        IDocumentSession GetSession();
    }
}
