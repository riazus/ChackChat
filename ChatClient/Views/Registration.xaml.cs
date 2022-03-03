using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;

namespace ChatClient
{
    public partial class Registration : Window
    {
        public string ViewModel { get; set; }
        
        public Registration()
        {
            InitializeComponent();
        }
        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }
        private void bNewUser_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text.Length > 0
                && tbSecondName.Text.Length > 0
                && tbNewPassword.Text.Length > 0
                && tbNewLogin.Text.Length > 0)
            {
                string loginUser = tbNewLogin.Text;
                string passwordUser = tbNewPassword.Text;
                string FirstName = tbFirstName.Text;
                string LastName = tbSecondName.Text;

                DB db = new DB();
                SqlCommand command = new SqlCommand("INSERT Users VALUES (@uL, @uP, @uFN, @uLN)",
                    db.GetConnection());

                command.Parameters.Add("@uL", SqlDbType.VarChar).Value = loginUser;
                command.Parameters.Add("@uP", SqlDbType.VarChar).Value = passwordUser;
                command.Parameters.Add("@uFN", SqlDbType.VarChar).Value = FirstName;
                command.Parameters.Add("@uLN", SqlDbType.VarChar).Value = LastName;

                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Aккаунт был создан!");
                    this.Hide();
                    Authorization authWindow = new Authorization();
                    authWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Аккаунт не был создан!");
                db.closeConnection();
            }
            else
                MessageBox.Show("Проверьте корректность данных!");
        }

        private void tbNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                bNewUser_Click(sender, e);
        }   
        private void bClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
