﻿using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebRestApi.DataAccess;
using WebRestApi.DataAccess.Repository;
using WebRestApi.Logger;
using WebRestApi.Service;
using WebRestApi.Service.Repository;

#pragma warning disable 1591
namespace WebRestApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddApiExplorer();
                
            services.AddSingleton<AbstractDbContext, WebRestApiContext>();
            services.AddDbContext<AbstractDbContext>();
            services.AddScoped<UserRepositoryBase, UserRepository>();
            services.AddScoped<MessageRepositoryBase, MessageRepository>();
            services.AddScoped<CommentRepositoryBase, CommentRepository>();
            services.AddScoped<IDataService, DataService>();
            services.AddSwaggerGen();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.Configure<FileLoggerOptions>(Configuration.GetSection("Logger"));
            services.ConfigureSwaggerGen(setup =>
            {
                var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(basePath, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var x = env.EnvironmentName;
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Web Rest API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}