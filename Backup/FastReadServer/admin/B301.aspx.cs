using common;
using FastReadServer.entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FastReadServer.admin
{
    public partial class B301 : AdminBasePage
    {

        protected string m_trainType = "1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();

            }
        }


        /// <summary>
        /// 绑定NewsCode
        /// </summary>
        /// <param name="facid"></param>
        private void ddlSpeadBind(string category)
        {
            this.ddlViewType.SelectedValue = category;
            
        }

        private void InitPage()
        {
            try
            {
                string newsid = CConvert.ToString(Request["id"]);
                DBB100 db = new DBB100();
                if (FC_Check.IsNotEmpty(newsid))//修改
                {
                    hidNewsId.Value = newsid;
                    DataSet dst = db.GetViewInfoById(newsid);
                    if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dst.Tables[0].Rows[0];
                        string category = CConvert.ToString(dr["vt_type"]);
                        this.txtTitle.Text = CConvert.ToString(dr["title"]);
                        this.txtDesc.Value = CConvert.ToString(dr["v_desc"]);
                        this.txtContent.Value = CConvert.ToString(dr["content"]);
                        this.hidRoute.Value = CConvert.ToString(dr["route"]);
                        imgPhoto.ImageUrl = "../upload/view/" + CConvert.ToString(dr["photo"]) ;

                        ddlSpeadBind(category);
                    }
                }
                else
                {
                    ddlSpeadBind("");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 输入校验
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            try
            {
                string errorMsg = string.Empty;
                bool result = true;
                //文章类型
                if (ddlViewType.SelectedValue == "")
                {
                    errorMsg += "训练类型必须选择!";
                    errorMsg += "<br/>";
                    result = false;
                }
                //标题
                if (string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
                {
                    errorMsg += "标题不能为空!";
                    errorMsg += "<br/>";
                    result = false;
                }
                //内容
                if (string.IsNullOrEmpty(this.txtContent.Value.Trim()))
                {
                    errorMsg += "内容不能为空!";
                    errorMsg += "<br/>";
                    result = false;
                }
                //说明
                if (string.IsNullOrEmpty(this.txtDesc.Value.Trim()))
                {
                    errorMsg += "说明不能为空!";
                    errorMsg += "<br/>";
                    result = false;
                }
                lbMsg.Text = errorMsg;

                if (hidNewsId.Value == "" && fileImg.HasFile == false)
                {
                    errorMsg += "必须选择封面文件!";
                    errorMsg += "<br/>";
                    result = false;

                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //输入校验
                if (!Check())
                {
                    return;
                }

                string sTitle = txtTitle.Text;
                string sViewType = ddlViewType.SelectedValue;
                string sContent = txtContent.Value;
                string sDesc = txtDesc.Value;
                string sRoute = hidRoute.Value;
                string sPhoto = "";

                

                if (fileImg.HasFile)
                {
                    string[] arrFile = fileImg.FileName.Split('.');
                    sPhoto = DateTime.Now.ToString("yyyyMMddHHmmssfff") +"."+ arrFile[arrFile.Length - 1];

                    string sPath = AppConfig.ImagePath + "view/";
                    if (Directory.Exists(sPath) == false)
                    {
                        Directory.CreateDirectory(sPath);
                    }

                    fileImg.SaveAs(sPath + sPhoto);

                    ReSizeImageFile(sPath + sPhoto, 800, 600);
                }

                string sId = hidNewsId.Value;

                DBB300 db = new DBB300();
                int iRet = 0;
                if (FC_Check.IsNotEmpty(sId))//修改
                {
                    iRet = db.Update(sId, sTitle,sRoute, sPhoto, sContent, sDesc);
                }
                else//新建
                {
                    iRet = db.Insert(ddlViewType.SelectedValue,sTitle, sRoute, sPhoto, sContent, sDesc);
                }
                if (iRet > 0)
                {
                    Response.Redirect("B300.aspx");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("B100.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("B100.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}