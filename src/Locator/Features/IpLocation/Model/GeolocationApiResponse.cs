namespace Locator.Features.IpLocation.Model
{
    public record GeolocationApiResponse(
        string Ip,
        double Latitude,
        double Longitude,
        string Country,
        string State,
        string City);
}
