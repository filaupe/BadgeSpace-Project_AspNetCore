using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BadgeSpace.Services.Extensions
{
    public static class AddAuthenticationSchemes
    {
        public static IServiceCollection AddAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSecret").Value!);
            services.AddAuthentication(x =>
            {
                x.DefaultSignInScheme = "JWT";
                x.DefaultAuthenticateScheme = "JWT";
                x.DefaultChallengeScheme = "JWT";
            })
            .AddJwtBearer("JWT", x =>
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
            });

            return services;
        }
    }
}
