using System;
using System.ServiceModel;

namespace SampleValidService.Common
{
    [ServiceContract]
    public interface ISampleService:IDisposable
    {
        [OperationContract]
        string DoSampleRequest(string sampleValue);
    }


}