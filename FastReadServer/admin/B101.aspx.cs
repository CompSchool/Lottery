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
    public partial class B101 : AdminBasePage
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

            DBB100 db = new DBB100();
            DataSet dst = db.GetCode(category);
            this.ddlSpead.DataSource = dst.Tables[0].DefaultView;
            this.ddlSpead.DataTextField = "categoryname";
            this.ddlSpead.DataValueField = "category";
            this.ddlSpead.DataBind();
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
                    DataSet dst = db.GetTrainInfoById(newsid);
                    if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dst.Tables[0].Rows[0];
                        string category = CConvert.ToString(dr["speed"]);
                        this.txtTitle.Text = CConvert.ToString(dr["title"]);
                        this.txtWordsCnt.Text = CConvert.ToString(dr["words"]);
                        newDesc.Value = CConvert.ToString(dr["content"]);
                        imgPhoto.ImageUrl = "../upload/train/" + CConvert.ToString(dr["photo"]) ;

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
                if (ddlSpead.Items.Count == 0)
                {
                    errorMsg += "阅读速度必须选择!";
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
                if (string.IsNullOrEmpty(this.newDesc.Value.Trim()))
                {
                    errorMsg += "内容不能为空!";
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
                string sPeed = ddlSpead.SelectedValue;
                string sContent = newDesc.Value;
                string sWords = txtWordsCnt.Text;
                string sPhoto = "";

                if (sWords == "")
                {
                    sWords = sContent.Length.ToString();
                }

                if (fileImg.HasFile)
                {
                    string[] arrFile = fileImg.FileName.Split('.');
                    sPhoto = DateTime.Now.ToString("yyyyMMddHHmmssfff") +"."+ arrFile[arrFile.Length - 1];

                    string sPath = AppConfig.ImagePath + "train/";
                    if (Directory.Exists(sPath) == false)
                    {
                        Directory.CreateDirectory(sPath);
                    }

                    fileImg.SaveAs(sPath + sPhoto);

                    ReSizeImageFile(sPath + sPhoto, 300, 400);
                }

                string sId = hidNewsId.Value;

                DBB100 db = new DBB100();
                int iRet = 0;
                if (FC_Check.IsNotEmpty(sId))//修改
                {
                    iRet = db.Update(sId, sTitle, sPeed, sPhoto, sContent, sWords);
                }
                else//新建
                {
                    iRet = db.Insert(m_trainType,sTitle, sPeed, sPhoto, sContent, sWords);
                }
                if (iRet > 0)
                {
                    Response.Redirect("B100.aspx");
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