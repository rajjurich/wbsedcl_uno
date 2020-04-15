using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using System.Reflection;
using Microsoft.Reporting.WebForms;





namespace UNO
{
    public partial class Leave_Data_Upload : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string userid;
        string Rowid1 = "";
        string LeaveAvaibleOnEdit = "";
        DataSet dsUpload;
        DataTable DtUpload;
        DataTable dtCheck;
        DataTable dtEmployeeExistance;
        DataTable dtLeaveFile;
        DataSet dsLVCutUpload;
        DataTable DtLVCutUpload;   
        DataTable dtLVCutUploadError;

        protected void Page_Load(object sender, EventArgs e)
        {
    

            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }


            if (ViewState["Row"] != null)
            {
                Rowid1 = ViewState["Row"].ToString();
            }
            else
            {
                Rowid1 = "0";
            }


            if (ViewState["LeaveAvaibleOnEdit"] != null)
            {
                LeaveAvaibleOnEdit = ViewState["LeaveAvaibleOnEdit"].ToString();
            }


            if (Page.IsPostBack == false)
            {
                bindDataGrid();
                bindCheckTable();
                FillEmployeeEntity();
                FillLeaveCodeEntity();
                lblLeaveAlloted.Visible = false;
                lblLeaveValue.Visible = false;
            }

