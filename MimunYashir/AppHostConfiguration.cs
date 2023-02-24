using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimunYashirInfrastructure.Configuration;
using MimunYashirInfrastructure.Cummon;
using MimunYashirInfrastructure.Extensions;
using MimunYashirInfrastructure.Log;
using MimunYashirPersistence;
using MimunYashirPersistence.Repositories;
using System.Text;

namespace MimunYashir
{
    public class AppHostConfiguration
    {
        public const string AuthenticationScheme = "Bearer";
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(AppGeneralLogger<>), typeof(AppGeneralLogger<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddMemoryCache();
            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetMainConnectionString());
            });
            services.AddScoped<IAppContext, WebAppContext>();
            services.AddScoped<WebAppContext>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(MainEfRepository<>));
            services.AddSingleton<ConfigurationService>();
            MimunYashirCore.ServicesConfiguration.ConfigureServices(services);

        }
        private static void ConfigureAuthServerIdentity(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetJwtConfig();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(jwtConfig.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }
    }

    public static class ConfigurationExtenstionServer
    {
        public static JWTConfig GetJwtConfig(this IConfiguration configuration) => configuration.GetSection("JWT").Get<JWTConfig>();
        
    }

    public class JWTConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int TokenValidityInMinutes { get; set; }
    }
}
