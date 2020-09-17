using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.IdentityModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Configuration;

using  NettapotekClient;


/*
 * Changes, at least some of them:
 * V 1.13.x.x
 * 20170103 - Fixed problems with B64 code, now default when SAML dialog is not used
 *          - Extract DNS name from cert for use in WS setup
 *          - Now supports use of expired certifiates
 *          - fixed bug in storage of "default customer"
 *          - Search for Reseptformidler cert adjusted to support new cert (E-helse)
 * 
 */

namespace NettapotekForm
{
    public partial class FormNettapotek : Form
    {

        string startupDir = string.Empty;
        string dataDir = string.Empty;
        X509Certificate2 apotekCert;
        //X509Certificate2 apotekCert = getSert("70716233765122732081071");
        X509Certificate2 RFCert ;

        String RFNA = String.Empty;
        String RFUtleverer = String.Empty;
        SamlForm samlForm = null;


        public FormNettapotek()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            tbIdKunde.Text = tbIdKunde.Text.Replace(" ", ""); // Remove spaces
            tbIdPasient.Text = tbIdPasient.Text.Replace(" ", "");
            Application.DoEvents();

            string idKunde = tbIdKunde.Text;
            string idPasient = (tbIdPasient.Text.Trim().Length > 0) ? tbIdPasient.Text : idKunde;
            String [] referansenummer = this.tbRefnr.Text.Trim().Split( new char[] { ';', ',' })  ;
            

           List<String> reseptliste;

           NettapotekClient.NettapotekClient cl = new NettapotekClient.NettapotekClient();
 
           cl.clientCert = apotekCert;
           cl.serviceCert = RFCert;
           cl.useSHA1 = cbSigning.Checked;
           // cl.testFromInternet = cbInternet.Checked;
           cl.RFUtleverer = RFUtleverer;
           cl.RFNA = RFNA;


           cl.msgIdFirstM9na1 = tbMessageIdFirstM9na1.Text;  

           lvResepter.Items.Clear();
           tbAntall.Text = "  --  ";
           Application.DoEvents()
               ;
           cl.samlText = getSamlToken();

           DateTime startTime = DateTime.Now;

           //cl.intercept = cbIntercept.Checked ;
           cl.intercept = interceptToolStripMenuItem.Checked;
           cl.preproduced = false;
           reseptliste = cl.getNARespetliste(idKunde, idPasient, referansenummer);

           if (interceptToolStripMenuItem.Checked == true)
           {
               messageIntercept editor = new messageIntercept(cl.lastLogFileName);
               editor.ShowDialog();           
               cl.intercept = false;
               cl.preproduced = true;

               reseptliste = cl.getNARespetliste(idKunde, idPasient, referansenummer);
           }


           cl.intercept = false;
           cl.preproduced = false;

            tbResponseTime.Text =  DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString();
            //reseptliste = cl.getRespetliste(idKunde, idPasient);

           tbAntall.Text = cl.antallResepterM9NA2.ToString();
           tbMessageIdFirstM9na1.Text = cl.msgIdFirstM9na1;
           tbMessageIdM9na1.Text = cl.msgIdLastM9na1;
           tbMessageIdM9na2.Text = cl.msgIdLastM9na2;
       
           if (reseptliste == null) MessageBox.Show(cl.errorMessage, "Feilmelding");
           else
           {
               foreach (String s in reseptliste)
               {

                   String[] reseptParts = s.Split(new char[] { '@' });
                   //lbResepter.Items.Add(s);
                   //lbResepter.Items[lbResepter.Items.Count - 1].BackColor = Color.Red;
                   ListViewItem resp = new ListViewItem(s);
                   resp.Tag = reseptParts[0]; // Resept id
                   resp.Text = reseptParts[2];
                   
                   switch (reseptParts[1]) // Reseptstatus
                   {
                       case "E":
                            
                           break;
                       case "U":
                           resp.BackColor = Color.Yellow;
                            
                           break;
                       case "T":
                           resp.ForeColor = Color.Red;
                           
                           break;
                       case "R":
                           resp.ForeColor = Color.LightSlateGray;
                            
                           break;
                           
                   }

                   resp.ToolTipText = reseptParts[3];
                   
                   lvResepter.Items.Add(resp);
               }
           }

           wbDiag.Navigate(dataDir + "\\" +cl.lastLogFileName);
          
