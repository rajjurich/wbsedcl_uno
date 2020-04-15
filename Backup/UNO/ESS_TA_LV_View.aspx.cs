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
    public partial class ESS_TA_LV_View : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        string manualattnddeletedate;
        string userid;


        public string strMailOption;
        public string strMailServer;
        public string strMailUserName;
        public string strMailPassword;
        public int strMailPort;
        public string strEmpFromAddress;
        public string strEmpToAddress = "";

        public string strdbFrmdt;
        public string strdbToDt;
        public string strFrmdt;
        public string Rowid;
        string strToDt;
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
                FillLeaveTypes();
                FillReasonLV();
            }
            manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
        }



        protected void bindDataGrid()
        {
            try
            {

                string strsql = "select ESS_LA_RowID,ess_LA_empid as EmpCode, ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name   ,convert(VARCHAR(20),Ess_LA_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_LA_Todt,103)= convert(VARCHAR(20),Ess_LA_fromdt,103) " +
                           " THEN convert(VARCHAR(20),Ess_LA_fromdt,103) ELSE CONVERT(VARCHAR(20),ESS_LA_TODT,103)  END AS TODATE, ESS_LA_LVCD , Case  ESS_LA_Status WHEN 'N' then 'Pending For Approval' " +
                          " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_LA_Status from ESS_TA_LA,ent_employee_personal_dtls " +
                          " where  ess_ta_la.ess_LA_empid=ent_employee_personal_dtls.epd_empid " +
                            "and ess_la_empid= '" + userid + "' And ess_la_isdeleted='0' order by convert(datetime,Ess_LA_fromdt,103)  desc";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLeave.DataSource = dt;
                gvLeave.DataBind();
                DropDownList ddl = (DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeave.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeave.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeave.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeave.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeave.PageCount == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex + 1 == gvLeave.PageCount)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeave.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeave.PageSize * gvLeave.PageIndex) + 1) + " to " + (gvLeave.PageSize * (gvLeave.PageIndex + 1));

                gvLeave.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }

        }
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            gvLeave.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            bindDataGrid();
        }

        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < gvLeave.Rows.Count; i++)
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
                        CheckBox delrows = (CheckBox)gvLeave.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)gvLeave.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }

                            objcmd.CommandText = "Update ESS_TA_LA set ESS_LA_ISDELETED = '1', ESS_LA_DELETEDDATE = convert(datetime,'" + manualattnddeletedate + "',103) where ESS_LA_Rowid = '" + lblRowId.Text + "' ";

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
                       // SendMailDelete();
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
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
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
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
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

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
                string message = "I had taken " + ddleaveType.SelectedValue + " leave from " + txtFrmDate.Text + " to " + txtTDate.Text + System.Environment.NewLine + "";
                message = message + "Reason: " + txtRemark.Text + System.Environment.NewLine + "";
                message = message + "I would request you to kindly approve the same." + System.Environment.NewLine + "";

                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";
                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                // objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "Leave Application";
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
        //public void SendMailDelete()
        //{
        //    FindEmailID();
        //    FindMgrMailid();

        //    try
        //    {
        //        SmtpClient objSMTPCLIENT = new SmtpClient();
        //        MailMessage objMailMessage = new MailMessage();
        //        string message = "I would like to cancel Leave from " + strdbFrmdt + " to " + strdbToDt + "";
        //        message = message + "Kindly do the needful." + System.Environment.NewLine + "";

        //        message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
        //        message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
        //        message = message + Session["loginName"].ToString() + "";

        //        objMailMessage.From = new MailAddress(strEmpFromAddress);
        //        objMailMessage.To.Add(strEmpToAddress.Trim());
        //        // p_Mailobj.CC.Add(_mailCCAddress.Trim());
        //        objMailMessage.Subject = "Cancellation of Leave Application";
        //        objMailMessage.Body = message.Trim();
        //        objMailMessage.Priority = MailPriority.High;

        //        objSMTPCLIENT.Port = strMailPort;
        //        objSMTPCLIENT.Host = strMailServer;

        //        if (strMailUserName != "")
        //        {
        //            CredentialCache.DefaultNetworkCredentials.UserName = strMailUserName;
        //            CredentialCache.DefaultNetworkCredentials.Password = strMailPassword;

        //            objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;

        //        }
        //        objSMTPCLIENT.Send(objMailMessage);


        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = "";
                string strsql = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strsql1 = "select ESS_LA_RowID,ess_LA_empid as EmpCode, ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name   ,convert(VARCHAR(20),Ess_LA_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_LA_Todt,103)= convert(VARCHAR(20),Ess_LA_fromdt,103) " +
                          " THEN convert(VARCHAR(20),Ess_LA_fromdt,103) ELSE CONVERT(VARCHAR(20),ESS_LA_TODT,103)  END AS TODATE, ESS_LA_LVCD , Case  ESS_LA_Status WHEN 'N' then 'Pending For Approval' " +
                         " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_LA_Status from ESS_TA_LA,ent_employee_personal_dtls " +
                         " where  ess_ta_la.ess_LA_empid=ent_employee_personal_dtls.epd_empid " +
                           "and ess_la_empid= '" + userid + "' And ess_la_isdeleted='0'  order by convert(datetime,Ess_LA_fromdt,103)  desc ";
                SqlDataAdapter da = new SqlDataAdapter(strsql1, conn);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


                if (txtTodate.Text.ToString() == "" && txtFromDate.Text.ToString() == "")
                {
                    gvLeave.DataSource = dt;
                    gvLeave.DataBind();

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
                    gvLeave.DataSource = _tempDT;
                    gvLeave.DataBind();
                }

                DropDownList ddl = (DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeave.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeave.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeave.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeave.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeave.PageCount == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex + 1 == gvLeave.PageCount)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeave.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeave.PageSize * gvLeave.PageIndex) + 1) + " to " + (gvLeave.PageSize * (gvLeave.PageIndex + 1));

                gvLeave.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }

        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvLeave.PageIndex = gvLeave.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvLeave.PageIndex = gvLeave.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }
        }

        protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                ViewState["RowId"] = e.CommandArgument.ToString();
                fillModifydata(e.CommandArgument.ToString());
                lblPnlMesg.Text = "";
                lblMessages.Text = "";
                mpeAddCall.Show();
            }

        }
        public void fillModifydata(string rowid)
        {
            try
            {

                string strsql = "select ess_La_Empid as EmpCode,ess_la_lvdays,convert(VARCHAR(20),Ess_La_fromdt,103) as FromDate, Convert(VARCHAR(20),Ess_La_Todt,103) " +
                                " AS ToDate,Ess_la_lvcd as LeaveCode, ESS_lA_RSNID, Case  ESS_La_Status WHEN 'N' then 'Pending For Approval'" +
                                 " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_la_Status, " +
                                 " ESS_lA_REMARK from ESS_TA_lA where ESS_LA_RowId =  " + rowid + "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                strFrmdt = DateTime.ParseExact(dt.Rows[0]["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                strToDt = DateTime.ParseExact(dt.Rows[0]["ToDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

                ViewState["strToDt"] = strToDt;
                txtFrmDate.Text = strFrmdt;

                txtTDate.Text = strToDt;

                ddleaveType.SelectedValue = dt.Rows[0]["LeaveCode"].ToString();


                ddlReasonTypeLVReq.SelectedValue = dt.Rows[0]["ESS_lA_RSNID"].ToString();

                string lvdays = dt.Rows[0]["ess_la_lvdays"].ToString();
                if (lvdays == "0.50")
                {
                    rbtLeaveType1.SelectedValue = "H";
                }
                else
                {
                    rbtLeaveType1.SelectedValue = "F";
                }
                txtRemark.Text = dt.Rows[0]["ESS_lA_REMARK"].ToString();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }



        }
        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkedit = (LinkButton)e.Row.FindControl("lnkEdit");
                    CheckBox chkApprove = (CheckBox)e.Row.FindControl("DeleteRows");

                    if ((e.Row.Cells[8].Text == "Approved") || (e.Row.Cells[8].Text == "Rejected"))
                    //if (Convert.ToBoolean(e.Row.Cells[1].Text) == true)
                    {
                        lnkedit.Enabled = false;
                        chkApprove.Enabled = false;
                    }

                    e.Row.Cells[2].Visible = false;


                }

                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[2].Visible = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }


        }

        private void FillLeaveTypes()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select Leave_ID,Leave_Description  from TA_Leave_File where  Leave_ISDELETED = 'FALSE'";
                SqlDataAdapter daType = new SqlDataAdapter(strSql, conn);
                DataTable dtType = new DataTable();
                daType.Fill(dtType);
                ddleaveType.DataValueField = "Leave_ID";
                ddleaveType.DataTextField = "Leave_Description";
                ddleaveType.DataSource = dtType;
                ddleaveType.DataBind();
                ddleaveType.Items.Insert(0, "Select One");
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
        private void FillReasonLV()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                                "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'LA' ";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReasonTypeLVReq.DataValueField = "Reason_ID";
                ddlReasonTypeLVReq.DataTextField = "Reason_Description";
                ddlReasonTypeLVReq.DataSource = dtReason;
                ddlReasonTypeLVReq.DataBind();
                ddlReasonTypeLVReq.Items.Insert(0, "Select One");
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

        protected void btnCancelLvReq_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();

        }
        public void ModifyLeaveApplication()
        {
            if (txtTDate.Text == "")
            {
                txtTDate.Text = txtFrmDate.Text;
            }

            strdbFrmdt = txtFrmDate.Text;
            strdbToDt = txtTDate.Text;



            strdbFrmdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            strdbToDt = DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            DateTime dtfromdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null);

            DateTime dtttodt = DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null);

            TimeSpan difference = dtttodt - dtfromdt;
            float LvDays = difference.Days + 1;

            if (rbtLeaveType1.SelectedValue == "H")
            {
                strdbToDt = strdbFrmdt;
                txtFrmDate.Text = txtFrmDate.Text;
                LvDays = 0.50f;
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;



            try
            {

                if (ddleaveType.SelectedValue.ToString() != "WP")
                {
                    objcmd.CommandText = "Select count(*) from ta_leave_summary where Lv_Emp_Id='" + Session["uid"].ToString() + "' and Lv_Leave_ID='" + ddleaveType.SelectedValue + "' and Lv_Available > " + LvDays + " ";
                    int LeaveBal;
                    LeaveBal = Convert.ToInt32(objcmd.ExecuteScalar());
                    if (Convert.ToInt32(LeaveBal) < 1)
                    {
                        lblMessages.Text = "Insufficient Leave Balance";
                        lblMessages.Visible = true;
                        mpeAddCall.Hide();
                        return;
                    }
                }


                objcmd.CommandText = "Select ESS_LA_EMPID FROM ESS_TA_LA Where ESS_LA_EMPID = '" + userid + "' and  ESS_LA_ROWID <> " + ViewState["RowId"] + " and ess_la_isdeleted='0' " +
                                      " And ((ESS_LA_FROMDT <= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                       " And ESS_LA_TODT >= Convert(DateTime,'" + strdbFrmdt + "',103)) " +
                                      " Or (ESS_LA_FROMDT <= Convert(DateTime,'" + strdbToDt + "',103) " +
                                       " And ESS_LA_TODT >= Convert(DateTime,'" + strdbToDt + "',103)) " +
                                       " Or (ESS_LA_FROMDT >= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                       " And ESS_LA_FROMDT <= Convert(DateTime,'" + strdbToDt + "',103)) " +
                                     " Or (ESS_LA_TODT >= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                     " And ESS_LA_TODT <= Convert(DateTime,'" + strdbToDt + "',103))) ";


                String strduplicateLeavedt = String.Empty;
                strduplicateLeavedt = (String)objcmd.ExecuteScalar();
                if (strduplicateLeavedt != null)
                {
                    lblMessages.Text = "Leave already applied for the date range";
                    lblMessages.Visible = true;
                    mpeAddCall.Hide();
                    return;
                }
                objcmd.CommandText = "select ISNULL(tday_status,'') as tday_status from TDAY where TDAY_DATE  between convert(datetime,'" + txtFrmDate.Text + "',103) and Convert(datetime,'" + txtTDate.Text + "',103)  AND tday_empcde= '" + Session["uid"].ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(objcmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                String strtdaystatus = "PR";

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (strtdaystatus == dt.Rows[i]["tday_status"].ToString())
                    {
                        lblMessages.Text = "Cannot apply for Leave as Employee is already present for the day";
                        lblMessages.Visible = true;
                        mpeAddCall.Hide();
                        return;
                    }
                    if (txtFrmDate.Text == txtTDate.Text)
                    {
                        if (dt.Rows[i]["tday_status"].ToString() == "ABW2" || dt.Rows[i]["tday_status"].ToString() == "ABWO")
                        {
                            lblMessages.Text = "Cannot apply Leave on weekly off";
                            lblMessages.Visible = true;
                            mpeAddCall.Hide();
                            return;
                        }
                        else if (dt.Rows[i]["tday_status"].ToString() == "ABHO")
                        {
                            lblMessages.Text = "Cannot apply Leave on holiday";
                            lblMessages.Visible = true;
                            mpeAddCall.Hide();
                            return;
                        }
                    }
                }
                SqlCommand cmdLvRule = new SqlCommand("PROC_LEAVE_RULE_VALIDATION", conn);
                cmdLvRule.CommandType = CommandType.StoredProcedure;
                cmdLvRule.Parameters.AddWithValue("@pEmpCode", userid);
                cmdLvRule.Parameters.AddWithValue("@pLeaveCode", ddleaveType.SelectedValue);
                cmdLvRule.Parameters.AddWithValue("@pLeaveFromDate", DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null));
                cmdLvRule.Parameters.AddWithValue("@pLeaveToDate", DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null));

                cmdLvRule.Parameters.AddWithValue("@pMinDaysLeave", 0).Direction = ParameterDirection.Output;

                cmdLvRule.Parameters.AddWithValue("@pMaxDaysLeave", 0).Direction = ParameterDirection.Output;
                SqlDataAdapter daLvRule = new SqlDataAdapter(cmdLvRule);
                DataTable dtLvRule = new DataTable();
                daLvRule.Fill(dtLvRule);

                string updatedfromDate = dtLvRule.Rows[0]["pleaveDate"].ToString();
                string updatedToDate = dtLvRule.Rows[dtLvRule.Rows.Count - 1]["pleaveDate"].ToString();

                string outMinDays = cmdLvRule.Parameters["@pMinDaysLeave"].Value==null?"":cmdLvRule.Parameters["@pMinDaysLeave"].Value.ToString();

                string outMaxDays =cmdLvRule.Parameters["@pMaxDaysLeave"].Value==null?"":cmdLvRule.Parameters["@pMaxDaysLeave"].Value.ToString();
                float lvCount = 0;
                if (dtLvRule.Rows.Count > 0)
                {
                    lvCount = dtLvRule.Rows.Count;

                    if (rbtLeaveType1.SelectedValue == "H")
                    {
                        lvCount = lvCount / 2;
                    }

                }
                if (outMinDays == "")
                {
                    outMinDays = "0";
                }
                if (outMaxDays == "")
                {
                    outMaxDays = "0";
                }


                if (Convert.ToInt32(outMinDays) > LvDays)
                {

                    lblMessages.Text = "Please apply minimum " + outMinDays.ToString() + " as per HR policies";
                    lblMessages.Visible = true;
                    mpeAddCall.Hide();
                    return;
                }
                if (Convert.ToInt32(outMaxDays) != 0)
                {
                    if (Convert.ToInt32(outMaxDays) < LvDays)
                    {

                        lblMessages.Text = "Please apply maximum " + outMinDays.ToString() + " as per HR policies";
                        lblMessages.Visible = true;
                        mpeAddCall.Hide();
                        return;
                    }
                }

                if (LvDays < lvCount)
                {
                    txtFrmDate.Text = Convert.ToDateTime(updatedfromDate.Remove(10)).ToString("dd/MM/yyyy");
                    txtTDate.Text = Convert.ToDateTime(updatedToDate.Remove(10)).ToString("dd/MM/yyyy");

                    lblPnlMesg.Text = "The leave dates and count has been changed as per HR policies";
                    lblPnlMesg.Visible = true;
                    //  mpeAddCall.Hide();
                    return;
                }


                if (rbtLeaveType1.SelectedValue == "F")
                {


                    objcmd.CommandText = "Update ESS_TA_LA set ESS_LA_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103), ESS_LA_TODT=Convert(datetime,'" + strdbToDt + "',103) ," +
                                        " ESS_LA_lvcd='" + ddleaveType.SelectedValue + "',ESS_LA_RSNID='" + ddlReasonTypeLVReq.SelectedValue + "', ESS_LA_LVDAYS='" + LvDays + "' ," +
                                 " ESS_LA_Remark='" + txtRemark.Text + "'  " +
                                 " where ESS_LA_ISDELETED='0' And ESS_LA_RowID=" + ViewState["RowId"] + "";



                    objcmd.ExecuteNonQuery();
                }

                else if (rbtLeaveType1.SelectedValue == "H")
                {

                    objcmd.CommandText = "Update ESS_TA_LA set ESS_LA_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103), ESS_LA_TODT=Convert(datetime,'" + strdbToDt + "',103) ," +
                                        " ESS_LA_lvcd='" + ddleaveType.SelectedValue + "',ESS_LA_RSNID='" + ddlReasonTypeLVReq.SelectedValue + "', ESS_LA_LVDAYS='0.5' ," +
                                 " ESS_LA_Remark='" + txtRemark.Text + "'  " +
                                 " where ESS_LA_ISDELETED='0' And ESS_LA_RowID=" + ViewState["RowId"] + "";



                    objcmd.ExecuteNonQuery();
                }
                lblMessages.Text = "Record updated Successfully";
                lblMessages.Visible = true;
                ViewState["RowId"] = "";
                ViewState["strToDt"] = "";


                chkMailConfiguration();
                if (strMailOption == "Y")
                {
                    SendMailEdit();
                }
                bindDataGrid();
                mpeAddCall.Hide();

            }
            catch (Exception ex)
            {

            }



        }
        protected void btnSaveLvReq_Click(object sender, EventArgs e)
        {
            ModifyLeaveApplication();
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvLeave.PageIndex = Convert.ToInt32(((DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
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
        protected void bindDataGrid(string status)
        {
            try
            {

                string strsql = "select ESS_LA_RowID,ess_LA_empid as EmpCode, ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name   ,convert(VARCHAR(20),Ess_LA_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_LA_Todt,103)= convert(VARCHAR(20),Ess_LA_fromdt,103) " +
                           " THEN convert(VARCHAR(20),Ess_LA_fromdt,103) ELSE CONVERT(VARCHAR(20),ESS_LA_TODT,103)  END AS TODATE, ESS_LA_LVCD , Case  ESS_LA_Status WHEN 'N' then 'Pending For Approval' " +
                          " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_LA_Status from ESS_TA_LA,ent_employee_personal_dtls " +
                          " where  ess_ta_la.ess_LA_empid=ent_employee_personal_dtls.epd_empid and ESS_LA_STATUS='" + status + "' " +
                            "and ess_la_empid= '" + userid + "' And ess_la_isdeleted='0' order by convert(datetime,Ess_LA_fromdt,103)  desc";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLeave.DataSource = dt;
                gvLeave.DataBind();
                DropDownList ddl = (DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeave.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeave.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeave.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeave.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeave.PageCount == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex + 1 == gvLeave.PageCount)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeave.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeave.PageSize * gvLeave.PageIndex) + 1) + " to " + (gvLeave.PageSize * (gvLeave.PageIndex + 1));

                gvLeave.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }

        }

    }
}