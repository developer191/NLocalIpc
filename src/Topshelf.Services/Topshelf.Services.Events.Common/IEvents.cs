using System.ServiceModel;

namespace Topshelf.Services.Events.Common
{
    [ServiceContract]
    public interface IEvents
    {
        [OperationContract(IsOneWay = true)]
        void OnEvent(TransportEventArgs transportEventArgs);
    }

}
