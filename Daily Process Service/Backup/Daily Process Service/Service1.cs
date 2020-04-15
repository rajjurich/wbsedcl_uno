using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;


namespace Daily_Process_Service
{
    public partial class Service1 : ServiceBase
    {
        private string strLOGPath = "", strFrequencyTime = "",strShiftScheduleTime="", strConnection = "",strProcedureName="",strLeaveProcessTime="";
        int iShiftScheduleDay, iLastShiftScheduleMonth, iLeaveProcessMonth, iLastLeaveProcessYear, iLeaveProcessDay, iLastLeaveProcessMonth;
        bool isShiftSheduleCreated = false,isLeaveProcessed=false;

        string strAllowMail="", mStrMailServer = "", mStrMailPort = "", mStrUserName = "", mStrPassword = "",mStrFromMailID="",mStrToMailID="",mStrMailBody="";
        string strSSCTime = "";

        public Service1()
        {
            InitializeComponent();


            strConnection = ConfigurationSettings.AppSettings["connectionString"];
            strLOGPath = ConfigurationSettings.AppSettings["LogPath"];
            strFrequencyTime = ConfigurationSettings.AppSettings["DailyProcessInterval"];

            mStrMailServer = ConfigurationSettings.AppSettings["MailServer"];
            mStrMailPort = ConfigurationSettings.AppSettings["Port"];
            mStrUserName = ConfigurationSettings.AppSettings["UserName"];
            mStrPassword = ConfigurationSettings.AppSettings["Password"];

            mStrFromMailID = ConfigurationSettings.AppSettings["FromMailID"];
            mStrToMailID = ConfigurationSettings.AppSettings["ToMailID"];
        
            tmrService.Interval = Convert.ToDouble(strFrequencyTime);

            strAllowMail = ConfigurationSettings.AppSettings["AllowMail"];

            strSSCTime = ConfigurationSettings.AppSettings["ShiftScheduleTime"];
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
        public void WriteLog(string strData)
        {
            try
            {
                StreamWriter SW = new StreamWriter(strLOGPath + "\\" + "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".LOG", true);
                SW.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : " + strData);
                SW.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

       

        private void tmrService_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

           // System.Threading.Thread.Sleep(10000);

            WriteLog("Current Timing is [ "+DateTime.Now.Hour.ToString().PadLeft(2, '0')+" ]");

            try
            {
                WriteLog("Swipe Prcoess Timer Execution  Started.");


                if (strFrequencyTime!="")
                {
               
                    tmrService.Enabled = false;

                    /*Parameters*/
                    iShiftScheduleDay = GetShiftCreationDay();
                    iLeaveProcessMonth = GetLeaveProcessMonth();
                    iLastShiftScheduleMonth = GetLastShiftCreationMonth();
                    iLastLeaveProcessYear = GetLastLeaveProcessYear();


                    iLeaveProcessDay = GetLeaveProcessDay();
                    iLastLeaveProcessMonth = GetLastLeaveProcessMonth();
                    /*Parameters*/



                    /*Shift Schedule Creation*/
                    //if (DateTime.Now.Day == iShiftScheduleDay && ((iLastShiftScheduleMonth+1)==DateTime.Now.Month))
                    if ((DateTime.Now.Hour.ToString().PadLeft(2,'0') == strSSCTime))
                    {                        
                        ShiftScheduleCreation();
                    }                   
                    /*Shift Schedule Creation*/                  
                    

                    SqlConnection objConUpload = null;
                    SqlCommand objCmdUpload = null;
                    objConUpload = new SqlConnection(strConnection);
                    objConUpload.Open();
                    objCmdUpload = new SqlCommand();
                    objCmdUpload.Connection = objConUpload;
                    objCmdUpload.CommandType = CommandType.StoredProcedure;



                    //----------ACS-EVENTS TO TASC-------------------------
                    //Transfering Data From ACS_EVENTS to TASC
                    //FOR THIS MAKE EVENT_TRACE FIELD OF TABLE TASC AS PRIMARY KEY

                    WriteLog("Going to Start InsertinTasc.");
                    objCmdUpload.CommandText = "PROC_INSERT_TASC";
                    objCmdUpload.CommandTimeout = 0;
                    objCmdUpload.ExecuteNonQuery();
                    //----------ACS-EVENTS TO TASC-------------------------
                     WriteLog("InsertinTasc Executeted SuccessFully");


                    System.Threading.Thread.Sleep(180000);

                    //----------TASC TO TDAY-------------------------
                    WriteLog( "Going to Start PROC_DailyProcess.");
                    objCmdUpload.CommandText = "PROC_DailyProcess";
                    objCmdUpload.CommandTimeout = 0;
                    objCmdUpload.ExecuteNonQuery();
                    WriteLog("PROC_DailyProcess Executed SuccessFully");
                    //----------TASC TO TDAY-------------------------



                    objCmdUpload.Dispose();
                    objConUpload.Close();

                    tmrService.Enabled = true;
                   
                   
                }
                WriteLog("Swipe Prcoess Timer Execution Stoped.");
            }
            catch (Exception ex)
            {

                WriteLog(ex.ToString());
                tmrService.Enabled = true;
            }
        }

       


        private int  GetShiftCreationDay()
        {
            SqlConnection objCn = null;
            SqlCommand objCmd = null;

            try 
            {
               
                objCn = new SqlConnection(strConnection);
                objCn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objCn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = " SELECT VALUE FROM ENT_PARAMS  WHERE MODULE='TA'  AND IDENTIFIER='SSC' AND CODE='SSC' ";
                int iDay = Convert.ToInt32(objCmd.ExecuteScalar());
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                return iDay;
            }
            catch(Exception ex)
            {
                
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                WriteLog(ex.ToString());
                return 0;
                
            }
        }
        private int GetLeaveProcessMonth()
        {
            SqlConnection objCn = null;
            SqlCommand objCmd = null;

            try
            {

                objCn = new SqlConnection(strConnection);
                objCn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objCn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = " SELECT VALUE FROM ENT_PARAMS  WHERE MODULE='TA'  AND IDENTIFIER='LPRO' AND CODE='LvYrEnd' ";
                int iDay = Convert.ToInt32(objCmd.ExecuteScalar());
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                return iDay;
            }
            catch (Exception ex)
            {

                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                WriteLog(ex.ToString());
                return 0;

            }
        }
        private int  GetLastShiftCreationMonth()
        {
            SqlConnection objCn = null;
            SqlCommand objCmd = null;

            try 
            {
               
                objCn = new SqlConnection(strConnection);
                objCn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objCn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = " SELECT VALUE FROM ENT_PARAMS  WHERE MODULE='TA'  AND IDENTIFIER='SSC' AND CODE='LAST-MONTH' ";
                int iDay = Convert.ToInt32(objCmd.ExecuteScalar());

                if (iDay==12)
                {
                    iDay = 0;
                }
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                return iDay;
            }
            catch(Exception ex)
            {
                
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                WriteLog(ex.ToString());
                return 0;
                
            }
        }
        private int  GetLastLeaveProcessYear()
        {
            SqlConnection objCn = null;
            SqlCommand objCmd = null;

            try
            {

                objCn = new SqlConnection(strConnection);
                objCn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objCn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = " SELECT VALUE FROM ENT_PARAMS  WHERE MODULE='TA'  AND IDENTIFIER='LPRO' AND CODE='LAST-YEAR' ";
                int iDay = Convert.ToInt32(objCmd.ExecuteScalar());
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                return iDay;
            }
            catch (Exception ex)
            {

                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                WriteLog( ex.ToString());
                return 0;

            }
        }
        private int GetLeaveProcessDay()
        {
            SqlConnection objCn = null;
            SqlCommand objCmd = null;

            try
            {

                objCn = new SqlConnection(strConnection);
                objCn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objCn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = " SELECT VALUE FROM ENT_PARAMS  WHERE MODULE='TA'  AND IDENTIFIER='LPRO' AND CODE='LvMnEnd' ";
                int iDay = Convert.ToInt32(objCmd.ExecuteScalar());
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                return iDay;
            }
            catch (Exception ex)
            {

                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                WriteLog(ex.ToString());
                return 0;

            }
        }
        private int GetLastLeaveProcessMonth()
        {
            SqlConnection objCn = null;
            SqlCommand objCmd = null;

            try
            {

                objCn = new SqlConnection(strConnection);
                objCn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objCn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = " SELECT VALUE FROM ENT_PARAMS  WHERE MODULE='TA'  AND IDENTIFIER='LPRO' AND CODE='LAST-MONTH' ";
                int iDay = Convert.ToInt32(objCmd.ExecuteScalar());
                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                return iDay;
            }
            catch (Exception ex)
            {

                objCn.Close();
                objCn.Dispose();
                objCmd.Dispose();
                WriteLog(ex.ToString());
                return 0;

            }
        }
        private void LeaveProcessYearly(string strAllotmentType)
        {

            SqlTransaction ObjTrans = null;
            SqlConnection objConUpload = null;
            SqlCommand objCmdUpload = null;



            try 
            {

                    WriteLog("Leave  Process [" + strAllotmentType + "] Timer Started.");                

                    
                    objConUpload = new SqlConnection(strConnection);
                    objConUpload.Open();
                    objCmdUpload = new SqlCommand();
                    ObjTrans = objConUpload.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    objCmdUpload.Transaction = ObjTrans;
                    objCmdUpload.Connection = ObjTrans.Connection;
                    objCmdUpload.CommandType = CommandType.StoredProcedure;


                    objCmdUpload.CommandText = "LEAVE_PROCESS_CREATOR";
                    objCmdUpload.CommandTimeout = 0;


                    objCmdUpload.Parameters.AddWithValue("@PYear", DateTime.Now.Year);
                    objCmdUpload.Parameters.AddWithValue("@pMonth", DateTime.Now.Month);
                    objCmdUpload.Parameters.AddWithValue("@pEmployeeCodeLVP", System.DBNull.Value);
                    objCmdUpload.Parameters.AddWithValue("@pAllotmentType", strAllotmentType);
                

                    objCmdUpload.ExecuteNonQuery();
                   
                    string strUpdateparam="";
                   

                   if (strAllotmentType=="Y")
                   {

                        strUpdateparam += " UPDATE ENT_PARAMS ";
                        strUpdateparam += " SET VALUE= " + DateTime.Now.Year;
                        strUpdateparam += " WHERE MODULE='TA' AND IDENTIFIER='LPRO' AND CODE='LAST-YEAR'";
               }
                   else if (strAllotmentType == "M")
                   {
                       strUpdateparam += " UPDATE ENT_PARAMS ";
                       strUpdateparam += " SET VALUE= " + DateTime.Now.Month;
                       strUpdateparam += " WHERE MODULE='TA' AND IDENTIFIER='LPRO' AND CODE='LAST-MONTH'";
                   }

                    UpdateENT_Param(ObjTrans, strUpdateparam);
                    ObjTrans.Commit();

                   // iLastLeaveProcessYear = DateTime.Now.Year;

                    WriteLog("LEAVE PROCESSION OF ALL EMPLOYEE DONE SUCCESSFULLY.");             

                    objCmdUpload.Dispose();
                    objConUpload.Close();

                    WriteLog("Leave  Process [" + strAllotmentType + "] Timer Stopped.");  
            }
            catch(Exception ex)
            {
                ObjTrans.Rollback();
            }
        }
        private void ShiftScheduleCreation()
        {
            SqlConnection objConUpload = null;
            SqlCommand objCmdUpload = null;

            try
            {
                    WriteLog("Shift Schedule Timer Started");
                           
                    objConUpload = new SqlConnection(strConnection);
                    objConUpload.Open();                    

                    objCmdUpload = new SqlCommand();
                    objCmdUpload.Connection = objConUpload;                           
                    objCmdUpload.CommandType = CommandType.StoredProcedure;
                    objCmdUpload.CommandText = "ShiftScheduleCreator";
                    objCmdUpload.CommandTimeout = 0;
                    objCmdUpload.Parameters.AddWithValue("@pMonth", Convert.ToInt32(DateTime.Now.Month));
                    objCmdUpload.Parameters.AddWithValue("@pYear", Convert.ToInt32(DateTime.Now.Year));
                    objCmdUpload.Parameters.AddWithValue("@pEmployeeCode", System.DBNull.Value);
                    objCmdUpload.ExecuteNonQuery();

                    WriteLog("ShiftSchedule Created SuccessFully");           
                    objCmdUpload.Dispose();
                    objConUpload.Close();              
                           
                    WriteLog("Shift Schedule Timer Stopped.");

                    if (strAllowMail=="Y")
                    {
                        GetEmployee();
                    }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());                
            }
        }

        private void GetEmployee()
        {
            SqlConnection objConUpload = null;
            SqlCommand objCmdUpload = null;

            try
            {
                WriteLog("Start of GetEmployee()");

                string strQuery = "";
                objConUpload = new SqlConnection(strConnection);
                objConUpload.Open();

                objCmdUpload = new SqlCommand();
                objCmdUpload.Connection = objConUpload;         

                strQuery += "";
              
                strQuery += " SELECT EPD_EMPID,EPD_FIRST_NAME+' '+EPD_LAST_NAME AS NAME ";
                strQuery += " FROM dbo.ENT_EMPLOYEE_PERSONAL_DTLS ";
                strQuery += " 		INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_EMPID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID ";
                strQuery += " LEFT JOIN TNA_EMPLOYEE_TA_CONFIG ON TNA_EMPLOYEE_TA_CONFIG.ETC_EMP_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID ";
                strQuery += " WHERE EPD_ISDELETED=0 ";
                strQuery += " AND ETC_EMP_ID IS NULL ";


                objCmdUpload.CommandType = CommandType.Text;
                objCmdUpload.CommandText = strQuery;
                objCmdUpload.CommandTimeout = 0;
                DataTable dtEmployee = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(objCmdUpload);
                da.Fill(dtEmployee);

                if (dtEmployee.Rows.Count > 0)
                {

                    StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("Hello Sir, <br />" + " Below is List of Employee whose Shift Schedule is not Created.");

                    sb.Append("<table style='background-color: yellow;border:1'>");

                    sb.Append("<th style='border: 1px solid black'>");
                    sb.Append("Employee Code");
                    sb.Append("</th>");
                    sb.Append("<th style='border: 1px solid black'>");
                    sb.Append("Employee Name");
                    sb.Append("</th>");

                    for(int i=0;i<dtEmployee.Rows.Count;i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td  style='border: 1px solid black'>" + dtEmployee.Rows[i][0].ToString() + "</td>");
                        sb.Append("<td  style='border: 1px solid black'>" + dtEmployee.Rows[i][1].ToString() + "</td>");
                        sb.Append("</tr>");           
                    }
                    sb.Append("</table>");
                    sb.Append( " Please do Attendance Configuration of these Employees");
                    mStrMailBody = sb.ToString();
                }
                
                objCmdUpload.Dispose();
                objConUpload.Close();

                WriteLog("End of GetEmployee()");
                SendMail();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
     
        public void SendMail()
        {

            try
            {
                WriteLog("Before Mail Send.");
                System.Net.Mail.SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();           

                objMailMessage.From = new MailAddress(mStrFromMailID);
                objMailMessage.To.Add(mStrToMailID);                        

                objMailMessage.Subject = "About Attendance ShiftSchedule Creation";
                objMailMessage.Body = mStrMailBody;
                objMailMessage.Priority = MailPriority.High;

                objSMTPCLIENT.Port = Convert.ToInt32(mStrMailPort);
                objSMTPCLIENT.Host = mStrMailServer;
                objMailMessage.IsBodyHtml = true;
               
                if (mStrUserName != "")
                {
                    CredentialCache.DefaultNetworkCredentials.UserName = mStrUserName;
                    CredentialCache.DefaultNetworkCredentials.Password = mStrPassword;
                    objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;
                }    
              
                objSMTPCLIENT.Send(objMailMessage);
                WriteLog("Mail has been sent.");
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());            
            }
        }


        private void UpdateENT_Param(SqlTransaction objTrans,string strQuery)
        {
            SqlCommand objCmd = new SqlCommand();
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objCmd.CommandText = strQuery;
            objCmd.CommandType = CommandType.Text;
            objCmd.ExecuteNonQuery();
        }


    }
}

//LAST MODIFIED         REASON
//18-NOV-2014 18:30     Added Functionality of Leave Month/Year
//03-Feb-2015 18:30     Now ShiftSchedule Creation Will Run Every day at 00:00
