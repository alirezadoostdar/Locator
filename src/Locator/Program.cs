using Locator.Common;
using Locator.Common.Persistence;
using Locator.Features.IpLocation;
using Locator.Features.IpLocation.Providers.IPGeoLocationReponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Locator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<IGeoLocationApi,IPGeolocationProvider>();

builder.Services.Configure<AppSetting>(builder.Configuration);
var settings = builder.Configuration.Get<AppSetting>();
ArgumentNullException.ThrowIfNull(settings, nameof(settings));

builder.Services.AddDbContext<LocatorDbContext>(options =>
{
    if (settings is null)
    {
        throw new Exception("Invalid settings");
    }
    options.UseMongoDB(settings.MongoDbSetting.Host, settings.MongoDbSetting.DatabaseName);
});

builder.Services.AddHttpClient<IGeoLocationApi,IPGeolocationProvider>(options =>
{
    options.BaseAddress = new Uri(settings.Featuers.IpLocation.IPGeolocationProviderBaseUrl);
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

builder.Services.AddMassTransit(options => {
    options.AddConsumers(typeof(IAssemblyMarker).Assembly);
    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.UseRawJsonDeserializer();

        cfg.Host(settings.RabbitMqConfigurations.Host,
            hostConfig =>
            {
                hostConfig.Username(settings.RabbitMqConfigurations.Username);
                hostConfig.Password(settings.RabbitMqConfigurations.Password);
            });

        cfg.ConfigureEndpoints(context);
    });
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