           button1.Enabled = true;
        }

        public X509Certificate2 getSert(String serialnumber)
        {
            X509Certificate2 cert = null;
         
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection col = x509Store.Certificates;
            X509Certificate2Collection col1 = col.Find(X509FindType.FindBySerialNumber, serialnumber, false);



            if (col1.Count == 0) // Try current user
            {
                x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                x509Store.Open(OpenFlags.ReadOnly);

                 col = x509Store.Certificates;
                 col1 = col.Find(X509FindType.FindBySerialNumber, serialnumber  , false);
            
            }

            if (col1.Count > 0)
            {
                cert = col1[0];
                String certname = cert.FriendlyName;

            }


            return cert;

        }
        static string HexToDecimal(string hex)
        {
            List<int> dec = new List<int> { 0 };   // decimal result

            foreach (char c in hex)
            {
                try // If conversion goes wrong, skip to next
                {
                    int carry = Convert.ToInt32(c.ToString(), 16);
                    // initially holds decimal value of current hex digit;
                    // subsequently holds carry-over for multiplication

                    for (int i = 0; i < dec.Count; ++i)
                    {
                        int val = dec[i] * 16 + carry;
                        dec[i] = val % 10;
                        carry = val / 10;
                    }

                    while (carry > 0)
                    {
                        dec.Add(carry % 10);
                        carry /= 10;
                    }
                }
                catch (Exception exp) { }

            }

            var chars = dec.Select(d => (char)('0' + d));
            var cArr = chars.Reverse().ToArray();
            return new string(cArr);
        }
        private void FormNettapotek_Load(object sender, EventArgs e)
        {


            setupThings();


        }

        private void setupThings()
        {

            String version;
            // Are we in a deployed environment? Or a combined catalog?
            //textBox1.Text = Assembly.GetExecutingAssembly().Location;

            //currentDir = textBox2.Text = Environment.CurrentDirectory;

            // Look for datakatalog in app.setting:
            string datakatalogFromAppConfig = String.Empty;

            String customDatakatalog = ConfigurationManager.AppSettings["customDatakatalog"];
           
            Boolean custom = false;
            Boolean.TryParse(customDatakatalog, out custom);
            if (custom) datakatalogFromAppConfig = ConfigurationManager.AppSettings["datakatalog"];


            startupDir = System.Windows.Forms.Application.StartupPath;

            // Sett dataDir til å være hvor programmet startes. Fungerer i DEBUG og stand-alone, men ikke DEPLOY
            dataDir = startupDir;
           

            version = System.Windows.Forms.Application.ProductVersion;
            if (ApplicationDeployment.IsNetworkDeployed != false)
            {
                // Sett datadir til den laaaange pathen under MS-deploy
                dataDir = ApplicationDeployment.CurrentDeployment.DataDirectory;
                Version v = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                version = v.ToString(4);
            }

            String programDatakatalog = dataDir;

            // Velg fra config dersom det er satt
            if (datakatalogFromAppConfig != null && datakatalogFromAppConfig != String.Empty)
            {
                if (Directory.Exists(datakatalogFromAppConfig) == false)
                {
                    try
                    {
                        Directory.CreateDirectory((datakatalogFromAppConfig));
                    }
                    catch (Exception excp)
                    {
                        MessageBox.Show(excp.Message, "Kunne ikke lage datastien:" + datakatalogFromAppConfig);
                    }
                }
                if (Directory.Exists(datakatalogFromAppConfig)) dataDir = datakatalogFromAppConfig;
            }

            tbCatalog.Text = dataDir;
            tbVersjon.Text = version;

            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

            createCatalogAndCopyFiles(programDatakatalog + "\\xml", dataDir + "\\xml");
            createCatalogAndCopyFiles(programDatakatalog + "\\log", dataDir + "\\log");

            Directory.SetCurrentDirectory(dataDir);

            String apotekCertSerialNo = ConfigurationManager.AppSettings["ApotekCertSerialNo"];
            String reseptformidlerCertSerialNo = ConfigurationManager.AppSettings["ReseptformidlerCertSerialNo"];

            //String showInterceptCheckbox = ConfigurationManager.AppSettings["ShowInterceptChecbox"];
            //cbIntercept.Visible = (showInterceptCheckbox != null && showInterceptCheckbox == "true");

            apotekCert = getSert(apotekCertSerialNo);
            RFCert = getSert(reseptformidlerCertSerialNo);

            String NAchoice = ConfigurationManager.AppSettings["RFNA"];
            if (NAchoice != null && NAchoice != String.Empty)
            {
                RFNA = ConfigurationManager.AppSettings[NAchoice];
            }
            else MessageBox.Show("Det er feil i oppsett mot RF. Se i config fila");

            this.Text = this.Name + "  " + NAchoice + " (" + RFNA + ")";

            String utlevererChoice = ConfigurationManager.AppSettings["RFUtleverer"];
            if (utlevererChoice != null && utlevererChoice != String.Empty)
            {
                RFUtleverer = ConfigurationManager.AppSettings[utlevererChoice];
                this.Text += " " + "Reseptmottak: " + utlevererChoice + " (" + RFUtleverer + ")";
            }

            if (RFUtleverer == String.Empty)
            {
                groupBox1.Enabled = false;
            }

            tbIdKunde.Text = ConfigurationManager.AppSettings["defaultKundeId"]; 

        }

