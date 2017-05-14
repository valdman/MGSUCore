using PostManagment;
using UserManagment;
using UserManagment.Application;
using DataAccess.Application;
using DataAccess;
using FileManagment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGSUCore
{
    public class Bootstraper
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationRoot _configuration;

        public Bootstraper(IServiceCollection services, IConfigurationRoot configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void Configure()
        {
            _services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
			_services.AddSingleton<IUserManager, UserManager>();
			_services.AddSingleton<IPostManager, PostManager>();
			_services.AddSingleton<IContactManager, ContactManager>();
			_services.AddSingleton<ISessionManager, SessionManager>();
			_services.AddSingleton<IFileManager, FileManager>();
			_services.AddSingleton<IImageResizer, ImageResizer>();

            //Register Settings
            var test = _configuration.GetSection("Storage");
            _services.Configure<FileStorageSettings>(_configuration.GetSection("FileStorageSettings"));
            _services.AddSingleton<ISessionProvider, SessionProvider>(
                (arg) => new SessionProvider(_configuration["ConnectionStrings:Mongo"]));


        }
    }
}