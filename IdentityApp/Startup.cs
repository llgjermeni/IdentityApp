using System.Collections.Generic;
using IdentityApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            var users = new Dictionary<string, string> { { "Admin", "Admin@1234" } };
            services.AddSingleton<IUserService>(new UserService(users));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddFacebook(options =>
                {
                    options.AppId = "your appId";
                    options.AppSecret = "your appSecret";
                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "your ConsumerKey";
                    options.ConsumerSecret = "your ConsumerSecret";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "your Microsoft ClientId";
                    options.ClientSecret = "your Microsoft ClientSecret";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "your Google ClientId";
                    options.ClientSecret = "Your Google ClientSecret";
                })
            .AddCookie(options =>
            {
                options.LoginPath = "/auth/signin";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRewriter(new Microsoft.AspNetCore.Rewrite.RewriteOptions().AddRedirectToHttps(301, 44343));
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
