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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<ICoreContextFactory, CoreContextFactory>();

            services.AddScoped<IPageRepository>(
                provider => new PageRepository(
                    Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTION_STRING") ??
                    throw new ArgumentException(
                        "There is no ASPNETCORE_CONNECTION_STRING provided"),
                    provider.GetService<ICoreContextFactory>()));
            
            services.AddScoped<IUserRepository>(
                provider => new UserRepository(
                    Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTION_STRING") ??
                    throw new ArgumentException(
                        "There is no ASPNETCORE_CONNECTION_STRING provided"),
                    provider.GetService<ICoreContextFactory>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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