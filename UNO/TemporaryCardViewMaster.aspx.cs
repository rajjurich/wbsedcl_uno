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
    public partial class TemporaryCardViewMaster : System.Web.UI.Page
    {
        SqlConnection Mstr_Con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                FillReasonEdit();
                ButtonDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvTempCard.ClientID + "');");
            }
        }

        void bindDataGrid()
        {
            try
            {
                string strsql = "";
                if (Mstr_Con.State == ConnectionState.Closed)
                {
                    Mstr_Con.Open();
                }
                strsql = "Select  TC_TMP_CARD_ID,TC_EMPLOYEE_ID,CONVERT(VARCHAR(10),TC_ISSUEDT,103) AS TC_ISSUEDT ,CONVERT(VARCHAR(10),TC_RETURNDT,103) AS TC_RETURNDT,TC_REASON_ID FROM TA_TEMPCARD WHERE TC_ISDELETED=0";
                SqlDataAdapter da = new SqlDataAdapter(strsql, Mstr_Con);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }

                gvTempCard.DataSource = dt;
                gvTempCard.DataBind();

                DropDownList ddl = (DropDownList)gvTempCard.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvTempCard.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvTempCard.PageIndex + 1).ToString();
                Label lblcount = (Label)gvTempCard.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvTempCard.DataSource).Rows.Count.ToString() + " Records.";
                if (gvTempCard.PageCount == 0)
                {
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTempCard.PageIndex + 1 == gvTempCard.PageCount)
                {
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTempCard.PageIndex == 0)
                {
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvTempCard.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTempCard.PageSize * gvTempCard.PageIndex) + 1) + " to " + (gvTempCard.PageSize * (gvTempCard.PageIndex + 1));

                gvTempCard.BottomPagerRow.Visible = true;


                if (dt.Rows.Count != 0)
                {
                    ButtonDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }

                else
                {
                    ButtonDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");

            }




        }


        private void FillReasonEdit()
        {
            try
            {

                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type";
                strSql = strSql + " and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'TC'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, Mstr_Con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlReasonAdd.DataValueField = "Reason_ID";
                ddlReasonAdd.DataTextField = "Reason_Description";
                ddlReasonAdd.DataSource = dt;
                ddlReasonAdd.DataBind();
                //ddlReasonAdd.Items.Insert(0, "Select One");
                ddlReasonAdd.Items.Insert(0, new ListItem("Select One", "0"));


                ddlReasonEdit.DataValueField = "Reason_ID";
                ddlReasonEdit.DataTextField = "Reason_Description";
                ddlReasonEdit.DataSource = dt;
                ddlReasonEdit.DataBind();
                //ddlReasonEdit.Items.Insert(0, "Select One");
                ddlReasonEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }


        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = false;
                for (int i = 0; i < gvTempCard.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvTempCard.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        check = true;
                        try
                        {
                            if (Mstr_Con.State == ConnectionState.Closed)
                            {
                                Mstr_Con.Open();
                            }

                            string strsql = "SELECT TC_EMPLOYEE_ID,TC_ORI_CARD_ID,TC_TMP_CARD_ID,TC_ISSUEDT,TC_RETURNDT,TC_REASON_ID FROM TA_TEMPCARD WHERE TC_ISDELETED='0'";

                            SqlCommand cmd = new SqlCommand(strsql, Mstr_Con);
                            cmd.CommandType = CommandType.Text;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (Mstr_Con.State == ConnectionState.Open)
                            {
                                Mstr_Con.Close();
                            }
                            if (dt.Rows.Count != 0)
                            {
                                lblMessages.Text = "Theses Temporary Card IDs " + gvTempCard.Rows[i].Cells[2].Text + " can not be deleted since it is already in use.";
                                lblMessages.Visible = true;
                            }
                            else
                            {
                                if (Mstr_Con.State == ConnectionState.Closed)
                                {
                                    Mstr_Con.Open();
                                }
                                //cmd = new SqlCommand("delete from TA_TEMPCARD where TC_TMP_CARD_ID='" + gvTempCard.Rows[i].Cells[2].Text + "' and TC_EMPLOYEE_ID ='" + gvTempCard.Rows[i].Cells[3].Text + "'", Mstr_Con);
                                
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                                if (Mstr_Con.State == ConnectionState.Open)
                                {
                                    Mstr_Con.Close();
                                }
                                bindDataGrid();
                            }
                        }
                        catch (Exception ex)
                        {
                            if (Mstr_Con.State == ConnectionState.Open)
                            {
                                Mstr_Con.Close();
                            }
                            UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
                        }
                    }
                }
                if (check == false)
                {
                    lblMessages.Text = "Please select record to be deleted.";
                    lblMessages.Visible = true;
                }

            }
            catch (Exception ex)
            {
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }
        }    

        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                if (Mstr_Con.State == ConnectionState.Closed)
                {
                    Mstr_Con.Open();
                }
                strsql = "Select  TC_TMP_CARD_ID,TC_EMPLOYEE_ID,CONVERT(VARCHAR(10),TC_ISSUEDT,103) AS TC_ISSUEDT ,CONVERT(VARCHAR(10),TC_RETURNDT,103) AS TC_RETURNDT,TC_REASON_ID FROM TA_TEMPCARD WHERE TC_ISDELETED=0";
                SqlDataAdapter da = new SqlDataAdapter(strsql, Mstr_Con);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }


                if (txtCompanyID.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
                {
                    gvTempCard.DataSource = dt;
                    gvTempCard.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"TC_TMP_CARD_ID~" +txtCompanyID.Text.Trim(), "S" },
				{"TC_EMPLOYEE_ID~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvTempCard.DataSource = _tempDT;
                    gvTempCard.DataBind();
                }

                DropDownList ddl = (DropDownList)gvTempCard.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvTempCard.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvTempCard.PageIndex + 1).ToString();
                Label lblcount = (Label)gvTempCard.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvTempCard.DataSource).Rows.Count.ToString() + " Records.";
                if (gvTempCard.PageCount == 0)
                {
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTempCard.PageIndex + 1 == gvTempCard.PageCount)
                {
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTempCard.PageIndex == 0)
                {
                    ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvTempCard.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTempCard.PageSize * gvTempCard.PageIndex) + 1) + " to " + (gvTempCard.PageSize * (gvTempCard.PageIndex + 1));

                gvTempCard.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvTempCard.PageIndex = Convert.ToInt32(((DropDownList)gvTempCard.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }
            
        }
        protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvTempCard.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }
        }
        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    string CompanyId = e.CommandArgument.ToString();
                    string strsql = "SELECT TC_EMPLOYEE_ID,TC_ORI_CARD_ID,TC_TMP_CARD_ID,TC_ISSUEDT,TC_RETURNDT,TC_REASON_ID FROM TA_TEMPCARD WHERE TC_TMP_CARD_ID ='" + CompanyId + "' AND TC_ISDELETED=0";
                    if (Mstr_Con.State == ConnectionState.Closed)
                    {
                        Mstr_Con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(strsql, Mstr_Con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (Mstr_Con.State == ConnectionState.Open)
                    {
                        Mstr_Con.Close();
                    }
                    if (dt.Rows.Count > 0)
                    {
                        txtTempCardIDEdit.Text = dt.Rows[0]["TC_TMP_CARD_ID"].ToString();
                        txtEmpCdEdit.Text = dt.Rows[0]["TC_EMPLOYEE_ID"].ToString();
                        txtIssueDateEdit.Text = Convert.ToDateTime(dt.Rows[0]["TC_ISSUEDT"].ToString()).ToString("dd/MM/yyyy");
                        txtReturnDateEdit.Text = Convert.ToDateTime(dt.Rows[0]["TC_RETURNDT"].ToString()).ToString("dd/MM/yyyy");
                        ddlReasonEdit.SelectedValue = ((dt.Rows[0]["TC_REASON_ID"].ToString() == "") ? "0" : dt.Rows[0]["TC_REASON_ID"].ToString());
                        txtTempCardIDEdit.Enabled = false;
                        mpeEditTC.Show();
                        
                    }
                    else
                    {
                        lblMessages.Text = "Records not found";
                        lblMessages.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }



        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvTempCard.PageIndex = gvTempCard.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvTempCard.PageIndex = gvTempCard.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }    
        }

       

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddTC.Hide();
        }

     
        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEditTC.Hide();
        }

       
        protected void btnSaveAdd_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (Mstr_Con.State == ConnectionState.Closed)
                {
                    Mstr_Con.Open();
                }

                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = Mstr_Con;
                objcmd.CommandText = "SELECT top 1  1  FROM TA_TEMPCARD  with(nolock)  Where  TC_TMP_CARD_ID='" + txtTempCardIDAdd.Text.Trim() + "' AND TC_ISDELETED=0 ";
                objcmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(objcmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }

                SqlDataAdapter dachker = new SqlDataAdapter(" select top 1 1 from	ENT_EMPLOYEE_PERSONAL_DTLS  with(nolock) where EPD_EMPID='" + txtEmpCdAdd.Text + "' and EPD_ISDELETED='0' ", Mstr_Con);
                DataTable dtchker = new DataTable();
                dachker.Fill(dtchker);

                string strStartDate = txtIssueDateAdd.Text;
                string strEndDate = txtReturnDateAdd.Text;

                string strQuery = "select  top  1 1   from TA_TEMPCARD  with(nolock) where  isnull(tc_isdeleted,0)=0  and  TC_EMPLOYEE_ID = '" + txtEmpCdAdd.Text + "'  and (((convert(datetime,'" + strStartDate + "',103)) between TC_ISSUEDT and TC_RETURNDT) or  ( (convert(datetime,'" + strEndDate + "',103)) between TC_ISSUEDT and TC_RETURNDT)) ";
                SqlDataAdapter dachk = new SqlDataAdapter(strQuery, Mstr_Con);
                DataTable dtchk = new DataTable();
                dachk.Fill(dtchk);

                if (dtchker.Rows.Count == 0)
                {
                    lblErrorAdd.Text = "Employee Id does not exists";
                    lblErrorAdd.Visible = true;
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    // TemporaryCard Already Exists.
                    lblErrorAdd.Text = "Temporary Card already exists.";
                    lblErrorAdd.Visible = true;
                    return;
                }
                else if (dtchk.Rows.Count > 0)
                {
                    lblErrorAdd.Text = "Date range is already used for this employee.";
                    lblErrorAdd.Visible = true;
                    return;
                }
                else
                {
                   

                    if (Mstr_Con.State == ConnectionState.Closed)
                    {
                        Mstr_Con.Open();
                    }

                    string InsertValue = "INSERT INTO TA_TEMPCARD ([TC_TMP_CARD_ID],[TC_EMPLOYEE_ID],[TC_ISSUEDT],[TC_RETURNDT],[TC_REASON_ID],[TC_ISDELETED],[TC_DELETEDDATE])VALUES('" + txtTempCardIDAdd.Text.ToUpper() + "','" + txtEmpCdAdd.Text.ToUpper() + "',convert(datetime,'" + txtIssueDateAdd.Text.Trim() + "',103),convert(datetime,'" + txtReturnDateAdd.Text + "',103),'" + ddlReasonAdd.SelectedValue + "',0,Null)";

                    SqlCommand cmd1 = new SqlCommand(InsertValue, Mstr_Con);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                    if (Mstr_Con.State == ConnectionState.Open)
                    {
                        Mstr_Con.Close();
                    }
                    txtTempCardIDAdd.Text = "";
                    txtEmpCdAdd.Text = "";
                    txtIssueDateAdd.Text = "";
                    txtReturnDateAdd.Text = "";
                    ddlReasonAdd.SelectedIndex = 0;
                    mpeAddTC.Show();
                    txtTempCardIDAdd.Focus();
                    lblErrorAdd.Text = "Record saved  successfully";
                    lblErrorAdd.Visible = true;
                    bindDataGrid();


                }

            }
            catch (Exception ex)
            {
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                    txtTempCardIDAdd.Text = "";
                    txtEmpCdAdd.Text = "";
                    txtIssueDateAdd.Text = "";
                    txtReturnDateAdd.Text = "";
                    ddlReasonAdd.SelectedIndex = 0;
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            } 
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mstr_Con.State == ConnectionState.Closed)
                {
                    Mstr_Con.Open();
                }

                string strQuery = "select  top  1 1   from TA_TEMPCARD  with(nolock) where  isnull(tc_isdeleted,0)=0  and  TC_EMPLOYEE_ID = '" + txtEmpCdAdd.Text + "'  and (((convert(datetime,'" + txtIssueDateEdit.Text.Trim() + "',103)) between TC_ISSUEDT and TC_RETURNDT) or  ( (convert(datetime,'" + txtReturnDateEdit.Text.Trim() + "',103)) between TC_ISSUEDT and TC_RETURNDT)) ";
                SqlDataAdapter dachk = new SqlDataAdapter(strQuery, Mstr_Con);
                DataTable dtchk = new DataTable();
                dachk.Fill(dtchk);

                 if (dtchk.Rows.Count > 0)
                {
                    lblMessages.Text = "Date range is already used for the this employee.";
                    lblMessages.Visible = true;
                    return;
                }

                string InsertValue = "UPDATE TA_TEMPCARD SET TC_ISSUEDT =convert(datetime,'" + txtIssueDateEdit.Text + "',103) , TC_RETURNDT=convert(datetime,'" + txtReturnDateEdit.Text + "',103) , TC_REASON_ID='" + ddlReasonEdit.SelectedValue + "' where TC_TMP_CARD_ID='" + txtTempCardIDEdit.Text.Trim() + "'";

                SqlCommand cmd = new SqlCommand(InsertValue, Mstr_Con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }
                mpeEditTC.Hide();
                lblMessages.Text = "Record modified successfully";
                lblMessages.Visible = true;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                if (Mstr_Con.State == ConnectionState.Open)
                {
                    Mstr_Con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TemporaryCardViewMaster");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            txtEmpCdAdd.Text = "";
            txtIssueDateAdd.Text = "";
            txtTempCardIDAdd.Text = "";
            txtReturnDateAdd.Text = "";
            ddlReasonAdd.SelectedValue = "0";
            lblErrorAdd.Text = "";
            mpeAddTC.Show();
            txtTempCardIDAdd.Focus();
        }

       



    }
}