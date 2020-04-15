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
    public partial class ESS_TA_OD_View : System.Web.UI.Page
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
        public string strEmpToAddress="";

        public string strdbFrmdt;
        public string strdbToDt;
        public string strFrmdt;
        public string Rowid;
        public string strtodt;

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

                FillReasonOD();
                gvOD.Columns[2].Visible = false;
            }
            manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
        }
     
        private void bindDataGrid()
        {
            try
            {

                //string strsql = "select ESS_OD_RowID,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
                //                " then '' else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD , Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' " +
                //               " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status from ESS_TA_OD,ent_employee_personal_dtls  where" +
                //    "   ess_ta_od.ess_OD_empid=ent_employee_personal_dtls.epd_empid " +
                //      "  and ess_od_empid='" + userid + "'  and ess_od_isdeleted='0' ";


                //changes made bya vaibhav put NA on behalf of blank in todate
                string strsql = "select ESS_OD_RowID,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, Ess_OD_Remark, case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
                      " then convert(VARCHAR(20),Ess_OD_fromdt,103) else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD , Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' " +
                     " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status from ESS_TA_OD,ent_employee_personal_dtls  where" +
          "   ess_ta_od.ess_OD_empid=ent_employee_personal_dtls.epd_empid " +
            "  and ess_od_empid='" + userid + "'  and ess_od_isdeleted='0' order by convert(datetime,Ess_OD_fromdt,103)  desc  ";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOD.DataSource = dt;
                gvOD.DataBind();
                DropDownList ddl = (DropDownList)gvOD.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvOD.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvOD.PageIndex + 1).ToString();
                Label lblcount = (Label)gvOD.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvOD.DataSource).Rows.Count.ToString() + " Records.";
                if (gvOD.PageCount == 0)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvOD.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvOD.PageIndex + 1 == gvOD.PageCount)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvOD.PageIndex == 0)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvOD.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvOD.PageSize * gvOD.PageIndex) + 1) + " to " + (gvOD.PageSize * (gvOD.PageIndex + 1));

                gvOD.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_OD_View");
            }

        }
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            gvOD.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            bindDataGrid();
        }



        protected void gvOD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOD.PageIndex = e.NewPageIndex;
            bindDataGrid();

        }



        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < gvOD.Rows.Count; i++)
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
                        CheckBox delrows = (CheckBox)gvOD.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)gvOD.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }

                            objcmd.CommandText = "Update ESS_TA_OD set ESS_OD_ISDELETED = '1', ESS_OD_DELETEDDATE = convert(datetime,'" + manualattnddeletedate + "',103) where ESS_OD_Rowid = '" + lblRowId.Text + "' ";

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


                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
                string message = "I had taken OD from " + txtFrmDate.Text + " to " + txtTDate.Text + System.Environment.NewLine + "";
                message = message + "Reason: " + txtRemark.Text + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
              //  objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
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
        public void SendMailDelete()
        {
            FindEmailID();
            FindMgrMailid();

            try
            {
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I would like to cancel OD from " + strdbFrmdt + " to " + strdbToDt + "";
                message = message + " Kindly do the needful." + System.Environment.NewLine + "";

                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "Cancellation of OD Application";
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
                string strsql1 = "select ESS_OD_RowID,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
                       " then convert(VARCHAR(20),Ess_OD_fromdt,103) else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD , Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' " +
                      " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status from ESS_TA_OD,ent_employee_personal_dtls  where" +
           "   ess_ta_od.ess_OD_empid=ent_employee_personal_dtls.epd_empid " +
             "  and ess_od_empid='" + userid + "'  and ess_od_isdeleted='0'  order by convert(datetime,Ess_OD_fromdt,103)  desc ";
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
                    gvOD.DataSource = dt;
                    gvOD.DataBind();

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
                    gvOD.DataSource = _tempDT;
                    gvOD.DataBind();
                }

                DropDownList ddl = (DropDownList)gvOD.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvOD.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvOD.PageIndex + 1).ToString();
                Label lblcount = (Label)gvOD.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvOD.DataSource).Rows.Count.ToString() + " Records.";
                if (gvOD.PageCount == 0)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvOD.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvOD.PageIndex + 1 == gvOD.PageCount)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvOD.PageIndex == 0)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvOD.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvOD.PageSize * gvOD.PageIndex) + 1) + " to " + (gvOD.PageSize * (gvOD.PageIndex + 1));

                gvOD.BottomPagerRow.Visible = true;

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


        protected void gvOD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedit = (LinkButton)e.Row.FindControl("lnkEdit");
                CheckBox chkApprove = (CheckBox)e.Row.FindControl("DeleteRows");

                if ((e.Row.Cells[8].Text == "Approved") || (e.Row.Cells[8].Text == "Rejected"))
                {
                    lnkedit.Enabled = false;
                    chkApprove.Enabled = false;
                }

            }
        }

        protected void gvOD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {


                ViewState["RowId"] = e.CommandArgument.ToString();
                fillModifydata(e.CommandArgument.ToString());
                mpeAddCall.Show();
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvOD.PageIndex = gvOD.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_OD_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvOD.PageIndex = gvOD.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_OD_View");
            }
        }

        public void fillModifydata(string strIdEmp)
        {
            try
            {
                string strsql = "select ess_OD_Empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, Convert(VARCHAR(20),Ess_OD_Todt,103) " +
                                " AS ToDate,Ess_OD_ODcd as ODCode, ESS_OD_RSNID, Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval'" +
                                 " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status, " +
                                 " ESS_OD_REMARK from ESS_TA_OD where ESS_OD_RowId =  " + strIdEmp + "";
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
                ddlReasonOD.SelectedValue = dt.Rows[0]["ESS_OD_RSNID"].ToString();
                txtRemark.Text = dt.Rows[0]["ESS_OD_REMARK"].ToString();
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
        private void FillReasonOD()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                               " and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'OD' ";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReasonOD.DataValueField = "Reason_ID";
                ddlReasonOD.DataTextField = "Reason_Description";
                ddlReasonOD.DataSource = dtReason;
                ddlReasonOD.DataBind();
                ddlReasonOD.Items.Insert(0, "Select One");
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

        protected void btnSaveOdReq_Click(object sender, EventArgs e)
        {
            ModifyODApplication();
        }

        protected void btnCancelOdreq_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }
        public void ModifyODApplication()
        {
            if (txtTDate.Text == "")
            {
                txtTDate.Text = txtFrmDate.Text;
            }
            strdbFrmdt = txtFrmDate.Text;
            strdbToDt = txtTDate.Text;
            strdbFrmdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            strdbToDt = DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            Rowid = Convert.ToString(ViewState["RowId"]);

            try
            {
                string str = "Select ESS_od_empid from ESS_TA_od Where (ESS_od_FROMDT between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbToDt + "',103)  OR ESS_OD_TODT  between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbToDt + "',103)) and ESS_OD_ISDELETED=0  AND ESS_OD_EMPID= '" + Session["uid"] + "' AND   ESS_OD_RowId <>  " + Rowid + "";
                SqlCommand objcmd = new SqlCommand(str, conn);
                String strduplicateLeavedt = String.Empty;
                strduplicateLeavedt = (String)objcmd.ExecuteScalar();
                if (strduplicateLeavedt != null)
                {


                    lblMessages.Text = "Outdoor already applied for the date range";
                    lblMessages.Visible = true;
                    mpeAddCall.Hide();


                    return;
                }


                objcmd.CommandText = "Update ESS_TA_OD set ESS_OD_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103),ESS_OD_ODCD='OD'," +
                                 " ESS_OD_RSNID='" + ddlReasonOD.SelectedValue + "'," +
                                 " ESS_OD_TODT=Convert(datetime,'" + strdbToDt + "',103),ESS_OD_Remark='" + txtRemark.Text + "'  " +
                                 " where ESS_OD_ISDELETED='0' And ESS_OD_RowID=" + Rowid + "";



                objcmd.ExecuteNonQuery();


                lblMessages.Text = "Record Updated Successfully";
                lblMessages.Visible = true;
                conn.Close();

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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }



        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvOD.PageIndex = Convert.ToInt32(((DropDownList)gvOD.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
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

                //string strsql = "select ESS_OD_RowID,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
                //                " then '' else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD , Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' " +
                //               " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status from ESS_TA_OD,ent_employee_personal_dtls  where" +
                //    "   ess_ta_od.ess_OD_empid=ent_employee_personal_dtls.epd_empid " +
                //      "  and ess_od_empid='" + userid + "'  and ess_od_isdeleted='0' ";


                //changes made bya vaibhav put NA on behalf of blank in todate
                string strsql = "select ESS_OD_RowID,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode,convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
                      " then convert(VARCHAR(20),Ess_OD_fromdt,103) else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD , Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' " +
                     " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_OD_Status from ESS_TA_OD,ent_employee_personal_dtls  where" +
          "   ess_ta_od.ess_OD_empid=ent_employee_personal_dtls.epd_empid " +
            "  and ess_od_empid='" + userid + "' and ESS_OD_STATUS='" + status + "'  and ess_od_isdeleted='0' order by convert(datetime,Ess_OD_fromdt,103)  desc  ";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOD.DataSource = dt;
                gvOD.DataBind();
                DropDownList ddl = (DropDownList)gvOD.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvOD.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvOD.PageIndex + 1).ToString();
                Label lblcount = (Label)gvOD.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvOD.DataSource).Rows.Count.ToString() + " Records.";
                if (gvOD.PageCount == 0)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvOD.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvOD.PageIndex + 1 == gvOD.PageCount)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvOD.PageIndex == 0)
                {
                    ((Button)gvOD.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvOD.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvOD.PageSize * gvOD.PageIndex) + 1) + " to " + (gvOD.PageSize * (gvOD.PageIndex + 1));

                gvOD.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_OD_View");
            }

        }
    }
}