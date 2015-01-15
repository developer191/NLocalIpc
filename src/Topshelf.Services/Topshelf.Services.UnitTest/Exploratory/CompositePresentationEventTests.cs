using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Topshelf.Services.UnitTest.Exploratory
{
    [TestClass]
    public class CompositePresentationEventTests
    {
        [TestMethod]
        public void SingleSubscriberAndPublisher_subcribeAndPublish_actionInvoked()
        {
            string value = null;
            IEventAggregator eventAggregator = new EventAggregator();
            eventAggregator.GetEvent<CompositePresentationEvent<string>>().Subscribe(x => { value = x; });

            eventAggregator.GetEvent<CompositePresentationEvent<string>>().Publish("test value");

            value.Should().BeEquivalentTo("test value");
        }
    }
}
