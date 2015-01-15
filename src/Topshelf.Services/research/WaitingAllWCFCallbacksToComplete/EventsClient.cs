using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;

namespace WaitingAllWCFCallbacksToComplete
{

    public class EventsClient : IEventsClient
    {
        private EventsProxy _eventsProxy;

        private EventsServiceCallback _eventsServiceCallback;

        private string _sender;

        private Dictionary<Guid, AutoResetEvent> raisedEvents = new Dictionary<Guid, AutoResetEvent>();

        public EventsClient()
        {
            _sender = Guid.NewGuid().ToString();
            var binding = new NetNamedPipeBinding("NamedPipeBinding");
            var endpoint = new EndpointAddress("net.pipe://localhost/WaitingAllWCFCallbacksToComplete.IEventsService");
            _eventsServiceCallback = new EventsServiceCallback();
            _eventsServiceCallback.EventRaised += new EventHandler<CrossProcessEventArgs>(_eventsServiceCallback_EventRaised);
            _eventsServiceCallback.EventProcessed += new EventHandler<CrossProcessEventArgs>(_eventsServiceCallback_EventProcessed);   
            var instanceContext = new InstanceContext(_eventsServiceCallback);
            _eventsProxy = new EventsProxy(instanceContext, binding, endpoint);
            _eventsProxy.Open();
            _eventsProxy.Initialize();
        }

        private void _eventsServiceCallback_EventProcessed(object sender, CrossProcessEventArgs e)
        {
            if (raisedEvents.ContainsKey(e.Id))
            {
                raisedEvents[e.Id].Set();
                raisedEvents.Remove(e.Id);
            }
        }

        void _eventsServiceCallback_EventRaised(object sender, CrossProcessEventArgs e)
        {
            EventRaised(sender,e);
        }

        public void RaiseAndWaitProcessed(CrossProcessEventArgs eventArgs)
        {
            this.raisedEvents[eventArgs.Id] = new AutoResetEvent(false);
            this._eventsProxy.Raise(new CrossProcessEventMessage(_sender,eventArgs.Data,eventArgs.Id));
            this.raisedEvents[eventArgs.Id].WaitOne(_eventsProxy.InnerChannel.OperationTimeout);
        }

        public event EventHandler<CrossProcessEventArgs> EventRaised = delegate { };

        public void Dispose()
        {
            this._eventsProxy.Close();
        }




    }
}