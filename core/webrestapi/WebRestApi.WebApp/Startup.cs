using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<CustomerDbContext> (options => options.UseInMemoryDatabase ("name"));
            services.AddSingleton<ICredentialsManager, CredentialsManager> ();
            services.AddSingleton<INetworkManager, NetworkManager> ();

            // services.AddRazorPages ();

            // services.AddIdentityCore<ApplicationUser> (options => {
            //         options.Password.RequireDigit = true;
            //         options.Password.RequireLowercase = true;
            //         options.Password.RequireUppercase = true;
            //         options.Password.RequiredLength = 6;
            //         options.Password.RequireNonAlphanumeric = false;
            //         options.Password.RequiredUniqueChars = 6;
            //     })
            //     .AddRoles<IdentityRole> ()
            //     .AddRoleStore<CustomerDbContext> ()
            //     .AddUserStore<CustomerDbContext> ()
            //     .AddDefaultTokenProviders ();

            // services.AddIdentityCore<IdentityUser> (options => {
            //         options.SignIn.RequireConfirmedAccount = true;
            //     })
            //     .AddRoles<IdentityRole> ()
            //     .AddRoleStore<CustomerDbContext>()
            //     .AddUserStore<CustomerDbContext>();

            // services.AddRazorPages (options => {
            //     options.Conventions.AuthorizePage ("/Index");
            //     options.Conventions.AuthorizeFolder ("/Private");
            //     options.Conventions.AllowAnonymousToPage ("/Login");
            // });

            // services.AddAuthenticationCore()
            // .AddIdentityServerJwt();

            // services.AddAuthorizationCore (options => {
            //     options.FallbackPolicy = new AuthorizationPolicyBuilder ()
            //         .RequireAuthenticatedUser ()
            //         .Build ();
            // });

            services.AddRazorPages ().AddRazorPagesOptions (options => {
                // options.Conventions.AuthorizeFolder ("/Account/Manage");
                // options.Conventions.AuthorizePage ("/Index");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            // app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

            // app.UseAuthentication();
            // app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapRazorPages ();
            });
        }
    }
}