           // BindGrid();

        }


        void bindCheckTable()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_GET_LEAVE_DETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                dtCheck = new DataTable();
                da.Fill(dtCheck);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvLeaveData.DataSource = dtCheck;
                gvLeaveData.DataBind();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }



        
        void bindEmployeeExistance()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_GET_LEAVE_EMPLOYEEDETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                dtEmployeeExistance = new DataTable();
                da.Fill(dtEmployeeExistance);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                //gvLeaveData.DataSource = dtEmployeeExistance;
                //gvLeaveData.DataBind();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }



        void bindLeaveFile()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_GET_LEAVE_FILE_DETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                dtLeaveFile = new DataTable();
                da.Fill(dtLeaveFile);
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }



        void updateLeavecut(string empid, string leaveid, decimal lvcut)
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand("PROC_UPDATE_LEAVE_DETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@LV_EMPID", empid);
                cmd.Parameters.AddWithValue("@LV_LEAVEID", leaveid);
                cmd.Parameters.AddWithValue("@LV_LVCUT", lvcut);
                cmd.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                throw ex;
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }



        //dtLeaveFile

        void bindDataGrid()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_GET_LEAVE_DETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvLeaveData.DataSource = dt;
                gvLeaveData.DataBind();
                DropDownList ddl = (DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeaveData.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeaveData.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeaveData.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeaveData.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeaveData.PageCount == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex + 1 == gvLeaveData.PageCount)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeaveData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeaveData.PageSize * gvLeaveData.PageIndex) + 1) + " to " + (gvLeaveData.PageSize * (gvLeaveData.PageIndex + 1));

                gvLeaveData.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }

        }


        private DataTable Get_PROC_GET_LEAVEDETAILS_BYROWID(string rowid)
        {
            DataTable dt = null;


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int _result = 0;

            SqlCommand cmd = new SqlCommand("PROC_GET_LEAVEDETAILS_BYROWID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LV_REC_ID", rowid);
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


        void bindEditData()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                DataTable dt = null;

                dt = Get_PROC_GET_LEAVEDETAILS_BYROWID(Rowid1);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvLeaveData.DataSource = dt;
                gvLeaveData.DataBind();
                DropDownList ddl = (DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeaveData.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeaveData.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeaveData.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeaveData.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeaveData.PageCount == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex + 1 == gvLeaveData.PageCount)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeaveData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeaveData.PageSize * gvLeaveData.PageIndex) + 1) + " to " + (gvLeaveData.PageSize * (gvLeaveData.PageIndex + 1));

                gvLeaveData.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }

        }

        protected void gvLeaveData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = e.NewPageIndex;
                bindDataGrid();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = Convert.ToInt32(((DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = gvLeaveData.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }

        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = gvLeaveData.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }

        }


        protected void gvLeaveData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
 
            if (e.CommandName == "Modify")
            {
                string Rowid = e.CommandArgument.ToString();

          
                ViewState["Row"] = Rowid;

                DataTable dt = null;

                dt = Get_PROC_GET_LEAVEDETAILS_BYROWID(Rowid);


                txtEditEmployeeName.Text = dt.Rows[0]["LV_EMP_ID"].ToString();
                txtEditEmployeeName.Enabled = false;

                txtEditLeaveCode.Text = dt.Rows[0]["LV_LEAVE_ID"].ToString();

                txtEditLeaveAllotmentAmount.Text = dt.Rows[0]["LV_ALLOTMENT"].ToString();

                ViewState["LeaveAvaibleOnEdit"] = dt.Rows[0]["LV_ALLOTMENT"].ToString();

                txtEditLeaveAvailableAmount.Text = dt.Rows[0]["LV_AVAILABLE"].ToString();

                txtEditLeaveOpeningBal.Text =  dt.Rows[0]["LV_OPENINGBAL"].ToString();

                txtEditLeaveCut.Text = dt.Rows[0]["LV_CUT"].ToString();

                //txtEditLeaveAvailed.Text = dt.Rows[0]["LV_AVAILED"].ToString();

                //txtEditLeaveEncashed.Text = dt.Rows[0]["LV_ENCASHED"].ToString();



                 mpeModifyZone.Show();
            }


        }

    

        protected void btnModifyCancelLeave_Click(object sender, EventArgs e)
        {
            mpeModifyZone.Hide();

        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {

            lblMessages.Visible = false;
            lblMessages.Text = "";
            mpeAddNewEntry.Show();
            lblLeaveAlloted.Visible = true;
            lblLeaveValue.Visible = false;


        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddZone.Hide();
            lblMessages.Visible = false;
            lblMessages.Text = "";
            lblBulkErrorMessage.Text = "";
            lblBulkErrorMessage.Visible = false;
            lblMessages.Text = string.Empty;
            lblMessages.Visible = false;
            //mpeAddZone.Show();

        }


        protected void btnCancelLeaveCut_Click(object sender, EventArgs e)
        {
            mpeLeaveCut.Hide();
            lblMessages.Visible = false;
            lblMessages.Text = "";
            lblLeaveCutError.Text = "";
            lblLeaveCutError.Visible = false;
            //mpeAddZone.Show();

        }


        protected void btnSubmitNewEntry_Onclick(object sender, EventArgs e)
        {

            string lvID = "0";

            if (Convert.ToDecimal(txtLeaveAllotment.Text) > Convert.ToDecimal(lblLeaveValue.Text))
            {
                lblErrorSingleEntry.Text = "Please enter leave allotment less than maximum value";
                lblErrorSingleEntry.Visible=true;
                mpeAddNewEntry.Show();
                return;
            }
            if (Convert.ToDecimal(txtLeaveAvailable.Text) > (Convert.ToDecimal(txtLeaveOpeningBal.Text) + Convert.ToDecimal(txtLeaveAllotment.Text)))
            {
                lblErrorSingleEntry.Text = "Available leave cannot be more than sum of Balance & Alloted";
                lblErrorSingleEntry.Visible = true;
                mpeAddNewEntry.Show();
                return;
            }
            if (SaveLeave(lvID)) 
            {
                mpeAddNewEntry.Show();

                bindDataGrid();

                ResellAll_NewEntry();
            }
            else
            {
                return;
            }


        }

        protected void btnModifySaveLeave_Click(object sender, EventArgs e)
        {
            
            SaveLeave(Rowid1);
            mpeModifyZone.Hide();
            this.bindDataGrid();

        }


        private bool SaveLeave(string LeaveID)
        {

            string employeeID = string.Empty;
            string leaveCode = string.Empty;
            double leaveAllotment;
            double leaveAvaliable;
            string leaveOpeningBal = string.Empty;
            //string leaveCut = string.Empty;
            ////string leaveAvailed = string.Empty;
            //string leaveEncashed = string.Empty;
            string IsDeleted = "0";
            string lvrecid = LeaveID;

            if (LeaveID == "0")
            {
                employeeID = lstEmployees.SelectedValue;
                leaveCode = lstLeaveType.SelectedValue;
                leaveAllotment = Convert.ToDouble(txtLeaveAllotment.Text);
                leaveAvaliable = Convert.ToDouble(txtLeaveAvailable.Text);
                leaveOpeningBal =  txtLeaveOpeningBal.Text;
                //leaveAvailed = "0.00";
                //leaveEncashed = "0.00";

            }
            else
            {
                employeeID = txtEditEmployeeName.Text;
                leaveCode = txtEditLeaveCode.Text;
                leaveAllotment = Convert.ToDouble(txtEditLeaveAllotmentAmount.Text);
                leaveAvaliable = Convert.ToDouble(txtEditLeaveAvailableAmount.Text);
                leaveOpeningBal = txtEditLeaveOpeningBal.Text;
                //leaveAvailed = "0.00";
                //leaveEncashed = "0.00";
                //leaveCut = null;

            }

            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand objcmd = new SqlCommand("PROC_SAVE_Employee_Leave_Configuration", conn);

                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.Parameters.AddWithValue("@LV_REC_ID", lvrecid);
                objcmd.Parameters.AddWithValue("@LV_EMP_ID", employeeID);
                objcmd.Parameters.AddWithValue("@LV_LEAVE_ID", leaveCode);
                objcmd.Parameters.AddWithValue("@LV_ALLOTMENT", leaveAllotment);
                objcmd.Parameters.AddWithValue("@LV_AVAILABLE", leaveAvaliable);
                //objcmd.Parameters.AddWithValue("@LV_AVAILED", leaveAvailed);
                //objcmd.Parameters.AddWithValue("@LV_ENCASHED", leaveEncashed);
                objcmd.Parameters.AddWithValue("@LV_ISDELETED", IsDeleted);
                objcmd.Parameters.AddWithValue("@LV_OPENINGBAL", leaveOpeningBal);

                //if (txtEditLeaveCut.Text != "")
                //{
                //    objcmd.Parameters.AddWithValue("@LV_LEAVECUT", leaveCut);
                //}

                objcmd.ExecuteNonQuery();

                bindDataGrid();
                ResellAll_NewEntry();

                if(lvrecid.ToString() != "0")
                {
                lblMessages.Visible = true;
                lblMessages.Text = "Leave modified successfully.";
                }
                else
                {
                 lblMessages.Visible = true;
                lblMessages.Text = "Leave configured successfully.";
                }

                return true;

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
               
                lblErrorSingleEntry.Visible = true;
                lblErrorSingleEntry.Text = ex.Message;

                return false;
                
            }
        }

        protected void btnAddCancelEntry_Onclick(object sender, EventArgs e)
        {
            ResellAll_NewEntry();
            mpeAddNewEntry.Hide();
          
            lblMessages.Text = "";
            lblMessages.Visible = false;
        }

       

        protected void btnDelete_Click(object sender, EventArgs e)  //Delete Applied Comp-Off
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            int _result = 0;


            for (int i = 0; i < gvLeaveData.Rows.Count; i++)
            {
                try
                {
                    CheckBox delrows = (CheckBox)gvLeaveData.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {

                        long rowID = Convert.ToInt64(gvLeaveData.Rows[i].Cells[2].Text);
                        SqlCommand cmd = new SqlCommand("PROC_DELETE_LEAVE_DETAILS_BYROWID", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LV_REC_ID", rowID);
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
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
                }

            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            bindDataGrid();
            //ResetAll();
        }


       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                string strsql1 = "";
                DataTable dt1;
                SqlDataAdapter da1;
               

                if (txtSearchEmployeeid.Text.ToString() == "" && txtSearchLeaveCode.Text.ToString() == "")
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    strsql = " SELECT LV_REC_ID,LV_EMP_ID,LV_LEAVE_ID,LV_ALLOTMENT,LV_AVAILABLE,LV_AVAILED,LV_ENCASHED,LV_OPENINGBAL,LV_CUT FROM dbo.TA_LEAVE_SUMMARY  " +
                             " WHERE    LV_ISDELETED = 'false' ";
                    SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandTimeout = 0;
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    gvLeaveData.DataSource = dt;
                    gvLeaveData.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"LV_EMP_ID~" +txtSearchEmployeeid.Text.Trim(), "S" },
				{"LV_LEAVE_ID~" +txtSearchLeaveCode.Text.Trim(), "S" }			
				 };


                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    if (txtSearchEmployeeid.Text.ToString() != "" && txtSearchLeaveCode.Text.ToString() == "")
                    {
                        strsql1 = " SELECT LV_REC_ID,LV_EMP_ID,LV_LEAVE_ID,LV_ALLOTMENT,LV_AVAILABLE,LV_AVAILED,LV_ENCASHED,LV_OPENINGBAL,LV_CUT FROM dbo.TA_LEAVE_SUMMARY " +
                                 " where LV_ISDELETED = 'false' AND LV_EMP_ID Like '" + txtSearchEmployeeid.Text + "%'";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else if (txtSearchLeaveCode.Text.ToString() != "" && txtSearchEmployeeid.Text.ToString() == "")
                    {
                        strsql1 = " SELECT LV_REC_ID,LV_EMP_ID,LV_LEAVE_ID,LV_ALLOTMENT,LV_AVAILABLE,LV_AVAILED,LV_ENCASHED,LV_OPENINGBAL,LV_CUT FROM dbo.TA_LEAVE_SUMMARY " +
                            "  where LV_ISDELETED = 'false' AND LV_LEAVE_ID Like '%" + txtSearchLeaveCode.Text + "%'";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else
                    {
                        strsql1 = "SELECT LV_REC_ID,LV_EMP_ID,LV_LEAVE_ID,LV_ALLOTMENT,LV_AVAILABLE,LV_AVAILED,LV_ENCASHED,LV_OPENINGBAL,LV_CUT FROM dbo.TA_LEAVE_SUMMARY  " +
                           " where LV_ISDELETED = 'false' AND LV_EMP_ID Like '" + txtSearchEmployeeid.Text + "%' AND LV_LEAVE_ID Like '%" + txtSearchLeaveCode.Text + "%' ";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    //DataTable _tempDT = new DataTable();
                    //Search _sc = new Search();
                    //if (_tempDT != null)
                    //{ _tempDT.Rows.Clear(); }
                    //_tempDT = _sc.searchTable(values, dt1);
                    gvLeaveData.DataSource = dt1;
                    gvLeaveData.DataBind();
                }

                DropDownList ddl = (DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeaveData.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeaveData.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeaveData.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeaveData.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeaveData.PageCount == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex + 1 == gvLeaveData.PageCount)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeaveData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeaveData.PageSize * gvLeaveData.PageIndex) + 1) + " to " + (gvLeaveData.PageSize * (gvLeaveData.PageIndex + 1));

                gvLeaveData.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        protected void btnBulkUpload_Click(object sender, EventArgs e)
        {

            mpeAddZone.Show();
            pnlgvLeaveData.Visible = false;
            lblMessages.Visible = false;
            lblMessages.Text = "";
            lblBulkErrorMessage.Text = "";
            lblBulkErrorMessage.Visible = false;
            lblMessages.Text = string.Empty;
            lblMessages.Visible = false;


        }


        protected void btnLvCut_Click(object sender, EventArgs e)
        {
            mpeLeaveCut.Show();

            lblLeaveCutError.Text = "";
            lblLeaveCutError.Visible = false;
            lblMessages.Text = string.Empty;
            lblMessages.Visible = false;
        }


        protected void btnSubmitAdd_OnClick(object sender, EventArgs e)
        {

            

        }


        protected void OnRowDataBound(object sender, EventArgs e)
        {

        }

   
        

