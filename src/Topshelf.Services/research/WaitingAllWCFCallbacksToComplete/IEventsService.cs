using System.ServiceModel;

namespace WaitingAllWCFCallbacksToComplete
{
    [ServiceContract(
        CallbackContract = typeof(IEventsServiceCallback),
        SessionMode = SessionMode.Required
       )]
    public interface IEventsService
    {
        [OperationContract(IsOneWay = true)]
        void Initialize();
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Raise(CrossProcessEventMessage eventMessage);

    }
}