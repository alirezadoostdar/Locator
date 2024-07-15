using Locator.Common.Persistence;
using Locator.Features.IpLocation.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Locator.Features.IpLocation
{
    public class LocationService(LocatorDbContext _dbContext, IGeoLocationApi _geoLocationApi)
    {
        public async Task<LocationResponse> GetLocationByIP(string ip,CancellationToken cancellationToken)
        {
            try
            {
                var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Ip == ip, cancellationToken);

                if (location is not null)
                {
                    return (LocationResponse)location;
                }
                var newLocation = await FetchLocationFromApi(ip, cancellationToken);
                return (LocationResponse)newLocation;
            }
            catch (Exception ex)
            {
                throw;
            }
        } 

        public async Task<LocationDetailResponse> GetLocationDetailByIp(string ip,CancellationToken cancellationToken)
        {
            try
            {
                var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Ip == ip, cancellationToken);

                if (location is not null)
                {
                    return (LocationDetailResponse)location;
                }
                var newLocation = await FetchLocationFromApi(ip, cancellationToken);
                return (LocationDetailResponse)newLocation;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async  Task<Location> FetchLocationFromApi(string ip, CancellationToken cancellationToken)
        {
            var responceLocation = await _geoLocationApi.GetAsync(ip, cancellationToken);
            var newLocation = new Location
            {
                Ip = ip,
                City = responceLocation.City,
                State = responceLocation.State,
                Country = responceLocation.Country,
                Latitude = responceLocation.Latitude,
                Longitude = responceLocation.Longitude
            };
            await _dbContext.Locations.AddAsync(newLocation, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newLocation;
        }
    }
}
