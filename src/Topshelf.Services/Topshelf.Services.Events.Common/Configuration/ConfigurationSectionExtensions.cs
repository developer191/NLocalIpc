using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Topshelf.Services.Events.Common.Configuration
{
    public abstract class ConfigurationSectionBase : ConfigurationSection
    {
        protected T Get<T>(string propertyName)
        {
            return (T)this[propertyName];
        }

        protected void Set<T>(string propertyName, T value)
        {
            this[propertyName] = value;
        }
    }
}
