using common;
using FastReadServer.entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastReadServer.admin
{
    public partial class A100 : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                InitPage();
               
            }

        }

        private void InitPage()
        {
            
        }

       

    }
}