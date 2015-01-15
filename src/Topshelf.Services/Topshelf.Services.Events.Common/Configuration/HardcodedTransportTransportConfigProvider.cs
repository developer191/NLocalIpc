using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Topshelf.Services.Events.Common.Configuration
{
    class HardcodedTransportTransportConfigProvider : ITransportConfigProvider
    {
        public TransportSettings Read()
        {
            const string pathToPublishing = "EventsPublisher";
            const string pathToSubscribing = "EventsSubscriptionService";
            TransportSettings transportSettings = new TransportSettings();
            transportSettings.PublishingAddress = "net.pipe://localhost/" + pathToPublishing;
            transportSettings.SubscribingAddress = "net.pipe://localhost/" + pathToSubscribing;
            transportSettings.Binding = "netNamedPipeBinding";
            transportSettings.BindingConfiguration = "NamedPipeBinding";
            transportSettings.PublishingContract = "Topshelf.Services.Events.Common.IEvents";
            transportSettings.SubscribingContract = "Topshelf.Services.Events.Common.IEventsSubscriptonService";
            return transportSettings;
        }
    }
}
