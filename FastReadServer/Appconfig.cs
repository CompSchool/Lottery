using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using common;
using System.Xml;
using System.IO;

namespace FastReadServer
{
    public class AppConfig
    {
        private const string C_KEY_CONNECTSTRING = "dbconn";
        private const string C_KEY_DEBUG = "isDebug";
        private const string C_TRUE = "true";

       
        //Connect string
        private static string m_strConnectString = "";
        public static string ConnectString
        {
            get { return m_strConnectString; }
            set { m_strConnectString = value; }
        }

        //Debug
        private static bool m_bIsDebug = true;
        public static bool IsDebug
        {
            get { return m_bIsDebug; }
            set { m_bIsDebug = value; }
        }


        //Timeout£®√Î£©
        private static int m_nDBTimeout = 30; //to execute SQL within 30√Î

        public static int DBTimeout
        {
            get { return m_nDBTimeout; }
            set { m_nDBTimeout = value; }
        }
        private static string m_sLogPath = "";

        public static string LogPath
        {
            get { return m_sLogPath; }
            set { m_sLogPath = value; }
        }

        private static string m_sImagePath = "";

        public static string ImagePath
        {
            get { return m_sImagePath; }
        }

        private static string m_sSiteRoot = "";

        public static string SiteRoot
        {
            get { return m_sSiteRoot; }
        }
        /// <summary>
        /// loads the configuration from the config file
        /// </summary>
        public static void Load()
        {
            string strValue = ""; //value

            //initialize

            strValue = ConfigurationManager.AppSettings[C_KEY_CONNECTSTRING];
            if (strValue != null)
                m_strConnectString = strValue;
            string strIsDebug = "";
            strIsDebug = ConfigurationManager.AppSettings[C_KEY_DEBUG];
            if (strIsDebug == C_TRUE)
                m_bIsDebug = true;
            else
                m_bIsDebug = false;
            

            //log ‰≥ˆ¬∑æ∂
            m_sLogPath = ConfigurationManager.AppSettings["LogPath"];
            m_sImagePath = ConfigurationManager.AppSettings["ImagePath"];
            m_sSiteRoot = ConfigurationManager.AppSettings["SiteRoot"];
          
            strValue = ConfigurationManager.AppSettings["DBTimeout"];
            if (strValue != null)
                m_nDBTimeout = CConvert.ToInt16(strValue);


        }


    }
}
