using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WaitingAllWCFCallbacksToComplete
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost _host;

        private EventsClient client1;

        private EventsClient client2;

        private IEventsService _eventsService;

        private EventsClient client3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _eventsService = new EventsServiceHost();
            _host = new ServiceHost(_eventsService, new Uri("net.pipe://localhost/WaitingAllWCFCallbacksToComplete.IEventsService"));
            _host.AddServiceEndpoint(
                "WaitingAllWCFCallbacksToComplete.IEventsService",
                new NetNamedPipeBinding("NamedPipeBinding"),
                "net.pipe://localhost/WaitingAllWCFCallbacksToComplete.IEventsService");
            
            _host.Open();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (client1!=null) return;
            client1 = new EventsClient();
            client1.EventRaised += new EventHandler<CrossProcessEventArgs>(client1_EventRaised);
        }

        void client1_EventRaised(object sender, CrossProcessEventArgs e)
        {

            Thread.Sleep(2000);
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
              
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            client2 = new EventsClient();
            client2.EventRaised += new EventHandler<CrossProcessEventArgs>(client2_EventRaised);

       }

        private void button5_Click(object sender, RoutedEventArgs e)
        {

            client3 = new EventsClient();
            client3.EventRaised += new EventHandler<CrossProcessEventArgs>(client3_EventRaised);
        }

        private Random random = new Random();

        private void client3_EventRaised(object sender, CrossProcessEventArgs e)
        {
            Thread.Sleep(4000);
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        private void client2_EventRaised(object sender, CrossProcessEventArgs e)
        {
            Thread.Sleep(2000);
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    client1.RaiseAndWaitProcessed(new CrossProcessEventArgs("data 1", Guid.NewGuid()));   
                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.Elapsed);
                });
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                client2.RaiseAndWaitProcessed(new CrossProcessEventArgs("data 2", Guid.NewGuid()));
                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);
            });
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            button4_Click(sender, e);
            button6_Click(sender, e);
        }
    }
}
