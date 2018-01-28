using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using common;

namespace FastReadServer.entity
{
    public class DBLogin : BaseEntity
    {

        public DBLogin() { }
        public DBLogin(DBOperator m_dbo) : base(m_dbo) { }



        public DataSet GetUserInfoByUserName(string sUserName)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select  * ");
            sb.Append(" from td_user ");
            sb.Append(" WHERE 1 = 1 ");



            sb.Append("  and login_name = ?login_name ");

            try
            {
                m_dbo.AddParameter("login_name", sUserName);
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


        public DataSet GetUserInfoById(string sUid)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" select  * ");
            sb.Append(" from td_user ");
            sb.Append(" WHERE 1 = 1 ");

            sb.Append("  and id = ?sUid ");

            try
            {
                m_dbo.AddParameter("sUid", sUid);
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