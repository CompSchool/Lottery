using entity.mutli;
using FastReadServer.entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastReadServer.admin
{
    public partial class B400 : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        public void InitPage()
        {
            DBIndex dbm = new DBIndex();
            DataTable dt = dbm.GetAllCarousel("1").Tables[0];

            rptData.DataSource = dt;
            rptData.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string sFileName = fileImg.FileName;
            fileImg.SaveAs(AppConfig.ImagePath + "/anima/" + sFileName);

            DBB400 dbm = new DBB400();
            dbm.InsertAnima(sFileName);

            Response.Redirect("B400.aspx");
        }
    }
}