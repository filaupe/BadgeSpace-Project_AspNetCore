using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace BadgeSpace.Domain.Extensions.Authentication
{
    public static class AddAuthenticationSchemes
    {
        public static IServiceCollection AddAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSecret").Value!);
            services.AddAuthentication(x =>
            {
                x.DefaultSignInScheme = "JWT_Or_Cookie";
                x.DefaultAuthenticateScheme = "JWT_Or_Cookie";
                x.DefaultChallengeScheme = "JWT_Or_Cookie";
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
            .AddCookie("Identity.Login", config => {
                config.Cookie.Name = "Identity.Login";
                config.LoginPath = "/Login";
                config.AccessDeniedPath = "/Home";
                config.ExpireTimeSpan = TimeSpan.FromHours(1);
            })
            .AddPolicyScheme("JWT_Or_Cookie", "JWT_Or_Cookie", x =>
            {
                x.ForwardDefaultSelector = context =>
                {
                    string authorization = context.Request.Headers[HeaderNames.Authorization]!;
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        return JwtBearerDefaults.AuthenticationScheme;

                    return "Identity.Login";
                };
            });

            return services;
        }
    }
}
