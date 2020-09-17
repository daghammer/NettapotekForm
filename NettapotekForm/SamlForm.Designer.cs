namespace NettapotekForm
{
    partial class SamlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SamlForm));
            this.tbSAML = new System.Windows.Forms.TextBox();
            this.cbUseB64 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbSAML
            // 
            this.tbSAML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSAML.Location = new System.Drawing.Point(25, 39);
            this.tbSAML.Multiline = true;
            this.tbSAML.Name = "tbSAML";
            this.tbSAML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSAML.Size = new System.Drawing.Size(895, 357);
            this.tbSAML.TabIndex = 0;
            this.tbSAML.TextChanged += new System.EventHandler(this.tbSAML_TextChanged);
            // 
            // cbUseB64
            // 
            this.cbUseB64.AutoSize = true;
            this.cbUseB64.Checked = true;
            this.cbUseB64.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseB64.Location = new System.Drawing.Point(64, 414);
            this.cbUseB64.Name = "cbUseB64";
            this.cbUseB64.Size = new System.Drawing.Size(152, 17);
            this.cbUseB64.TabIndex = 1;
            this.cbUseB64.Text = "vedlegg som Base64 kode";
            this.cbUseB64.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(731, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lim inn SAML token her. Dersom det er tomt, eller dialogen ikke er åpnet vil \"SAM" +
    "L assertion example.xml\" sendes. B64 versjon blir konvertert automatisk.";
            // 
            // SamlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 443);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbUseB64);
            this.Controls.Add(this.tbSAML);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SamlForm";
            this.Text = "SamlForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbSAML;
        public System.Windows.Forms.CheckBox cbUseB64;
        private System.Windows.Forms.Label label1;
    }
}