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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string conn = "Host = localhost; User Id = postgres; Database = isso; Port = 5432; Password = postgres;";
        public static NpgsqlConnection connection = new NpgsqlConnection(conn);
        public static DataSet ds = new DataSet();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Avtorizacia.root != "Администратор")
            {
                сотрудникиToolStripMenuItem.Enabled = false;
                видыРаботToolStripMenuItem.Enabled = false;
                адресаСотрудниковToolStripMenuItem.Enabled = false;
            }
        }
        public static bool Modification_Execute(string sql)
        {
            NpgsqlCommand command;
            command = new NpgsqlCommand(sql, connection);
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                connection.Close();
                return false;
            }
            connection.Close();
            return true;
        }
        private void просмотрСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }
        private void настройкаПаролейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Password pass = new Password();
            pass.Show();
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sotrudnik sotrud = new Sotrudnik();
            sotrud.Show();
        }
        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client cl = new Client();
            cl.Show();
        }
        private void видыРаботToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rabota rabota = new Rabota();
            rabota.Show();
        }
        private void адресаФирмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdresFirm adf = new AdresFirm();
            adf.Show();
        }
        private void поискСотрудниковToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SotrudPoisk sp = new SotrudPoisk();
            sp.Show();
        }
        private void поискФирмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FirmaPoisk fp = new FirmaPoisk();
            fp.Show();
        }
        private void выполненныеРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vipraboti vipraboti = new Vipraboti();
            vipraboti.Show();
        }
        private void актыРаботToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Akty akty = new Akty();
            akty.Show();
        }
        private void адресаСотрудниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdresSotrud ads = new AdresSotrud();
            ads.Show();
        }
        private void фирмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Firma firma = new Firma();
            firma.Show();
        }
    }
}