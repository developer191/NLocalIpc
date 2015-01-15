using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleValidService.Common;
using Topshelf.Services.Common;
using System.Configuration;
namespace SampleValidService.Agent
{
    [Agent("SampleValidService.Agent")]
    public class SampleService : ISampleService
    {
        public string DoSampleRequest(string sampleValue)
        {
            string readed = ConfigurationManager.AppSettings["thirdPartyConfig"].ToString();
            return sampleValue
                + Environment.NewLine
                + readed;
        }

        public void Dispose()
        {
            
        }
    }
}
