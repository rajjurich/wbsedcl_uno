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
    public partial class ESS_TA_MA_View : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        string manualattnddeletedate;
        string userid;
        public string strFrmdt;
        public string Rowid;

        public string strtodt;

        public string strMailOption;
        public string strMailServer;
        public string strMailUserName;
        public string strMailPassword;
        public int strMailPort;
        public string strEmpFromAddress;
        public string strEmpToAddress="";

        public string strdbFrmdt;
        public string strdbToDt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }

            if (!Page.IsPostBack)
            {

                bindDataGrid();
                ddlStatus.SelectedIndex = 0;
                FillReasonMA();
          
                gvManual.Columns[2].Visible = false;
            }

            manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
        }

   

        private void bindDataGrid()
        {
            try
            {

                string strsql = "select ESS_MA_RowID, ess_ma_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_ma_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_ma_Todt,103)= convert(VARCHAR(20),Ess_ma_fromdt,103) " +
                                " then convert(VARCHAR(20),Ess_ma_fromdt,103) else convert(VARCHAR(20),Ess_ma_Todt,103)  end as ToDate ,convert(char(5),ESS_MA_FROMTM,8) as [In],convert(char(5),ESS_MA_TOTM,8) as [Out], Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
                                " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_Ma_Status from ESS_TA_MA,ent_employee_personal_dtls where ess_ma_empid='" + userid + "' " +
                                "  and  ess_ta_ma.ess_ma_empid=ent_employee_personal_dtls.epd_empid  and ess_ma_isdeleted='0'  order by convert(datetime,Ess_MA_fromdt,103)  desc ";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvManual.DataSource = dt;
                gvManual.DataBind();
                DropDownList ddl = (DropDownList)gvManual.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvManual.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvManual.PageIndex + 1).ToString();
                Label lblcount = (Label)gvManual.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvManual.DataSource).Rows.Count.ToString() + " Records.";
                if (gvManual.PageCount == 0)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvManual.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvManual.PageIndex + 1 == gvManual.PageCount)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvManual.PageIndex == 0)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvManual.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManual.PageSize * gvManual.PageIndex) + 1) + " to " + (gvManual.PageSize * (gvManual.PageIndex + 1));

                gvManual.BottomPagerRow.Visible = true;


                if (dt.Rows.Count != 0)
                {
                    btnSearch.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }


        }
   

     
        protected void gvManual_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvManual.PageIndex = e.NewPageIndex;
            bindDataGrid();

        }


        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < gvManual.Rows.Count; i++)
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand objcmd = new SqlCommand();
                    objcmd.Connection = conn;
                    SqlTransaction trans;
                    trans = conn.BeginTransaction();
                    try
                    {
                        objcmd.Transaction = trans;
                        CheckBox delrows = (CheckBox)gvManual.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)gvManual.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }

                            objcmd.CommandText = "Update ESS_TA_MA set ESS_MA_isdeleted = '1', ESS_MA_DELETEDDATE = convert(datetime,'" + manualattnddeletedate + "',103) where ESS_MA_Rowid = '" + lblRowId.Text + "' ";

                            objcmd.ExecuteNonQuery();

                            trans.Commit();
                            Check = true;
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }

                }


                if (Check == true)
                {
                    lblMessages.Text = "Record Deleted Successfully";
                    lblMessages.Visible = true;
                    chkMailConfiguration();
                    if (strMailOption == "Y")
                    {
                        SendMailDelete();
                    }
                }
                else if (marked == false)
                {
                    lblMessages.Text = "Please select record to Delete";
                    lblMessages.Visible = true;
                }
                bindDataGrid();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_OD_View");
            }


        }


        public void chkMailConfiguration()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
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
                conn.Close();
            }

        }

        public void FindEmailID()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;


            objcmd.CommandText = "select epd_email from ENT_EMPLOYEE_PERSONAL_DTLS where epd_empid= '" + userid + "'";
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
            conn.Close();
        }


        public void FindMgrMailid()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objMgrcmd = new SqlCommand();
            objMgrcmd.Connection = conn;
            objMgrcmd.CommandText = "select ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMAIL from ENT_HierarchyDef,ENT_EMPLOYEE_PERSONAL_DTLS " +
                          " where ENT_HierarchyDef.Hier_Mgr_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID and ENT_HierarchyDef.Hier_Emp_ID='" + userid + "'";
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
            conn.Close();
        }

        public void SendMailEdit()
        {
            FindEmailID();
            FindMgrMailid();

            try
            {
                if (txtTDate.Text == "")
                {
                    txtTDate.Text = txtFrmDate.Text;
                }
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I had applied Manual Attendance from " + txtFrmDate.Text + " to " + txtTDate.Text + System.Environment.NewLine + "";
                message = message + "Reason: " + txt_Remarks.Text + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";

                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "Manual Attendance Application";
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
        public void SendMailDelete()
        {
            FindEmailID();
            FindMgrMailid();

            try
            {
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I would like to cancel Manual Attendance from " + strdbFrmdt + " to " + strdbToDt + "";
                message = message + "Kindly do the needful." + System.Environment.NewLine + "";

                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "Cancellation of Manual Attendance Application";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                lblMessages.Text = "";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strsql = "select ESS_MA_RowID, ess_ma_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_ma_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_ma_Todt,103)= convert(VARCHAR(20),Ess_ma_fromdt,103) " +
                                 " then convert(VARCHAR(20),Ess_ma_fromdt,103) else convert(VARCHAR(20),Ess_ma_Todt,103)  end as ToDate ,convert(char(5),ESS_MA_FROMTM,8) as [In],convert(char(5),ESS_MA_TOTM,8) as [Out], Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
                                 " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_Ma_Status from ESS_TA_MA,ent_employee_personal_dtls where ess_ma_empid='" + userid + "' " +
                                 "  and  ess_ta_ma.ess_ma_empid=ent_employee_personal_dtls.epd_empid  and ess_ma_isdeleted='0'   order by convert(datetime,Ess_MA_fromdt,103)  desc";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


                if (txtTodate.Text.ToString() == "" && txtFromDate.Text.ToString() == "")
                {
                    gvManual.DataSource = dt;
                    gvManual.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"fromdate~" +txtFromDate.Text.Trim(), "D" },
				{"Todate~" +txtTodate.Text.Trim(), "D" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvManual.DataSource = _tempDT;
                    gvManual.DataBind();
                }

                DropDownList ddl = (DropDownList)gvManual.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvManual.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvManual.PageIndex + 1).ToString();
                Label lblcount = (Label)gvManual.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvManual.DataSource).Rows.Count.ToString() + " Records.";
                if (gvManual.PageCount == 0)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvManual.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvManual.PageIndex + 1 == gvManual.PageCount)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvManual.PageIndex == 0)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvManual.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManual.PageSize * gvManual.PageIndex) + 1) + " to " + (gvManual.PageSize * (gvManual.PageIndex + 1));

                gvManual.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }
        }
       

        protected void gvManual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedit = (LinkButton)e.Row.FindControl("lnkEdit");
                CheckBox chkApprove = (CheckBox)e.Row.FindControl("DeleteRows");

                if ((e.Row.Cells[9].Text == "Approved") || (e.Row.Cells[9].Text == "Rejected"))
                //if (Convert.ToBoolean(e.Row.Cells[1].Text) == true)
                {
                    lnkedit.Enabled = false;
                    chkApprove.Enabled = false;
                }

            }
        }

        protected void gvManual_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                ViewState["RowId"] = e.CommandArgument.ToString();
                fillModifydata(e.CommandArgument.ToString());
                mpeAddCall.Show();
            }
        }
        private void FillReasonMA()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                                "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE'   and  Reason_Type = 'MA'";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReason.DataValueField = "Reason_ID";
                ddlReason.DataTextField = "Reason_Description";
                ddlReason.DataSource = dtReason;
                ddlReason.DataBind();
                ddlReason.Items.Insert(0, "Select One");

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
            }
        }
        private void fillModifydata(string Rowid)
        {

            string strsql = "select ess_ma_empid as EmpCode,convert(VARCHAR(20),Ess_ma_fromdt,103) as FromDate, Convert(VARCHAR(20),Ess_ma_Todt,103) AS " +
                                    " ToDate,convert(char(5),ESS_MA_FROMTM,8) as [In],convert(char(5),ESS_MA_TOTM,8) as [Out], Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
                                   " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_Ma_Status,ESS_MA_RSNID,ESS_MA_REMARK from ESS_TA_MA where " +
                                   "  ESS_MA_RowId = " + Rowid + "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            strFrmdt = DateTime.ParseExact(dt.Rows[0]["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            strtodt = DateTime.ParseExact(dt.Rows[0]["ToDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            txtFrmDate.Text = strFrmdt;
            txtTDate.Text = strtodt;
            frm_time.Text = dt.Rows[0]["In"].ToString();
            To_Time.Text = dt.Rows[0]["Out"].ToString();
            ddlReason.SelectedValue = dt.Rows[0]["ESS_MA_RSNID"].ToString();
            txt_Remarks.Text = dt.Rows[0]["ESS_MA_Remark"].ToString();

        }
        public void ModifyManualAttendance()
        {

            string strdbFrmdt;
            string strdbToDt;
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            if (txtTDate.Text == "")
            {
                txtTDate.Text = txtFrmDate.Text;
            }

            strdbFrmdt = txtFrmDate.Text;
            strdbToDt = txtTDate.Text;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            Rowid = Convert.ToString(ViewState["RowId"]);
          
            string str = "select top 1 1 from ESS_TA_MA where ESS_MA_EMPID='" + Session["uid"].ToString() + "'  " +
             " and  ESS_MA_STATUS in('A','N')  and isnull(ESS_MA_ISDELETED,0)=0 and  ESS_MA_RowId <>  " + Rowid + " and " +
                " ( ( convert(datetime,(convert(varchar(10),ESS_MA_FROMDT,103)),103) " +
                  " between convert(datetime,'" + strdbFrmdt + "',103)   and  convert(datetime,'" + strdbToDt + "',103)) " +
                    "or " +
                     " ( convert(datetime,(convert(varchar(10),ESS_MA_TODT,103)),103) " +
                  " between   convert(datetime,'" + strdbFrmdt + "',103)  and  convert(datetime,'" + strdbToDt + "',103)) )";

            SqlCommand cmd = new SqlCommand(str, conn);
            DataTable dtchk = new DataTable();
            SqlDataAdapter dachk = new SqlDataAdapter(cmd);
            
            if (dtchk != null)
            {
                if (dtchk.Rows.Count > 0)
                {
                    lblMessages.Text = "Manual Attendance already applied for the date range";
                    lblMessages.Visible = true;
                    mpeAddCall.Hide();
                    return;
                }
            }

            objcmd.CommandText = "select ISNULL(tday_status,'') as tday_status from TDAY where TDAY_DATE  between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbToDt + "',103)  AND tday_empcde= '" + Session["uid"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(objcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if ("OD" == dt.Rows[i]["tday_status"].ToString())
                {
                    lblMessages.Text = "Outdoor is already applied on this date you can't apply Manual Attendance.";
                    lblMessages.Visible = true;
                    mpeAddCall.Hide();
                    return;
                }

            }

            strdbFrmdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            strdbToDt = DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

        
         
            try
            {

                objcmd.CommandText = "Update ESS_TA_MA set ESS_MA_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103),ESS_MA_FROMTM=Convert(varchar(8),'" + frm_time.Text + "',103)," +
                                 " ESS_MA_TOTM=Convert(varchar(8),'" + To_Time.Text + "',103),ESS_MA_RSNID='" + ddlReason.SelectedValue + "'," +
                                 " ESS_MA_TODT=Convert(datetime,'" + strdbToDt + "',103),ESS_MA_Remark='" + txt_Remarks.Text + "'  " +
                                 " where ESS_MA_ISDELETED='0' And ESS_MA_RowID=" + Rowid + "";
                objcmd.ExecuteNonQuery();
                lblMessages.Text = "Record Updated Successfully";
                lblMessages.Visible = true;
                chkMailConfiguration();
                if (strMailOption == "Y")
                {
                    SendMailEdit();
                }
                bindDataGrid();
                mpeAddCall.Hide();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }



        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvManual.PageIndex = gvManual.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvManual.PageIndex = gvManual.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }
        }

        protected void btnSaveManualAtt_Click(object sender, EventArgs e)
        {
            ModifyManualAttendance();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvManual.PageIndex = Convert.ToInt32(((DropDownList)gvManual.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue == "AL")
            {
                ddlStatus.SelectedIndex = 0;
                bindDataGrid();
            }
            else
            {
                bindDataGrid(ddlStatus.SelectedValue);
            }
        }
        private void bindDataGrid(string status)
        {
            try
            {

                string strsql = "select ESS_MA_RowID, ess_ma_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_ma_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_ma_Todt,103)= convert(VARCHAR(20),Ess_ma_fromdt,103) " +
                                " then convert(VARCHAR(20),Ess_ma_fromdt,103) else convert(VARCHAR(20),Ess_ma_Todt,103)  end as ToDate ,convert(char(5),ESS_MA_FROMTM,8) as [In],convert(char(5),ESS_MA_TOTM,8) as [Out], Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
                                " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_Ma_Status from ESS_TA_MA,ent_employee_personal_dtls where ess_ma_empid='" + userid + "' " +
                                "  and ESS_MA_STATUS='" + status + "' and  ess_ta_ma.ess_ma_empid=ent_employee_personal_dtls.epd_empid  and ess_ma_isdeleted='0'  order by convert(datetime,Ess_MA_fromdt,103)  desc ";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvManual.DataSource = dt;
                gvManual.DataBind();
                DropDownList ddl = (DropDownList)gvManual.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvManual.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvManual.PageIndex + 1).ToString();
                Label lblcount = (Label)gvManual.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvManual.DataSource).Rows.Count.ToString() + " Records.";
                if (gvManual.PageCount == 0)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvManual.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvManual.PageIndex + 1 == gvManual.PageCount)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvManual.PageIndex == 0)
                {
                    ((Button)gvManual.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvManual.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManual.PageSize * gvManual.PageIndex) + 1) + " to " + (gvManual.PageSize * (gvManual.PageIndex + 1));

                gvManual.BottomPagerRow.Visible = true;


                if (dt.Rows.Count != 0)
                {
                    btnSearch.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }


        }
    }
}