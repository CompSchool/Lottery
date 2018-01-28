using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using common;

namespace FastReadServer
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Initial
            AppConfig.Load();
            DBOperator.ConnectString = AppConfig.ConnectString;

            BaseEntity.IsDebug = AppConfig.IsDebug;
            BaseEntity.DBTimeout = AppConfig.DBTimeout;
            BaseEntity.ConnectString = AppConfig.ConnectString;

            //DBTM_MESSAGE entMsgData = new DBTM_MESSAGE();
            //Message.IsDebug = AppConfig.IsDebug;
            //Message.Data = entMsgData.SelectByKey();

            //
            // Register a handler for SiteMap.SiteMapResolve events to hide the
            // root node from SiteMapPath controls.
            //
            //SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(HideRootNode);
        }
        static SiteMapNode HideRootNode(Object sender, SiteMapResolveEventArgs e)
        {
            //
            // Hide the root node from SiteMapPath controls by cloning the site
            // map from the current node up to the node below the root node and
            // setting that node's ParentNode property to null.
            //
            if (SiteMap.CurrentNode == null)
            {
                return null;
            }

            SiteMapNode node = SiteMap.CurrentNode.Clone();
            SiteMapNode current = node;
            SiteMapNode root = SiteMap.RootNode;

            if (current != root) // Just in case the current node *is* the root node!
            {
                while (node.ParentNode != root)
                {
                    node.ParentNode = node.ParentNode.Clone();
                    node = node.ParentNode;
                }
                node.ParentNode = null;
            }
            return current;
        }
        protected void Application_End(object sender, EventArgs e)
        {


        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            try
            {
                //LogWriter.dbLogWriter(((Users)Session["USERS"]).getSimei_cd(),
                //                      ((Users)Session["USERS"]).getSimei_nm(),
                //                      ((Users)Session["USERS"]).getRiyou_kb(),
                //                      Request.Url.ToString(), 0, ex.Source, ex.Message);
                //LogWriter.dbLogWriter("",
                //                      "",
                //                      "",
                //                      Request.Url.ToString(), 0, ex.Source, ex.Message);
            }
            catch { }

            try
            {
                Context.ClearError();

            }
            catch { }
        }

    }
}