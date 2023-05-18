using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Mapping;
using LeavePlanner.Persistence;
using LeavePlanner.Persistence.Repositories;
using LeavePlanner.Utilities.ExceptionHandler;
using LeavePlanner.Utilities.Hubs;
using LeavePlanner.Utilities.Logger;
using LeavePlanner.Utilities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            #region Options

            services.AddOptions();


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<Contacts>(Configuration.GetSection("Contacts"));
            services.Configure<HolidayApi>(Configuration.GetSection("HolidayApi"));

            #endregion Options

            #region Services

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHolidayService, HolidayService>();
            services.AddScoped<IDataSeeder, DataSeeder>();

            #endregion Services

            #region Auto Mapper

            var mappingConfig = new MapperConfiguration(mc =>
            {
                var profiles = new List<Profile>
                {
                    new LeaveProfile(), new EmployeeProfile()
                };
                mc.AddProfiles(profiles);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion Auto Mapper

            #region Cookie policy

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #endregion Cookie policy

            #region DB Context

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion DB Context

            #region Identity and Identity Options

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                    options.SignIn.RequireConfirmedAccount = false)
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

            #region Routing

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            #endregion Routing

            #region MVC, Razor pages, Signal R

            services.AddControllersWithViews();
            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();
            services.AddSignalR();

            #endregion MVC, Razor pages, Signal R

            #region App cookie

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            #endregion App cookie

            services.AddAuthorization();
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IDataSeeder dataSeeder)
        {
            #region Error

            if (env.IsDevelopment())
            {
                app.UseGlobalExceptionHandler(errorPagePath: "/Home/GlobalError", respondWithJsonErrorDetails: true);
            }
            else
            {
                app.UseGlobalExceptionHandler(errorPagePath: "/Home/GlobalError", respondWithJsonErrorDetails: true);
                app.UseHsts();
            }

            #endregion Error

            #region Logger

            loggerFactory.AddLog4Net();
            ApplicationLogging.LoggerFactory = loggerFactory;

            #endregion Logger

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            #region Endpoints

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<CalendarHub>("/hubs/refetchEvents");
                endpoints.MapControllerRoute(
                                             name: "default",
                                             pattern: "{controller=Home}/{action=LandPage}/{id?}");
                endpoints.MapRazorPages();
            });

            #endregion Endpoints

            app.UseCookiePolicy();
            app.UseResponseCaching();

            #region Seed initial tables

            dataSeeder.SeedData();

            #endregion Seed initial tables
        }
    }

}
