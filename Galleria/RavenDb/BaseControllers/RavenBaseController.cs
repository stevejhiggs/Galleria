using System.Web.Mvc;
using Raven.Client;
using Galleria.Core.RavenDb.Session;

namespace Galleria.RavenDb.BaseControllers
{
    public abstract class RavenBaseController : Controller
    {
        public IDocumentSession RavenSession { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IRavenDocumentSessionFactory docSessionFactory = new RavenWebDocumentSessionFactory();
            RavenSession = docSessionFactory.GetSession();
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