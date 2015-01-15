using System.Diagnostics;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace Topshelf.Services.AcceptanceTests.Events
{
    [Binding]
    public class TranportEvents
    {
        [AfterScenario]
        public void AfterScenarion()
        {
            var daemons = Process.GetProcesses().Where(x => x.ProcessName == "Topshelf.Services.Daemon");
            Thread.Sleep(1000);
            foreach (var daemon in daemons)
            {
                   daemon.Kill();
            }
        }
    }
}