using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace UNO
{
    public partial class TE_Milestone : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();
            if (!IsPostBack)
            {
                fill_project();
                fill_grid_milestone();

                if (Session["PROJECT_CODE"] == null)
                {
                    Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>projsele()</script>");
                    //Response.Redirect("ProjectFileView.aspx");

                }
                else
                {
                    //lblProjID.Text = Session["PROJECT_CODE"].ToString();
                    fetchprodetails();
                    dataFetch();


                }
            }


        }

        private void dataFetch()
        {
            //DataTable dt = new DataTable();

            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            ////ERROR-24-JAN
            //String sqlstr = "SELECT WBS_ID ,WBS_DESC ,WBS_STARTDATE,WBS_TODATE,WBS_MILESTONEID  FROM  TE_MILESTONE_WBS_DEF WHERE WBS_PROJID ='" + lblProjID.Text + "' and WBS_ISDELETED=0";
            ////ERROR-24-JAN

            //SqlDataAdapter sqlda = new SqlDataAdapter(sqlstr, con);

            //sqlda.Fill(dt);
            //msView.DataSource = dt;
            //msView.DataBind();
            //// swapnil Start
            //if (dt.Rows.Count == 0)
            //{
            //    //btn_modify.Enabled = false;
            //    //btn_delete.Enabled = false;
            //}
            ////swapnil end
            //sqlda.Dispose();
            //con.Close();

        }

        private void fetchprodetails()
        {

            //    //  Session["PROJECT_CODE"] = lblProjID.Text;
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    String sqlstr1 = "SELECT PROJECT_DESC,PROJECT_DIV_CODES ,CLIENT_NAME,PROJECT_SITE,PROJECT_STATE FROM dbo.TE_Project_File  INNER JOIN TE_CLIENT_FILE ON TE_Project_File.PROJECT_CLIENT_ID = TE_CLIENT_FILE.CLIENT_ID WHERE PROJECT_CODE ='" + lblProjID.Text + "'";
            //    SqlCommand sqlcmd = new SqlCommand(sqlstr1, con);
            //    SqlDataReader dr = sqlcmd.ExecuteReader();

            //    while (dr.Read())
            //    {
            //    //    lbl_projdesc.Text = dr["PROJECT_DESC"].ToString();
            //    //    lbl_div.Text = dr["PROJECT_DIV_CODES"].ToString();
            //    //    lbl_clientname.Text = dr["CLIENT_NAME"].ToString();
            //    //    lbl_state.Text = dr["PROJECT_STATE"].ToString();

            //    }

            //    sqlcmd.Dispose();
            //    dr.Close();
            //    con.Close();

        }


        protected void search_Click(object sender, ImageClickEventArgs e)
        {
            fetchprodetails();
            dataFetch();

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            //Response.Redirect("TEMileStoneWBS.aspx");

        }

        //protected void btn_modify_Click(object sender, EventArgs e)
        //{
        //    Boolean flag = false;
        //    foreach (GridViewRow rw in msView.Rows)
        //    {
        //        CheckBox chkBx = (CheckBox)rw.FindControl("iDSelect");
        //        if (chkBx != null && chkBx.Checked)
        //        {
        //            flag = true;
        //            break;
        //        }
        //    }
        //    if (flag == false)
        //    {
        //        Response.Write("<script>alert('Select the Milestones and WBS first.');</script>");
        //    }
        //    else
        //    {
        //        //Pops_ModalPopupExtender.Show();
        //    }

        //}

        //protected void btn_delete_Click(object sender, EventArgs e)
        //{


        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    SqlCommand objcmd = new SqlCommand();
        //    objcmd.Connection = con;

        //    try
        //    {
        //        for (int i = 0; i < msView.Rows.Count; i++)
        //        {
        //            CheckBox chkbox = (CheckBox)msView.Rows[i].Cells[0].FindControl("iDSelect");
        //            if (chkbox.Checked == true)
        //            {
        //                objcmd.CommandText = "UPDATE TE_MILESTONE_WBS_DEF SET WBS_ISDELETED = '1',WBS_DELETEDDATE= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103)" +
        //                                    "WHERE WBS_PROJID='" + Session["PROJECT_CODE"] + "' and WBS_MILESTONEID='" + msView.Rows[i].Cells[1].Text + "'and WBS_ID='" + msView.Rows[i].Cells[2].Text + "'";
        //                objcmd.ExecuteNonQuery();

        //                objcmd.CommandText = "UPDATE TE_PROJECT_MILESTONE_DEF SET MILESTONE_ISDELETED='1',MILESTONE_DELETEDDATE= convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103)" +
        //                               " WHERE PROJECT_CODE='" + Session["PROJECT_CODE"] + "' and MILESTONE_ID ='" + msView.Rows[i].Cells[1].Text + "'";

        //                objcmd.ExecuteNonQuery();
        //            }
        //            this.LblMsg.Text = "Record Deleted Successfully.";
        //            LoadJScript();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.LblMsg.Text = ex.Message;
        //        LoadJScript();
        //    }
        //    dataFetch();
        //    objcmd.Dispose();
        //    con.Close();
        //}
        internal void LoadJScript()
        {
            ClientScriptManager script = Page.ClientScript;
            if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
            {
                script.RegisterStartupScript(this.GetType(), "HideLabel",
                "<script type='text/javascript'>HideLabel('" + LblMsg.ClientID + "')</script>");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }
            //    String updateqry = "UPDATE TE_MILESTONE_WBS_DEF set WBS_DESC = '" + txtSanctRemarks.Text + "' WHERE WBS_ID='" + Session["WBSID"].ToString() + "' and WBS_MILESTONEID='" + Session["MSID"].ToString() + "'     ";
            //    SqlCommand sqlupdate = new SqlCommand(updateqry, con);
            //    sqlupdate.ExecuteNonQuery();
            //    //Pops_ModalPopupExtender.Hide();
            //    dataFetch();
            //    sqlupdate.Dispose();
            //    con.Close();
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            //Pops_ModalPopupExtender.Hide();
        }
        protected void chkSelectChanged(object sender, EventArgs e)
        {
            CheckBox chkSelected = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chkSelected.Parent.Parent;
            Session["MSID"] = dr.Cells[1].Text;
            Session["WBSID"] = dr.Cells[2].Text;
        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {

        }



        protected void fill_project()
        {



            string sql_project_selection = "select Description from TE_Projects where DeleteFlag is null";
            SqlCommand cmd = new SqlCommand(sql_project_selection, con);

            SqlDataReader dr_project = cmd.ExecuteReader();

            while (dr_project.Read())
            {

                ddl_projectnames.Items.Add(dr_project["Description"].ToString());

            }
            dr_project.Close();

        }

        protected int get_project_id(string project_name)
        {

            int proj_id = 0;

            string sql_project_id = "select ID from TE_Projects where Description='" + project_name + "' AND DeleteFlag is null";

            SqlCommand cmd_project_id = new SqlCommand(sql_project_id, con);
            SqlDataReader dr_project_id = cmd_project_id.ExecuteReader();

            if (dr_project_id.Read())
                proj_id = Convert.ToInt32(dr_project_id["ID"]);

            dr_project_id.Close();

            return proj_id;




        }
        protected void fill_grid_milestone()
        {

            int pro_id = get_project_id(ddl_projectnames.SelectedItem.ToString());

            string sql_fill_milestone = "select id,Description from TE_Milestone where ProjectID =" + pro_id + " and deleteflag is null";

            SqlCommand cmd_milestone = new SqlCommand(sql_fill_milestone, con);
            SqlDataAdapter da_milestone = new SqlDataAdapter(cmd_milestone);

            DataTable dt = new DataTable();

            da_milestone.Fill(dt);

            msView.DataSource = dt;
            msView.DataBind();


        }

        protected void ddl_projectnames_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_grid_milestone();

            string pro_name = ddl_projectnames.SelectedItem.ToString();
            int p_id = get_project_id(pro_name);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (con.State == ConnectionState.Open)
            {

                string sql_basline = "select * from TE_Projects where  id=" + p_id + " and BaseLine ='Baseline'";
                SqlCommand cmd_baseline = new SqlCommand(sql_basline, con);
                SqlDataReader dr_baseline = cmd_baseline.ExecuteReader();

                if (dr_baseline.Read())
                {
                    btn_add.Visible = false;
                    msView.Columns[1].Visible = false;
                    msView.Columns[2].Visible = false;
                    pnlAddProject.Visible = false;
                    pnl_Edit.Visible = false;
                    pnl_del_project.Visible = false;
                    lblbaselinemsg.Visible = true;

                }
                else
                {
                    lblbaselinemsg.Visible = false;
                    btn_add.Visible = true;
                    msView.Columns[1].Visible = true;
                    msView.Columns[2].Visible = true;
                    pnlAddProject.Visible = true;
                    pnl_Edit.Visible = true;
                    pnl_del_project.Visible = true;


                }



            }

        }

        protected void btnSubmitAdd_Click1(object sender, EventArgs e)
        {


            int pr_id = get_project_id(ddl_projectnames.SelectedItem.ToString());

            if (pr_id == 0)
            {
            }
            else
            {




                //string sql_milestone_insert = "insert into TE_Milestone(ProjectID,Description,PlannedStartDate,PlannedEndDate) values(" + pr_id + ",'" + txtDescriptionEdit.Text + "',convert(datetime,'" + txtPlannedStartDateAdd.Text + "',103),convert(datetime,'" + txtPlannedEndDateAdd.Text + "',103))";
                string sql_milestone_insert = "insert into TE_Milestone(ProjectID,Description) values(" + pr_id + ",'" + txtDescriptionEdit.Text + "')";
                
                SqlCommand cmd_milestone_insert = new SqlCommand(sql_milestone_insert, con);
                cmd_milestone_insert.ExecuteNonQuery();
                fill_grid_milestone();
            }
            txtDescriptionEdit.Text = "";
            //txtPlannedEndDateAdd.Text = "";
            //txtPlannedStartDateAdd.Text = "";

        }

        protected void msView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                string milestoneID = e.CommandArgument.ToString();


                if (e.CommandName == "Modify")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Select * from TE_Milestone where ID = '" + milestoneID + "' ", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        lblmilestone_id.Text = milestoneID;

                        lblproject_id.Text = dt.Rows[0]["ProjectID"].ToString();
                        txtDescriptionEdit_pnl.Text = dt.Rows[0]["Description"].ToString();
                        //txtPlannedStartDate_edit.Text = Convert.ToDateTime(dt.Rows[0]["PlannedStartDate"]).ToString("dd-MM-yyyy");
                        //txtPlannedEndDate_edit.Text = Convert.ToDateTime(dt.Rows[0]["PlannedEndDate"]).ToString("dd-MM-yyyy");

                        ModalPopupExtender1.Show();

                    }
                }
                if (e.CommandName == "Remove")
                {

                    lblmilestone_id_delpnl.Text = milestoneID;

                    Pops_ModalPopupExtender.Show();

                }




            }

            catch (Exception ex)
            {

            }
        }

        protected void btnmodify_sub_Click(object sender, EventArgs e)
        {

            //DateTime dt =Convert.ToDateTime(DateTime.ParseExact(txtPlannedStartDate_edit.Text, "dd MM yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy"));

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (con.State == ConnectionState.Open)
                {

                    string sql_update_milestone = "update TE_Milestone set Description='" + txtDescriptionEdit_pnl.Text + "' ";

                    //sql_update_milestone += "PlannedStartDate=convert(DATETIME,'" + txtPlannedStartDate_edit.Text + "',103) ,";

                    //sql_update_milestone += "PlannedEndDate= CONVERT(DATETIME,'" + txtPlannedEndDate_edit.Text + "',103) ";

                    //sql_update_milestone += "PlannedEndDate=dDate_edit.Text, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy") + "";


                    sql_update_milestone += "where id=" + Convert.ToInt32(lblmilestone_id.Text) + " and projectid=" + Convert.ToInt32(lblproject_id.Text) + "";



                    SqlCommand cmd = new SqlCommand(sql_update_milestone, con);
                    cmd.ExecuteNonQuery();
                    fill_grid_milestone();

                    con.Close();

                    //DateTime result = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", provider);

                    //(DateTime.ParseExact(dt.Rows[0]["PlannedStartDate"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");

                }
            }
            catch (Exception ex)
            {

            }


            //try
            //{
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }
            //    SqlCommand cmd = new SqlCommand("spUpdate_milestone", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Convert.ToInt32(lblmilestone_id.Text);
            //    //cmd.Parameters.Add("@Project_ID", SqlDbType.BigInt).Value = lblproject_id.Text;
            //    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescriptionEdit_pnl.Text;

            //    cmd.Parameters.Add("@PlannedStartDate", SqlDbType.DateTime).Value =txtPlannedStartDate_edit.Text;

            //    cmd.Parameters.Add("@PlannedEndDate", SqlDbType.DateTime).Value = txtPlannedEndDate_edit.Text;

            //    //cmd.Parameters.AddWithValue("@PlannedStartDate",(txtPlannedStartDate_edit.Text));

            //    //cmd.Parameters.AddWithValue("@PlannedEndDate", Convert.ToDateTime(txtPlannedEndDate_edit.Text));

            //    cmd.ExecuteNonQuery();
            //    if (con.State == ConnectionState.Open)
            //    {
            //        con.Close();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    if (con.State == ConnectionState.Open)
            //    {
            //        con.Close();
            //    }
            //}



        }

        protected void msView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            msView.PageIndex = e.NewPageIndex;
            fill_grid_milestone();
        }

        protected void btn_del_submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con.State == ConnectionState.Open)
                {


                    string sql_del = "update TE_Milestone set deleteflag=1,deletedate=getdate() where ID =" + Convert.ToInt32(lblmilestone_id_delpnl.Text) + " ";


                    SqlCommand del_cmd = new SqlCommand(sql_del, con);
                    del_cmd.ExecuteNonQuery();

                    fill_grid_milestone();

                }
            }

            catch (Exception ex)
            {

            }

        }

    }
}