using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;

using System.Net;
using System.Net.Mail;
namespace UNO
{
    public partial class ESS_TA_OD_Edit : System.Web.UI.Page
    {


       public string strIdEmp;
       public string strFrmdt;
       public string Rowid;
       public string strtodt;


       public string strMailOption;
       public string strMailServer;
       public string strMailUserName;
       public string strMailPassword;
       public int strMailPort;
       public string strEmpFromAddress;
       public string strEmpToAddress;

       public string strdbFrmdt;
       public string strdbToDt;
      
    
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Literal Ltl = (Literal)Page.Master.FindControl("PopupVis");


            if (!Page.IsPostBack)
            {


                if (Session["EmpID"] != null)

                    strIdEmp = Session["EmpID"].ToString();

                  if (Session["RquestDT"] != null)

                    strFrmdt = Session["RquestDT"].ToString();

               

                Rowid = Convert.ToString(Request.QueryString["id"]);
             
          


              
                //fillList();
                FillReason();
                FillODTypes();


                fillModifydata(strIdEmp);

               // Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");
            }
           //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");

        }


        public void fillModifydata(string strIdEmp)
        {
            //string strsql = "select ma_recid,ma_empid, (Epd_first_name + ' ' + epd_last_name)as name ,convert(varchar(5),ma_swipetime,114) as ma_swipetime,Convert(datetime,ma_swipedate,103) as ma_swipedate, ma_mode,ma_processflag,ma_reasonid,ma_userid from TA_Manual_att,ENT_EMPLOYEE_PERSONAL_DTLS where ma_isdeleted='0' and ta_manual_att.ma_empid=ENT_EMPLOYEE_PERSONAL_DTLS.epd_empid and ta_manual_att.ma_recid='" + strIdEmp +  "'";

            string strsql = "select ess_OD_Empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, Convert(VARCHAR(20),Ess_OD_Todt,103) " +
                            " AS ToDate,Ess_OD_ODcd as ODCode, ESS_OD_RSNID, Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval'" +
                             " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status, " +
                             " ESS_OD_REMARK from ESS_TA_OD where ESS_OD_RowId =  " + Rowid + "";
                      
           //") M ";
            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);


          
                // ddlHolidayType.Enabled = false;
                //   txtHolidayid.ReadOnly = true;
                 //txtCalendarFrom.Text  = Convert.ToDateTime(dt.Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                 //txtCalendarTo.Text = Convert.ToDateTime(dt.Rows[0]["ToDate"]).ToString("dd/MM/yyyy");


            strFrmdt = DateTime.ParseExact(dt.Rows[0]["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            strtodt = DateTime.ParseExact(dt.Rows[0]["ToDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");


            txtCalendarFrom.Text = strFrmdt;

            txtCalendarTo.Text = strtodt;



                 ddlODType.SelectedValue = dt.Rows[0]["ODCode"].ToString();
              
                //ddlModeType.SelectedValue = dt.Rows[0]["ma_mode"].ToString();
                 ddlReasonType.SelectedValue = dt.Rows[0]["ESS_OD_RSNID"].ToString();

                if (txtCalendarTo.Text != "")
                {
                    rbtDate.SelectedValue = "1";
                }




                txt_Remarks.Text = dt.Rows[0]["ESS_OD_REMARK"].ToString();
         


            //txtRequestDate.Enabled = false;
            //txtEmployeeCode.Enabled = false;
            //txtEmployeeName.Enabled = false;
            //txtSanctnedCode.Enabled = false;

            


          

            //holidate = Convert.ToDateTime(dt.Rows[0][3]).ToString("dd/MM/yyyy");
            //txtholidaydate.Value = holidate;

            //holiswap = Convert.ToDateTime(dt.Rows[0][4]).ToString("dd/MM/yyyy");

        }


        private void FillODTypes()
        {
            string strSql = "select CODE,VALUE from ENT_PARAMS where MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' AND CODE='OD' ";
            SqlDataAdapter daLeave = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtLeave = new DataTable();
            daLeave.Fill(dtLeave);
            ddlODType.DataValueField = "CODE";
            ddlODType.DataTextField = "VALUE";
            ddlODType.DataSource = dtLeave;
            ddlODType.DataBind();
            ddlODType.Items.Insert(0, "Select One");

         

        }


        private void FillReason()
        {
            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                           " and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'OD' ";
            SqlDataAdapter daReason = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtReason = new DataTable();
            daReason.Fill(dtReason);
            ddlReasonType.DataValueField = "Reason_ID";
            ddlReasonType.DataTextField = "Reason_Description";
            ddlReasonType.DataSource = dtReason;
            ddlReasonType.DataBind();
            ddlReasonType.Items.Insert(0, "Select One");

        }


      
        protected void Btnclear_Click(object sender, EventArgs e)
        {
            strIdEmp = Convert.ToString(Request.QueryString["id"]);
            fillModifydata(strIdEmp);
            this.messageDiv.InnerHtml = "";
            
        }



        protected void lstEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtEmployeeCode.Text = lstEmp.SelectedItem.ToString().Substring(lstEmp.SelectedItem.ToString().Length - 10);
            //txtEmployeeName.Text = lstEmp.SelectedItem.ToString().Substring(0, lstEmp.SelectedItem.ToString().Length - 10);
            //fillSanctionedby();
            //fillAbsentStatus();
            ////bindDataGrid();
        }

