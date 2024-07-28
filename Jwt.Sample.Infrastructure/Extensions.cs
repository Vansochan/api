using System.Text;
using Jwt.Sample.Domain.Database;
using Jwt.Sample.Domain.Repositories;
using Jwt.Sample.Infrastructure.SqlServer.Database;
using Jwt.Sample.Infrastructure.SqlServer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Thinktecture;

namespace Jwt.Sample.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddDefaultJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Settings:Issuer"],
                    ValidAudience = configuration["Jwt:Settings:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Settings:Key"]))
                };
            });
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDapperContext, DapperContext>();

        services.AddDbContextPool<DefaultDbContext>((_, option) =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.AddTableHintSupport();
                opt.AddBulkOperationSupport();
            });
        });
        
        services.AddDefaultJwt(configuration);
        
        return services;
    }
}