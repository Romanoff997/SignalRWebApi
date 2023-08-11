// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using MyClassLib.Interface;
using MyClassLib.Services;
using System.ComponentModel.Design;
using System.Text.Json;

HubConnection hubConnection;
IJsonConverter jsonConverter = new JsonNewtonConverter();
Console.WriteLine("Hello, World!");
hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7204/CityHub").Build();
hubConnection.On<string>("Send", message => 
    Console.WriteLine($"Message from server: {message}")
);
hubConnection.On<string>("AddCity", message => 
{ 
    Console.WriteLine($"Message from server: {message}");
    Response<City> df = jsonConverter.ReadJson<Response<City>>(message);
});

await hubConnection.StartAsync();
bool isExit = false;
while (!isExit)
{ 
    var message = Console.ReadLine();
    if (message != "exit")
         await hubConnection.SendAsync("CreateCity", new City() {name = message });
    else
        isExit = true;
        
}
Console.ReadLine();