using System;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace Topshelf.Services.Events.Common.Configuration
{
    public class TransportSettings : ConfigurationSectionBase
    {
        private const string BindingConfigurationName = "bindingConfiguration";
        private const string PublishingContractName = "publishingContract";
        private const string PublishingAddressName = "publishingAddress";
        private const string SubscribingAddressName = "subscribingAddress";
        private const string BindingName = "binding";
        private const string SubscribingContractName = "subscribingContract";

        [ConfigurationProperty(SubscribingContractName, IsRequired = true)]
        public string SubscribingContract
        {
            get { return Get<string>(SubscribingContractName); }
            set { Set(SubscribingContractName, value); }
        }

        [ConfigurationProperty(PublishingContractName, IsRequired = true)]
        public string PublishingContract
        {
            get { return Get<string>(PublishingContractName); }
            set { Set(PublishingContractName, value); }
        }

        [ConfigurationProperty(PublishingAddressName, IsRequired = true)]
        public string PublishingAddress
        {
            get { return Get<string>(PublishingAddressName); }
            set { Set(PublishingAddressName, value); }
        }

        [ConfigurationProperty(SubscribingAddressName, IsRequired = true)]
        public string SubscribingAddress
        {
            get { return Get<string>(SubscribingAddressName); }
            set { Set(SubscribingAddressName, value); }
        }

        [ConfigurationProperty(BindingName, IsRequired = true)]
        public string Binding
        {
            get { return Get<string>(BindingName); }
            set { Set(BindingName, value); }
        }

        [ConfigurationProperty(BindingConfigurationName, IsRequired = true)]
        public string BindingConfiguration
        {
            get { return Get<string>(BindingConfigurationName); }
            set { Set(BindingConfigurationName, value); }
        }


    }
}