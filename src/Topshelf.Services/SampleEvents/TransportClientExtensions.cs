using System;
using Topshelf.Services.Events.Client;
using Topshelf.Services.Events.Common;

namespace SampleEvents
{
    public static class TransportClientExtensions
    {
        public static void Publish(this ITransportClient transportClient, string name, string value)
        {
            transportClient.Publish(new TransportEventArgs(new EventType(name), value));
        }

        public static void Subscribe(this ITransportClient transportClient, string name,Action<string> action)
        {
            transportClient.Subsribe(new EventType(name), action);
        }
    }
}