        void createCatalogAndCopyFiles(String from, String to)
        {
            int filesCopied = 0;
            int filesNotCopied = 0;

            if( !Directory.Exists(to) ) {
                Directory.CreateDirectory(to);
            }
            if (from == to) return;

            foreach (string file in Directory.GetFiles(from))
            {
                String destFile = to + "\\" + Path.GetFileName(file);

                if (File.Exists(destFile)) continue;

                try
                {
                    File.Copy(file, to + "\\" + Path.GetFileName(file), false); // Copy if it doesn't exist
                    filesCopied++;
                }
                catch (IOException ioexcp) // Will happen if file exist
                {
                    filesNotCopied++;
                }

            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                Process.Start("explorer.exe", dataDir);
            }
        }

        private void lbResepter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void lbResepter_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (lbResepter.SelectedItem == null) return;
        //    String resept = (String)lbResepter.SelectedItem;
        //    String[] info = resept.Split(new char[] { ':' });
        //    String reseptID = info[0];


        //    lbResepter.Enabled = false;
        //    Application.DoEvents();

        //    string idKunde = tbIdKunde.Text;
        //    string idPasient = (tbIdPasient.Text.Trim().Length > 0) ? tbIdPasient.Text : idKunde;

 
        //    NettapotekClient.NettapotekClient cl = new NettapotekClient.NettapotekClient();

        //    cl.clientCert = apotekCert;
        //    cl.serviceCert = RFCert;
        //    cl.useSHA1 = cbSigning.Checked;
        //    cl.testFromInternet = cbInternet.Checked;


        //    cl.msgIdFirstM9na1 = tbMessageIdFirstM9na1.Text;
        //    cl.msgIdLastM9na1 = tbMessageIdM9na1.Text;
        //    cl.msgIdLastM9na2 = tbMessageIdM9na2.Text;


        //    DateTime startTime = DateTime.Now;
        //    String detaljer = cl.getNARespetDetajer(idKunde, idPasient, reseptID);
        //    tbResponseTime.Text = DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString();
        //    //reseptliste = cl.getRespetliste(idKunde, idPasient);

        //    if (detaljer == null) MessageBox.Show(cl.errorMessage, "Feilmelding");
        //    else
        //    {
        //        tbDetaljer.Text = detaljer;
        //    }

        //    lbResepter.Enabled = true;

        //    wbDiag.Navigate(dataDir + "\\" + cl.lastLogFileName);


        //}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbIdKunde_TextChanged(object sender, EventArgs e)
        {
            tbMessageIdFirstM9na1.Clear();
            tbMessageIdM9na1.Clear();
            tbMessageIdM9na2.Clear();
            tbMessageIdM9na3.Clear();
            tbMessageIdM9na4.Clear();
            lvResepter.Items.Clear();
        }

