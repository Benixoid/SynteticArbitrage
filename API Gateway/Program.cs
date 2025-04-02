using Microsoft.AspNetCore.Components.Routing;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Threading.Tasks;
namespace API_Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();
            
            app.MapGet("/", () => "Hello World!");
            await app.UseOcelot();
            app.Run();
        }
    }
}
