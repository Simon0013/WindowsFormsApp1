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
    public partial class Akty : Form
    {
        public Akty()
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
        private void Tour_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT aktviprabot.kodakta AS Номер, date AS Дата, CONCAT_WS(' ', family, sotrudniki.name, otchestvo) AS ФИО_сотрудника, CONCAT_WS(' ', fam, client.name, surname) AS ФИО_клиента FROM aktviprabot, sotrudniki, client WHERE sotrudniki.kodsotrud = aktviprabot.kodsotrud AND client.kodclienta = aktviprabot.kodclienta";
            Table_Fill("АктРабот", sql);
            dataGridView1.DataSource = Form1.ds.Tables["АктРабот"];
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
            for (int i = 0; i < Form1.ds.Tables["АктРабот"].Rows.Count; i++)
            {
                if (kod == Convert.ToInt32(Form1.ds.Tables["АктРабот"].Rows[i]["Номер"].ToString())) ++kod;
                else break;
            }
            string sql = "INSERT INTO aktviprabot VALUES (" + kod.ToString() + ", 1, 1, 1)";
            Form1.Modification_Execute(sql);
            sql = "SELECT CONCAT_WS(' ', family, name, otchestvo) AS fio FROM sotrudniki WHERE kodsotrud = 1";
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            NpgsqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            string fio1 = dataReader["fio"].ToString();
            Form1.connection.Close();
            sql = "SELECT CONCAT_WS(' ', fam, name, surname) AS fio FROM client WHERE kodclienta = 1";
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            dataReader = command.ExecuteReader();
            dataReader.Read();
            string fio2 = dataReader["fio"].ToString();
            Form1.connection.Close();
            Form1.ds.Tables["АктРабот"].Rows.Add(new object[] { kod, 1, fio1, fio2 });
            dataGridView1.CurrentCell = null;
            AktCurrent up = new AktCurrent();
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
                AktCurrent up = new AktCurrent();
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
                message = "Вы точно хотите удалить из журнала акт " + dataGridView1.Rows[n].Cells["Номер"].Value + "?";
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
            string sql = "DELETE FROM aktviprabot WHERE kodakta = " + dataGridView1.Rows[n].Cells["Номер"].Value;
            Form1.Modification_Execute(sql);
            Form1.ds.Tables["АктРабот"].Rows.RemoveAt(n);
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            n = -1;
        }
        private void Tour_Activated(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
    }
}
