﻿using System;
using System.Text;
using DB.Context;
using DB.Interfaces;
using DB.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NSwag.AspNetCore;

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
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });
            
            var auth0Authority = Environment.GetEnvironmentVariable("AUTH_CONFIG_AUTHORITY") ??
                                   throw new ArgumentException("There is no AUTH_CONFIG_AUTHORITY provided");
            
            var auth0Audience = Environment.GetEnvironmentVariable("AUTH_CONFIG_AUDIENCE") ??
                              throw new ArgumentException("There is no AUTH_CONFIG_AUDIENCE provided");
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	            //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = auth0Authority;
                options.Audience = auth0Audience;
	            options.RequireHttpsMetadata = false;
	            options.TokenValidationParameters = new TokenValidationParameters
	            {
		            ValidateIssuerSigningKey = false,
		            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret")),
		            ValidateIssuer = false,
		            //ValidIssuer = "http://localhost:5000",
		            ValidateAudience = false,
		            //ValidAudience = auth0Audience,
		            ValidateLifetime = false,
		            //ClockSkew = TimeSpan.Zero
	            };
            });

            services.AddSwagger();

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

	        app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
		        .AllowAnyMethod()
		        .AllowAnyHeader());
            
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling = 
                    PropertyNameHandling.SnakeCase;
            });

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "DefaultApi",
                    "api/{controller}/{action}");
            });
        }
    }
}