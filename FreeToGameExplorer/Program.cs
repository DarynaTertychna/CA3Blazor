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
        BaseAddress = new Uri("https://free-to-play-games-database.p.rapidapi.com/api/")  //!!! I need to check full link again, idk but paridapi changed my subscripr 
    };

    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "19e0966934msh388c1117c0379c2p1d02b5jsn24f05b13f25d");      //I need to check key again, runs but with some issues
    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "free-to-play-games-database.p.rapidapi.com");


    return new GameApiClient(httpClient);
});


//async void TestMyError r() { }    //to test my code analysis


await builder.Build().RunAsync();
