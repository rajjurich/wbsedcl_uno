using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration.Assemblies;
namespace UNO
{
    public partial class ControllerStatus : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Page.IsPostBack == false)
                {
                    bindDataGrid();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            }
        }

        public void bindDataGrid()
        {
            try
            {
        

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataTable _dt = new DataTable();
                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    string strsql = " SELECT CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_FIRMWARE_VERSION_NO ,CTLR_CONN_STATUS " +
                                        " FROM ACS_CONTROLLER Where CTLR_ISDELETED = '0' ";
                    _dt = getDataTable(strsql, conn);
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("fillControllerStatus", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string levelId = Session["levelId"].ToString();
                    cmd.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(_dt);
                
                }
               if (_dt.Rows.Count != 0)
                {

                    gvControllerStatus.DataSource = _dt;
                    gvControllerStatus.DataBind();
                    gvControllerStatus.HeaderRow.Cells[5].Visible = false;
                }
                DropDownList ddl = (DropDownList)gvControllerStatus.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvControllerStatus.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvControllerStatus.PageIndex + 1).ToString();
                Label lblcount = (Label)gvControllerStatus.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvControllerStatus.DataSource).Rows.Count.ToString() + " Records.";
                if (gvControllerStatus.PageCount == 0)
                {
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvControllerStatus.PageIndex + 1 == gvControllerStatus.PageCount)
                {
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvControllerStatus.PageIndex == 0)
                {
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvControllerStatus.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvControllerStatus.PageSize * gvControllerStatus.PageIndex) + 1) + " to " + (gvControllerStatus.PageSize * (gvControllerStatus.PageIndex + 1));

                ((Label)gvControllerStatus.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvControllerStatus.PageSize * gvControllerStatus.PageIndex) + 1) + " to " + (((gvControllerStatus.PageSize * (gvControllerStatus.PageIndex + 1)) - 10) + gvControllerStatus.Rows.Count);

                gvControllerStatus.BottomPagerRow.Visible = true;
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            
            }
        }

        public DataTable getDataTable(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();

                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();

                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
                return null; 

            }
        }

        protected void OnTimerIntervalElapse(object sender, EventArgs e)
        {
            try
            {
                bindDataGrid();
               // btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            }
        }
       
        protected void gvControllerStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Cells[5].Visible = true;
                    if (e.Row.Cells[5].Text.Trim() == "1")
                    {
                     e.Row.BackColor = System.Drawing.Color.LightCoral;
                     e.Row.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (e.Row.Cells[5].Text.Trim() == "0")
                    {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.SlateBlue;
                    }
                    e.Row.Cells[5].Visible = false;

                    //if (e.Row.Cells[5].Text.Trim() == "1")
                    //{ e.Row.BackColor = System.Drawing.Color.Red; }
                    //else if (e.Row.Cells[5].Text.Trim() == "0")
                    //{ e.Row.BackColor = System.Drawing.Color.LightGreen; }
                    //e.Row.Cells[5].Visible = false;
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            
            }
        }
       
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvControllerStatus.PageIndex = Convert.ToInt32(((DropDownList)gvControllerStatus.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvControllerStatus.PageIndex = gvControllerStatus.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvControllerStatus.PageIndex = gvControllerStatus.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerStatus");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                //string strsql = "";
                string strsql1 = "";
                DataTable dt1;
                SqlDataAdapter da1;

                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string strsql = " SELECT CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_FIRMWARE_VERSION_NO ,CTLR_CONN_STATUS " +
                                         " FROM ACS_CONTROLLER Where CTLR_ISDELETED = '0' ";

                    SqlCommand cmd = new SqlCommand(strsql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = 0;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    //if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                    //{
                    gvControllerStatus.DataSource = dt;
                    gvControllerStatus.DataBind();
                }
                //else
                //{
                //    String[,] values = { 
                //{"CTLR_ID~" +txtUserID.Text.Trim(), "S" },
                //{"CTLR_DESCRIPTION~" +txtLevelID.Text.Trim(), "S" }			
                // };
                //    DataTable _tempDT = new DataTable();
                //    Search _sc = new Search();
                //    if (_tempDT != null)
                //    { _tempDT.Rows.Clear(); }
                //    _tempDT = _sc.searchTable(values, dt);
                //    gvControllerStatus.DataSource = _tempDT;
                //    gvControllerStatus.DataBind();
                //}
                else
                {
                    String[,] values = { 
				{"CTLR_ID-" +txtUserID.Text.Trim(), "S" },
				{"CTLR_DESCRIPTION-" +txtLevelID.Text.Trim(), "S" }			
				 };


                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    if (txtUserID.Text.ToString() != "" && txtLevelID.Text.ToString() == "")
                    {
                        strsql1 = " SELECT CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_FIRMWARE_VERSION_NO ,CTLR_CONN_STATUS" +
                                 " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' AND CTLR_ID Like '" + txtUserID.Text + "%'  order by CTLR_ID ASC";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else if (txtLevelID.Text.ToString() != "" && txtUserID.Text.ToString() == "")
                    {
                        strsql1 = " SELECT CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_FIRMWARE_VERSION_NO ,CTLR_CONN_STATUS " +
                            " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' AND CTLR_DESCRIPTION Like '%" + txtLevelID.Text + "%' order by CTLR_ID ASC";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else
                    {
                        strsql1 = " SELECT CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_FIRMWARE_VERSION_NO ,CTLR_CONN_STATUS " +
                           " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' AND CTLR_ID Like '" + txtUserID.Text + "%' AND CTLR_DESCRIPTION Like '%" + txtLevelID.Text + "%' order by CTLR_ID ASC ";
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
                    gvControllerStatus.DataSource = dt1;
                    gvControllerStatus.DataBind();
                }


                DropDownList ddl = (DropDownList)gvControllerStatus.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvControllerStatus.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvControllerStatus.PageIndex + 1).ToString();
                Label lblcount = (Label)gvControllerStatus.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvControllerStatus.DataSource).Rows.Count.ToString() + " Records.";
                if (gvControllerStatus.PageCount == 0)
                {
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvControllerStatus.PageIndex + 1 == gvControllerStatus.PageCount)
                {
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvControllerStatus.PageIndex == 0)
                {
                    ((Button)gvControllerStatus.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvControllerStatus.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvControllerStatus.PageSize * gvControllerStatus.PageIndex) + 1) + " to " + (gvControllerStatus.PageSize * (gvControllerStatus.PageIndex + 1));

                ((Label)gvControllerStatus.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvControllerStatus.PageSize * gvControllerStatus.PageIndex) + 1) + " to " + (((gvControllerStatus.PageSize * (gvControllerStatus.PageIndex + 1)) - 10) + gvControllerStatus.Rows.Count);

                gvControllerStatus.BottomPagerRow.Visible = true;

               // gvControllerStatus.Columns[5].Visible = false;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }
        }
    }
}