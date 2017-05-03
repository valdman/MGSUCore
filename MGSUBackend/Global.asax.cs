using System.Web.Http;
using SimpleInjector.Integration.WebApi;

namespace MGSUBackend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Bootstraper().Configure();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
