using System;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.Events.Client
{
    public class WCFEventsSubscriber : IEvents
    {
        private readonly Action<TransportEventArgs> _action;


        public WCFEventsSubscriber(Action<TransportEventArgs> action)
        {
            _action = action;
        }

        public void OnEvent(TransportEventArgs transportEventArgs)
        {
            _action.Invoke(transportEventArgs);
        }
    }
}