        protected void gvStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvStatus.PageIndex = e.NewPageIndex;
            //fillAbsentStatus();
        }

        protected void txtSanctndCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lstSanctioned_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtSanctndCode.Text = lstSanctioned.SelectedItem.ToString().Substring(lstSanctioned.SelectedItem.ToString().Length - 10);
            //txtSanctiondName.Text = lstSanctioned.SelectedItem.ToString().Substring(0, lstSanctioned.SelectedItem.ToString().Length - 10);

        }

          public void chkMailConfiguration()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
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

        public void FindEmailID()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;


            objcmd.CommandText = "select epd_email from ENT_EMPLOYEE_PERSONAL_DTLS where epd_empid= '" + strIdEmp + "'";
            //int rows = Convert.ToInt16(objcmd.ExecuteScalar());
            SqlDataReader dr = objcmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["epd_email"] != null)
                {
                    //string usname = dr["UserID'].ToString();
                    strEmpFromAddress = dr["epd_email"].ToString();
                }

            }
            objcmd.Dispose();
        }


        public void FindMgrMailid()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();
            SqlCommand objMgrcmd = new SqlCommand();
            objMgrcmd.Connection = conn;
            objMgrcmd.CommandText = "select ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMAIL from ENT_HierarchyDef,ENT_EMPLOYEE_PERSONAL_DTLS " +
                          " where ENT_HierarchyDef.Hier_Mgr_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID and ENT_HierarchyDef.Hier_Emp_ID='" + strIdEmp + "'";
            //int rows = Convert.ToInt16(objcmd.ExecuteScalar());
            SqlDataReader drmgr = objMgrcmd.ExecuteReader();
            if (drmgr.Read())
            {
                if (drmgr["epd_email"] != null)
                {
                    //string usname = dr["UserID'].ToString();
                    strEmpToAddress = drmgr["epd_email"].ToString();
                }

            }

        }

        public void SendMail()
        {
            FindEmailID();
            FindMgrMailid();

            try
            {
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I had taken OD from " + strdbFrmdt + " to " + strdbToDt + System.Environment.NewLine + "";
                message = message + "Reason: " + txt_Remarks.Text + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "OD Application";
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

            }
        }



        public void ModifyODApplication()
        {

       

            if (txtCalendarTo.Text == "")
            {
                txtCalendarFrom.Text = txtCalendarTo.Text;
            }

            strdbFrmdt = txtCalendarFrom.Text;
            strdbToDt = txtCalendarTo.Text;



            strdbFrmdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            strdbToDt = DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");


            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;

            Rowid = Convert.ToString(Request.QueryString["id"]);

            try
            {



                objcmd.CommandText = "Select ESS_od_empid from ESS_TA_od Where ESS_od_FROMDT between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbToDt + "',103)  AND ESS_OD_EMPID= '" +  Session["EmpID"].ToString() + "' AND   ESS_OD_RowId <>  " + Rowid + "";
                //int rows = Convert.ToInt16(objcmd.ExecuteScalar());
                String strduplicateLeavedt = String.Empty;
                strduplicateLeavedt = (String)objcmd.ExecuteScalar();
                if (strduplicateLeavedt != null)
                {

                    this.messageDiv.InnerHtml = "OD already applied for the date range";
                    ScriptManager.RegisterStartupScript(
             UpdatePanel,
             this.GetType(),
             "MyAction",
             "MTimer();",
             true);

                    //string someScript = "";
                    //someScript = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                    //Button1.Attributes.Add("OnClick", "PopWindow()");
                    return;
                }


                objcmd.CommandText = "Update ESS_TA_OD set ESS_OD_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103),ESS_OD_ODcd='" + ddlODType.SelectedValue + "'," +
                                 " ESS_OD_RSNID='" + ddlReasonType.SelectedValue + "'," +
                                 " ESS_OD_TODT=Convert(datetime,'" + strdbToDt + "',103),ESS_OD_Remark='" + txt_Remarks.Text + "'  " +
                                 " where ESS_OD_ISDELETED='0' And ESS_OD_RowID=" + Rowid + "";



                objcmd.ExecuteNonQuery();

                this.messageDiv.InnerHtml = "Record Saved Successfully";
                conn.Close();

                chkMailConfiguration();
                if (strMailOption == "Y")
                {
                    SendMail();
                }


                Session.Remove("EmpID");
                Session.Remove("RquestDT");

                string someScript3 = "";
                someScript3 = "<script language='javascript'>setTimeout(\"clearMessageDiv('messageDiv')\",2000);</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript3);

            }
            catch (Exception ex)
            {
                this.messageDiv.InnerHtml = ex.Message;
            }



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCalendarTo.Text == "")
            {
               txtCalendarTo.Text = txtCalendarFrom.Text;
            }



            ModifyODApplication();
     
        }

      

       



       
          

        }

        
       
    }
