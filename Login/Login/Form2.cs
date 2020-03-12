using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Login
{
    public partial class Form2 : Form
    {

        Thread th;
        public Form2()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 ini = new Form1();

            this.Close();
            th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void opennewform(object obj)
        {
            Application.Run(new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("insert into `users` (`eMail`, `uName`, `pWord` ) values (@email,@usn,@pass);", db.getConnection());
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = eMail.Text;
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = uName.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pWord.Text;

            db.opencon();

            if (!checkTextBox())
            {
                if (checkuName())
                {
                    MessageBox.Show("Este usuario ya existe!","Usuario duplicado",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }
                else
                {

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Cuenta creada con exito","Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error","Informacion vacia", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese informacion primero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

            db.closecon();
        }

        public Boolean checkuName()
        {
            DB db = new DB();

            String User = uName.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from `users` where `uName` = @usn", db.getConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = User;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean checkTextBox()
        {
            String user = uName.Text;
            String pass = pWord.Text;
            String correo = eMail.Text;

            if (user.Equals("") || pass.Equals("") || correo.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void eMail_Enter(object sender, EventArgs e)
        {

        }
    }
}
