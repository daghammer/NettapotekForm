using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

using System.ServiceModel.Channels;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Services.Configuration;

using System.IO;


using NettapotekForm.UtlevererWebService;
using NettapotekForm.NettapotekWebService;

namespace NettapotekClient
{
    class NettapotekClient
    {

        //Fra Internett: http://139.112.129.5/NA/NAWebServiceSoapHttpPort   
        //Fra Helsenett:  http://139.116.132.22/NA/NAWebServiceSoapHttpPort

        public X509Certificate2 clientCert; // Disse må settes før kall til webservice
        public X509Certificate2 serviceCert;
        public Boolean useSHA1 = false;
        //public Boolean testFromInternet = false;

        public String RFNA = string.Empty;
        public String RFUtleverer = string.Empty;
        public String errorMessage = "";

        public Boolean intercept = false;
        public Boolean preproduced = false;

        public int antallResepterM9NA2;
        public string msgIdFirstM9na1 = string.Empty;
        public string msgIdLastM9na1 = string.Empty;
        public string msgIdLastM9na2 = string.Empty;
        public string msgIdLastM9na3 = string.Empty;
        public string msgIdLastM9na4 = string.Empty;

        public string lastLogFileName = string.Empty;
        public string samlText = "<bas:Base64Container xmlns:bas=\"http://www.kith.no/xmlstds/base64container\">PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4NCjxzYW1sOkFzc2VydGlvbiB4bWxuczpzYW1sPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIiBJRD0iczI1YzY5N2RhNjM1YTBmMzg5MDI2YWU1OGZkYTBiY2MyZWViNzcwY2U5IiBJc3N1ZUluc3RhbnQ9IjIwMTYtMDEtMjVUMDg6NDE6MzhaIiBWZXJzaW9uPSIyLjAiPg0KCTxzYW1sOklzc3Vlcj5pZHBvcnRlbi5kaWZpLm5vLXYzPC9zYW1sOklzc3Vlcj4NCgk8ZHM6U2lnbmF0dXJlIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIj4NCgkJPGRzOlNpZ25lZEluZm8+DQoJCQk8ZHM6Q2Fub25pY2FsaXphdGlvbk1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMTAveG1sLWV4Yy1jMTRuIyIvPg0KCQkJPGRzOlNpZ25hdHVyZU1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyNyc2Etc2hhMSIvPg0KCQkJPGRzOlJlZmVyZW5jZSBVUkk9IiNzMjVjNjk3ZGE2MzVhMGYzODkwMjZhZTU4ZmRhMGJjYzJlZWI3NzBjZTkiPg0KCQkJCTxkczpUcmFuc2Zvcm1zPg0KCQkJCQk8ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI2VudmVsb3BlZC1zaWduYXR1cmUiLz4NCgkJCQkJPGRzOlRyYW5zZm9ybSBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMTAveG1sLWV4Yy1jMTRuIyIvPg0KCQkJCTwvZHM6VHJhbnNmb3Jtcz4NCgkJCQk8ZHM6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI3NoYTEiLz4NCgkJCQk8ZHM6RGlnZXN0VmFsdWU+RmlqMkVBNEVWaGwxMmVtb2plUTdwRXJSZUV3PTwvZHM6RGlnZXN0VmFsdWU+DQoJCQk8L2RzOlJlZmVyZW5jZT4NCgkJPC9kczpTaWduZWRJbmZvPg0KCQk8ZHM6U2lnbmF0dXJlVmFsdWU+cmY4dFdQZE9lNUxvbk85RWR4SVhnN2lnbENvSEhSckZBYjYxVmxVLzhpdXk1ajBRNEFZbEJnUU9UQXVIYnY2YWV1cDkrVVY1MGQ0a0dJWmdoOEc5WFgydjhFOVE3aXNPY0lpZkJPR2lZcXJYQXc4Nk04TGhRdFhwNHNMdjBSeWd2SktPOFQzeElVSHdQS0ZTQk5BeXVrdGtCcndMblM0aDRFTm5LRTU5YnBIWWl1Y1NBd3RrVVl3RTVOOHdOMno0eW5aekhzOHQzS0VVTHJRYS8zSFpod01iQmFmWm40PC9kczpTaWduYXR1cmVWYWx1ZT4NCgkJPGRzOktleUluZm8+DQoJCQk8ZHM6WDUwOURhdGE+DQoJCQkJPGRzOlg1MDlDZXJ0aWZpY2F0ZT5NSUlGQ1RDQ0EvR2dBd0lCQWdJTENDdFBISlIzT0E2REJMUXdEUVlKS29aSWh2Y05BUUVMQlFBd1N6RUxNQWtHQTFVRUJoTUNUazh4N1gwUmxIQ2hROUh5U2g5VEFxdTRBVzd3SDFaWGhqYkVrS0RMdUVvSU1ZUk5JU0J0eE1ic3g3WDBSbEhDaFE5SHlTaDlUQUN0UEhKUjNPQTZEQkxRd0RRWUpLb1pJaHZjTkFRRXNPY0lpZkJPR2lZcXJYQXc4Nk04TGhRdFhwNHNMdjBkT2U1TG9uTzlFZHhJWGc3aWdsQzh4N1gwUmxIQ2hROUNUazh4N1gwUmxIQ2hROUh5U2g5VEFxdTRBVzd3SDFaWGhqYkVrPC9kczpYNTA5Q2VydGlmaWNhdGU+DQoJCQk8L2RzOlg1MDlEYXRhPg0KCQk8L2RzOktleUluZm8+DQoJPC9kczpTaWduYXR1cmU+DQoJPHNhbWw6U3ViamVjdD4NCgkJPHNhbWw6TmFtZUlEIEZvcm1hdD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOm5hbWVpZC1mb3JtYXQ6cGVyc2lzdGVudCIgTmFtZVF1YWxpZmllcj0iaWRwb3J0ZW4uZGlmaS5uby12MyIgU1BOYW1lUXVhbGlmaWVyPSJuZXR0YXBvdGVrMSI+TWYvWmZIRHJwenBoZmh0ZWJIRGJUQzYwUjd2Tzwvc2FtbDpOYW1lSUQ+DQoJCTxzYW1sOlN1YmplY3RDb25maXJtYXRpb24gTWV0aG9kPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6Y206YmVhcmVyIj4NCgkJCTxzYW1sOlN1YmplY3RDb25maXJtYXRpb25EYXRhIEluUmVzcG9uc2VUbz0iXzQ1NzA1MjJlN2RhYzA5MWYxODIyZTYyYzcwYTVlZWM1IiBOb3RPbk9yQWZ0ZXI9IjIwMTYtMDEtMjVUMDg6NTE6MzhaIiBSZWNpcGllbnQ9Imh0dHA6Ly9uZXR0YXBvdGVrMS5uby90ZXN0c3AvYXNzZXJ0aW9uY29uc3VtZXIiLz4NCgkJPC9zYW1sOlN1YmplY3RDb25maXJtYXRpb24+DQoJPC9zYW1sOlN1YmplY3Q+DQoJPHNhbWw6Q29uZGl0aW9ucyBOb3RCZWZvcmU9IjIwMTYtMDEtMjVUMDg6MzE6MzhaIiBOb3RPbk9yQWZ0ZXI9IjIwMTYtMDEtMjVUMDg6NTE6MzhaIj4NCgkJPHNhbWw6QXVkaWVuY2VSZXN0cmljdGlvbj4NCgkJCTxzYW1sOkF1ZGllbmNlPm5ldHRhcG90ZWsxPC9zYW1sOkF1ZGllbmNlPg0KCQk8L3NhbWw6QXVkaWVuY2VSZXN0cmljdGlvbj4NCgk8L3NhbWw6Q29uZGl0aW9ucz4NCgk8c2FtbDpBdXRoblN0YXRlbWVudCBBdXRobkluc3RhbnQ9IjIwMTYtMDEtMjVUMDg6NDE6MzhaIiBTZXNzaW9uSW5kZXg9InMyMGZmZWVmMjhjZDNhM2YzMDM5YjFjMmVhNDM2ODg5YTBjOTgzYzMwNCI+DQoJCTxzYW1sOkF1dGhuQ29udGV4dD4NCgkJCTxzYW1sOkF1dGhuQ29udGV4dENsYXNzUmVmPnVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphYzpjbGFzc2VzOlBhc3N3b3JkUHJvdGVjdGVkVHJhbnNwb3J0PC9zYW1sOkF1dGhuQ29udGV4dENsYXNzUmVmPg0KCQk8L3NhbWw6QXV0aG5Db250ZXh0Pg0KCTwvc2FtbDpBdXRoblN0YXRlbWVudD4NCgk8c2FtbDpBdHRyaWJ1dGVTdGF0ZW1lbnQ+DQoJCTxzYW1sOkF0dHJpYnV0ZSBOYW1lPSJ1aWQiPg0KCQkJPHNhbWw6QXR0cmlidXRlVmFsdWUgeG1sbnM6eHM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hIiB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiB4c2k6dHlwZT0ieHM6c3RyaW5nIj4wMTAxNjBYWFhYWDwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT4NCgkJPC9zYW1sOkF0dHJpYnV0ZT4NCgkJPHNhbWw6QXR0cmlidXRlIE5hbWU9IkN1bHR1cmUiPg0KCQkJPHNhbWw6QXR0cmlidXRlVmFsdWUgeG1sbnM6eHM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hIiB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiB4c2k6dHlwZT0ieHM6c3RyaW5nIj5uYjwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT4NCgkJPC9zYW1sOkF0dHJpYnV0ZT4NCgkJPHNhbWw6QXR0cmlidXRlIE5hbWU9InBvc3RrYXNzZWxldmVyYW5kb2VyTmF2biI+DQoJCQk8c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4bWxuczp4cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEiIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIHhzaTp0eXBlPSJ4czpzdHJpbmciPkRpZ2lwb3N0PC9zYW1sOkF0dHJpYnV0ZVZhbHVlPg0KCQk8L3NhbWw6QXR0cmlidXRlPg0KCQk8c2FtbDpBdHRyaWJ1dGUgTmFtZT0iZXBvc3RhZHJlc3NlIj4NCgkJCTxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhtbG5zOnhzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYSIgeG1sbnM6eHNpPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSIgeHNpOnR5cGU9InhzOnN0cmluZyI+b2xhQG5vcmRtYW5uLm5vPC9zYW1sOkF0dHJpYnV0ZVZhbHVlPg0KCQk8L3NhbWw6QXR0cmlidXRlPg0KCQk8c2FtbDpBdHRyaWJ1dGUgTmFtZT0ibW9iaWx0ZWxlZm9ubnVtbWVyIj4NCgkJCTxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhtbG5zOnhzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYSIgeG1sbnM6eHNpPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSIgeHNpOnR5cGU9InhzOnN0cmluZyI+OTk5OTk5OTk8L3NhbWw6QXR0cmlidXRlVmFsdWU+DQoJCTwvc2FtbDpBdHRyaWJ1dGU+DQoJCTxzYW1sOkF0dHJpYnV0ZSBOYW1lPSJyZXNlcnZhc2pvbiI+DQoJCQk8c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4bWxuczp4cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEiIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIHhzaTp0eXBlPSJ4czpzdHJpbmciPk5FSTwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT4NCgkJPC9zYW1sOkF0dHJpYnV0ZT4NCgkJPHNhbWw6QXR0cmlidXRlIE5hbWU9InN0YXR1cyI+DQoJCQk8c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4bWxuczp4cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEiIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIHhzaTp0eXBlPSJ4czpzdHJpbmciPkFLVElWPC9zYW1sOkF0dHJpYnV0ZVZhbHVlPg0KCQk8L3NhbWw6QXR0cmlidXRlPg0KCQk8c2FtbDpBdHRyaWJ1dGUgTmFtZT0iQXV0aE1ldGhvZCI+DQoJCQk8c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4bWxuczp4cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEiIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIHhzaTp0eXBlPSJ4czpzdHJpbmciPk1pbmlkLU9UQzwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT4NCgkJPC9zYW1sOkF0dHJpYnV0ZT4NCgkJPHNhbWw6QXR0cmlidXRlIE5hbWU9IlNlY3VyaXR5TGV2ZWwiPg0KCQkJPHNhbWw6QXR0cmlidXRlVmFsdWUgeG1sbnM6eHM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hIiB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiB4c2k6dHlwZT0ieHM6c3RyaW5nIj40PC9zYW1sOkF0dHJpYnV0ZVZhbHVlPg0KCQk8L3NhbWw6QXR0cmlidXRlPg0KCTwvc2FtbDpBdHRyaWJ1dGVTdGF0ZW1lbnQ+DQo8L3NhbWw6QXNzZXJ0aW9uPg==</bas:Base64Container>" ;

