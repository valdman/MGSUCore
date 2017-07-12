﻿using PostManagment;
using UserManagment;
using UserManagment.Application;
using DataAccess.Application;
using DataAccess;
using FileManagment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using MGSUCore.Authentification;
using ProjectManagment.Application;
using ProjectManagment;
using DonationManagment.Application;
using DonationManagment;
using Common;

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

        public async void Configure()
        {
            _services //DI Work
            .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
			.AddSingleton<IUserManager, UserManager>()
			.AddSingleton<IPostManager, PostManager>()
			.AddSingleton<IContactManager, ContactManager>()
            .AddSingleton<IProjectManager, ProjectManager>()
            .AddSingleton<IDonationManager, DonationManager>()
			.AddSingleton<IFileManager, FileManager>()
			.AddSingleton<IImageResizer, ImageResizer>()

            //Register auth middleware
            .AddSingleton<IAuthorizationHandler, IsAuthentificatedAuthHandler>()
            .AddSingleton<IAuthorizationHandler, IsInRoleRoleAuthHandler>();

            //Register Settings
            _services.Configure<FileStorageSettings>(_configuration.GetSection("FileStorageSettings"));
            _services.Configure<DeploySettings>(_configuration.GetSection("DeploySettings"));

            var sessionProvider = new SessionProvider(_configuration["ConnectionStrings:Mongo"]);
            _services.AddSingleton<ISessionProvider, SessionProvider>(
                (arg) => sessionProvider);
            //await DataConstraintsProvider.CreateConstraints(sessionProvider);
        }
    }
}