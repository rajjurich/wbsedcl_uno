using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace UNO
{
    public partial class ScheduleSettingView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SchedulerSettings.aspx");
        }

        void bindDataGrid()
        {
            try
            {
                string strsql = " SELECT SCHEDULER_TASK_TYPE,SCHEDULER_DESCRIPTION,SCHEDULER_FREQUENCY,SUBSTRING(CONVERT(VARCHAR,SCHEDULER_TIME,114),0,6) AS SCHEDULER_TIME " +
                                " FROM SCHEDULER Where SCHEDULER_ISDELETED = '0' ORDER BY CASE WHEN ISNUMERIC(SCHEDULER_TASK_TYPE) = 1 THEN 0 ELSE 1 END, " +
                                " CASE WHEN ISNUMERIC(SCHEDULER_TASK_TYPE) = 1 THEN CAST(SCHEDULER_TASK_TYPE AS INT) ELSE 0 END ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvScheduleSettings.PageSize = 5;
                gvScheduleSettings.DataSource = dt;
                gvScheduleSettings.DataBind();
                if (gvScheduleSettings.Rows.Count >= 1)
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
                throw ex;
            }

        }
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            gvScheduleSettings.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            bindDataGrid();
        }

        protected void gvScheduleSettings_DataBound(object sender, EventArgs e)
        {
            string strsql = " SELECT SCHEDULER_TASK_TYPE,SCHEDULER_DESCRIPTION,SCHEDULER_FREQUENCY,SCHEDULER_TIME" +
                            " FROM SCHEDULER Where SCHEDULER_ISDELETED = '0' ORDER BY CASE WHEN ISNUMERIC(SCHEDULER_TASK_TYPE) = 1 THEN 0 ELSE 1 END, " +
                            " CASE WHEN ISNUMERIC(SCHEDULER_TASK_TYPE) = 1 THEN CAST(SCHEDULER_TASK_TYPE AS INT) ELSE 0 END ";
            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcnt = dt.Rows.Count;

            if (gvScheduleSettings.Rows.Count > 0)
            {
                pager.Visible = true;

                if (ViewState["PageSize"] != null)
                {
                    DropDownList ddPagesize = gvScheduleSettings.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                }

                Label lblCount = gvScheduleSettings.BottomPagerRow.FindControl("lblPageCount") as Label;
                int totRecords = (gvScheduleSettings.PageIndex * gvScheduleSettings.PageSize) + gvScheduleSettings.PageSize;                
                int totCustomerCount = recordcnt;
                totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
                lblPageCount.Text = ((gvScheduleSettings.PageIndex * gvScheduleSettings.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) + " of " + totCustomerCount.ToString();
                gvScheduleSettings.BottomPagerRow.Visible = true;

            }
            else
            {
                pager.Visible = false;
            }
        }

        protected void gvScheduleSettings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvScheduleSettings.PageIndex = e.NewPageIndex;
            bindDataGrid();
        }

        protected void gvScheduleSettings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvScheduleSettings.EditIndex = e.NewEditIndex;
            string ID = gvScheduleSettings.Rows[gvScheduleSettings.EditIndex].Cells[2].Text;
            Response.Redirect("SchedulerSettings.aspx?id=" + ID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //bool Check = false;
            for (int i = 0; i < gvScheduleSettings.Rows.Count; i++)
            {
                SqlConnection conn = new SqlConnection(AccessController.m_connecton);
                conn.Open();
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;
                SqlTransaction trans;
                trans = conn.BeginTransaction();
                try
                {
                    objcmd.Transaction = trans;
                    CheckBox delrows = (CheckBox)gvScheduleSettings.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                       // Check = true;
                        objcmd.CommandText = "Update SCHEDULER set SCHEDULER_ISDELETED = '1',SCHEDULER_DELETEDDATE = convert(datetime,'" + DateTime.Now + "',103) where SCHEDULER_TASK_TYPE = '" + gvScheduleSettings.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();                     

                        trans.Commit();
                        this.messageDiv.InnerHtml = "Record Deleted Successfully";
                        string someScript = "";
                        someScript = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                        bindDataGrid(); 
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }

            }            
                        
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            string strsql = " SELECT SCHEDULER_TASK_TYPE,SCHEDULER_DESCRIPTION,SCHEDULER_FREQUENCY,SUBSTRING(CONVERT(VARCHAR,SCHEDULER_TIME,114),0,6) AS SCHEDULER_TIME " +
                            " FROM SCHEDULER Where SCHEDULER_ISDELETED = '0' ORDER BY CASE WHEN ISNUMERIC(SCHEDULER_TASK_TYPE) = 1 THEN 0 ELSE 1 END, " +
                            " CASE WHEN ISNUMERIC(SCHEDULER_TASK_TYPE) = 1 THEN CAST(SCHEDULER_TASK_TYPE AS INT) ELSE 0 END ";
            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
            {
                cmdReset_Click(sender, e);
            }
            else
            {
                String[,] values = { 
				{"SCHEDULER_TASK_TYPE-" +txtCompanyId.Text.Trim(), "S" },
				{"SCHEDULER_DESCRIPTION-" +txtCompanyName.Text.Trim(), "S" }			
				 };
                DataTable _tempDT = new DataTable();
                Search _sc = new Search();
                if (_tempDT != null)
                { _tempDT.Rows.Clear(); }
                _tempDT = _sc.searchTable(values, dt);
                gvScheduleSettings.DataSource = _tempDT;
                gvScheduleSettings.DataBind();
            }

        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyId.Text = "";
            txtCompanyName.Text = "";
            bindDataGrid();

        }

    }
}