using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NettapotekForm
{
    public partial class SamlForm : Form
    {
        public SamlForm()
        {
            InitializeComponent();
        }

        private void tbSAML_TextChanged(object sender, EventArgs e)
        {
            decodeIfNecessary();
        }
        void decodeIfNecessary()
        {
            string samltext = tbSAML.Text;
            if ( samltext.Contains("saml:Assertion") == false ) // indicates B64
            {
                try
                {
                    byte[] base64EncodedBytes = System.Convert.FromBase64String(samltext);
                    tbSAML.Text = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                }
                catch (Exception excp)
                {

                }
            }
        }

    }
}
