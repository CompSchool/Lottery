using common;
using entity.mutli;
using FastReadServer.entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace FastReadServer
{
    /// <summary>
    /// api 的摘要说明
    /// </summary>
    public class api : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";

            Hashtable htRet = new Hashtable();

            htRet["ok"] = false;
            //检查权限
            if (CheckVertical(context) == true)
            {
                string action = context.Request["act"];

                switch (action)
                {
                    //
                    case "GetAwardUsers":
                        htRet = GetAwardUsersList(context);
                        break;
                    //
                    case "loadQues":
                        htRet = GetQuesList(context);
                        break;
                    //
                    case "addQues":
                        htRet = CreateQues(context);
                        break;
                    //
                    case "removeQues":
                        htRet = RemvoeQues(context);
                        break;
                    //
                    case "updateQues":
                        htRet = UpdateQues(context);
                        break;
                    //
                    case "detailQues":
                        htRet = DetailQues(context);
                        break;
                    //
                    case "loadTrain":
                        htRet = GetTrain(context);
                        break;
                    //修改密码
                    case "changePwd":
                        htRet = ChangePwd(context);
                        break;
                    //修改授权信息  
                    case "changeVerCode":
                        htRet = ChangePwd(context);
                        break;
                    //
                    case "loadVercode":
                        htRet = GetVercodeList(context);
                        break;
                    //
                    case "createVercode":
                        htRet = CreateVercode(context);
                        break;
                    case "vercodeBak":
                        htRet = ChangeVerCodeInfo(context);
                        break;
                    //
                    case "loadView":
                        htRet = GetViewList(context);
                        break;
                    //
                    case "removeView"://删除视幅训练
                        htRet = RemoveView(context);
                        break;
                    //
                    case "loadUpdateLog":
                        htRet = GetUpdateLog(context);
                        break;
                    //
                    case "checkvercode":
                        htRet = CheckVerCode(context);
                        break;
                    //
                    case "update"://更新逻辑
                        htRet = GetUpdateInfo(context);
                        break;
                    //
                    case "removeAnima"://删除动画图片
                        htRet = RemoveAnima(context);
                        break;
                    default:
                        htRet["msg"] = "无效的请求";
                        break;
                }
            }
            else
            {
                htRet["msg"] = "无权限";
            }
            string callback = CConvert.ToString(context.Request["callback"]);
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string sJson = jss.Serialize(htRet);
            if (callback != "")
            {
                sJson = callback + "(" + sJson + ")"; ;
            }
            context.Response.Write(sJson);
        }


        private bool CheckVertical(HttpContext context)
        {
            return true;
        }

        /// <summary>
        /// 获取抽奖用户信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetAwardUsersList(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sVerCode = CConvert.ToString(context.Request["vercode"]);
                int iLimit = CConvert.ToInt32(context.Request["limit"]);
                int iPage = CConvert.ToInt32(context.Request["page"]);

                int allRows = 0;
                DBAwardUsers dbm = new DBAwardUsers();
                DataSet ds = dbm.GetAwardUsersList(sVerCode, iPage, iLimit, ref allRows);

                //DBIndex dbm = new DBIndex();
                //DataSet ds = dbm.GetVercodeList(sVerCode, iPage, iLimit, ref allRows);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["UserID"] = CConvert.ToString(dr["UserID"]);
                        htItem["UserName"] = CConvert.ToString(dr["UserName"]);
                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = allRows;
                    htRet["curpage"] = iPage;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 获取轮播图片信息
        /// </summary>
        /// <returns></returns>
        private Hashtable CheckVerCode(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                DBIndex dbm = new DBIndex();
                string sVerCode = CConvert.ToString(context.Request["vercode"]);
                string sMac = CConvert.ToString(context.Request["mac"]);

                DataSet ds = dbm.getVercodeInfoByCode(sVerCode);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    htRet["msg"] = "授权码不正确！";
                    htRet["ok"] = false;
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    if (CConvert.ToString(dr["mac"]) == "")
                    {
                        if (dbm.UpdateMac(sVerCode, sMac) != 1)
                        {
                            htRet["msg"] = "授权码不正确！";
                            htRet["ok"] = false;
                            return htRet;

                        }
                    }
                    else
                    {
                        if (CConvert.ToString(dr["mac"]) != sMac)
                        {

                            htRet["msg"] = "授权码不正确！";
                            htRet["ok"] = false;
                            return htRet;
                        }
                    }


                    htRet["ok"] = true;
                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }


        /// <summary>
        /// 回复咨询信息
        /// </summary>
        /// <returns></returns>
        private Hashtable CreateVercode(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                int iNum = CConvert.ToInt32(context.Request["num"]);
                if (iNum == 0)
                {

                    htRet["msg"] = "请输入正确的数字！";
                    htRet["ok"] = false;
                    return htRet;
                }

                DBIndex dbm = new DBIndex();
                int iRet = dbm.CreateVerCode(iNum);
                if (iRet <= 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 回复咨询信息
        /// </summary>
        /// <returns></returns>
        private Hashtable RemvoeQues(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sQuesId = CConvert.ToString(context.Request["qid"]);

                DBIndex dbm = new DBIndex();
                int iRet = dbm.RemoveQuestion(sQuesId);
                if (iRet <= 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 回复咨询信息
        /// </summary>
        /// <returns></returns>
        private Hashtable UpdateQues(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sQuesId = CConvert.ToString(context.Request["qid"]);
                string sQuesTitle = CConvert.ToString(context.Request["title"]);
                string sQuesOpt1 = CConvert.ToString(context.Request["op1"]);
                string sQuesOpt2 = CConvert.ToString(context.Request["op2"]);
                string sQuesOpt3 = CConvert.ToString(context.Request["op3"]);
                string sQuesOpt4 = CConvert.ToString(context.Request["op4"]);
                string sQuesRight = CConvert.ToString(context.Request["right"]);

                DBIndex dbm = new DBIndex();
                int iRet = dbm.UpdateQuestion(sQuesId, sQuesTitle, sQuesOpt1, sQuesOpt2, sQuesOpt3, sQuesOpt4, sQuesRight);
                if (iRet <= 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 回复咨询信息
        /// </summary>
        /// <returns></returns>
        private Hashtable CreateQues(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sTrainId = CConvert.ToString(context.Request["id"]);
                string sQuesType = CConvert.ToString(context.Request["qt"]);
                string sQuesTitle = CConvert.ToString(context.Request["title"]);
                string sQuesOpt1 = CConvert.ToString(context.Request["op1"]);
                string sQuesOpt2 = CConvert.ToString(context.Request["op2"]);
                string sQuesOpt3 = CConvert.ToString(context.Request["op3"]);
                string sQuesOpt4 = CConvert.ToString(context.Request["op4"]);
                string sQuesRight = CConvert.ToString(context.Request["right"]);

                DBIndex dbm = new DBIndex();
                int iRet = dbm.CreateQuestion(sQuesType, sTrainId, sQuesTitle, sQuesOpt1, sQuesOpt2, sQuesOpt3, sQuesOpt4, sQuesRight);
                if (iRet <= 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 通过留言信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetUpdateInfo(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {
                string sMode = CConvert.ToString(context.Request["mode"]).Trim();
                if (sMode == "")
                {
                    sMode = "1";//默认【部分更新】
                }

                string sVerCode = CConvert.ToString(context.Request["vercode"]).Trim();
                string sLastTime = CConvert.ToString(context.Request["lasttime"]).Trim();

                DBIndex dbm = new DBIndex();

                DataSet ds = dbm.getVercodeInfoByCode(sVerCode);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    htRet["ok"] = false;
                    htRet["msg"] = "授权码错误";
                    return htRet;
                }


                if (sMode == "2")
                {
                    sLastTime = "2010-01-01 00:00:00";
                }
                //临时文件夹
                string sUpdTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string sPath = AppConfig.ImagePath + sVerCode + "/";

                if (Directory.Exists(sPath) == false)
                {
                    Directory.CreateDirectory(sPath);
                }

                DirectoryInfo di = new DirectoryInfo(sPath);
                di.Delete(true);
                if (Directory.Exists(sPath + "anima/") == false)
                {
                    Directory.CreateDirectory(sPath + "anima/");
                }
                if (Directory.Exists(sPath + "train/") == false)
                {
                    Directory.CreateDirectory(sPath + "train/");
                }
                if (Directory.Exists(sPath + "test/") == false)
                {
                    Directory.CreateDirectory(sPath + "test/");
                }
                if (Directory.Exists(sPath + "view/") == false)
                {
                    Directory.CreateDirectory(sPath + "view/");
                }


                //返回的json数据
                Hashtable htJson = new Hashtable();

                //获取最近更新日期之后的 卡通图标
                ArrayList lstAnima = new ArrayList();
                DataSet dsAnima = dbm.getAnimaByTime(sLastTime);

                foreach (DataRow dr in dsAnima.Tables[0].Rows)
                {
                    Hashtable ht = new Hashtable();
                    ht["id"] = CConvert.ToString(dr["img_id"]);
                    ht["file_name"] = CConvert.ToString(dr["file_name"]);

                    File.Copy(AppConfig.ImagePath + "anima/" + CConvert.ToString(dr["file_name"]), sPath + "anima/" + CConvert.ToString(dr["file_name"]));
                    lstAnima.Add(ht);
                }
                htJson["anima"] = lstAnima;

                //课堂训练
                ArrayList lstTrain = new ArrayList();

                DataSet dsTrain = dbm.getTrainByTime("1", sLastTime);
                foreach (DataRow dr in dsTrain.Tables[0].Rows)
                {
                    Hashtable ht = new Hashtable();

                    ht["id"] = CConvert.ToString(dr["train_id"]);
                    ht["title"] = CConvert.ToString(dr["title"]);
                    ht["photo"] = CConvert.ToString(dr["photo"]);
                    ht["content"] = CConvert.ToString(dr["content"]);
                    ht["words"] = CConvert.ToString(dr["words"]);
                    ht["speed"] = CConvert.ToString(dr["speed"]);

                    File.Copy(AppConfig.ImagePath + "train/" + CConvert.ToString(dr["photo"]), sPath + "train/" + CConvert.ToString(dr["photo"]));

                    lstTrain.Add(ht);
                }
                htJson["train"] = lstTrain;



                //阅读测评
                ArrayList lstTest = new ArrayList();

                DataSet dsTest = dbm.getTrainByTime("2", sLastTime);
                foreach (DataRow dr in dsTest.Tables[0].Rows)
                {
                    Hashtable ht = new Hashtable();

                    ht["id"] = CConvert.ToString(dr["train_id"]);
                    ht["title"] = CConvert.ToString(dr["title"]);
                    ht["photo"] = CConvert.ToString(dr["photo"]);
                    ht["content"] = CConvert.ToString(dr["content"]);
                    ht["words"] = CConvert.ToString(dr["words"]);
                    ht["speed"] = CConvert.ToString(dr["speed"]);
                    File.Copy(AppConfig.ImagePath + "test/" + CConvert.ToString(dr["photo"]), sPath + "test/" + CConvert.ToString(dr["photo"]));

                    lstTest.Add(ht);
                }
                htJson["test"] = lstTest;


                //测评问题
                ArrayList lstQues = new ArrayList();
                DataSet dsQues = dbm.getQuesByTime("2", sLastTime);
                foreach (DataRow dr in dsQues.Tables[0].Rows)
                {
                    Hashtable ht = new Hashtable();

                    ht["id"] = CConvert.ToString(dr["q_id"]);
                    ht["q_type"] = CConvert.ToString(dr["q_type"]);
                    ht["train_id"] = CConvert.ToString(dr["train_id"]);
                    ht["title"] = CConvert.ToString(dr["title"]);
                    ht["op1"] = CConvert.ToString(dr["op1"]);
                    ht["op2"] = CConvert.ToString(dr["op2"]);
                    ht["op3"] = CConvert.ToString(dr["op3"]);
                    ht["op4"] = CConvert.ToString(dr["op4"]);
                    ht["answer"] = CConvert.ToString(dr["answer"]);

                    lstQues.Add(ht);
                }
                htJson["question"] = lstQues;


                //视幅拓展训练
                ArrayList lstView = new ArrayList();
                DataSet dsView = dbm.getViewByTime( sLastTime);
                foreach (DataRow dr in dsView.Tables[0].Rows)
                {
                    Hashtable ht = new Hashtable();

                    ht["id"] = CConvert.ToString(dr["viewtrain_id"]);
                    ht["type"] = CConvert.ToString(dr["vt_type"]);
                    ht["title"] = CConvert.ToString(dr["title"]);
                    ht["photo"] = CConvert.ToString(dr["photo"]);
                    ht["content"] = CConvert.ToString(dr["content"]);
                    ht["route"] = CConvert.ToString(dr["route"]);
                    ht["v_desc"] = CConvert.ToString(dr["v_desc"]);

                    File.Copy(AppConfig.ImagePath + "view/" + CConvert.ToString(dr["photo"]), sPath + "view/" + CConvert.ToString(dr["photo"]));

                    lstView.Add(ht);
                }
                htJson["view"] = lstView;


                JavaScriptSerializer jss = new JavaScriptSerializer();

                StreamWriter sw = new StreamWriter(sPath + "data.json");
                sw.Write(jss.Serialize(htJson));
                sw.Flush();
                sw.Close();

                ZipOutputStream s = new ZipOutputStream(File.Create(AppConfig.ImagePath + sVerCode + sUpdTime + ".zip"));

                s.SetLevel(6);

                Compress(sPath, sPath, s);

                s.Finish();
                s.Close();

                htRet["ok"] = true;
                htRet["lasttime"] = sUpdTime;
                htRet["file"] = AppConfig.SiteRoot + "upload/" + sVerCode + sUpdTime + ".zip";


                dbm.InsertUpdateLog(sVerCode);

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="s">ZipOutputStream对象</param>
        public void Compress(string parent, string source, ZipOutputStream s)
        {
            string[] filenames = Directory.GetFileSystemEntries(source);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    Compress(parent, file, s);  //递归压缩子文件夹
                }
                else
                {
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[4 * 1024];
                        ZipEntry entry = new ZipEntry(file.Replace(parent, ""));     //此处去掉盘符，如D:\123\1.txt 去掉D:
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);

                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
            }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        private Hashtable ChangePwd(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {


                string sOldPwd = CConvert.ToString(context.Request["opwd"]).Trim();
                string sNewPwd = CConvert.ToString(context.Request["npwd"]).Trim();

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }
                string sUid = user.UserId;

                DBIndex dbm = new DBIndex();

                DataSet ds = new DBLogin().GetUserInfoById(sUid);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    htRet["msg"] = "用户不存在！";
                    htRet["ok"] = false;
                    return htRet;
                }
                if (System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sOldPwd, "MD5") != CConvert.ToString(ds.Tables[0].Rows[0]["pwd"]))
                {

                    htRet["msg"] = "旧密码不正确！";
                    htRet["ok"] = false;
                    return htRet;
                }

                int iRet = dbm.ChangePwd(sUid, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sNewPwd, "MD5"));
                if (iRet == 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                    return htRet;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }



        /// <summary>
        /// 修改授权信息
        /// </summary>
        /// <returns></returns>
        private Hashtable ChangeVerCodeInfo(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {


                string sId = CConvert.ToString(context.Request["id"]).Trim();
                string sBak = CConvert.ToString(context.Request["bak"]).Trim();
                string sState = CConvert.ToString(context.Request["state"]).Trim();

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                DBIndex dbm = new DBIndex();
                int iRet = dbm.ChangeVerCode(sId, sBak, sState);
                if (iRet == 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                    return htRet;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 修改授权信息
        /// </summary>
        /// <returns></returns>
        private Hashtable DetailQues(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {


                string sId = CConvert.ToString(context.Request["qid"]).Trim();

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetQuesById(sId);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    htRet["msg"] = "指定的问题不存在！";
                    htRet["ok"] = false;
                    return htRet;
                }
                else
                {
                    htRet["ok"] = true;

                    DataRow dr = ds.Tables[0].Rows[0];

                    Hashtable ht = new Hashtable();

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        ht[dc.ColumnName] = CConvert.ToString(dr[dc.ColumnName]);
                    }

                    htRet["data"] = ht;



                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 删除滚动图
        /// </summary>
        /// <returns></returns>
        private Hashtable RemoveAnima(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {


                string sImg = CConvert.ToString(context.Request["id"]).Trim();
                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                }
                DBB400 dbm = new DBB400();

                int iRet = dbm.RemoveAnima(sImg);
                if (iRet == 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 删除滚动图
        /// </summary>
        /// <returns></returns>
        private Hashtable RemoveView(HttpContext context)
        {
            Hashtable htRet = new Hashtable();

            try
            {


                string sViewId = CConvert.ToString(context.Request["vid"]).Trim();
                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                }
                DBB300 dbm = new DBB300();

                int iRet = dbm.RemoveView(sViewId);
                if (iRet == 0)
                {
                    htRet["msg"] = "服务器异常，请联系管理员！";
                    htRet["ok"] = false;
                }
                else
                {
                    htRet["ok"] = true;

                }

            }
            catch (Exception ex)
            {

                htRet["ok"] = false;
                htRet["msg"] = "处理失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 获取在线咨询列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetArchiveList(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {
                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetAllAchievement();
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["id"] = CConvert.ToString(dr["id"]);
                        htItem["title"] = CConvert.ToString(dr["title"]);
                        htItem["time"] = CConvert.ToString(dr["time"]);
                        // htItem["content"] = System.Web.HttpUtility.HtmlDecode(CConvert.ToString(dr["content"]));
                        htItem["bigimg"] = CConvert.ToString(dr["bigimg"]);
                        htItem["img"] = CConvert.ToString(dr["img"]);

                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 获取在线咨询列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetQaList(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {
                int iLimit = CConvert.ToInt32(context.Request["limit"]);
                int iPage = CConvert.ToInt32(context.Request["page"]);
                string sContent = CConvert.ToString(context.Request["sname"]);
                string sAll = CConvert.ToString(context.Request["all"]);

                int allRows = 0;
                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetQAList(sAll, sContent, iPage, iLimit, ref allRows);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["nickname"] = CConvert.ToString(dr["nickname"]);
                        htItem["problem"] = (CConvert.ToString(dr["problem"]));
                        htItem["time"] = CConvert.ToString(dr["time"]);
                        htItem["id"] = CConvert.ToString(dr["id"]);
                        htItem["state"] = CConvert.ToString(dr["state"]);
                        htItem["reply"] = (CConvert.ToString(dr["reply"]));
                        htItem["replytime"] = CConvert.ToString(dr["replytime"]);

                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = allRows;
                    htRet["curpage"] = iPage;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 获取新闻列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetTrain(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sType = CConvert.ToString(context.Request["tp"]);
                string sSpeed = CConvert.ToString(context.Request["speed"]);
                string sKey = CConvert.ToString(context.Request["key"]);
                int iLimit = CConvert.ToInt32(context.Request["limit"]);
                int iPage = CConvert.ToInt32(context.Request["page"]);

                int allRows = 0;
                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetTrainList(sType, sSpeed, sKey, iPage, iLimit, ref allRows);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["title"] = CConvert.ToString(dr["title"]);
                        htItem["create_time"] = CConvert.ToString(dr["create_time"]);
                        htItem["train_id"] = CConvert.ToString(dr["train_id"]);
                        htItem["photo"] = CConvert.ToString(dr["photo"]);
                        htItem["content"] = CConvert.ToString(dr["content"]);
                        htItem["words"] = CConvert.ToString(dr["words"]);
                        htItem["speed"] = CConvert.ToString(dr["speed"]);
                        htItem["speedname"] = CConvert.ToString(dr["speedname"]);
                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = allRows;
                    htRet["curpage"] = iPage;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 获取新闻列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetViewList(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sViewType = CConvert.ToString(context.Request["vt"]);
                string sKey = CConvert.ToString(context.Request["key"]);
                int iLimit = CConvert.ToInt32(context.Request["limit"]);
                int iPage = CConvert.ToInt32(context.Request["page"]);

                int allRows = 0;
                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetViewList(sViewType, sKey, iPage, iLimit, ref allRows);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["title"] = CConvert.ToString(dr["title"]);
                        htItem["create_time"] = CConvert.ToString(dr["create_time"]);
                        htItem["viewtrain_id"] = CConvert.ToString(dr["viewtrain_id"]);
                        htItem["photo"] = CConvert.ToString(dr["photo"]);
                        htItem["content"] = CConvert.ToString(dr["content"]);
                        htItem["route"] = CConvert.ToString(dr["route"]);
                        htItem["desc"] = CConvert.ToString(dr["v_desc"]);
                        htItem["vt_type"] = CConvert.ToString(dr["vt_type"]);
                        switch (CConvert.ToString(dr["vt_type"]))
                        {
                            case "1":
                                htItem["vt_name"] = "视点移动训练";
                                break;
                            case "2":
                                htItem["vt_name"] = "视幅扩展训练";
                                break;
                            case "3":
                                htItem["vt_name"] = "瞬间感知能力";
                                break;

                        }
                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = allRows;
                    htRet["curpage"] = iPage;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 获取新闻列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetVercodeList(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sVerCode = CConvert.ToString(context.Request["vercode"]);
                int iLimit = CConvert.ToInt32(context.Request["limit"]);
                int iPage = CConvert.ToInt32(context.Request["page"]);

                int allRows = 0;
                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetVercodeList(sVerCode, iPage, iLimit, ref allRows);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["id"] = CConvert.ToString(dr["id"]);
                        htItem["code"] = CConvert.ToString(dr["code"]);
                        htItem["mac"] = CConvert.ToString(dr["mac"]);
                        htItem["use_time"] = CConvert.ToString(dr["use_time"]);
                        if (CConvert.ToString(dr["state"]) == "1")
                        {
                            htItem["state_name"] = "正常";
                        }
                        else
                        {
                            htItem["state_name"] = "停用";
                        }
                        htItem["state"] = CConvert.ToString(dr["state"]);
                        htItem["bak"] = CConvert.ToString(dr["bak"]);
                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = allRows;
                    htRet["curpage"] = iPage;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }

        /// <summary>
        /// 获取新闻列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetQuesList(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sTestId = CConvert.ToString(context.Request["id"]);
                string sQType = CConvert.ToString(context.Request["qt"]);

                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetQuesList(sTestId, sQType);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();


                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            htItem[dc.ColumnName] = CConvert.ToString(dr[dc.ColumnName]);

                        }

                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = lst.Count;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }


        /// <summary>
        /// 获取新闻列表信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetUpdateLog(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {

                FastReadServer.admin.UserInfo user = (FastReadServer.admin.UserInfo)context.Session[FastReadServer.admin.CConst.CSession.C_UserInfoKey];
                if (user == null || user.UserId == "")
                {
                    htRet["msg"] = "用户信息超时，请重新登录！";
                    htRet["ok"] = false;
                    return htRet;
                }

                string sVerCode = CConvert.ToString(context.Request["vercode"]);
                int iLimit = CConvert.ToInt32(context.Request["limit"]);
                int iPage = CConvert.ToInt32(context.Request["page"]);

                int allRows = 0;
                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetVercodeUpdateLog(sVerCode, iPage, iLimit, ref allRows);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Hashtable htItem = new Hashtable();

                        htItem["code"] = CConvert.ToString(dr["code"]);
                        htItem["last_time"] = CConvert.ToString(dr["last_time"]);
                        lst.Add(htItem);
                    }

                    htRet["ok"] = true;
                    htRet["lst"] = lst;
                    htRet["cnt"] = allRows;
                    htRet["curpage"] = iPage;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取轮播信息失败！" + ex.Message;
            }
            return htRet;
        }
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <returns></returns>
        private Hashtable GetDetail(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {
                string sID = CConvert.ToString(context.Request["id"]);

                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetNewsById(sID);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    DataRow dr = ds.Tables[0].Rows[0];
                    Hashtable htItem = new Hashtable();

                    htItem["category"] = CConvert.ToString(dr["category"]);
                    htItem["categoryname"] = CConvert.ToString(dr["categoryname"]);
                    htItem["title"] = CConvert.ToString(dr["title"]);
                    htItem["author"] = CConvert.ToString(dr["author"]);
                    htItem["time"] = CConvert.ToString(dr["time"]);
                    htItem["content"] = CConvert.ToString(dr["content"]);

                    //前一条
                    DataSet ds1 = dbm.getNewsPrev(CConvert.ToString(dr["category"]), sID);
                    Hashtable htPrev = new Hashtable();
                    htPrev["url"] = "";
                    htPrev["title"] = "";
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        htPrev["title"] = CConvert.ToString(ds1.Tables[0].Rows[0]["title"]);
                        htPrev["id"] = CConvert.ToString(ds1.Tables[0].Rows[0]["id"]);
                    }

                    //后一条
                    DataSet ds2 = dbm.getNewsNext(CConvert.ToString(dr["category"]), sID);
                    Hashtable htnext = new Hashtable();
                    htnext["url"] = "";
                    htnext["title"] = "";
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        htnext["title"] = CConvert.ToString(ds2.Tables[0].Rows[0]["title"]);
                        htnext["id"] = CConvert.ToString(ds2.Tables[0].Rows[0]["id"]);
                    }


                    htRet["ok"] = true;
                    htRet["data"] = htItem;
                    htRet["prev"] = htPrev;
                    htRet["next"] = htnext;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取新闻详情失败！" + ex.Message;
            }
            return htRet;
        }


        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <returns></returns>
        private Hashtable GetArchiveDetail(HttpContext context)
        {
            Hashtable htRet = new Hashtable();
            Hashtable rowData = new Hashtable();
            try
            {
                string sID = CConvert.ToString(context.Request["id"]);

                DBIndex dbm = new DBIndex();
                DataSet ds = dbm.GetAchievementById(sID);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    htRet["ok"] = true;
                    htRet["cnt"] = 0;
                    htRet["msg"] = "无数据！";
                }
                else
                {
                    ArrayList lst = new ArrayList();
                    DataRow dr = ds.Tables[0].Rows[0];
                    Hashtable htItem = new Hashtable();

                    htItem["id"] = CConvert.ToString(dr["id"]);
                    htItem["title"] = CConvert.ToString(dr["title"]);
                    htItem["time"] = CConvert.ToString(dr["time"]);
                    htItem["content"] = System.Web.HttpUtility.HtmlDecode(CConvert.ToString(dr["content"]));
                    htItem["bigimg"] = CConvert.ToString(dr["bigimg"]);
                    htItem["img"] = CConvert.ToString(dr["img"]);


                    htRet["ok"] = true;
                    htRet["data"] = htItem;
                }

            }
            catch (Exception ex)
            {

                htRet["err"] = true;
                htRet["msg"] = "获取新闻详情失败！" + ex.Message;
            }
            return htRet;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}