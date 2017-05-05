using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Filters;
using DataAccess;
using DataAccess.Application;
using DataAccess.Repositories;
using MGSUBackend.Authentification;
using PostManagment;
using SimpleInjector;
using UserManagment;
using UserManagment.Application;

namespace MGSUBackend
{
    public class Bootstraper
    {
        public Container Configure()
        {
            var container = new Container();
            container.Register(GetSessionProvider, Lifestyle.Singleton);
            container.Register<ISessionProvider>(() => container.GetInstance<SessionProvider>(), Lifestyle.Singleton);
            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Singleton);
            container.Register<IUserManager, UserManager>(Lifestyle.Singleton);
            container.Register<IPostManager, PostManager>(Lifestyle.Singleton);
            container.Register<IContactManager, ContactManager>(Lifestyle.Singleton);
            container.Register<ISessionManager, SessionManager>(Lifestyle.Singleton);

            container.Register<IAuthenticationFilter, AuthenticationFilter>(Lifestyle.Singleton);
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