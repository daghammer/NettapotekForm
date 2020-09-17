namespace NettapotekForm
{
    partial class FormNettapotek
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNettapotek));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbIdKunde = new System.Windows.Forms.TextBox();
            this.tbIdPasient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbDetaljer = new System.Windows.Forms.TextBox();
            this.cbSigning = new System.Windows.Forms.CheckBox();
            this.tbCatalog = new System.Windows.Forms.TextBox();
            this.tbVersjon = new System.Windows.Forms.TextBox();
            this.btnOpenCatalog = new System.Windows.Forms.Button();
            this.tbAntall = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMessageIdM9na1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMessageIdM9na2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbResponseTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRefnr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbMessageIdFirstM9na1 = new System.Windows.Forms.TextBox();
            this.wbDiag = new System.Windows.Forms.WebBrowser();
            this.label11 = new System.Windows.Forms.Label();
            this.tbMessageIdM9na4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbMessageIdM9na3 = new System.Windows.Forms.TextBox();
            this.lvResepter = new System.Windows.Forms.ListView();
            this.label13 = new System.Windows.Forms.Label();
            this.tbGuid = new System.Windows.Forms.TextBox();
            this.btnTestM91 = new System.Windows.Forms.Button();
            this.btnTestM93 = new System.Windows.Forms.Button();
            this.btnTestM10 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSAML = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interceptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(1103, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(287, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // tbIdKunde
            // 
            this.tbIdKunde.Location = new System.Drawing.Point(96, 49);
            this.tbIdKunde.Name = "tbIdKunde";
            this.tbIdKunde.Size = new System.Drawing.Size(175, 20);
            this.tbIdKunde.TabIndex = 8;
            this.tbIdKunde.TextChanged += new System.EventHandler(this.tbIdKunde_TextChanged);
            // 
            // tbIdPasient
            // 
            this.tbIdPasient.Location = new System.Drawing.Point(96, 71);
            this.tbIdPasient.Name = "tbIdPasient";
            this.tbIdPasient.Size = new System.Drawing.Size(175, 20);
            this.tbIdPasient.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "ID kunde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "ID pasient";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(479, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "hent Reseptliste";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbDetaljer
            // 
            this.tbDetaljer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDetaljer.Location = new System.Drawing.Point(610, 154);
            this.tbDetaljer.Multiline = true;
            this.tbDetaljer.Name = "tbDetaljer";
            this.tbDetaljer.ReadOnly = true;
            this.tbDetaljer.Size = new System.Drawing.Size(360, 526);
            this.tbDetaljer.TabIndex = 14;
            // 
            // cbSigning
            // 
            this.cbSigning.AutoSize = true;
            this.cbSigning.Location = new System.Drawing.Point(318, 53);
            this.cbSigning.Name = "cbSigning";
            this.cbSigning.Size = new System.Drawing.Size(136, 17);
            this.cbSigning.TabIndex = 16;
            this.cbSigning.Text = "SHA-1 signering (dårlig)";
            this.cbSigning.UseVisualStyleBackColor = true;
            // 
            // tbCatalog
            // 
            this.tbCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCatalog.Location = new System.Drawing.Point(84, 711);
            this.tbCatalog.Name = "tbCatalog";
            this.tbCatalog.ReadOnly = true;
            this.tbCatalog.Size = new System.Drawing.Size(1200, 20);
            this.tbCatalog.TabIndex = 17;
            // 
            // tbVersjon
            // 
            this.tbVersjon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbVersjon.Location = new System.Drawing.Point(84, 689);
            this.tbVersjon.Name = "tbVersjon";
            this.tbVersjon.ReadOnly = true;
            this.tbVersjon.Size = new System.Drawing.Size(112, 20);
            this.tbVersjon.TabIndex = 18;
            this.tbVersjon.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnOpenCatalog
            // 
            this.btnOpenCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenCatalog.Location = new System.Drawing.Point(1302, 708);
            this.btnOpenCatalog.Name = "btnOpenCatalog";
            this.btnOpenCatalog.Size = new System.Drawing.Size(88, 23);
            this.btnOpenCatalog.TabIndex = 19;
            this.btnOpenCatalog.Text = "Åpne katalog";
            this.btnOpenCatalog.UseVisualStyleBackColor = true;
            this.btnOpenCatalog.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbAntall
            // 
            this.tbAntall.Location = new System.Drawing.Point(66, 126);
            this.tbAntall.Name = "tbAntall";
            this.tbAntall.ReadOnly = true;
            this.tbAntall.Size = new System.Drawing.Size(73, 20);
            this.tbAntall.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Antall:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 695);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Versjon:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 719);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Datamappe:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 573);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Last M9na1:";
            // 
            // tbMessageIdM9na1
            // 
            this.tbMessageIdM9na1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMessageIdM9na1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessageIdM9na1.Location = new System.Drawing.Point(96, 567);
            this.tbMessageIdM9na1.Name = "tbMessageIdM9na1";
            this.tbMessageIdM9na1.ReadOnly = true;
            this.tbMessageIdM9na1.Size = new System.Drawing.Size(299, 21);
            this.tbMessageIdM9na1.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 594);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Last M9na2:";
            // 
            // tbMessageIdM9na2
            // 
            this.tbMessageIdM9na2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMessageIdM9na2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessageIdM9na2.Location = new System.Drawing.Point(96, 588);
            this.tbMessageIdM9na2.Name = "tbMessageIdM9na2";
            this.tbMessageIdM9na2.ReadOnly = true;
            this.tbMessageIdM9na2.Size = new System.Drawing.Size(299, 21);
            this.tbMessageIdM9na2.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(412, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Response time [ms]:";
            // 
            // tbResponseTime
            // 
            this.tbResponseTime.Location = new System.Drawing.Point(522, 122);
            this.tbResponseTime.Name = "tbResponseTime";
            this.tbResponseTime.ReadOnly = true;
            this.tbResponseTime.Size = new System.Drawing.Size(122, 20);
            this.tbResponseTime.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Referansenr:";
            // 
            // tbRefnr
            // 
            this.tbRefnr.Location = new System.Drawing.Point(96, 93);
            this.tbRefnr.Name = "tbRefnr";
            this.tbRefnr.Size = new System.Drawing.Size(358, 20);
            this.tbRefnr.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 551);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "First M9na1:";
            // 
            // tbMessageIdFirstM9na1
            // 
            this.tbMessageIdFirstM9na1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMessageIdFirstM9na1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessageIdFirstM9na1.Location = new System.Drawing.Point(96, 545);
            this.tbMessageIdFirstM9na1.Name = "tbMessageIdFirstM9na1";
            this.tbMessageIdFirstM9na1.ReadOnly = true;
            this.tbMessageIdFirstM9na1.Size = new System.Drawing.Size(299, 21);
            this.tbMessageIdFirstM9na1.TabIndex = 32;
            this.tbMessageIdFirstM9na1.DoubleClick += new System.EventHandler(this.tbMessageIdFirstM9na1_DoubleClick);
            // 
            // wbDiag
            // 
            this.wbDiag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbDiag.Location = new System.Drawing.Point(976, 154);
            this.wbDiag.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDiag.Name = "wbDiag";
            this.wbDiag.Size = new System.Drawing.Size(414, 526);
            this.wbDiag.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 637);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "Last M9na4:";
            // 
            // tbMessageIdM9na4
            // 
            this.tbMessageIdM9na4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMessageIdM9na4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessageIdM9na4.Location = new System.Drawing.Point(96, 631);
            this.tbMessageIdM9na4.Name = "tbMessageIdM9na4";
            this.tbMessageIdM9na4.ReadOnly = true;
            this.tbMessageIdM9na4.Size = new System.Drawing.Size(299, 21);
            this.tbMessageIdM9na4.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 616);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Last M9na3:";
            // 
            // tbMessageIdM9na3
            // 
            this.tbMessageIdM9na3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMessageIdM9na3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessageIdM9na3.Location = new System.Drawing.Point(96, 610);
            this.tbMessageIdM9na3.Name = "tbMessageIdM9na3";
            this.tbMessageIdM9na3.ReadOnly = true;
            this.tbMessageIdM9na3.Size = new System.Drawing.Size(299, 21);
            this.tbMessageIdM9na3.TabIndex = 35;
            // 
            // lvResepter
            // 
            this.lvResepter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvResepter.AutoArrange = false;
            this.lvResepter.Location = new System.Drawing.Point(28, 154);
            this.lvResepter.MultiSelect = false;
            this.lvResepter.Name = "lvResepter";
            this.lvResepter.Size = new System.Drawing.Size(554, 388);
            this.lvResepter.TabIndex = 39;
            this.lvResepter.UseCompatibleStateImageBehavior = false;
            this.lvResepter.View = System.Windows.Forms.View.List;
            this.lvResepter.SelectedIndexChanged += new System.EventHandler(this.lvResepter_SelectedIndexChanged);
            this.lvResepter.DoubleClick += new System.EventHandler(this.lvResepter_DoubleClick);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 671);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 41;
            this.label13.Text = "Guid:";
            // 
            // tbGuid
            // 
            this.tbGuid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbGuid.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGuid.Location = new System.Drawing.Point(84, 665);
            this.tbGuid.Name = "tbGuid";
            this.tbGuid.ReadOnly = true;
            this.tbGuid.Size = new System.Drawing.Size(294, 21);
            this.tbGuid.TabIndex = 40;
            this.tbGuid.Text = "(dobbeltklikk for ny GUID til clipboard)";
            this.tbGuid.DoubleClick += new System.EventHandler(this.tbGuid_DoubleClick);
            // 
            // btnTestM91
            // 
            this.btnTestM91.Location = new System.Drawing.Point(6, 19);
            this.btnTestM91.Name = "btnTestM91";
            this.btnTestM91.Size = new System.Drawing.Size(94, 23);
            this.btnTestM91.TabIndex = 42;
            this.btnTestM91.Text = "m91";
            this.btnTestM91.UseVisualStyleBackColor = true;
            this.btnTestM91.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnTestM93
            // 
            this.btnTestM93.Location = new System.Drawing.Point(106, 19);
            this.btnTestM93.Name = "btnTestM93";
            this.btnTestM93.Size = new System.Drawing.Size(92, 23);
            this.btnTestM93.TabIndex = 43;
            this.btnTestM93.Text = "m93 (viser m1)";
            this.btnTestM93.UseVisualStyleBackColor = true;
            this.btnTestM93.Click += new System.EventHandler(this.btnTestM93_Click);
            // 
            // btnTestM10
            // 
            this.btnTestM10.Location = new System.Drawing.Point(204, 19);
            this.btnTestM10.Name = "btnTestM10";
            this.btnTestM10.Size = new System.Drawing.Size(126, 23);
            this.btnTestM10.TabIndex = 44;
            this.btnTestM10.Text = "m10 (Kansellering)";
            this.btnTestM10.UseVisualStyleBackColor = true;
            this.btnTestM10.Click += new System.EventHandler(this.btnTestM10_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestM91);
            this.groupBox1.Controls.Add(this.btnTestM10);
            this.groupBox1.Controls.Add(this.btnTestM93);
            this.groupBox1.Location = new System.Drawing.Point(750, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 58);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "\"Vanlig apotek\" meldinger (bruker data fra pasient/resept-id)";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnSAML
            // 
            this.btnSAML.BackColor = System.Drawing.Color.Yellow;
            this.btnSAML.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSAML.Location = new System.Drawing.Point(641, 53);
            this.btnSAML.Name = "btnSAML";
            this.btnSAML.Size = new System.Drawing.Size(93, 25);
            this.btnSAML.TabIndex = 46;
            this.btnSAML.Text = "SAML";
            this.btnSAML.UseVisualStyleBackColor = false;
            this.btnSAML.Click += new System.EventHandler(this.btnSAML_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.testToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1402, 24);
            this.menuStrip1.TabIndex = 48;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.settingsToolStripMenuItem.Text = "Innstillinger";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.interceptToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // interceptToolStripMenuItem
            // 
            this.interceptToolStripMenuItem.CheckOnClick = true;
            this.interceptToolStripMenuItem.Name = "interceptToolStripMenuItem";
            this.interceptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.interceptToolStripMenuItem.Text = "Intercept";
            this.interceptToolStripMenuItem.ToolTipText = "Stopper den ferdige meldingen rett før den skal sendes\r\nslik at den kan redigeres" +
    "";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.aboutToolStripMenuItem.Text = "Hjelp";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem.Text = "Hjelp";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "Om";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // FormNettapotek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 743);
            this.Controls.Add(this.btnSAML);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbGuid);
            this.Controls.Add(this.lvResepter);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbMessageIdM9na4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbMessageIdM9na3);
            this.Controls.Add(this.wbDiag);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbMessageIdFirstM9na1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbRefnr);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbResponseTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbMessageIdM9na2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbMessageIdM9na1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbAntall);
            this.Controls.Add(this.btnOpenCatalog);
            this.Controls.Add(this.tbVersjon);
            this.Controls.Add(this.tbCatalog);
            this.Controls.Add(this.cbSigning);
            this.Controls.Add(this.tbDetaljer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIdPasient);
            this.Controls.Add(this.tbIdKunde);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormNettapotek";
            this.Text = "Nettapotek test";
            this.Load += new System.EventHandler(this.FormNettapotek_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FormNettapotek_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbIdKunde;
        private System.Windows.Forms.TextBox tbIdPasient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbDetaljer;
        private System.Windows.Forms.CheckBox cbSigning;
        private System.Windows.Forms.TextBox tbCatalog;
        private System.Windows.Forms.TextBox tbVersjon;
        private System.Windows.Forms.Button btnOpenCatalog;
        private System.Windows.Forms.TextBox tbAntall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbMessageIdM9na1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbMessageIdM9na2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbResponseTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRefnr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbMessageIdFirstM9na1;
        private System.Windows.Forms.WebBrowser wbDiag;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbMessageIdM9na4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbMessageIdM9na3;
        private System.Windows.Forms.ListView lvResepter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbGuid;
        private System.Windows.Forms.Button btnTestM91;
        private System.Windows.Forms.Button btnTestM93;
        private System.Windows.Forms.Button btnTestM10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSAML;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interceptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    }
}

