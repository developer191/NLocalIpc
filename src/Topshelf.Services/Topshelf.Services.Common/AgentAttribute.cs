using System;

namespace Topshelf.Services.Common
{
    public class AgentAttribute : Attribute
    {
        public AgentAttribute(string uid)
        {
            Uid = uid;
        }

        public string Uid { get; private set; }
    }
}