        private void lbResepter_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void lvResepter_DoubleClick(object sender, EventArgs e)
        {
            if (lvResepter.SelectedItems.Count == 0) return;
            ListViewItem i = lvResepter.SelectedItems[0];
            string resept = i.Text;
            //String[] info = resept.Split(new char[] { ':' });
            String reseptID = (string)i.Tag;

            tbDetaljer.Text = "m9na2:" + Environment.NewLine + lvResepter.SelectedItems[0].ToolTipText;
                 
            lvResepter.Enabled = false;
            Application.DoEvents();

            string idKunde = tbIdKunde.Text;
            string idPasient = (tbIdPasient.Text.Trim().Length > 0) ? tbIdPasient.Text : idKunde;


            NettapotekClient.NettapotekClient cl = new NettapotekClient.NettapotekClient();

            cl.clientCert = apotekCert;
            cl.serviceCert = RFCert;
            cl.useSHA1 = cbSigning.Checked;
            //cl.testFromInternet = cbInternet.Checked;
            cl.RFUtleverer = RFUtleverer;
            cl.RFNA = RFNA;


            cl.msgIdFirstM9na1 = tbMessageIdFirstM9na1.Text;
            cl.msgIdLastM9na1 = tbMessageIdM9na1.Text;
            cl.msgIdLastM9na2 = tbMessageIdM9na2.Text;

            cl.samlText = getSamlToken();

            DateTime startTime = DateTime.Now;
            Guid reseptIdGUID = new Guid(reseptID);



            cl.intercept = interceptToolStripMenuItem.Checked;
            cl.preproduced = false;
            String detaljer = cl.getNARespetDetajer(idKunde, idPasient, reseptIdGUID.ToString());

            if (interceptToolStripMenuItem.Checked == true)
            {
                messageIntercept editor = new messageIntercept(cl.lastLogFileName);
                editor.ShowDialog();
                cl.intercept = false;
                cl.preproduced = true;

                detaljer = cl.getNARespetDetajer(idKunde, idPasient, reseptIdGUID.ToString());
            }



            cl.intercept = false;
            cl.preproduced = false;


           //  String detaljer = cl.getNARespetDetajer(idKunde, idPasient, reseptIdGUID.ToString() );
            tbResponseTime.Text = DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString();
            tbMessageIdM9na3.Text = cl.msgIdLastM9na3;
            tbMessageIdM9na4.Text = cl.msgIdLastM9na4;

            //reseptliste = cl.getRespetliste(idKunde, idPasient);

            if (detaljer == null) MessageBox.Show(cl.errorMessage, "Feilmelding");
            else
            {
                tbDetaljer.Text += Environment.NewLine + " ------------- " + Environment.NewLine + "m9na4:" + Environment.NewLine + detaljer;
            }

            lvResepter.Enabled = true;

            wbDiag.Navigate(dataDir + "\\" + cl.lastLogFileName);

        }

