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
    public partial class EmployeePinConfigView : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvCardConfig.ClientID + "');");
            }

        }

        void bindDataGrid()
        {
            try
            {

                string strsql = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                strsql = " select emp.EPD_FIRST_NAME+'-'+emp.EPD_LAST_NAME as name,emp.EPD_EMPID as cc_emp_id ,emp.EPD_CARD_ID as CARD_CODE ,pin,usecount,ignore_apb, " +
                         " case when status=0 then 'False' else 'True' end  as status,Convert(varchar,activation_date,103) as activation_date, " +
                         " Convert(varchar,expiry_date,103) as expiry_date FROM  " +
                         " ENT_EMPLOYEE_PERSONAL_DTLS emp inner join  " +
                         " ACS_CARD_CONFIG on(emp.EPD_EMPID=ACS_CARD_CONFIG.cc_emp_id) " +
                         " inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                         " on emp.EPD_EMPID=eod.EOD_EMPID " +
                         " where emp.epd_isdeleted='0' and eod.EOD_ACTIVE='1' and ACS_CARD_CONFIG.ACE_isdeleted='0' ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
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
                                                  join comwise in thisDataSet.AsEnumerable() on acs.Field<string>("cc_emp_id") equals comwise.Field<string>("EOD_EMPID")
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

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvCardConfig.DataSource = temp;
                gvCardConfig.DataBind();
                DropDownList ddl = (DropDownList)gvCardConfig.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCardConfig.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCardConfig.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCardConfig.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCardConfig.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCardConfig.PageCount == 0)
                {
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCardConfig.PageIndex + 1 == gvCardConfig.PageCount)
                {
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCardConfig.PageIndex == 0)
                {
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvCardConfig.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCardConfig.PageSize * gvCardConfig.PageIndex) + 1) + " to " + (gvCardConfig.PageSize * (gvCardConfig.PageIndex + 1));

                ((Label)gvCardConfig.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCardConfig.PageSize * gvCardConfig.PageIndex) + 1) + " to " + (((gvCardConfig.PageSize * (gvCardConfig.PageIndex + 1)) - 10) + gvCardConfig.Rows.Count);

                gvCardConfig.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
            }

        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCardConfig.PageIndex = Convert.ToInt32(((DropDownList)gvCardConfig.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
                clear();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCardConfig.PageIndex = gvCardConfig.PageIndex - 1;
                bindDataGrid();
                clear();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
            }

        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCardConfig.PageIndex = gvCardConfig.PageIndex + 1;
                bindDataGrid();
                clear();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                string strsql = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //strsql = " select COMPANY_ID,COMPANY_NAME,COMPANY_ADDRESS,COMPANY_RO_ADDRESS1,COMPANY_RO_ADDRESS1," +
                //        " COMPANY_PHONE1,COMPANY_PHONE2,COMPANY_PIN,COMPANY_RO_PIN,COMPANY_RO_PHONE1,COMPANY_RO_PHONE2," +
                //        " COMPANY_CITY,COMPANY_RO_CITY,COMPANY_STATE,COMPANY_RO_STATE from ENT_COMPANY  WHERE COMPANY_ISDELETED = '0'";

                strsql = " select emp.EPD_FIRST_NAME+'-'+emp.EPD_LAST_NAME as name,emp.EPD_EMPID as cc_emp_id ,emp.EPD_CARD_ID as CARD_CODE ,pin,usecount,ignore_apb, " +
                      " case when status=0 then 'False' else 'True' end  as status,Convert(varchar,activation_date,103) as activation_date, " +
                      " Convert(varchar,expiry_date,103) as expiry_date FROM  " +
                      " ENT_EMPLOYEE_PERSONAL_DTLS emp inner join  " +
                      " ACS_CARD_CONFIG on(emp.EPD_EMPID=ACS_CARD_CONFIG.cc_emp_id) " +
                      " inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                      " on emp.EPD_EMPID=eod.EOD_EMPID " +
                      " where emp.epd_isdeleted='0' and eod.EOD_ACTIVE='1' and ACS_CARD_CONFIG.ACE_isdeleted='0'";



                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


                if (textzoneid.Text.ToString() == "" && textzonename.Text.ToString() == "")
                {
                    gvCardConfig.DataSource = dt;
                    gvCardConfig.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"cc_emp_id ~" +textzoneid.Text.Trim(), "S" },
				{"CARD_CODE ~" +textzonename.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvCardConfig.DataSource = _tempDT;
                    gvCardConfig.DataBind();
                }

                DropDownList ddl = (DropDownList)gvCardConfig.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCardConfig.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCardConfig.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCardConfig.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCardConfig.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCardConfig.PageCount == 0)
                {
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCardConfig.PageIndex + 1 == gvCardConfig.PageCount)
                {
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCardConfig.PageIndex == 0)
                {
                    ((Button)gvCardConfig.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvCardConfig.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCardConfig.PageSize * gvCardConfig.PageIndex) + 1) + " to " + (gvCardConfig.PageSize * (gvCardConfig.PageIndex + 1));

                ((Label)gvCardConfig.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCardConfig.PageSize * gvCardConfig.PageIndex) + 1) + " to " + (((gvCardConfig.PageSize * (gvCardConfig.PageIndex + 1)) - 10) + gvCardConfig.Rows.Count);

                gvCardConfig.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {

        }

        protected void gvCardConfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCardConfig.PageIndex = e.NewPageIndex;
                bindDataGrid();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
            }
        }

        protected void gvCardConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string Rowid = e.CommandArgument.ToString();
                ViewState["empid"] = Rowid;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                LinkButton btn = (LinkButton)e.CommandSource;
                GridViewRow grdrow = ((GridViewRow)btn.NamingContainer);
                //CheckBox chkSelect = (CheckBox)grdrow.FindControl("chkSelect");
               int status=1;
               // if (chkSelect.Checked)
               // {
               //     status = 1;
               // }
               // else 
               // {

                //    status = 0;
                //}


                //string selectData = "update ACS_CARD_CONFIG set status=" + status  + " where CC_EMP_ID='" + Rowid + "'";
                //SqlCommand cmdUpdate = new SqlCommand(selectData, conn);
               //cmdUpdate.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_Insert_ACS_PinConfig_New]";
                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@userCount", "");
                cmd.Parameters.AddWithValue("@activationDate", "");
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@PIN","");
                cmd.Parameters.AddWithValue("@ExpiryDate", "");

                cmd.Parameters.AddWithValue("@EmpCode", Rowid);
                cmd.Parameters.AddWithValue("@Cmpcde", "");
                cmd.Parameters.AddWithValue("@Loccde", "");
                cmd.Parameters.AddWithValue("@divcde", "");
                cmd.Parameters.AddWithValue("@depcde", "");
                cmd.Parameters.AddWithValue("@catcde", "");
                cmd.Parameters.AddWithValue("@Command", "StatusChange");



                int j = cmd.ExecuteNonQuery();
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Status Change Successfully');", true);
                
               string CloseWindow;
               CloseWindow = "alert('Status Change Successfully')";
                ScriptManager.RegisterStartupScript(this,this.GetType(), "CloseWindow", CloseWindow, true);




                bindDataGrid();
                
                //   Response.Redirect("EmployeeCardConfigureEdit.aspx?id=" + Rowid);

            }

            if (e.CommandName == "ModifyPin")
            {
                string Rowid = e.CommandArgument.ToString();
                ViewState["empid"] = Rowid;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                LinkButton btn = (LinkButton)e.CommandSource;
                GridViewRow grdrow = ((GridViewRow)btn.NamingContainer);
                //CheckBox chkSelect = (CheckBox)grdrow.FindControl("chkSelect");
                
                //if (chkSelect.Checked)
                {
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = "select NEXT VALUE FOR PinSequence";
                    string cmdStr = "select NEXT VALUE FOR PinSequence";
                    SqlCommand sqlCmd = new SqlCommand(cmdStr, conn);



                    int NewPin = (int)sqlCmd.ExecuteScalar();



                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_Insert_ACS_PinConfig_New]";
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.AddWithValue("@userCount", "");
                    cmd.Parameters.AddWithValue("@activationDate", "");
                    cmd.Parameters.AddWithValue("@Status", "");
                    cmd.Parameters.AddWithValue("@PIN", NewPin);
                    cmd.Parameters.AddWithValue("@ExpiryDate", "");

                    cmd.Parameters.AddWithValue("@EmpCode", Rowid);
                    cmd.Parameters.AddWithValue("@Cmpcde", "");
                    cmd.Parameters.AddWithValue("@Loccde", "");
                    cmd.Parameters.AddWithValue("@divcde", "");
                    cmd.Parameters.AddWithValue("@depcde", "");
                    cmd.Parameters.AddWithValue("@catcde", "");
                    cmd.Parameters.AddWithValue("@Command", "PinChange");



                    int j = cmd.ExecuteNonQuery();
                    // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Pin Change Successfully');", true);
                    string CloseWindow;
                    CloseWindow = "alert('Pin Change Successfully')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


                    //string selectData = "update ACS_CARD_CONFIG set PIN='" + NewPin + "' where CC_EMP_ID='" + Rowid + "'";
                    //SqlCommand cmdUpdatePin = new SqlCommand(selectData, conn);
                    //cmdUpdatePin.ExecuteNonQuery();
                    bindDataGrid();
                    //   Response.Redirect("EmployeeCardConfigureEdit.aspx?id=" + Rowid);

                }
               // else
               // {

               //     string CloseWindow;
               //     CloseWindow = "alert('Please Update Status First')";
               //     ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

              //  }





               
                
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = false;
                for (int i = 0; i < gvCardConfig.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvCardConfig.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        check = true;
                        try
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_Insert_ACS_PinConfig_New]";
                            cmd.CommandTimeout = 0;

                            cmd.Parameters.AddWithValue("@userCount", "");
                            cmd.Parameters.AddWithValue("@activationDate", "");
                            cmd.Parameters.AddWithValue("@Status", "");
                            cmd.Parameters.AddWithValue("@PIN", "");
                            cmd.Parameters.AddWithValue("@ExpiryDate", "");

                            cmd.Parameters.AddWithValue("@EmpCode", gvCardConfig.Rows[i].Cells[3].Text);
                            cmd.Parameters.AddWithValue("@Cmpcde", "");
                            cmd.Parameters.AddWithValue("@Loccde", "");
                            cmd.Parameters.AddWithValue("@divcde", "");
                            cmd.Parameters.AddWithValue("@depcde", "");
                            cmd.Parameters.AddWithValue("@catcde", "");
                            cmd.Parameters.AddWithValue("@Command", "Delete");

                            int j = cmd.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Record Deleted Successfully');", true);
                           // objcmd.CommandText = "delete from ACS_CARD_CONFIG where cc_emp_id='" + gvCardConfig.Rows[i].Cells[2].Text + "'";
                           // objcmd.ExecuteNonQuery();
                            
                            lblMessages.Text = "Record(s) deleted SuccessFully.";
                            lblMessages.Visible = true;
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
                            UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
                        }
                    }
                }
                bindDataGrid();
                if (check == false)
                {
                    lblMessages.Text = "Please select record to delete.";
                    lblMessages.Visible = true;
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfigView");
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeePinConfigureADD.aspx");

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            clear();
            mpeditPopup.Hide();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string empid = ViewState["empid"].ToString();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }


            SqlCommand cmd1 = new SqlCommand("sp_UpdateACS_CARD_CONFIG", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid;
            cmd1.Parameters.Add("@USECOUNT", SqlDbType.VarChar).Value = txtMinimumSwipe.Text;
            cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = Pin.Text;
            cmd1.Parameters.Add("@ACTIVATION_DATE", SqlDbType.DateTime).Value = DateTime.ParseExact(Act_Date.Text, "dd/MM/yyyy", null);
            cmd1.Parameters.Add("@EXPIRY_DATE", SqlDbType.DateTime).Value = DateTime.ParseExact(Exp_Date1.Text, "dd/MM/yyyy", null);

            if (Chk_Sts.Checked == true)
                cmd1.Parameters.Add("@STATUS", SqlDbType.Bit).Value = 1;
            else
                cmd1.Parameters.Add("@STATUS", SqlDbType.Bit).Value = 0;

            int j = cmd1.ExecuteNonQuery();


            string strUpdateEALConfig = " UPDATE EAL_CONFIG SET FLAG='1' WHERE EMPLOYEE_CODE='" + empid + "' and FLAG='0'";
            SqlCommand cmdEAConfig = new SqlCommand();
            cmdEAConfig.Connection = conn;
            cmdEAConfig.CommandText = strUpdateEALConfig;
            cmdEAConfig.CommandType = CommandType.Text;
            cmdEAConfig.ExecuteNonQuery();

            if (j > 0)
            {
                lblMessages.Text = "Record updated successfully.";
                lblMessages.Visible = true;
            }

            bindDataGrid();
            mpeditPopup.Hide();
        }

        private void clear()
        {
            lblMessages.Text = "";
            lblMessages.Visible = false;

        }
    }
}