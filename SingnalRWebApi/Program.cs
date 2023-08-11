using Microsoft.AspNetCore.SignalR;
using MyClassLib.Interface;
using MyClassLib.Services;
using SingnalRWebApi.Domain.Repositories;
using SingnalRWebApi.Domain.Repositories.Abstract;
using SingnalRWebApi.Domain.Repositories.EntityFramework;
using SingnalRWebApi.Hubs;
using SingnalRWebApi.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddTransient<ICityModelRepository, EFCityModelRepository>(); 
builder.Services.AddTransient<DataManager>();
builder.Services.AddTransient<CityNotificationHub>();
builder.Services.AddSingleton<IJsonConverter>(provider => {

    return new JsonNewtonConverter();
});
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.MapHub<CityNotificationHub>("/CityHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();
