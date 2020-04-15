using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace UNO
{
    public partial class TE_Activity : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindddlProject();
            }
        }

        private void BindddlProject()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from TE_Projects where DeleteFlag is null and ManagerEmpCode = '" + Session["uid"].ToString() + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlProject.DataValueField = "ID";
                ddlProject.DataTextField = "Description";
                ddlProject.DataSource = dt;
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("Select One", "0"));

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void BindddlMilestone(string ProjectID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from TE_Milestone where DeleteFlag is null and ProjectID = '" + ProjectID + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlMilestone.DataValueField = "ID";
                ddlMilestone.DataTextField = "Description";
                ddlMilestone.DataSource = dt;
                ddlMilestone.DataBind();
                ddlMilestone.Items.Insert(0, new ListItem("Select One", "0"));

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void BindddlWBS(string ProjectID, string MilestoneID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from TE_WBS where DeleteFlag is null and ProjectID = '" + ProjectID + "' and MilestoneID = '" + MilestoneID + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlWBS.DataValueField = "ID";
                ddlWBS.DataTextField = "Description";
                ddlWBS.DataSource = dt;
                ddlWBS.DataBind();
                ddlWBS.Items.Insert(0, new ListItem("Select One", "0"));

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void BindgvActivity(string ProjectID, string MilestoneID, string WBSID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from TE_Activity where DeleteFlag is null and ProjectId = '" + ProjectID + "' and MilestoneId = '" + MilestoneID + "' and WBSID = '" + WBSID + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvActivity.DataSource = dt;
                gvActivity.DataBind();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }


        private void SetInitialRow()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ResourceType", typeof(string)));
                //dt.Columns.Add(new DataColumn("PlannedStartDate", typeof(string)));
                //dt.Columns.Add(new DataColumn("PlannedEndDate", typeof(string)));
                dt.Columns.Add(new DataColumn("Hours", typeof(string)));
                dr = dt.NewRow();
                dr["ResourceType"] = string.Empty;
                //dr["PlannedStartDate"] = string.Empty;
                //dr["PlannedEndDate"] = string.Empty;
                dr["Hours"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;

                gvResource.DataSource = dt;
                gvResource.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }


        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        DropDownList ddlResourceTypeAdd = (DropDownList)gvResource.Rows[rowIndex].FindControl("ddlResourceTypeAdd");

                        //TextBox txtPlannedStartDateAdd = (TextBox)gvResource.Rows[rowIndex].FindControl("txtPlannedStartDateAdd");
                        //TextBox txtPlannedEndDateAdd = (TextBox)gvResource.Rows[rowIndex].FindControl("txtPlannedEndDateAdd");
                        TextBox txtHoursAdd = (TextBox)gvResource.Rows[rowIndex].FindControl("txtHoursAdd");

                        //drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["ResourceType"] = ddlResourceTypeAdd.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["PlannedStartDate"] = txtPlannedStartDateAdd.Text;
                        //dtCurrentTable.Rows[i - 1]["PlannedEndDate"] = txtPlannedEndDateAdd.Text;
                        dtCurrentTable.Rows[i - 1]["Hours"] = txtHoursAdd.Text;

                        rowIndex++;

                    }
                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvResource.DataSource = dtCurrentTable;
                    gvResource.DataBind();
                }
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlResourceTypeAdd = (DropDownList)gvResource.Rows[rowIndex].FindControl("ddlResourceTypeAdd");
                        //TextBox txtPlannedStartDateAdd = (TextBox)gvResource.Rows[rowIndex].FindControl("txtPlannedStartDateAdd");
                        //TextBox txtPlannedEndDateAdd = (TextBox)gvResource.Rows[rowIndex].FindControl("txtPlannedEndDateAdd");
                        TextBox txtHoursAdd = (TextBox)gvResource.Rows[rowIndex].FindControl("txtHoursAdd");

                        ddlResourceTypeAdd.SelectedValue = dt.Rows[i]["ResourceType"].ToString();
                        //txtPlannedStartDateAdd.Text = (DateTime.ParseExact(dt.Rows[i]["PlannedStartDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                        //txtPlannedEndDateAdd.Text = (DateTime.ParseExact(dt.Rows[i]["PlannedEndDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                        txtHoursAdd.Text = dt.Rows[i]["Hours"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindddlMilestone(ddlProject.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void ddlMilestone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindddlWBS(ddlProject.SelectedValue, ddlMilestone.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void ddlWBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindgvActivity(ddlProject.SelectedValue, ddlMilestone.SelectedValue, ddlWBS.SelectedValue);
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
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID = '" + ddlProject.SelectedValue + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (dt.Rows[0][0].ToString() == "BASELINED")
                {
                    lblMessageOKPopUp.Text = "This Project is already Baselined. Cannot add Activities now.";
                    mpeOKPopUp.Show();
                }
                else
                {
                    SetInitialRow();
                    txtDescriptionAdd.Text = "";
                    mpeAddActivity.Show();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnAddResource_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void gvResource_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select * from ENT_CATEGORY", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }


                    int rowNo = e.Row.RowIndex;
                    DataTable CurrentTable = (DataTable)ViewState["CurrentTable"];
                    DropDownList ddlResourceTypeAdd = (DropDownList)e.Row.FindControl("ddlResourceTypeAdd");
                    ddlResourceTypeAdd.DataValueField = "CAT_CATEGORY_ID";
                    ddlResourceTypeAdd.DataTextField = "CAT_CATEGORY_DESCRIPTION";
                    ddlResourceTypeAdd.DataSource = dt;
                    ddlResourceTypeAdd.DataBind();
                    ddlResourceTypeAdd.Items.Insert(0, new ListItem("Select One", "0"));

                    if (CurrentTable.Rows[rowNo]["ResourceType"].ToString() == string.Empty)
                    {
                        ddlResourceTypeAdd.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlResourceTypeAdd.SelectedValue = CurrentTable.Rows[rowNo]["ResourceType"].ToString();
                    }
                    //TextBox txtPlannedStartDateAdd = (TextBox)e.Row.FindControl("txtPlannedStartDateAdd");
                    //txtPlannedStartDateAdd.Text = CurrentTable.Rows[rowNo]["PlannedStartDate"].ToString();
                    //TextBox txtPlannedEndDateAdd = (TextBox)e.Row.FindControl("txtPlannedEndDateAdd");
                    //txtPlannedEndDateAdd.Text = CurrentTable.Rows[rowNo]["PlannedEndDate"].ToString();
                    TextBox txtHoursAdd = (TextBox)e.Row.FindControl("txtHoursAdd");
                    txtHoursAdd.Text = CurrentTable.Rows[rowNo]["Hours"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvResource_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveResource")
                {
                    int rowNo = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlResourceTypeAdd = (DropDownList)gvResource.Rows[i].FindControl("ddlResourceTypeAdd");
                        //TextBox txtPlannedStartDateAdd = (TextBox)gvResource.Rows[i].FindControl("txtPlannedStartDateAdd");
                        //TextBox txtPlannedEndDateAdd = (TextBox)gvResource.Rows[i].FindControl("txtPlannedEndDateAdd");
                        TextBox txtHoursAdd = (TextBox)gvResource.Rows[i].FindControl("txtHoursAdd");

                        dt.Rows[i]["ResourceType"] = ddlResourceTypeAdd.SelectedValue;
                        //dt.Rows[i]["PlannedStartDate"] = txtPlannedStartDateAdd.Text;
                        //dt.Rows[i]["PlannedEndDate"] = txtPlannedEndDateAdd.Text;
                        dt.Rows[i]["Hours"] = txtHoursAdd.Text;
                    }
                    dt.Rows.RemoveAt(rowNo);
                    ViewState["CurrentTable"] = dt;
                    gvResource.DataSource = dt;
                    gvResource.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvActivity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ActivityID = e.CommandArgument.ToString();
            try
            {
                if (e.CommandName == "Modify")
                {
                    lblActivityIdEdit.Text = e.CommandArgument.ToString();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID = '" + ddlProject.SelectedValue + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    if (dt.Rows[0][0].ToString() == "BASELINED")
                    {
                        lblMessageOKPopUp.Text = "This Project is already Baselined. Cannot modify Activities now.";
                        mpeOKPopUp.Show();
                    }
                    else
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlCommand cmd1 = new SqlCommand("Select * from TE_ActivityResourceType where DeleteFlag is null and ActivityID = '" + ActivityID + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        SqlCommand cmd2 = new SqlCommand("Select * from TE_Activity where DeleteFlag is null and ID = '" + ActivityID + "'", conn);
                        cmd2.CommandType = CommandType.Text;
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        txtDescriptionEdit.Text = dt2.Rows[0]["Description"].ToString();
                        gvResourceEdit.DataSource = dt1;



                        DataTable ResourceEdit = new DataTable();
                        ResourceEdit.Columns.Add(new DataColumn("FlagEdit", typeof(string)));
                        ResourceEdit.Columns.Add(new DataColumn("ResourceID", typeof(string)));
                        ResourceEdit.Columns.Add(new DataColumn("ResourceType", typeof(string)));
                        //ResourceEdit.Columns.Add(new DataColumn("PlannedStartDate", typeof(string)));
                        //ResourceEdit.Columns.Add(new DataColumn("PlannedEndDate", typeof(string)));
                        ResourceEdit.Columns.Add(new DataColumn("Hours", typeof(string)));
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            DataRow dr = ResourceEdit.NewRow();
                            dr["FlagEdit"] = "EDIT";
                            dr["ResourceID"] = dt1.Rows[i]["ID"].ToString();
                            dr["ResourceType"] = dt1.Rows[i]["ResourceType"].ToString();
                            //dr["PlannedStartDate"] = (DateTime.ParseExact(dt1.Rows[i]["PlannedStartDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                            //dr["PlannedEndDate"] = (DateTime.ParseExact(dt1.Rows[i]["PlannedEndDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                            dr["Hours"] = dt1.Rows[i]["HoursPerDay"].ToString();
                            ResourceEdit.Rows.Add(dr);

                        }
                        ViewState["ResourceEdit"] = ResourceEdit;
                        gvResourceEdit.DataBind();
                        mpeEditActivity.Show();
                    }
                }
                if (e.CommandName == "Remove")
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID = '" + ddlProject.SelectedValue + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    if (dt.Rows[0][0].ToString() == "BASELINED")
                    {
                        lblMessageOKPopUp.Text = "This Project is already Baselined. Cannot delete Activities now.";
                        mpeOKPopUp.Show();
                    }
                    else
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd1 = new SqlCommand("update TE_Activity set DeleteFlag = 1, DeleteDate = GETDATE() where ID = '" + ActivityID + "';update TE_ActivityResourceType set DeleteFlag = 1, DeleteDate = GETDATE() where ActivityID = '" + ActivityID + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        BindgvActivity(ddlProject.SelectedValue, ddlMilestone.SelectedValue, ddlWBS.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvResourceEdit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable dt = (DataTable)ViewState["ResourceEdit"];
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select * from ENT_CATEGORY", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    DropDownList ddlResourceTypeEdit = (DropDownList)e.Row.FindControl("ddlResourceTypeEdit");
                    ddlResourceTypeEdit.DataValueField = "CAT_CATEGORY_ID";
                    ddlResourceTypeEdit.DataTextField = "CAT_CATEGORY_DESCRIPTION";
                    ddlResourceTypeEdit.DataSource = dt1;
                    ddlResourceTypeEdit.DataBind();
                    ddlResourceTypeEdit.Items.Insert(0, new ListItem("Select One", "0"));

                    if (dt.Rows[e.Row.RowIndex]["FlagEdit"].ToString() == "EDIT")
                    {
                        ((Label)e.Row.FindControl("lblFlagEdit")).Text = "EDIT";
                        ((Label)e.Row.FindControl("lblResourceID")).Text = dt.Rows[e.Row.RowIndex]["ResourceID"].ToString();
                        ddlResourceTypeEdit.SelectedValue = dt.Rows[e.Row.RowIndex]["ResourceType"].ToString();
                        //TextBox txtPlannedStartDateEdit = (TextBox)e.Row.FindControl("txtPlannedStartDateEdit");
                        //txtPlannedStartDateEdit.Text = dt.Rows[e.Row.RowIndex]["PlannedStartDate"].ToString();
                        //TextBox txtPlannedEndDateEdit = (TextBox)e.Row.FindControl("txtPlannedEndDateEdit");
                        //txtPlannedEndDateEdit.Text = dt.Rows[e.Row.RowIndex]["PlannedEndDate"].ToString();
                        TextBox txtHoursEdit = (TextBox)e.Row.FindControl("txtHoursEdit");
                        txtHoursEdit.Text = dt.Rows[e.Row.RowIndex]["Hours"].ToString();
                    }
                    else
                    {
                        ((Label)e.Row.FindControl("lblFlagEdit")).Text = "NEW";
                        ((Label)e.Row.FindControl("lblResourceID")).Text = "0";
                        ddlResourceTypeEdit.SelectedIndex = 0;
                        //TextBox txtPlannedStartDateEdit = (TextBox)e.Row.FindControl("txtPlannedStartDateEdit");
                        //txtPlannedStartDateEdit.Text = (dt.Rows[e.Row.RowIndex]["PlannedStartDate"].ToString() == "" ? "" : dt.Rows[e.Row.RowIndex]["PlannedStartDate"].ToString());
                        //TextBox txtPlannedEndDateEdit = (TextBox)e.Row.FindControl("txtPlannedEndDateEdit");
                        //txtPlannedEndDateEdit.Text = (dt.Rows[e.Row.RowIndex]["PlannedEndDate"].ToString() == "" ? "" : dt.Rows[e.Row.RowIndex]["PlannedEndDate"].ToString());
                        TextBox txtHoursEdit = (TextBox)e.Row.FindControl("txtHoursEdit");
                        txtHoursEdit.Text = (dt.Rows[e.Row.RowIndex]["Hours"].ToString() == "" ? "" : dt.Rows[e.Row.RowIndex]["HoursPerDay"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvResourceEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveResource")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    string flag = ((Label)((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).FindControl("lblFlagEdit")).Text;
                    string ResourceID = ((Label)((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).FindControl("lblResourceID")).Text;

                    DataTable dt = (DataTable)ViewState["ResourceEdit"];
                    DataTable dtCurrent = dt.Clone();
                    for (int i = 0; i < gvResourceEdit.Rows.Count; i++)
                    {
                        DataRow drCurrent = dtCurrent.NewRow();
                        drCurrent["FlagEdit"] = ((Label)gvResourceEdit.Rows[i].FindControl("lblFlagEdit")).Text;
                        drCurrent["ResourceID"] = ((Label)gvResourceEdit.Rows[i].FindControl("lblResourceID")).Text;
                        drCurrent["ResourceType"] = ((DropDownList)gvResourceEdit.Rows[i].FindControl("ddlResourceTypeEdit")).SelectedValue;
                        //drCurrent["PlannedStartDate"] = ((TextBox)gvResourceEdit.Rows[i].FindControl("txtPlannedStartDateEdit")).Text;
                        //drCurrent["PlannedEndDate"] = ((TextBox)gvResourceEdit.Rows[i].FindControl("txtPlannedEndDateEdit")).Text;
                        drCurrent["Hours"] = ((TextBox)gvResourceEdit.Rows[i].FindControl("txtHoursEdit")).Text;
                        dtCurrent.Rows.Add(drCurrent);
                    }
                    dt = dtCurrent;
                    if (flag == "EDIT")
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("Update TE_ActivityResourceType set DeleteFlag = 1, DeleteDate = GETDATE() where ID = '" + ResourceID + "'", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        dt.Rows.RemoveAt(rowIndex);
                    }
                    else
                    {
                        dt.Rows.RemoveAt(rowIndex);
                    }
                    ViewState["ResourceEdit"] = dt;
                    gvResourceEdit.DataSource = dt;
                    gvResourceEdit.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnAddResourceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["ResourceEdit"];
                DataTable dtCurrent = dt.Clone();
                for (int i = 0; i < gvResourceEdit.Rows.Count; i++)
                {
                    DataRow drCurrent = dtCurrent.NewRow();
                    drCurrent["FlagEdit"] = ((Label)gvResourceEdit.Rows[i].FindControl("lblFlagEdit")).Text;
                    drCurrent["ResourceID"] = ((Label)gvResourceEdit.Rows[i].FindControl("lblResourceID")).Text;
                    drCurrent["ResourceType"] = ((DropDownList)gvResourceEdit.Rows[i].FindControl("ddlResourceTypeEdit")).SelectedValue;
                    //drCurrent["PlannedStartDate"] = ((TextBox)gvResourceEdit.Rows[i].FindControl("txtPlannedStartDateEdit")).Text;
                    //drCurrent["PlannedEndDate"] = ((TextBox)gvResourceEdit.Rows[i].FindControl("txtPlannedEndDateEdit")).Text;
                    drCurrent["Hours"] = ((TextBox)gvResourceEdit.Rows[i].FindControl("txtHoursEdit")).Text;
                    dtCurrent.Rows.Add(drCurrent);
                }
                dt = dtCurrent;



                DataRow dr = dt.NewRow();
                dr["FlagEdit"] = "NEW";
                dr["ResourceID"] = "0";
                dr["ResourceType"] = "";
                //dr["PlannedStartDate"] = "";
                //dr["PlannedEndDate"] = "";
                dr["Hours"] = "";
                dt.Rows.Add(dr);
                ViewState["ResourceEdit"] = dt;
                gvResourceEdit.DataSource = dt;
                gvResourceEdit.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnAddNewActivity_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spInsertActivity", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProjectId", SqlDbType.BigInt).Value = ddlProject.SelectedValue;
                cmd.Parameters.Add("@MilestoneId", SqlDbType.BigInt).Value = ddlMilestone.SelectedValue;
                cmd.Parameters.Add("@WBSId", SqlDbType.BigInt).Value = ddlWBS.SelectedValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescriptionAdd.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string ActivityID = dt.Rows[0][0].ToString();
                for (int i = 0; i < gvResource.Rows.Count; i++)
                {
                    SqlCommand cmd1 = new SqlCommand("spInsertResourceType", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@ActivityID", SqlDbType.BigInt).Value = ActivityID;
                    cmd1.Parameters.Add("@ResourceType", SqlDbType.VarChar).Value = ((DropDownList)gvResource.Rows[i].FindControl("ddlResourceTypeAdd")).SelectedValue;
                    //cmd1.Parameters.Add("@PlannedStartDate", SqlDbType.DateTime).Value = ((TextBox)gvResource.Rows[i].FindControl("txtPlannedStartDateAdd")).Text;
                    //cmd1.Parameters.Add("@PlannedEndDate", SqlDbType.DateTime).Value = ((TextBox)gvResource.Rows[i].FindControl("txtPlannedEndDateAdd")).Text;
                    cmd1.Parameters.Add("@Hours", SqlDbType.DateTime).Value = ((TextBox)gvResource.Rows[i].FindControl("txtHoursAdd")).Text;
                    cmd1.ExecuteNonQuery();
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindgvActivity(ddlProject.SelectedValue, ddlMilestone.SelectedValue, ddlWBS.SelectedValue);
                mpeAddActivity.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnEditActivity_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("update TE_Activity set [Description] = '" + txtDescriptionEdit.Text + "' where ID = '" + lblActivityIdEdit.Text + "'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                for (int i = 0; i < gvResourceEdit.Rows.Count; i++)
                {
                    if (((Label)gvResourceEdit.Rows[i].FindControl("lblFlagEdit")).Text == "EDIT")
                    {
                        SqlCommand cmd1 = new SqlCommand("update TE_ActivityResourceType set ResourceType = '" + ((DropDownList)gvResourceEdit.Rows[i].FindControl("ddlResourceTypeEdit")).SelectedValue + "', HoursPerDay = '" + ((TextBox)gvResourceEdit.Rows[i].FindControl("txtHoursEdit")).Text + "' where ID = '" + ((Label)gvResourceEdit.Rows[i].FindControl("lblResourceID")).Text + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into TE_ActivityResourceType (ActivityID, ResourceType, HoursPerDay) values ('" + lblActivityIdEdit.Text + "', '" + ((DropDownList)gvResourceEdit.Rows[i].FindControl("ddlResourceTypeEdit")).SelectedValue + "', '" + ((TextBox)gvResourceEdit.Rows[i].FindControl("txtHoursEdit")).Text + "')", conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindgvActivity(ddlProject.SelectedValue, ddlMilestone.SelectedValue, ddlWBS.SelectedValue);
                mpeEditActivity.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnOKPopUp_Click(object sender, EventArgs e)
        {
            mpeOKPopUp.Hide();
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddActivity.Hide();
        }

        protected void gvActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)gvActivity.DataSource;
                string ActivityID = dt.Rows[e.Row.RowIndex]["ID"].ToString();
                HyperLink lnkAssignActivity = (HyperLink)e.Row.FindControl("lnkAssignActivity");
                lnkAssignActivity.NavigateUrl = "TE_ActivityAssignment.aspx?ProjectID=" + ddlProject.SelectedValue + "&MilestoneId=" + ddlMilestone.SelectedValue + "&WBSID=" + ddlWBS.SelectedValue + "&ActivityID=" + ActivityID + "&iframe=true&width=900&height=510";
            }
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            mpeEditActivity.Hide();
        }
        

    }
}