using Newtonsoft.Json;
using SignalRWebApi.Domain.Repositories;
using SignalRWebApi.Domain.Repositories.Abstract;
using SignalRWebApi.Domain.Repositories.EntityFramework;
using SignalRWebApi.Hubs;
using SignalRWebApi.Server.Helpers;
using SignalRWebApi.Shared.Interface;
using SignalRWebApi.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddTransient<ICityModelRepository, EFCityModelRepository>(); 
builder.Services.AddTransient<DataManager>();
builder.Services.AddTransient<CityNotificationHub>();
//builder.Services.AddTransient<ICity, CityEntity>();
builder.Services.AddSingleton<IJsonConverter>(provider => {

    return new JsonNewtonConverter(new JsonSerializerSettings()
    { 
    });
});
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
//builder.WebHost.UseStaticWebAssets();
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

//app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();
