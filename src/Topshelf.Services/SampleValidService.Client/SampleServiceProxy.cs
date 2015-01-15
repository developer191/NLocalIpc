using System.ServiceModel;
using System.ServiceModel.Channels;
using SampleValidService.Common;

namespace SampleValidService.Client
{
    public class SampleServiceProxy : ClientBase<ISampleService>, ISampleService
    {
        public SampleServiceProxy(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        { }

        public string DoSampleRequest(string sampleValue)
        {
            return Channel.DoSampleRequest(sampleValue);
        }
    }
}
