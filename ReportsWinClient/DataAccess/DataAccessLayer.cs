using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.Models.ReportModels;
using Library.Utils;
using Newtonsoft.Json;

namespace ReportsWinClient.DataAccess
{
    public static class DataAccessLayer
    {
        public static List<int> ReadFurnaceLines()
        {
            const string uri = "InductionDowntime/Distinctfurnacelines/";
            

            InductionDowntimeRequestObject obj = new InductionDowntimeRequestObject
            {
                StartDate = Convert.ToDateTime("2017-01-01"),
                EndDate = Convert.ToDateTime("2100-01-01"),
                LineNumber = null,
                JobNumber = null,
                ProcessCode = null
            };

            string qs = "?" + StrUtils.GetQueryString(obj);
            string fullurl = $"{Properties.Settings.Default.PortalUrl}{uri}{qs}";

            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallback;
            HttpClient httpclient = new HttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = httpclient.GetAsync(fullurl).Result;
                if (response != null && response.IsSuccessStatusCode && response.Content != null)
                {
                    return JsonConvert.DeserializeObject<List<int>>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }


        public static InductionDowntimeProcessCodeSummary ReadDowntimeEvents(InductionDowntimeRequestObject req)
        {
            const string uri = "InductionDowntime";


            InductionDowntimeRequestObject obj = new InductionDowntimeRequestObject
            {
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                LineNumber = req.LineNumber,
                JobNumber = req.JobNumber,
                ProcessCode = req.ProcessCode,
            };

            string qs = "?" + StrUtils.GetQueryString(obj);
            string fullurl = $"{Properties.Settings.Default.PortalUrl}{uri}{qs}";

            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallback;
            HttpClient httpclient = new HttpClient();

            HttpResponseMessage response;
            try
            {
                response = httpclient.GetAsync(fullurl).Result;
                if (response != null && response.IsSuccessStatusCode && response.Content != null)
                {
                    return JsonConvert.DeserializeObject<InductionDowntimeProcessCodeSummary>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }


        public static bool CertificateValidationCallback(Object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null)
                {
                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                            (status.Status == X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                        }
                        else
                        {
                            if (status.Status != X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            // In all other cases, return false.
            return false;
        }
    }
}
