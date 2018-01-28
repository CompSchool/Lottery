using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using common;
using System.Web;

/// <summary>
/// BaseEntity概要说明
/// </summary>
public class BaseSingle
{
    protected DBOperator dbo = new DBOperator();

    private bool m_IsMulti = false;

    public static string ConnectString;

    public BaseSingle(DBOperator m_dbo)
    {
        dbo = m_dbo;
        m_IsMulti = true;
    }

    public BaseSingle()
    {
        //DBOperator.ConnectString = "";
    }
    

    ~BaseSingle()
    {
        try
        {
            if (m_IsMulti == false)
            {
                dbo.Close();
            }
        }
        catch { }
    }

}


