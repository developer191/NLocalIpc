using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using FluentAssertions;
using SampleValidService.Client;
using TechTalk.SpecFlow;

using Topshelf.Services.Core;
using Topshelf.Services.Events.Client;
using Topshelf.Services.Events.Common;

namespace Topshelf.Services.AcceptanceTests.Steps
{
    [Binding]
    public class TransportConfigurationSteps
    {
        private SampleServiceClient client;

        [Given(@"Daemon is alive")]
        public void GivenDaemonIsElive()
        {

        }

        [Given(@"Daemon run with Config read form separated file")]
        public void GivenDaemonRunWithConfigReadFormSeparatedFile()
        {
            ILauncher launcher = new Launcher();
            launcher.StartAgent();
            ScenarioContext.Current.Set(launcher);
        }

        [Given(@"I do Sample Request")]
        public void GivenISubscribe()
        {
            client = new SampleServiceClient();
            var response = client.DoSampleRequest("Sample Request");
            ScenarioContext.Current.Set(response);

        }

        [Then(@"I get produced Response with Sample Request")]
        public void ThenEverythingIsOk()
        {
            var response =  ScenarioContext.Current.Get<string>();
            response.Should().Contain("Sample Request");
        }

        [Then(@"Response contains info from pointed Config")]
        public void EverythingIsOk()
        {
            var response = ScenarioContext.Current.Get<string>();
            response.Should().Contain("Third party config");
            client.Dispose();
        }

        
    }

}
