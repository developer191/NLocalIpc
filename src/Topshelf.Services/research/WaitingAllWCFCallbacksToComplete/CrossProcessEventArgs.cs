using System;
using System.Runtime.Serialization;

namespace WaitingAllWCFCallbacksToComplete
{
    [DataContract]
    public class CrossProcessEventArgs : EventArgs
    {


        public CrossProcessEventArgs(string data, Guid id)
        {
            this.Data = data;
            this.Id = id;
        }

        [DataMember]
        public string Data { get; set; }

        [DataMember]
        public Guid Id { get; set; }


    }
}