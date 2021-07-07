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
    public partial class AktCurrent : Form
    {
        public AktCurrent()
        {
            InitializeComponent();
        }
        private void Realiz_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Text = Form1.ds.Tables["АктРабот"].Rows[Vipraboti.n]["Дата"].ToString();
                comboBox1.Text = Form1.ds.Tables["АктРабот"].Rows[Vipraboti.n]["ФИО_сотрудника"].ToString();
                comboBox2.Text = Form1.ds.Tables["АктРабот"].Rows[Vipraboti.n]["ФИО_клиента"].ToString();
                textBox1.Text = Form1.ds.Tables["АктРабот"].Rows[Vipraboti.n]["Номер"].ToString();
                NpgsqlCommand command = Form1.connection.CreateCommand();
                command.CommandText = "SELECT CONCAT_WS(' ', family, name, otchestvo) AS fio FROM sotrudniki";
                Form1.connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) comboBox1.Items.Add(reader["fio"]);
                Form1.connection.Close();
                command = Form1.connection.CreateCommand();
                command.CommandText = "SELECT CONCAT_WS(' ', fam, name, surname) AS fio FROM client";
                Form1.connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read()) comboBox2.Items.Add(reader["fio"]);
                Form1.connection.Close();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Не указана редактируемая запись таблицы!!!", "Ошибка");
                Close();
                return;
            }
            textBox1.Enabled = false;
        }
        public static int n = -1;
        public static object kod = null;
        private void TourNow_FormClosed(object sender, EventArgs e)
        {
           Akty.n = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT kodsotrud FROM sotrudniki WHERE CONCAT_WS(' ', family, name, otchestvo) = '" + comboBox1.Text + "'";
            Form1.connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string kod1 = reader["kodsotrud"].ToString();
            Form1.connection.Close();
            command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT kodclienta FROM client WHERE CONCAT_WS(' ', fam, name, surname) = '" + comboBox2.Text + "'";
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            string kod2 = reader["kodclienta"].ToString();
            Form1.connection.Close();
            command = Form1.connection.CreateCommand();
            command.CommandText = "UPDATE aktviprabot SET date = '" + dateTimePicker1.Value.ToShortDateString() + "', kodsotrud = " + kod1 + ", kodclienta = " + kod2 + " WHERE kodakta = " + textBox1.Text;
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
            Form1.ds.Tables["АктРабот"].Rows.RemoveAt(Akty.n);
            Form1.ds.Tables["АктРабот"].Rows.Add(new object[] { textBox1.Text, dateTimePicker1.Value.ToShortDateString(), comboBox1.Text, comboBox2.Text });
        }
    }
}