        public NettapotekClient()
        {

        }


        public List<String> getNARespetliste(String idkunde, String idpasient, String[] refnummer)
        {
            List<String> reseptliste = new List<string>();

            antallResepterM9NA2 = -1;

XmlDocument xd = new XmlDocument();
            if (preproduced == false)
            {



                xd.Load("xml\\m9na1-2016-06-06.xml");

                XmlNamespaceManager nsmgr1 = new XmlNamespaceManager(xd.NameTable);
                nsmgr1.AddNamespace("na1", "http://www.kith.no/xmlstds/eresept/m9na1/2016-06-06");
                nsmgr1.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");

                XmlNode M9NA1 = xd.SelectSingleNode("//na1:M9NA1", nsmgr1);
                XmlNode msgid = xd.SelectSingleNode("//mh:MsgId", nsmgr1);
                XmlNode msginfo = xd.SelectSingleNode("//mh:MsgInfo", nsmgr1);
                XmlNode genDate = xd.SelectSingleNode("//mh:GenDate", nsmgr1);

                XmlNodeList replaceList = xd.GetElementsByTagName("Replace");
                foreach (XmlNode replace in replaceList)
                {
                    if (replace.Attributes["type"].Value == "SAML")
                    {
                        replace.ParentNode.InnerXml= samlText;
                        break;
                    }
                }
            

                if (msgid != null)
                {
                    String msgIdString =  System.Guid.NewGuid().ToString();
                    msgid.InnerText = msgIdString;
                    genDate.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzzzzz");
                    if (msgIdFirstM9na1.Length > 0) // Do we have a conversation?
                    {
                        XmlNode ConvRef = xd.CreateNode(XmlNodeType.Element, "ConversationRef", nsmgr1.LookupNamespace("mh"));
                        XmlNode RefToParent = xd.CreateNode(XmlNodeType.Element, "RefToParent", nsmgr1.LookupNamespace("mh"));
                        RefToParent.InnerText = msgIdFirstM9na1;
                        ConvRef.AppendChild(RefToParent);
                        XmlNode RefToConv = xd.CreateNode(XmlNodeType.Element, "RefToConversation", nsmgr1.LookupNamespace("mh"));
                        RefToConv.InnerText = msgIdFirstM9na1;
                        ConvRef.AppendChild(RefToConv);

                        msginfo.InsertAfter(ConvRef, msgid);
                    }
                    else
                    {
                        msgIdFirstM9na1 = msgIdString;
                    }
                    msgIdLastM9na1 = msgIdString;
                }
                XmlNodeList kunde = xd.GetElementsByTagName("IdKunde");
                if (kunde.Count > 0) {  
                    XmlNode kundeid = kunde[0].FirstChild;
                    if (kundeid != null)
                    {
                        kundeid.InnerText = idkunde;
                    }
                }

                // Add referansenummer om det er oppgitt

                if (M9NA1 != null && refnummer != null)
                {
                    foreach (String refn in refnummer)
                    {
                        if (refn.Length != 0)
                        {
                            XmlNode refnummerNode = xd.CreateNode(XmlNodeType.Element, "RefNr", "http://www.kith.no/xmlstds/eresept/m9na1/2016-06-06");
                            refnummerNode.InnerText = refn.Trim();
                            M9NA1.AppendChild(refnummerNode);
                        }
                    }

                }

                XmlNodeList patient = xd.GetElementsByTagName("Patient");

                if (patient.Count > 0)
                    {

                        XmlNode patientId = patient[0].FirstChild.FirstChild;
                        if (patientId != null)
                        {
                            patientId.InnerText = idpasient;

                        }
                    }
           
                lastLogFileName = "log\\M9NA1_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                File.WriteAllText(lastLogFileName, xd.OuterXml);
            
            }
            else
            {
                // read from last log file:

                FileStream fs = new FileStream (lastLogFileName, FileMode.Open);

                xd.Load(fs);  
            }
            if (intercept == false)
            {


                //X509Certificate2 clientCert = new X509Certificate2();
                //X509Certificate2 serviceCert = new X509Certificate2();

                var asbe = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(
                    System.ServiceModel.MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, true);


                asbe.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
                asbe.SetKeyDerivation(false);

                // we don't use the default (SignBeforeEncryptAndEncryptSignature) since the signature is not encrypted
                asbe.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;


                asbe.LocalClientSettings.DetectReplays = false;
                asbe.LocalServiceSettings.DetectReplays = false;
                // asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;

                // http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702/ws-securitypolicy-1.2-spec-os.html

                if (useSHA1)
                {
                    asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256; // TEST NETTAPOTEK  Sha1
                }
                else
                {
                    asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256Sha256; // NY FOR NETTAPOTEK Sha256 AES256
                }

                //
                //asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15; // Denne er tilsvarende "Reseptmottak" 

                asbe.IncludeTimestamp = false;

                // use soap1.1 with utf-8 encoding
                var tme = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
                var quotas = tme.ReaderQuotas;
                quotas.MaxArrayLength = Int32.MaxValue;
                quotas.MaxBytesPerRead = Int32.MaxValue;
                quotas.MaxDepth = Int32.MaxValue;
                quotas.MaxNameTableCharCount = Int32.MaxValue;
                quotas.MaxStringContentLength = Int32.MaxValue;

                // use http transport with no security
                var http = new HttpTransportBindingElement
                {
                    MaxReceivedMessageSize = int.MaxValue,
                    KeepAliveEnabled = false
                };

                CustomBinding cbind = new CustomBinding(asbe, tme, http);

                EndpointAddress address;

                String [] subject  = serviceCert.SubjectName.Name.Split ( new char [] { ',' });
                String dnsName = "RESEPTFORMIDLEREN";
                foreach (String s in subject)
                {
                    if (s.Contains("CN=")) dnsName = s.Replace("CN=", "").Trim();
                }

                address = new EndpointAddress(new Uri(RFNA), EndpointIdentity.CreateDnsIdentity(dnsName));  

                NettapotekForm.NettapotekWebService.NAWebClient naW = new NAWebClient(cbind, address);

                naW.ClientCredentials.ClientCertificate.Certificate = clientCert;
                naW.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCert;


                M9na1 m9na1 = new M9na1();
                m9na1.dokument = Encoding.UTF8.GetBytes(xd.DocumentElement.OuterXml);

                String retstring = "";
                try
                {
                    // NAWebService_m9na1Response m9na1orApprec = await naW.NAWebService_m9na1Async(m9na1);
                

                    M9na2 m9na2 = naW.NAWebService_m9na1(m9na1);
                    byte[] retval = (byte[])m9na2.dokument;
                    retstring = System.Text.Encoding.UTF8.GetString(retval);
                    XmlDocument m9na2Doc = new XmlDocument();

                    m9na2Doc.LoadXml(retstring);

                    lastLogFileName = "log\\M9NA2_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                    File.WriteAllText(lastLogFileName, retstring);


                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(m9na2Doc.NameTable);
                    nsmgr.AddNamespace("na2", "http://www.kith.no/xmlstds/eresept/m9na2/2016-10-26");
                    nsmgr.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");


                    XmlNode nodeMsgId = m9na2Doc.SelectSingleNode("//mh:MsgId", nsmgr);
                    if (nodeMsgId != null)
                        msgIdLastM9na2 = nodeMsgId.InnerText;

                    XmlNode nodeAntall = m9na2Doc.SelectSingleNode("//na2:Antall", nsmgr);
                    if (nodeAntall != null)
                        int.TryParse(nodeAntall.InnerText, out antallResepterM9NA2);
                    else // remove new version and try the original ns
                    {
                        nsmgr.RemoveNamespace("na2", "http://www.kith.no/xmlstds/eresept/m9na2/2016-10-26");
                        nsmgr.AddNamespace("na2", "http://www.kith.no/xmlstds/eresept/m9na2/2016-06-06");
                        nodeAntall = m9na2Doc.SelectSingleNode("//na2:Antall", nsmgr); // try again!
                        if (nodeAntall != null)
                            int.TryParse(nodeAntall.InnerText, out antallResepterM9NA2);
                    }

                    XmlNodeList resepter = m9na2Doc.SelectNodes("//na2:Reseptinfo", nsmgr);
                    for  ( int node = 0; node< resepter.Count; node++   )
                    {
                        XmlNode resept = resepter.Item(node);
                        XmlNode nfs;
                        XmlNode status;
                        string reseptStatus = string.Empty;

                        string reseptID = "";
                        string reseptDetails = String.Empty;

                        string reseptInfo = "";

                        nfs = resept.SelectSingleNode("na2:Forskrivningsdato", nsmgr);
                        if (nfs != null)
                        {

                            reseptDetails += "Forskrivningsdato: " + nfs.InnerText + Environment.NewLine;
                        }


                        nfs = resept.SelectSingleNode("na2:ReseptId", nsmgr);
                        if (nfs != null)
                        {
                            reseptID =   nfs.InnerText;
                            reseptDetails += "ReseptId: " + reseptID + Environment.NewLine;
                        }


                    
                        nfs = resept.SelectSingleNode("na2:IdLegemiddelMerkevare", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "IdLegemiddelMerkevare: " + nfs.InnerText + Environment.NewLine;
                        }
                        
                        nfs = resept.SelectSingleNode("na2:IdLegemiddelVirkestoff", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "IdLegemiddelVirkestoff: " + nfs.InnerText + Environment.NewLine;
                        }
                        
                        nfs = resept.SelectSingleNode("na2:Nr", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "Nr: " + nfs.InnerText + Environment.NewLine;
                        }  
                                        
                        nfs =  resept.SelectSingleNode("na2:NavnFormStyrke", nsmgr);
                        if (nfs != null)
                        {
                            reseptInfo += nfs.InnerText +  " (Legemiddel)"  ;
                            reseptDetails += "NavnFormStyrke: " + nfs.InnerText + Environment.NewLine;
                        }

                        if (reseptInfo.Length == 0)
                        {
                        
                            nfs = resept.SelectSingleNode("na2:LegemiddelblandingNavn", nsmgr);
                            if (nfs != null)
                            {
                                reseptInfo += nfs.InnerText + " (Legemiddelblanding)"  ;
                                reseptDetails += "LegemiddelblandingNavn: " + nfs.InnerText + Environment.NewLine;
                            }
                        }
                        if (reseptInfo.Length == 0)
                        {
                            nfs = resept.SelectSingleNode("na2:ProdGruppe", nsmgr);
                            if (nfs != null)
                            {
                                reseptInfo +=  nfs.Attributes["DN"].Value + " (Produktgruppe)" ;
                                reseptDetails += "ProdGruppe: " + nfs.Attributes["DN"].Value + Environment.NewLine;
                            }
                        }

                    
                        nfs = resept.SelectSingleNode("na2:Bruksomrade", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "Bruksomrade: " + nfs.InnerText + Environment.NewLine;
                        }  


                            
                         nfs = resept.SelectSingleNode("na2:Antall", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "Antall: " + nfs.InnerText + Environment.NewLine;
                        }  
                               
                        //nfs = resept.SelectSingleNode("na2:Mengde", nsmgr);
                        //if (nfs != null)
                        //{
                        //    reseptDetails += "Mengde: " + nfs.InnerText + Environment.NewLine;
                        //}  
                                
                        nfs = resept.SelectSingleNode("na2:Reiterasjon", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "Reiterasjon: " + nfs.InnerText + Environment.NewLine;
                        }  
                                    
                        nfs = resept.SelectSingleNode("na2:TidligsteUtlevering", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "TidligsteUtlevering: " + nfs.InnerText + Environment.NewLine;
                        }  
                                    
                        nfs = resept.SelectSingleNode("na2:RekvirentId", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "RekvirentId: " + nfs.InnerText + Environment.NewLine;
                        }  
                                        
                        nfs = resept.SelectSingleNode("na2:NavnRekvirent", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "NavnRekvirent: " + nfs.InnerText + Environment.NewLine;
                        }  
                      
                                           

                        status = resept.SelectSingleNode("na2:Status", nsmgr);
                        if (status != null)
                        {
                             reseptStatus = status.Attributes["V"].Value;
                             reseptDetails += "Status: " + status.Attributes["V"].Value +Environment.NewLine;
                        }


                        nfs = resept.SelectSingleNode("na2:StatusSoknadSLV", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "StatusSoknadSLV: " + nfs.Attributes["DN"].Value + Environment.NewLine;
                        }

                        nfs = resept.SelectSingleNode("na2:SisteUtlevering", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "SisteUtlevering: " + nfs.InnerText + Environment.NewLine;
                        }

                        nfs = resept.SelectSingleNode("na2:MetodeEkspedering", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "MetodeEkspedering: " + nfs.Attributes["DN"].Value + Environment.NewLine;
                        }

                        nfs = resept.SelectSingleNode("na2:RefHjemmel", nsmgr);
                        if (nfs != null)
                        {
                            reseptDetails += "RefHjemmel: " + nfs.Attributes["DN"].Value + Environment.NewLine;
                        }  


                        reseptliste.Add(reseptID + "@" + reseptStatus + "@" + reseptInfo + "@" + reseptDetails);
                    }

                    int i = retval.Length;
                 
                }
                catch (FaultException<NettapotekForm.NettapotekWebService.AppRecFault> appRec)
                {
                    NettapotekForm.NettapotekWebService.AppRecFault af = appRec.Detail;
                    byte[] retval = (byte[])af.dokument;
                    retstring = System.Text.Encoding.UTF8.GetString(retval);
                    errorMessage = "AppRec: ";

                    lastLogFileName = "log\\Apprec_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                    File.WriteAllText(lastLogFileName, retstring);
                    XmlDocument appRecDoc = new XmlDocument();

                    appRecDoc.LoadXml(retstring);
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(appRecDoc.NameTable);
                    nsmgr.AddNamespace("apprec", "http://www.kith.no/xmlstds/apprec/2004-11-21");
                    // Finn Id, Status og Error

                    XmlNode status = appRecDoc.SelectSingleNode("//apprec:Status", nsmgr);
                    if (status != null)
                    {
                        errorMessage += Environment.NewLine + "Status: " + status.Attributes["DN"].Value;
                        errorMessage += " (" + status.Attributes["V"].Value + ")"; 
                    }
                    XmlNode error = appRecDoc.SelectSingleNode("//apprec:Error", nsmgr);
                    if (error != null)
                    {
                        errorMessage += Environment.NewLine + "Error: " + error.Attributes["DN"].Value;
                        errorMessage += " (" + error.Attributes["V"].Value + ")";
                    }
                    return null;
                }
                 catch (Exception excp) {
 
                    errorMessage = excp.Message + Environment.NewLine ;
                    if (excp.InnerException != null) errorMessage += Environment.NewLine + excp.InnerException.Message;
                    return null;
                 
                }

            }

            return reseptliste;

        }

