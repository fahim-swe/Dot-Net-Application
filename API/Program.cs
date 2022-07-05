
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            // var host = CreateHostBuilder(args).Build();
            //     using var scope = host.Services.CreateAsyncScope();
            //     var services = scope.ServiceProvider;
            //     try{
            //         var context = services.GetRequiredService<DataContext>();
            //         await context.Database.MigrateAsync();
            //         await Seed.SeedUsers(context);

            //     }catch(Exception ex){
            //     var logger = services.GetRequiredService<ILogger<Program>>();
            //     logger.LogError(ex, "An Error");
            //     }
            //     await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
