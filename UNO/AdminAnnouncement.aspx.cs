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
    public partial class AdminAnnouncement : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string userid;
        string RowId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }
            if (!Page.IsPostBack)
            {
                bindDataGrid();
            }

        }

        protected void gvAnnouncement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
               
                ViewState["RowId"] = e.CommandArgument.ToString();
                fillModifydata(e.CommandArgument.ToString());
                mpeEditContent.Show();
            }
        }
        private void fillModifydata(string RowID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_AdminAnnouncement", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strCommand", "Modify");
                cmd.Parameters.AddWithValue("@intID", RowID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtContentEdit.InnerText = Convert.ToString(dt.Rows[0]["varHeadLine"]);
                    txtFrmDateEdit.Text = Convert.ToString(dt.Rows[0]["dtFromDate"]);
                    txtTDateEdit.Text = Convert.ToString(dt.Rows[0]["dtToDate"]);
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AdminAnnouncement");
            }

        }
        private void bindDataGrid()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_AdminAnnouncement", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@strCommand", "Select");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAnnouncement.DataSource = dt;
            gvAnnouncement.DataBind();
            if (dt.Rows.Count != 0)
            {
                btnSearch.Enabled = true;
                btnDelete.Enabled = true;
                DropDownList ddl = (DropDownList)gvAnnouncement.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvAnnouncement.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvAnnouncement.PageIndex + 1).ToString();
                Label lblcount = (Label)gvAnnouncement.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvAnnouncement.DataSource).Rows.Count.ToString() + " Records.";
                if (gvAnnouncement.PageCount == 0)
                {
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAnnouncement.PageIndex + 1 == gvAnnouncement.PageCount)
                {
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAnnouncement.PageIndex == 0)
                {
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvAnnouncement.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvAnnouncement.PageSize * gvAnnouncement.PageIndex) + 1) + " to " + (gvAnnouncement.PageSize * (gvAnnouncement.PageIndex + 1));

                gvAnnouncement.BottomPagerRow.Visible = true;
            }
            else
            {

                btnSearch.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvAnnouncement.PageIndex = gvAnnouncement.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AdminAnnouncement");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvAnnouncement.PageIndex = gvAnnouncement.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AdminAnnouncement");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvAnnouncement.PageIndex = Convert.ToInt32(((DropDownList)gvAnnouncement.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_AdminAnnouncement", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strCommand", "Insert");
                cmd.Parameters.AddWithValue("@varHeadLine", txtContent.InnerText.Trim());
                cmd.Parameters.AddWithValue("@dtFromDate", txtFrmDate.Text.Trim());
                cmd.Parameters.AddWithValue("@dtToDate", txtTDate.Text.Trim());
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    lblMessages.Text = "Record Saved Successfully";
                    lblMessages.Visible = true;
                }
                else if (i == -1)
                {
                    lblMessages.Text = "Record Already Exists.";
                    lblMessages.Visible = true;
                }
                mpeAddCall.Hide();
                bindDataGrid();
            }
            catch( Exception ex)
            {
                lblMessages.Text = ex.Message;
                lblMessages.Visible = true; 
                conn.Close();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            mpeAddCall.Hide();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < gvAnnouncement.Rows.Count; i++)
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand objcmd = new SqlCommand("USP_AdminAnnouncement");
                    objcmd.Connection = conn;
                    SqlTransaction trans;
                    trans = conn.BeginTransaction();
                    try
                    {
                        objcmd.Transaction = trans;
                        CheckBox delrows = (CheckBox)gvAnnouncement.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)gvAnnouncement.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }

                           // SqlCommand cmd = new SqlCommand("USP_AdminAnnouncement", conn);
                            objcmd.CommandType = CommandType.StoredProcedure;
                            objcmd.Parameters.AddWithValue("@strCommand", "Delete");
                            objcmd.Parameters.AddWithValue("@intID", lblRowId.Text);
                            objcmd.ExecuteNonQuery();
                            trans.Commit();
                            Check = true;
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }

                }


                if (Check == true)
                {
                    lblMessages.Text = "Record Deleted Successfully";
                    lblMessages.Visible = true;                  
                }
                else if (marked == false)
                {
                    lblMessages.Text = "Please select record to Delete";
                    lblMessages.Visible = true;
                }
                bindDataGrid();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AdminAnnouncement");
            }

        }

        protected void New(object sender, EventArgs e)
        {
          
            txtContent.InnerText = "";
            txtFrmDate.Text = "";
            txtTDate.Text = "";
            mpeAddCall.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = "";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_AdminAnnouncement", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strCommand", "Select");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (txtTodate.Text.ToString() == "" && txtFromDate.Text.ToString() == "")
                {
                    gvAnnouncement.DataSource = dt;
                    gvAnnouncement.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"dtFromDate~" +txtFromDate.Text.Trim(), "S" },
				{"dtToDate~" +txtTodate.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvAnnouncement.DataSource = _tempDT;
                    gvAnnouncement.DataBind();
                }

                DropDownList ddl = (DropDownList)gvAnnouncement.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvAnnouncement.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvAnnouncement.PageIndex + 1).ToString();
                Label lblcount = (Label)gvAnnouncement.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvAnnouncement.DataSource).Rows.Count.ToString() + " Records.";
                if (gvAnnouncement.PageCount == 0)
                {
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAnnouncement.PageIndex + 1 == gvAnnouncement.PageCount)
                {
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAnnouncement.PageIndex == 0)
                {
                    ((Button)gvAnnouncement.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvAnnouncement.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvAnnouncement.PageSize * gvAnnouncement.PageIndex) + 1) + " to " + (gvAnnouncement.PageSize * (gvAnnouncement.PageIndex + 1));

                gvAnnouncement.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AdminAnnouncement");
            }

        }

        protected void btnEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_AdminAnnouncement", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strCommand", "Update");
                cmd.Parameters.AddWithValue("@varHeadLine", txtContentEdit.InnerText.Trim());
                cmd.Parameters.AddWithValue("@dtFromDate",  txtFrmDateEdit.Text.Trim());
                cmd.Parameters.AddWithValue("@dtToDate", txtTDateEdit.Text.Trim());
                cmd.Parameters.AddWithValue("@intID", Convert.ToString(ViewState["RowId"]));
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    lblMessages.Text = "Record Updated Successfully";
                    lblMessages.Visible = true;
                }
                else if(i==-1)
                {
                    lblMessages.Text = "Record Already Exists.";
                    lblMessages.Visible = true;
                }
                mpeEditContent.Hide();
                bindDataGrid();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                lblMessages.Visible = true;
                conn.Close();
            }
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            mpeEditContent.Hide();
        }

    }
}