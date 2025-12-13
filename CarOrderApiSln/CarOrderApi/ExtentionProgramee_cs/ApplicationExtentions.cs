using CarOrderApi.Data;
using CarOrderApi.Repositories;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarOrderApi.ExtentionProgramee_cs
{
    public static class ApplicationExtentions
    {
        public static IServiceCollection ApplicationService(this IServiceCollection service, IConfiguration config)
        {
            //Database Configure
            service.AddDbContext<AppDbContext>(op => op.UseSqlServer(config.GetConnectionString("con")));
            //Identity:
            service.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //Dependency Injection:
            service.AddScoped<ICarRepository, CarRepository>();
            service.AddScoped<ICarService, CarService>();


            //Jwt Authentication:
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Op => {
                Op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["jwt:Issuer"],
                    ValidAudience = config["jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:Key"]))
                };
            });
            service.AddScoped<TokenServices>();
            return service;
        }

    }
}
