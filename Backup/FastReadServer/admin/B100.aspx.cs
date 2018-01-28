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
    public partial class B100 : AdminBasePage
    {

        DBB100 db = new DBB100();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                InitPage();
               
            }

        }

        private void InitPage()
        {
            string code = ddlSpead.SelectedValue;
            ddlSpeadBind();
        }

    



        /// <summary>
        /// 绑定NewsCode
        /// </summary>
        /// <param name="facid"></param>
        private void ddlSpeadBind()
        {
            DataSet dst = db.GetNewsCode();

            DataTable dtNew = dst.Tables[0].Clone();
            DataRow drNew = dtNew.Rows.Add();
            drNew["category"] = "";
            drNew["categoryname"] = "请选择";

            foreach (DataRow dr in dst.Tables[0].Rows)
            {
                drNew = dtNew.Rows.Add();
                drNew.ItemArray = dr.ItemArray;

            }

            this.ddlSpead.DataSource = dtNew.DefaultView;
            this.ddlSpead.DataTextField = "categoryname";
            this.ddlSpead.DataValueField = "category";
            this.ddlSpead.DataBind();
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
                CSession.setSession(CConst.PAGEID.PAGE_ID_B100, CConst.CSession.C_NewsId, string.Empty);
                Response.Redirect("B101.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }



    }
}