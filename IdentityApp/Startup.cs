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
                    options.AppId = "254243155160654";
                    options.AppSecret = "8cd678674cddfff3f062e77fd2dce236";
                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "zKQo45x2jElBvTOSbIsqEAQOV";
                    options.ConsumerSecret = "S15PXa8ZpkC5G9P8ENOCr1yfG7YPXQYWcPAXmznnZvPuhquJhD";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "45256a15-d81c-4ab3-a536-aef9865369a1";
                    options.ClientSecret = "jcxqgCQ093{pnDLHAI89$-*";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "915952567520-2qbii9fdhg4tkt8228pfk0of7g3gbem4.apps.googleusercontent.com";
                    options.ClientSecret = "uVZcvmr-z5khnTLtMGIBSOJl";
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
