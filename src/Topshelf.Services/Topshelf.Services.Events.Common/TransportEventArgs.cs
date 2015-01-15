using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Topshelf.Services.Events.Common
{
    [DataContract]
    public class TransportEventArgs
    {
        public TransportEventArgs(EventType type, string value)
        {
            Type = type;
            Value = value;
        }

        [DataMember]
        public EventType Type { get; private set; }

        [DataMember]
        public string Value { get; private set; }
    }


}
