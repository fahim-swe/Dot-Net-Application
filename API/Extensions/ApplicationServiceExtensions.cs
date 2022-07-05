using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Helper;
using API.Interface;
using API.Service;
using API.Services;
using API.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
           
            services.Configure<CloudinarySettings>(_config.GetSection("CloudinarySettings"));
            
            services.AddSingleton<PresenceTracker>();
            
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILikesService, LikesService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            return services;
        }


        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration _config)
        {
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };

                    // For signalR
                    options.Events = new JwtBearerEvents 
                    {
                        OnMessageReceived = context =>{
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            
                            if(!string.IsNullOrEmpty(accessToken) && 
                                 path.StartsWithSegments("/hubs")){
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                }

            );
            
            return services;
        }
    }
}