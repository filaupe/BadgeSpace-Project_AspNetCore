using BadgeSpace.Domain.Extensions.Authentication;
using BadgeSpace.Domain.Interfaces.Repository.Certification;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Repository.Empress;
using BadgeSpace.Domain.Interfaces.Repository.Student;
using BadgeSpace.Domain.Interfaces.Repository.User;
using BadgeSpace.Domain.Interfaces.Services.Autentication;
using BadgeSpace.Domain.Interfaces.Services.Entities.Certification;
using BadgeSpace.Domain.Interfaces.Services.Entities.Course;
using BadgeSpace.Domain.Interfaces.Services.Entities.Empress;
using BadgeSpace.Domain.Interfaces.Services.Entities.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.User;
using BadgeSpace.Infra;
using BadgeSpace.Infra.Repositories.Entities.Certification;
using BadgeSpace.Infra.Repositories.Entities.Course;
using BadgeSpace.Infra.Repositories.Entities.Empress;
using BadgeSpace.Infra.Repositories.Entities.Student;
using BadgeSpace.Infra.Repositories.Entities.User;
using BadgeSpace.Infra.Services.Authentication;
using BadgeSpace.Infra.Services.Entities.Certification;
using BadgeSpace.Infra.Services.Entities.Course;
using BadgeSpace.Infra.Services.Entities.Empress;
using BadgeSpace.Infra.Services.Entities.Student;
using BadgeSpace.Infra.Services.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthenticationScheme(builder.Configuration);

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection")!, 
                        b => b.MigrationsAssembly("BadgeSpace.Infra")));

            builder.Services.AddTransient<IRepositoryUser, ReposiotoryUser>();
            builder.Services.AddTransient<IRepositoryStudent, ReposiotoryStudent>();
            builder.Services.AddTransient<IRepositoryEmpress, ReposiotoryEmpress>();
            builder.Services.AddTransient<IRepositoryCourse, ReposiotoryCourse>();
            builder.Services.AddTransient<IRepositoryCertification, ReposiotoryCertification>();

            builder.Services.AddTransient<IServiceUser, ServiceUser>();
            builder.Services.AddTransient<IServiceStudent, ServiceStudent>();
            builder.Services.AddTransient<IServiceEmpress, ServiceEmpress>();
            builder.Services.AddTransient<IServiceCourse, ServiceCourse>();
            builder.Services.AddTransient<IServiceCertification, ServiceCertification>();

            builder.Services.AddTransient<IAuthCookieService, AuthCookieService>();
            builder.Services.AddTransient<IAuthJsonWebTokenService, AuthJsonWebTokenService>();

            //API
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            //API
            app.UseCors();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}