using Locator.Common;
using Locator.Features.IpLocation.Model;
using Locator.Features.IpLocation.Providers.IPGeoLocationReponse;
using Microsoft.Extensions.Options;

namespace Locator.Features.IpLocation.Providers.IpApiProvider
{
    public class IpApiProvider(IOptions<AppSetting> options, HttpClient httpClient) : IGeoLocationApi
    {
        private readonly IpLocationSettings _settings = options.Value.Featuers.IpLocation;
        public async Task<GeolocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken)
        {
            string url = $"/{ip}";
            HttpResponseMessage res = await httpClient.GetAsync(url);
            res.EnsureSuccessStatusCode();
            string responseBody = await res.Content.ReadAsStringAsync();
            var response = await httpClient.GetFromJsonAsync<IpApiProviderResponse>(url, cancellationToken);
            if (response is null)
            {
                throw new Exception("h");

            }
            return new GeolocationApiResponse(ip,
                response.lat,
                response.lon,
                response.country,
                response.status,
                response.city);
        }
    }
}
