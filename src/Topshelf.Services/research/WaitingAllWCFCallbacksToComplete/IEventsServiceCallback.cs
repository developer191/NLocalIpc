using System;
using System.ServiceModel;

namespace WaitingAllWCFCallbacksToComplete
{
    public interface IEventsServiceCallback
    {
        [OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
        void OnEventRaised(CrossProcessEventMessage eventMessage);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void OnMessageProcessed(CrossProcessEventMessage eventMessage);
    }


}