using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel.Channels;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Topshelf.Services.Events.Common.Configuration
{
    public class TransportConfigProvider : ITransportConfigProvider
    {
        private FileConfigurationSource _fileConfigurationSource;

        public TransportConfigProvider(string pathToConfigurationFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var directory = Directory.GetParent(assembly.Location).FullName;
            var path = Path.Combine(directory, pathToConfigurationFile);
            if (!File.Exists(path)) throw new ArgumentException("File with config not found.");
            _fileConfigurationSource = new FileConfigurationSource(path);
        }

        public TransportSettings Read()
        {
            const string name = "Topshelf.Services.Events.Configuration";
            var section = _fileConfigurationSource.GetSection(name);
            //var section = _customConfigManager.Configuration.GetSection(name); ;
            TransportSettings transportSettings = (section as TransportSettings);
            return transportSettings;
        }
    }
}