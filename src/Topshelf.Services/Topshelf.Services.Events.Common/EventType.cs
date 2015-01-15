using System.Runtime.Serialization;

namespace Topshelf.Services.Events.Common
{
    [DataContract]
    public class EventType
    {
        public EventType(string name)
        {
            //Contract.Requires( !StringExtensions.IsNullOrWhiteSpace(name))
            Name = name;
        }

        [DataMember]
        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("Name: {0}", Name);
        }

        public bool Equals(EventType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (EventType)) return false;
            return Equals((EventType) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}