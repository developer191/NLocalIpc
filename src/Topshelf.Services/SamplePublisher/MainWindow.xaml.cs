using System.Windows;

using Microsoft.Practices.Composite.Events;
using SampleEvents;
using Topshelf.Services.Events.Client;
//using Topshelf.Services.EventsAggregation;

namespace SamplePublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEventAggregator _eventAggregator;
        private ITransportClient _transportClient;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //_eventAggregator = new EventAggregator();
            comboBox.SelectedIndex = 0;
          //  _transport = new TransportClient();

        }

        private void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            string value = textBox.Text;
            if (_transportClient == null)
            {
                _transportClient = new CrossProcessTransportClient();
            }

            if (comboBox.SelectedIndex == 0)
            {
                //var ribbon = _eventAggregator.GetEvent<RibbonClickTransportEvent>();
                //ribbon.Publish(value);
                _transportClient.Publish("NoSubscriber", value);
            }
            if (comboBox.SelectedIndex == 1)
            {
                //var ribbon = _eventAggregator.GetEvent<ShortcutTransportEvent>();
                //ribbon.Publish(value);
                _transportClient.Publish("Event1", value);
            }
            if (comboBox.SelectedIndex == 2)
            {
                //var ribbon = _eventAggregator.GetEvent<ContextChangedTransportEvent>();
                //ribbon.Publish(value);
                _transportClient.Publish("Event2", value);
            }


        }


    }
}
