using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Grpc.Core;
using ProtoBuf.Grpc.Client;
using RiskCompiler.Shared.GrpcServices;
using Syncfusion.Blazor;

namespace RiskCompiler.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton(services =>
            {
                // Create a gRPC-Web channel pointing to the backend server
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
                var channel = GrpcChannel.ForAddress("https://localhost:44393/", new GrpcChannelOptions { HttpClient = httpClient });

                // Now we can instantiate a gRPC-web client for the channel
                return channel.CreateGrpcService<IRiskCompilerGrpcService>();
            });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjUxMDMyQDMxMzgyZTMxMmUzMGRFNzhuVEtsOW9JcnF5MFdUNHgyTzdtZGs4a0xVMUVZTzMrS1N3R01TcEU9");
            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}
