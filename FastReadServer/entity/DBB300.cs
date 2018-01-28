using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using common;

namespace FastReadServer.entity
{
    public class DBB300 : BaseEntity
    {

        public DBB300() { }
        public DBB300(DBOperator m_dbo) : base(m_dbo) { }
      
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        internal int RemoveView(string sId)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" delete from td_view_training where viewtrain_id=?id");

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
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        internal int Update(string sId, string sTitle, string sRoute, string sPhoto, string sContent, string sDesc)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" update td_view_training set create_time=now(), title = ?title,route = ?route,v_desc = ?v_desc, content = ?content");

                m_dbo.AddParameter("title", sTitle);
                m_dbo.AddParameter("route", sRoute);
                m_dbo.AddParameter("v_desc", sDesc);
                m_dbo.AddParameter("content", sContent);



                if (sPhoto != "")
                {
                    sb.Append("    ,photo = ?photo ");
                    m_dbo.AddParameter("photo", sPhoto);
                }

                sb.Append("  where viewtrain_id=?id ");


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
        internal int Insert(string sType,string sTitle, string sRoute, string sPhoto, string sContent, string sDesc)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" insert into td_view_training (vt_type,title,route,v_desc,content,photo,create_time)values(?vt_type,?title,?route,?v_desc,?content,?photo,now())");

                m_dbo.AddParameter("vt_type", sType);
                m_dbo.AddParameter("title", sTitle);
                m_dbo.AddParameter("route", sRoute);
                m_dbo.AddParameter("v_desc", sDesc);
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