        public String getNARespetDetajer(String idKunde, String idPasient, String reseptID)
        {
            String detaljer = String.Empty;


            XmlDocument xd = new XmlDocument();

            if (preproduced == false)
            {



                xd.Load("xml\\m9na3-2016-06-06.xml");


                XmlNamespaceManager nsmgr3 = new XmlNamespaceManager(xd.NameTable);
                nsmgr3.AddNamespace("na3", "http://www.kith.no/xmlstds/eresept/m9na3/2016-06-06");
                nsmgr3.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");

                XmlNode M9NA1 = xd.SelectSingleNode("//na3:M9NA3", nsmgr3);
                XmlNode msgid = xd.SelectSingleNode("//mh:MsgId", nsmgr3);
                XmlNode msginfo = xd.SelectSingleNode("//mh:MsgInfo", nsmgr3);
                XmlNode genDate = xd.SelectSingleNode("//mh:GenDate", nsmgr3);

                msgIdLastM9na3 = System.Guid.NewGuid().ToString();

                if (msgid != null) msgid.InnerText = msgIdLastM9na3;
                if (genDate != null) genDate.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzzzzz");
                if (msgIdFirstM9na1.Length > 0) // Do we have a conversation?
                {
                    XmlNode ConvRef = xd.CreateNode(XmlNodeType.Element, "ConversationRef", nsmgr3.LookupNamespace("mh"));

                    XmlNode RefToParent = xd.CreateNode(XmlNodeType.Element, "RefToParent", nsmgr3.LookupNamespace("mh"));
                    RefToParent.InnerText = msgIdLastM9na2;
                    ConvRef.AppendChild(RefToParent);
                    XmlNode RefToConv = xd.CreateNode(XmlNodeType.Element, "RefToConversation", nsmgr3.LookupNamespace("mh"));
                    RefToConv.InnerText = msgIdFirstM9na1;
                    ConvRef.AppendChild(RefToConv);

                    msginfo.InsertAfter(ConvRef, msgid);
                }


                XmlNode kunde = xd.SelectSingleNode("//na3:IdKunde", nsmgr3);

                if (kunde != null)
                {
                    XmlNode kundeid = kunde.FirstChild;
                    if (kundeid != null)
                    {
                        kundeid.InnerText = idKunde;
                    }
                }
                XmlNodeList patient = xd.GetElementsByTagName("Patient");

                if (patient.Count > 0)
                {

                    XmlNode patientId = patient[0].FirstChild.FirstChild;
                    if (patientId != null)
                    {
                        patientId.InnerText = idPasient;

                    }
                }
                XmlNodeList rID = xd.GetElementsByTagName("ReseptId");
                if (rID.Count > 0) rID[0].InnerText = reseptID;

                XmlNodeList replaceList = xd.GetElementsByTagName("Replace");
                foreach (XmlNode replace in replaceList)
                {
                    if (replace.Attributes["type"].Value == "SAML")
                    {
                        replace.ParentNode.InnerXml = samlText;
                        break;
                    }
                }


                lastLogFileName = "log\\M9NA3_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                File.WriteAllText(lastLogFileName, xd.OuterXml);


            }

            else
            {
                // read from last log file:

                FileStream fs = new FileStream(lastLogFileName, FileMode.Open);

                xd.Load(fs);
            }

            if (intercept == false)
            {

                var asbe = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(
System.ServiceModel.MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, true);


                asbe.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
                asbe.SetKeyDerivation(false);

                // we don't use the default (SignBeforeEncryptAndEncryptSignature) since the signature is not encrypted
                asbe.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;


                asbe.LocalClientSettings.DetectReplays = false;
                asbe.LocalServiceSettings.DetectReplays = false;
                // asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;

                // http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702/ws-securitypolicy-1.2-spec-os.html

                if (useSHA1)
                {
                    asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256; // TEST NETTAPOTEK  Sha1
                }
                else
                {
                    asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256Sha256; // NY FOR NETTAPOTEK Sha256 AES256
                }

                //
                //asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15; // Denne er tilsvarende "Reseptmottak" 

                asbe.IncludeTimestamp = false;

                // use soap1.1 with utf-8 encoding
                var tme = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
                var quotas = tme.ReaderQuotas;
                quotas.MaxArrayLength = Int32.MaxValue;
                quotas.MaxBytesPerRead = Int32.MaxValue;
                quotas.MaxDepth = Int32.MaxValue;
                quotas.MaxNameTableCharCount = Int32.MaxValue;
                quotas.MaxStringContentLength = Int32.MaxValue;

                // use http transport with no security
                var http = new HttpTransportBindingElement
                {
                    MaxReceivedMessageSize = int.MaxValue,
                    KeepAliveEnabled = false
                };

                CustomBinding cbind = new CustomBinding(asbe, tme, http);

                EndpointAddress address;
                String[] subject = serviceCert.SubjectName.Name.Split(new char[] { ',' });
                String dnsName = "RESEPTFORMIDLEREN";
                foreach (String s in subject)
                {
                    if (s.Contains("CN=")) dnsName = s.Replace("CN=", "").Trim();
                }

                address = new EndpointAddress(new Uri(RFNA), EndpointIdentity.CreateDnsIdentity(dnsName));  
                NettapotekForm.NettapotekWebService.NAWebClient naW = new NAWebClient(cbind, address);
                

                naW.ClientCredentials.ClientCertificate.Certificate = clientCert;
                naW.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCert;

                

                M9na3 m9na3 = new M9na3();
                M9na4 m9na4 ;
                m9na3.dokument = Encoding.UTF8.GetBytes(xd.DocumentElement.OuterXml);

                String retstring = "";
                try
                {
                    m9na4 = naW.NAWebService_m9na3(m9na3);
                 
                    byte[] retval = (byte[])m9na4.dokument;
                    retstring = System.Text.Encoding.UTF8.GetString(retval);
                    XmlDocument m9na4Doc = new XmlDocument();

                    m9na4Doc.LoadXml(retstring);


                    lastLogFileName = "log\\M9NA4_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                    File.WriteAllText(lastLogFileName, retstring);


                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(m9na4Doc.NameTable);
                    nsmgr.AddNamespace("na4", "http://www.kith.no/xmlstds/eresept/m9na4/2016-06-06");
                    nsmgr.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");
                    nsmgr.AddNamespace("fel", "http://www.kith.no/xmlstds/felleskomponent1");
                
                    XmlNode nodeMsgId = m9na4Doc.SelectSingleNode("//mh:MsgId", nsmgr);
                    if (nodeMsgId != null)
                        msgIdLastM9na4 = nodeMsgId.InnerText;

                    XmlNode reseptid = m9na4Doc.SelectSingleNode("//na4:ReseptId", nsmgr);
                    if (reseptid != null) detaljer += "ReseptId: " + reseptid.InnerText+ Environment.NewLine;
                    else detaljer += "ReseptId: <null>";

                    XmlNode status = m9na4Doc.SelectSingleNode("//na4:Status", nsmgr);
                    if (status != null) detaljer += "Status: " + status.Attributes["DN"].Value + Environment.NewLine;
                    else detaljer += "Status: <null>";

                    XmlNode refkode = m9na4Doc.SelectSingleNode("//na4:RefKode", nsmgr);
                    if (refkode != null) detaljer += "RefKode: " + refkode.Attributes["DN"].Value + Environment.NewLine;
                
                    XmlNode kortdose = m9na4Doc.SelectSingleNode("//na4:Kortdose", nsmgr);
                    if (kortdose != null) detaljer += "Kortdose: " + kortdose.Attributes["DN"].Value + Environment.NewLine;

                    XmlNode institusjon = m9na4Doc.SelectSingleNode("//na4:InstitusjonsID", nsmgr);
                    if (institusjon != null)
                    {
                        XmlNode Id = institusjon.SelectSingleNode("fel:Id", nsmgr);
                        XmlNode Type = institusjon.SelectSingleNode("fel:TypeId", nsmgr);
                        if( Id != null && Type != null )
                            detaljer += "InstitusjonsID: " + Id.InnerText + " ("  + Type.Attributes["V"].Value + ")" + Environment.NewLine;
                    }
                    XmlNodeList utleveringer = m9na4Doc.SelectNodes("//na4:Utlevering", nsmgr);
                    for (int node = 0; utleveringer != null && node < utleveringer.Count; node++)
                    {
                        XmlNode resept = utleveringer.Item(node);

                        XmlNode nfs;

                        string utleveringsInfo = "";
                        nfs = resept.SelectSingleNode("na4:NavnFormStyrke", nsmgr);
                        if (nfs != null)
                        {
                            utleveringsInfo += "  NavnFormStyrke: " + nfs.InnerText + Environment.NewLine;
                        }
                 
                         //nfs = resept.SelectSingleNode("na4:Pakningsstr", nsmgr);
                         //if (nfs != null)
                         //{
                         //    utleveringsInfo += "  Pakningsstr: " + nfs.InnerText + Environment.NewLine;
                         //}


                        nfs = resept.SelectSingleNode("na4:AntallPakningerUtlevertTotalt", nsmgr);
                        if (nfs != null)
                        {
                            utleveringsInfo += "AntallPakningerUtlevertTotalt: " + nfs.InnerText + Environment.NewLine; ;
                        }
                     
                       // reseptliste.Add(reseptID + ": " + reseptInfo);
                         detaljer += utleveringsInfo;
                    }

                    int i = retval.Length;
                }
                catch (FaultException<NettapotekForm.NettapotekWebService.AppRecFault> appRec)
                {
                    NettapotekForm.NettapotekWebService.AppRecFault af = appRec.Detail;
                    byte[] retval = (byte[])af.dokument;
                    retstring = System.Text.Encoding.UTF8.GetString(retval);
                    errorMessage = "AppRec: ";

                    lastLogFileName = "log\\Apprec_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                    File.WriteAllText(lastLogFileName, retstring);
                    XmlDocument appRecDoc = new XmlDocument();

                    appRecDoc.LoadXml(retstring);
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(appRecDoc.NameTable);
                    nsmgr.AddNamespace("apprec", "http://www.kith.no/xmlstds/apprec/2004-11-21");
                    // Finn Id, Status og Error

                    XmlNode status = appRecDoc.SelectSingleNode("//apprec:Status", nsmgr);
                    if (status != null)
                    {
                        errorMessage += Environment.NewLine + "Status: " + status.Attributes["DN"].Value;
                        errorMessage += " (" + status.Attributes["V"].Value + ")";
                    }
                    XmlNode error = appRecDoc.SelectSingleNode("//apprec:Error", nsmgr);
                    if (error != null)
                    {
                        errorMessage += Environment.NewLine + "Error: " + error.Attributes["DN"].Value;
                        errorMessage += " (" + error.Attributes["V"].Value + ")";
                    }
                    return null;
                }
                catch (Exception excp)
                {

                    errorMessage = excp.Message + Environment.NewLine + excp.InnerException.Message;
                    return null;

                }

            }

            return detaljer;
        }

