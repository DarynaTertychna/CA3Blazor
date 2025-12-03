using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FreeToGameExplorer;
using FreeToGameExplorer.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IGameApiClient>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://www.freetogame.com/api/")
    };

    return new GameApiClient(httpClient);
});

await builder.Build().RunAsync();
