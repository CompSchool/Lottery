using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using common;

/// <summary>
/// BaseEntity概要说明
/// </summary>
public class BaseEntity
{

    protected DBOperator m_dbo = new DBOperator();

    private bool m_IsMulti = false;


    protected const int m_Page_Rows = 15;

    public BaseEntity(DBOperator dbo)
    {
        m_dbo = dbo;
        m_IsMulti = true;
    }

    public BaseEntity()
    {
        DBOperator.ConnectString = m_strConnectString;
        //m_dbo.Open();
    }

    //public BaseEntity(string strCon)
    //{
    //    DBOperator.ConnectString = strCon;
    //    m_dbo = new DBOperator();
    //}

    //DB接続文字列
    public const string C_CONNECTSTRING = "";
    private const string C_APPNAME = "";

    //Connect string
    private static string m_strConnectString = C_CONNECTSTRING;
    public static string ConnectString
    {
        get { return m_strConnectString; }
        set { m_strConnectString = value; }
    }

    //Debug
    private static bool m_bIsDebug = true;
    public static bool IsDebug
    {
        get { return m_bIsDebug; }
        set { m_bIsDebug = value; }
    }

    //Timeout（秒）

    private static int m_nDBTimeout = 30; //30秒
    public static int DBTimeout
    {
        get { return m_nDBTimeout; }
        set { m_nDBTimeout = value; }
    }


    ~BaseEntity()
    {
        try
        {
            if (m_IsMulti == false)
            {
                m_dbo.Close();
            }
        }
        catch { }
    }
}


