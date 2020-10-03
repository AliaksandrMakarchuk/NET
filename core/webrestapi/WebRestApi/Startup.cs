using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebRestApi.Logger;
using WebRestApi.Repository;
using WebRestApi.Services;

namespace WebRestApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // services.AddDbContext<WebRestApiContext>(
            //     options => options.
            //     .(Configuration.GetConnectionString($"WebRestApi{Configuration.GetValue<string>("EnvironmentName","HomeEnv")}")));
            services.AddScoped<UserRepositoryBase, UserRepository>();
            services.AddScoped<MessageRepositoryBase, MessageRepository>();
            services.AddScoped<CommentRepositoryBase, CommentRepository>();
            services.AddScoped<IDataService, DataService>();
            services.Configure<FileLoggerOptions>(Configuration.GetSection("Logger"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var x = env.EnvironmentName;
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
