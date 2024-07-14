using Locator.Common;
using Locator.Common.Persistence;
using Locator.Features.IpLocation;
using Locator.Features.IpLocation.Providers.IPGeoLocationReponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<IGeoLocationApi,IPGeolocationProvider>();

var settings = builder.Configuration.Get<AppSetting>();

builder.Services.AddDbContext<LocatorDbContext>(options =>
{
    if (settings is null)
    {
        throw new Exception("Invalid settings");
    }
    options.UseMongoDB(settings.MongoDbSetting.Host, settings.MongoDbSetting.DatabaseName);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.MapIplocationEndPoint();
app.Run();

