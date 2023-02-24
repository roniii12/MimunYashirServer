using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimunYashirInfrastructure.Extensions;
using NLog;
using NLog.Web;

namespace MimunYashir
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                SetNlogConnectionString(builder.Configuration.GetMainConnectionString());

                AppHostConfiguration.ConfigureServices(builder.Services, builder.Configuration);

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseMiddleware<Middlewares.RequestLoggerMiddleware>();

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();

            }
        }

        public static void SetNlogConnectionString(string connectionString)
        {
            GlobalDiagnosticsContext.Set("connectionString", connectionString);
        }
    }
}