using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        /*private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ConnectUser();
        }*/

        void ConnectUser()
        {
            if (!isConnected)
            {
                try
                {
                    string UserName = Convert.ToString(App.Current.Resources["UserName"]);
                    /*Authorization auth = new Authorization();
                    string UserName = auth.UserName;*/
                    client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                    ID = client.Connect(UserName);
                    bConnDicon.Content = "Disconnect";
                    isConnected = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка при подключению к серверу!\n" +
                        "Скажите админу, чтобы перезапустил сервер!");
                }
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                bConnDicon.Content = "Connect";
                isConnected = false;
            }

        }
        private void bConnDicon_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }
        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        /*private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }*/

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
            }
        }

        private void bClose_Click(object sender, RoutedEventArgs e)
        {
            DisconnectUser();
            Application.Current.Shutdown();
        }

        private void bHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Это основное окно\n" +
            "Чтобы отправить сообщение, сначало нужно подключится к серверу по кнопке 'Connect'\n" +
            "Затем нужно набрать сообщение в нижнем поле\n" +
            "Верхнее поле служит для просмотра сообщений");
        }

        /*private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
                bConnDicon_Click(sender, e);
        }*/
    }
}
