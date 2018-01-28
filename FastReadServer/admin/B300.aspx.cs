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
    public partial class B300 : AdminBasePage
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


        

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("B301.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }



    }
}