        // test: "vanlig" utleverer
        public String getRespetliste(String idpasient) {
            

            var asbe = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(System.ServiceModel.MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, true);
             
            asbe.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            asbe.SetKeyDerivation(false);

            // we don't use the default (SignBeforeEncryptAndEncryptSignature) since the signature is not encrypted
            asbe.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;

            asbe.LocalClientSettings.DetectReplays = false;
            asbe.LocalServiceSettings.DetectReplays = false;
            asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;
            
            asbe.IncludeTimestamp = false;
             
            // use soap1.1 with utf-8 encoding
            var tme = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
            var quotas = tme.ReaderQuotas;
            quotas.MaxArrayLength = Int32.MaxValue;
            quotas.MaxBytesPerRead = Int32.MaxValue;
            quotas.MaxDepth = Int32.MaxValue;
            quotas.MaxNameTableCharCount = Int32.MaxValue;
            quotas.MaxStringContentLength = Int32.MaxValue;

            // use http transport with no security
            var http = new HttpTransportBindingElement
            {
                MaxReceivedMessageSize = int.MaxValue,
                KeepAliveEnabled = false
            };

            CustomBinding cbind = new CustomBinding(asbe, tme, http);
            String[] subject = serviceCert.SubjectName.Name.Split(new char[] { ',' });
            String dnsName = "RESEPTFORMIDLEREN";
            foreach (String s in subject)
            {
                if (s.Contains("CN=")) dnsName = s.Replace("CN=", "").Trim();
            }

             
            var address = new EndpointAddress(new Uri(RFUtleverer), EndpointIdentity.CreateDnsIdentity(dnsName));
            UtlevererWebClient utlW = new UtlevererWebClient(cbind, address);
            //RekvirentWebClient rekW = new RekvirentWebClient(cbind, address);

            utlW.ClientCredentials.ClientCertificate.Certificate = clientCert;
            utlW.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCert;

            XmlDocument xd = new XmlDocument();
            xd.Load("xml\\M91.xml");
            //xd.Load("M91Turid_Gran_Dato_Ski_Storsenter2.xml");
            //xd.Load("m91-skistorsenter.xml");

            XmlNodeList msgid = xd.GetElementsByTagName("MsgId");
            if (msgid.Count > 0) msgid[0].InnerText = System.Guid.NewGuid().ToString();
            XmlNodeList genDate = xd.GetElementsByTagName("GenDate");
            if (genDate.Count > 0) genDate[0].InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzzzzz");
            XmlNodeList fnr = xd.GetElementsByTagName("Fnr");
            if (fnr.Count > 0) fnr[0].InnerText = idpasient;


            lastLogFileName = "log\\M91_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
            File.WriteAllText(lastLogFileName, xd.OuterXml);

            M9_1 m91 = new M9_1();

            m91.dokument = Encoding.UTF8.GetBytes(xd.DocumentElement.OuterXml);


            M9_2 m92 = utlW.UtlevererWebService_m9_1(m91);
            byte[] retval = (byte[])m92.dokument;
            string retstring = System.Text.Encoding.UTF8.GetString(retval);

            lastLogFileName = "log\\M92_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
            File.WriteAllText(lastLogFileName, retstring);

            int i = retval.Length;


            return retstring;

        }

