using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp1
{
    public partial class Avtorizacia : Form
    {
        public Avtorizacia()
        {
            InitializeComponent();
        }
        private void Avtorizacia_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            string sql = "SELECT login FROM polzovateli ORDER BY login";
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader["login"]);
            Form1.connection.Close();
            textBox1.UseSystemPasswordChar = true;
        }
        public static string root = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.connection.Open();
            string sql = "SELECT password FROM polzovateli WHERE login = '" + comboBox1.Text + "'";
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            if (textBox1.Text == dataReader["password"].ToString())
            {
                Form1.connection.Close();
                if (comboBox1.Text == "Admin")
                    root = "Администратор";
                Hide();
                Form1 sot = new Form1();
                sot.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Неправильный пароль");
            }
            Form1.connection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox1.UseSystemPasswordChar = false;
            else
                textBox1.UseSystemPasswordChar = true;
        }
    }
}
