using Locator.Common;
using Locator.Features.IpLocation.Model;
using Microsoft.Extensions.Options;

namespace Locator.Features.IpLocation.Providers.IPGeoLocationReponse
{
    public class IPGeolocationProvider(IOptions<AppSetting> options, HttpClient httpClient) : IGeoLocationApi
    {
        private readonly IpLocationSettings _settings = options.Value.Featuers.IpLocation;
        public async Task<GeolocationApiResponse> GetAsync(string ip ,CancellationToken cancellationToken)
        {

            string url = $"?apiKey={_settings.IPGeolocationProviderAPIKey}&ip={ip}";
            HttpResponseMessage res = await httpClient.GetAsync(url);
            res.EnsureSuccessStatusCode();
            string responseBody = await res.Content.ReadAsStringAsync();
            var response = await httpClient.GetFromJsonAsync<IPGeoLocationResponse>(url, cancellationToken);
            if (response is null)
            {
                throw new Exception("h");

            }
            return new GeolocationApiResponse(ip,
                response.Latitude,
                response.Longitude,
                response.Country,
                response.State,
                response.City);
        }
    }
}
