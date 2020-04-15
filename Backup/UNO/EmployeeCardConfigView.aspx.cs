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
    public partial class EmployeeCardConfigView : System.Web.UI.Page
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
                strsql = " select emp.EPD_EMPID as cc_emp_id ,emp.EPD_CARD_ID as CARD_CODE ,pin,usecount,ignore_apb, "+
                         " status,Convert(varchar,activation_date,103) as activation_date, "+
                         " Convert(varchar,expiry_date,103) as expiry_date FROM  "+
                         " ENT_EMPLOYEE_PERSONAL_DTLS emp inner join  "+
                         " ACS_CARD_CONFIG on(emp.EPD_EMPID=ACS_CARD_CONFIG.cc_emp_id) "+
                         " inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod "+
                         " on emp.EPD_EMPID=eod.EOD_EMPID "+
                         " where emp.epd_isdeleted='0' and eod.EOD_ACTIVE='1' ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvCardConfig.DataSource = dt;
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

                strsql = " select emp.EPD_EMPID as cc_emp_id ,emp.EPD_CARD_ID as CARD_CODE ,pin,usecount,ignore_apb, " +
                      " status,Convert(varchar,activation_date,103) as activation_date, " +
                      " Convert(varchar,expiry_date,103) as expiry_date FROM  " +
                      " ENT_EMPLOYEE_PERSONAL_DTLS emp inner join  " +
                      " ACS_CARD_CONFIG on(emp.EPD_EMPID=ACS_CARD_CONFIG.cc_emp_id) " +
                      " inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                      " on emp.EPD_EMPID=eod.EOD_EMPID " +
                      " where emp.epd_isdeleted='0' and eod.EOD_ACTIVE='1' ";



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
                string selectData = "select [USECOUNT],[PIN],CONVERT(varchar(10),ACTIVATION_DATE,103) as ACTIVATION_DATE,CONVERT(varchar(10),EXPIRY_DATE,103) as EXPIRY_DATE,STATUS from ACS_CARD_CONFIG where CC_EMP_ID='" + Rowid + "'";
                SqlCommand cmdUpdate = new SqlCommand(selectData, conn);
                SqlDataReader dr = cmdUpdate.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["STATUS"].ToString() == "True")
                        Chk_Sts.Checked = true;
                    else
                        Chk_Sts.Checked = false;


                    txtMinimumSwipe.Text = dr["USECOUNT"].ToString();
                    Act_Date.Text = dr["ACTIVATION_DATE"].ToString();
                    Pin.Text = dr["PIN"].ToString();
                    Exp_Date1.Text = dr["EXPIRY_DATE"].ToString();
                    break;
                }
                mpeditPopup.Show(); 
             //   Response.Redirect("EmployeeCardConfigureEdit.aspx?id=" + Rowid);
             
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
                            string s1  = gvCardConfig.Rows[i].Cells[2].Text;
                            string str = "select EMPLOYEE_CODE from EAL_CONFIG where  EMPLOYEE_CODE='" + s1 + "'";
                            SqlCommand cmd = new SqlCommand(str, conn);
                            cmd.CommandType = CommandType.Text;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            if (dt.Rows.Count != 0)
                            {
                                lblMessages.Text = "This Employee " + s1 + "  can not be deleted since it is already in use.";
                                lblMessages.Visible = true;
                                return;
                            }
                            else { lblMessages.Text = ""; }
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }


                                SqlCommand objcmd = new SqlCommand();
                                objcmd.Connection = conn;
                                check = true;
                                objcmd.CommandText = "update EAL_CONFIG  set flag='2' where employee_code='" + gvCardConfig.Rows[i].Cells[2].Text + "'";
                                objcmd.ExecuteNonQuery();

                                objcmd.CommandText = "delete from ACS_CARD_CONFIG where cc_emp_id='" + gvCardConfig.Rows[i].Cells[2].Text + "'";
                                objcmd.ExecuteNonQuery();
                               
                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }
                                lblMessages.Text = "Please select record to delete.";
                                lblMessages.Visible = true;
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
                if (check == false)
                {
                   
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

              bindDataGrid();
              lblMessages.Text = "Record(s) deleted SuccessFully.";
              lblMessages.Visible = true;  


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeCardConfigureADD.aspx");

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


