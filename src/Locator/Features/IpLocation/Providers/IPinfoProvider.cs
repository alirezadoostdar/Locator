using Locator.Features.IpLocation.Model;

namespace Locator.Features.IpLocation.Providers
{
    public class IPinfoProvider : IGeoLocationApi
    {
        public Task<GeolocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
