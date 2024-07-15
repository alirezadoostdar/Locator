﻿namespace Locator.Common;

public partial class Features
{
    public required IpLocationSettings IpLocation { get; set; } = null;
}

public class IpLocationSettings
{
    public required string IPGeolocationProviderAPIKey { get; set; }
    public required string IPGeolocationProviderBaseUrl { get; set; }
}
