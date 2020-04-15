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
    public partial class VMS_BlackList : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public string EmpCode = "";
        static string path = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                EmpCode = Session["uid"].ToString();
            }
            if (!IsPostBack)
            {
                BindGridView();
                Button4.Attributes.Add("onclick", "javascript:return handleDelete('" + gvBlackList.ClientID + "');");
            }
            cetxtFrmDate.StartDate = System.DateTime.Now;
            CalendarExtender3.StartDate = System.DateTime.Now;
            CalendarExtender1.StartDate = System.DateTime.Now;
            CalendarExtender2.StartDate = System.DateTime.Now;
            


        }
        private void BindGridView()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "View";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ViewState["dataTable"] = dt;
                string expression = "";

                expression = "BlackListed = 'BlackListed'";


                if (expression != "")
                {
                    try
                    {
                        dt = dt.Select(expression).CopyToDataTable();
                        dt.AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        dt = null;

                    }
                }
                if (txtVisitorId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "" && txtMobileNumber.Text.ToString() == "")
                {
                    gvBlackList.DataSource = dt;
                    gvBlackList.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"AllVisitorID~" +txtVisitorId.Text.Trim(), "S" },
				{"VisitorCompany~" +txtCompanyName.Text.Trim(), "S" },
		        {"mobileNo~" +txtMobileNumber.Text.Trim(), "S" }	
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvBlackList.DataSource = _tempDT;
                    gvBlackList.DataBind();
                }
                if (gvBlackList.Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)gvBlackList.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvBlackList.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvBlackList.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvBlackList.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvBlackList.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvBlackList.PageCount == 0)
                    {
                        ((Button)gvBlackList.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvBlackList.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvBlackList.PageIndex + 1 == gvBlackList.PageCount)
                    {
                        ((Button)gvBlackList.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvBlackList.PageIndex == 0)
                    {
                        ((Button)gvBlackList.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvBlackList.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvBlackList.PageSize * gvBlackList.PageIndex) + 1) + " to " + (gvBlackList.PageSize * (gvBlackList.PageIndex + 1));

                    gvBlackList.BottomPagerRow.Visible = true;
                }

            }
            catch (Exception ex) { Message(true, ex.Message); }
            finally { conn.Close(); }

        }
        public void BinGvpopUp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "View";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string expression = "";
                expression = "BlackListed = ''";

                if (expression != "")
                {
                    try
                    {
                        dt = dt.Select(expression).CopyToDataTable();
                        dt.AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        dt = null;

                    }
                    gvVisitorPopup.DataSource = dt;
                    gvVisitorPopup.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Message(false, "");
            BindGridView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtVisitorId.Text = "";
            txtCompanyName.Text = "";
            txtMobileNumber.Text = "";
            gvBlackList.SelectedIndex = 0;
            Message(false, "");
            BindGridView();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "InsertBlackList";
                cmd.Parameters.Add("@Visitor_ID", SqlDbType.NVarChar).Value = txtVisitorIDpopup.Text.Trim();
                cmd.Parameters.Add("@Blacklist_Reason", SqlDbType.NVarChar).Value = txtReason.Text.Trim();
                cmd.Parameters.Add("@blacklisted_from_date", SqlDbType.NVarChar).Value = txtFrmDate.Text.Trim();
                cmd.Parameters.Add("@blackListed_To_date", SqlDbType.NVarChar).Value = txtTDate.Text.Trim();
                cmd.Parameters.Add("@BlackListed_by", SqlDbType.NVarChar).Value = EmpCode;
                cmd.Parameters.AddWithValue("@URL", path);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Message(true, "Record saved Successfully");
                    BindGridView();
                    BinGvpopUp();
                    mpeNewNonBlacklisted.Hide();
                }
                else
                {
                    Message(true, "Server error");
                }

                //ScriptManager.RegisterStartupScript(Page, GetType(), "MyScript", "alert('Sucessfully Saved.');", true);
            }

            catch (Exception ex) { Message(true, ex.Message); }
            finally { conn.Close(); mpeBlackList.Hide(); }

           
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            sectionGV3.Visible = true;
            Section1.Visible = false;
            table4.Visible = false;
            Tr7.Visible = true;
            mpeBlackList.Hide();
        }
        protected void gvBlackList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          
            try
            {
                string[] commandArgs;
                if (e.CommandName == "Modify")
                {
                     commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string visitorId = commandArgs[0];
                    string visitorName = commandArgs[1];
                    string Rowid = commandArgs[2];

                    txtVisitorIdEdit.Text = visitorId;
                    txtVisitorNameEdit.Text = visitorName;

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT  convert(VARCHAR(10),blacklisted_from_date,103) AS fromdate,convert(VARCHAR(10),blackListed_To_date,103) AS ToDate,Blacklist_Reason FROM Visitor_BlackListed WHERE coalesce(Isdeleted,0)=0 and convert(VARCHAR(10),blackListed_To_date,103) >= convert(VARCHAR(10),getdate(),103) AND Rowid='" + Rowid + "'", conn);
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dap.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtFromdateEdit.Text = dt.Rows[0]["fromdate"].ToString();
                        txtToDateEdit.Text = dt.Rows[0]["ToDate"].ToString();
                        txtReasonEdit.Text = dt.Rows[0]["Blacklist_Reason"].ToString();
                        hdnRowID.Value = Rowid;                    
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    mpeNewNonBlacklistedEdit.Show();
                }
                else if (e.CommandName == "NonBlackList")
                {
                    commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });                  
                    string Rowid = commandArgs[1];
                    hdnRowID.Value = Rowid;
                    ViewState["VisitorIdNonBlackList"] = e.CommandArgument.ToString();
                    hdnRowID.Value = Rowid;      
                    mpeBlackList.Show();
                }
                else if (e.CommandName == "View")
                {
                    GetBlackListedDetails(e.CommandArgument.ToString(), "");
                    mpeView.Show();
                }
                else if (e.CommandName == "BlackList")
                {
                    sectionGV3.Visible = false;
                    Section1.Visible = true;
            

                }
            }
            catch (Exception ex)
            {
                Message(true, ex.Message);
            }
        }
        protected void GetBlackListedDetails(string VisitorId, string New)
        {
            if (New == "New")
            {
                try
                {
                    DataTable dt1 = (DataTable)ViewState["dataTable"];
                    string expression = "AllVisitorID = '" + VisitorId + "' and BlackListed = 'BlackListed'";
                    dt1 = dt1.Select(expression).CopyToDataTable();
                    dt1.AcceptChanges();
                    if (dt1.Rows.Count > 0)
                    {
                        txtFrmDate.Text = Convert.ToString(dt1.Rows[0]["blacklisted_from_date"]);
                        txtTDate.Text = Convert.ToString(dt1.Rows[0]["blackListed_To_date"]);
                        txtReason.Text = Convert.ToString(dt1.Rows[0]["Blacklist_Reason"]);
                    }

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "ViewBlackList";
                cmd.Parameters.Add("@Visitor_ID", SqlDbType.NVarChar).Value = VisitorId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    lblVId.Text = Convert.ToString(dt.Rows[0]["AllVisitorID"]);
                    lblVName.Text = Convert.ToString(dt.Rows[0]["Visitor_Name"]);
                    lblVDesig.Text = Convert.ToString(dt.Rows[0]["Designation"]);
                    lblVCompany.Text = Convert.ToString(dt.Rows[0]["VisitorCompany"]);
                    gvPopUp.DataSource = dt;
                    gvPopUp.DataBind();
                }
            }




        }
        private void Message(bool Value, string msg)
        {
            lblMessages.Visible = Value;
            lblMessages.Text = msg;
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                Message(false, "");
                gvBlackList.PageIndex = Convert.ToInt32(((DropDownList)gvBlackList.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindGridView();
            }
            catch (Exception ex)
            {
                Message(true, ex.Message);

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                Message(false, "");
                gvBlackList.PageIndex = gvBlackList.PageIndex - 1;
                BindGridView();
            }
            catch (Exception ex)
            {
                Message(true, ex.Message);
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                Message(false, "");
                gvBlackList.PageIndex = gvBlackList.PageIndex + 1;
                BindGridView();
            }
            catch (Exception ex)
            {
                Message(true, ex.Message);
            }
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlStatus.SelectedValue == "ALL")
            //{
            BindGridView();
            //}

        }
        protected void gvBlackList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lblBlackList = (LinkButton)e.Row.FindControl("lblBalckListed");
                LinkButton lnkBlackList = (LinkButton)e.Row.FindControl("lnkEdit");
                if (lblBlackList.Text == "BlackListed")
                {
                    //   lnkBlackList.Enabled = false;
                    //e.Row.BackColor = System.Drawing.Color.FromArgb(255 99 71);
                    // e.Row.BackColor = System.Drawing.Color.Red;
                    lblBlackList.ForeColor = System.Drawing.Color.Red;

                }


            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            mpeView.Hide();
        }
        protected void gvPopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPopUp.PageIndex = e.NewPageIndex;
            BindGridView();

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            BinGvpopUp();
            sectionGV3.Visible = true;
            Section1.Visible = false;
            Tr3.Visible = false;
            Tr7.Visible = true;
            mpeNewNonBlacklisted.Show();
            lblMessages.Text = "";

            txtFrmDate.Text = "";
            txtTDate.Text = "";
            txtReason.Text = "";


        }
        protected void gvVisitorPopup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                 if (e.CommandName == "BlackList")
                {
                    sectionGV3.Visible = false;
                    Section1.Visible = true;

                    Tr3.Visible = true;
                    Tr7.Visible = false;
                    table4.Visible = true;
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string visitorId = commandArgs[0];
                    string visitorName = commandArgs[1];

                    txtVisitorIDpopup.Text = visitorId;
                    txtVisitorNamePopup.Text = visitorName;


                }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            mpeNewNonBlacklisted.Hide();
        }
        protected void btnCancelNew_Click(object sender, EventArgs e)
        {
            sectionGV3.Visible = true;
            Section1.Visible = false;
            Tr3.Visible = false;
            Tr7.Visible = true;
            BindGridView();
            BinGvpopUp();
        }
        protected void btnsaveNew_Click(object sender, EventArgs e)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string VisitorID=ViewState["VisitorIdNonBlackList"].ToString();
                SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "VisitorIdNonBlackList";
                cmd.Parameters.AddWithValue("@Visitor_ID", VisitorID);
                cmd.Parameters.AddWithValue("@URL", path);               
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Rowid", hdnRowID.Value);
                cmd.ExecuteNonQuery();
                BindGridView();
                BinGvpopUp();
                mpeBlackList.Hide();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


            }
            catch (Exception ex)
            { 
            
            
            }

        }
        protected void btnCancelNew_Click1(object sender, EventArgs e)
        {
            mpeBlackList.Hide();
            BindGridView();
            BinGvpopUp();
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = false;
                for (int i = 0; i < gvBlackList.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvBlackList.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        if (check == false)
                        {
                            check = true;
                        }
                        try
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }

                            string visitorID = gvBlackList.Rows[i].Cells[2].Text;
                            SqlCommand cmd = new SqlCommand("Visitor_Sp_DeleteBlackListed", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@visiitorID", visitorID);
                            cmd.Parameters.AddWithValue("@URL", path);
                            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                            cmd.ExecuteNonQuery();

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
                            UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Error");
                        }
                    }
                }
                if (check == false)
                {
                    lblMessages.Text = "Please select record to delete.";
                    lblMessages.Visible = true;
                }
                BindGridView();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Error");
            }
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            mpeNewNonBlacklistedEdit.Hide();
        }
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "UpdateBlackList";
                cmd.Parameters.Add("@Visitor_ID", SqlDbType.NVarChar).Value = txtVisitorIdEdit.Text.Trim();
                cmd.Parameters.Add("@Blacklist_Reason", SqlDbType.NVarChar).Value = txtReasonEdit.Text.Trim();
               // cmd.Parameters.Add("@blacklisted_from_date", SqlDbType.DateTime).Value = DateTime.ParseExact(txtFromdateEdit.Text.Trim(),"dd/MM/yyyy",null);
                //cmd.Parameters.Add("@blackListed_To_date", SqlDbType.DateTime).Value = DateTime.ParseExact(txtToDateEdit.Text.Trim(), "dd/MM/yyyy", null);

                cmd.Parameters.Add("@blacklisted_from_date", SqlDbType.NVarChar).Value = txtFromdateEdit.Text.Trim();
                cmd.Parameters.Add("@blackListed_To_date", SqlDbType.NVarChar).Value = txtToDateEdit.Text.Trim();

                cmd.Parameters.AddWithValue("@Rowid", hdnRowID.Value);
                cmd.Parameters.AddWithValue("@URL", path);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.ExecuteNonQuery();
                BindGridView();
                BinGvpopUp();
                Message(true, "Record saved Successfully");
                mpeNewNonBlacklistedEdit.Hide();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            { 
            
            }
        }
        protected void btnSerchInner_Click(object sender, EventArgs e)
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                  SqlCommand cmd = new SqlCommand("USP_VISITOR_BLACKLIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "View";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string expression = "";
                expression = "BlackListed = ''";

                if (expression != "")
                {
                    try
                    {
                        dt = dt.Select(expression).CopyToDataTable();
                        dt.AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        dt = null;

                    }
                }


                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtEmpName.Text.ToString() == "" && txtVehicleNo.Text.ToString() == "")
                {
                    gvVisitorPopup.DataSource = dt;
                    gvVisitorPopup.DataBind();
                }
                else
                {
                    String[,] values = { 
                
                {"Visitor_Name~" +txtEmpName.Text.Trim(), "S" },
                {"AllVisitorID~" +txtVehicleNo.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvVisitorPopup.DataSource = _tempDT;
                    gvVisitorPopup.DataBind();
                }
                //DropDownList ddl = (DropDownList)gvVisitorApproval.BottomPagerRow.FindControl("ddlPageNo");
                //for (int i = 1; i <= gvVisitorApproval.PageCount; i++)
                //{
                //    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //}
                //ddl.SelectedValue = (gvVisitorApproval.PageIndex + 1).ToString();
                //Label lblcount = (Label)gvVisitorApproval.BottomPagerRow.FindControl("lblTotal");
                //lblcount.Text = ((DataTable)gvVisitorApproval.DataSource).Rows.Count.ToString() + " Records.";
                //if (gvVisitorApproval.PageCount == 0)
                //{
                //    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                //    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                //}
                //if (gvVisitorApproval.PageIndex + 1 == gvVisitorApproval.PageCount)
                //{
                //    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                //}
                //if (gvVisitorApproval.PageIndex == 0)
                //{
                //    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                //}
                ////((Label)gvVisitorApproval.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVisitorApproval.PageSize * gvVisitorApproval.PageIndex) + 1) + " to " + (gvVisitorApproval.PageSize * (gvVisitorApproval.PageIndex + 1));

                //((Label)gvVisitorApproval.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVisitorApproval.PageSize * gvVisitorApproval.PageIndex) + 1) + " to " + (((gvVisitorApproval.PageSize * (gvVisitorApproval.PageIndex + 1)) - 10) + gvVisitorApproval.Rows.Count);

                //gvVisitorApproval.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

    }
}