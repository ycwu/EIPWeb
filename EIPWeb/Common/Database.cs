using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SCMWeb
{
    /// <summary>
    /// Database 的摘要描述
    /// </summary>
    public class Database
    {
        DLLConfig dllconfig = new DLLConfig();
        public Database()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region ERP Table
        //取得資料表同步設定檔資料表
        public DataTable GetSyncTableDataTable()
        {
            string sSQL = @"
SELECT * FROM SyncTable WHERE IsEnable=1
";
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Connection = con;
            DataTable dtResult = this.GetDataTable(cmd);
            return dtResult;
        }
        //取得資料表同步設定檔資料表
        public DataTable GetSyncTableDataTable(string sSourceTable)
        {
            string sSQL = @"
SELECT * FROM SyncTable WHERE IsEnable=1 AND SourceTable=@SourceTable
";
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Parameters.Add("@Table", SqlDbType.NVarChar).Value = sSourceTable;
            cmd.Connection = con;
            DataTable dtResult = this.GetDataTable(cmd);
            return dtResult;
        }
        //執行同步資料表指令
        public bool DoSyncTable(string sSQL)
        {
            bool bResult = false;
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Connection = con;
            int iResult = this.ExecuteNonQuery(cmd);
            if (iResult >= 0)
                bResult = true;
            return bResult;
        }
        //取得資料表欄位清單
        public string GetColumnList(string sTableName)
        {
            string sResult = "";
            string sSQL = @"
DECLARE @Column NVARCHAR(4000)='',@Result NVARCHAR(4000)
SELECT @Column=@Column+COLUMN_NAME+',' from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{0}' AND COLUMN_NAME<>'IsTransfer'
SELECT @Result=SUBSTRING(@Column,1,LEN(@Column)-1)
SELECT @Result
";
            sSQL = string.Format(sSQL, sTableName);
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Connection = con;
            object oResult = this.ExecuteScalar(cmd);
            if (oResult != null)
                sResult = oResult.ToString();
            return sResult;
        }

        //取得公司別資料表
        public DataTable GetCompanyDataTable()
        {
            string sSQL = @"
SELECT * FROM Company WHERE IsEnable=1 ORDER BY UniqueID
";
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Connection = con;
            DataTable dtResult = this.GetDataTable(cmd);
            return dtResult;
        }
        //取得請購單-單頭
        public DataTable GetPURTADataTable(string sCOMPANY)
        {
            string sSQL = @"
SELECT * FROM PURTA WHERE COMPANY=@COMPANY AND IsTransfer=0
";
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Parameters.Add("@COMPANY", SqlDbType.NVarChar).Value = sCOMPANY;
            cmd.Connection = con;
            DataTable dtResult = this.GetDataTable(cmd);
            return dtResult;
        }
        //取得請購單-單身
        public DataTable GetPURTBDataTable(string sCOMPANY)
        {
            string sSQL = @"
SELECT * FROM PURTB WHERE COMPANY=@COMPANY AND IsTransfer=0
";
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Parameters.Add("@COMPANY", SqlDbType.NVarChar).Value = sCOMPANY;
            cmd.Connection = con;
            DataTable dtResult = this.GetDataTable(cmd);
            return dtResult;
        }
        //取得廠商發送方式
        public string GetNotifyType(string sCOMPANY, string sVendorCode)
        {
            string sResult = "";
            string sSQL = @"
SELECT MA057 FROM ePURMA WHERE COMPANY=@COMPANY AND MA001=@MA001
";
            SqlConnection con = new SqlConnection(dllconfig.DBConnectionString);
            SqlCommand cmd = new SqlCommand(sSQL);
            cmd.Parameters.Add("@COMPANY", SqlDbType.NVarChar).Value = sCOMPANY;
            cmd.Parameters.Add("@MA001", SqlDbType.NVarChar).Value = sVendorCode;
            cmd.Connection = con;
            object oResult = this.ExecuteScalar(cmd);
            if (oResult != null)
                sResult = oResult.ToString();
            return sResult;
        }

        #endregion

        #region ExecuteScalar
        public object ExecuteScalar(SqlCommand cmd)
        {
            object oResult = null;
            bool open = false;
            SqlConnection OrginalConn = cmd.Connection;
            SqlConnection con = null;
            if (OrginalConn == null)
            {
                con = new SqlConnection(dllconfig.DBConnectionString);
                cmd.Connection = con;
            }
            else
            {
                con = cmd.Connection;
            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
                open = true;
            }

            try
            {
                oResult = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (open == true)
                    con.Close();
            }
            cmd.Connection = OrginalConn;
            return oResult;
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int iResult = -1;
            bool open = false;
            SqlConnection OrginalConn = cmd.Connection;
            SqlConnection con = null;
            if (OrginalConn == null)
            {
                con = new SqlConnection(dllconfig.DBConnectionString);
                cmd.Connection = con;
            }
            else
            {
                con = cmd.Connection;
            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
                open = true;
            }

            try
            {
                iResult = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (open == true)
                    con.Close();
            }
            cmd.Connection = OrginalConn;
            return iResult;
        }
        #endregion

        #region GetDataTable
        public DataTable GetDataTable(SqlCommand cmd)
        {
            bool open = false;
            SqlConnection OrginalConn = cmd.Connection;

            SqlConnection con = null;
            if (OrginalConn == null)
            {
                con = new SqlConnection(dllconfig.DBConnectionString);
                cmd.Connection = con;
            }
            else
            {
                con = cmd.Connection;
            }

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtResult = new DataTable();

            if (con.State != ConnectionState.Open)
            {
                con.Open();
                open = true;
            }

            try
            {
                adp.Fill(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (open == true)
                    con.Close();
            }

            cmd.Connection = OrginalConn;
            return dtResult;
        }
        public DataSet GetDataSet(SqlCommand cmd)
        {
            bool open = false;
            SqlConnection OrginalConn = cmd.Connection;

            SqlConnection con = null;
            if (OrginalConn == null)
            {
                con = new SqlConnection(dllconfig.DBConnectionString);
                cmd.Connection = con;
            }
            else
            {
                con = cmd.Connection;
            }

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet dsResult = new DataSet();

            if (con.State != ConnectionState.Open)
            {
                con.Open();
                open = true;
            }

            try
            {
                adp.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (open == true)
                    con.Close();
            }

            cmd.Connection = OrginalConn;
            return dsResult;
        }
        #endregion
    }

}