        // test: "vanlig" utleverer
        public string getResept(String pasient_id, String reseptid)
        {
 
            var asbe = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(System.ServiceModel.MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, true);

            asbe.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            asbe.SetKeyDerivation(false);

            // we don't use the default (SignBeforeEncryptAndEncryptSignature) since the signature is not encrypted
            asbe.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;

            asbe.LocalClientSettings.DetectReplays = false;
            asbe.LocalServiceSettings.DetectReplays = false;
            asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;

            asbe.IncludeTimestamp = false;

            // use soap1.1 with utf-8 encoding
            var tme = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
            var quotas = tme.ReaderQuotas;
            quotas.MaxArrayLength = Int32.MaxValue;
            quotas.MaxBytesPerRead = Int32.MaxValue;
            quotas.MaxDepth = Int32.MaxValue;
            quotas.MaxNameTableCharCount = Int32.MaxValue;
            quotas.MaxStringContentLength = Int32.MaxValue;

            // use http transport with no security
            var http = new HttpTransportBindingElement
            {
                MaxReceivedMessageSize = int.MaxValue,
                KeepAliveEnabled = false
            };

            CustomBinding cbind = new CustomBinding(asbe, tme, http);
           
            String[] subject = serviceCert.SubjectName.Name.Split(new char[] { ',' });
            String dnsName = "RESEPTFORMIDLEREN";
            foreach (String s in subject)
            {
                if (s.Contains("CN=")) dnsName = s.Replace("CN=", "").Trim();
            }
            var address = new EndpointAddress(new Uri(RFUtleverer), EndpointIdentity.CreateDnsIdentity(dnsName));

            UtlevererWebClient utlW = new UtlevererWebClient(cbind, address);
            //RekvirentWebClient rekW = new RekvirentWebClient(cbind, address);

            utlW.ClientCredentials.ClientCertificate.Certificate = clientCert;
            utlW.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCert;

            XmlDocument xd = new XmlDocument();
            xd.Load("xml\\M93.xml");
          
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xd.NameTable);
            nsmgr.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");
            

