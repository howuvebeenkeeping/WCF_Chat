using ChatClient.ServiceChat;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel;

namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        private bool isConnected = false;
        private ServiceChatClient client;
        private int id;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new InstanceContext(this));
                id = client.Connect(textboxUserName.Text);
                textboxUserName.IsEnabled = false;
                btnConnectDisconnect.Content = "Disconnect";
                isConnected = true;
            }
        }

        private void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(id);
                client = null;
                textboxUserName.IsEnabled = true;
                btnConnectDisconnect.Content = "Connect";
                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected) DisconnectUser();
            else ConnectUser();

        }

        void IServiceChatCallback.MessageCallback(string message)
        {
            labelChat.Items.Add(message);
            labelChat.ScrollIntoView(labelChat.Items[labelChat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(textboxMessage.Text, id);
                    textboxMessage.Text = string.Empty;
                }
            }
        }
    }
}
