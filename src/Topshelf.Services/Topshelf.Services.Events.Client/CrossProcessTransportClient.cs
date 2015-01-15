using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ServiceModelEx;
using Topshelf.Services.Events.Common.Configuration;

namespace Topshelf.Services.Events.Client
{
    /// <remarks>
    /// All message send synchronously to server, 
    /// but received asynchronously by subscribers.
    /// </remarks>
    public class CrossProcessTransportClient : TransportClientBase
    {
        public CrossProcessTransportClient()
        {
            //TODO: check if Daemon alive and run it if not
            //TODO: use manually read config to initialize proxies
            TransportSettings transportSettings = _transportConfigProvider.Read();

            NetNamedPipeBinding publishingBinding = new NetNamedPipeBinding();
            publishingBinding.TransactionFlow = true;
            publishingBinding.Name = transportSettings.Binding;
            publishingBinding.Namespace = transportSettings.BindingConfiguration;

            NetNamedPipeBinding subscribingBinding = new NetNamedPipeBinding();
            subscribingBinding.TransactionFlow = true;
            subscribingBinding.Name = transportSettings.Binding;
            subscribingBinding.Namespace = transportSettings.BindingConfiguration;

            _publisher = new EventsPublishingProxy(publishingBinding,  new EndpointAddress(transportSettings.PublishingAddress));
            WCFEventsSubscriber subscriber = new WCFEventsSubscriber(OnEvent);
            InstanceContext context = new InstanceContext(subscriber);
            _subscriber = new EventsSubscriptionProxy(context, subscribingBinding, new EndpointAddress(transportSettings.SubscribingAddress));
            _subscriber.Subscribe("OnEvent");//TODO: ()=>subscriber.OnEvent();
        }


        public override void Dispose()
        {
            _subscriptions.Clear();
            (_publisher as IDisposable).Dispose();
            (_subscriber as IDisposable).Dispose();
        }
    }
}
