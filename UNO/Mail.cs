using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace UNO
{
    public static class Mail
    {

        static string strMailOption;
        static string strMailServer;
        static string strMailUserName;
        static string strMailPassword;
        static int strMailPort;

        public static string con = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        public static void chkMailConfiguration()
        {

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            String strSql = "select Identifier,value FROM ENT_PARAMS WHERE  MODULE='ESS'";
            SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["Identifier"].ToString() == "WITHMAIL")
                {
                    strMailOption = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVER")
                {
                    strMailServer = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERUSERNAME")
                {
                    strMailUserName = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERPASSWORD")
                {
                    strMailPassword = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILPORT")
                {
                    strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);
                }
                //strMailServer=dt.Rows[i]["Value"].ToString();
                //strMailUserName=dt.Rows[i]["Value"].ToString();
                //strMailPassword=dt.Rows[i]["Value"].ToString();
                // strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);

            }

        }

        public static void SendMail(string from, string to, string CC, string subject, string body)
        {
            try
            {
                chkMailConfiguration();
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();

                objMailMessage.From = new MailAddress(from);

                string s = to;
                string[] values = s.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    objMailMessage.To.Add(values[i].Trim());
                }

                //objMailMessage.To.Add(to.Trim());

                // objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
                if (CC.Trim() != "")
                    objMailMessage.CC.Add(CC.Trim());

                objMailMessage.Subject = subject;
                objMailMessage.Body = body.Trim();
                objMailMessage.Priority = MailPriority.High;

                objSMTPCLIENT.Port = strMailPort;
                objSMTPCLIENT.Host = strMailServer;

                objSMTPCLIENT.EnableSsl = ConfigurationManager.AppSettings["sslEnable"] == null || ConfigurationManager.AppSettings["sslEnable"].ToString() == "" ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["sslEnable"]);

                objMailMessage.IsBodyHtml = true;

                if (strMailUserName != "")
                {
                    CredentialCache.DefaultNetworkCredentials.UserName = strMailUserName;
                    CredentialCache.DefaultNetworkCredentials.Password = strMailPassword;

                    objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;

                }
                objSMTPCLIENT.Send(objMailMessage);


            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Mail");
            }
        }
        public static void SendMail(string To, string Password)
        {
            try
            {
                string strMailOption = "";
                string strMailServer = "";
                string strMailUserName = "";
                string strMailPassword = "";
                int strMailPort = 0;
                String strSql = "select Identifier,value FROM ENT_PARAMS WHERE  MODULE='ESS'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["Identifier"].ToString() == "WITHMAIL")
                    {
                        strMailOption = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVER")
                    {
                        strMailServer = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERUSERNAME")
                    {
                        strMailUserName = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERPASSWORD")
                    {
                        strMailPassword = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILPORT")
                    {
                        strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);
                    }
                }
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "Your Password is : " + Password;
                objMailMessage.From = new MailAddress("uno@cms.co.in");
                objMailMessage.To.Add(To.Trim());

                objMailMessage.Subject = "Your UNO-Tempus password";
                objMailMessage.Body = message.Trim();
                objMailMessage.Priority = MailPriority.High;

                objSMTPCLIENT.Port = strMailPort;
                objSMTPCLIENT.Host = strMailServer;

                if (strMailUserName != "")
                {
                    CredentialCache.DefaultNetworkCredentials.UserName = strMailUserName;
                    CredentialCache.DefaultNetworkCredentials.Password = strMailPassword;

                    objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;

                }
                objSMTPCLIENT.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Mail");
            }

        }



    }
}