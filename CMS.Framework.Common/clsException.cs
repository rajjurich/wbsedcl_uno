using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;
using System.Threading;


namespace CMS.Framework.Common
{
    public static class clsException
    {
        public static void UNO_DBErrorLog(string error_Description, string error_trace, string error_module)
        {
          
            Thread t = new Thread(() =>
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[clsUtility.ConnectionStringName()].ConnectionString.ToString());
                try
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand objcmd = new SqlCommand();
                    objcmd.Parameters.AddWithValue("@errordescription", error_Description);
                    objcmd.Parameters.AddWithValue("@errorTrace", error_trace);
                    objcmd.Parameters.AddWithValue("@errorModule", error_module);
                    objcmd.Connection = conn;
                    objcmd.CommandText = "Insert into UNO_ErrorLog(Error_Description,Error_Trace,Error_Module) Values(@errordescription,@errorTrace,@errorModule)";                    
                    objcmd.ExecuteNonQuery();
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
                    WriteLog("logOccuredWhileInsertioninTable", ex.Message, error_Description, error_trace, error_module);

                }
            }
                 );
            t.Start();

        }

        public static void WriteLog(string strFileName, string strMessage, string error_Description, string error_trace, string error_module)
        {
            try
            {
                //string path = HttpContext.Current.Server.MapPath("~\\UNO_Log\\");

                System.IO.StreamWriter fs = new System.IO.StreamWriter("C:\\UNOESSLOG\\ " + DateTime.Now.ToString("ddMMyyyy") + ".LOG", true);


                //System.IO.StreamWriter fs = new System.IO.StreamWriter(path + DateTime.Now.ToString("ddMMyyyy") + ".LOG", true);

                //System.IO.StreamWriter fs = new System.IO.StreamWriter(strLogPath + "\\ExecutionLog\\" + strFileName + DateTime.Now.ToString("ddMMyyyy") + ".LOG", true);
                fs.WriteLine("--------------------------------------------------------------------------------");
                fs.WriteLine(strMessage + "\t " + DateTime.Now);
                fs.WriteLine("--------------------------------------------------------------------------------");

                fs.WriteLine("-------Original ErrorDescription-----------------------------------------------------");
                fs.WriteLine(error_Description + "\t " + DateTime.Now);
                fs.WriteLine("--------------------------------------------------------------------------------");

                fs.WriteLine("-------Original StackTrace---------------------------------------------------------------------");
                fs.WriteLine(error_trace + "\t " + DateTime.Now);
                fs.WriteLine("--------------------------------------------------------------------------------");

                fs.WriteLine("-------Original ErrorModule------------------------------------------------");
                fs.WriteLine(error_module + "\t " + DateTime.Now);
                fs.WriteLine("--------------------------------------------------------------------------------");

                fs.Close();
            }
            catch (Exception ex)
            {
            }
        }


    }
}
