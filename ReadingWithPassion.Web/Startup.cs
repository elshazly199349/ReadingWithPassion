using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ReadingWithPassion.Web.Models.DataAccess;
using ReadingWithPassion.Web.Models.Repository.Classes;
using ReadingWithPassion.Web.Models.Repository.Interfaces;
using ReadingWithPassion.Web.Security;
using ReadingWithPassion.Web.ViewModels.Account;

namespace ReadingWithPassion.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static readonly List<CultureInfo> supportedCultures = new List<CultureInfo>{
            new CultureInfo("ar-EG"),
            new CultureInfo("en-US")
            };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
             {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>{
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ReadingWithPassionDBConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Authontication
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "939702425933-llnpqkr0na4l2u6ejm4ubk5b14avek7c.apps.googleusercontent.com";
                options.ClientSecret = "a95ofJgE79uoUqj0NvSenzAm";
            })
                .AddFacebook(options => {
                    options.ClientId = "591827334902547";
                    options.ClientSecret = "a89fcb3fe9822e6672bbcc749b013674";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ControlRolePolicy", policy => policy.AddRequirements(new ManageRolesAndClaimsRequirment()));
            });
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            //inject intefraces
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<StringDataPRotection>();
            services.AddSingleton<IAuthorizationHandler, CanControleOthersRolesAndClaims>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
