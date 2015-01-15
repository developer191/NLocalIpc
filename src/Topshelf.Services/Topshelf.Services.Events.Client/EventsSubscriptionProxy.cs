using System.ServiceModel;
using System.ServiceModel.Channels;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.Events.Client
{
    public class EventsSubscriptionProxy : DuplexClientBase<IEventsSubscriptonService>, IEventsSubscriptonService
    {
        public EventsSubscriptionProxy(InstanceContext inputInstance,Binding binding, EndpointAddress remoteAddress)
            : base(inputInstance,binding,remoteAddress)
        { }

        public void Subscribe(string eventOperation)
        {
            Channel.Subscribe(eventOperation);
        }
        public void Unsubscribe(string eventOperation)
        {
            Channel.Unsubscribe(eventOperation);
        }
    }
}
