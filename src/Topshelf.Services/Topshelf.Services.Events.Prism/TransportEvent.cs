//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using Microsoft.Practices.Composite.Events;
//using Microsoft.Practices.Composite.Presentation.Events;
//using Topshelf.Services.Events.Client;
//using Topshelf.Services.Events.Common;

    

//namespace Topshelf.Services.EventsAggregation
//{

// To send and receive static type events cross process client only needs:
// 1. add references
// 2. add config file as content 
// 3. public class SpecificTransportEvent : TransportEvent {} 
// 4. _eventAggregator.GetEvent<SpecificTransportEvent>().Subscribe(OnSpecificEvent)
// 5. _eventAggregator.GetEvent<SpecificTransportEvent>().Puplish("Event data")
//    ///<seealso cref="CompositePresenationEvent"/>
//    public class TransportEvent : EventBase
//    {

//        private readonly Dictionary<string , EventsSubscriptionProxy> _subscriptions =
//            new Dictionary<string, EventsSubscriptionProxy>();

//        private EventsPublishingProxy _publisher;

//        public TransportEvent()
//        {
//            _publisher = new EventsPublishingProxy();
//        }

//        public SubscriptionToken Subscribe(Action<TPayload> action)
//        {
//            return Subscribe(action, ThreadOption.PublisherThread);
//        }

//        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption)
//        {
//            return Subscribe(action, threadOption, false);
//        }

//        public SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
//        {
//            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
//        }

//        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
//        {
//            return Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);
//        }

//        public virtual SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TPayload> filter)
//        {
//            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
//            IDelegateReference filterReference;
//            if (filter != null)
//            {
//                filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);
//            }
//            else
//            {
//                filterReference = new DelegateReference(new Predicate<TPayload>(delegate { return true; }), true);
//            }
//            EventSubscription<TPayload> subscription;
//            switch (threadOption)
//            {
//                case ThreadOption.PublisherThread:
//                    subscription = new EventSubscription<TPayload>(actionReference, filterReference);
//                    break;
//                case ThreadOption.BackgroundThread:
//                    subscription = new BackgroundEventSubscription<TPayload>(actionReference, filterReference);
//                    break;
//                //case ThreadOption.UIThread:
//                //    subscription = new DispatcherEventSubscription<TPayload>(actionReference, filterReference, UIDispatcher);
//                //    break;
//                default:
//                    subscription = new EventSubscription<TPayload>(actionReference, filterReference);
//                    break;
//            }

//            return InternalSubscribe(subscription);
//        }



//        public virtual void Publish(TPayload payload)
//        {
//            InternalPublish(new Event(GetType().FullName,payload.ToString()));
//        }

//        public virtual void Unsubscribe(Action<TPayload> subscriber)
//        {
//            lock (Subscriptions)
//            {
//                IEventSubscription eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
//                if (eventSubscription != null)
//                {
//                    Subscriptions.Remove(eventSubscription);
//                    UnsubscribeProxy(eventSubscription.SubscriptionToken);
//                }
//            }
//        }

//        public virtual bool Contains(Action<TPayload> subscriber)
//        {
//            IEventSubscription eventSubscription;
//            lock (Subscriptions)
//            {
//                eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
//            }
//            return eventSubscription != null;
//        }

//        public override bool Contains(SubscriptionToken token)
//        {
//            return base.Contains(token);
//        }

//        protected override void InternalPublish(params object[] arguments)
//        {
//            base.InternalPublish(arguments);
//            _publisher.OnEvent(arguments.First() as Event);
//        }


//        protected override SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
//        {
//            SubscriptionToken token = base.InternalSubscribe(eventSubscription);
//            string transportToken = token + GetType().FullName;
//            EventsSubscriber subscriber = new EventsSubscriber(transportToken,eventSubscription.GetExecutionStrategy());
//            InstanceContext context = new InstanceContext(subscriber);
//            var subscriptionProxy = new EventsSubscriptionProxy(context);
//            subscriptionProxy.Subscribe("OnEvent");
//            _subscriptions.Add(GetType().FullName, subscriptionProxy);
//            return token;
//        }

//        public override void Unsubscribe(SubscriptionToken token)
//        {
//            UnsubscribeProxy(token);
//            base.Unsubscribe(token);
//        }

//        private void UnsubscribeProxy(SubscriptionToken token)
//        {
//            var subscriptionProxy = _subscriptions[token + GetType().FullName];
//            subscriptionProxy.Unsubscribe("OnEvent");
//            subscriptionProxy.Close();
//            _subscriptions.Remove(GetType().FullName);
//        }
//    }
//}