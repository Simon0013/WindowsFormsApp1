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
    public partial class Sotrudnik : Form
    {
        public Sotrudnik()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["kodsotrud"].ToString();
            textBox2.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["family"].ToString();
            textBox3.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["name"].ToString();
            textBox4.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["otchestvo"].ToString();
            textBox5.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["doljnost"].ToString();
            textBox6.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["adres"].ToString();
            textBox7.Text = Form1.ds.Tables["sotrudniki"].Rows[n]["telephone"].ToString();
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT nazvanfirm FROM firm WHERE kodfirm = " + Form1.ds.Tables["sotrudniki"].Rows[n]["firm_kodfirm"].ToString();
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            comboBox1.Text = reader["nazvanfirm"].ToString();
            Form1.connection.Close();
            textBox1.Enabled = false;
        }
        public void FiledsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
            textBox1.ReadOnly = false;
        }
        int n = 0;
        private void Sotrudnik_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT * FROM sotrudniki", Form1.connection);
            if (Form1.ds.Tables["sotrudniki"] != null) Form1.ds.Tables["sotrudniki"].Clear();
            da1.Fill(Form1.ds, "sotrudniki");
            Form1.connection.Close();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT nazvanfirm FROM firm";
            Form1.connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) comboBox1.Items.Add(reader["nazvanfirm"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["sotrudniki"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string kodfirm;
            Form1.connection.Open();
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT kodfirm FROM firm WHERE nazvanfirm = '" + comboBox1.Text + "'";
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            kodfirm = reader["kodfirm"].ToString();
            Form1.connection.Close();
            if (n == Form1.ds.Tables["sotrudniki"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO sotrudniki VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', " + kodfirm + ")";
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
                Form1.ds.Tables["sotrudniki"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, kodfirm });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE sotrudniki SET family = '" + textBox2.Text + "', name = '" + textBox3.Text + "', otchestvo = '" + textBox4.Text + "', doljnost = '" + textBox5.Text + "', adres = '" + textBox6.Text + "', telephone = '" + textBox7.Text + "', firm_kodfirm = " + kodfirm + " WHERE kodsotrud = " + textBox1.Text;
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
                Form1.ds.Tables["sotrudniki"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, kodfirm });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки сотрудника " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM sotrudniki WHERE kodsotrud = " + textBox1.Text;
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["sotrudniki"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["sotrudniki"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["sotrudniki"].Rows.Count) n++;
            if (Form1.ds.Tables["sotrudniki"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["sotrudniki"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["sotrudniki"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
