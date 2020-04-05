using gIdeas.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace gIdeas
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configure ASP.Net pipe-line services
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            /// Add the anti-forgery service and identify the 
            /// the header name of the request to identify and
            /// validate the token
            services.AddAntiforgery(a =>
            {
                a.HeaderName = "X-AntiForgery-TOKEN";
            });
            /// Add Mvc service to the application
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            /// Pass the SQL server connection to the db context
            /// receive the connection string from the package.json
            services.AddDbContext<gAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MainConnection"));
            });

            //**** Setup Identity options with custom user Class
            services.AddIdentityCore<gUser>(options =>
            {
                /// Modify claim object accessors 
                options.ClaimsIdentity.UserIdClaimType = "UserId";
                options.ClaimsIdentity.SecurityStampClaimType = "SecurityStamp";
                options.ClaimsIdentity.RoleClaimType = gAppConst.AccessClaims.Type;
                /// Specify that the user's email address must be unique
                options.User.RequireUniqueEmail = true;
                /// Set the password option for the user
                options.Password = gAppConst.PasswordOptions;
            })
            .AddEntityFrameworkStores<gAppDbContext>()// Add the db context or custom one
            .AddSignInManager<AuthManager<gUser>>()
            .AddDefaultTokenProviders();

            /// Setup for EF identity cookie options 
            void CookieAuthenticationCookies(CookieAuthenticationOptions options)
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.ClaimsIssuer = "G-Ideas";/// Change this later
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            }

            services.AddAuthentication()
                .AddCookie(gAppConst._AuthSchemeApplication, CookieAuthenticationCookies);

            // Add Authorization policies for users.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(gAppConst.AccessPolicies.LevelOne, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] { gAppConst.AccessClaims.Admin });
                });
                options.AddPolicy(gAppConst.AccessPolicies.LevelTwo, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] {gAppConst.AccessClaims.Admin,
                                                              gAppConst.AccessClaims.QAManager }) ;
                });
                options.AddPolicy(gAppConst.AccessPolicies.LevelThree, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] {gAppConst.AccessClaims.Admin,
                                                              gAppConst.AccessClaims.QAManager,
                                                              gAppConst.AccessClaims.QACoordinator }) ;
                });
                options.AddPolicy(gAppConst.AccessPolicies.LevelFour, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] {gAppConst.AccessClaims.Admin,
                                                              gAppConst.AccessClaims.QAManager,
                                                              gAppConst.AccessClaims.QACoordinator,
                                                              gAppConst.AccessClaims.Staff }) ;
                });
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            /// Add the anti-forgery cookie to the context response
            app.Use(next => context =>
            {
                switch (context.Request.Path.Value)
                {
                    case "/":
                        AntiforgeryTokenSet tokens = antiforgery.GetAndStoreTokens(context);
                        context.Response.Cookies.Append(
                            "AntiForgery-TOKEN",
                            tokens.RequestToken,
                            new CookieOptions() { HttpOnly = false });
                        break;
                }
                return next(context);
            });

            /// App setup to use the following middle-wares
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            /// User MVC Routes for the api calls
            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
