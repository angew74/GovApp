using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Gov.Core.Identity;
using Gov.Structure;
using Gov.Structure.Config;
using Gov.Structure.Identity;
using GovApp.Filters;
using GovApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GovApp
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            //  Configuration = configuration;      
            var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
            this.Configuration = builder.Build();

        }

        public const string CookieAuthScheme = "GovApp";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddDefaultTokenProviders()
              .AddUserStore<UserStore>()
              .AddRoleStore<RoleStore>()
              .AddSignInManager<AuthSignInManager>()
              .AddRoles<ApplicationRole>()
              .AddDefaultTokenProviders()
              .AddUserManager<ApplicationUserManager>()
              .AddRoleManager<RoleManager<ApplicationRole>>()
              .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;               
            });
           /* services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.EventsType = typeof(GovCookieAuthenticationEvents);

            });*/
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings                
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;
                // Lockout settings  
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = false;
            });
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings  
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "GovApp";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.LoginPath = $"/Account/Login";  // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login  
                options.LogoutPath = $"/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout  
                options.AccessDeniedPath = $"/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied  
                options.SlidingExpiration = true;
                options.EventsType = typeof(GovCookieAuthenticationEvents);
                // Simply return 401 responses when authentication fails 
                // as opposed to the default of redirecting to the login page
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireClaim("admin"));
                options.AddPolicy("RequireUserRole",
                    policy => policy.RequireClaim("user"));
                options.AddPolicy("RequireGuestRole",
                 policy => policy.RequireClaim("Guest"));
            });
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .ConfigureApiBehaviorOptions(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;

             });
            services.AddSession();
            services.AddScoped<IUserStore<ApplicationUser>, UserStore>();
            services.AddScoped<UserStore>();
            services.AddScoped<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.Configure<MailConfig>(Configuration.GetSection("mailConfig"));
            services.Configure<ComunicazioneConfig>(Configuration.GetSection("comunicazioneConfig"));
            services.Configure<PagingConfig>(Configuration.GetSection("paginationConfig"));
            services.Configure<ElezioneConfig>(Configuration.GetSection("elezioneConfig"));
            services.AddScoped<GovCookieAuthenticationEvents>();
            services.AddScoped<LockAuthorizePermission>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);         
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.
            var connectionString = Configuration.GetConnectionString("GovContextConnection");
            builder.RegisterModule(new AutofacGovModule(connectionString));

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
