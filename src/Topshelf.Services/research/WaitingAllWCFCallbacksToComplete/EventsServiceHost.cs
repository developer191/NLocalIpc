using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

using WaitEvents = System.Collections.Generic.Dictionary<string, System.Threading.AutoResetEvent>;

namespace WaitingAllWCFCallbacksToComplete
{
    [ServiceBehavior(
    IncludeExceptionDetailInFaults = true,
    InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EventsServiceHost : IEventsService
    {
        private List<IEventsServiceCallback> _callbacks = new List<IEventsServiceCallback>();

        public OperationContext OperationContext
        {
            get
            {
                var context = OperationContext.Current;
                return context;
            }
        }

        public void Initialize()
        {
            var id = OperationContext.SessionId;
            var callback = OperationContext.GetCallbackChannel<IEventsServiceCallback>();
            if (!_callbacks.Contains(callback)) _callbacks.Add(callback);
        }

        public void Raise(CrossProcessEventMessage eventMessage)
        {
            ThreadPool.QueueUserWorkItem(
                x =>
                {
                    foreach (var callback in _callbacks)
                    {
                        callback.OnEventRaised(eventMessage);
                    }
                    foreach (var callback in _callbacks)
                    {
                        callback.OnMessageProcessed(eventMessage);
                    }
              });
        }

    }
}