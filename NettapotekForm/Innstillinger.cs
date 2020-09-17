using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;


namespace NettapotekForm
{
    public partial class Innstillinger : Form
    {
        public Innstillinger()
        {
            InitializeComponent();
        }

        private void Innstillinger_Load(object sender, EventArgs e)
        {
            String datakatalog = ConfigurationManager.AppSettings["datakatalog"];
            tbFolder.Text = datakatalog;
            String customDatakatalog = ConfigurationManager.AppSettings["customDatakatalog"];
            Boolean custom = false;
            Boolean.TryParse(customDatakatalog, out custom);
            if (custom) rbCustom.Select();
            else rbDefault.Select();

            String defaultKundeId = ConfigurationManager.AppSettings["defaultKundeId"];
            tbKundeid.Text = defaultKundeId;

            initCertCombos();
        }
        void initCertCombos()
        {
            X509Certificate2 certSelected = null;
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            System.IdentityModel.Tokens.X509SecurityToken securityToken;

            x509Store.Open(OpenFlags.ReadOnly);
            String storedClientCertSerial = ConfigurationManager.AppSettings["ApotekCertSerialNo"];
            String storedrfEncryptionCertSerial = ConfigurationManager.AppSettings["ReseptformidlerCertSerialNo"];


            X509Certificate2Collection col = x509Store.Certificates;

            foreach (X509Certificate2 cert in col)
            {
                Sertifikat s = new Sertifikat(cert);
                if (cert.HasPrivateKey == true)
                {
                    cmbCertClient.Items.Add(s);
                    if (s.cert.SerialNumber == storedClientCertSerial) cmbCertClient.SelectedItem = s;
                }
                if (cert.Subject.Contains("Reseptformidleren") || cert.Subject.Contains("RESEPTFORMIDLEREN"))
                {

                    cmbCertServer.Items.Add(s);
                    if (s.cert.SerialNumber == storedrfEncryptionCertSerial) cmbCertServer.SelectedItem = s;
                }
            }
            if (cmbCertServer.SelectedIndex < 0 && cmbCertServer.Items.Count > 0) cmbCertServer.SelectedIndex = 0;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save the current settings:

            try
            {

                Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["customDatakatalog"].Value = rbCustom.Checked ? "true" : "false";
                configuration.AppSettings.Settings["datakatalog"].Value = tbFolder.Text;
                configuration.AppSettings.Settings["defaultKundeId"].Value = tbKundeid.Text;
                Sertifikat selectedCert = (Sertifikat)cmbCertClient.SelectedItem;
                if (selectedCert != null)
                {
                    configuration.AppSettings.Settings["ApotekCertSerialNo"].Value = selectedCert.cert.SerialNumber;
                    int length = selectedCert.cert.SerialNumber.Length;
                }
                selectedCert = (Sertifikat)cmbCertServer.SelectedItem;
                if (selectedCert != null)
                {
                    configuration.AppSettings.Settings["ReseptformidlerCertSerialNo"].Value = selectedCert.cert.SerialNumber;
                }

                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("CreateConfigurationFile: {0}", err.ToString());
                DialogResult = System.Windows.Forms.DialogResult.Abort;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();

        }

        private void tbFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult folderBrowserResult = folderBrowserDialog1.ShowDialog();
            if (folderBrowserResult == System.Windows.Forms.DialogResult.OK)
            {
                tbFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
    public class Sertifikat : Object
    {
        public X509Certificate2 cert;
        string shortname;
        public Sertifikat(X509Certificate2 c)
        {
            cert = c;

            shortname = cert.Subject;
            string namepartSerialnumber = String.Empty;
            string namepartO = String.Empty;

            String[] nameComponents = cert.SubjectName.Name.Split(new char[] { ',' });
            foreach (string nameComp in nameComponents)
            {
                if (nameComp.Contains("OU=")) shortname = nameComp.Replace("OU=", "").Trim();
                if (nameComp.Contains("SERIALNUMBER=")) namepartSerialnumber = nameComp.Replace("SERIALNUMBER=", "").Trim();
                if (nameComp.Contains("O=")) namepartO = nameComp.Replace("O=", "").Trim();

            }
            foreach (X509KeyUsageExtension usage_extension in cert.Extensions.OfType<X509KeyUsageExtension>())
            {
                shortname += ", key usage: ";
                //if ((usage_extension.KeyUsages & X509KeyUsageFlags.KeyEncipherment) == X509KeyUsageFlags.KeyEncipherment) shortname += " KeyEnciperment";
                if ((usage_extension.KeyUsages & X509KeyUsageFlags.DataEncipherment) == X509KeyUsageFlags.DataEncipherment) shortname += " Kryptering";
                if ((usage_extension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.DigitalSignature) shortname += " DigitalSignatur";
                if ((usage_extension.KeyUsages & X509KeyUsageFlags.NonRepudiation) == X509KeyUsageFlags.NonRepudiation) shortname += " Non-Repudiation";

            }




            shortname += " [" + namepartSerialnumber + ":" + namepartO + " ] SN:" + cert.SerialNumber;


        }
        public override string ToString()
        {

            return shortname;
        }
    }
}
