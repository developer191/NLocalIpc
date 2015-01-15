using System.ServiceModel;
using System.ServiceModel.Channels;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.Events.Client
{
    public class EventsPublishingProxy : ClientBase<IEvents>, IEvents
    {
        public EventsPublishingProxy(Binding binding, EndpointAddress remoteAddress)
            : base(binding,remoteAddress)
        { }


        public void OnEvent(TransportEventArgs transportEventArgs)
        {
            Channel.OnEvent(transportEventArgs);
        }
    }
}
