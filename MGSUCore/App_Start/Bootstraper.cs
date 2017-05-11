using DataAccess;
using DataAccess.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using PostManagment;
using SimpleInjector;
using UserManagment;
using UserManagment.Application;

namespace MGSUCore
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
            
            container.Verify();
            return container;
        }

        private SessionProvider GetSessionProvider()
        {
            //todo: avoid hardcode
            return new SessionProvider("mongodb://mgsu:mgsuForJambul@127.0.0.1:27017/mgsu");
        }
    }

    public sealed class SimpleInjectorControllerActivator : IControllerActivator
    {
        private readonly Container container;
        public SimpleInjectorControllerActivator(Container c) { container = c; }

        public object Create(ControllerContext c) =>
            container.GetInstance(c.ActionDescriptor.ControllerTypeInfo.AsType());

        public void Release(ControllerContext c, object controller) { }
    }
}