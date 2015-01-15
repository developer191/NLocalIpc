using System.ServiceModel;
using ServiceModelEx;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.Events.Agent
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EventsPublisherService : PublishService<IEvents>, IEvents
    {
        public EventsPublisherService()
        {
        }

        public void OnEvent(TransportEventArgs transportEventArgs)
        {
            FireEvent(transportEventArgs);
        }
    }
}
