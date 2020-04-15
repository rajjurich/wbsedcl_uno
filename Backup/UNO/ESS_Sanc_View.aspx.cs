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
using System.Collections.Specialized;
using System.Collections;

using System.Net;
using System.Net.Mail;
using System.Threading;

namespace UNO
{
    public partial class ESS_Sanc_View : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        public string strMailOption;
        public string strMailServer;
        public string strMailUserName;
        public string strMailPassword;
        public int strMailPort;
        public string strEmpFromAddress;
        public string strEmpToAddress;


        public Label lblFrmDt;
        public Label lblToDt;
        public Label lblStats;

        public static string userid;

       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }

            gvManualAttnd.Columns[2].Visible = false;
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                BindCount();
                gvManualAttnd.Columns[10].Visible = false;
                if (cmbEntity.SelectedValue == "LA")
                {
                    gvManualAttnd.Columns[5].Visible = true;
                }
            }

        }
        protected void BindCount()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("exec Usp_Application_Count @Id='" + userid + "'", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    lblLa.Text = Convert.ToString(ds.Tables[0].Rows[0]["ESS_TA_LA"]);
                    lblMa.Text = Convert.ToString(ds.Tables[1].Rows[0]["ESS_TA_MA"]);
                    lblGP.Text = Convert.ToString(ds.Tables[2].Rows[0]["ESS_TA_GP"]);
                    lblOd.Text = Convert.ToString(ds.Tables[3].Rows[0]["ESS_TA_OD"]);
                    lblCo.Text = Convert.ToString(ds.Tables[4].Rows[0]["ESS_CO_CD"]);
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }

        }
        private DataTable GetDataTable(string EmpCode, string RequestType, string StatusType)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_ESS_SANCTION_DATA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
                cmd.Parameters.Add("@RequestType", SqlDbType.NVarChar).Value = RequestType;
                if (ddlStatus.SelectedValue != "ALL")
                {
                    cmd.Parameters.Add("@StatusType", SqlDbType.NVarChar).Value = StatusType;
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public void bindDataGrid()
        {

            try
            {
                DataTable _tempDT = new DataTable();
                DataTable dt = new DataTable();
                dt = GetDataTable(userid, cmbEntity.SelectedValue, ddlStatus.SelectedValue);
                if (txtTodate.Text.ToString() == "" && txtFromDate.Text.ToString() == "" && txtEmpCode.Text.ToString() == "" && txtName.Text.ToString() == "")
                {
                    gvManualAttnd.DataSource = dt;
                    gvManualAttnd.DataBind();
                    if (dt.Rows.Count != 0)
                    {
                        btnApprove.Enabled = true;
                        btnDelete.Enabled = true;
                        DropDownList ddl = (DropDownList)gvManualAttnd.BottomPagerRow.FindControl("ddlPageNo");
                        for (int i = 1; i <= gvManualAttnd.PageCount; i++)
                        {
                            ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                        ddl.SelectedValue = (gvManualAttnd.PageIndex + 1).ToString();
                        Label lblcount = (Label)gvManualAttnd.BottomPagerRow.FindControl("lblTotal");
                        lblcount.Text = ((DataTable)gvManualAttnd.DataSource).Rows.Count.ToString() + " Records.";
                        if (gvManualAttnd.PageCount == 0)
                        {
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (gvManualAttnd.PageIndex + 1 == gvManualAttnd.PageCount)
                        {
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (gvManualAttnd.PageIndex == 0)
                        {
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        }
                        ((Label)gvManualAttnd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManualAttnd.PageSize * gvManualAttnd.PageIndex) + 1) + " to " + (gvManualAttnd.PageSize * (gvManualAttnd.PageIndex + 1));

                        gvManualAttnd.BottomPagerRow.Visible = true;
                        selectChk();
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                    }
                }
                else
                {
                    String[,] values = { 
				{"fromdate~" +txtFromDate.Text.Trim(), "D" },
				{"Todate~" +txtTodate.Text.Trim(), "D" },
			    {"EmpID~" +txtEmpCode.Text.Trim(), "S" }	,               
                {"Name~" +txtName.Text.Trim(), "S" }
				 };
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvManualAttnd.DataSource = _tempDT;
                    gvManualAttnd.DataBind();
                    if (_tempDT.Rows.Count != 0)
                    {
                        DropDownList ddl = (DropDownList)gvManualAttnd.BottomPagerRow.FindControl("ddlPageNo");
                        for (int i = 1; i <= gvManualAttnd.PageCount; i++)
                        {
                            ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                        ddl.SelectedValue = (gvManualAttnd.PageIndex + 1).ToString();
                        Label lblcount = (Label)gvManualAttnd.BottomPagerRow.FindControl("lblTotal");
                        lblcount.Text = ((DataTable)gvManualAttnd.DataSource).Rows.Count.ToString() + " Records.";
                        if (gvManualAttnd.PageCount == 0)
                        {
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (gvManualAttnd.PageIndex + 1 == gvManualAttnd.PageCount)
                        {
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (gvManualAttnd.PageIndex == 0)
                        {
                            ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        }
                        ((Label)gvManualAttnd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManualAttnd.PageSize * gvManualAttnd.PageIndex) + 1) + " to " + (gvManualAttnd.PageSize * (gvManualAttnd.PageIndex + 1));

                        gvManualAttnd.BottomPagerRow.Visible = true;
                        btnApprove.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                    }
                }

                CommonValue();

            }
            catch (Exception ex)
            {

            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindDataGrid();
        }
       
        protected void gvManualAttnd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                gvManualAttnd.EditIndex = e.NewEditIndex;
                string ID = gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[2].Text;
                string EmpID = gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[3].Text;
                string strFromDt = gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[4].Text;
            }
            catch(Exception ex)
            {
            }

        }
        protected void gvManualAttnd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvManualAttnd.PageIndex = e.NewPageIndex;
            bindDataGrid();


        }
        protected void gvManualAttnd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string abc = row.Cells[2].Text;
            }
        }

        protected void gvManualAttnd_RowCommand2(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "Edit")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblname = (Label)gvManualAttnd.Rows[row.RowIndex].FindControl("lblName");
                Label lblMgrRemark = (Label)gvManualAttnd.Rows[row.RowIndex].FindControl("lblMgrRemark");
                txtSanctRemarks.Text = lblMgrRemark.Text;
                Session["SanctRowid"] = lblname.Text;
                mpeAddCall.Show();
            }
        }
        protected void gvManualAttnd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            gvManualAttnd.Columns[5].Visible = false;
            gvManualAttnd.Columns[12].Visible = true;
            if (cmbEntity.SelectedValue == "LA")
            {

                gvManualAttnd.Columns[5].Visible = true;
            }
            if (cmbEntity.SelectedValue != "OD" && cmbEntity.SelectedValue != "GP")
            {
                gvManualAttnd.Columns[12].Visible=false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMgrRemark = (Label)e.Row.FindControl("lblMgrRemark");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (lblMgrRemark.Text != "")
                {
                    lnkEdit.Text = "View Remark";
                }
            }

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                string strSanctId = Session["SanctRowid"].ToString();
                SqlConnection conn = new SqlConnection(m_connectons);
                SqlCommand cmd = new SqlCommand();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                if (cmbEntity.SelectedValue == "LA")
                {

                    cmd.CommandText = "update ESS_TA_LA set ESS_LA_SANC_REMARK='" + txtSanctRemarks.Text + "' where ESS_LA_ROWID='" + strSanctId + "'";
                }
                else if (cmbEntity.SelectedValue == "OD")
                {

                    cmd.CommandText = "update ESS_TA_OD set ESS_OD_SANC_REMARK='" + txtSanctRemarks.Text + "' where ESS_OD_ROWID='" + strSanctId + "'";
                }
                else if (cmbEntity.SelectedValue == "MA")
                {

                    cmd.CommandText = "update ESS_TA_MA set ESS_MA_SANC_REMARK='" + txtSanctRemarks.Text + "' where ESS_MA_ROWID='" + strSanctId + "'";
                }
                else if (cmbEntity.SelectedValue == "GP")
                {

                    cmd.CommandText = "update ESS_TA_GP set ESS_GP_SANC_REMARK='" + txtSanctRemarks.Text + "' where ESS_GP_ROWID='" + strSanctId + "'";
                }
                else if (cmbEntity.SelectedValue == "CO")
                {

                    cmd.CommandText = "update ESS_TA_CO set ESS_CO_SANC_REMARK='" + txtSanctRemarks.Text + "' where ESS_CO_ROWID='" + strSanctId + "'";
                }

                cmd.ExecuteNonQuery();
                conn.Close();
                txtSanctRemarks.Text = "";
                lblMessages.Text = "Comment Saved Successfully";
                lblMessages.Visible = true;
                mpeAddCall.Hide();
                bindDataGrid();
                BindCount();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }

        //For LA
        private void ApproveRejectLeave(string EmpCode, string Rowid, string FromDate, string ToDate, string strRequestType, string LeaveCode)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_ESS_LA_REQUEST", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
            cmd.Parameters.Add("@RowId", SqlDbType.NVarChar).Value = Rowid;
            cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            cmd.Parameters.Add("@strRequestType", SqlDbType.NVarChar).Value = strRequestType;
            cmd.Parameters.Add("@LeaveCode", SqlDbType.NVarChar).Value = LeaveCode;
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        //FOR OP
        private void ApproveRejectOutPass(string EmpCode, string Rowid, string FromDate, string strRequestType)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_ESS_OP_REQUEST", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
            cmd.Parameters.Add("@RowId", SqlDbType.NVarChar).Value = Rowid;
            cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            cmd.Parameters.Add("@strRequestType", SqlDbType.NVarChar).Value = strRequestType;
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        // For OD,CO,MA
        private void ApproveRejectRequest(string storedProcName ,string EmpCode, string Rowid, string FromDate, string ToDate, string strRequestType)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(storedProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
            cmd.Parameters.Add("@RowId", SqlDbType.NVarChar).Value = Rowid;
            cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            cmd.Parameters.Add("@strRequestType", SqlDbType.NVarChar).Value = strRequestType;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void ApproveApplication()
        {
            bool Check = false;
            CheckBox chkall = (CheckBox)gvManualAttnd.HeaderRow.Cells[0].FindControl("ChkAll");
            if (chkall.Checked == true)
            {
                for (int i = 0; i < gvManualAttnd.Rows.Count; i++)
                {
                    try
                    {
                        Label rowid = (Label)gvManualAttnd.Rows[i].FindControl("lblname");
                        Label empid = (Label)gvManualAttnd.Rows[i].FindControl("lblEmpCode");
                        Label levcode = (Label)gvManualAttnd.Rows[i].FindControl("lblleavecde");
                        lblFrmDt = (Label)gvManualAttnd.Rows[i].FindControl("lblFrmDt");
                        lblToDt = (Label)gvManualAttnd.Rows[i].FindControl("lblToDt");
                        lblStats = (Label)gvManualAttnd.Rows[i].FindControl("lblStats");
                        if (lblStats.Text != "Approved")
                        {
                            if (cmbEntity.SelectedValue == "MA")
                            {

                                Check = true;
                                ApproveRejectRequest("USP_ESS_MA_REQUEST", empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "A");
                            }

                            else if (cmbEntity.SelectedValue == "LA")
                            {
                                Check = true;
                                ApproveRejectLeave(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "A", levcode.Text);

                            }

                            else if (cmbEntity.SelectedValue == "GP")
                            {
                                Check = true;
                                ApproveRejectOutPass(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, "A");
                            }

                            else if (cmbEntity.SelectedValue == "OD")
                            {
                                Check = true;
                                ApproveRejectRequest("USP_ESS_OD_REQUEST", empid.Text, rowid.Text, lblFrmDt.Text, lblToDt.Text, "A");

                            }

                            else if (cmbEntity.SelectedValue == "CO")
                            {

                                Check = true;
                                ApproveRejectRequest("USP_ESS_CO_REQUEST", empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "A");


                            }
                            chkMailConfiguration();
                            if (strMailOption == "Y")
                            {
                                GetMailIDs(empid.Text);
                                Thread thread = new Thread(() => SendMail(empid.Text));
                                thread.Start();
                            }
                        }
                        else
                        {
                            lblMessages.Visible = true;
                            lblMessages.Text = "Selected request is already approved";
                            return;
                        }


                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
            }
            else
            {

                for (int i = 0; i < gvManualAttnd.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox chkRows = (CheckBox)gvManualAttnd.Rows[i].FindControl("DeleteRows");
                        if (chkRows.Checked == true)
                        {
                            Label rowid = (Label)gvManualAttnd.Rows[i].FindControl("lblname");
                            Label empid = (Label)gvManualAttnd.Rows[i].FindControl("lblEmpCode");
                            Label levcode = (Label)gvManualAttnd.Rows[i].FindControl("lblleavecde");
                            lblFrmDt = (Label)gvManualAttnd.Rows[i].FindControl("lblFrmDt");
                            lblToDt = (Label)gvManualAttnd.Rows[i].FindControl("lblToDt");
                            lblStats = (Label)gvManualAttnd.Rows[i].FindControl("lblStats");
                            if (lblStats.Text != "Approved")
                            {
                                if (cmbEntity.SelectedValue == "MA")
                                {

                                    Check = true;
                                    ApproveRejectRequest("USP_ESS_MA_REQUEST",empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "A");
                                }

                                else if (cmbEntity.SelectedValue == "LA")
                                {
                                    Check = true;
                                    ApproveRejectLeave(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "A", levcode.Text);

                                }

                                else if (cmbEntity.SelectedValue == "GP")
                                {
                                    Check = true;
                                    ApproveRejectOutPass(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, "A");
                                }

                                else if (cmbEntity.SelectedValue == "OD")
                                {
                                    Check = true;
                                    ApproveRejectRequest("USP_ESS_OD_REQUEST", empid.Text, rowid.Text, lblFrmDt.Text, lblToDt.Text, "A");

                                }

                                else if (cmbEntity.SelectedValue == "CO")
                                {

                                    Check = true;
                                    ApproveRejectRequest("USP_ESS_CO_REQUEST", empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "A");


                                }
                                chkMailConfiguration();
                                if (strMailOption == "Y")
                                {
                                    GetMailIDs(empid.Text);
                                    Thread thread = new Thread(() => SendMail(empid.Text));
                                    thread.Start();
                                }
                                   

                            }
                            else
                            {
                                lblMessages.Visible = true;
                                lblMessages.Text = "Selected request is already approved";
                                return;
                            }
                            

                        }
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }

                }

            }



        }


        public void RejectApplication()
        {
            bool Check = false;
            CheckBox chkall = (CheckBox)gvManualAttnd.HeaderRow.Cells[0].FindControl("ChkAll");
            if (chkall.Checked == true)
            {


                for (int i = 0; i < gvManualAttnd.Rows.Count; i++)
                {
                    try
                    {
                     
                        Label rowid = (Label)gvManualAttnd.Rows[i].FindControl("lblname");
                        Label empid = (Label)gvManualAttnd.Rows[i].FindControl("lblEmpCode");
                        Label levcode = (Label)gvManualAttnd.Rows[i].FindControl("lblleavecde");
                        lblFrmDt = (Label)gvManualAttnd.Rows[i].FindControl("lblFrmDt");
                        lblToDt = (Label)gvManualAttnd.Rows[i].FindControl("lblToDt");
                        lblStats = (Label)gvManualAttnd.Rows[i].FindControl("lblStats");
                        if (cmbEntity.SelectedValue == "MA")
                        {
                            Check = true;
                            ApproveRejectRequest("USP_ESS_MA_REQUEST",empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "R");
                        }

                        else if (cmbEntity.SelectedValue == "LA")
                        {
                            Check = true;
                            ApproveRejectLeave(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "R", levcode.Text);
                        }

                        else if (cmbEntity.SelectedValue == "GP")
                        {
                            Check = true;
                            ApproveRejectOutPass(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, "R");
                        }

                        else if (cmbEntity.SelectedValue == "OD")
                        {
                            Check = true;
                            ApproveRejectRequest("USP_ESS_OD_REQUEST", empid.Text, rowid.Text, lblFrmDt.Text, lblToDt.Text, "R");
                        }

                        else if (cmbEntity.SelectedValue == "CO")
                        {
                            Check = true;
                            ApproveRejectRequest("USP_ESS_CO_REQUEST", empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "R");

                        }
                           chkMailConfiguration();
                           if (strMailOption == "Y")
                           {
                               GetMailIDs(empid.Text);
                               Thread thread = new Thread(() => SendMailforRejectedApp(empid.Text));
                               thread.Start();
                           }


                    }
                    catch (Exception ex)
                    {
                        
                    }


                }
            }
            else
            {

                for (int i = 0; i < gvManualAttnd.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox chkRows = (CheckBox)gvManualAttnd.Rows[i].FindControl("DeleteRows");
                        if (chkRows.Checked == true)
                        {
                            //string rowid = Session["SanctRowid"].ToString();
                            Label rowid = (Label)gvManualAttnd.Rows[i].FindControl("lblname");
                            // string rowid = gvManualAttnd.Rows[i].Cells[2].Text;

                            Label empid = (Label)gvManualAttnd.Rows[i].FindControl("lblEmpCode");

                            Label levcode = (Label)gvManualAttnd.Rows[i].FindControl("lblleavecde");

                            lblFrmDt = (Label)gvManualAttnd.Rows[i].FindControl("lblFrmDt");

                            lblToDt = (Label)gvManualAttnd.Rows[i].FindControl("lblToDt");
                            lblStats = (Label)gvManualAttnd.Rows[i].FindControl("lblStats");
                            if (cmbEntity.SelectedValue == "MA")
                            {

                                Check = true;
                                ApproveRejectRequest("USP_ESS_MA_REQUEST",empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "R");
                            }

                            else if (cmbEntity.SelectedValue == "LA")
                            {
                                Check = true;
                                ApproveRejectLeave(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "R", levcode.Text);
                            }

                            else if (cmbEntity.SelectedValue == "GP")
                            {
                                Check = true;
                                ApproveRejectOutPass(empid.Text.Trim(), rowid.Text, lblFrmDt.Text, "R");
                            }

                            else if (cmbEntity.SelectedValue == "OD")
                            {
                                Check = true;
                                ApproveRejectRequest("USP_ESS_OD_REQUEST", empid.Text, rowid.Text, lblFrmDt.Text, lblToDt.Text, "R");
                            }

                            else if (cmbEntity.SelectedValue == "CO")
                            {
                                Check = true;
                                ApproveRejectRequest("USP_ESS_CO_REQUEST", empid.Text.Trim(), rowid.Text, lblFrmDt.Text, lblToDt.Text, "R");
                            }
                               chkMailConfiguration();
                               if (strMailOption == "Y")
                               {
                                   GetMailIDs(empid.Text);
                                   Thread thread = new Thread(() => SendMailforRejectedApp(empid.Text));
                                   thread.Start();
                               }

                        }
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApproveApplication();
            bindDataGrid();
            BindCount();
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
               

            }

        }
        private void GetMailIDs(string strEmpCode)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand("USP_DashBoard", conn);
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "getEmailId";
            objcmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = strEmpCode;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(objcmd);
            da.Fill(ds);
            strEmpFromAddress = Convert.ToString(ds.Tables[0].Rows[0][0]); //emplyee email id
            strEmpToAddress = Convert.ToString(ds.Tables[1].Rows[0][0]); //manager email id

        }
     

        public void SendMailforRejectedApp(string strEmpCode)
        {

                    string message = "";
                    try
                    {
                        SmtpClient objSMTPCLIENT = new SmtpClient();
                        MailMessage objMailMessage = new MailMessage();



                        if (cmbEntity.SelectedValue == "MA")
                        {
                            message = "Manual Attendance Application submitted by you for  '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Rejected.";
                        }

                        if (cmbEntity.SelectedValue == "LA")
                        {
                            message = "Leave Application submitted by you for '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Rejected.";
                        }

                        if (cmbEntity.SelectedValue == "GP")
                        {
                            message = "Official Out-Pass Application submitted by you for '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Rejected.";
                        }

                        if (cmbEntity.SelectedValue == "OD")
                        {
                            message = "OD Application submitted by you for '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Rejected.";
                        }


                        if (cmbEntity.SelectedValue == "CO")
                        {
                            message = "CO Application submitted by you for '" + lblFrmDt.Text + "'";
                            message = message + "has been Rejected.";
                        }


                        objMailMessage.From = new MailAddress(strEmpToAddress);
                        objMailMessage.To.Add(strEmpFromAddress.Trim());
                        objMailMessage.Subject = "Rejected Application";
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
        
        public void SendMail(string strEmpCode)
        {
 
                    string message = "";

                    try
                    {
                        SmtpClient objSMTPCLIENT = new SmtpClient();
                        MailMessage objMailMessage = new MailMessage();
                        if (cmbEntity.SelectedValue == "MA")
                        {
                            message = "Manual Attendance Application submitted by you for  '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Approved.";
                        }

                        if (cmbEntity.SelectedValue == "LA")
                        {
                            message = "Leave Application submitted by you for '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Approved.";
                        }

                        if (cmbEntity.SelectedValue == "GP")
                        {
                            message = "Official Out-Pass Application submitted by you for '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Approved.";
                        }

                        if (cmbEntity.SelectedValue == "OD")
                        {
                            message = "OD Application submitted by you for '" + lblFrmDt.Text + "' to '" + lblToDt.Text + "'";
                            message = message + "has been Approved.";
                        }



                        if (cmbEntity.SelectedValue == "CO")
                        {
                            message = "CO Application submitted by you for '" + lblFrmDt.Text + "'";
                            message = message + "has been Approved.";
                        }


                        objMailMessage.From = new MailAddress(strEmpToAddress);
                        objMailMessage.To.Add(strEmpFromAddress.Trim());
                        objMailMessage.Subject = "Approved Application";
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

        protected void cmbEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStatus.SelectedIndex = 0;
            gvManualAttnd.PageIndex = 0;
            bindDataGrid();
            lblMessages.Text = "";
            lblMessages.Visible = false;
          //  CommonValue();
        }
        private void CommonValue()
        {
            if ((cmbEntity.SelectedValue == "LA") || (cmbEntity.SelectedValue == "OD"))
            {
                gvManualAttnd.Columns[9].Visible = true;
            }
            else
            {
                gvManualAttnd.Columns[9].Visible = false;
            }

            if (cmbEntity.SelectedValue == "CO")
            {
                gvManualAttnd.HeaderRow.Cells[7].Text = "Non-Working Date";
                gvManualAttnd.HeaderRow.Cells[6].Text = "Com-Off Date";
            }
            else if (cmbEntity.SelectedValue == "MA" || cmbEntity.SelectedValue == "GP")
            {
                gvManualAttnd.HeaderRow.Cells[7].Text = "To Date-Time";
                gvManualAttnd.HeaderRow.Cells[6].Text = "From Date-Time";
            }
            else
            {
                gvManualAttnd.HeaderRow.Cells[7].Text = "To Date";
                gvManualAttnd.HeaderRow.Cells[6].Text = "From Date";
            }
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            RejectApplication();
            bindDataGrid();
            BindCount();
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvManualAttnd.PageIndex = Convert.ToInt32(((DropDownList)gvManualAttnd.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvManualAttnd.PageIndex = gvManualAttnd.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_Sanc_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvManualAttnd.PageIndex = gvManualAttnd.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_Sanc_View");
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectChk();
            bindDataGrid();
        }

        private void selectChk()
        {
            if (gvManualAttnd.Rows.Count > 0)
            {
                if (ddlStatus.SelectedValue == "N")
                {
                    ((CheckBox)gvManualAttnd.HeaderRow.Cells[0].FindControl("ChkAll")).Visible = true;
                }
                else
                {
                    ((CheckBox)gvManualAttnd.HeaderRow.Cells[0].FindControl("ChkAll")).Visible = false;
                }
            }

        }



        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvManualAttnd.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvManualAttnd.HeaderRow.Cells[0].FindControl("ChkAll");
                if (chkall.Checked == true)
                {
                    CheckBox chkRows = (CheckBox)gvManualAttnd.Rows[i].FindControl("DeleteRows");

                    chkRows.Checked = true;
                }
                else
                {
                    CheckBox chkRows = (CheckBox)gvManualAttnd.Rows[i].FindControl("DeleteRows");

                    chkRows.Checked = false;
                }
            }
        }






    }
}