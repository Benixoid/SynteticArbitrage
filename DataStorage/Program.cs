using Asp.Versioning;
using DataStorage.Configs;
using DataStorage.Database;
using DataStorage.Database.DbServices;
using DataStorage.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

namespace DataStorage
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
                Log.Information("Starting up the SynteticArbitrage DataStorage API...");
                
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
                builder.Services.AddControllers().AddJsonOptions(options => {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddSwaggerGen();

                builder.Host.UseSerilog();

                builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

                //DB settings
                builder.Services.AddDbContext<AppDbContext>(opts =>
                    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                    /*.EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information)*/);

                //Add required services to DI
                builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
                builder.Services.AddScoped<IDataManager, DataManager>();
                builder.Services.AddScoped<IPriceDifferencesRepository, PriceDifferenceRepository>();
                builder.Services.AddScoped<IPriceService, PriceService>();
                builder.Services.AddSingleton(Log.Logger);


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
