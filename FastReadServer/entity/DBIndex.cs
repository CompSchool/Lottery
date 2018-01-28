using common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace entity.mutli
{
    public class DBIndex : BaseEntity
    {
        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public int CreateQuestion(string sType, string sTrainId, string sTitle, string sOpt1, string sOpt2, string sOpt3, string sOpt4, string sRight)
        {
            int iRet = 0;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" insert into td_question  ");
            sb.Append(" (q_type,train_id,title,op1,op2,op3,op4,answer,create_time) ");
            sb.Append(" values ");
            sb.Append(" (?q_type,?train_id,?title,?op1,?op2,?op3,?op4,?answer,now()) ");
            try
            {

                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("q_type", sType);
                m_dbo.AddParameter("train_id", sTrainId);
                m_dbo.AddParameter("title", sTitle);
                m_dbo.AddParameter("op1", sOpt1);
                m_dbo.AddParameter("op2", sOpt2);
                m_dbo.AddParameter("op3", sOpt3);
                m_dbo.AddParameter("op4", sOpt4);
                m_dbo.AddParameter("answer", sRight);
                iRet += this.m_dbo.ExecuteSQL(CConvert.ToString(sb));


            }
            catch (Exception e)
            {
                iRet = -1;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return iRet;
        }

        public int UpdateQuestion(string sId, string sTitle, string sOpt1, string sOpt2, string sOpt3, string sOpt4, string sRight)
        {
            int iRet = 0;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" update td_question set create_time = now(), ");
            sb.Append(" title=?title,op1=?op1,op2=?op2,op3=?op3,op4=?op4,answer=?answer ");
            sb.Append(" where ");
            sb.Append(" q_id=?id ");
            try
            {

                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("id", sId);
                m_dbo.AddParameter("title", sTitle);
                m_dbo.AddParameter("op1", sOpt1);
                m_dbo.AddParameter("op2", sOpt2);
                m_dbo.AddParameter("op3", sOpt3);
                m_dbo.AddParameter("op4", sOpt4);
                m_dbo.AddParameter("answer", sRight);
                iRet += this.m_dbo.ExecuteSQL(CConvert.ToString(sb));


            }
            catch (Exception e)
            {
                iRet = -1;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return iRet;
        }

        public int RemoveQuestion(string sId)
        {
            int iRet = 0;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" Delete from td_question  ");
            sb.Append(" where ");
            sb.Append(" q_id=?id ");
            try
            {

                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("id", sId);
                iRet += this.m_dbo.ExecuteSQL(CConvert.ToString(sb));


            }
            catch (Exception e)
            {
                iRet = -1;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return iRet;
        }


        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public int InsertUpdateLog(string sVerCode)
        {
            int iRet = 0;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" insert into td_update_log  ");
            sb.Append(" (code,last_time) ");
            sb.Append(" values ");
            sb.Append(" (?code,now()) ");
            try
            {
                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("code", sVerCode);
                iRet += this.m_dbo.ExecuteSQL(CConvert.ToString(sb));


            }
            catch (Exception e)
            {
                iRet = -1;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return iRet;
        }

        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public int CreateVerCode(int iNum)
        {
            int iRet = 0;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" insert into td_license  ");
            sb.Append(" (code,state) ");
            sb.Append(" values ");
            sb.Append(" (?code,1) ");
            try
            {

                for (int i = 0; i < iNum; i++)
                {
                    m_dbo.RemoveAllParameters();
                    m_dbo.AddParameter("code", Guid.NewGuid().ToString().Replace("-", ""));
                    iRet += this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
                }

            }
            catch (Exception e)
            {
                iRet = -1;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return iRet;
        }
        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public DataSet getAnimaByTime(string sLastTime)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_anima ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND create_time >= ?create_time ");
            sb.Append(" order by img_id ");

            m_dbo.AddParameter("create_time", sLastTime);
            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public DataSet getTrainByTime(string sType, string sLastTime)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_training ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND train_type = ?train_type ");
            sb.Append(" AND create_time >= ?create_time ");
            sb.Append(" order by train_id ");

            m_dbo.AddParameter("train_type", sType);
            m_dbo.AddParameter("create_time", sLastTime);
            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sLastTime">最后更新时间</param>
        /// <returns></returns>
        public DataSet getViewByTime(string sLastTime)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_view_training ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND create_time >= ?create_time ");
            sb.Append(" order by vt_type,viewtrain_id ");

            m_dbo.AddParameter("create_time", sLastTime);
            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }
        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public DataSet getQuesByTime(string sType, string sLastTime)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_question ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND q_type = ?q_type ");
            sb.Append(" AND create_time >= ?create_time ");
            sb.Append(" order by create_time ");

            m_dbo.AddParameter("q_type", sType);
            m_dbo.AddParameter("create_time", sLastTime);
            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }
        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <returns></returns>
        public DataSet GetAllCarousel(string sType)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_anima ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" order by img_id desc ");

            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }
        /// <summary>
        /// 获取在线咨询信息
        /// </summary>
        /// <param name="iPage">页码</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetQAList(string sAll, string sContent, int iPage, int iLimit, ref int iAllRows)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();

            sb.Append(" select ");
            sb.Append("  count(*) cnt  ");
            sb.Append(" from td_qa ");
            sb.Append(" WHERE 1 = 1 ");
            if (sAll == "")
            {
                sb.Append(" AND state = 1 ");
            }
            if (sContent != "")
            {
                sb.Append(" AND ( nickname LIKE CONCAT('%',?keyworkd,'%') OR problem LIKE CONCAT('%',?keyworkd,'%')  OR reply LIKE CONCAT('%',?keyworkd,'%')  ) ");
            }
            m_dbo.AddParameter("keyworkd", sContent);
            iAllRows = CConvert.ToInt32(m_dbo.Select(sb.ToString()).Tables[0].Rows[0][0]);

            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  *  ");
            sb.Append(" from td_qa ");
            sb.Append(" WHERE 1 = 1 ");
            if (sAll == "")
            {
                sb.Append(" AND state = 1 ");
            }
            if (sContent != "")
            {
                sb.Append(" AND ( nickname LIKE CONCAT('%',?keyworkd,'%') OR problem LIKE CONCAT('%',?keyworkd,'%')  OR reply LIKE CONCAT('%',?keyworkd,'%')  ) ");
            }

            if (iAllRows > (iPage - 1) * iLimit)
            {

                sb.Append(" order by time desc ");

                sb.Append(" limit  " + (iPage - 1) * iLimit + "," + iLimit);
            }
            else
            {
                sb.Append(" AND 1 = 2 ");
            }
            try
            {

                m_dbo.AddParameter("keyworkd", sContent);

                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }
        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <param name="iPage">页码</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetTrainList(string sType, string sSpeed, string sKey, int iPage, int iLimit, ref int iAllRows)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();


            m_dbo.RemoveAllParameters();


            sb.Append(" select ");
            sb.Append("  count(*) cnt  ");
            sb.Append(" from td_training ");
            sb.Append(" WHERE 1 = 1 ");
            if (sType != "")
            {
                sb.Append(" AND train_type = ?train_type ");
                m_dbo.AddParameter("train_type", sType);

            }
            if (sSpeed != "")
            {
                sb.Append(" AND speed = ?speed ");
                m_dbo.AddParameter("speed", sSpeed);

            }
            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  ( title LIKE CONCAT('%',?keyword,'%') OR content LIKE CONCAT('%',?keyword,'%')   )");
            }

            iAllRows = CConvert.ToInt32(m_dbo.Select(sb.ToString()).Tables[0].Rows[0][0]);

            m_dbo.RemoveAllParameters();
            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  t1.*,t2.categoryname speedname  ");
            sb.Append(" from td_training t1 ");

            sb.Append(" left join td_code t2 on t1.speed = t2.category  ");
            sb.Append(" WHERE 1 = 1 ");

            if (sType != "")
            {
                sb.Append(" AND train_type = ?train_type ");
                m_dbo.AddParameter("train_type", sType);

            }
            if (sSpeed != "")
            {
                sb.Append(" AND speed = ?speed ");
                m_dbo.AddParameter("speed", sSpeed);

            }
            if (sKey != "")
            {
                sb.Append(" AND  ( title LIKE CONCAT('%',?keyword,'%') OR content LIKE CONCAT('%',?keyword,'%')   )");
                m_dbo.AddParameter("keyword", sKey);
            }

            if (iAllRows > (iPage - 1) * iLimit)
            {

                sb.Append(" order by create_time desc ");

                sb.Append(" limit  " + (iPage - 1) * iLimit + "," + iLimit);
            }
            else
            {
                sb.Append(" AND 1 = 2 ");
            }


            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }
        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sType">类型</param>
        /// <param name="sKey">关键字</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetViewList(string sType, string sKey, int iPage, int iLimit, ref int iAllRows)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();


            m_dbo.RemoveAllParameters();


            sb.Append(" select ");
            sb.Append("  count(*) cnt  ");
            sb.Append(" from td_view_training ");
            sb.Append(" WHERE 1 = 1 ");
            if (sType != "")
            {
                sb.Append(" AND vt_type = ?vt_type ");
                m_dbo.AddParameter("vt_type", sType);

            }
            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  title LIKE CONCAT('%',?keyword,'%')  ");
            }

            iAllRows = CConvert.ToInt32(m_dbo.Select(sb.ToString()).Tables[0].Rows[0][0]);

            m_dbo.RemoveAllParameters();
            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  t1.*  ");
            sb.Append(" from td_view_training t1 ");

            sb.Append(" WHERE 1 = 1 ");

            if (sType != "")
            {
                sb.Append(" AND vt_type = ?vt_type ");
                m_dbo.AddParameter("vt_type", sType);

            }
            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  title LIKE CONCAT('%',?keyword,'%')  ");
            }

            if (iAllRows > (iPage - 1) * iLimit)
            {

                sb.Append(" order by create_time desc ");

                sb.Append(" limit  " + (iPage - 1) * iLimit + "," + iLimit);
            }
            else
            {
                sb.Append(" AND 1 = 2 ");
            }


            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sKey">授权信息</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetVercodeList(string sKey, int iPage, int iLimit, ref int iAllRows)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();


            m_dbo.RemoveAllParameters();


            sb.Append(" select ");
            sb.Append("  count(*) cnt  ");
            sb.Append(" from td_license ");
            sb.Append(" WHERE 1 = 1 ");
            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  ( code LIKE CONCAT('%',?keyword,'%') OR bak LIKE CONCAT('%',?keyword,'%')   )");
            }

            iAllRows = CConvert.ToInt32(m_dbo.Select(sb.ToString()).Tables[0].Rows[0][0]);

            m_dbo.RemoveAllParameters();
            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  t1.*  ");
            sb.Append(" from td_license t1 ");

            sb.Append(" WHERE 1 = 1 ");

            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  ( code LIKE CONCAT('%',?keyword,'%') OR bak LIKE CONCAT('%',?keyword,'%')   )");
            }

            if (iAllRows > (iPage - 1) * iLimit)
            {

                sb.Append(" order by id desc ");

                sb.Append(" limit  " + (iPage - 1) * iLimit + "," + iLimit);
            }
            else
            {
                sb.Append(" AND 1 = 2 ");
            }


            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sKey">授权信息</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetQuesList(string sTestId, string sQuesType)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();


            m_dbo.RemoveAllParameters();



            m_dbo.RemoveAllParameters();
            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  t1.*  ");
            sb.Append(" from td_question t1 ");

            sb.Append(" WHERE 1 = 1 ");

            sb.Append(" and  q_type=?qtype ");
            sb.Append(" and  train_id=?train_id ");
            m_dbo.AddParameter("qtype", sQuesType);

            m_dbo.AddParameter("train_id", sTestId);


            sb.Append(" order by create_time ");



            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }


        /// <summary>
        /// 获取滚动图片信息
        /// </summary>
        /// <param name="sKey">授权信息</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetVercodeUpdateLog(string sKey, int iPage, int iLimit, ref int iAllRows)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();


            m_dbo.RemoveAllParameters();


            sb.Append(" select ");
            sb.Append("  count(*) cnt  ");
            sb.Append(" from td_update_log ");
            sb.Append(" WHERE 1 = 1 ");
            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  ( code LIKE CONCAT('%',?keyword,'%')    )");
            }

            iAllRows = CConvert.ToInt32(m_dbo.Select(sb.ToString()).Tables[0].Rows[0][0]);

            m_dbo.RemoveAllParameters();
            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  t1.*  ");
            sb.Append(" from td_update_log t1 ");

            sb.Append(" WHERE 1 = 1 ");

            if (sKey != "")
            {
                m_dbo.AddParameter("keyword", sKey);

                sb.Append(" AND  ( code LIKE CONCAT('%',?keyword,'%')  )");
            }

            if (iAllRows > (iPage - 1) * iLimit)
            {

                sb.Append(" order by upd_id desc ");

                sb.Append(" limit  " + (iPage - 1) * iLimit + "," + iLimit);
            }
            else
            {
                sb.Append(" AND 1 = 2 ");
            }


            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取新闻明细
        /// </summary>
        /// <param name="sId">新闻ID</param>
        /// <returns></returns>
        public DataSet GetNewsById(string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_news ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND id = ?id ");

            try
            {
                m_dbo.AddParameter("id", sId);
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }



        /// <summary>
        /// 获取成果明细
        /// </summary>
        /// <param name="sId">成果ID</param>
        /// <returns></returns>
        public DataSet GetAchievementById(string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_achievements ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND id = ?id ");

            try
            {
                m_dbo.AddParameter("id", sId);
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取成果明细
        /// </summary>
        /// <param name="sId">成果ID</param>
        /// <returns></returns>
        public DataSet getVercodeInfoByCode(string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_license ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND code = ?id ");

            try
            {
                m_dbo.AddParameter("id", sId);
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }

        /// <summary>
        /// 获取成果明细
        /// </summary>
        /// <param name="sId">成果ID</param>
        /// <returns></returns>
        public DataSet GetAllAchievement()
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();

            sb.Append(" select * ");
            sb.Append(" from td_achievements ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" ORDER BY time desc ");
            try
            {
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }


        /// <summary>
        /// 获取新闻明细
        /// </summary>
        /// <param name="sNickName">昵称</param>
        /// <param name="sQQ">QQ</param>
        /// <param name="sTel">电话</param>
        /// <param name="sContent">内容</param>
        /// <returns></returns>
        public int CreateQA(string sNickName, string sQQ, string sTel, string sContent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into td_qa(nickname,tel,qq,problem,reply,time,isreply,state)values( ");
            sb.Append(" ?nickname,?tel,?qq,?problem,'',now(),'0','0') ");

            try
            {
                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("nickname", sNickName);
                m_dbo.AddParameter("qq", sQQ);
                m_dbo.AddParameter("tel", sTel);
                m_dbo.AddParameter("problem", sContent);
                return this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQid"></param>
        /// <returns></returns>
        public int UpdateMac(string sVerCode, string sMac)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update td_license set use_time = now(),mac=?mac where code=?code ");

            try
            {
                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("mac", sMac);
                m_dbo.AddParameter("code", sVerCode);
                return this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQid"></param>
        /// <returns></returns>
        public int PassQA(string sQid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update td_qa set state = 1 where id=?qid ");

            try
            {
                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("qid", sQid);
                return this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQid"></param>
        /// <returns></returns>
        public int ChangeVerCode(string sId, string sBak, string sState)
        {
            StringBuilder sb = new StringBuilder();

            try
            {


                m_dbo.RemoveAllParameters();
                sb.Append(" update td_license set id = id ");
                if (sBak != "")
                {
                    sb.Append(" ,bak = ?bak ");
                    m_dbo.AddParameter("bak", sBak);
                }
                if (sState != "")
                {
                    sb.Append(" ,state = ?state ");
                    m_dbo.AddParameter("state", sState);
                }


                sb.Append("  where id=?id ");
                m_dbo.AddParameter("id", sId);
                return this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQid"></param>
        /// <returns></returns>
        public DataSet GetQuesById(string sId)
        {
            StringBuilder sb = new StringBuilder();
            DataSet dst = null;
            try
            {


                m_dbo.RemoveAllParameters();
                sb.Append(" select * from td_question ");
                sb.Append("  where q_id=?id ");
                m_dbo.AddParameter("id", sId);
                return this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQid"></param>
        /// <returns></returns>
        public int ChangePwd(string sUid, string newPwd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update td_user set pwd = ?pwd where id=?uid ");

            try
            {
                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("uid", sUid);
                m_dbo.AddParameter("pwd", newPwd);
                return this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQid"></param>
        /// <param name="sContent"></param>
        /// <returns></returns>
        public int ReplyQA(string sQid, string sContent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update td_qa set reply = ?reply,replytime=now() where id=?qid ");

            try
            {
                m_dbo.RemoveAllParameters();
                m_dbo.AddParameter("reply", sContent);
                m_dbo.AddParameter("qid", sQid);
                return this.m_dbo.ExecuteSQL(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// 获取前一条新闻明细
        /// </summary>
        /// <param name="category">新闻分类</param>
        /// <param name="sId">基准新闻ID</param>
        /// <returns></returns>
        public DataSet getNewsPrev(string category, string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_news ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND category = ?category ");
            sb.Append(" AND id < ?id ");
            sb.Append(" order by id desc ");
            sb.Append(" limit 0,1 ");

            try
            {
                m_dbo.AddParameter("category", category);
                m_dbo.AddParameter("id", sId);
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }/// <summary>
        /// 获取后一条新闻明细
        /// </summary>
        /// <param name="category">新闻分类</param>
        /// <param name="sId">基准新闻ID</param>
        /// <returns></returns>
        public DataSet getNewsNext(string category, string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_news ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND category = ?category ");
            sb.Append(" AND id > ?id ");
            sb.Append(" order by id ");
            sb.Append(" limit 0,1 ");
            try
            {
                m_dbo.AddParameter("category", category);
                m_dbo.AddParameter("id", sId);
                dst = this.m_dbo.Select(CConvert.ToString(sb));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    this.m_dbo.Close();
                }
                catch { }
            }
            return dst;
        }
    }
}
