using System.ServiceModel;
using ServiceModelEx;

namespace Topshelf.Services.Events.Common
{
    [ServiceContract(CallbackContract = typeof(IEvents))]
    public interface IEventsSubscriptonService : ISubscriptionService
    {
    }
}
