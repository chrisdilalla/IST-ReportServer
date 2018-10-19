using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Web.Configuration;
using Library.Utils;
using MDLibrary.Utils;

namespace WebservicePortal.Utilities
{
    public interface IDatabaseConfiguration
    {
        string GetConnectionString();
    }

    // ReSharper disable once InconsistentNaming
    public class PortalConfig: IDatabaseConfiguration
    {
        //Portal Configuration - first 4 left as examples
        public static string AuthIssuerUrl => ReadStringParameter("AuthIssuerUrl", "");
        public static int AuthErpTokenLifeMins => ReadIntParameter("AuthErpTokenLifeMins", 5);
        public static bool HttpsOnly => ReadBoolParameter("HttpsOnly", true);
        public static double AuthDefaultAccountLockoutTimeMins => ReadIntParameter("AuthDefaultAccountLockoutTimeMins", 15);



        #region Implementation

        public static string ReadStringParameter(string key, string dftValue)
        {
            string ret = dftValue;
            try
            {
                string value = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(value))
                {
                    ret = value;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return ret;
        }

        public static bool ReadBoolParameter(string key, bool dftValue)
        {
            string text = ReadStringParameter(key, dftValue.ToString(CultureInfo.InvariantCulture));
            return StrUtils.CvtStrToBool(text, dftValue);
        }

        public static int ReadIntParameter(string key, int dftValue)
        {
            string text = ReadStringParameter(key, dftValue.ToString(CultureInfo.InvariantCulture));
            return StrUtils.CvtStrToInt(text, dftValue);
        }

        private static string[] ReadStringListParameter(string authaudience, string dftValue)
        {
            string unparsed = ReadStringParameter(authaudience, dftValue);
            return unparsed.Split(';');
        }


        #endregion

        public static string ReadConnString()
        {
            return WebConfigurationManager.ConnectionStrings["InductionData"].ConnectionString;
        }


        string IDatabaseConfiguration.GetConnectionString()
        {
            return ReadConnString();
        }


    }
}