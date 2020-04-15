using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UNO
{
    public partial class WorkDayviewMaster : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string Entity_Mode = string.Empty;
        string emplyeeid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvWorkDay();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvWorkDay.ClientID + "');");
            }
        }

        private void BindgvWorkDay()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select  CODE,VALUE FROM ENT_PARAMS WHERE MODULE='TA' AND IDENTIFIER='WorkDay'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                gvWorkDay.DataSource = dt;
                gvWorkDay.DataBind();

                DropDownList ddl = (DropDownList)gvWorkDay.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvWorkDay.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvWorkDay.PageIndex + 1).ToString();
                Label lblcount = (Label)gvWorkDay.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvWorkDay.DataSource).Rows.Count.ToString() + " Records.";
                if (gvWorkDay.PageCount == 0)
                {
                    ((Button)gvWorkDay.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvWorkDay.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvWorkDay.PageIndex + 1 == gvWorkDay.PageCount)
                {
                    ((Button)gvWorkDay.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvWorkDay.PageIndex == 0)
                {
                    ((Button)gvWorkDay.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvWorkDay.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvWorkDay.PageSize * gvWorkDay.PageIndex) + 1) + " to " + (gvWorkDay.PageSize * (gvWorkDay.PageIndex + 1));

                ((Label)gvWorkDay.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvWorkDay.PageSize * gvWorkDay.PageIndex) + 1) + " to " + (((gvWorkDay.PageSize * (gvWorkDay.PageIndex + 1)) - 10) + gvWorkDay.Rows.Count);

                gvWorkDay.BottomPagerRow.Visible = true;

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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvWorkDay.PageIndex = Convert.ToInt32(((DropDownList)gvWorkDay.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindgvWorkDay();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvWorkDay.PageIndex = gvWorkDay.PageIndex - 1;
                BindgvWorkDay();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvWorkDay.PageIndex = gvWorkDay.PageIndex + 1;
                BindgvWorkDay();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                ViewState["PageMode"] = "Add";
                //Entity_Mode = "Add";
                string someScript = "";
                someScript = "<script language='javascript'>CheckComp();</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                txtDesc.Attributes.Add("readonly", "readonly");
                Entity_Mode = "Add";
                mpeAddEdit.Show();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void gvWorkDay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    lblError.Visible = false;

                     ViewState["PageMode"]="Modify";
                    //Entity_Mode = "Modify";
                    string someScript = "";
                    someScript = "<script language='javascript'>CheckComp();</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                    emplyeeid = e.CommandArgument.ToString();
                    Modify_Data(emplyeeid);
                    mpeAddEdit.Show();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void Modify_Data(string Employeeid)
        {
            try
            {
                string strsql = "SELECT CODE , VALUE FROM ENT_PARAMS WHERE MODULE='TA' AND IDENTIFIER='WorkDay' AND VALUE='" + Employeeid + "'";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    txtID.Text = dt.Rows[i]["CODE"].ToString();
                    txtDesc.Text = dt.Rows[i]["VALUE"].ToString();
                    if (txtDesc.Text != "")
                    {
                        string str = "";
                        str = txtDesc.Text.Substring(2, txtDesc.Text.Length - 2);
                        if (str.Contains("PC"))
                        {
                            ChkPL.Checked = true;
                        }
                        if (str.Contains("WC"))
                        {
                            ChkWO.Checked = true;
                        }
                        if (str.Contains("HC"))
                        {
                            ChkHO.Checked = true;
                        }
                        if (txtDesc.Text == "ALL")
                        {
                            ChkPL.Checked = true;
                            ChkWO.Checked = true;
                            ChkHO.Checked = true;
                        }
                        if (txtDesc.Text == "")
                        {
                            ChkPL.Checked = false;
                            ChkWO.Checked = false;
                            ChkHO.Checked = false;
                        }
                    }
                }
                TblInfo.Visible = true;
                //TblBtn.Visible = true;
                //DoorConfig.Visible = true;
                txtID.Enabled = false;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand objcmd = null;

                try
                {
                    if (ViewState["PageMode"].ToString() == "Add")
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        objcmd = new SqlCommand();
                        objcmd.Connection = conn;
                        objcmd.CommandText = "Select VALUE from ENT_PARAMS Where code='" + txtID.Text.Trim() + "' AND MODULE='TA' AND IDENTIFIER='WorkDay'";
                        string rows = Convert.ToString(objcmd.ExecuteScalar());
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        if (rows != "")
                        {
                            lblError.Text = "Work Day Master File Code Already Exists.";
                            lblError.Visible = true;
                            return;
                        }


                        if (txtDesc.Text != "")
                        {
                            string InsertValue = "INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE, VALUE) VALUES('TA','WorkDay','" + txtID.Text.ToUpper() + "','" + txtDesc.Text + "')";
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            objcmd = new SqlCommand(InsertValue, conn);
                            objcmd.ExecuteNonQuery();
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            // Resolved Bug 388 - Swapnil Start
                            mpeAddEdit.Hide();
                            // Resolved Bug 388 - Swapnil End
                            lblMessages.Text = "Record Saved Successfully.";
                            lblMessages.Visible = true;
                            //Label1.Text = "Record Saved Successfully.";
                            txtID.Text = "";
                            txtDesc.Text = "";
                            ChkHO.Checked = false;
                            ChkPL.Checked = false;
                            ChkWO.Checked = false;
                            //LoadJScript();

                            BindgvWorkDay();
                        }
                    }
                    else
                    {
                        string InsertValue = "UPDATE ENT_PARAMS SET VALUE ='" + txtDesc.Text + "' where CODE='" + txtID.Text.Trim() + "' AND MODULE='TA' AND IDENTIFIER='WorkDay'";
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        objcmd = new SqlCommand(InsertValue, conn);
                        objcmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        lblMessages.Text = "Record updated Successfully";
                        lblMessages.Visible = true;
                        //Label1.Text = "Record Saved Successfully";
                        txtID.Text = "";
                        txtDesc.Text = "";
                        ChkHO.Checked = false;
                        ChkPL.Checked = false;
                        ChkWO.Checked = false;
                        txtID.Enabled = true;
                        //LoadJScript();
                        Session.Remove("Mode");
                        mpeAddEdit.Hide();

                        BindgvWorkDay();
                    }

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
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["PageMode"].ToString() == "Modify" && emplyeeid != null)
                {
                    Modify_Data(emplyeeid);
                }
                else
                {
                    txtID.Text = "";
                    txtDesc.Text = "";
                    ChkHO.Checked = false;
                    ChkPL.Checked = false;
                    ChkWO.Checked = false;
                    Session.Remove("Mode");
                   // return;

                }
                mpeAddEdit.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;                
                for (int i = 0; i < gvWorkDay.Rows.Count; i++)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand objcmd = new SqlCommand();
                    objcmd.Connection = conn;
                    SqlTransaction trans;
                    trans = conn.BeginTransaction();
                    try
                    {
                        objcmd.Transaction = trans;
                        CheckBox delrows = (CheckBox)gvWorkDay.Rows[i].FindControl("DeleteRows");
                        if (delrows.Checked == true)
                        {
                            Check = true;
                            objcmd.CommandText = "DELETE FROM ENT_PARAMS WHERE CODE='" + gvWorkDay.Rows[i].Cells[2].Text + "' and VALUE='" + gvWorkDay.Rows[i].Cells[3].Text + "' AND MODULE='TA' AND IDENTIFIER='WorkDay'";
                            objcmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        throw ex;
                    }
                }
                if (Check == true)
                {
                    lblMessages.Text = "Record(s) Deleted Successfully";
                    lblMessages.Visible = true;
                    BindgvWorkDay();
                    return;
                }
                else
                {
                    lblMessages.Text = "Please Select Records To Delete.";
                    lblMessages.Visible = true;
                }
              
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "WorkDayMaster");
            }
        }     

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try 
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                string strsql = "Select  CODE,VALUE FROM ENT_PARAMS WHERE MODULE='TA' AND IDENTIFIER='WorkDay'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
               
                String[,] values = { 
				{"CODE~" +txtUserID.Text.Trim(), "S" }	,
		        {"VALUE~"+txtLevelID.Text.Trim(),"S"}
				 };
                            DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvWorkDay.DataSource = _tempDT;
                    gvWorkDay.DataBind();

                        
                

            }
            catch(Exception ex)
            {

            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "";
            txtLevelID.Text="";
            BindgvWorkDay();
        }

    }
}