            XmlNodeList msgid = xd.GetElementsByTagName("MsgId");
            if (msgid.Count > 0) msgid[0].InnerText = System.Guid.NewGuid().ToString();
            XmlNodeList genDate = xd.GetElementsByTagName("GenDate");
            if (genDate.Count > 0) genDate[0].InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzzzzz");
            XmlNodeList reseptId = xd.GetElementsByTagName("ReseptId");
            if (reseptId.Count > 0) reseptId[0].InnerText = reseptid;
            

            // Get Patient/Ident/id
            XmlNode patient = xd.SelectSingleNode("//mh:Patient/mh:Ident/mh:Id", nsmgr);
            if (patient != null) patient.InnerText = pasient_id;
           
            M9_3 m93 = new M9_3();

            m93.dokument = Encoding.UTF8.GetBytes(xd.DocumentElement.OuterXml);

            lastLogFileName = "log\\M93_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
            File.WriteAllText(lastLogFileName, xd.OuterXml);

            M9_4 m94 = utlW.UtlevererWebService_m9_3(m93);
            byte[] retval = (byte[])m94.dokument;
            string retstring = System.Text.Encoding.UTF8.GetString(retval);
            
            lastLogFileName = "log\\M94_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
            File.WriteAllText(lastLogFileName, retstring);

