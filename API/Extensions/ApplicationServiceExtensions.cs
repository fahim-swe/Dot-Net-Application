using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interface;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration _config)
        {
            string mySqlConnectionStr = _config.GetConnectionString("DefaultConnection");  
            services.AddDbContext<DataContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr))); 
            
            return services;
        }


        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration _config){
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}