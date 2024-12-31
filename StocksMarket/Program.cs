using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Services;

namespace StocksMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();   
            builder.Services.AddScoped<IStocksRepository, StocksRepository>();   
            builder.Services.AddScoped<IFinnhubService,FinnhubService>();
            builder.Services.AddScoped<IStocksService, StocksService>();
            builder.Services.AddHttpClient();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            builder.Host.UseSerilog((HostBuilderContext context , IServiceProvider services , 
                LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
                
            });
            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders  | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            var app = builder.Build();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
