using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Topshelf.Services.Common;

namespace Topshelf.Services.Daemon
{
    public class AgentCatalogBuilder
    {
        public const string AgentAssemblyNamingConvension = "*.Agent.dll";

        public string AgentsDirectory = Environment.CurrentDirectory;

        //public object[] GetAgents()
        //{
        //    var agentTypes = GetAgentsTypes();
        //    var agents = new List<object>();
        //    foreach (Type type in agentTypes)
        //    {
        //        object agent = type.CreateInstance(null);
        //        agents.Add(agent);
        //    }
        //    return agents.ToArray();
        //}

        public Type[] GetAgentTypes()
        {
            AgentsDirectory = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            string[] agentAssemblies = Directory.GetFiles(AgentsDirectory, AgentAssemblyNamingConvension);
            List<Type> agentTypes = new List<Type>();
            foreach (string agentAssembly in agentAssemblies)
            {
                Assembly assembly =  Assembly.LoadFile(agentAssembly);
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    object[] agentAttributes = type.GetCustomAttributes(typeof (AgentAttribute), false);
                    if (agentAttributes.Length >=1)
                    {
                        agentTypes.Add(type);
                    }
                    //TODO: validate for parameter less constructor
                    //TODO: validate that type if WCF type
                }
            }
            return agentTypes.ToArray();
        }
    }
}
