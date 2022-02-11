using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using Date;
using Models.Models;
using Services.Implementation;
using Services.Interfaces;
using AcademyProject.Models;

namespace AcademyProject
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
            services.AddControllersWithViews();

            services.TryAddScoped<SignInManager<User>>();

            services.AddTransient<ApplicationDbContext>();

            services.AddRazorPages();

            services.AddAutoMapper(typeof(ErrorViewModel));

            RegisterDatabaseServices(services);
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
           
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }

        private static void RegisterDatabaseServices(IServiceCollection services)
        {
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICoursesUserService, CoursesUserService>();
        }
    }
}