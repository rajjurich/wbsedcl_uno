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
    public partial class ESS_TA_GP_View : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        string manualattnddeletedate;
        string userid;
        public string strFrmdt;
        public string Rowid;
        public string strToDt;

        public string strMailOption;
        public string strMailServer;
        public string strMailUserName;
        public string strMailPassword;
        public int strMailPort;
        public string strEmpFromAddress;
        public string strEmpToAddress;

        public string strdbFrmdt;
        public string strdbToDt;

        protected void Page_Load(object sender, EventArgs e)
        {
            //HtmlGenericControl div = (HtmlGenericControl)Page.Master.FindControl("menuDiv1");

            //div.Visible = true;

            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }

            if (!Page.IsPostBack)
            {



                bindDataGrid();
                ddlStatus.SelectedIndex = 0;
                FillReasonOutPass();
             
                gvGP.Columns[2].Visible = false;
            }
            manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
        }
   
        private void FillReasonOutPass()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                                "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'GP' ";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlResonOutPaas.DataValueField = "Reason_ID";
                ddlResonOutPaas.DataTextField = "Reason_Description";
                ddlResonOutPaas.DataSource = dtReason;
                ddlResonOutPaas.DataBind();
                ddlResonOutPaas.Items.Insert(0, "Select One");

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
        private void bindDataGrid()
        {
            try
            {
                string strsql = "select ESS_GP_RowID,ess_GP_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_GP_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_GP_Todt,103)= convert(VARCHAR(20),Ess_GP_fromdt,103) " +
                          "  then ''else convert(VARCHAR(20),Ess_GP_Todt,103)  end as ToDate,convert(char(5),ESS_GP_FROMTM,8) as [In],convert(char(5),ESS_GP_TOTM,8) as [Out], Case  ESS_GP_Status WHEN 'N' then 'Pending For Approval' " +
                         " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_GP_Status from ESS_TA_GP,ent_employee_personal_dtls  " +
                          " where  ess_ta_gp.ess_gp_empid=ent_employee_personal_dtls.epd_empid " +
                         " and ess_gp_empid= '" + userid + "' and ess_gp_isdeleted='0'  order by convert(datetime,Ess_GP_fromdt,103)  desc";


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                #region OPAccess
                ///////////Started///////////
                DataTable thisDataSet = new DataTable(); ;
                DataTable temp = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    string levelId = Session["levelId"].ToString();

                    SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
                    thisDataSet = new DataTable();
                    adpt.Fill(thisDataSet);

                    IEnumerable<DataRow> drRow = (from acs in dt.AsEnumerable()
                                                  join comwise in thisDataSet.AsEnumerable() on acs.Field<string>("ess_GP_empid") equals comwise.Field<string>("EOD_EMPID")
                                                  select acs
                                 );
                    if (Session["uid"].ToString().ToLower() == "admin")
                    {
                        temp = dt;
                    }
                    else
                    {
                        temp = drRow.CopyToDataTable();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                ///////////end////////////
                # endregion OPAccess
                gvGP.DataSource = temp;
                gvGP.DataBind();
          


                if (dt.Rows.Count != 0)
                {
                    btnSearch.Enabled = true;
                    btnDelete.Enabled = true;
                    DropDownList ddl = (DropDownList)gvGP.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvGP.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvGP.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvGP.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvGP.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvGP.PageCount == 0)
                    {
                        ((Button)gvGP.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvGP.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvGP.PageIndex + 1 == gvGP.PageCount)
                    {
                        ((Button)gvGP.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvGP.PageIndex == 0)
                    {
                        ((Button)gvGP.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvGP.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvGP.PageSize * gvGP.PageIndex) + 1) + " to " + (gvGP.PageSize * (gvGP.PageIndex + 1));

                    gvGP.BottomPagerRow.Visible = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_GP_View");
            }

        }
      



        protected void gvGP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGP.PageIndex = e.NewPageIndex;
            bindDataGrid();

        }

    
        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < gvGP.Rows.Count; i++)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand objcmd = new SqlCommand();
                    objcmd.Connection = conn;
                    SqlTransaction trans=null;
                    try
                    {
                        trans = conn.BeginTransaction();
                        objcmd.Transaction = trans;
                        CheckBox delrows = (CheckBox)gvGP.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)gvGP.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {

                            if (marked == false)
                            {
                                marked = true;
                            }

                            objcmd.CommandText = "Update ESS_TA_GP set ESS_GP_isdeleted = '1', ESS_GP_DELETEDDATE = convert(datetime,'" + manualattnddeletedate + "',103) where ESS_GP_Rowid = '" + lblRowId.Text + "' ";
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_GP_View");
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

        public void SendMailDelete()
        {
            FindEmailID();
            FindMgrMailid();

            try
            {
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I would like to cancel Official Out-Pass Application from " + strdbFrmdt + " to " + strdbToDt + "";
                message = message + "Kindly do the needful." + System.Environment.NewLine + "";

                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = " Cancellation of Official Out-Pass Application";
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
                string strsql = "select ESS_GP_RowID,ess_GP_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_GP_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_GP_Todt,103)= convert(VARCHAR(20),Ess_GP_fromdt,103) " +
                           "  then ''else convert(VARCHAR(20),Ess_GP_Todt,103)  end as ToDate,convert(char(5),ESS_GP_FROMTM,8) as [In],convert(char(5),ESS_GP_TOTM,8) as [Out], Case  ESS_GP_Status WHEN 'N' then 'Pending For Approval' " +
                          " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_GP_Status from ESS_TA_GP,ent_employee_personal_dtls  " +
                           " where  ess_ta_gp.ess_gp_empid=ent_employee_personal_dtls.epd_empid " +
                          " and ess_gp_empid= '" + userid + "' and ess_gp_isdeleted='0'  order by convert(datetime,Ess_GP_fromdt,103)  desc";

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
                    gvGP.DataSource = dt;
                    gvGP.DataBind();

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
                    gvGP.DataSource = _tempDT;
                    gvGP.DataBind();
                }

                DropDownList ddl = (DropDownList)gvGP.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvGP.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvGP.PageIndex + 1).ToString();
                Label lblcount = (Label)gvGP.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvGP.DataSource).Rows.Count.ToString() + " Records.";
                if (gvGP.PageCount == 0)
                {
                    ((Button)gvGP.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvGP.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvGP.PageIndex + 1 == gvGP.PageCount)
                {
                    ((Button)gvGP.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvGP.PageIndex == 0)
                {
                    ((Button)gvGP.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvGP.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvGP.PageSize * gvGP.PageIndex) + 1) + " to " + (gvGP.PageSize * (gvGP.PageIndex + 1));

                gvGP.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_GP_View");
            }

        }


        protected void gvGP_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvGP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {


                ViewState["RowId"] = e.CommandArgument.ToString();
                fillModifydata(e.CommandArgument.ToString());
                mpeAddCall.Show();
            }
        }
        public void fillModifydata(string Rowid)
        {

            string strsql = "select ess_GP_empid as EmpCode,convert(VARCHAR(20),Ess_GP_fromdt,103) as FromDate, Convert(VARCHAR(20),Ess_GP_Todt,103) AS " +
                                    " ToDate,convert(char(5),ESS_GP_FROMTM,8) as [In],convert(char(5),ESS_GP_TOTM,8) as [Out], Case  ESS_GP_Status WHEN 'N' then 'Pending For Approval' " +
                                   " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_GP_Status,ESS_GP_RSNID,ESS_GP_REMARK from ESS_TA_GP where ess_GP_empid='" + userid + "'  " +
                                   " AND  ESS_GP_RowId = " + Rowid + "";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //strFrmdt = DateTime.ParseExact(dt.Rows[0]["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            //strToDt = DateTime.ParseExact(dt.Rows[0]["ToDate"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            //strFrmdt = CMSDateTime.CMSDateTime.ConvertToDateTime(dt.Rows[0]["FromDate"].ToString(), "dd/MM/yyyy").ToString("dd/MM/yyyy");
            //strToDt = CMSDateTime.CMSDateTime.ConvertToDateTime(dt.Rows[0]["ToDate"].ToString(), "dd/MM/yyyy").ToString("dd/MM/yyyy");

            strFrmdt = dt.Rows[0]["FromDate"].ToString();
            strToDt = dt.Rows[0]["ToDate"].ToString();

            WriteLog("after From Date :-" + strdbFrmdt);
            WriteLog("after To Date :-" + strToDt);

            txtFrmDate.Text = strFrmdt;
            txtTDate.Text = strToDt;

            WriteLog("Text IN From Date :-" + strdbFrmdt);
            WriteLog("Text IN To Date :-" + strToDt);

            txtInOuP.Text = dt.Rows[0]["In"].ToString();
            txtInTimeOutP.Text = dt.Rows[0]["Out"].ToString();

            //ddlModeType.SelectedValue = dt.Rows[0]["ma_mode"].ToString();
           ddlResonOutPaas.SelectedValue = dt.Rows[0]["ESS_GP_RSNID"].ToString();
           txtRemark.Text = dt.Rows[0]["ESS_GP_Remark"].ToString();


        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvGP.PageIndex = gvGP.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_GP_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvGP.PageIndex = gvGP.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_GP_View");
            }
        }
        protected void btnSaveOdReq_Click(object sender, EventArgs e)
        {
            ModifyGatePass();
            mpeAddCall.Hide();
        }
        protected void btnCancelOdreq_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvGP.PageIndex = Convert.ToInt32(((DropDownList)gvGP.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
        public void ModifyGatePass()
        {
            string strdbFrmdt;
            string strdbToDt;

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
            //string str = "Select ESS_GP_empid from ESS_TA_GP Where ESS_GP_FROMDT between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbToDt + "',103)  AND ESS_GP_EMPID= '" + Session["uid"] + "' AND   ESS_GP_RowId <>  " + Rowid + "";
            //SqlCommand cmd = new SqlCommand(str, conn);
            //String strduplicateLeavedt = String.Empty;
            //strduplicateLeavedt = (String)cmd.ExecuteScalar();
            //if (strduplicateLeavedt != null)
            //{


            //    lblMessages.Text = "Out-Pass already applied for the date range";
            //    lblMessages.Visible = true;
            //    mpeAddCall.Hide();


            //    return;
            //}

            SqlCommand cmd = new SqlCommand("USP_DashBoard @strCommand='CheckOutPassForEdit',@FromDate='" + strdbFrmdt + "',@Todate='" + strdbToDt + "',@FromTime='" + txtInOuP.Text + "',@ToTime='" + txtInTimeOutP.Text + "',@userId='" + Convert.ToString(Session["uid"]) + "',@Rowid=" + Rowid + "", conn);
            string strResult = Convert.ToString(cmd.ExecuteScalar());
            if (strResult != "")
            {
                lblMessages.Text = "Outpass already applied for the date and time";
                lblMessages.Visible = true;
                return;
            }

            //strdbFrmdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            //strdbToDt = DateTime.ParseExact(strdbToDt, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");


            strdbFrmdt = CMSDateTime.CMSDateTime.ConvertToDateTime(strdbFrmdt, "dd/MM/yyyy").ToString("dd/MM/yyyy");
            strdbToDt = CMSDateTime.CMSDateTime.ConvertToDateTime(strdbToDt, "dd/MM/yyyy").ToString("dd/MM/yyyy");


            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;



            try
            {

                objcmd.CommandText = "Update ESS_TA_GP set ESS_GP_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103),ESS_GP_FROMTM=Convert(varchar(8),'" + txtInOuP.Text + "',103)," +
                                 " ESS_GP_TOTM=Convert(varchar(8),'" + txtInTimeOutP.Text + "',103),ESS_GP_RSNID='" +  ddlResonOutPaas.SelectedValue + "'," +
                                 " ESS_GP_TODT=Convert(datetime,'" + strdbToDt + "',103),ESS_GP_Remark='" + txtRemark.Text + "'  " +
                                 " where ESS_GP_ISDELETED='0' And ESS_GP_RowID=" + Rowid + "";



                objcmd.ExecuteNonQuery();

                lblMessages.Text = "Record Saved Successfully";
                lblMessages.Visible = true ;
                conn.Close();

                chkMailConfiguration();
                if (strMailOption == "Y")
                {
                    SendMailEdit();
                }

                bindDataGrid();

            }
            catch (Exception ex)
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
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I had taken  Official Out-Pass for " + strdbFrmdt + " to " + strdbToDt + System.Environment.NewLine + "";
                message = message + "Reason: " + txtRemark.Text + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "Official Out-Pass Application";
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
                string strsql = "select ESS_GP_RowID,ess_GP_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_GP_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_GP_Todt,103)= convert(VARCHAR(20),Ess_GP_fromdt,103) " +
                          "  then ''else convert(VARCHAR(20),Ess_GP_Todt,103)  end as ToDate,convert(char(5),ESS_GP_FROMTM,8) as [In],convert(char(5),ESS_GP_TOTM,8) as [Out], Case  ESS_GP_Status WHEN 'N' then 'Pending For Approval' " +
                         " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_GP_Status from ESS_TA_GP,ent_employee_personal_dtls  " +
                          " where  ess_ta_gp.ess_gp_empid=ent_employee_personal_dtls.epd_empid " +
                         " and ess_gp_empid= '" + userid + "'  and ESS_GP_STATUS='" + status + "' and ess_gp_isdeleted='0'  order by convert(datetime,Ess_GP_fromdt,103)  desc";


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvGP.DataSource = dt;
                gvGP.DataBind();



                if (dt.Rows.Count != 0)
                {
                    btnSearch.Enabled = true;
                    btnDelete.Enabled = true;
                    DropDownList ddl = (DropDownList)gvGP.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvGP.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvGP.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvGP.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvGP.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvGP.PageCount == 0)
                    {
                        ((Button)gvGP.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvGP.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvGP.PageIndex + 1 == gvGP.PageCount)
                    {
                        ((Button)gvGP.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvGP.PageIndex == 0)
                    {
                        ((Button)gvGP.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvGP.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvGP.PageSize * gvGP.PageIndex) + 1) + " to " + (gvGP.PageSize * (gvGP.PageIndex + 1));

                    gvGP.BottomPagerRow.Visible = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_GP_View");
            }

        }

        private void WriteLog(string strMessage)
        {
            string strPath = "C:\\logs"; //@"D:\cms\UNO-LIVE\LOG\LeaveLog.txt";
            bool exists = System.IO.Directory.Exists(strPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(strPath);
            System.IO.StreamWriter SW = null;
            try
            {
                SW = new System.IO.StreamWriter(strPath + "\\LogFile.txt", true);
                SW.WriteLine(DateTime.Now.ToString("ddMMyyyy HHmmss") + " " + strMessage);

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
            finally
            {
                SW.Close();
            }
        }
      

    }
}