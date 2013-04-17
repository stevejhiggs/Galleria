using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;

namespace Galleria.Core.RavenDb.Session
{
    public class RavenWebDocumentSessionFactory : IRavenDocumentSessionFactory
    {
        public const string ItemKey = "RavenSession";

        public IDocumentSession GetSession(IDocumentStore documentStore)
        {
            IDocumentSession session = null;
            bool isCached = false;

            if (HttpContext.Current != null)
            {
                object o = HttpContext.Current.Items[ItemKey];
                if (o != null)
                {
                    session = o as IDocumentSession;
                    isCached = true;
                }
            }

            if (session == null)
            {
                session = documentStore.OpenSession();
            }

            if (session != null && !isCached)
            {
                HttpContext.Current.Items[ItemKey] = session;
            }
            return session;
        }
    }
}