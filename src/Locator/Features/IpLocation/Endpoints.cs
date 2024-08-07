﻿using Locator.Features.IpLocation.Cunsumers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace Locator.Features.IpLocation
{
    public static class Endpoints
    {
        public static IEndpointRouteBuilder MapIplocationEndPoint(this IEndpointRouteBuilder endpoint)
        {
            var endpointGroup = endpoint.MapGroup("/Locations").WithTags("location"); ;

            endpointGroup.MapGet("/{ip_address:required}",
                            (LocationService locationService,
                             [FromRoute(Name = "ip_address")] string IpAddress,
                             CancellationToken cancellationToken) =>
                            {
                                if (!IPAddress.TryParse(IpAddress, out IPAddress? address))
                                {
                                    return Results.BadRequest("Invalid Ip Address.");
                                }

                                return Results.Ok(locationService.GetLocationByIP(address, cancellationToken));
                            });

            endpointGroup.MapGet("/Locations/{ip_addres:required}/details",
                async (LocationService locationService,
                [FromRoute(Name = "ip_addres")] string IpAddress,
                CancellationToken cancellationToken) =>
            {
                 return await locationService.GetLocationDetailByIp(IpAddress, cancellationToken);
            });

            endpointGroup.MapPost("/notify/{ip_address}",([FromRoute(Name = "ip_addres")] string IpAddress,
                IPublishEndpoint publicher) =>
            {
                return publicher.Publish(new GetIpLocationMessage(IpAddress));
            });
            
            return endpoint;
        }

    }
}
