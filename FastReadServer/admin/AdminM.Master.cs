using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastReadServer.admin
{
    public partial class AdminM : System.Web.UI.MasterPage
    {
        protected string m_PageId = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            Console.Out.WriteLine("AdminM Page_Load Start");

            if (!IsPostBack)
            {
                string[] arr = this.Page.ToString().Replace("ASP.", "").Split('_');

                m_PageId = arr[arr.Length - 2].ToUpper();

                UserInfo user = (UserInfo)Session[CConst.CSession.C_UserInfoKey];
                lblUerName.Text = user.UserName;
            }


            Console.Out.WriteLine("AdminM Page_Load End");
        }
    }
}