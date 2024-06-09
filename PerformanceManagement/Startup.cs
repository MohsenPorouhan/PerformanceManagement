using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerformanceManagement.Models;
using PerformanceManagement.Models.ICTAdmin;
using PerformanceManagement.Util;
using Microsoft.AspNetCore.HttpOverrides;
namespace PerformanceManagement
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddDbContextPool<AppDbContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("PMDBConnection")));

            services.AddSingleton<IConnProvider, ConnProvider>();
            services.AddSingleton<IConnProviderOBI, OBIConnProvider>();
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //Note: If you change here so you should change ResetPassword and Register action in AccountController
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(o =>
            {
                o.TokenLifespan = TimeSpan.FromMinutes(5);
            });
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //});
            services.AddTransient<IAuthorizationHandler, BeginningOfPeriodHandler>();
            //var appDbContext = services.BuildServiceProvider().GetService<AppDbContext>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("BeginningOfPeriod", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new BeginningOfPeriodRequirement()
                    {

                    });
                });

            });
            services.AddTransient<IAuthorizationHandler, IntermediateOfPeriodHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("IntermediateOfPeriod", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new IntermediateOfPeriodRequirement()
                    {

                    });
                });

            });
            services.AddTransient<IAuthorizationHandler, EndOfPeriodHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("EndOfPeriod", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new EndOfPeriodRequirement()
                    {

                    });
                });

            });
            services.AddTransient<IAuthorizationHandler, ProtestationOfPeriodHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("ProtestationOfPeriod", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new ProtestationOfPeriodRequirement()
                    {

                    });
                });

            });
            services.AddTransient<IAuthorizationHandler, MustChangePasswordHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("MustChangePassword", policy =>
                {
                    //policy.requireauthenticateduser();
                    policy.AddRequirements(new MustChangePasswordRequirement()
                    {

                    });
                });

            });
            services.AddTransient<IAuthorizationHandler, ScoreViewHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("ScoreView", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new ScoreViewRequirement()
                    {

                    });
                });

            });
            services.PostConfigure<CookieAuthenticationOptions>(
                IdentityConstants.ApplicationScheme, opt =>
                {
                    opt.LoginPath = "/ictadmin/account/login";
                    opt.AccessDeniedPath = "/ICTAdmin/account/AccessDenied";
                }
                );
            services.AddScoped<IEmailSender, EmailSender>();
            //services.AddScoped<MustChangePassActionFilter>();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(
                options =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                    //options.Filters.Add(new AuthorizeFilter("MustChangePassword"));
                    options.Filters.Add(new MustChangePassActionFilter());
                }).AddXmlSerializerFormatters(); //SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ////////////////////////////////////////////////////////////////////////////////////////////
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
            /////////////////////////////////////////////////////////////////////////////////////////////
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseSession();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "hradmin",
                    template: "hradmin/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "ictadmin",
                    template: "ictadmin/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "coacher",
                    template: "coacher/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "employee",
                    template: "employee/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "planingadmin",
                    template: "planningadmin/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
