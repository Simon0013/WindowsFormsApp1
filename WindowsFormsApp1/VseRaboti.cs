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
    public partial class VseRaboti : Form
    {
        public VseRaboti()
        {
            InitializeComponent();
        }
        private void VseRaboti_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Text = Form1.ds.Tables["ВыполненныеРаботы"].Rows[Vipraboti.n]["Вид_работы"].ToString();
                comboBox2.Text = Form1.ds.Tables["ВыполненныеРаботы"].Rows[Vipraboti.n]["Номер_акта"].ToString();
                textBox1.Text = Form1.ds.Tables["ВыполненныеРаботы"].Rows[Vipraboti.n]["Объём"].ToString();
                NpgsqlCommand command = Form1.connection.CreateCommand();
                command.CommandText = "SELECT nazvanie FROM raboti";
                Form1.connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) comboBox1.Items.Add(reader["nazvanie"]);
                Form1.connection.Close();
                command = Form1.connection.CreateCommand();
                command.CommandText = "SELECT kodakta FROM aktviprabot";
                Form1.connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read()) comboBox2.Items.Add(reader["kodakta"]);
                Form1.connection.Close();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Не указана редактируемая запись таблицы!!!", "Ошибка");
                Close();
                return;
            }
        }
        public static int n = -1;
        public static string kod;
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT kodrabot FROM raboti WHERE nazvanie = '" + comboBox1.Text + "'";
            Form1.connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            kod = reader["kodrabot"].ToString();
            Form1.connection.Close();
            command = Form1.connection.CreateCommand();
            command.CommandText = "UPDATE rabota_po_akty SET kodakta = " + comboBox2.Text + ", obiem_rabot = '" + textBox1.Text + "' WHERE kodrabot = " + kod;
            Form1.connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ne)
            {
                MessageBox.Show(ne.Message, "Ошибка");
                Form1.connection.Close();
                return;
            }
            Form1.connection.Close();
            Form1.ds.Tables["ВыполненныеРаботы"].Rows.RemoveAt(Vipraboti.n);
            Form1.ds.Tables["ВыполненныеРаботы"].Rows.Add(new object[] { kod, comboBox2.Text, comboBox1.Text, textBox1.Text });
        }
        private void VseRaboti_FormClosed(object sender, FormClosedEventArgs e)
        {
            Vipraboti.n = -1;
        }
    }
}
