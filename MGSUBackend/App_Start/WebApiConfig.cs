using System.Web.Http;

namespace MGSUBackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}