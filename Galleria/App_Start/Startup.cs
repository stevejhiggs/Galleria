using Microsoft.Owin;
using Microsoft.Practices.Unity.WebApi;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Galleria.App_Start.Startup))]

namespace Galleria.App_Start
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();
			config.DependencyResolver =  new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

			app.MapSignalR();


			config.MapHttpAttributeRoutes();


			app.UseWebApi(config);

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			UnityConfig.GetConfiguredContainer();
			MappingConfig.SetupMappings();
			RavenConfig.SetupRaven();
		}
	}
}
