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
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=Zuzannosoro123;database=usuarios");
        Thread th;
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 reg = new Form2();

            this.Close();
            th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void opennewform(object obj)
        {
            Application.Run(new Form2()); 
        }

        private void openMain()
        {
            Application.Run(new Main());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            String User = uName.Text;
            String pass = pWord.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from `users` where `uName` = @usn and `pWord` = @pass", db.getConnection());

            command.Parameters.Add("@usn",MySqlDbType.VarChar).Value = User;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                this.Close();
                th = new Thread(openMain);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                if (User.Trim().Equals(""))
                {
                    MessageBox.Show("Ingrese su usuario para iniciar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }else if(pass.Trim().Equals(""))
                {
                    MessageBox.Show("Ingrese su contraseña para iniciar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña Incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void pWord_Enter(object sender, EventArgs e)
        {

        }
    }
}
