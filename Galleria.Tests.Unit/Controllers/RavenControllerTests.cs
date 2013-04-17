using Galleria.RavenDb.BaseControllers;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Listeners;
using System;
using System.Web;
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
