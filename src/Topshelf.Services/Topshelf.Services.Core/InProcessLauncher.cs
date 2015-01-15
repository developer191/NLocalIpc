using Topshelf.Services.Daemon;

namespace Topshelf.Services.Core
{
    public class InProcessLauncher : ILauncher
    {
       
        public void StartAgent()
        {
            EnsureDaemonRunning();
        }

        private void EnsureDaemonRunning()
        {
            Program.Run();
        }
    }
}