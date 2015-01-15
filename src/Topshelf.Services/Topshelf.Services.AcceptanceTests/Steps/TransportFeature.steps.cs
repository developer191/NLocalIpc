using System;
using System.Threading;
using FluentAssertions;
using FluentAssertions.Assertions;
using SampleEvents;
using TechTalk.SpecFlow;
using Topshelf.Services.Core;
using Topshelf.Services.Events.Client;

namespace Topshelf.Services.AcceptanceTests.Steps
{
    [Binding]
    public class EasyAndFlexibleConfigurationSteps
    {


        private ITransportClient _transport;
        private AutoResetEvent _waitHandle;
        private string _value;


        [Given(@"Daemon loaded")]
        public void GivenDaemonLoaded()
        {
            //InProcessLauncher
            ILauncher launcher = new Launcher();
            launcher.StartAgent();
        }

        [Given(@"Subscribe on ""Shortcut"" event")]
        public void GivenSubscribeOnShortcutEvent()
        {
            _transport = new CrossProcessTransportClient();
            _waitHandle = new AutoResetEvent(false);
            _transport.Subscribe("Shortcut", Wait);
            
        }

        private void Wait(string obj)
        {
            _value = obj;
            _waitHandle.Set();
        }

        [Then(@"Subscriber notified")]
        public void ThenSubsriberNotified()
        {
            _waitHandle.WaitOne();
           _value.Should().BeEquivalentTo("Event1");
        }

        [When(@"""Shortcut"" event published")]
        public void WhenShortcutEventPublished()
        {
            _transport.Publish("Shortcut", "Event1");
        }

        //run exe
        //creat trunsport
        //subsrive 
        // sleep for time out from XML
        //if reached lust method - means was notified
    }
}