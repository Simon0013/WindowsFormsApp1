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
    public partial class SotrudPoisk : Form
    {
        public SotrudPoisk()
        {
            InitializeComponent();
        }
        private void SotrudPoisk_Load(object sender, EventArgs e)
        {
            string sql;
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT doljnost FROM sotrudniki";
            command = new NpgsqlCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader["doljnost"]);
            Form1.connection.Close();
            sql = "SELECT nazvanfirm FROM firm";
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader["nazvanfirm"]);
            Form1.connection.Close();
            sql = "SELECT CONCAT_WS (', ', area, city, street, house, index) FROM adressotr";
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox3.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT sotrudniki.kodsotrud AS Номер, family AS Фамилия, name AS Имя, otchestvo AS Отчество, doljnost AS Должность, telephone AS Телефон, nazvanfirm AS Фирма, CONCAT_WS (', ', area, city, street, house, index) AS Адрес FROM sotrudniki, firm, adressotr WHERE kodfirm = firm_kodfirm AND adressotr.kodsotrud = sotrudniki.kodsotrud";
            Vipraboti.Table_Fill("ПоискСотрудников", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ПоискСотрудников"];
            dataGridView1.Columns["Номер"].Visible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Enabled = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ds.Tables["ПоискСотрудников"].DefaultView.RowFilter = "";
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Form1.ds.Tables["ПоискСотрудников"].DefaultView.RowFilter = "Должность = '" + comboBox1.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                Form1.ds.Tables["ПоискСотрудников"].DefaultView.RowFilter = "Фирма = '" + comboBox2.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                Form1.ds.Tables["ПоискСотрудников"].DefaultView.RowFilter = "Адрес = '" + comboBox3.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
    }
}
