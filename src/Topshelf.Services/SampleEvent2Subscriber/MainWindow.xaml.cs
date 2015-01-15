using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using Microsoft.Practices.Composite.Events;
using SampleEvents;
using Topshelf.Services.Events.Client;
//using Topshelf.Services.EventsAggregation;

namespace EventSubscriber
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
           // _transportClient = new TransportClient();
        }

        private void Subsribe_Click(object sender, RoutedEventArgs e)
        {
            if (_transportClient == null)
            {
                _transportClient = new CrossProcessTransportClient();
            }
            //var theEvent = _eventAggregator.GetEvent<ShortcutTransportEvent>();
            //theEvent.Subscribe(OnEvent);
            _transportClient.Subscribe("Event2", OnEvent);
        }

        private void OnEvent(string shortCut)
        {
            this.labelShortcut.Content = shortCut;
        }

        private void Unsubscribe_Click(object sender, RoutedEventArgs e)
        {
            if (_transportClient == null)
            {
                _transportClient = new CrossProcessTransportClient();
            }
           // var theEvent = _eventAggregator.GetEvent<TransportEvent<string>>();
            //theEvent.Unsubscribe(OnEvent);
            _transportClient.Unsubsribe(OnEvent);
        }

    }
}
