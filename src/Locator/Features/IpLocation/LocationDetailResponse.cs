using Locator.Features.IpLocation.Domain;

namespace Locator.Features.IpLocation
{
    public record LocationDetailResponse(
        string Ip,
        double Latitude,
        double Longitude,
        string Country,
        string State,
        string City)
    {
        public static explicit operator  LocationDetailResponse(Location location)
            => new LocationDetailResponse(location.Ip,
                location.Latitude,
                location.Longitude,
                location.Country,
                location.State,
                location.City);
    }
}
