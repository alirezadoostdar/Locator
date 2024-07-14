using Locator.Features.IpLocation.Model;

namespace Locator.Features.IpLocation
{
    public interface IGeoLocationApi
    {
        Task<GeolocationApiResponse> GetAsync(string ip , CancellationToken cancellationToken);
    }
}
