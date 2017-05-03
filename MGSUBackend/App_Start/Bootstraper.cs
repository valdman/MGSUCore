using System.Web.Configuration;
using System.Web.Http;
using DataAccess;
using DataAccess.Application;
using DataAccess.Repositories;
using MongoDB.Driver;
using SimpleInjector;
using UserManagment.Entities;

namespace MGSUBackend
{
    public class Bootstraper
    {
        public Container Configure()
        {
            var container  = new Container();
            
            container.Register(GetSessionProvider, Lifestyle.Singleton);
            container.Register<ISessionProvider>(() => container.GetInstance<SessionProvider>(), Lifestyle.Singleton);
            
            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Singleton);


            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            return container;
        }

        private SessionProvider GetSessionProvider()
        {
            return new SessionProvider(WebConfigurationManager
                .ConnectionStrings["MongoConnectionString"]
                .ConnectionString);
        }
    }
}