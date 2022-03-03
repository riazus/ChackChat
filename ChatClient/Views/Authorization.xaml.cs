using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        /*public string UserName { get; set; }*/

        private void bEntry_Click(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text.Length > 0
                && tbLogin.Text.Length > 0)
            {
                App.Current.Resources["UserName"] = tbLogin.Text;
                /*UserName = tbLogin.Text;*/
                string loginUser = tbLogin.Text;
                string passwordUser = tbPassword.Text;

                DB db = new DB();
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("SELECT * FROM Users " +
                    "WHERE login = @uL AND password = @uP",
                    db.GetConnection());
                command.Parameters.Add("@uL", SqlDbType.VarChar).Value = loginUser;
                command.Parameters.Add("@uP", SqlDbType.VarChar).Value = passwordUser;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                /*
                 * Проблема в сравнении пароля.
                 * Даже если логин правильный, а пароль нет, то все равно выведет
                 * отсутствие аккаунта
                 */
                if (table.Rows.Count > 0)
                {
                    this.Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Кажется, у вас нет аккаунта...\nПожалуйста, зарегистрируйтесь.");
            }
            else
                MessageBox.Show("Проверьте корректность данных!");
        }
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                bEntry_Click(sender, e);
        }
        private void bNonAcc_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registr = new Registration();
            registr.Show();
            this.Close();
        }

        private void bClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Коротко об этом приложении...\n" +
            "Это мини-меесенджер, который позволяет подключится к одному единственному чату\n" +
            "Чат из себя представляет большой 'котёл'\n" +
            "Сообщения видны всем пользователям\n" +
            "Убедительная просьба не писать оскорбления и иные...\n" +
            "Неуважительные сообщения другим пользователям\n" +
            "Спасибо, что решили потратить свое время\n" +
            "Просто наслаждайтесь...\n" +
            "Developed by RiazUs");
        }

        /*private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }*/
    }
}
