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
    public partial class AdresSotrud : Form
    {
        public AdresSotrud()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["adressotr"].Rows[n]["area"].ToString();
            textBox2.Text = Form1.ds.Tables["adressotr"].Rows[n]["city"].ToString();
            textBox3.Text = Form1.ds.Tables["adressotr"].Rows[n]["street"].ToString();
            textBox4.Text = Form1.ds.Tables["adressotr"].Rows[n]["house"].ToString();
            textBox5.Text = Form1.ds.Tables["adressotr"].Rows[n]["index"].ToString();
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS (' ', family, name, otchestvo) AS fiosotrud FROM sotrudniki WHERE kodsotrud = " + Form1.ds.Tables["adressotr"].Rows[n]["kodsotrud"].ToString();
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            comboBox1.Text = reader["fiosotrud"].ToString();
            Form1.connection.Close();
        }
        public void FiledsForm_Clear()
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }
        int n = 0;
        private void AdresSotrud_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT * FROM adressotr", Form1.connection);
            if (Form1.ds.Tables["adressotr"] != null) Form1.ds.Tables["adressotr"].Clear();
            da1.Fill(Form1.ds, "adressotr");
            Form1.connection.Close();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS (' ', family, name, otchestvo) AS fiosotrud FROM sotrudniki";
            Form1.connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) comboBox1.Items.Add(reader["fiosotrud"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["adressotr"].Rows.Count > n)
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
            if (n == Form1.ds.Tables["adressotr"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO adressotr VALUES (" + kodsotrud + ", '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "')";
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
                Form1.ds.Tables["adressotr"].Rows.Add(new object[] { kodsotrud, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE adressotr SET area = '" + textBox1.Text + "', city = '" + textBox2.Text + "', street = '" + textBox3.Text + "', house = '" + textBox4.Text + "', index = '" + textBox5.Text + "' WHERE kodsotrud = " + kodsotrud;
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
                Form1.ds.Tables["adressotr"].Rows.Add(new object[] { kodsotrud, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки адрес сотрудника " + Form1.ds.Tables["adressotr"].Rows[n]["kodsotrud"].ToString() + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM adressotr WHERE kodsotrud = " + Form1.ds.Tables["adressotr"].Rows[n]["kodsotrud"].ToString();
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["adressotr"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["adressotr"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["adressotr"].Rows.Count) n++;
            if (Form1.ds.Tables["adressotr"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["adressotr"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["adressotr"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}