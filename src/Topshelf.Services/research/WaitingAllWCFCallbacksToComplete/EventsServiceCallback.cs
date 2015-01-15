using System;
using System.ServiceModel;

namespace WaitingAllWCFCallbacksToComplete
{
    [CallbackBehavior(
    IncludeExceptionDetailInFaults = true,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    AutomaticSessionShutdown = false)]
    public class EventsServiceCallback : IEventsServiceCallback
    {

        public event EventHandler<CrossProcessEventArgs> EventRaised = delegate { };
        public event EventHandler<CrossProcessEventArgs> EventProcessed = delegate { };
        public void OnEventRaised(CrossProcessEventMessage eventMessage)
        {
            EventRaised(eventMessage.Sender, new CrossProcessEventArgs(eventMessage.Data, eventMessage.Id));
        }

        public void OnMessageProcessed(CrossProcessEventMessage eventMessage)
        {
            EventProcessed(eventMessage.Sender, new CrossProcessEventArgs(eventMessage.Data, eventMessage.Id));
        }
    }
}