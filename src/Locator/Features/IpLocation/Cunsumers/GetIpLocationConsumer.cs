﻿using MassTransit;
using System.Net;

namespace Locator.Features.IpLocation.Cunsumers
{

    public class GetIpLocationConsumer(
       LocationService locationService,
       IPublishEndpoint publisher) : IConsumer<GetIpLocationMessage>
    {
        public async Task Consume(ConsumeContext<GetIpLocationMessage> context)
        {
            if (!IPAddress.TryParse(context.Message.Ip, out IPAddress? address))
            {
                return;
            }

           var location = await locationService.GetLocationByIP(address!, context.CancellationToken);
        }
    }
}
