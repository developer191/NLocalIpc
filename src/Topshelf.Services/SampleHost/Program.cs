using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf.Services.Common;
using Topshelf.Services.Core;

namespace SampleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ILauncher launcher = new Launcher();
            launcher.StartAgent();
        }
    }
}
