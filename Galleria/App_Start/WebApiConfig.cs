using System.Web.Http;

namespace Galleria
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			config.MapHttpAttributeRoutes();
        }
    }
}