            // Pakk ut resepten
            xd.LoadXml(retstring);
            XmlNamespaceManager nsmgr2 = new XmlNamespaceManager(xd.NameTable);
            nsmgr2.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");
            nsmgr2.AddNamespace("bas", "http://www.kith.no/xmlstds/base64container");

            XmlNode resept = xd.SelectSingleNode("//mh:Content/bas:Base64Container", nsmgr2);
            if (resept != null)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(resept.InnerText);
                String M1text = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                lastLogFileName = "log\\M1_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
                File.WriteAllText(lastLogFileName, M1text);

            }

            int i = retval.Length;


            return retstring;

        }
        public string kansellerResept(String pasient_id, String reseptid)
        {

            var asbe = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(System.ServiceModel.MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, true);

            asbe.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            asbe.SetKeyDerivation(false);

            // we don't use the default (SignBeforeEncryptAndEncryptSignature) since the signature is not encrypted
            asbe.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;

            asbe.LocalClientSettings.DetectReplays = false;
            asbe.LocalServiceSettings.DetectReplays = false;
            asbe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;

            asbe.IncludeTimestamp = false;

            // use soap1.1 with utf-8 encoding
            var tme = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
            var quotas = tme.ReaderQuotas;
            quotas.MaxArrayLength = Int32.MaxValue;
            quotas.MaxBytesPerRead = Int32.MaxValue;
            quotas.MaxDepth = Int32.MaxValue;
            quotas.MaxNameTableCharCount = Int32.MaxValue;
            quotas.MaxStringContentLength = Int32.MaxValue;

            // use http transport with no security
            var http = new HttpTransportBindingElement
            {
                MaxReceivedMessageSize = int.MaxValue,
                KeepAliveEnabled = false
            };

            CustomBinding cbind = new CustomBinding(asbe, tme, http);
            String[] subject = serviceCert.SubjectName.Name.Split(new char[] { ',' });
            String dnsName = "RESEPTFORMIDLEREN";
            foreach (String s in subject)
            {
                if (s.Contains("CN=")) dnsName = s.Replace("CN=", "").Trim();
            }
            var address = new EndpointAddress(new Uri(RFUtleverer), EndpointIdentity.CreateDnsIdentity(dnsName));

      
            UtlevererWebClient utlW = new UtlevererWebClient(cbind, address);
            //RekvirentWebClient rekW = new RekvirentWebClient(cbind, address);

            utlW.ClientCredentials.ClientCertificate.Certificate = clientCert;
            utlW.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCert;

            XmlDocument xd = new XmlDocument();
            xd.Load("xml\\M10.xml");

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xd.NameTable);
            nsmgr.AddNamespace("mh", "http://www.kith.no/xmlstds/msghead/2006-05-24");

            XmlNodeList msgid = xd.GetElementsByTagName("MsgId");
            if (msgid.Count > 0) msgid[0].InnerText = System.Guid.NewGuid().ToString();
            XmlNodeList genDate = xd.GetElementsByTagName("GenDate");
            if (genDate.Count > 0) genDate[0].InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzzzzz");
            XmlNodeList reseptId = xd.GetElementsByTagName("ReseptId");
            if (reseptId.Count > 0) reseptId[0].InnerText = reseptid;
            XmlNodeList utleveringsdato = xd.GetElementsByTagName("Utleveringsdato");
            if (utleveringsdato.Count > 0) utleveringsdato[0].InnerText = DateTime.Now.ToString("yyyy-MM-dd");

            // Get Patient/Ident/id
            XmlNode patient = xd.SelectSingleNode("//mh:Patient/mh:Ident/mh:Id", nsmgr);
            if (patient != null) patient.InnerText = pasient_id;


            M10 m10 = new M10();

            m10.dokument = Encoding.UTF8.GetBytes(xd.DocumentElement.OuterXml);

            lastLogFileName = "log\\M10_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
            File.WriteAllText(lastLogFileName, xd.OuterXml);

            AppRec apprec = utlW.UtlevererWebService_m10(m10);
            byte[] retval = (byte[])apprec.dokument;
            string retstring = System.Text.Encoding.UTF8.GetString(retval);
           
            lastLogFileName = "log\\Apprec-M10_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xml";
            File.WriteAllText(lastLogFileName, retstring);

            int i = retval.Length;


            return retstring;

        }
    }
}
