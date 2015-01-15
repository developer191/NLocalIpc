using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using ServiceModelEx;
using Topshelf.Services.Events.Agent;
using Topshelf.Services.Events.Common.Configuration;


namespace Topshelf.Services.Daemon
{
    public class Program
    {
        private const string transportStartedName = "Topshelf.Services.Daemon";

        public static void Main(string[] args)
        {
            EventWaitHandle waitHandle = EventWaitHandle.OpenExisting(transportStartedName);
            Run();
            waitHandle.Set();
            Console.Read();
        }

        public static void Run()
        {
            RunTransportServices();
            RunAgents();
        }

        private static void RunAgents()
        {

            //Autofac.ContainerBuilder builder = new ContainerBuilder();
            AgentCatalogBuilder agentCatalogBuilder = new AgentCatalogBuilder();
            string agentUid = "SampleValidService.Agent.SampleService";
            //get assembly SampleValidService.Agent.dll
            //activate SampleValidService.Agent.SampleService class instance
            //read config from  SampleValidService.Agent.dll.config
            //create service host from instance
            // open host
            Type[] agentTypes = agentCatalogBuilder.GetAgentTypes();
            foreach (Type agentType in agentTypes)
            {
                var configName = GetAgentConfigName(agentType) + ".config";
                var config = GetConfigSource(configName);
                var appSettingsSection = config.GetSection("appSettings") as AppSettingsSection;
                var address = new Uri(appSettingsSection.Settings["address"].Value);
                AppDomainHost appDomainHost = AppDomainHost.CreateConfigured(agentType, address);
                appDomainHost.Open();
            }
            //publiserServiceHost.AddServiceEndpoint()
        }

        private static string GetAgentConfigName(Type agentType)
        {
            var result = agentType.Assembly.ManifestModule.Name;
            return result;
        }

        private static FileConfigurationSource GetConfigSource(string configFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var directory = Directory.GetParent(assembly.Location).FullName;
            var path = Path.Combine(directory, configFileName);
            if (!File.Exists(path)) throw new ArgumentException("123File with config not found.");
            var fileConfigurationSource = new FileConfigurationSource(path);
            return fileConfigurationSource;
        }

        private static void RunTransportServices()
        {
            ITransportConfigProvider transportConfigProvider = new TransportConfigProvider("Topshelf.Services.Events.Agent.dll.config");
            TransportSettings transportSettings = transportConfigProvider.Read();

            ServiceHost<EventsPublisherService> publiserServiceHost = new ServiceHost<EventsPublisherService>();
            NetNamedPipeBinding publishingBinding = new NetNamedPipeBinding();
            publishingBinding.TransactionFlow = true;
            publishingBinding.Name = transportSettings.Binding;
            publishingBinding.Namespace = transportSettings.BindingConfiguration;
            publiserServiceHost.AddServiceEndpoint(transportSettings.PublishingContract, publishingBinding, transportSettings.PublishingAddress);
            publiserServiceHost.Open();

            ServiceHost<EventsSubscriptionService> subscriptionServiceHost = new ServiceHost<EventsSubscriptionService>();
            NetNamedPipeBinding subscribingBinding = new NetNamedPipeBinding();
            subscribingBinding.TransactionFlow = true;
            subscribingBinding.Name = transportSettings.Binding;
            subscribingBinding.Namespace = transportSettings.BindingConfiguration;
            subscriptionServiceHost.AddServiceEndpoint(transportSettings.SubscribingContract, subscribingBinding, transportSettings.SubscribingAddress);
            subscriptionServiceHost.Open();
        }
    }

}
