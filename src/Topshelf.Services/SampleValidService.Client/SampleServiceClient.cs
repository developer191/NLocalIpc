using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SampleValidService.Common;

namespace SampleValidService.Client
{
    public class SampleServiceClient : ISampleService
    {
        private SampleServiceProxy _publisher;
        private const string AgentId = "SampleValidService.Agent";

        public SampleServiceClient()
        {
            var config = GetConfigSource(AgentId + ".dll.config");
            var appSettingsSection = config.GetSection("appSettings") as AppSettingsSection;
            NetNamedPipeBinding publishingBinding = new NetNamedPipeBinding();
            publishingBinding.TransactionFlow = true;
            publishingBinding.Name = appSettingsSection.Settings["binding"].Value;
            publishingBinding.Namespace = appSettingsSection.Settings["bindingConfiguration"].Value; 

            var address = appSettingsSection.Settings["address"].Value;
            _publisher = new SampleServiceProxy(publishingBinding, new EndpointAddress(address));
        }

        public string DoSampleRequest(string sampleValue)
        {
         return   _publisher.DoSampleRequest(sampleValue);
        }

        private static FileConfigurationSource GetConfigSource(string configFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var directory = Directory.GetParent(assembly.Location).FullName;
            var path = Path.Combine(directory, configFileName);
            if (!File.Exists(path)) throw new ArgumentException("File with config not found.");
            var fileConfigurationSource = new FileConfigurationSource(path);
            return fileConfigurationSource;
        }

        public void Dispose()
        {
            _publisher.Close();
        }
    }
}
