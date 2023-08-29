#define CLIENT
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRWebApi.Shared.Interface;
using SignalRWebApi.Shared.Services;
using SignalRWebApi.Client;
using SignalRWebApi.Client.Services;
using Newtonsoft.Json;
using SignalRWebApi.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var settings = new AppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(settings.BaseUrl) });
builder.Services.AddSingleton<IJsonConverter>(provider => 
{
    return new JsonNewtonConverter(new JsonSerializerSettings()
    {

    });
});
builder.Services.AddTransient<CitiesService>();

builder.Services.AddTransient<HubConnection>(provider => {

    return new HubConnectionBuilder()
                .WithUrl($"{settings.BaseUrl}/CityHub")
                .Build();
});

await builder.Build().RunAsync();
