using Asp.Versioning;
using PriceDataFetcher.Service;
using PriceDataFetcher.Service.Background;
using Serilog;

namespace PriceDataFetcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build())
               .CreateLogger();
            try
            {
                Log.Information("Starting up the SynteticArbitrage PriceDataFetcher API...");
                var builder = WebApplication.CreateBuilder(args);

                //CORS settings
                var corsOrigins = "SynteticArbitrage";
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: corsOrigins,
                                      policy =>
                                      {
                                          policy
                                          .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                      });
                });

                //API Versioning
                builder.Services.AddApiVersioning(options => {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader("X-Api-Version"),
                        new MediaTypeApiVersionReader("v")
                    );
                })
               .AddApiExplorer(options => {
                   options.GroupNameFormat = "'v'VVV";
                   options.SubstituteApiVersionInUrl = true;
               });
                // Add services to the container.
                builder.Services.AddSingleton<ISymbolInfoReader, BinanceSymbolInfoReader>();
                builder.Services.AddSingleton<IBTCSymbolReader, BTCSymbolReader>();
                builder.Services.AddSingleton<IApiReader, ApiReader>();
                builder.Services.AddSingleton<IPriceReader, BinancePriceReader>();
                builder.Services.AddHostedService<BackgroundPriceReader>();
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Host.UseSerilog();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.UseCors(corsOrigins);

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }
    }
}
