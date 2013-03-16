using System.Web.Mvc;
using Raven.Client;
using Galleria.Core.RavenDb.Session;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Threading;
using System;

namespace Galleria.RavenDb.BaseControllers
{
    public abstract class RavenBaseApiController : ApiController
    {
        public IDocumentStore Store
        {
            get { return LazyDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = RavenDocumentStore.Instance;
            return docStore;
        });

        public IAsyncDocumentSession RavenSession { get; set; }

        public async override Task<HttpResponseMessage> ExecuteAsync(
            HttpControllerContext controllerContext,
            CancellationToken cancellationToken)
        {
            using (RavenSession = Store.OpenAsyncSession())
            {
                var result = await base.ExecuteAsync(controllerContext, cancellationToken);
                await RavenSession.SaveChangesAsync();

                return result;
            }
        }
    }
}