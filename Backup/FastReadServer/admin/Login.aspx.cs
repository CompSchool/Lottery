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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove(CConst.CSession.C_UserInfoKey);

                if (HttpContext.Current.Request.Cookies[CConst.CCookie.KEY_PWD] != null)
                {
                    //do something
                    txtPwd.Value = HttpContext.Current.Request.Cookies[CConst.CCookie.KEY_PWD].Value;
                    txtUserName.Value = HttpContext.Current.Request.Cookies[CConst.CCookie.KEY_USERNAME].Value;
                }

                lblMsg.Text = "";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (txtUserName.Value == "")
            {
                lblMsg.Text = "请输入用户名";
                return;
            }
            if (txtPwd.Value == "")
            {
                lblMsg.Text = "请输入密码";
                return;
            }

            DBLogin dbm = new DBLogin();
            DataSet ds = dbm.GetUserInfoByUserName(txtUserName.Value);
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblMsg.Text = "用户名或者密码不正确";
                return;
            }
            else
            {
                string encodePwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Value, "MD5");
                DataRow dr = ds.Tables[0].Rows[0];

                if (CConvert.ToString(dr["pwd"]) != encodePwd)
                {
                    lblMsg.Text = "用户名或者密码不正确";
                    return;

                }
                else
                {
                    UserInfo user = new UserInfo();
                    user.UserId = CConvert.ToString(dr["id"]);
                    user.UserName = CConvert.ToString(dr["nick_name"]);
                    Session[CConst.CSession.C_UserInfoKey] = user;
                }
            }

            if (chkRememberMe.Checked)
            {

                HttpCookie cookie = new HttpCookie(CConst.CCookie.KEY_PWD);
                cookie.Value = txtPwd.Value;
                cookie.Expires = DateTime.Now.AddDays(7);

                HttpCookie cookie2 = new HttpCookie(CConst.CCookie.KEY_USERNAME);
                cookie2.Value = txtPwd.Value;
                cookie2.Expires = DateTime.Now.AddDays(7);


                HttpContext.Current.Response.Cookies.Remove(CConst.CCookie.KEY_PWD);
                HttpContext.Current.Response.Cookies.Remove(CConst.CCookie.KEY_USERNAME);
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Add(cookie2);

            }
            string sJumpPage = CConvert.ToString(Session[CConst.CSession.C_BACK_PAGE]);
            if ( sJumpPage == "")
            {
                sJumpPage = "A000.aspx";
            }
            Response.Redirect(sJumpPage);
        }
    }
}