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
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }
        public static bool Modification_Execute(string sql)
        {
            NpgsqlCommand command;
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("Обновление БД не было выполнено из-за не указания обновленных данных или несоответствия их типов!!!", "Ошибка");
                Form1.connection.Close();
                return false;
            }
            Form1.connection.Close();
            return true;
        }
        private void Password_Load(object sender, EventArgs e)
        {
            string sql;
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT login FROM polzovateli ORDER BY login";
            command = new NpgsqlCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader["login"]);
            Form1.connection.Close();
            textBox1.UseSystemPasswordChar = true;
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT password FROM polzovateli WHERE login = '" + comboBox1.Text + "'";
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            NpgsqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            if (textBox1.Text == dataReader["password"].ToString())
            {
                Form1.connection.Close();
                if (textBox2.Text == textBox3.Text)
                {
                    sql = "UPDATE polzovateli SET Password = '" + textBox2.Text + "' WHERE login = '" + comboBox1.Text + "'";
                    Modification_Execute(sql);
                    Close();
                }
                else
                    MessageBox.Show("Неверное подтверждение пароля");
            }
            else
                MessageBox.Show("Неправильный старый пароль");
        }
    }
}