using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Grpc.Core;
using ProtoBuf.Grpc.Server;
using RiskCompiler.DataAccessLayer.EfCode;
using RiskCompiler.Server.GrpcServices;
using RiskCompiler.ServiceLayer.AutoMapper;
using AutoMapper;
using System.Reflection;
using RiskCompiler.ServiceLayer.Services.Core;

namespace RiskCompiler.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCodeFirstGrpc(config => 
            {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
            });

            services.AddDbContext<RiskCompilerContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    optionAction =>
                    {
                        optionAction.MigrationsAssembly("RiskCompiler.DataAccessLayer");
                    }
                )
            );

            services.AddAutoMapper(Assembly.Load("RiskCompiler.ServiceLayer"));

            services.AddScoped<IRiskAssessmentService, RiskAssessmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapGrpcService<RiskCompilerGrpcService>().EnableGrpcWeb();
            });

            //
            // NPB: a less-than-optimal place to call Ensure{Deleted,Created}()
            //
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var serviceScope = serviceScopeFactory.CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetService<RiskCompilerContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            //SeedDB.Seed(options);
        }
    }
}
