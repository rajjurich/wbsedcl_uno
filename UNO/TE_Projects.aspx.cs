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
    public partial class TE_Projects : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGvProjects();
            }
        }

        private void BindGvProjects()
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
                gvProjects.DataSource = dt;
                gvProjects.DataBind();
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

        private void BindddlDivisionEdit()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT OCE_ID, [OCE_DESCRIPTION] AS DIVNAME FROM ENT_ORG_COMMON_ENTITIES WHERE CEM_ENTITY_ID='DIV' AND OCE_ISDELETED=0 AND OCE_DELETEDDATE IS NULL", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlDivisionEdit.DataValueField = "OCE_ID";
                ddlDivisionEdit.DataTextField = "DIVNAME";
                ddlDivisionEdit.DataSource = dt;
                ddlDivisionEdit.DataBind();
                ddlDivisionEdit.Items.Insert(0, new ListItem("Select One", "0"));
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

        private void BindddlClientEdit()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from dbo.TE_CLIENT_FILE", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Close();
                }
                ddlClientEdit.DataValueField = "Client_ID";
                ddlClientEdit.DataTextField = "Client_Name";
                ddlClientEdit.DataSource = dt;
                ddlClientEdit.DataBind();
                ddlClientEdit.Items.Insert(0, new ListItem("Select One", "0"));

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        private void BindddlClientAdd()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from TE_CLIENT_FILE where CLIENT_ISDELETED = 0", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlClientAdd.DataValueField = "CLIENT_ID";
                ddlClientAdd.DataTextField = "CLIENT_NAME";
                ddlClientAdd.DataSource = dt;
                ddlClientAdd.DataBind();
                ddlClientAdd.Items.Insert(0, new ListItem("Select One", "0"));

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


        private void BindddlDivisionAdd()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT OCE_ID, [OCE_DESCRIPTION] AS DIVNAME FROM ENT_ORG_COMMON_ENTITIES WHERE CEM_ENTITY_ID='DIV' AND OCE_ISDELETED=0 AND OCE_DELETEDDATE IS NULL", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlDivisionAdd.DataValueField = "OCE_ID";
                ddlDivisionAdd.DataTextField = "DIVNAME";
                ddlDivisionAdd.DataSource = dt;
                ddlDivisionAdd.DataBind();
                ddlDivisionAdd.Items.Insert(0, new ListItem("Select One", "0"));

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

        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ProjectID = e.CommandArgument.ToString();
                if (e.CommandName == "Baseline")
                {
                    lblProjectIdBaseline.Text = ProjectID;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd2 = new SqlCommand("Select * from TE_Activity where DeleteFlag is null and ProjectId = '" + ProjectID + "'", conn);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (dt2.Rows.Count == 0)
                    {
                        lblMessageBaseline.Text = "This Project appears to be Incomplete. After baseline you cannot modify the project. Are you sure to baseline it?";
                    }
                    else
                    {
                        lblMessageBaseline.Text = "After baseline you cannot modify the project. Are you sure to baseline it?";
                    }
                    mpeBaselinePopUp.Show();
                }
                if (e.CommandName == "Modify")
                {
                    lblIdEdit.Text = ProjectID;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select * from TE_Projects where DeleteFlag is null and ID = '" + ProjectID + "' and ManagerEmpCode = '" + Session["uid"].ToString() + "'", conn);
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
                        lblMessageGeneral.Text = "This project is already baselined. You cannot Edit this project now.";
                        mpeOKPopUp.Show();
                    }
                    else
                    {
                        lblProjectCodeEdit.Text = "Project Code : " + dt.Rows[0]["ProjectCode"].ToString();
                        txtDescriptionEdit.Text = dt.Rows[0]["Description"].ToString();
                        BindddlClientEdit();
                        ddlClientEdit.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                        BindddlDivisionEdit();
                        ddlDivisionEdit.SelectedValue = dt.Rows[0]["DivisionCode"].ToString();
                        txtLocationEdit.Text = dt.Rows[0]["Location"].ToString();
                        txtStateEdit.Text = dt.Rows[0]["State"].ToString();
                        txtRemarksEdit.Text = dt.Rows[0]["Remarks"].ToString();
                        txtPlannedStartDateEdit.Text = (DateTime.ParseExact(dt.Rows[0]["PlannedStartDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                        txtPlannedEndDateEdit.Text = (DateTime.ParseExact(dt.Rows[0]["PlannedEndDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                        mpeEditProject.Show();
                    }
                }
                if (e.CommandName == "Remove")
                {
                    lblProjectIdRemove.Text = ProjectID;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select BaseLine from TE_Projects where DeleteFlag is null and ID = '" + ProjectID + "'", conn);
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
                        lblMessageGeneral.Text = "This project is already baselined. You cannot remove this project now.";
                        mpeOKPopUp.Show();
                    }
                    else
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd1 = new SqlCommand("Select * from dbo.TE_Milestone where DeleteFlag is null and ProjectID = '" + ProjectID + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        if (dt1.Rows.Count != 0)
                        {
                            lblMessageRemove.Text = "This Project is not empty. Are you sure to Delete the project?";
                        }
                        else
                        {
                            lblMessageRemove.Text = "Are you sure to Delete the project?";
                        }
                        mpeRemovePopUp.Show();
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
                SqlCommand cmd = new SqlCommand("spUpdateProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Convert.ToInt32(lblIdEdit.Text);
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescriptionEdit.Text;
                cmd.Parameters.Add("@ClientID", SqlDbType.VarChar).Value = ddlClientEdit.SelectedValue;
                cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar).Value = ddlDivisionEdit.SelectedValue;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = txtLocationEdit.Text;
                cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = txtStateEdit.Text;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarksEdit.Text;
                cmd.Parameters.Add("@PlannedStartDate", SqlDbType.DateTime).Value = txtPlannedStartDateEdit.Text;
                cmd.Parameters.Add("@PlannedEndDate", SqlDbType.DateTime).Value = txtPlannedEndDateEdit.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindGvProjects();
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

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spInsertProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProjectCode", SqlDbType.VarChar).Value = txtProjectCodeAdd.Text;
                //cmd.Parameters.Add("@ProjectCode", SqlDbType.VarChar).Value = ddlProjectNameAdd.SelectedValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescriptionAdd.Text;
                cmd.Parameters.Add("@ManagerEmpCode", SqlDbType.VarChar).Value = Session["uid"].ToString();
                cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar).Value = ddlDivisionAdd.SelectedValue;
                cmd.Parameters.Add("@ClientID", SqlDbType.VarChar).Value = ddlClientAdd.SelectedValue;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = txtLocationAdd.Text;
                cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = txtStateAdd.Text;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarksAdd.Text;
                cmd.Parameters.Add("@PlannedStartDate", SqlDbType.DateTime).Value = txtPlannedStartDateAdd.Text;
                cmd.Parameters.Add("@PlannedEndDate", SqlDbType.DateTime).Value = txtPlannedEndDateAdd.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindGvProjects();
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

        protected void btnAddNewProject_Click(object sender, EventArgs e)
        {
            try
            {
                txtDescriptionAdd.Text = "";
                BindddlClientAdd();
                ddlClientAdd.SelectedValue = "0";
                BindddlDivisionAdd();
                ddlDivisionAdd.SelectedValue = "0";
                txtLocationAdd.Text = "";
                txtStateAdd.Text = "";
                txtRemarksAdd.Text = "";
                txtPlannedStartDateAdd.Text = "";
                txtPlannedEndDateAdd.Text = "";
                mpeAddProject.Show();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
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
                SqlCommand cmd = new SqlCommand("update TE_Projects set DeleteFlag = 1, DeleteDate = GETDATE() where ID = '" + lblProjectIdRemove.Text + "'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindGvProjects();
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

        protected void btnYesBaseline_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("update TE_Projects set BaseLine = 'BASELINED' where ID = '" + lblProjectIdBaseline.Text + "'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BindGvProjects();
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