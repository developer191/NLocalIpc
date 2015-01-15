using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Composite.Events;
using SampleEvents;
using Topshelf.Services.Events.Client;
using Topshelf.Services.Events.Common;

namespace SampleSubscribe2
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
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //_eventAggregator = new EventAggregator();
            //_transportClient = new TransportClient();
        }

        private void Subsribe_Click(object sender, RoutedEventArgs e)
        {
            //var theEvent = _eventAggregator.GetEvent<ContextChangedTransportEvent>();
            //theEvent.Subscribe(OnEvent);
            if (_transportClient == null)
            {
                _transportClient = new CrossProcessTransportClient();
            }
            _transportClient.Subscribe("Event1", OnEvent);

        }

        private void OnEvent(string @event)
        {
            this.labelContext.Content = @event;
        }

        private void Unsubscribe_Click(object sender, RoutedEventArgs e)
        {
            //var theEvent = _eventAggregator.GetEvent<ContextChangedTransportEvent>();
            //theEvent.Unsubscribe(OnEvent);
            if (_transportClient == null)
            {
                _transportClient = new CrossProcessTransportClient();
            }
            _transportClient.Unsubsribe(OnEvent);

        }
    }
}
