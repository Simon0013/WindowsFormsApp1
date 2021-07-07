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
    public partial class Vipraboti : Form
    {
        public Vipraboti()
        {
            InitializeComponent();
        }
        public static int n = -1;
        public static void Table_Fill(string name, string sql)
        {
            Form1.connection.Open();
            if (Form1.ds.Tables[name] != null)
                Form1.ds.Tables[name].Clear();
            NpgsqlDataAdapter da;
            da = new NpgsqlDataAdapter(sql, Form1.connection);
            da.Fill(Form1.ds, name);
            Form1.connection.Close();
        }
        private void Vipraboti_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT rabota_po_akty.kodrabot AS Номер, kodakta AS Номер_акта, nazvanie AS Вид_работы, obiem_rabot AS Объём FROM rabota_po_akty, raboti WHERE raboti.kodrabot = rabota_po_akty.kodrabot";
            Table_Fill("ВыполненныеРаботы", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ВыполненныеРаботы"];
            dataGridView1.Columns["Номер"].Visible = false;
            dataGridView1.CurrentCell = null;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            n = dataGridView1.Rows.Count;
            int kod = 1;
            for (int i = 0; i < Form1.ds.Tables["ВыполненныеРаботы"].Rows.Count; i++)
            {
                if (kod == Convert.ToInt32(Form1.ds.Tables["ВыполненныеРаботы"].Rows[i]["Номер"].ToString())) ++kod;
                else break;
            }
            string sql = "INSERT INTO rabota_po_akty VALUES (" + kod.ToString() + ", 1, '0')";
            Form1.Modification_Execute(sql);
            sql = "SELECT nazvanie FROM raboti WHERE kodrabot = " + kod.ToString();
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            NpgsqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            string nazvanie = dataReader["nazvanie"].ToString();
            Form1.connection.Close();
            Form1.ds.Tables["ВыполненныеРаботы"].Rows.Add(new object[] { kod, 1, nazvanie, 0 });
            dataGridView1.CurrentCell = null;
            VseRaboti up = new VseRaboti();
            up.Show();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = dataGridView1.CurrentRow.Index;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            if (n >= 0)
            {
                VseRaboti up = new VseRaboti();
                up.Show();
            }
            else
            {
                MessageBox.Show("Не указан редактируемый экземпляр!!!", "Ошибка");
                return;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string message;
            try
            {
                message = "Вы точно хотите удалить из журнала работу " + dataGridView1.Rows[n].Cells["Номер"].Value + "?";
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Не указана удаляемая запись таблицы!!!", "Ошибка");
                return;
            }
            string caption = "Удаление из журнала";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult rezult = MessageBox.Show(message, caption, buttons);
            if (rezult == DialogResult.No) { return; }
            string sql = "DELETE FROM rabota_po_akty WHERE kodrabot = " + dataGridView1.Rows[n].Cells["Номер"].Value;
            Form1.Modification_Execute(sql);
            Form1.ds.Tables["ВыполненныеРаботы"].Rows.RemoveAt(n);
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            n = -1;
        }
        private void Vipraboti_Activated(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
    }
}
