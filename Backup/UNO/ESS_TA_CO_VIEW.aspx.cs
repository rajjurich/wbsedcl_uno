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
using System.Drawing;
using System.Collections;
using System.Text;


namespace UNO
{
    public partial class ESS_TA_CO_VIEW : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        string manualattnddeletedate;
        string userid;
        public string strMailOption;
        public string strMailServer;
        public string strMailUserName;
        public string strMailPassword;
        public int strMailPort;
        public string strEmpFromAddress;
        public string strEmpToAddress;
        public string strdbFrmdt;
        public string strdbToDt;
        string Rowid1 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = cmdSearch.UniqueID;
            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "click")
            {
                bindDataPopUpGrid();
                mpeAddCompOff.Show();
            }

            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }

            if (ViewState["Row"] != null)
            {
                Rowid1 = ViewState["Row"].ToString();
            }

            if (!Page.IsPostBack)
            {
                bindDataGrid();
                //bindDataPopUpGrid();
                btnAdd_Click();
                BindReasonType();
                FillEmployeeEntity();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvLVDetails.ClientID + "');");
            }

            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");

        }
        private void FillEmployeeEntity()
        {
            string strSql = "";

            //strSql = "SELECT epd_empid as ID,EPD_FIRST_NAME+'~'+epd_empid as NAME FROM ENT_employee_personal_dtls where  epd_isdeleted='0' ";
            strSql = " SELECT epd_empid as ID,EPD_FIRST_NAME +'  '+ EPD_LAST_NAME +'('+epd_empid+')' as NAME FROM ENT_employee_personal_dtls e with (nolock) " +
                     " inner join ent_employee_official_dtls eo with (nolock) on e.epd_empid=eo.eod_empid  " +
                     " where  epd_isdeleted='0' and isnull(EOD_ACTIVE,0)=1 ";

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            if (thisDataSet.Tables[0].Rows.Count > 0)
            {
                ddlEmp.DataSource = thisDataSet.Tables[0];
                ddlEmp.DataTextField="Name";
                ddlEmp.DataValueField = "ID";
                ddlEmp.DataBind();

                ddlEmp.Items.Insert(0,new ListItem("Select","0"));
            }

        }
        
        private DataTable Get_PROC_GET_CO_DETAILS_BYEMPID()
        {
            DataTable dt = null;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int _result = 0;

            SqlCommand cmd = new SqlCommand("PROC_GET_CO_DETAILS_BYEMPID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ESS_CO_EMPID", "");
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return dt;
        }
        private DataTable Get_PROC_GET_CO_DETAILS_BYSTATUS(string Status)
        {
            DataTable dt = null;

            string status = cmbStatus.SelectedValue.ToString();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("PROC_GET_CO_DETAILS_BYSTATUS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ESS_CO_EMPID", userid);
            cmd.Parameters.AddWithValue("@ESS_CO_STATUS", Status);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);


            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return dt1;

        }
        private DataTable Get_PROC_GET_CO_DETAILS_BYROWID(string rowid)
        {
            DataTable dt = null;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int _result = 0;

            SqlCommand cmd = new SqlCommand("PROC_GET_CO_DETAILS_BYROWID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ESS_CO_ROWID", rowid);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return dt;
        }
        private DataTable Get_PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID(string empid)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            
            SqlCommand cmd = new SqlCommand("PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ESS_CO_EMPID", empid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return dt1;
        }
        private void SAVE_ESS_TA_CO(DateTime fromdate, DateTime LeaveAganistDate, int rowID, string employeeID, ref string strErrorMsg,ref string strSuccessMsg)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            Decimal NoOfdays = 1;

            string Reason;
            string Remark;

            if (rowID != 0)
            {
                Reason = ddlReasonEdit.SelectedItem.Text;
                Remark = txtRemarkEdit.Text;

            }
            else
            {
                Reason = ddlReasonType.SelectedItem.Text;
                Remark = txtRemark.Text;

            }

            SqlCommand cmd = new SqlCommand("PROC_SAVE_ESS_TA_CO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ESS_CO_RowID", rowID);
            cmd.Parameters.AddWithValue("@ESS_CO_EMPID", employeeID);
            cmd.Parameters.AddWithValue("@ESS_CO_FROMDT", fromdate);
            // cmd.Parameters.AddWithValue("@ESS_CO_TODT", fromdate);
            cmd.Parameters.AddWithValue("@ESS_CO_CD", "CO");
            cmd.Parameters.AddWithValue("@ESS_CO_RSNID", "1");
            cmd.Parameters.AddWithValue("@ESS_CO_REMARK", Remark);
            cmd.Parameters.AddWithValue("@ESS_CO_SANCID", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_SANCDT", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_SANC_REMARK", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_ORDER", DBNull.Value);
            cmd.Parameters.AddWithValue("@ESS_CO_STATUS", "N");
            cmd.Parameters.AddWithValue("@ESS_CO_OLDSTATUS", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_ISDELETED", 0);
            cmd.Parameters.AddWithValue("@ESS_CO_DELETEDDATE", DBNull.Value);
            cmd.Parameters.AddWithValue("@ESS_CO_DAYS", NoOfdays);
            cmd.Parameters.AddWithValue("@ESS_CO_REASON", Reason);
            cmd.Parameters.AddWithValue("@ESS_CO_LEAVEAGANISTDATE", LeaveAganistDate);
            cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
            cmd.Parameters["@strErrMsg"].Size = 1000;
            cmd.Parameters.AddWithValue("@strSuccMsg", '0').Direction = ParameterDirection.Output;
            cmd.Parameters["@strSuccMsg"].Size = 1000;
            cmd.ExecuteNonQuery();
            strErrorMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
            strSuccessMsg = cmd.Parameters["@strSuccMsg"].Value.ToString();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void BindReasonType()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("PROC_GET_CO_REASONTYPE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter daReason = new SqlDataAdapter(cmd);
            DataTable dtReason = new DataTable();
            daReason.Fill(dtReason);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            ddlReasonType.DataValueField = "Reason_ID";
            ddlReasonType.DataTextField = "Reason_Description";
            ddlReasonType.DataSource = dtReason;
            ddlReasonType.DataBind();
            ddlReasonType.Items.Insert(0, "Select One");

            ddlReasonEdit.DataValueField = "Reason_ID";
            ddlReasonEdit.DataTextField = "Reason_Description";
            ddlReasonEdit.DataSource = dtReason;
            ddlReasonEdit.DataBind();
            ddlReasonEdit.Items.Insert(0, "Select One");

        }
        void bindDataGrid()
        {
            try
            {
                DataTable dt = null;

                dt = Get_PROC_GET_CO_DETAILS_BYEMPID();

                if (dt.Rows.Count != 0)
                {
                    gvLVDetails.DataSource = dt;
                    gvLVDetails.DataBind();
                    DropDownList ddl = (DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo1");
                    for (int i = 1; i <= gvLVDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvLVDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvLVDetails.BottomPagerRow.FindControl("lblTotal1");
                    lblcount.Text = ((DataTable)gvLVDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvLVDetails.PageCount == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious1")).Enabled = false;
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex + 1 == gvLVDetails.PageCount)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious1")).Enabled = false;
                    }
                    //   ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing1")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1));
                    ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing1")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (((gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1)) - gvLVDetails.PageSize) + gvLVDetails.Rows.Count);
                    gvLVDetails.BottomPagerRow.Visible = true;

                }
                else
                {
                    gvLVDetails.DataSource = dt;
                    gvLVDetails.DataBind();

                }
                txtfromDate.Text = "";

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }

        }
        void bindDataPopUpGrid()
        {
            try
            {
                DataTable dt1 = null;
      
                string empCode = ddlEmp.SelectedValue.ToString();

                dt1 = Get_PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID(empCode);

                if (dt1.Rows.Count > 0)
                {
                    gvPopUp.Visible = true;
                    gvPopUp.PageSize = 5;
                    gvPopUp.DataSource = dt1;
                    gvPopUp.DataBind();

                    DropDownList ddl = (DropDownList)gvPopUp.BottomPagerRow.FindControl("ddlPageNo");

                    for (int i = 1; i <= gvPopUp.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvPopUp.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvPopUp.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvPopUp.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvPopUp.PageCount == 0)
                    {
                        ((Button)gvPopUp.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvPopUp.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvPopUp.PageIndex + 1 == gvPopUp.PageCount)
                    {
                        ((Button)gvPopUp.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvPopUp.PageIndex == 0)
                    {
                        ((Button)gvPopUp.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing1")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (((gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1)) - 10) + gvLVDetails.Rows.Count);

                    gvPopUp.BottomPagerRow.Visible = true;

                }
                else
                {
                    gvPopUp.DataSource = null;
                    gvPopUp.DataBind();
                    if (ddlEmp.SelectedIndex == 0)
                        gvPopUp.Visible = false;
                    else
                        gvPopUp.Visible = true;
                }
              
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
        }
      
        protected void gvLVDetails_DataBound(object sender, EventArgs e)
        {
            DataTable dt1 = null;

            dt1 = Get_PROC_GET_CO_DETAILS_BYEMPID();

            int recordcnt = dt1.Rows.Count;
        }
       
        protected void gvLVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }
        protected void gvLVDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLVDetails.PageIndex = e.NewPageIndex;
            bindDataGrid();
        }
        private bool ValidateOnPaging()
        {
            foreach (GridViewRow gvr in gvPopUp.Rows)
            {
                CheckBox SaveRows = (CheckBox)gvr.FindControl("SaveRows");

                if (SaveRows.Checked == true)
                {
                    lblAddError.Text = "Please Submit the selected Days before proceeding";
                    lblAddError.Visible = true;
                    return false;
                }
                //else
                //{
                //    lblAddError.Text = "";
                //    lblAddError.Visible = true;
                //}
            }

            return true;
        }
        protected void gvPopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (ValidateOnPaging())
            {
                gvPopUp.PageIndex = e.NewPageIndex;
                bindDataPopUpGrid();
            }

        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvPopUp.PageIndex = gvPopUp.PageIndex - 1;
                bindDataPopUpGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvPopUp.PageIndex = gvPopUp.PageIndex + 1;
                bindDataPopUpGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvPopUp.PageIndex = Convert.ToInt32(((DropDownList)gvPopUp.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataPopUpGrid();
                mpeAddCompOff.Show();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
        }
        protected void gvPrevious1(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = gvLVDetails.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }

        }
        protected void gvNext1(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = gvLVDetails.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }

        }
        protected void ChangePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = Convert.ToInt32(((DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo1")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
        }
        protected void gvLVDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            mpModifyCommon.Show();
        }
        protected void gvPopUp_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddpagesize = sender as DropDownList;
            gvLVDetails.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            bindDataGrid();
        }
        protected void btnCancelAdd_Click(object sender, EventArgs e)  // Cancel Button Click Event
        {
            ResetAll();
            mpeAddCompOff.Hide();
        }
        void btnAdd_Click()   // New Button Click Event
        {
            ResetAll();
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e) // Pop Submit Comp Off 
        {
            try
            {
                bool isSaved = false;
                int fromdate = Convert.ToDateTime(txtfromDate1.Text).DayOfYear;
                int toDate = Convert.ToDateTime(txtToDate1.Text).DayOfYear;

                string from = Convert.ToDateTime(txtfromDate1.Text).Date.ToString("dd/MM/yy");
                string to = Convert.ToDateTime(txtToDate1.Text).Date.ToString("dd/MM/yy");

                string employeeID = "";

                if (ValidateOnSubmit())
                {
                    ArrayList arrlist = new ArrayList();
                    StringBuilder strError = new StringBuilder();
                    int difference = 0;
                    int j = 0;

                    if (fromdate == toDate)
                    {
                        difference = 1;
                    }
                    else
                    {
                        difference = (toDate - fromdate) + 1;
                    }

                    for (int i = 0; i < difference; i++)
                    {
                        arrlist.Add(Convert.ToDateTime(txtfromDate1.Text).AddDays(i).ToString("dd/MM/yyyy"));
                    }
                    foreach (GridViewRow gr in gvPopUp.Rows)
                    {
                        int rowIndex = gr.RowIndex;
                        CheckBox SaveRows = (CheckBox)gvPopUp.Rows[rowIndex].FindControl("SaveRows");
                        Label lblEMPID = (Label)gvPopUp.Rows[rowIndex].FindControl("lblEMPID");

                        if (SaveRows.Checked)
                        {
                            SqlDataAdapter da = new SqlDataAdapter("select top 1 1 from tday with(nolock) where TDAY_EMPCDE='" + lblEMPID.Text + "' and tday_date=Convert(datetime,'" + arrlist[j].ToString() + "',105) and tday_status='AB'", conn);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                                strError.Append(arrlist[j].ToString());
                            j = j + 1;
                        }
                    }
                    if (strError.Length > 1)
                    {
                        lblAddError.Text = "Invalid Day Selection!";
                        lblAddError.Visible = true;
                        mpeAddCompOff.Show();
                        return;
                    }
                    j = 0;
                    foreach (GridViewRow gr in gvPopUp.Rows)
                    {
                        int rowIndex = gr.RowIndex;
                        CheckBox SaveRows = (CheckBox)gvPopUp.Rows[rowIndex].FindControl("SaveRows");
                        Label lblRowID = (Label)gvPopUp.Rows[rowIndex].FindControl("lblRowID");
                        Label lblFromDT = (Label)gvPopUp.Rows[rowIndex].FindControl("lblFromDT");
                        Label lblEMPID = (Label)gvPopUp.Rows[rowIndex].FindControl("lblEMPID");

                        if (SaveRows.Checked == true)
                        {
                            DateTime CO_fromdate = Convert.ToDateTime(arrlist[j].ToString());

                            string leaveDate = Convert.ToDateTime(lblFromDT.Text).ToString("dd/MM/yyyy");

                            DateTime LeaveAgainstDate = Convert.ToDateTime(leaveDate);
                            try
                            {
                                string strErrmsg = string.Empty;
                                string strSuccessMsg = string.Empty;
                                int rowID = 0;
                                employeeID = lblEMPID.Text;
                                SAVE_ESS_TA_CO(CO_fromdate, LeaveAgainstDate, rowID, employeeID, ref strErrmsg, ref strSuccessMsg);
                                j = j + 1;
                                if (strErrmsg.Trim().Length > 1)
                                {
                                    lblAddError.Text = strErrmsg;
                                    lblAddError.Visible = true;
                                    isSaved = false;

                                }
                                else
                                {
                                    lblAddError.Text = strSuccessMsg;
                                    lblAddError.Visible = true;
                                    isSaved = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
                            }
                        }
                        
                    }
                }

                if (isSaved)
                {
                    //ResetAll();
                    txtfromDate.Text = "";
                    //FillEmployeeEntity();
                    txtfromDate1.Text = "";
                    txtToDate1.Text = "";
                    ddlEmp.SelectedIndex = 0;
                    ddlReasonType.SelectedIndex = -1;
                    txtRemark.Text = "";
                    bindDataPopUpGrid();
                    bindDataGrid();
                    mpeAddCompOff.Show();
                    ScriptManager.RegisterClientScriptBlock(UpAdd, UpAdd.GetType(), "Script", "validateChosen();", true);

                }
                
            }
            catch (Exception ee)
            {
                UNOException.UNO_DBErrorLog(ee.Message, ee.StackTrace, "ESSTACO");
            }

        }
        protected void btnModifySave_Click(object sender, EventArgs e)
        {

            bool isSaved = false;

            int row = Convert.ToInt32(Rowid1);

            string fromdate = Convert.ToDateTime(txtEditFromDate.Text).ToString("dd/MM/yyyy");

            DateTime CompOffDate = Convert.ToDateTime(fromdate);
            string strposteddate = "01/01/9999";

            try
            {
                string employeeIdEdit = txtEditEmployee.Text;
                string strErrmsg = string.Empty;
                string strOutMsg = string.Empty;

                SqlDataAdapter da = new SqlDataAdapter("select top 1 1 from tday with(nolock) where TDAY_EMPCDE='" + employeeIdEdit + "' and tday_date=Convert(datetime,'" + fromdate + "',105) ", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblErrorEdit.Text = "Invalid Day Selection!";
                    lblErrorEdit.Visible = true;
                    return;
                }

                SAVE_ESS_TA_CO(CompOffDate, Convert.ToDateTime(strposteddate), row, employeeIdEdit, ref strErrmsg, ref strOutMsg);
                if (strOutMsg.Trim().Length > 1)
                {
                    lblErrorEdit.Text = strOutMsg.Trim();
                    lblErrorEdit.Visible = true;
                    mpModifyCommon.Show();
                    bindDataGrid();
                }
                else
                {
                    lblErrorEdit.Text = strErrmsg.Trim();
                    lblErrorEdit.Visible = true;
                    mpModifyCommon.Show();
                }
                isSaved = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UK_ESS_TA_CO_ESS_CO_FROMDT"))
                {
                    lblErrorEdit.Text = "Already applied on " + "  -  " + CompOffDate.ToString("dd/MM/yyyy");
                    lblErrorEdit.Visible = true;
                    return;
                }
                else
                {
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
                }
            }

        }
        protected void btnModifyCancel_Click(object sender, EventArgs e)
        {
            mpModifyCommon.Hide();
            ResetAll();
        }
        private bool ValidateOnSubmit()
        {

            int fromdate = Convert.ToInt32(Convert.ToDateTime(txtfromDate1.Text).DayOfYear);
            int todate = Convert.ToInt32(Convert.ToDateTime(txtToDate1.Text).DayOfYear);
            int DaysSelected = 0;
            int chkcount = 0;

            if (fromdate == todate)
            {
                DaysSelected = 1;
            }
            else
            {
                DaysSelected = (todate - fromdate) + 1;
            }

            foreach (GridViewRow dgvRow in gvPopUp.Rows)
            {
                CheckBox SaveRows = (CheckBox)dgvRow.FindControl("SaveRows");

                if (SaveRows.Checked == true)
                {
                    chkcount = chkcount + 1;
                }
            }
            if (chkcount == 0)
            {
                lblAddError.Visible = true;
                lblAddError.Text = "Please Select the record";
                return false;
            }

            if (chkcount > DaysSelected)
            {
                lblAddError.Visible = true;
                lblAddError.Text = "Days checked is more than the days selected";
                return false;
            }

            if (chkcount < DaysSelected)
            {
                lblAddError.Visible = true;
                lblAddError.Text = "Days checked is less than the days selected";
                return false;
            }
        
            return true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)  //Delete Applied Comp-Off
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            int _result = 0;

            for (int i = 0; i < gvLVDetails.Rows.Count; i++)
            {
                try
                {
                    CheckBox delrows = (CheckBox)gvLVDetails.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {

                        int rowID = Convert.ToInt32(gvLVDetails.Rows[i].Cells[2].Text);
                        SqlCommand cmd = new SqlCommand("PROC_DELETE_CO_DETAILS_BYROWID", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ESS_CO_RowID", rowID);
                        cmd.ExecuteNonQuery();
                        int result = cmd.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        da.Fill(dt1);

                        if (result > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "Message", "alert('Record deleted successfully');", true);
                        }

                        string someScript1 = "";
                        someScript1 = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), "onload", someScript1);
                    }

                }

                catch (Exception ex)
                {
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
                }

            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            ResetAll();
        }
        protected void gvLVDetails_sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvPopUp_sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void gvLVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit3")
            {
                string Rowid = e.CommandArgument.ToString();


                ViewState["Row"] = Rowid;

                DataTable dt = null;

                dt = Get_PROC_GET_CO_DETAILS_BYROWID(Rowid);

                txtEditFromDate.Text = Convert.ToDateTime(dt.Rows[0]["CO_FROMDT"].ToString()).ToString("dd/MM/yyyy");

                txtLeaveDate.Text = Convert.ToDateTime(dt.Rows[0]["ESS_CO_LEAVEAGANISTDATE"].ToString()).ToString("dd/MM/yyyy");

                ddlReasonEdit.SelectedValue = dt.Rows[0]["Reason_ID"].ToString();

                txtRemarkEdit.Text = dt.Rows[0]["CO_REMARK"].ToString();

                txtEditEmployee.Text = dt.Rows[0]["CO_EMPID"].ToString();

                mpModifyCommon.Show();
            }

        }
       
      
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                string strsql = "SELECT ESS_CO_RowID AS CO_ROWID,ESS_CO_EMPID CO_EMPID,'CO' AS LEAVECODE," +
                              "ESS_CO_REQUESTDT CO_REQUESTDT,CONVERT(VARCHAR(10),ESS_CO_FROMDT,105)  CO_FROMDT," +
                              "ESS_CO_CD CO_CD,ESS_CO_RSNID CO_RSNID,ESS_CO_REMARK CO_REMARK,ESS_CO_SANCID CO_SANCID," +
                              "ESS_CO_SANCDT CO_SANCDT,ESS_CO_SANC_REMARK CO_SANC_REMARK," +
                              "ESS_CO_ORDER CO_ORDER,isnull(ESS_CO_STATUS,'N') CO_STATUS,ESS_CO_OLDSTATUS CO_OLDSTATUS," +
                              "ESS_CO_ISDELETED CO_ISDELETED,ESS_CO_DELETEDDATE CO_DELETEDDATE," +
                              "ESS_CO_DAYS CO_DAYS,CONVERT(VARCHAR(10),ESS_CO_LEAVEAGANISTDATE,105) ESS_CO_LEAVEAGANISTDATE" +
                              "   FROM ESS_TA_CO WHERE ESS_CO_ISDELETED = 0 AND  ESS_CO_STATUS =  'N'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtfromDate.Text.ToString() == "")
                {
                    cmdReset_Click(sender, e);
                }
                else
                {
                    String[,] values = { 
                {"CO_EMPID~" +txtfromDate.Text.Trim(), "S" }		
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvLVDetails.DataSource = _tempDT;
                    gvLVDetails.DataBind();
                    _tempDT = _sc.searchTable(values, dt);
                    gvLVDetails.DataSource = _tempDT;
                    gvLVDetails.DataBind();

                    DropDownList ddl = (DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo1");
                    for (int i = 1; i <= gvLVDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvLVDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvLVDetails.BottomPagerRow.FindControl("lblTotal1");
                    lblcount.Text = ((DataTable)gvLVDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvLVDetails.PageCount == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious1")).Enabled = false;
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex + 1 == gvLVDetails.PageCount)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious1")).Enabled = false;
                    }
                    ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing1")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1));

                    gvLVDetails.BottomPagerRow.Visible = true;
                }
            }

            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        protected void cmdReset_Click(object sender, EventArgs e)  // TO reset all to Default Value
        {
            ResetAll();
        }
        void ResetAll()
        {
            txtfromDate.Text = "";
          
            txtfromDate1.Text = "";
            txtToDate1.Text = "";
            ddlReasonType.SelectedIndex = -1;
            txtRemark.Text = "";
            
            lblAddError.Text = "";        
            lblErrorEdit.Text = "";           

        }
        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            ResetAll();
            mpeAddCompOff.Show();
            ScriptManager.RegisterClientScriptBlock(UpAdd, UpAdd.GetType(), "Script", "validateChosen();", true);
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

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

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
        public void FindEmailID()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            objcmd.CommandText = "select epd_email from ENT_EMPLOYEE_PERSONAL_DTLS where epd_empid= '" + userid + "'";
            
            SqlDataReader dr = objcmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["epd_email"] != null)
                {
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
            SqlDataReader drmgr = objMgrcmd.ExecuteReader();
            if (drmgr.Read())
            {
                if (drmgr["epd_email"] != null)
                {
                    strEmpToAddress = drmgr["epd_email"].ToString();
                }
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
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

                string message = "I had taken COMP - OFF on " + txtfromDate.Text + System.Environment.NewLine + "";
                message = message + "Reason: " + txtRemark.Text + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + ddlEmp.SelectedItem.Text + "";

                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                objMailMessage.Bcc.Add("tk_helpdesk@cms.co.in");
                objMailMessage.Subject = "COMP - OFF Application";
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
                lblAddError.Text = ex.Message;
                lblErrorEdit.Visible = true;
                lblErrorEdit.Text = ex.Message;
                lblErrorEdit.Visible = true;
            }
        }
        
        
        protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = cmbStatus.SelectedValue.ToString();
            if (status == "A")
            {
                try
                {
                    DataTable dt = null;

                    dt = Get_PROC_GET_CO_DETAILS_BYSTATUS(status);

                    gvLVDetails.DataSource = dt;
                    gvLVDetails.DataBind();
                    DropDownList ddl = (DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo1");
                    for (int i = 1; i <= gvLVDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvLVDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvLVDetails.BottomPagerRow.FindControl("lblTotal1");
                    lblcount.Text = ((DataTable)gvLVDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvLVDetails.PageCount == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious1")).Enabled = false;
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex + 1 == gvLVDetails.PageCount)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious1")).Enabled = false;
                    }
                    ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing1")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1));

                    gvLVDetails.BottomPagerRow.Visible = true;

                    txtfromDate.Text = "";

                }
                catch (Exception ex)
                {
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
                }
            }
            else
            {
                bindDataGrid();
            }
        }

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDataPopUpGrid();
            ScriptManager.RegisterClientScriptBlock(UpAdd, UpAdd.GetType(), "Script", "validateChosen();", true);
            mpeAddCompOff.Show();
        }

    }
}

