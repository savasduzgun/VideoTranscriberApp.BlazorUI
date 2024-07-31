using BlazorDownloadFile;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);
await builder.Build().RunAsync();