//RETRIEVE EXCEL SHEETNAMES
        private String[] GetExcelSheetNames(string excelFile, string path)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;




            try
            {
                // Connection String. Change the excel file to the file you will search.
                String connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;;Data Source={0};Extended Properties=Excel 8.0", path);

                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);

                // Open connection with the database.
                objConn.Open();

                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        protected void btnImportLeaveCut_Click(object sender, EventArgs e)
        {

            if (fileLeaveUpload.HasFile)
            {

                try
                {

                    string extension = Path.GetExtension(fileLeaveUpload.FileName).ToLower();
                    string file = Path.GetFileNameWithoutExtension(fileLeaveUpload.FileName).ToLower();
                    string path = string.Concat(Server.MapPath("~/Uploaded Folder/" + fileLeaveUpload.FileName));
                    string excelConnectionString = "";
                    fileLeaveUpload.SaveAs(path);

                    if (extension == ".xls")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }
                    else if (extension == ".xlsx")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.12.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }

                    else
                    {
                        lblLeaveCutError.Text = "File Format Not Correct.";
                        lblLeaveCutError.Visible = true;
                        return;

                    }

                    OleDbConnection connection = new OleDbConnection();

                    connection.ConnectionString = excelConnectionString;

                    bool CONSTANT_ISDELETED = false;
                    string CONSTANT_LV_REC_ID = string.Empty;

                    string[] sheetName = GetExcelSheetNames(file,path);

                    OleDbCommand command = new OleDbCommand("select * from ["+sheetName[0].ToString()+"]", connection);
                    //OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);

                    OleDbDataAdapter da = new OleDbDataAdapter(command);

                    dsLVCutUpload = new DataSet();

                    da.Fill(dsLVCutUpload);

                    DtLVCutUpload = dsLVCutUpload.Tables[0];

                   


                    for (int i = 0; i < DtLVCutUpload.Rows.Count; i++)
                    {
                        int count = 0;
                        try
                        {
                            


                            if (DtLVCutUpload.Rows[i]["LV_EMP_ID"].ToString() != "" && DtLVCutUpload.Rows[i]["LV_LEAVE_ID"].ToString() != "" && DtLVCutUpload.Rows[i]["LV_CUT"].ToString() != "")
                            {
                                count = count + 1;
                                //updateLeavecut(DtLVCutUpload.Rows[i]["LV_EMP_ID"].ToString(), DtLVCutUpload.Rows[i]["LV_LEAVE_ID"].ToString(), Convert.ToDecimal(DtLVCutUpload.Rows[i]["LV_CUT"].ToString()));
                            }

                        }
                        catch (Exception ex)
                        {


                        }

                        dtLVCutUploadError = new DataTable();
                  
                        DataTable dt1 = dsLVCutUpload.Tables[0];
                     
                        dtLVCutUploadError.Columns.Add("LV_EMP_ID", typeof(string));
                        dtLVCutUploadError.Columns.Add("LV_LEAVE_ID", typeof(string));
                        dtLVCutUploadError.Columns.Add("LV_CUT", typeof(decimal));

                        //for (int j = 0; j < DtLVCutUpload.Rows.Count; j++)
                        //{
                            if (DtLVCutUpload.Rows[i]["LV_EMP_ID"].ToString() == "" || DtLVCutUpload.Rows[i]["LV_LEAVE_ID"].ToString() == "" || DtLVCutUpload.Rows[i]["LV_CUT"].ToString() == "")
                            {
                                    dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            }
                            else
                            {

                                count = count + 1;

                            }
                        //}


                        //for (int k = 0; k < DtLVCutUpload.Rows.Count; k++)
                        //{

                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            SqlCommand objcmd = new SqlCommand();
                            objcmd.Connection = conn;
                            objcmd.CommandText = "SELECT COUNT(*) FROM ENT_EMPLOYEE_PERSONAL_DTLS  WHERE EPD_EMPID ='" + DtLVCutUpload.Rows[i]["LV_EMP_ID"].ToString() + "' and EPD_ISDELETED ='false' ";
                            int cnt = Convert.ToInt32(objcmd.ExecuteScalar());
                            if (cnt == 0)
                            {
                                dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            }
                            else
                            {

                                count = count + 1;

                            }

                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
            

                        //}


                        //for (int l = 0; l < DtLVCutUpload.Rows.Count; l++)
                        //{

                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }

                            SqlCommand objcmd1 = new SqlCommand();
                            objcmd1.Connection = conn;
                            objcmd1.CommandText = "SELECT COUNT(*) FROM TA_LEAVE_SUMMARY  WHERE LV_LEAVE_ID ='" + DtLVCutUpload.Rows[i]["LV_LEAVE_ID"].ToString() + "' and LV_ISDELETED ='false' ";
                            int cnt1 = Convert.ToInt32(objcmd1.ExecuteScalar());
                            if (cnt1 == 0)
                            {
                                dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            }
                            else
                            {

                                count = count + 1;

                            }

                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }

                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }

                            SqlCommand objcmd2 = new SqlCommand();
                            objcmd2.Connection = conn;
                            objcmd2.CommandText = "SELECT COUNT(*) FROM TA_LEAVE_SUMMARY  WHERE  LV_EMP_ID ='" + DtLVCutUpload.Rows[i]["LV_EMP_ID"].ToString() + "' and  LV_LEAVE_ID ='" + DtLVCutUpload.Rows[i]["LV_LEAVE_ID"].ToString() + "' and LV_ISDELETED ='false' ";
                            int cnt2 = Convert.ToInt32(objcmd2.ExecuteScalar());
                            if (cnt2 == 0)
                            {
                                dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            }
                            else
                            {

                                count = count + 1;

                            }

                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }

                        //}


                        //for (int m = 0; m < DtLVCutUpload.Rows.Count; m++)
                        //{
                         try
                            {
                                decimal lvid = Convert.ToDecimal(DtLVCutUpload.Rows[i]["LV_CUT"].ToString());
                                count = count + 1;
                            }
                            catch(Exception ex)
                            {
                                dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            }
                        //}

                         if (count == 6)
                         {
                             updateLeavecut(DtLVCutUpload.Rows[i]["LV_EMP_ID"].ToString(), DtLVCutUpload.Rows[i]["LV_LEAVE_ID"].ToString(), Convert.ToDecimal(DtLVCutUpload.Rows[i]["LV_CUT"].ToString()));
                     
                         }

                    }

                    if (dtLVCutUploadError.Rows.Count > 0)
                    {
                        ShowReport();
                        ReportViewer1.Visible = true;
                        lblMessages.Text = "Error report generated, please rectify and upload again.";
                        lblMessages.Visible = true;
                        bindDataGrid();

                    }
                    else
                    {
                        ReportViewer1.Visible = false;
                        lblMessages.Text = "Records saved sucessfully.";
                        lblMessages.Visible = true;

                        bindDataGrid();

                    }



                }
                catch (Exception ex)
                {
                    //if (lblLeaveCutError.Text == "No data")
                    //{
                        lblLeaveCutError.Text =  "Employee data not present in Leave summary";
                        lblLeaveCutError.Visible = true;
                    //}
                    //Label1.Text = ex.Message;
                    mpeLeaveCut.Show();


                }

            }
            
        }


        protected void btnImport_Click(object sender, EventArgs e)
        {


            if (fileuploadExcel.HasFile)
            {

                try
                {

                    string extension = Path.GetExtension(fileuploadExcel.FileName).ToLower();

                    string path = string.Concat(Server.MapPath("~/Uploaded Folder/" + fileuploadExcel.FileName));
                    string excelConnectionString = "";
                    fileuploadExcel.SaveAs(path);

                    if (extension == ".xls")
                    {
                         excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }
                    else if (extension == ".xlsx")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.12.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }

                    else
                    {
                        lblBulkErrorMessage.Text = "File Format Not Correct.";
                        lblBulkErrorMessage.Visible = true;
                        mpeAddZone.Show();
                        return;

                    }

                    OleDbConnection connection = new OleDbConnection();

                    connection.ConnectionString = excelConnectionString;


                    bool CONSTANT_ISDELETED = false;
                    string  CONSTANT_LV_REC_ID = string.Empty;
                    OleDbCommand command = new OleDbCommand("select *," + CONSTANT_ISDELETED + " as [LV_ISDELETED] from [Sheet1$]", connection);

                    OleDbDataAdapter da = new OleDbDataAdapter(command);



                     dsUpload = new DataSet();

                     da.Fill(dsUpload);

                     DtUpload = dsUpload.Tables[0];

                     ShowReport();

                     if (CheckData())
                     {

                         connection.Open();

                         DbDataReader dr = command.ExecuteReader();

                         string sqlConnectionString = ConfigurationManager.ConnectionStrings["connection_string"].ToString();


                         SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);

                         bulkInsert.DestinationTableName = "TA_LEAVE_SUMMARY";
                        // bulkInsert.ColumnMappings.Add("LV_REC_ID", "LV_REC_ID");
                         bulkInsert.ColumnMappings.Add("LV_EMP_ID","LV_EMP_ID");
                         bulkInsert.ColumnMappings.Add("LV_LEAVE_ID", "LV_LEAVE_ID");
                         bulkInsert.ColumnMappings.Add("LV_OPENINGBAL", "LV_OPENINGBAL");
                         bulkInsert.ColumnMappings.Add("LV_ALLOTMENT", "LV_ALLOTMENT");
                         bulkInsert.ColumnMappings.Add("LV_AVAILABLE", "LV_AVAILABLE");
                         //bulkInsert.ColumnMappings.Add("LV_AVAILED", "LV_AVAILED");
                         //bulkInsert.ColumnMappings.Add("LV_ENCASHED", "LV_ENCASHED");
                         bulkInsert.ColumnMappings.Add("LV_ISDELETED", "LV_ISDELETED");
                         

                         bulkInsert.WriteToServer(dr);


                         lblBulkErrorMessage.Text = "Records saved successfully";
                         lblBulkErrorMessage.Visible = true;

                         //lblMessages.Text = "Records saved successfully";
                         //lblMessages.Visible = true;

                         connection.Close();
                     }
                     else
                     {
                         mpeAddZone.Show();
                         return;
                     }
                }
                catch (Exception ex)
                {

                    //Label1.Text = ex.Message;

                }

            }

            mpeAddZone.Show();

        }



        private bool CheckData()
        {

            this.bindCheckTable();
            this.bindEmployeeExistance();
            this.bindLeaveFile();

            try
            {

                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {

                    if (DtUpload.Rows[i]["LV_EMP_ID"].ToString() == "")
                    {

                        int RowNo = i + 1;
                        lblBulkErrorMessage.Text =  "Please enter Employee ID in row " + RowNo;
                        lblBulkErrorMessage.Visible = true;

                        return false;

                    }

                }



                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {

                    if (DtUpload.Rows[i]["LV_LEAVE_ID"].ToString() == "")
                    {

                        int RowNo = i + 1;

                        lblBulkErrorMessage.Text = "Please enter valid Leave ID in row " + RowNo;
                        lblBulkErrorMessage.Visible = true;


                        return false;

                    }

                }


                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {

                    if (DtUpload.Rows[i]["LV_ALLOTMENT"].ToString() == "")
                    {

                        int RowNo = i + 1;

                        lblBulkErrorMessage.Text = "Please enter valid leave allotment in row " + RowNo;
                        lblBulkErrorMessage.Visible = true;

                        return false;

                    }


                    if (DtUpload.Rows[i]["LV_ALLOTMENT"].ToString() != "")
                    {
                        try
                        {
                            decimal empid = Convert.ToDecimal(DtUpload.Rows[i]["LV_ALLOTMENT"].ToString());

                        }
                        catch (Exception ex)
                        {
                            int RowNo = i + 1;
                            lblBulkErrorMessage.Text = "Please enter valid leave allotment in row " + RowNo;
                            lblBulkErrorMessage.Visible = true;

                            return false;
                        }

                    }

                }

                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {

                    if (DtUpload.Rows[i]["LV_AVAILABLE"].ToString() == "")
                    {

                        int RowNo = i + 1;

                        lblBulkErrorMessage.Text = "Please enter valid leave available in row " + RowNo;
                        lblBulkErrorMessage.Visible = true;


                        return false;

                    }


                    if (DtUpload.Rows[i]["LV_AVAILABLE"].ToString() != "")
                    {
                        try
                        {
                            decimal empid = Convert.ToDecimal(DtUpload.Rows[i]["LV_AVAILABLE"].ToString());

                        }
                        catch (Exception ex)
                        {
                            int RowNo = i + 1;
                            lblBulkErrorMessage.Text = "Please enter valid leave available in row " + RowNo;
                            lblBulkErrorMessage.Visible = true;

                            return false;
                        }

                    }

                }


                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {

                    if (DtUpload.Rows[i]["LV_OPENINGBAL"].ToString() == "")
                    {

                        int RowNo = i + 1;

                        lblBulkErrorMessage.Text = "Please enter valid leave opening balance in row " + RowNo;
                        lblBulkErrorMessage.Visible = true;

                        return false;

                    }


                    if (DtUpload.Rows[i]["LV_OPENINGBAL"].ToString() != "")
                    {
                        try
                        {
                            decimal empid = Convert.ToDecimal(DtUpload.Rows[i]["LV_OPENINGBAL"].ToString());

                        }
                        catch (Exception ex)
                        {
                            int RowNo = i + 1;
                            lblBulkErrorMessage.Text = "Please enter valid leave opening balance in row " + RowNo;
                            lblBulkErrorMessage.Visible = true;

                            return false;
                        }

                    }


                }



                //for (int i = 0; i < DtUpload.Rows.Count; i++)
                //{

                //    if (DtUpload.Rows[i]["LV_ENCASHED"].ToString() == "")
                //    {

                //        int RowNo = i + 1;

                //        lblBulkErrorMessage.Text = "Please enter valid leave encased in row " + RowNo;
                //        lblBulkErrorMessage.Visible = true;
                //        return false;

                //    }


                //    if (DtUpload.Rows[i]["LV_ENCASHED"].ToString() != "")
                //    {
                //        try
                //        {
                //            decimal empid = Convert.ToDecimal(DtUpload.Rows[i]["LV_ENCASHED"].ToString());

                //        }
                //        catch (Exception ex)
                //        {
                //            int RowNo = i + 1;
                //            lblBulkErrorMessage.Text = "Please enter valid leave encashed in row " + RowNo;
                //            lblBulkErrorMessage.Visible = true;

                //            return false;
                //        }

                //    }

                //}


                
                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {

                    for (int j = 0; j < dtCheck.Rows.Count; j++)
                    {

                        if (DtUpload.Rows[i]["LV_EMP_ID"].ToString().Trim() == dtCheck.Rows[j]["LV_EMP_ID"].ToString().Trim()
                            && DtUpload.Rows[i]["LV_LEAVE_ID"].ToString().Trim() == dtCheck.Rows[j]["LV_LEAVE_ID"].ToString().Trim())
                        {

                            int RowNo = i + 1;

                            lblBulkErrorMessage.Text = "Duplicate records of Employee ID   " 
                                                       + DtUpload.Rows[i]["LV_EMP_ID"].ToString() + "  for Leave Code  "
                                                       + DtUpload.Rows[i]["LV_LEAVE_ID"].ToString().Trim() +
                                                        " at Row No " + RowNo;
                            lblBulkErrorMessage.Visible = true;

                            return false;

                        }
                    }

                }

                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {
                    int chk = 0;

                    int RowNo = i + 1;

                    for (int j = 0; j < dtEmployeeExistance.Rows.Count; j++)
                    {

                        if (DtUpload.Rows[i]["LV_EMP_ID"].ToString().Trim() == dtEmployeeExistance.Rows[j]["EPD_EMPID"].ToString().Trim())
                        {

                            chk = 1;

                        }
                    }

                    if (chk != 1)
                    {
                        lblBulkErrorMessage.Text = "Employee ID " + DtUpload.Rows[i]["LV_EMP_ID"].ToString() 
                                                    + "is not a valid Employee ID" +
                                                   " at Row No " + RowNo;
                        lblBulkErrorMessage.Visible = true;

                        return false;
                    }

                }

                for (int i = 0; i < DtUpload.Rows.Count; i++)
                {
                    int chk = 0;

                    int RowNo = i + 1;

                    for (int j = 0; j < dtLeaveFile.Rows.Count; j++)
                    {

                        if (DtUpload.Rows[i]["LV_LEAVE_ID"].ToString().Trim() == dtLeaveFile.Rows[j]["Leave_ID"].ToString().Trim())
                        {

                            chk = 1;

                        }
                    }

                    if (chk != 1)
                    {
                        lblBulkErrorMessage.Text = "Leave ID " + DtUpload.Rows[i]["LV_LEAVE_ID"].ToString() 
                                                    + "is not a valid Leave ID" +
                                                   " at Row No " + RowNo;
                        lblBulkErrorMessage.Visible = true;

                        return false;
                    }

                }

            }

            catch (Exception ex)
            {
                lblBulkErrorMessage.Text = ex.Message;

                return false;
            }

            return true;

        }


      
        private void FillEmployeeEntity()
        {
            try
            {
                string strSql = "";

                strSql = " SELECT EPD_EMPID as ID ,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
                         " FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  "+
                         " on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1' ";
                //strSql = " select Left(EPD_FIRST_NAME + space(28),30) + epd_empid as Name ,epd_empid as ID from ENT_EMPLOYEE_PERSONAL_DTLS where epd_isdeleted='0' ";


                //strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+ oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0'";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                lstEmployees.DataValueField = "ID";
                lstEmployees.DataTextField = "NAME";

                lstEmployees.DataSource = thisDataSet.Tables[0];

                lstEmployees.DataBind();
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }


        private void FillLeaveCodeEntity()
        {
            try
            {
                string strSql = "";

                strSql = "SELECT Leave_ID AS ID,Leave_Description AS NAME FROM dbo.TA_Leave_File WHERE  Leave_ISDELETED =  0";
                //strSql = " select Left(EPD_FIRST_NAME + space(28),30) + epd_empid as Name ,epd_empid as ID from ENT_EMPLOYEE_PERSONAL_DTLS where epd_isdeleted='0' ";


                //strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+ oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0'";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                lstLeaveType.DataValueField = "ID";
                lstLeaveType.DataTextField = "NAME";

                lstLeaveType.DataSource = thisDataSet.Tables[0];

                lstLeaveType.DataBind();
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }


        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string XlsPath = Server.MapPath(@"~/Uploaded Folder/uploadformat.xls");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(fileDet.FullName);
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        protected void btnDownloadLeaveCutExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string XlsPath = Server.MapPath(@"~/Uploaded Folder/uploadLeaveCutformat.xls");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(fileDet.FullName);
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        private void ResellAll_NewEntry()
        {
            FillEmployeeEntity();
            FillLeaveCodeEntity();
            txtLeaveOpeningBal.Text = string.Empty;
            txtLeaveAllotment.Text = string.Empty;
            txtLeaveAvailable.Text = string.Empty;
            lblErrorSingleEntry.Visible = false;
            lblErrorSingleEntry.Text = string.Empty;
            lblMessages.Text = string.Empty;
            lblMessages.Visible = false;
            
        }


        private void ShowReport()
        {
            try
            {

                ReportViewer1.LocalReport.ReportPath = "RDLC\\LV_CUT_REPORT.rdlc";
                ReportViewer1.Visible = true;


                String commandText = "SELECT '1' AS LV_EMP_ID,LV_LEAVE_ID,LV_OPENINGBAL,LV_ALLOTMENT,LV_AVAILABLE FROM TA_LEAVE_SUMMARY";

                String commandType = "Text";

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME

                DataSet dsLvcut = new DataSet();

                dsLvcut.Tables.Add(dtLVCutUploadError);

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dsLvcut.Tables[0];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();

               
            }

            catch (Exception ex)
            {

            }
        }

        private DataSet ExecuteQuery(string strQuery, string dataSetName, string tableName)
        {

            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = m_connectons;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = strQuery;
                DataSet dataSet = new DataSet(dataSetName);
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dataSet, tableName);
                return dataSet;
            }

            catch (Exception ex)
            {
                return null;
            }

        }

        private bool leaveData()
        {
            //string query = " select * from (select L.LR_CODE, L.LR_CATEGORYID ,L.LR_ALLOTMENT,L.LR_ACCUMULATION,E.EOD_EMPID,s.LV_LEAVE_ID, " +
            //              " s.LV_OPENINGBAL from ENT_EMPLOYEE_OFFICIAL_DTLS E " +
            //              " INNER JOIN ENT_CATEGORY C ON E.EOD_CATEGORY_ID=C.CAT_CATEGORY_ID " +
            //              " INNER JOIN TA_LEAVE_RULE_NEW L  ON C.CAT_CATEGORY_ID=L.LR_CATEGORYID " +
            //              " inner join TA_LEAVE_SUMMARY s on e.EOD_EMPID=s.LV_EMP_ID " +
            //              " WHERE L.LR_ISDELETED='0' " +
            //              " AND E.EOD_EMPID='" + lstEmployees.SelectedValue.ToString() + "' )a where LV_LEAVE_ID='" + lstLeaveType.SelectedValue.ToString() + "' ";

            string query = " select * from (select L.LR_CODE, L.LR_CATEGORYID ,L.LR_ALLOTMENT,L.LR_ACCUMULATION,E.EOD_EMPID,  l.leaveID as LV_LEAVE_ID " +
                //,s.LV_LEAVE_ID, " +" s.LV_OPENINGBAL 
                             "   from ENT_EMPLOYEE_OFFICIAL_DTLS E " +
                          " INNER JOIN ENT_CATEGORY C ON E.EOD_CATEGORY_ID=C.CAT_CATEGORY_ID " +
                          " INNER JOIN TA_LEAVE_RULE_NEW L  ON C.CAT_CATEGORY_ID=L.LR_CATEGORYID " +
                //" inner join TA_LEAVE_SUMMARY s on e.EOD_EMPID=s.LV_EMP_ID " +
                          " WHERE L.LR_ISDELETED='0' " +
                          " AND E.EOD_EMPID='" + lstEmployees.SelectedValue.ToString() + "' )a where LV_LEAVE_ID='" + lstLeaveType.SelectedValue.ToString() + "' ";
            
            
            SqlDataAdapter da =new SqlDataAdapter(query,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        lblLeaveValue.Text = dt.Rows[0]["LR_ALLOTMENT"].ToString();
        //        if (dt.Rows[0]["LV_OPENINGBAL"].ToString() != "")
        //        {
        //            txtLeaveOpeningBal.Text = dt.Rows[0]["LV_OPENINGBAL"].ToString();
        //        }
        //        else
        //        {
        //            txtLeaveOpeningBal.Text = "NA";
        //        }
        //        txtLeaveOpeningBal.Enabled=false;
        //        lblLeaveAlloted.Visible = true;
        //        lblLeaveValue.Visible = true;
        //    }
        //    else 
        //    { 

        //    }
        //}

        //protected void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    leaveData();
        //    mpeAddNewEntry.Show();
        //}

        //protected void lstLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    leaveData();
        //    mpeAddNewEntry.Show();
        //}
            if (dt.Rows.Count > 0)
            {
                lblLeaveValue.Text = dt.Rows[0]["LR_ALLOTMENT"].ToString();

                lblLeaveAlloted.Visible = true;
                lblLeaveValue.Visible = true;
                lblErrorSingleEntry.Visible = false;
                btnSubmitNewEntry.Enabled = true;
                return true;
            }
            else
            {
                btnSubmitNewEntry.Enabled = false;
                return false;
            }
        }

        protected void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            leaveData();
            mpeAddNewEntry.Show();
        }

        protected void lstLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!leaveData())
            {
                lblLeaveValue.Text = "N.A";

                //lblErrorSingleEntry.Text = "No Leave has been alloted to this Category";
                //lblErrorSingleEntry.Visible = true;
                lblLeaveValue.Visible = true;
            }
            mpeAddNewEntry.Show();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearchEmployeeid.Text = "";
            txtSearchLeaveCode.Text = "";
            bindDataGrid();
        }




    }

}