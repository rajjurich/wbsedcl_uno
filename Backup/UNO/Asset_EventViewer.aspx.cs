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
    public partial class Asset_EventViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    gvEvent.PageSize = 10;
                    bindDataGrid();
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Script", "HightlightMyTable();", true);
        }
        public DataTable getTable()
        {
            int type = 0;
            if (RBLDataType.SelectedValue.ToString() == "01")
                type = 1;
            if (RBLDataType.SelectedValue.ToString() == "02")
                type = 2;

            Hashtable ht = new Hashtable();
            ht.Add("@type", type);
            DataSet ds = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EVENT_VIEWER", ht);
            return ds.Tables[0];
        }

        public void bindDataGrid()
        {
            DataTable dt = getTable();

            gvEvent.DataSource = dt;
            gvEvent.DataBind();

            DropDownList ddl = (DropDownList)gvEvent.BottomPagerRow.FindControl("ddlPageNo");
            for (int i = 1; i <= gvEvent.PageCount; i++)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddl.SelectedValue = (gvEvent.PageIndex + 1).ToString();
            Label lblcount = (Label)gvEvent.BottomPagerRow.FindControl("lblTotal");
            lblcount.Text = ((DataTable)gvEvent.DataSource).Rows.Count.ToString() + " Records.";
            if (gvEvent.PageCount == 0)
            {
                ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            }
            if (gvEvent.PageIndex + 1 == gvEvent.PageCount)
            {
                ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            }
            if (gvEvent.PageIndex == 0)
            {
                ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
            }

            ((Label)gvEvent.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEvent.PageSize * gvEvent.PageIndex) + 1) + " to " + (((gvEvent.PageSize * (gvEvent.PageIndex + 1)) - 10) + gvEvent.Rows.Count);

            gvEvent.BottomPagerRow.Visible = true;

        }


        protected void CmdPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmdPause.Text == "Release")
                {
                    ctlTimer.Enabled = false;
                    CmdPause.Text = "Start";
                }
                else
                {
                    ctlTimer.Enabled = true;
                    CmdPause.Text = "Release";
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void OnTimerIntervalElapse(object sender, EventArgs e)
        {
            bindDataGrid();
        }

        protected void RBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindDataGrid();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = getTable();

                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {
                    gvEvent.DataSource = dt;
                    gvEvent.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"Event_Employee_Code~" +txtUserID.Text.Trim(), "S" },
                {"Name~" +txtLevelID.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEvent.DataSource = _tempDT;
                    gvEvent.DataBind();
                }
                DropDownList ddl = (DropDownList)gvEvent.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEvent.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEvent.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEvent.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEvent.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEvent.PageCount == 0)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEvent.PageIndex + 1 == gvEvent.PageCount)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEvent.PageIndex == 0)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvEvent.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEvent.PageSize * gvEvent.PageIndex) + 1) + " to " + (((gvEvent.PageSize * (gvEvent.PageIndex + 1)) - 10) + gvEvent.Rows.Count);

                gvEvent.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEvent.PageIndex = Convert.ToInt32(((DropDownList)gvEvent.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEvent.PageIndex = gvEvent.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEvent.PageIndex = gvEvent.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void gvEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        { 
        }


    }
}