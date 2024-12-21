using ArcProxy.Core.Extensions;
using ArcProxy.Infrastructure.Extensions;
using AspNetCore.Proxy;

namespace ArcProxy.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCors();
            builder.Services.AddProxies();
            builder.Services.AddMemoryCache();

            builder.Services.AddCore(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseCors(conf =>
            {
                conf.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
