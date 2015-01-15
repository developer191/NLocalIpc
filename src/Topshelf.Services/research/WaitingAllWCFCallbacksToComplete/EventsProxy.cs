using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WaitingAllWCFCallbacksToComplete
{
    public class EventsProxy : ClientBase<IEventsService>, IEventsService
    {


        public EventsProxy(InstanceContext inputInstance, Binding binding, EndpointAddress remoteAddress)
            : base(inputInstance, binding, remoteAddress)
        { }

        public void Dispose()
        { }

        public void Initialize()
        {
           Channel.Initialize();
        }

        public void Raise(CrossProcessEventMessage eventMessage)
        {
            Channel.Raise(eventMessage);
        }
    }
}