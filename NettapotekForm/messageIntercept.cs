using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;


namespace NettapotekForm
{
    public partial class messageIntercept : Form
    {

        string interceptFile;

        public messageIntercept(string filename)
        {
            interceptFile = filename;
            InitializeComponent();
        }

        private void messageIntercept_Load(object sender, EventArgs e)
        {
            lblFilename.Text = "Intercepting log file: " + Directory.GetCurrentDirectory() + "\\" + interceptFile;
            tbInterceptedMessage.Text = File.ReadAllText(interceptFile);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            File.WriteAllText(interceptFile, tbInterceptedMessage.Text);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
