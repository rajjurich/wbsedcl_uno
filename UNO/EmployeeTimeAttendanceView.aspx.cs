using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UNO
{
    public partial class EmployeeTimeAttendanceView : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string TimeAttenddeletedate;
        void bindDataGrid()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strsql = "select etc_rowid as rowid, etc_emp_id, EPD_FIRST_NAME +' '+ EPD_LAST_NAME as Name , etc_minimum_swipe,etc_shiftcode,etc_weekend, " +
                                " etc_weekoff,convert(varchar,etc_shift_start_date,103) as etc_shift_start_date  " +
                                " from TNA_EMPLOYEE_TA_CONFIG conf inner join ENT_EMPLOYEE_PERSONAL_DTLS emp " +
                                " on conf.ETC_EMP_ID=emp.EPD_EMPID inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop " +
                                " on emp.EPD_EMPID=eop.EOD_EMPID where ETC_ISDELETED='0' and emp.EPD_ISDELETED='0' and eop.EOD_ACTIVE='1' ";

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvTimeAttendanceView.DataSource = dt;
                gvTimeAttendanceView.DataBind();
                DropDownList ddl = (DropDownList)gvTimeAttendanceView.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvTimeAttendanceView.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvTimeAttendanceView.PageIndex + 1).ToString();
                Label lblcount = (Label)gvTimeAttendanceView.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvTimeAttendanceView.DataSource).Rows.Count.ToString() + " Records.";
                if (gvTimeAttendanceView.PageCount == 0)
                {
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimeAttendanceView.PageIndex + 1 == gvTimeAttendanceView.PageCount)
                {
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimeAttendanceView.PageIndex == 0)
                {
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
              

                ((Label)gvTimeAttendanceView.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTimeAttendanceView.PageSize * gvTimeAttendanceView.PageIndex) + 1) + " to " + (((gvTimeAttendanceView.PageSize * (gvTimeAttendanceView.PageIndex + 1)) - 10) + gvTimeAttendanceView.Rows.Count);

                gvTimeAttendanceView.BottomPagerRow.Visible = true;


                if (dt.Rows.Count != 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }

                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                fillweeklyOff();
                fillweeklyEnd();
                bindshifttype();
            }

        }
        protected void gvTimeAttendanceView_DataBound(object sender, EventArgs e)
        {
            /*
            string strsql = "select etc_rowid as rowid,etc_emp_id,etc_minimum_swipe,etc_shiftcode,etc_weekend,etc_weekoff,convert(varchar,etc_shift_start_date,103) as etc_shift_start_date  from TNA_EMPLOYEE_TA_CONFIG where ETC_ISDELETED='0'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connections);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcount = dt.Rows.Count;
            if (dt.Rows.Count != 0)
            {
                pager.Visible = true;
                if (ViewState["PageSize"] != null)
                {
                    DropDownList ddPagesize = gvTimeAttendanceView.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                }

                Label lblCount = gvTimeAttendanceView.BottomPagerRow.FindControl("lblPageCount") as Label;
                int totRecords = (gvTimeAttendanceView.PageIndex * gvTimeAttendanceView.PageSize) + gvTimeAttendanceView.PageSize;
                //int totCustomerCount = AdvWorksDB.GetCustomersCount(hfSearchCriteria.Value);
                int totCustomerCount = recordcount;
                totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
                lblPageCount.Text = ((gvTimeAttendanceView.PageIndex * gvTimeAttendanceView.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) ;
                gvTimeAttendanceView.BottomPagerRow.Visible = true;
            }

            */

        }
        protected void gvTimeAttendanceView_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void gvTimeAttendanceView_PageIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvTimeAttendanceView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTimeAttendanceView.PageIndex = e.NewPageIndex;
            bindDataGrid();
        }
        public void fillweeklyOff()
        {
            string strSql = "select mwk_cd from dbo.TA_WKLYOFF where MWK_OFF=1";



            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ddweekoff.DataSource = thisDataSet;
            ddweekoff.DataValueField = "mwk_cd";
            ddweekoff.DataTextField = "mwk_cd";
            ddweekoff.DataBind();


        }
        public void fillweeklyEnd()
        {
            string strSql = "select mwk_cd from dbo.TA_WKLYOFF where MWK_OFF=0";
            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ddWeekend.DataSource = thisDataSet;
            ddWeekend.DataValueField = "mwk_cd";
            ddWeekend.DataTextField = "mwk_cd";
            ddWeekend.DataBind();


        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeTimeAttendanceADD.aspx");
        }
        public void fillPopUpDetails(string rowid)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sql = "select ETC_EMP_ID,ETC_MINIMUM_SWIPE,ETC_SHIFTCODE,ETC_WEEKEND,ETC_WEEKOFF,ETC_SHIFT_START_DATE,ScheduleType,ShiftType from TNA_EMPLOYEE_TA_CONFIG where ETC_ISDELETED='0' and  ETC_ROWID='" + rowid + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                txtMinSwipe.Text = dt.Rows[0]["ETC_MINIMUM_SWIPE"].ToString();
                //txtShiftCode.Text = dt.Rows[0]["ETC_SHIFTCODE"].ToString();
                ddWeekend.SelectedValue = dt.Rows[0]["ETC_WEEKEND"].ToString();
                ddweekoff.SelectedValue = dt.Rows[0]["ETC_WEEKOFF"].ToString();
                txtshiftStartDate.Text = Convert.ToDateTime(dt.Rows[0]["ETC_SHIFT_START_DATE"]).ToString("dd/MM/yyyy");
                rblScheduleType.SelectedValue = dt.Rows[0]["ScheduleType"].ToString();
                if (rblScheduleType.SelectedValue == "Pattern")
                {
                    ddlShiftPattern.SelectedValue = dt.Rows[0]["ShiftType"].ToString();
                    ddlShiftPattern.Attributes.Add("style", "display:block");
                    ddlShift.Attributes.Add("style", "display:none");
                }
                else
                {
                    ddlShift.SelectedValue = dt.Rows[0]["ShiftType"].ToString();
                    ddlShift.Attributes.Add("style", "display:block");
                    ddlShiftPattern.Attributes.Add("style", "display:none");
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }

        }
        protected void gvTimeAttendanceView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit3")
                {
                    string rowid = e.CommandArgument.ToString();
                    //Session["rowid"] = rowid;
                    hdnRowID.Value = rowid;
                    bindshifttype();
                    fillPopUpDetails(rowid);
                    lblMessages.Text = "";
                    pops_modalTimeExtender.Show();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string Shift = rblScheduleType.SelectedValue == "Pattern" ? ddlShiftPattern.SelectedValue : ddlShift.SelectedValue.ToString();
                    cmd.CommandText = "update TNA_EMPLOYEE_TA_CONFIG set etc_minimum_swipe='" + txtMinSwipe.Text + "' , etc_shiftcode='" + Shift + "' , ScheduleType='" + rblScheduleType.SelectedValue + "', ShiftType='" + Shift + "',etc_weekend='" + ddWeekend.SelectedValue + "',etc_weekoff='" + ddweekoff.SelectedValue + "',etc_shift_start_date=convert(datetime,'" + txtshiftStartDate.Text + "', 103) where etc_rowid='" + hdnRowID.Value + "'";
                    cmd.ExecuteNonQuery();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    bindDataGrid();
                    pops_modalTimeExtender.Hide();
                    lblMessages.Text = "Record updated successfully.";
                    lblMessages.Visible = true;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }


        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            bool Check = false;
            string SelStr = "";
            for (int i = 0; i < gvTimeAttendanceView.Rows.Count; i++)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;
                //  SqlTransaction trans;
                // trans = conn.BeginTransaction();
                try
                {
                    // objcmd.Transaction = trans;
                    CheckBox delrows = (CheckBox)gvTimeAttendanceView.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        string Empcde = gvTimeAttendanceView.Rows[i].Cells[2].Text;

                        SelStr = "select TDAY_EMPCDE from TDAY where " +
                                 " TDAY_EMPCDE='" + Empcde + "'";
                        SqlDataAdapter da1 = new SqlDataAdapter(SelStr, conn);
                        da1.SelectCommand.CommandTimeout = 0;
                        DataTable dt = new DataTable();
                        da1.Fill(dt);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        int recordcnt = dt.Rows.Count;
                        if (recordcnt != 0)
                        {


                            //messageDiv.InnerHtml = "This Employee " + Empcde + " can not be deleted since it is already in use.";
                            lblMessages.Text = "This Employee " + Empcde + " can not be deleted since it is already in use.";
                            lblMessages.Visible = true;
                            //Response.Write("<script language='javascript'>alert('select Records.');</script>");
                            return;
                        }
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        Check = true;
                        objcmd.CommandText = "update TNA_EMPLOYEE_TA_CONFIG set etc_isdeleted='1' ,etc_deleteddate = GETDATE() where ETC_EMP_ID='" + gvTimeAttendanceView.Rows[i].Cells[2].Text + "'";
                        objcmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        //  trans.Commit();
                        //Response.Write("<script language='javascript'>alert('Controller deleted Successfully.');</script>");
                    }

                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
                    //  trans.Rollback();
                    // throw ex;
                }


            }
            if (Check == false)
            {
                lblMessages.Text = "Please select record to delete.";
                lblMessages.Visible = true;
            }
            if (Check == true)
            {
                // messageDiv.InnerHtml = "Records Deleted Successfully";
                lblMessages.Text = "Records Deleted Successfully";
                lblMessages.Visible = true;
                bindDataGrid();

                //string someScript = "";
                //someScript = "<script language='javascript'>setTimeout(\"clearFunction('LblMsg')\",2000);</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);




                return;
            }


            bindDataGrid();

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvTimeAttendanceView.PageIndex = Convert.ToInt32(((DropDownList)gvTimeAttendanceView.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvTimeAttendanceView.PageIndex = gvTimeAttendanceView.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvTimeAttendanceView.PageIndex = gvTimeAttendanceView.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strsql = "select etc_rowid as rowid, etc_emp_id, EPD_FIRST_NAME +' '+ EPD_LAST_NAME as Name , etc_minimum_swipe,etc_shiftcode,etc_weekend, " +
                                " etc_weekoff,convert(varchar,etc_shift_start_date,103) as etc_shift_start_date  " +
                                " from TNA_EMPLOYEE_TA_CONFIG conf inner join ENT_EMPLOYEE_PERSONAL_DTLS emp " +
                                " on conf.ETC_EMP_ID=emp.EPD_EMPID inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop " +
                                " on emp.EPD_EMPID=eop.EOD_EMPID where ETC_ISDELETED='0' and emp.EPD_ISDELETED='0' and eop.EOD_ACTIVE='1' ";

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (textempid.Text.ToString() == "" && textshiftid.Text.ToString() == "")
                {
                    bindDataGrid();
                }
                else
                {
                    String[,] values = { 
              {"etc_emp_id~" +textempid.Text.Trim(), "S" },
				{"etc_shiftcode~" +textshiftid.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvTimeAttendanceView.DataSource = _tempDT;
                    gvTimeAttendanceView.DataBind();

                }
                DropDownList ddl = (DropDownList)gvTimeAttendanceView.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvTimeAttendanceView.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvTimeAttendanceView.PageIndex + 1).ToString();
                Label lblcount = (Label)gvTimeAttendanceView.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvTimeAttendanceView.DataSource).Rows.Count.ToString() + " Records.";
                if (gvTimeAttendanceView.PageCount == 0)
                {
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimeAttendanceView.PageIndex + 1 == gvTimeAttendanceView.PageCount)
                {
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimeAttendanceView.PageIndex == 0)
                {
                    ((Button)gvTimeAttendanceView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvTimeAttendanceView.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTimeAttendanceView.PageSize * gvTimeAttendanceView.PageIndex) + 1) + " to " + (gvTimeAttendanceView.PageSize * (gvTimeAttendanceView.PageIndex + 1));
                ((Label)gvTimeAttendanceView.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTimeAttendanceView.PageSize * gvTimeAttendanceView.PageIndex) + 1) + " to " + (((gvTimeAttendanceView.PageSize * (gvTimeAttendanceView.PageIndex + 1)) - 10) + gvTimeAttendanceView.Rows.Count);
                gvTimeAttendanceView.BottomPagerRow.Visible = true;
            }


            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendanceView");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pops_modalTimeExtender.Hide();
        }
        protected void rblScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindshifttype();
            pops_modalTimeExtender.Show();

        }
        private void bindshifttype()
        {

            string strSql = "";

            strSql = "SELECT SHIFT_ID,SHIFT_DESCRIPTION FROM dbo.TA_SHIFT WHERE SHIFT_ISDELETED = '0'";

            SqlCommand cmd;
            SqlDataAdapter adpt;
            DataTable thisDataSet;
            cmd = new SqlCommand(strSql, conn);
            adpt = new SqlDataAdapter(cmd);
            thisDataSet = new DataTable();
            adpt.Fill(thisDataSet);

            ddlShift.DataValueField = "SHIFT_ID";
            ddlShift.DataTextField = "SHIFT_DESCRIPTION";
            ddlShift.DataSource = thisDataSet;
            ddlShift.DataBind();

            string strSql1 = "";
            strSql1 = "SELECT SHIFT_PATTERN_ID,SHIFT_PATTERN_DESCRIPTION + ' - ' + CASE WHEN SHIFT_PATTERN_TYPE = 'WK' THEN 'WEEKLY'  WHEN SHIFT_PATTERN_TYPE = 'DL' THEN 'DAILY'" +
                     "WHEN  SHIFT_PATTERN_TYPE = 'MN' THEN 'MONTHLY' " +
                     "WHEN SHIFT_PATTERN_TYPE = 'BW' THEN 'BI-WEEKLY' END AS SHIFT_PATTERN_DESCRIPTION " +
                     "FROM TA_SHIFT_PATTERN WHERE SHIFT_ISDELETED = '0'";

            cmd = new SqlCommand(strSql1, conn);
            adpt = new SqlDataAdapter(cmd);
            thisDataSet = new DataTable();
            adpt.Fill(thisDataSet);

            ddlShiftPattern.DataValueField = "SHIFT_PATTERN_ID";
            ddlShiftPattern.DataTextField = "SHIFT_PATTERN_DESCRIPTION";
            ddlShiftPattern.DataSource = thisDataSet;
            ddlShiftPattern.DataBind();

        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            textshiftid.Text = "";
            textempid.Text = "";
            bindDataGrid();
        }


    }
}
