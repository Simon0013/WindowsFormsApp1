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
    public partial class Rabota : Form
    {
        public Rabota()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["raboti"].Rows[n]["kodrabot"].ToString();
            textBox2.Text = Form1.ds.Tables["raboti"].Rows[n]["nazvanie"].ToString();
            textBox3.Text = Form1.ds.Tables["raboti"].Rows[n]["cena"].ToString();
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS (' ', family, name, otchestvo) AS fiosotrud FROM sotrudniki WHERE kodsotrud = " + Form1.ds.Tables["raboti"].Rows[n]["kodsotrud"].ToString();
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            comboBox1.Text = reader["fiosotrud"].ToString();
            Form1.connection.Close();
            textBox1.Enabled = false;
        }
        public void FiledsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
            textBox1.ReadOnly = false;
        }
        int n = 0;
        private void Rabota_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT * FROM raboti", Form1.connection);
            if (Form1.ds.Tables["raboti"] != null) Form1.ds.Tables["raboti"].Clear();
            da1.Fill(Form1.ds, "raboti");
            Form1.connection.Close();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS (' ', family, name, otchestvo) AS fiosotrud FROM sotrudniki";
            Form1.connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) comboBox1.Items.Add(reader["fiosotrud"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["raboti"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string kodsotrud;
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT kodsotrud FROM sotrudniki WHERE CONCAT_WS (' ', family, name, otchestvo) = '" + comboBox1.Text + "'";
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            kodsotrud = reader["kodsotrud"].ToString();
            Form1.connection.Close();
            if (n == Form1.ds.Tables["raboti"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO raboti VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', " + kodsotrud + ")";
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
                Form1.ds.Tables["raboti"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text, kodsotrud });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE raboti SET nazvanie = '" + textBox2.Text + "', cena = '" + textBox3.Text + "', kodsotrud = " + kodsotrud + " WHERE kodrabot = " + textBox1.Text;
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
                Form1.ds.Tables["raboti"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text, kodsotrud });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки услугу " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM raboti WHERE kodrabot = " + textBox1.Text;
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["raboti"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["raboti"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["raboti"].Rows.Count) n++;
            if (Form1.ds.Tables["raboti"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["raboti"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["raboti"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
