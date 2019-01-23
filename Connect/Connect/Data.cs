using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

public class Data
{
    //public static string str_con = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
    public static string str_con = "DSN=SMILE_MBR_WEB;uid=smile;pwd=AnhMinh167TruongDinh";
    private OdbcConnection connect = new OdbcConnection(str_con);

    public void OpenClose(string isSate)
    {
        if (isSate.ToUpper() == "OPEN")
        {
            connect = new OdbcConnection(str_con);
            connect.Open();
        }
        else
            connect.Close();
    }
    private DataTable GetParamProc(string ProcedureName)
    {
        OpenClose("OPEN");
        DataTable dt = new DataTable();
        string st_sql = "SELECT  p.name, p.is_output, t.name as [type], p.max_length";
        st_sql += " FROM sys.parameters p JOIN sys.objects o ON p.[object_id] = o.[object_id]";
        st_sql += " JOIN sys.types AS t ON t.user_type_id = p.system_type_id";
        st_sql += " WHERE o.name='" + ProcedureName + "'";
        OdbcCommand cmd = new OdbcCommand(st_sql, connect);
        cmd.CommandTimeout = 0;
        cmd.Parameters.Clear();

        dt.Load(cmd.ExecuteReader());
        OpenClose("CLOSE");
        return dt;
    }
    public OdbcCommand GetData(string ProcedureName, List<string> parama)
    {

        string ProName = ProcedureName;
        DataTable dataTable = GetParamProc(ProcedureName);
        if (dataTable.Rows.Count > 0)
        {
            ProName += "(";
            for (int i = 1; i <= dataTable.Rows.Count; i++)
            {
                ProName += "?,";
            }
            ProName = ProName.Substring(0,ProName.Length-1) + ")";
        }

        OpenClose("OPEN");
        DataTable dt = new DataTable();
        OdbcCommand cmd = new OdbcCommand("{call " + ProName + "}", connect);
        cmd.CommandTimeout = 0;
        cmd.Parameters.Clear();
        foreach (DataRow r in dataTable.Rows)
        {
            int index = 0;
            if (r["is_output"].ToString() == "True")
            {
                if (r["type"].ToString() == "nvarchar")
                {
                    if (r["max_length"].ToString() != "-1")
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), int.Parse(r["max_length"].ToString()) / 2).Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), 4000).Direction = ParameterDirection.Output;
                    }
                }
                else
                {
                    if (r["max_length"].ToString() != "-1")
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), int.Parse(r["max_length"].ToString())).Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), 4000).Direction = ParameterDirection.Output;
                    }
                }
            }
            else
            {
                if (r["type"].ToString() == "nvarchar")
                {
                    if (r["max_length"].ToString() != "-1")
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), int.Parse(r["max_length"].ToString()) / 2).Value = parama[index];
                    }
                    else
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), 4000).Value = parama[index];
                    }
                }
                else
                {
                    if (r["max_length"].ToString() != "-1")
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), int.Parse(r["max_length"].ToString())).Value = parama[index];
                    }
                    else
                    {
                        cmd.Parameters.Add(r["name"].ToString(), Libralys.Type(r["type"].ToString()), 4000).Value = parama[index];
                    }
                }
            }
            index++;
        }
        return cmd;
    }
    ///////////////////////////////////////////////////////////////////////
    public DataTable GetData_DataTable(string ProcedureName, List<string> parama)
    {
        DataTable dt = new DataTable();
        OdbcCommand cmd = new OdbcCommand();
        cmd = GetData(ProcedureName, parama);
        dt.Load(cmd.ExecuteReader());
        OpenClose("CLOSE");
        return dt;
    }
    //public string GetData_String(string ProcedureName, List<string> parama)
    //{
    //    DataTable dt = new DataTable();
    //    OdbcCommand cmd = new OdbcCommand();
    //    cmd = GetData(ProcedureName, parama);
    //    cmd.ExecuteReader();
    //    OpenClose("CLOSE");
    //    return resuft;
    //}
}
