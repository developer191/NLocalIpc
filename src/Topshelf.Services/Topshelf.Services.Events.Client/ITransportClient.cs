using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.Events.Client
{
    // 1. add reference 
    // 2. add config file as content
    public interface ITransportClient:IDisposable
    {
        void Publish(TransportEventArgs transportEventArgs);
        //void SubsribeOnAll(Action<string> action)
        void Subsribe(EventType eventType, Action<string> action);
        void Unsubsribe(Action<string> actionToSubscribe);
        void UnsubsribeFromEvent(EventType eventToUnsubscribe);
        void UnsubsribeFromAll();
    }
}
