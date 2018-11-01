using System;
using DB.Context;
using DB.Interfaces;
using DB.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTION_STRING") ??
                                   throw new ArgumentException("There is no ASPNETCORE_CONNECTION_STRING provided");
            
            services.AddCors();
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped<ICoreContextFactory, CoreContextFactory>();

            services.AddScoped<IPageRepository>(
                provider => new PageRepository(connectionString,
                    provider.GetService<ICoreContextFactory>()));
            
            services.AddScoped<IUserRepository>(
                provider => new UserRepository(connectionString,
                    provider.GetService<ICoreContextFactory>()));
            
            services.AddScoped<IPostRepository>(
                provider => new PostRepository(connectionString,
                    provider.GetService<ICoreContextFactory>()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "DefaultApi",
                    "api/{controller}/{action}");
            });
        }
    }
}