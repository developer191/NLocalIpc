using System;
using System.Collections.Generic;
using System.ServiceModel;
using Topshelf.Services.Events.Common;
using Topshelf.Services.Events.Common.Configuration;

namespace Topshelf.Services.Events.Client
{
    /// <remarks>
    /// All messages go through server and filtered only on client side.
    /// </remarks>
    public abstract class TransportClientBase : ITransportClient
    {

        protected Dictionary<EventType, List<Action<string>>> _subscriptions = new Dictionary<EventType, List<Action<string>>>();

        protected ITransportConfigProvider _transportConfigProvider = new TransportConfigProvider("Topshelf.Services.Events.Agent.dll.config");
        protected IEventsSubscriptonService _subscriber;
        protected IEvents _publisher;

        protected TransportClientBase(ITransportConfigProvider configProvider, IEventsSubscriptonService subscriber, IEvents publisher)
        {
            _transportConfigProvider = configProvider;
            _publisher = publisher;
            _subscriber = subscriber;
        }

        protected TransportClientBase() {}

        public void Publish(TransportEventArgs transportEventArgs)
        {
            _publisher.OnEvent(transportEventArgs);
        }

        
        //TODO:Check how works EventAggregator if to subscribe 2 times with the same action
        public void Subsribe(EventType eventType, Action<string> action)
        {
            lock (_subscriptions)
            {
                if (!_subscriptions.ContainsKey(eventType))
                {
                    _subscriptions.Add(eventType, new List<Action<string>>(new[] { action }));
                }
                else
                {
                    _subscriptions[eventType].Add(action);
                }
            }
        }

        public void Unsubsribe(Action<string> actionToUnsubscribe)
        {
            foreach (var subscription in _subscriptions)
            {
                if (subscription.Value.Contains(actionToUnsubscribe))
                {
                    subscription.Value.Remove(actionToUnsubscribe);
                }
            }
        }

        public void UnsubsribeFromEvent(EventType eventToUnsubscribe)
        {
            if (_subscriptions.ContainsKey(eventToUnsubscribe))
            {
                _subscriptions.Remove(eventToUnsubscribe);
            }
        }

        public void UnsubsribeFromAll()
        {
            _subscriptions.Clear();
        }

        protected void OnEvent(TransportEventArgs transportEventArgs)
        {
            if (_subscriptions.ContainsKey(transportEventArgs.Type))
            {
                var actions = _subscriptions[transportEventArgs.Type];
                foreach (var action in actions)
                {
                    //TODO: investigate use of weak action
                    action.Invoke(transportEventArgs.Value);
                }
            }
        }


        public abstract void Dispose();
    }
}
