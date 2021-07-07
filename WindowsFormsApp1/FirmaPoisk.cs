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
    public partial class FirmaPoisk : Form
    {
        public FirmaPoisk()
        {
            InitializeComponent();
        }
        private void FirmaPoisk_Load(object sender, EventArgs e)
        {
            string sql;
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT director FROM firm";
            command = new NpgsqlCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT CONCAT_WS (' ', fam, name, surname) FROM client";
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT CONCAT_WS (', ', area, city, street, house, index) FROM adresfirm";
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox3.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT firm.kodfirm AS Код, nazvanfirm AS Название, timerabot AS Время_работ, CONCAT_WS (' ', fam, name, surname) AS ФИО_клиента, director AS Директор, CONCAT_WS (', ', area, city, street, house, index) AS Адрес FROM firm, client, adresfirm WHERE client.kodclienta = firm.kodclienta AND adresfirm.kodfirm = firm.kodfirm";
            Vipraboti.Table_Fill("ПоискФирм", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ПоискФирм"];
            dataGridView1.Columns["Код"].Visible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Enabled = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Form1.ds.Tables["ПоискФирм"];
            Form1.ds.Tables["ПоискФирм"].DefaultView.RowFilter = "";
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Form1.ds.Tables["ПоискФирм"].DefaultView.RowFilter = "Директор = '" + comboBox1.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                Form1.ds.Tables["ПоискФирм"].DefaultView.RowFilter = "ФИО_клиента = '" + comboBox2.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                Form1.ds.Tables["ПоискФирм"].DefaultView.RowFilter = "Адрес = '" + comboBox3.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
    }
}
