// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Text;
using Common.Domain;
using Common.Infrastructure.MongoDB;
using LoginMS.Data;
using MessageBroker.Infrastructure;
using MessageBroker.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LoginMS.Web
{
    /// <summary>
    /// This class contains the implementation of the startup initializations.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var log = new Log();
            services.AddSingleton<ILog>(log);
            
            ConfigureRepositories(services);
            ConfigureMessageBroker (services);
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Login API", 
                    Version = "v1",
                    Description ="Vikle login API.",
                    Contact = new OpenApiContact
                    {
                        Name = "David Fernández Ramos",
                        Email = "dfernandezramos@uoc.edu"
                    }
                });
            });
            
            // Adding Authentication  
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                
                // Adding Jwt Bearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });
            
            services.AddTransient<DatabasePopulator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabasePopulator dbPopulator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vikle API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbPopulator.Seed().Wait();
        }
        
        void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton(new MongoDbSettings
            {
                ServerConnection = Configuration["ConnectionStrings:MongoDb:ServerConnection"],
                Database = Configuration["ConnectionStrings:MongoDb:DatabaseName"]
            });
            services.AddTransient<ILoginRepository, LoginRepository>();
        }
        
        void ConfigureMessageBroker (IServiceCollection services)
        {
            services.Configure<EventProducerConfiguration> (Configuration.GetSection ("MessageBrokerProducer"));
            services.AddTransient<IProducer, Producer> ();
        }
    }
}