using System;
using System.ServiceModel;


namespace WaitingAllWCFCallbacksToComplete
{
    public interface IEventsClient : IDisposable
    {
        void RaiseAndWaitProcessed(CrossProcessEventArgs eventArgs);
        event EventHandler<CrossProcessEventArgs> EventRaised;
    }


}