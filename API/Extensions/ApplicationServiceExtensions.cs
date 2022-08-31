using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Helper;
using API.Interface;
using API.Services;
using API.SignalR;
using Microsoft.EntityFrameworkCore;


namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
        {
            services.AddDbContext<DataContext>(
                options => {
                    var connectionString = _config.GetConnectionString("Database");
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            );

            services.AddSingleton<PresenceTracker>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            return services;
        }
    }
}