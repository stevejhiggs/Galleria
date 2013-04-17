using System.Web.Mvc;
using Raven.Client;
using Galleria.Core.RavenDb.Session;

namespace Galleria.RavenDb.BaseControllers
{
    public abstract class RavenBaseController : Controller
    {
        public static IDocumentStore DocumentStore { get; set; }

        public IDocumentSession RavenSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (RavenSession == null)
            {
                IRavenDocumentSessionFactory docSessionFactory = new RavenWebDocumentSessionFactory();
                RavenSession = docSessionFactory.GetSession(DocumentStore);
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }
    }
}