        private void lvResepter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvResepter.SelectedItems.Count > 0)
            {
                tbDetaljer.Text = "m9na2:" + Environment.NewLine + lvResepter.SelectedItems[0].ToolTipText;
            }
            wbDiag.Navigate("");
        }

        private void tbGuid_DoubleClick(object sender, EventArgs e)
        {
            String guidText = Guid.NewGuid().ToString();
            tbGuid.Text = guidText;
            Clipboard.SetText(guidText);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String reseptliste;

            NettapotekClient.NettapotekClient cl = new NettapotekClient.NettapotekClient();

            cl.clientCert = apotekCert;
            cl.serviceCert = RFCert;
            cl.RFUtleverer = RFUtleverer;
            cl.RFNA = RFNA;

            //cl.testFromInternet = cbInternet.Checked;

            String pasientID = (tbIdPasient.Text.Trim().Length < 11) ? tbIdKunde.Text : tbIdPasient.Text;


            reseptliste = cl.getRespetliste(pasientID);

            if (reseptliste == null) MessageBox.Show(cl.errorMessage, "Feilmelding");
            else
            {
                wbDiag.Navigate(dataDir + "\\" + cl.lastLogFileName);
            }

            // wbDiag.Navigate(dataDir + "\\" + cl.lastLogFileName);
        }

        private void btnTestM93_Click(object sender, EventArgs e)
        {
            String m1;
             
            if (lvResepter.SelectedItems.Count == 0) return;
            ListViewItem i = lvResepter.SelectedItems[0];
            string resept = i.Text;
            //String[] info = resept.Split(new char[] { ':' });
            String reseptID = (string)i.Tag;
            String pasientID = (tbIdPasient.Text.Trim().Length < 11) ? tbIdKunde.Text : tbIdPasient.Text;


            NettapotekClient.NettapotekClient cl = new NettapotekClient.NettapotekClient();

            cl.clientCert = apotekCert;
            cl.serviceCert = RFCert;
            cl.RFUtleverer = RFUtleverer;
            cl.RFNA = RFNA;

            // cl.testFromInternet = cbInternet.Checked;

            m1 = cl.getResept(pasientID, reseptID);

            if (m1 == null) MessageBox.Show(cl.errorMessage, "Feilmelding");
            else
            {
                wbDiag.Navigate(dataDir + "\\" + cl.lastLogFileName);
            }
        }

        private void btnTestM10_Click(object sender, EventArgs e)
        {
            String apprec;

            if (lvResepter.SelectedItems.Count == 0) return;
            ListViewItem i = lvResepter.SelectedItems[0];
            string resept = i.Text;
            //String[] info = resept.Split(new char[] { ':' });
            String reseptID = (string)i.Tag;


            NettapotekClient.NettapotekClient cl = new NettapotekClient.NettapotekClient();

            cl.clientCert = apotekCert;
            cl.serviceCert = RFCert;
            cl.RFUtleverer = RFUtleverer;
            cl.RFNA = RFNA;
            // cl.testFromInternet = cbInternet.Checked;
            String pasientID = (tbIdPasient.Text.Trim().Length < 11) ? tbIdKunde.Text : tbIdPasient.Text;
            apprec = cl.kansellerResept(pasientID, reseptID);

            if (apprec == null) MessageBox.Show(cl.errorMessage, "Feilmelding");
            else
            {
                wbDiag.Navigate(dataDir + "\\" + cl.lastLogFileName);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSAML_Click(object sender, EventArgs e)
        {
            if (samlForm == null || samlForm.IsDisposed)
            {
                samlForm = new SamlForm();
                //samlForm.tbSAML.Text = "Lim inn SAML token her!";
               
            }
            samlForm.Show();
             samlForm.BringToFront();
        }

        public string oldGetSamlToken()
        {
            String samlToken;

            if (samlForm != null && samlForm.tbSAML.Text.Trim().Length > 0)
            {
                
                if (samlForm.cbUseB64.Checked)
                {
                    samlToken = "<bas:Base64Container xmlns:bas=\"http://www.kith.no/xmlstds/base64container\">";
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(samlForm.tbSAML.Text);
                    samlToken += System.Convert.ToBase64String(plainTextBytes);
                    samlToken += "</bas:Base64Container>";
                }
                else samlToken = samlForm.tbSAML.Text;
            }
            else
            {
                samlToken = File.ReadAllText("xml\\SAML assertion example.xml");
            }
            return samlToken;
        }

        public string getSamlToken()
        {
            Boolean useB64 = true;
            String samlToken = String.Empty;

            if (samlForm != null ) {
                useB64 = samlForm.cbUseB64.Checked; 
                if (samlForm.tbSAML.Text.Trim().Length > 0)
                {
                    samlToken = samlForm.tbSAML.Text;
                } 
            }

            if (samlToken.Length == 0)
            {
                samlToken = File.ReadAllText("xml\\SAML assertion example.xml");
            }

            if (useB64)
            {
                String samlTokenB64 =  "<bas:Base64Container xmlns:bas=\"http://www.kith.no/xmlstds/base64container\">";
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(samlToken);
                samlTokenB64 += System.Convert.ToBase64String(plainTextBytes);
                samlTokenB64 += "</bas:Base64Container>";

                samlToken = samlTokenB64;
            }

            return samlToken;
        }

        private void FormNettapotek_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            {
                Process.Start("explorer.exe", startupDir);
            }
        }

        private void tbMessageIdFirstM9na1_DoubleClick(object sender, EventArgs e)
        {
            tbMessageIdM9na1.ReadOnly = false;
            tbMessageIdM9na2.ReadOnly = false;
            tbMessageIdM9na3.ReadOnly = false;
            tbMessageIdM9na4.ReadOnly = false;
            tbMessageIdFirstM9na1.ReadOnly = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (editSettings() == System.Windows.Forms.DialogResult.OK)
            {
                setupThings();
            }
        }
        DialogResult editSettings()
        {
            DialogResult res;
            Innstillinger s = new Innstillinger();
            res = s.ShowDialog();

            s.Dispose();
            return res;

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                Process.Start(startupDir + "\\" + ConfigurationManager.AppSettings["helpfile"]);
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
            about.Dispose();

        }

        private void cbIntercept_CheckedChanged(object sender, EventArgs e)
        {

        }       
    }
}
