using BadgeSpace.Domain.Extensions.Authentication;
using BadgeSpace.Domain.Interfaces.Repository.Base;
using BadgeSpace.Domain.Interfaces.Repository.User;
using BadgeSpace.Domain.Interfaces.Services.Autentication;
using BadgeSpace.Domain.Interfaces.Services.Entities.User;
using BadgeSpace.Infra;
using BadgeSpace.Infra.Repositories.Entities.User;
using BadgeSpace.Infra.Resources.Validation;
using BadgeSpace.Infra.Services.Authentication;
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

            builder.Services.AddScoped<IServiceUser, ServiceUser>();

            builder.Services.AddScoped<IRepositoryUser, ReposiotoryUser>();

            builder.Services.AddScoped<IAuthCookieService, AuthCookieService>();
            builder.Services.AddScoped<IAuthJsonWebTokenService, AuthJsonWebTokenService>();

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