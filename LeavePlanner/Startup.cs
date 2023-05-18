using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Mapping;
using LeavePlanner.Persistence;
using LeavePlanner.Utilities.Email;
using LeavePlanner.Utilities.ExceptionHandler;
using LeavePlanner.Utilities.Logger;
using LeavePlanner.Utilities.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace LeavePlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<Contacts>(Configuration.GetSection("Contacts"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Auto Mapper Configurations

            var mappingConfig = new MapperConfiguration(mc =>
            {
                var profiles = new List<Profile>
                {
                    new TestProfile()
                };
                mc.AddProfiles(profiles);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion Auto Mapper Configurations

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region DB Context

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion DB Context

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            #region Identity and Identity Options

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            #endregion Identity and Identity Options

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.Cookie.Name = "LoginCookie"; // TODO name
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    // options.SlidingExpiration = true;
                });

            services.AddAuthorization();
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseGlobalExceptionHandler(errorPagePath: "/Home/GlobalError", respondWithJsonErrorDetails: true);
            }
            else
            {
                app.UseGlobalExceptionHandler(errorPagePath: "/Home/GlobalError", respondWithJsonErrorDetails: true);
                app.UseHsts();

            }

            #region Logger

            loggerFactory.AddLog4Net();
            ApplicationLogging.LoggerFactory = loggerFactory;

            #endregion Logger

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            Directory.CreateDirectory("Documents");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Documents")),
                RequestPath = new PathString("/documents")
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Test}/{action=LandPage}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseCookiePolicy();
            app.UseResponseCaching();
            Seed.SeedUsers(userManager, roleManager);

        }
    }
}
