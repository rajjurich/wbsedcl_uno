using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace UNO
{
    public partial class SACPersonalisationView : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindgvPersonalisation();
            }
        }

        private void BindgvPersonalisation()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd;
                if (ddlMode.SelectedValue == "Personalised")
                {
                    //cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_PERSO_FLAG = 'P' or EPD_PERSO_FLAG is not null", conn);
                   // cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1 and EPD_PERSO_FLAG = 'P' or EPD_PERSO_FLAG is not null", conn);

                    cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1 and EPD_PERSO_FLAG = 'P' ", conn);
                    gvPersonalisation.Columns[0].Visible = false;
                }
                else if (ddlMode.SelectedValue == "NonPersonalised")
                {
                    //cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_PERSO_FLAG <> 'P' or EPD_PERSO_FLAG is null", conn);
                    cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1 AND EPD_PERSO_FLAG <> 'P' or EPD_PERSO_FLAG is null", conn);
                    gvPersonalisation.Columns[0].Visible = true;
                }
                else
                {
                    //cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS", conn);
                    cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1", conn);
                    gvPersonalisation.Columns[0].Visible = true;
                }
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtCompanyName.Text.ToString() == "" && txtCompanyID.Text.ToString() == "")
                {
                    gvPersonalisation.DataSource = dt;
                    gvPersonalisation.DataBind();

                    //BindgvPersonalisation();
                    //cmdReset_Click(sender, e);
                }
                else
                {
                    String[,] values = { 
                {"EPD_empid~" +txtCompanyID.Text.Trim(), "S" },
                {"Name~" +txtCompanyName.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvPersonalisation.DataSource = _tempDT;
                    gvPersonalisation.DataBind();
                    _tempDT = _sc.searchTable(values, dt);
                    gvPersonalisation.DataSource = _tempDT;
                    gvPersonalisation.DataBind();

                }


                DropDownList ddl = (DropDownList)gvPersonalisation.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvPersonalisation.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvPersonalisation.PageIndex + 1).ToString();
                Label lblcount = (Label)gvPersonalisation.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvPersonalisation.DataSource).Rows.Count.ToString() + " Records.";
                if (gvPersonalisation.PageCount == 0)
                {
                    ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvPersonalisation.PageIndex + 1 == gvPersonalisation.PageCount)
                {
                    ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvPersonalisation.PageIndex == 0)
                {
                    ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvPersonalisation.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvPersonalisation.PageSize * gvPersonalisation.PageIndex) + 1) + " to " + (gvPersonalisation.PageSize * (gvPersonalisation.PageIndex + 1));

                ((Label)gvPersonalisation.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvPersonalisation.PageSize * gvPersonalisation.PageIndex) + 1) + " to " + (((gvPersonalisation.PageSize * (gvPersonalisation.PageIndex + 1)) - 10) + gvPersonalisation.Rows.Count);

                gvPersonalisation.BottomPagerRow.Visible = true;


                //if (dt.Rows.Count != 0)
                //{
                //    btnDelete.Enabled = true;
                //    btnSearch.Enabled = true;
                //}

                //else
                //{
                //    btnDelete.Enabled = false;
                //    btnSearch.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersonalisation");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvPersonalisation.PageIndex = Convert.ToInt32(((DropDownList)gvPersonalisation.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindgvPersonalisation();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersonalisation");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvPersonalisation.PageIndex = gvPersonalisation.PageIndex - 1;
                BindgvPersonalisation();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersonalisation");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvPersonalisation.PageIndex = gvPersonalisation.PageIndex + 1;
                BindgvPersonalisation();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersonalisation");
            }
        }

        protected void gvPersonalisation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPersonalisation.PageIndex = e.NewPageIndex;
                BindgvPersonalisation();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersonalisation");
            }
        }

        protected void gvPersonalisation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string EmpCode = e.CommandArgument.ToString();
                Response.Redirect("SACPersonalisation.aspx?EmpCode=" + EmpCode, true);
            }
        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindgvPersonalisation();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersonalisation");
            }
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            //ddlMode.SelectedIndex = 2;
            ddlMode.SelectedValue = "NonPersonalised";
            ddlMode_SelectedIndexChanged(ddlMode, EventArgs.Empty);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd;
                if (ddlMode.SelectedValue == "Personalised")
                {
                    //cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_PERSO_FLAG = 'P' or EPD_PERSO_FLAG is not null", conn);
                   // cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1 and EPD_PERSO_FLAG = 'P' or EPD_PERSO_FLAG is not null", conn);

                    cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1 and EPD_PERSO_FLAG = 'P' ", conn);
                    gvPersonalisation.Columns[0].Visible = false;
                }
                else if (ddlMode.SelectedValue == "NonPersonalised")
                {
                    //cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_PERSO_FLAG <> 'P' or EPD_PERSO_FLAG is null", conn);
                    cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1 AND EPD_PERSO_FLAG <> 'P' or EPD_PERSO_FLAG is null", conn);
                    gvPersonalisation.Columns[0].Visible = true;
                }
                else
                {
                    //cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS", conn);
                    cmd = new SqlCommand("Select EPD_EMPID, EPD_FIRST_NAME + coalesce( EPD_MIDDLE_NAME, '' )+ EPD_LAST_NAME as Name from ENT_EMPLOYEE_PERSONAL_DTLS EPD inner join ENT_EMPLOYEE_OFFICIAL_DTLS EOD on EPD.EPD_EMPID=EOD.EOD_EMPID where EOD.EOD_ACTIVE=1", conn);
                    gvPersonalisation.Columns[0].Visible = true;
                }


                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtCompanyName.Text.ToString() == "" && txtCompanyID.Text.ToString() == "")
                {
                    BindgvPersonalisation();
                    //cmdReset_Click(sender, e);
                }
                else
                {
                    String[,] values = { 
                {"EPD_empid~" +txtCompanyID.Text.Trim(), "S" },
                {"Name~" +txtCompanyName.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvPersonalisation.DataSource = _tempDT;
                    gvPersonalisation.DataBind();
                    _tempDT = _sc.searchTable(values, dt);
                    gvPersonalisation.DataSource = _tempDT;
                    gvPersonalisation.DataBind();

                    DropDownList ddl = (DropDownList)gvPersonalisation.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvPersonalisation.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvPersonalisation.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvPersonalisation.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvPersonalisation.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvPersonalisation.PageCount == 0)
                    {
                        ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvPersonalisation.PageIndex + 1 == gvPersonalisation.PageCount)
                    {
                        ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvPersonalisation.PageIndex == 0)
                    {
                        ((Button)gvPersonalisation.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvPersonalisation.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvPersonalisation.PageSize * gvPersonalisation.PageIndex) + 1) + " to " + (gvPersonalisation.PageSize * (gvPersonalisation.PageIndex + 1));

                    ((Label)gvPersonalisation.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvPersonalisation.PageSize * gvPersonalisation.PageIndex) + 1) + " to " + (((gvPersonalisation.PageSize * (gvPersonalisation.PageIndex + 1)) - 10) + gvPersonalisation.Rows.Count);

                    gvPersonalisation.BottomPagerRow.Visible = true;
                }
            }


            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }

    }
}