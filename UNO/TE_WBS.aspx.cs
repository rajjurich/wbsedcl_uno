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
    public partial class TE_WBS : System.Web.UI.Page
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
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void BindgvWBS(string ProjectID, string MilestoneID)
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
                gvWBS.DataSource = dt;
                gvWBS.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProject.SelectedValue == "0")
                {
                    ddlMilestone.Items.Clear();
                    ddlMilestone.Items.Insert(0, new ListItem("Select One", "0"));
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select * from TE_Milestone where DeleteFlag is null and ProjectID = '" + ddlProject.SelectedValue + "'", conn);
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
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void ddlMilestone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProject.SelectedValue != "0")
                {
                    if (ddlMilestone.SelectedValue != "0")
                    {
                        BindgvWBS(ddlProject.SelectedValue, ddlMilestone.SelectedValue);
                    }
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
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID= '" + ddlProject.SelectedValue + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (dt.Rows[0]["BaseLine"].ToString() == "BASELINED")
                {
                    lblMessageOKPopUp.Text = "This Project is already baselined. You cannot Add WBS now.";
                    mpeOKPopUp.Show();
                }
                else
                {
                    txtDescriptionAdd.Text = "";
                    //txtPlannedStartDateAdd.Text = "";
                    //txtPlannedEndDateAdd.Text = "";
                    mpeAddWBS.Show();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void gvWBS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string WBSID = e.CommandArgument.ToString();
                if (e.CommandName == "Modify")
                {
                    lblWBSIDEdit.Text = WBSID;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID= '" + ddlProject.SelectedValue + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (dt.Rows[0]["BaseLine"].ToString() == "BASELINED")
                    {
                        lblMessageOKPopUp.Text = "This Project is already baselined. You cannot Edit WBS now.";
                        mpeOKPopUp.Show();
                    }
                    else
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd1 = new SqlCommand("Select * from TE_WBS where DeleteFlag is null and ID = '" + WBSID + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        txtDescriptionEdit.Text = dt1.Rows[0]["Description"].ToString();
                        //txtPlannedStartDateEdit.Text = (DateTime.ParseExact(dt1.Rows[0]["PlannedStartDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                        //txtPlannedEndDateEdit.Text = (DateTime.ParseExact(dt1.Rows[0]["PlannedEndDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");

                        mpeEditWBS.Show();
                    }
                }
                if (e.CommandName == "Remove")
                {
                    lblWBSIDRemove.Text = WBSID;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID= '" + ddlProject.SelectedValue + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (dt.Rows[0]["BaseLine"].ToString() == "BASELINED")
                    {
                        lblMessageOKPopUp.Text = "This Project is already baselined. You cannot Delete WBS now.";
                        mpeOKPopUp.Show();
                    }
                    else
                    {
                        lblMessageRemove.Text = "Are you sure to delete this WBS?";
                        mpeRemoveWBS.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spUpdateWBS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WBSID", SqlDbType.BigInt).Value = lblWBSIDEdit.Text;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescriptionEdit.Text;
                //cmd.Parameters.Add("@PlannedStartDate", SqlDbType.DateTime).Value = txtPlannedStartDateEdit.Text;
                //cmd.Parameters.Add("@PlannedEndDate", SqlDbType.DateTime).Value = txtPlannedEndDateEdit.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindgvWBS(ddlProject.SelectedValue, ddlMilestone.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void btnYesRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("update TE_WBS set DeleteFlag = 1, DeleteDate = GETDATE() where ID = '" + lblWBSIDRemove.Text + "'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindgvWBS(ddlProject.SelectedValue, ddlMilestone.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void btnAddNewWBS_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spInsertWBS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProjectId", SqlDbType.BigInt).Value = ddlProject.SelectedValue;
                cmd.Parameters.Add("@MilestoneId", SqlDbType.BigInt).Value = ddlMilestone.SelectedValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescriptionAdd.Text;
                //cmd.Parameters.Add("@PlannedStartDate", SqlDbType.DateTime).Value = txtPlannedStartDateAdd.Text;
                //cmd.Parameters.Add("@PlannedEndDate", SqlDbType.DateTime).Value = txtPlannedEndDateAdd.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindgvWBS(ddlProject.SelectedValue, ddlMilestone.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}