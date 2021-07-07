namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видыРаботToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.адресаФирмToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.адресаСотрудниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выполненныеРаботыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.актыРаботToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запросыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискСотрудниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискФирмToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрСправкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПаролейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.фирмыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.данныеToolStripMenuItem,
            this.журналыToolStripMenuItem,
            this.запросыToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сотрудникиToolStripMenuItem,
            this.клиентыToolStripMenuItem,
            this.видыРаботToolStripMenuItem,
            this.адресаФирмToolStripMenuItem,
            this.адресаСотрудниковToolStripMenuItem,
            this.фирмыToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.данныеToolStripMenuItem.Text = "Данные";
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.сотрудникиToolStripMenuItem_Click);
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // видыРаботToolStripMenuItem
            // 
            this.видыРаботToolStripMenuItem.Name = "видыРаботToolStripMenuItem";
            this.видыРаботToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.видыРаботToolStripMenuItem.Text = "Виды работ";
            this.видыРаботToolStripMenuItem.Click += new System.EventHandler(this.видыРаботToolStripMenuItem_Click);
            // 
            // адресаФирмToolStripMenuItem
            // 
            this.адресаФирмToolStripMenuItem.Name = "адресаФирмToolStripMenuItem";
            this.адресаФирмToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.адресаФирмToolStripMenuItem.Text = "Адреса фирм";
            this.адресаФирмToolStripMenuItem.Click += new System.EventHandler(this.адресаФирмToolStripMenuItem_Click);
            // 
            // адресаСотрудниковToolStripMenuItem
            // 
            this.адресаСотрудниковToolStripMenuItem.Name = "адресаСотрудниковToolStripMenuItem";
            this.адресаСотрудниковToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.адресаСотрудниковToolStripMenuItem.Text = "Адреса сотрудников";
            this.адресаСотрудниковToolStripMenuItem.Click += new System.EventHandler(this.адресаСотрудниковToolStripMenuItem_Click);
            // 
            // журналыToolStripMenuItem
            // 
            this.журналыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выполненныеРаботыToolStripMenuItem,
            this.актыРаботToolStripMenuItem});
            this.журналыToolStripMenuItem.Name = "журналыToolStripMenuItem";
            this.журналыToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.журналыToolStripMenuItem.Text = "Журналы";
            // 
            // выполненныеРаботыToolStripMenuItem
            // 
            this.выполненныеРаботыToolStripMenuItem.Name = "выполненныеРаботыToolStripMenuItem";
            this.выполненныеРаботыToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.выполненныеРаботыToolStripMenuItem.Text = "Выполненные работы";
            this.выполненныеРаботыToolStripMenuItem.Click += new System.EventHandler(this.выполненныеРаботыToolStripMenuItem_Click);
            // 
            // актыРаботToolStripMenuItem
            // 
            this.актыРаботToolStripMenuItem.Name = "актыРаботToolStripMenuItem";
            this.актыРаботToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.актыРаботToolStripMenuItem.Text = "Акты работ";
            this.актыРаботToolStripMenuItem.Click += new System.EventHandler(this.актыРаботToolStripMenuItem_Click);
            // 
            // запросыToolStripMenuItem
            // 
            this.запросыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поискСотрудниковToolStripMenuItem,
            this.поискФирмToolStripMenuItem});
            this.запросыToolStripMenuItem.Name = "запросыToolStripMenuItem";
            this.запросыToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.запросыToolStripMenuItem.Text = "Запросы";
            // 
            // поискСотрудниковToolStripMenuItem
            // 
            this.поискСотрудниковToolStripMenuItem.Name = "поискСотрудниковToolStripMenuItem";
            this.поискСотрудниковToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.поискСотрудниковToolStripMenuItem.Text = "Поиск сотрудников";
            this.поискСотрудниковToolStripMenuItem.Click += new System.EventHandler(this.поискСотрудниковToolStripMenuItem_Click_1);
            // 
            // поискФирмToolStripMenuItem
            // 
            this.поискФирмToolStripMenuItem.Name = "поискФирмToolStripMenuItem";
            this.поискФирмToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.поискФирмToolStripMenuItem.Text = "Поиск фирм";
            this.поискФирмToolStripMenuItem.Click += new System.EventHandler(this.поискФирмToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.просмотрСправкиToolStripMenuItem,
            this.настройкаПаролейToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // просмотрСправкиToolStripMenuItem
            // 
            this.просмотрСправкиToolStripMenuItem.Name = "просмотрСправкиToolStripMenuItem";
            this.просмотрСправкиToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.просмотрСправкиToolStripMenuItem.Text = "Просмотр справки";
            this.просмотрСправкиToolStripMenuItem.Click += new System.EventHandler(this.просмотрСправкиToolStripMenuItem_Click);
            // 
            // настройкаПаролейToolStripMenuItem
            // 
            this.настройкаПаролейToolStripMenuItem.Name = "настройкаПаролейToolStripMenuItem";
            this.настройкаПаролейToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.настройкаПаролейToolStripMenuItem.Text = "Настройка паролей";
            this.настройкаПаролейToolStripMenuItem.Click += new System.EventHandler(this.настройкаПаролейToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 410);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Вы успешно авторизировались в системе!";
            // 
            // фирмыToolStripMenuItem
            // 
            this.фирмыToolStripMenuItem.Name = "фирмыToolStripMenuItem";
            this.фирмыToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.фирмыToolStripMenuItem.Text = "Фирмы";
            this.фирмыToolStripMenuItem.Click += new System.EventHandler(this.фирмыToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 440);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запросыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрСправкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаПаролейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыРаботToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискСотрудниковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискФирмToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem адресаФирмToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem адресаСотрудниковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выполненныеРаботыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem актыРаботToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фирмыToolStripMenuItem;
    }
}