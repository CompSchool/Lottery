using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using common;

namespace FastReadServer.entity
{
    public class DBB100 : BaseEntity
    {

        public DBB100() { }
        public DBB100(DBOperator m_dbo) : base(m_dbo) { }
        //
        private string m_ID;

        public string id
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        //
        private string m_category;

        public string category
        {
            get { return m_category; }
            set { m_category = value; }
        }

        private string m_categoryname;

        public string categoryname
        {
            get { return m_categoryname; }
            set { m_categoryname = value; }
        }


        private string m_title;

        public string title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        private string m_author;

        public string author
        {
            get { return m_author; }
            set { m_author = value; }
        }

        private string m_time;

        public string time
        {
            get { return m_time; }
            set { m_time = value; }
        }

        private string m_content;

        public string content
        {
            get { return m_content; }
            set { m_content = value; }
        }











        internal DataSet GetNewsCode()
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select  category ,categoryname ");
            sb.Append(" from td_code ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append("  order by id ");

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
        /// 获取指点ID的news文章
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns></returns>
        internal DataSet GetTrainInfoById(string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_training ");
            sb.Append(" WHERE 1 = 1 ");

            if (FC_Check.IsNotEmpty(sId))
            {
                sb.Append(" AND train_id = ?id ");
                m_dbo.AddParameter("id", sId);
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
        /// 获取指点ID的news文章
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns></returns>
        internal DataSet GetViewInfoById(string sId)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select * ");
            sb.Append(" from td_view_training ");
            sb.Append(" WHERE 1 = 1 ");

            if (FC_Check.IsNotEmpty(sId))
            {
                sb.Append(" AND viewtrain_id = ?id ");
                m_dbo.AddParameter("id", sId);
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
        internal DataSet GetCode(string category)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select  category ,categoryname ");
            sb.Append(" from td_code ");
            sb.Append(" WHERE 1 = 1 ");
            if (FC_Check.IsNotEmpty(category))
            {
                sb.Append(" AND category = ?category ");
                m_dbo.AddParameter("category", category);
            }


            sb.Append("  order by id ");

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
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        internal int Update(string sId, string sTitle, string sSpeed, string sPhoto, string sContent, string sWordsCnt)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" update td_training set create_time=now(), title = ?title,speed = ?speed,words = ?words, content = ?content");

                m_dbo.AddParameter("title", sTitle);
                m_dbo.AddParameter("speed", sSpeed);
                m_dbo.AddParameter("words", sWordsCnt);
                m_dbo.AddParameter("content", sContent);



                if (sPhoto != "")
                {
                    sb.Append("    ,photo = ?photo ");
                    m_dbo.AddParameter("photo", sPhoto);
                }

                sb.Append("  where train_id=?id ");


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
        /// 新建
        /// </summary>
        /// <param name="entity"></param>
        internal int Insert(string sType,string sTitle, string sSpeed, string sPhoto, string sContent, string sWordsCnt)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" insert into td_training (train_type,title,speed,words,content,photo,create_time)values(?train_type,?title,?speed,?words,?content,?photo,now())");

                m_dbo.AddParameter("train_type", sType);
                m_dbo.AddParameter("title", sTitle);
                m_dbo.AddParameter("speed", sSpeed);
                m_dbo.AddParameter("words", sWordsCnt);
                m_dbo.AddParameter("content", sContent);
                m_dbo.AddParameter("photo", sPhoto);


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
    }
}