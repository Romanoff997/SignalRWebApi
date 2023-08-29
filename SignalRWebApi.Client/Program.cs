#define CLIENT
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRWebApi.Shared.Interface;
using SignalRWebApi.Shared.Services;
using SignalRWebApi.Client;
using SignalRWebApi.Client.Services;
using System.Text.Json;
using Newtonsoft.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var jsonOptions = new JsonSerializerOptions();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7204") });
builder.Services.AddSingleton<IJsonConverter>(provider => {

    return new JsonNewtonConverter(new JsonSerializerSettings()
    {

    });
});
builder.Services.AddTransient<CitiesService>();

builder.Services.AddTransient<HubConnection>(provider => {

    return new HubConnectionBuilder()
                .WithUrl($"https://localhost:7204/CityHub")
                .Build();
});

await builder.Build().RunAsync();
