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
    public partial class Firma : Form
    {
        public Firma()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["firm"].Rows[n]["kodfirm"].ToString();
            textBox2.Text = Form1.ds.Tables["firm"].Rows[n]["nazvanfirm"].ToString();
            textBox3.Text = Form1.ds.Tables["firm"].Rows[n]["timerabot"].ToString();
            textBox4.Text = Form1.ds.Tables["firm"].Rows[n]["director"].ToString();
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS (' ', fam, name, surname) AS fioclient FROM client WHERE kodclienta = " + Form1.ds.Tables["firm"].Rows[n]["kodclienta"].ToString();
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            comboBox1.Text = reader["fioclient"].ToString();
            Form1.connection.Close();
            textBox1.Enabled = false;
        }
        public void FiledsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
            textBox1.ReadOnly = false;
        }
        int n = 0;
        private void Firma_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT * FROM firm", Form1.connection);
            if (Form1.ds.Tables["firm"] != null) Form1.ds.Tables["firm"].Clear();
            da1.Fill(Form1.ds, "firm");
            Form1.connection.Close();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS (' ', fam, name, surname) AS fioclient FROM client";
            Form1.connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) comboBox1.Items.Add(reader["fioclient"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["firm"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string kodclienta;
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT kodclienta FROM client WHERE CONCAT_WS (' ', family, name, otchestvo) = '" + comboBox1.Text + "'";
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            kodclienta = reader["kodclienta"].ToString();
            Form1.connection.Close();
            if (n == Form1.ds.Tables["firm"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO firm VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', " + kodclienta + ", '" + textBox3.Text + "')";
                Form1.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException)
                {
                    MessageBox.Show("Добавление экземпляра не было успешно проведено из-за неуказания его данных или" + " несоответствия их типов или попытки добавить экземпляр с уже используемым кодом!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                textBox1.Enabled = false;
                Form1.ds.Tables["firm"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text, kodclienta, textBox4.Text });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE firm SET nazvanfirm = '" + textBox2.Text + "', timerabot = '" + textBox3.Text + "', director = '" + textBox4.Text + "', kodclienta = " + kodclienta + " WHERE kodrabot = " + textBox1.Text;
                Form1.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException)
                {
                    MessageBox.Show("Изменения не были успешно сохранены из-за несовпадения типов значений!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                Form1.ds.Tables["firm"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text, kodclienta, textBox4.Text });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки фирму " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM firm WHERE kodfirm = " + textBox1.Text;
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["firm"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["firm"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
            else
            {
                FiledsForm_Clear();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (n > 0)
            {
                n--;
                FiledsForm_Fill();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (n < Form1.ds.Tables["firm"].Rows.Count) n++;
            if (Form1.ds.Tables["firm"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["firm"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["firm"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
