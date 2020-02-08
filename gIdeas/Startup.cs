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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /// Add anti forgery middle-ware
            services.AddAntiforgery(a =>
            {
                a.HeaderName = "X-AntiForgery-TOKEN";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            /// Get the db connection string from the appsetting.json
            services.AddDbContext<gAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"));
            });

            /// Setup EF core context and identity
            services.AddIdentityCore<gUser>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = gAppConst._ClaimUserId;
                options.ClaimsIdentity.RoleClaimType = gAppConst._ClaimRole;
                options.User.RequireUniqueEmail = true;
                options.Password = gAppConst._PasswordOptions;
            })
            .AddEntityFrameworkStores<gAppDbContext>()// Add the db context or custom one
            .AddSignInManager<SignInManager<gUser>>()
            .AddDefaultTokenProviders();

            /// Setup for EF identity cookie options 
            Action<CookieAuthenticationOptions> CookieAuthenticationCookies = options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.ClaimsIssuer = "G-Ideas";/// Change this later
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            };

            services.AddAuthentication()
                .AddCookie(gAppConst._AuthSchemeApplication, CookieAuthenticationCookies);

            // Add Authorization policies for users.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(gAppConst._Admin, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] { gAppConst._Admin });
                });
                options.AddPolicy(gAppConst._QAManager, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] { gAppConst._QAManager });
                });
                options.AddPolicy(gAppConst._QACoordinator, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] { gAppConst._QACoordinator });
                });
                options.AddPolicy(gAppConst._Staff, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] { gAppConst._Staff });
                });
                options.AddPolicy(gAppConst._All, policy =>
                {
                    policy.AuthenticationSchemes.Add(gAppConst._AuthSchemeApplication);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", new string[] { gAppConst._Staff, 
                                                               gAppConst._QACoordinator,
                                                               gAppConst._QAManager,
                                                               gAppConst._Admin});
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
