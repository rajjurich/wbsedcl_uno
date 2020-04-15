using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;

namespace UNO
{
    public partial class TE_ActivityAssignment : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string ProjectID = "";
        string MilestoneId = "";
        string WBSID = "";
        string ActivityID = "";
        string Date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectID"].ToString();
            MilestoneId = Request.QueryString["MilestoneId"].ToString();
            WBSID = Request.QueryString["WBSID"].ToString();
            ActivityID = Request.QueryString["ActivityID"].ToString();

            if (!IsPostBack)
            {
                BindgvAssignment(ProjectID, MilestoneId, WBSID, ActivityID);
            }
        }

        private void BindgvAssignment(string ProjectID, string MilestoneId, string WBSID, string ActivityID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select a.ID, b.EOD_CATEGORY_ID, a.ResourceID, a.PlannedStartDate, a.PlannedEndDate, a.TotalManHours from TE_AssignedActivity a inner join ENT_EMPLOYEE_OFFICIAL_DTLS b on a.ResourceID = b.EOD_EMPID where a.DeleteFlag is null and a.ProjectId = '" + ProjectID + "' and a.MilestoneId = '" + MilestoneId + "' and a.WBSID = '" + WBSID + "' and a.ActivityId = '" + ActivityID + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvAssignment.DataSource = dt;


                DataTable current = new DataTable();
                current.Columns.Add(new DataColumn("Flag", typeof(string)));
                current.Columns.Add(new DataColumn("ID", typeof(string)));
                current.Columns.Add(new DataColumn("ResourceType", typeof(string)));
                current.Columns.Add(new DataColumn("EmployeeCode", typeof(string)));
                current.Columns.Add(new DataColumn("PlannedStartDate", typeof(string)));
                current.Columns.Add(new DataColumn("PlannedEndDate", typeof(string)));
                current.Columns.Add(new DataColumn("Hours", typeof(string)));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = current.NewRow();
                    dr["Flag"] = "EDIT";
                    dr["ID"] = dt.Rows[i]["ID"].ToString();
                    dr["ResourceType"] = dt.Rows[i]["EOD_CATEGORY_ID"].ToString();
                    dr["EmployeeCode"] = dt.Rows[i]["ResourceID"].ToString();
                    dr["PlannedStartDate"] = (DateTime.ParseExact(dt.Rows[i]["PlannedStartDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    dr["PlannedEndDate"] = (DateTime.ParseExact(dt.Rows[i]["PlannedEndDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    dr["Hours"] = dt.Rows[i]["TotalManHours"].ToString();
                    current.Rows.Add(dr);
                }
                ViewState["Assignment"] = current;
                gvAssignment.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void ddlResourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlResourceType = (DropDownList)sender;
                GridViewRow gvRow = (GridViewRow)ddlResourceType.NamingContainer;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT EPD_EMPID,EPD_FIRST_NAME + ' ' + EPD_LAST_NAME as Name FROM dbo.ENT_EMPLOYEE_PERSONAL_DTLS INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS ON EOD_EMPID = EPD_EMPID WHERE EOD_CATEGORY_ID='" + ddlResourceType.SelectedValue + "' and EPD_ISDELETED = 'False' AND EPD_IsDeleted = 'FALSE'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                DropDownList ddlEmployee = (DropDownList)gvRow.FindControl("ddlEmployee");
                ddlEmployee.DataTextField = "Name";
                ddlEmployee.DataValueField = "EPD_EMPID";
                ddlEmployee.DataSource = dt;
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select One", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable Current = (DataTable)ViewState["Assignment"];
                    DropDownList ddlResourceType = (DropDownList)e.Row.FindControl("ddlResourceType");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select * from TE_ActivityResourceType a inner join ENT_CATEGORY b on a.ResourceType = b.CAT_CATEGORY_ID where a.DeleteFlag is null and a.ActivityID = '" + ActivityID + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    ddlResourceType.DataTextField = "CAT_CATEGORY_DESCRIPTION";
                    ddlResourceType.DataValueField = "CAT_CATEGORY_ID";
                    ddlResourceType.DataSource = dt;
                    ddlResourceType.DataBind();
                    ddlResourceType.Items.Insert(0, new ListItem("Select One", "0"));

                    ddlResourceType.SelectedValue = Current.Rows[e.Row.RowIndex]["ResourceType"].ToString();
                    ddlResourceType_SelectedIndexChanged(ddlResourceType, new EventArgs());
                    ((Label)e.Row.FindControl("lblFlag")).Text = Current.Rows[e.Row.RowIndex]["Flag"].ToString();
                    ((Label)e.Row.FindControl("lblID")).Text = Current.Rows[e.Row.RowIndex]["ID"].ToString();
                    ((DropDownList)e.Row.FindControl("ddlEmployee")).SelectedValue = Current.Rows[e.Row.RowIndex]["EmployeeCode"].ToString();
                    ((TextBox)e.Row.FindControl("txtPlannedStartDate")).Text = Current.Rows[e.Row.RowIndex]["PlannedStartDate"].ToString();
                    ((TextBox)e.Row.FindControl("txtPlannedEndDate")).Text = Current.Rows[e.Row.RowIndex]["PlannedEndDate"].ToString();
                    ((TextBox)e.Row.FindControl("txtHours")).Text = Current.Rows[e.Row.RowIndex]["Hours"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Assignment"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["Assignment"];
                    DataTable dt = dtCurrentTable.Clone();

                    for (int i = 0; i < gvAssignment.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Flag"] = ((Label)gvAssignment.Rows[i].FindControl("lblFlag")).Text;
                        dr["ID"] = ((Label)gvAssignment.Rows[i].FindControl("lblID")).Text;
                        dr["ResourceType"] = ((DropDownList)gvAssignment.Rows[i].FindControl("ddlResourceType")).SelectedValue;
                        dr["EmployeeCode"] = ((DropDownList)gvAssignment.Rows[i].FindControl("ddlEmployee")).SelectedValue;
                        dr["PlannedStartDate"] = ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedStartDate")).Text;
                        dr["PlannedEndDate"] = ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedEndDate")).Text;
                        dr["Hours"] = ((TextBox)gvAssignment.Rows[i].FindControl("txtHours")).Text;
                        dt.Rows.Add(dr);
                    }
                    DataRow dr1 = dt.NewRow();
                    dr1["Flag"] = "NEW";
                    dr1["ID"] = "0";
                    dr1["ResourceType"] = "0";
                    dr1["EmployeeCode"] = "0";
                    dr1["PlannedStartDate"] = "";
                    dr1["PlannedEndDate"] = "";
                    dr1["Hours"] = "";

                    dt.Rows.Add(dr1);
                    dtCurrentTable = dt;
                    gvAssignment.DataSource = dtCurrentTable;
                    ViewState["Assignment"] = dtCurrentTable;
                    gvAssignment.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }

        }

        protected void gvAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    DataTable dtCurrent = (DataTable)ViewState["Assignment"];
                    DataTable dt = dtCurrent.Clone();
                    for (int i = 0; i < gvAssignment.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Flag"] = ((Label)gvAssignment.Rows[i].FindControl("lblFlag")).Text;
                        dr["ID"] = ((Label)gvAssignment.Rows[i].FindControl("lblID")).Text;
                        dr["ResourceType"] = ((DropDownList)gvAssignment.Rows[i].FindControl("ddlResourceType")).SelectedValue;
                        dr["EmployeeCode"] = ((DropDownList)gvAssignment.Rows[i].FindControl("ddlEmployee")).SelectedValue;
                        dr["PlannedStartDate"] = ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedStartDate")).Text;
                        dr["PlannedEndDate"] = ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedEndDate")).Text;
                        dr["Hours"] = ((TextBox)gvAssignment.Rows[i].FindControl("txtHours")).Text;
                        dt.Rows.Add(dr);
                    }

                    LinkButton lnkDelete = (LinkButton)e.CommandSource;
                    GridViewRow gvRow = (GridViewRow)lnkDelete.NamingContainer;
                    if (((Label)gvRow.FindControl("lblFlag")).Text == "EDIT")
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("update TE_AssignedActivity set DeleteFlag = 1, DeleteDate = GETDATE() where ID = '" + ((Label)gvRow.FindControl("lblID")).Text + "'", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        dt.Rows.RemoveAt(gvRow.RowIndex);
                    }
                    else
                    {
                        dt.Rows.RemoveAt(gvRow.RowIndex);
                    }
                    dtCurrent = dt;
                    ViewState["Assignment"] = dtCurrent;
                    gvAssignment.DataSource = dtCurrent;
                    gvAssignment.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
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
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    if (((Label)gvAssignment.Rows[i].FindControl("lblFlag")).Text == "EDIT")
                    {
                        SqlCommand cmd = new SqlCommand("update TE_AssignedActivity set ProjectId = '" + ProjectID + "', MilestoneId = '" + MilestoneId + "', WBSID = '" + WBSID + "', ActivityId = '" + ActivityID + "', ResourceID = '" + ((DropDownList)gvAssignment.Rows[i].FindControl("ddlEmployee")).SelectedValue + "', PlannedStartDate = convert(datetime,'" + ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedStartDate")).Text + "',103), PlannedEndDate = convert(datetime,'" + ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedEndDate")).Text + "',103), TotalManHours = '" + ((TextBox)gvAssignment.Rows[i].FindControl("txtHours")).Text + "', Progess = 'SCHEDULED' where ID = '" + ((Label)gvAssignment.Rows[i].FindControl("lblID")).Text + "'", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        //vaibhav new

                   

                        //update te_acticityResourceType
                        SqlCommand cmd1 = new SqlCommand("sp_update_activityResourceType", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@emp_category", SqlDbType.VarChar).Value = ((DropDownList)gvAssignment.Rows[i].FindControl("ddlResourceType")).SelectedValue;
                        cmd1.Parameters.Add("@activity_id", SqlDbType.BigInt).Value = ActivityID;
                        cmd1.ExecuteNonQuery();

                        //update te_activity
                        SqlCommand cmd2 = new SqlCommand("sp_update_activityDates", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@activity_id", SqlDbType.BigInt).Value = ActivityID;
                        cmd2.ExecuteNonQuery();

                        //update te_WBS
                        SqlCommand cmd3 = new SqlCommand("sp_update_WBSDates", conn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@wbsid", SqlDbType.BigInt).Value = WBSID;
                        cmd3.ExecuteNonQuery();

                        //update te_Milestone
                        SqlCommand cmd4 = new SqlCommand("sp_update_MilestoneDates", conn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add("@milestoneId", SqlDbType.BigInt).Value = MilestoneId;
                        cmd4.ExecuteNonQuery();

                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("insert into TE_AssignedActivity (ProjectId, MilestoneId, WBSID, ActivityId, ResourceID, PlannedStartDate, PlannedEndDate, TotalManHours, Progess) values ('" + ProjectID + "', '" + MilestoneId + "', '" + WBSID + "', '" + ActivityID + "', '" + ((DropDownList)gvAssignment.Rows[i].FindControl("ddlEmployee")).SelectedValue + "', convert(datetime,'" + ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedStartDate")).Text + "', 103), convert(datetime,'" + ((TextBox)gvAssignment.Rows[i].FindControl("txtPlannedEndDate")).Text + "', 103), '" + ((TextBox)gvAssignment.Rows[i].FindControl("txtHours")).Text + "', 'SCHEDULED')", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        //vaibhav new

                        //update te_acticityResourceType
                        SqlCommand cmd1 = new SqlCommand("sp_update_activityResourceType", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@emp_category", SqlDbType.VarChar).Value = ((DropDownList)gvAssignment.Rows[i].FindControl("ddlResourceType")).SelectedValue;
                        cmd1.Parameters.Add("@activity_id", SqlDbType.BigInt).Value = ActivityID;
                        cmd1.ExecuteNonQuery();

                        //update te_activity
                        SqlCommand cmd2 = new SqlCommand("sp_update_activityDates", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@activity_id", SqlDbType.BigInt).Value = ActivityID;
                        cmd2.ExecuteNonQuery();

                        //update te_WBS
                        SqlCommand cmd3 = new SqlCommand("sp_update_WBSDates", conn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@wbsid", SqlDbType.BigInt).Value = WBSID;
                        cmd3.ExecuteNonQuery();

                        //update te_Milestone
                        SqlCommand cmd4 = new SqlCommand("sp_update_MilestoneDates", conn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add("@milestoneId", SqlDbType.BigInt).Value = MilestoneId;
                        cmd4.ExecuteNonQuery();

                        ////update te_Projects
                        //SqlCommand cmd5 = new SqlCommand("sp_update_WBSDates", conn);
                        //cmd5.CommandType = CommandType.StoredProcedure;
                        //cmd5.Parameters.Add("@activity_id", SqlDbType.BigInt).Value = ActivityID;
                        //cmd5.ExecuteNonQuery();




                    }
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }
}