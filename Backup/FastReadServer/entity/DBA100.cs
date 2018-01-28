using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using common;

namespace FastReadServer.entity
{
    public class DBB400 : BaseEntity
    {

        public DBB400() { }
        public DBB400(DBOperator m_dbo) : base(m_dbo) { }



        public int InsertAnima(string sImg)
        {
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" Insert into td_anima (file_name,create_time) ");
            sb.Append(" Values( ");
            sb.Append(" ?img_src,now() ");



            sb.Append("  ) ");

            try
            {
                m_dbo.AddParameter("img_src", sImg);
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

        public int RemoveAnima(string sId)
        {
            StringBuilder sb = new StringBuilder();
            m_dbo.RemoveAllParameters();
            sb.Append(" delete from td_anima  ");
            sb.Append(" where");
            sb.Append(" img_id=?id");
            try
            {
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
    }
}