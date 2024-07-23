﻿using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Contracts;

namespace WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("Corspolicy", builder =>
                  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
             services.Configure<IISOptions>(options => 
             {
                 
             });
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, IServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSqlServer<RepositoryContext>((configuration.GetConnectionString("sqlConnection")));


    }
}
