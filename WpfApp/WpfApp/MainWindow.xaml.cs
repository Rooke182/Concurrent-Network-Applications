using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ClientProj;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client m_client;
        public MainWindow(Client client)
        {
            InitializeComponent();
            m_client = client;
        }

        public void UpdateChatBox(string message)
        {
            chatBox.Dispatcher.Invoke(() =>
            {
                chatBox.Text = message + Environment.NewLine;
            });
        }

        public void sendMessageButton_Click(object sender, EventArgs e)
        {
            m_client.SendMessage();
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (messageText.Text == "")
            {
                MessageBox.Show("No message in Text Box!", "Warning");
            }
            else
            {
                string message = messageText.Text;
                messageText.Text = "";
                if (localName.Text == "")
                {
                    MessageBox.Show("Please enter a name into the name box", "Warning");
                    messageText.Text = message;
                }
                else
                {
                    string name = localName.Text;
                    SendMessage(name + " says: " + message + "\n");
                }
            }

        }

        private void SendMessage(string status)
        {
            chatBox.Text += status;
        }

        private void messageText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
