using Locator.Features.IpLocation.Model;

namespace Locator.Features.IpLocation.Providers.IPGeoLocationReponse
{
    public class IPGeolocationProvider : IGeoLocationApi
    {
        public async Task<GeolocationApiResponse> GetAsync(string ip ,CancellationToken cancellationToken)
        {
            string apiKey = "1ecfa9a4e7d6487eb02fb43dd6dc0acb"; // کلید API خود را جایگزین کنید
            string apiUrl = "https://api.ipgeolocation.io/ipgeo";
            string ipAddress = "94.182.46.220"; // آدرس IP مورد نظر خود را وارد کنید

            string urlWithParams = $"{apiUrl}?apiKey={apiKey}&ip={ipAddress}";
            var httpClient = new HttpClient();
            HttpResponseMessage res = await httpClient.GetAsync(urlWithParams);
            res.EnsureSuccessStatusCode();
            string responseBody = await res.Content.ReadAsStringAsync();
            var response = await httpClient.GetFromJsonAsync<IPGeoLocationResponse>(urlWithParams, cancellationToken);
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
