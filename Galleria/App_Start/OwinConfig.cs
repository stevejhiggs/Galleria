using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Galleria.OwinConfig))]
namespace Galleria
{
	public class OwinConfig
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}
