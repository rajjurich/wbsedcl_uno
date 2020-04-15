/*
 Author Name:Shrinith Sanil
 * 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace UNO
{

    public class DataLog
    {

        public int DTL_ID
        {
            get;
            set;
        }

        public int DTL_LOG_INDEX
        {
            get;
            set;
        }

        public string DTL_MODULE
        {
            get;
            set;
        }

        public string DTL_PAGE
        {
            get;
            set;
        }

        public string DTL_OLDDATA
        {
            get;
            set;
        }

        public string DTL_NEWDATA
        {
            get;
            set;
        }

        public DateTime DTL_DATETIME
        {
            get;
            set;
        }

        public string DTL_OPERATION
        {
            get;
            set;
        }

        public string DTL_TABLENAME
        {
            get;
            set;
        }

        public string DTL_USERID
        {
            get;
            set;
        }

    
    }


    public class DataTansactionLog
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public static string connString = conn.ConnectionString;

        private static DataTable Execute(string Query, String con)
        {

            try
            {

                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "DataTansactionLog");
                return null;
            }

        }
        public static DataSet GetDataTransactionDetails(string url)
        {
            DataSet ds = new DataSet();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_GET_DataTransactionLog", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@URL", url);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            ds = new DataSet();
            da.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }
        public static void DataTransactionDetails(DataLog Log)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "USP_Save_DataTransactionLog";
                cmd.Parameters.AddWithValue("@DTL_LOG_INDEX", Log.DTL_LOG_INDEX);
                cmd.Parameters.AddWithValue("@DTL_MODULE", Log.DTL_MODULE);
                cmd.Parameters.AddWithValue("@DTL_PAGE", Log.DTL_PAGE);
                cmd.Parameters.AddWithValue("@DTL_OLDDATA", Log.DTL_OLDDATA);
                cmd.Parameters.AddWithValue("@DTL_NEWDATA", Log.DTL_NEWDATA);
                cmd.Parameters.AddWithValue("@DTL_OPERATION", Log.DTL_OPERATION);
                cmd.Parameters.AddWithValue("@DTL_TABLENAME", Log.DTL_TABLENAME);
                cmd.Parameters.AddWithValue("@DTL_USERID", Log.DTL_USERID);
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "DataTansactionLog");
            }

        }
        //Added by Pooja
        public static void InsertLog(string strNewData, string strOldData, string stroperation, string strtable, string strpath, string Maxvalue)
        {
            DataSet ds = null;
            ds = DataTansactionLog.GetDataTransactionDetails(strpath);
            DataLog Log = new DataLog();
            string olddata = string.Empty;

            if (stroperation.ToUpper() == "UPDATE")
                olddata = strOldData;
            if (Maxvalue == "")
                Log.DTL_LOG_INDEX = Convert.ToInt32(ds.Tables[1].Rows[0]["MAX"].ToString());
            else
                Log.DTL_LOG_INDEX = Convert.ToInt32(Maxvalue);

            Log.DTL_MODULE = ds.Tables[0].Rows[0]["Modules"].ToString();
            Log.DTL_OLDDATA = olddata;
            Log.DTL_NEWDATA = strNewData;
            Log.DTL_OPERATION = stroperation.ToUpper();
            Log.DTL_TABLENAME = strtable;
            Log.DTL_PAGE = strpath;
            Log.DTL_USERID = HttpContext.Current.Session["uid"].ToString();

            DataTansactionLog.DataTransactionDetails(Log);

        }

    }

}