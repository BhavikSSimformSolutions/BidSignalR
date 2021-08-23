using BidSignalR.ConHub;
using BidSignalR.Services;
using BidSignalR.Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace BidSignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSignalR().AddAzureSignalR("Endpoint=https://atgsinalr.service.signalr.net;AccessKey=CVy+BctRjtiAuSuoFqtXzWv29kAB062DcSFMpifGhQQ=;Version=1.0;");

            services.Add(new ServiceDescriptor(typeof(IMessageHandler), typeof(MessageHandler), ServiceLifetime.Transient)); // Transient

            services.AddScoped<IRestClientApiCall, RestClientApiCall>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseCors(builder =>
            {
                builder.WithOrigins("null")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<ConnectionHub>("/chat");
            });
        }
    }
}
