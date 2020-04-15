using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace UNO
{
    public partial class OutpassView : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        SqlConnection conn = new SqlConnection(m_connectons);
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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }


            if (!Page.IsPostBack)
            {


                bindDataGrid();
                FillReason();
                CalendarExtender1.EndDate = DateTime.Now.Date;
                CalendarExtender2.EndDate = DateTime.Now.Date;

            }
            manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
            btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvOutpass.ClientID + "');");
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("OutPassADD.aspx");
        }
        void bindDataGrid()
        {
            try
            {
                DataTable dt = GetOutPassData();
                gvOutpass.DataSource = dt;
                gvOutpass.DataBind();
                if (gvOutpass.Rows.Count > 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                    DropDownList ddl = (DropDownList)gvOutpass.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvOutpass.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvOutpass.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvOutpass.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvOutpass.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvOutpass.PageCount == 0)
                    {
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvOutpass.PageIndex + 1 == gvOutpass.PageCount)
                    {
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvOutpass.PageIndex == 0)
                    {
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvOutpass.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvOutpass.PageSize * gvOutpass.PageIndex) + 1) + " to " + (((gvOutpass.PageSize * (gvOutpass.PageIndex + 1)) - 10) + gvOutpass.Rows.Count);

                    gvOutpass.BottomPagerRow.Visible = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static DataTable GetOutPassData()
        {

            string strsql = "select OP_RECID, OP_EMPID , ent_employee_personal_dtls.Epd_First_Name + ' ' +ent_employee_personal_dtls.EPD_LAST_NAME as Name,convert(VARCHAR(20),OP_SWIPEFROMDATE,103) as FromDate," +
                                " case " +
                                "  when convert(VARCHAR(20),OP_SWIPETODATE,103)= convert(VARCHAR(20)," +
                                "OP_SWIPEFROMDATE,103) " +
                                " then '' else convert(VARCHAR(20),OP_SWIPETODATE,103)  end as " +
                                "ToDate ,convert(char(5),OP_SWIPEFROMTIME,8) " +
                                " as [In_Time],convert(char(5),OP_SWIPETOTIME,8) as [Out_Time], " +
                                " OP_STATUS from TA_Outpass_Att with(nolock),ent_employee_personal_dtls with(nolock)" +
                                " inner join ENT_EMPLOYEE_OFFICIAL_DTLS " +
                                " on EPD_EMPID=EOD_EMPID " +
                                " where  TA_Outpass_Att.OP_EMPID=ent_employee_personal_dtls.epd_empid  " +
                                " and OP_ISDELETED='0' and EPD_ISDELETED='0' and EOD_ACTIVE='1' ";
            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }       
        protected void gvManualAttnd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOutpass.PageIndex = e.NewPageIndex;
            bindDataGrid();

        }
        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            bool Check = false;
            bool marked = false;
            for (int i = 0; i < gvOutpass.Rows.Count; i++)
            {
                SqlConnection conn = new SqlConnection(m_connectons);
                conn.Open();
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;

                try
                {

                    CheckBox delrows = (CheckBox)gvOutpass.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        if (marked == false)
                        {
                            marked = true;
                        }
                        Label lblId = (Label)gvOutpass.Rows[i].FindControl("lblId");

                        string rowid = lblId.Text;


                        objcmd.CommandType = CommandType.StoredProcedure;
                        objcmd.CommandText = "[Proc_Insert_HRENTRY_Outpass]";
                        objcmd.Parameters.AddWithValue("@pREQ_RECID", rowid);
                        objcmd.Parameters.AddWithValue("@strCommand", "Delete");
                        objcmd.Parameters.AddWithValue("@output", '0').Direction = ParameterDirection.Output;
                        objcmd.Parameters["@output"].Size = 1000;

                        objcmd.ExecuteNonQuery();
                        lblmsg.Text = objcmd.Parameters["@output"].Value.ToString();
                       

                        Check = true;

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    // trans.Rollback();
                    // throw ex;
                }

            }
            if (Check == true)
            {
                lblMessages.Text = "Record(s) Deleted Successfully";
                lblMessages.Visible = true;
            }
            else if (marked == false)
            {
                lblMessages.Text = "Please select record to Delete";
                lblMessages.Visible = true;
            }
            bindDataGrid();
        
        }                    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = "";

        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            DataTable dt = GetOutPassData();
            if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
            {
                cmdReset_Click(sender, e);
            }
            else
            {
                String[,] values = { 
				{"OP_EMPID~" +txtCompanyId.Text.Trim(), "S" },
				{"name~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                DataTable _tempDT = new DataTable();
                Search _sc = new Search();
                if (_tempDT != null)
                { _tempDT.Rows.Clear(); }
                _tempDT = _sc.searchTable(values, dt);
                gvOutpass.DataSource = _tempDT;
                gvOutpass.DataBind();
                if (_tempDT.Rows.Count > 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                    DropDownList ddl = (DropDownList)gvOutpass.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvOutpass.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvOutpass.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvOutpass.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvOutpass.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvOutpass.PageCount == 0)
                    {
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvOutpass.PageIndex + 1 == gvOutpass.PageCount)
                    {
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvOutpass.PageIndex == 0)
                    {
                        ((Button)gvOutpass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }


                    ((Label)gvOutpass.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvOutpass.PageSize * gvOutpass.PageIndex) + 1) + " to " + (((gvOutpass.PageSize * (gvOutpass.PageIndex + 1)) - 10) + gvOutpass.Rows.Count);

                    gvOutpass.BottomPagerRow.Visible = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
            }

        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyId.Text = "";
            txtCompanyName.Text = "";
            lblMessages.Text = "";
            bindDataGrid();
            //Response.Redirect(Request.RawUrl);

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvOutpass.PageIndex = Convert.ToInt32(((DropDownList)gvOutpass.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvOutpass.PageIndex = gvOutpass.PageIndex - 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvOutpass.PageIndex = gvOutpass.PageIndex + 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        private void FillReason()
        {
            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                            "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'GP' ";
            SqlDataAdapter daReason = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtReason = new DataTable();
            daReason.Fill(dtReason);
            ddlReasonType.DataValueField = "Reason_ID";
            ddlReasonType.DataTextField = "Reason_Description";
            ddlReasonType.DataSource = dtReason;
            ddlReasonType.DataBind();
            ddlReasonType.Items.Insert(0, "Select One");

        }
        protected void gvOutpass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit1")
            {
                string strRowid = e.CommandArgument.ToString();
                GetOutpassDataByRowID(strRowid);
                mpModifyOutpass.Show();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();

            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;

            //DateTime tascfrmdt, tascTodt;
            //tascfrmdt = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd/MM/yyyy", null);
            //tascTodt = DateTime.ParseExact(txtToDate.Text.Trim(), "dd/MM/yyyy", null);
            try
            {


                objcmd.CommandText = "Proc_Insert_HRENTRY_Outpass";
                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.Parameters.AddWithValue("@pREQ_RECID", hdnRowID.Value);
                objcmd.Parameters.AddWithValue("@EntityFlag", "OP");
                objcmd.Parameters.AddWithValue("@ReasonCode", ddlReasonType.SelectedValue.ToString());
                objcmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text);
                objcmd.Parameters.AddWithValue("@LeaveCode", "");
                //objcmd.Parameters.AddWithValue("@leaveFromdate", DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null));
                //objcmd.Parameters.AddWithValue("@leaveTodate", DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null));

                objcmd.Parameters.AddWithValue("@leaveFromdate", CMSDateTime.CMSDateTime.ConvertToDateTime(txtFromDate.Text, "dd/MM/yyyy"));
                objcmd.Parameters.AddWithValue("@leaveTodate", CMSDateTime.CMSDateTime.ConvertToDateTime(txtToDate.Text, "dd/MM/yyyy"));

                objcmd.Parameters.AddWithValue("@strCommand", "Update");
                string fromTime = "01/01/1900 " + frm_time.Text;
                string ToTime = "01/01/1900 " + To_Time.Text;
                objcmd.Parameters.AddWithValue("@from_time", fromTime);
                objcmd.Parameters.AddWithValue("@to_time", ToTime);
                objcmd.Parameters.AddWithValue("@OP_Mode", "N");
                objcmd.Parameters.AddWithValue("@output", "").Direction = ParameterDirection.Output;
                objcmd.Parameters["@output"].Size = 1000;
                objcmd.ExecuteNonQuery();
                lblMessages.Text = objcmd.Parameters["@output"].Value.ToString();
                conn.Close();
                mpModifyOutpass.Hide();
                bindDataGrid();


            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "OutPass");
                conn.Close();
                conn.Dispose();

            }
        }
        public void GetOutpassDataByRowID(string strRowid)
        {
            SqlDataAdapter da = new SqlDataAdapter("exec Proc_Insert_HRENTRY_Outpass @strCommand='Select',@pREQ_RECID='" + strRowid + "'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txt_Remarks.Text = dt.Rows[0]["Remarks"].ToString();
            txtEmployeeCode.Text = dt.Rows[0]["Empid"].ToString();
            frm_time.Text = dt.Rows[0]["FromTime"].ToString();
            To_Time.Text = dt.Rows[0]["ToTime"].ToString();
            txtFromDate.Text = dt.Rows[0]["FromDate"].ToString();
            txtToDate.Text = dt.Rows[0]["ToDate"].ToString();
            txtEmployeeName.Text = dt.Rows[0]["EName"].ToString();
            ddlReasonType.SelectedValue = dt.Rows[0]["ReasonID"].ToString();
            hdnRowID.Value = dt.Rows[0]["RecID"].ToString();
        }
        protected void Btnclear_Click(object sender, EventArgs e)
        {
            mpModifyOutpass.Hide();
        }
    }
}