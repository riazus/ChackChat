using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ChatClient
{
    class DB
    {
        
        private const string ConnectionString = "Server=.\\SQLEXPRESS;Database=Messenger;Trusted_Connection=True;";
        SqlConnection connect = new SqlConnection(ConnectionString);

        public void openConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }
        public void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }
        public SqlConnection GetConnection()
        {
            return connect;
        }
    }
}
