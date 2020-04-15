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
using System.Collections.Specialized;
using System.Collections;

namespace UNO
{
    public partial class ActivityBrowser : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack == false)
                {

                    setControllers();
                    bindGrid();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
            }
        }

        public void setControllers()
        {
            cmbContrller.Items.Clear();
            cmbContrller.Items.Add(new ListItem("ALL", "-1"));
            cmbContrller.SelectedValue = "-1";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string _strsql = "select CTLR_ID,CTLR_DESCRIPTION from ACS_CONTROLLER where CTLR_ISDELETED='0' ";
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, conn);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        cmbContrller.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));

                    }
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
            }
        }

        public void bindGrid()
        {

            try
            {
                String _strWhere = "";
                DataTable _dt = new DataTable();
                if (cmbContrller.SelectedValue.ToString() == "-1")
                { _strWhere = ""; }
                else
                { _strWhere = " and C.CTLR_ID='" + cmbContrller.SelectedValue.ToString() + "'"; }

                String _tableName = "EVENT_LOG_";
                if (DateTime.Now.Date.Month < 10)
                { _tableName = _tableName + "0" + DateTime.Now.Date.Month + DateTime.Now.Date.Year; }
                else
                { _tableName = _tableName + DateTime.Now.Date.Month + DateTime.Now.Date.Year; }

                //string strsql = "select DATETIME,C.CTLR_DESCRIPTION,EVENT_DESC,STATUS,RETRY,USER_ID " +
                //                "from " + _tableName + " E LEFT OUTER JOIN ACS_CONTROLLER C ON E.CTLR_ID=C.CTLR_ID " +
                //                "where Convert(varchar(20), E.[DATETIME], 101)= Convert(varchar(20), getdate(), 101) " + _strWhere + "  order by E.DATETIME desc ";

                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_GetActivityData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tableName", _tableName);
                    cmd.Parameters.AddWithValue("@strwhereclause", _strWhere);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = 0;
                   
                    da.Fill(_dt);
                    //cmd.ExecuteNonQuery();
                }


                catch (Exception ex)
                {
                    //throw ex;
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }




//                string strsql = @"select DATETIME,C.CTLR_DESCRIPTION,substring(EVENT_DESC,0,len(EVENT_DESC)-3)+
//                              case 
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0101' then 'Device Status' 
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0103' then 'Set Datetime'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0105' then 'Get Datetime'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='010B' then 'Device Configuration'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0111' then 'Access level'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0113' then 'Timezones'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0115' then 'Holiday List'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0117' then 'Access Point'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0119' then 'Input Point'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='011B' then 'Output Point'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='011D' then 'User Profile'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0121' then 'Anti-Passback'
//                              when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0123' then 'Reset All Anti-Passback'
//                              else substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))
//                              end as EVENT_DESC
//                    
//                            ,STATUS,RETRY,USER_ID " +
//                            "from " + _tableName + " E LEFT OUTER JOIN ACS_CONTROLLER C ON E.CTLR_ID=C.CTLR_IP " +
//                            "where Convert(varchar(20), E.[DATETIME], 101)= Convert(varchar(20), getdate(), 101) " + _strWhere + "  order by E.DATETIME desc ";



                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //DataTable _dt = new DataTable();
                //_dt = getDataTable(_dt, conn);

                if (_dt.Rows.Count != 0)
                {
                    gvActivity.DataSource = _dt;
                    gvActivity.DataBind();
                }
                else
                {
                    gvActivity.DataSource = null;
                    gvActivity.DataBind();
                }
                DropDownList ddl = (DropDownList)gvActivity.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvActivity.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvActivity.PageIndex + 1).ToString();
                Label lblcount = (Label)gvActivity.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvActivity.DataSource).Rows.Count.ToString() + " Records.";
                if (gvActivity.PageCount == 0)
                {
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvActivity.PageIndex + 1 == gvActivity.PageCount)
                {
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvActivity.PageIndex == 0)
                {
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvActivity.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvActivity.PageSize * gvActivity.PageIndex) + 1) + " to " + (gvActivity.PageSize * (gvActivity.PageIndex + 1));

                ((Label)gvActivity.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvActivity.PageSize * gvActivity.PageIndex) + 1) + " to " + (((gvActivity.PageSize * (gvActivity.PageIndex + 1)) - 10) + gvActivity.Rows.Count);

                gvActivity.BottomPagerRow.Visible = true;

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
                gvActivity.DataSource = null;
                gvActivity.DataBind();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
                return null;
            }
        }

        public string getValue(string _strQuery, SqlConnection _sqlconn)
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
                return _ds.Tables[0].Rows[0][0].ToString().Trim();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
                return "";
            }
        }

        protected void OnTimerIntervalElapse(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void CmdPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmdPause.Text == "Pause")
                {
                    ctlTimer.Enabled = false;
                    CmdPause.Text = "Start";
                }
                else
                {
                    ctlTimer.Enabled = true;
                    CmdPause.Text = "Pause";
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");

            }

        }

        protected void cmbContrller_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String _strWhere = "";
                DataTable dt = new DataTable();
                if (cmbContrller.SelectedValue.ToString() == "-1")
                { _strWhere = ""; }
                else
                { _strWhere = " and C.CTLR_ID='" + cmbContrller.SelectedValue.ToString() + "'"; }

                String _tableName = "EVENT_LOG_";
                if (DateTime.Now.Date.Month < 10)
                { _tableName = _tableName + "0" + DateTime.Now.Date.Month + DateTime.Now.Date.Year; }
                else
                { _tableName = _tableName + DateTime.Now.Date.Month + DateTime.Now.Date.Year; }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_GetActivityData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tableName", _tableName);
                    cmd.Parameters.AddWithValue("@strwhereclause", _strWhere);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = 0;

                    da.Fill(dt);
                    //cmd.ExecuteNonQuery();




                }


                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {
                    gvActivity.DataSource = dt;
                    gvActivity.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"CTLR_DESCRIPTION-" +txtUserID.Text.Trim(), "S" },
                {"EVENT_DESC-" +txtLevelID.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvActivity.DataSource = _tempDT;
                    gvActivity.DataBind();
                }
                DropDownList ddl = (DropDownList)gvActivity.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvActivity.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvActivity.PageIndex + 1).ToString();
                Label lblcount = (Label)gvActivity.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvActivity.DataSource).Rows.Count.ToString() + " Records.";
                if (gvActivity.PageCount == 0)
                {
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvActivity.PageIndex + 1 == gvActivity.PageCount)
                {
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvActivity.PageIndex == 0)
                {
                    ((Button)gvActivity.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvActivity.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvActivity.PageSize * gvActivity.PageIndex) + 1) + " to " + (gvActivity.PageSize * (gvActivity.PageIndex + 1));

                ((Label)gvActivity.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvActivity.PageSize * gvActivity.PageIndex) + 1) + " to " + (((gvActivity.PageSize * (gvActivity.PageIndex + 1)) - 10) + gvActivity.Rows.Count);

                gvActivity.BottomPagerRow.Visible = true;

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

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvActivity.PageIndex = Convert.ToInt32(((DropDownList)gvActivity.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvActivity.PageIndex = gvActivity.PageIndex - 1;
                bindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvActivity.PageIndex = gvActivity.PageIndex + 1;
                bindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
            }
        }

        protected void gvActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int i = 0;
                    if (e.Row.Cells[3].Text.Trim() == "Successful")
                    { e.Row.BackColor = System.Drawing.Color.Green; }
                    else
                    { e.Row.BackColor = System.Drawing.Color.Red; }
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcitvityBrowser");
            }
        }

    }
}