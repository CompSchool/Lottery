using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.IO;

namespace FastReadServer.admin
{
    public class AdminBasePage : System.Web.UI.Page
    {
        public UserInfo MUser = new UserInfo();
        public AdminBasePage()
        {
            this.Load += AdminBasePage_Load;
        }

        void AdminBasePage_Load(object sender, EventArgs e)
        {
            Console.Out.WriteLine("AdminBasePage_Load Start");

            if (this.Session[CConst.CSession.C_UserInfoKey] != null)
            {
                MUser = (UserInfo)this.Session[CConst.CSession.C_UserInfoKey];
            }
            else
            {
                //ASP.admin_a100_aspx
                string[] arr = this.Page.ToString().Replace("ASP.", "").Split('_');
                Session[CConst.CSession.C_BACK_PAGE] = arr[arr.Length - 2] + "." + arr[arr.Length - 1];

                Response.Redirect("Login.aspx");
            }

            Console.Out.WriteLine("AdminBasePage_Load End");
        }

        protected void ReSizeImageFile(string sFullName, int width, int height)
        {
            try
            {
                Bitmap mapSrc = new Bitmap(sFullName);

                Bitmap newMap = new Bitmap(mapSrc, new Size(width, height));
                mapSrc.Dispose();
                mapSrc = null;
                File.Delete(sFullName);

                switch (System.IO.Path.GetExtension(sFullName).ToLower())
                {
                    case ".png":
                        newMap.Save(sFullName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        newMap.Save(sFullName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }
            }
            catch
            {

            }
        }

    }
}