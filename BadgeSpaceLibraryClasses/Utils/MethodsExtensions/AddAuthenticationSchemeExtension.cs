using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace BadgeSpace.Utils.MethodsExtensions
{
    public static class AddAuthenticationSchemeExtension
    {
        public static IServiceCollection AddAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSecret").Value);
            services.AddAuthentication(x =>
             {
                 x.DefaultAuthenticateScheme = "JWT_Or_Cookie";
                 x.DefaultChallengeScheme = "JWT_Or_Cookie";
             })
            .AddJwtBearer( x =>
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
            .AddPolicyScheme("JWT_Or_Cookie", "JWT_Or_Cookie", x =>
            {
                x.ForwardDefaultSelector = context =>
                {
                    string authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        return JwtBearerDefaults.AuthenticationScheme;

                    return IdentityConstants.ApplicationScheme;
                };
            });

            return services;
        }
    }
}
