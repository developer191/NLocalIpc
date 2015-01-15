using ServiceModelEx;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.Events.Agent
{
    public class EventsSubscriptionService : SubscriptionManager<IEvents>, IEventsSubscriptonService
    {
    }
}
