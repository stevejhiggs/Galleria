using Galleria.RavenDb.BaseControllers;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Listeners;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Galleria.Tests.Unit.Controllers
{
    public abstract class RavenControllerTests
    {
        private readonly EmbeddableDocumentStore documentStore;
		protected ControllerContext ControllerContext { get; set; }

        protected RavenControllerTests()
		{
			documentStore = new EmbeddableDocumentStore
			                {
			                	RunInMemory = true
			                };

			documentStore.RegisterListener(new NoStaleQueriesAllowed());
			documentStore.Initialize();
		}

		protected void SetupData(Action<IDocumentSession> action)
		{
			using (var session = documentStore.OpenSession())
			{
				action(session);
				session.SaveChanges();
			}
		}

		public class NoStaleQueriesAllowed : IDocumentQueryListener
		{
			public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
			{
				queryCustomization.WaitForNonStaleResults();
			}
		}

		public void Dispose()
		{
			documentStore.Dispose();
		}

        protected async Task ExecuteApiAction<TController>(Action<TController> action) where TController : RavenBaseApiController, new()
        {
            var controller = new TController { RavenSession = documentStore.OpenAsyncSession() };

            var httpContext = new Moq.Mock<HttpContextBase>();
            httpContext.Setup(x => x.Response).Returns(new Moq.Mock<HttpResponseBase>().Object);
            action(controller);

            await controller.RavenSession.SaveChangesAsync();
        }

		protected void ExecuteAction<TController>(Action<TController> action) where TController: RavenBaseController, new()
		{
			var controller = new TController {RavenSession = documentStore.OpenSession()};

            var httpContext = new Moq.Mock<HttpContextBase>();
            httpContext.Setup(x => x.Response).Returns(new Moq.Mock<HttpResponseBase>().Object);
			ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
			controller.ControllerContext = ControllerContext;

			action(controller);

			controller.RavenSession.SaveChanges();
		}

        protected void ExecuteAction(RavenBaseController controller, Action<RavenBaseController> action)
        {
            controller.RavenSession = documentStore.OpenSession();

            var httpContext = new Moq.Mock<HttpContextBase>();
            httpContext.Setup(x => x.Response).Returns(new Moq.Mock<HttpResponseBase>().Object);
            ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
            controller.ControllerContext = ControllerContext;

            action(controller);

            controller.RavenSession.SaveChanges();
        }
    }
}
