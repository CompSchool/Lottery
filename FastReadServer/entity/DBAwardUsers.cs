using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using common;

namespace FastReadServer.entity
{
    public class DBAwardUsers : BaseEntity
    {
         public DBAwardUsers() { }
         public DBAwardUsers(DBOperator m_dbo) : base(m_dbo) { }
      
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

                sb.Append(" delete from AwardUsers where UserID=?id");

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
        internal int Update(string sId, string sUserName)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" update AwardUsers set UserName = ?userName");

                m_dbo.AddParameter("userName", sUserName);

                sb.Append("  where UserID=?id ");


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
        internal int Insert(int iUserID,string sUserName)
        {
            try
            {
                m_dbo.RemoveAllParameters();
                StringBuilder sb = new StringBuilder();


                sb.Append(" insert into AwardUsers (UserID,UserName)values(?UserID,?UserName)");

                m_dbo.AddParameter("UserID", iUserID);
                m_dbo.AddParameter("UserName", sUserName);


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
        /// 获取抽奖用户信息
        /// </summary>
        /// <param name="sKey">授权信息</param>
        /// <param name="iLimit">每页条数</param>
        /// <param name="hasMore">总条数</param>
        /// <returns></returns>
        public DataSet GetAwardUsersList(string sKey, int iPage, int iLimit, ref int iAllRows)
        {
            DataSet dst = null;
            StringBuilder sb = new StringBuilder();


            m_dbo.RemoveAllParameters();


            sb.Append(" select ");
            sb.Append("  count(*) cnt  ");
            sb.Append(" from AwardUsers ");
            sb.Append(" WHERE 1 = 1 ");

            iAllRows = CConvert.ToInt32(m_dbo.Select(sb.ToString()).Tables[0].Rows[0][0]);

            m_dbo.RemoveAllParameters();

            sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append("  t1.*  ");
            sb.Append(" from AwardUsers t1 ");

            sb.Append(" WHERE 1 = 1 ");

           
            if (iAllRows > (iPage - 1) * iLimit)
            {

                sb.Append(" order by UserID desc ");

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
    }


}