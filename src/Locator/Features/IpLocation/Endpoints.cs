using Microsoft.AspNetCore.Mvc;

namespace Locator.Features.IpLocation
{
    public static class Endpoints
    {
        public static IEndpointRouteBuilder MapIplocationEndPoint(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/Locations/{ip_addres:required}",
                async (LocationService locationService,
                [FromRoute(Name = "ip_addres")] string IpAddress,
                CancellationToken cancellationToken) =>
            {
                return await locationService.GetLocationByIP(IpAddress, cancellationToken);
            });
            return endpoint;
        }
    }
}
