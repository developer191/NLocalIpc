using System;
using System.Runtime.Serialization;

namespace WaitingAllWCFCallbacksToComplete
{
    [DataContract]
    public class CrossProcessEventMessage
    {
        public CrossProcessEventMessage(string sender, string data, Guid id)
        {
            Sender = sender;
            Data = data;
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public string Data { get; set; }
    }
}