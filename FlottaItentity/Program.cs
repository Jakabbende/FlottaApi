using FlottaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlottaItentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<FlottaContext>(options => {
                string connstr = builder.Configuration.GetConnectionString("FlottaConnection");
                options.UseMySql(connstr, ServerVersion.AutoDetect(connstr));
            });
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
