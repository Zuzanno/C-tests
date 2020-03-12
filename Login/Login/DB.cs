using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Login
{

    class DB
    {
        private MySqlConnection con = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=Zuzannosoro123;database=usuarios");


        public void opencon()
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void closecon()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }


        public MySqlConnection getConnection()
        {
            return con;
        }
    }
}
