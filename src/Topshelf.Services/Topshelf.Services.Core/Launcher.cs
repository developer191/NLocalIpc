using System;
using System.Diagnostics;
using System.Threading;
using Topshelf.Services.Daemon;

namespace Topshelf.Services.Core
{
    public class Launcher : ILauncher
    {
        private const string transportStartedName = "Topshelf.Services.Daemon";


        public void StartAgent()
        {
            EnsureDaemonRunning();
        }

        private void EnsureDaemonRunning()
        {
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset,
             transportStartedName);
            var type = typeof(Program);
            Console.WriteLine("Daemon run " + type);
            string location = type.Assembly.Location;
            ProcessStartInfo processStartInfo = new ProcessStartInfo(location);
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            // EventPublisher.Program.Main(null);
            waitHandle.WaitOne();
        }
    }
}