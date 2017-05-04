using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using SimpleInjector.Integration.WebApi;

namespace MGSUBackend
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Bootstraper().Configure();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}