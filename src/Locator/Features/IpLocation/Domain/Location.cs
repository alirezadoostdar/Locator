﻿using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Locator.Features.IpLocation.Domain
{
    [Collection("Locations")]
    public class Location
    {
        public ObjectId Id { get; set; }
        public string Ip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
    }
}
