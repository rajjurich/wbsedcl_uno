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
    public partial class ExpenseEntry : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string ProjectCode = "";
        string MilestoneId = "";
        string WBSID = "";
        string ActivityID = "";
        string Date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null || Session["uid"].ToString() == "")
            {
                Response.Redirect("Login.aspx", true);
            }
            ProjectCode = Request.QueryString["ProjectCode"].ToString();
            MilestoneId = Request.QueryString["MilestoneId"].ToString();
            WBSID = Request.QueryString["WBSID"].ToString();
            ActivityID = Request.QueryString["ActivityID"].ToString();


            Date = DateTime.ParseExact(Request.QueryString["Date"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            //Date = DateTime.ParseExact(Request.QueryString["Date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //Date = DateTime.Parse(Request.QueryString["Date"].ToString());

            if (!IsPostBack)
            {

                BindGvExpense();
            }
        }

        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int i = e.Row.RowIndex;
                    DataTable dt = (DataTable)gvExpense.DataSource;

                    Label lblExpenseID = (Label)e.Row.FindControl("lblExpenseID");
                    Label lblFlagExpense = (Label)e.Row.FindControl("lblFlagExpense");
                    TextBox txtPurpose = (TextBox)e.Row.FindControl("txtPurpose");
                    DropDownList ddlMode = (DropDownList)e.Row.FindControl("ddlMode");
                    TextBox txtKM = (TextBox)e.Row.FindControl("txtKM");
                    TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                    DropDownList ddlBillable = (DropDownList)e.Row.FindControl("ddlBillable");
                    DropDownList ddlPaidBy = (DropDownList)e.Row.FindControl("ddlPaidBy");

                    if (dt.Rows[i]["EXPENSE_REQ_STATUS"].ToString() == "REQUESTED")
                    {
                        lblFlagExpense.Text = "EDIT";
                        lblExpenseID.Text = dt.Rows[i]["RES_EXP_ROWID"].ToString();
                        txtPurpose.Text = dt.Rows[i]["EXPENSE_PURPOSE"].ToString();
                        ddlMode.SelectedValue = dt.Rows[i]["EXPENSE_MODE"].ToString();
                        if (dt.Rows[i]["EXPENSE_MODE"].ToString() == "Owned")
                        {
                            txtKM.Text = dt.Rows[i]["EXPENSE_KMSIFOWNED"].ToString();
                            txtKM.Enabled = true;

                        }
                        else
                        {
                            txtKM.Text = "";
                            txtKM.Enabled = false;
                        }
                        txtAmount.Text = dt.Rows[i]["EXPENSE_AMOUNT"].ToString();
                        ddlBillable.SelectedValue = (dt.Rows[i]["EXPENSE_BILLABLE"].ToString() == "0") ? "Yes" : "No";
                        ddlPaidBy.SelectedValue = (dt.Rows[i]["EXPENSE_PAIDBYWHOM"].ToString() == "0") ? "Client" : "Self";

                        GridView gvEvidance = (GridView)e.Row.FindControl("gvEvidance");
                        BindGvEvidance(gvEvidance, dt.Rows[i]["PROJECT_CODE"].ToString(), dt.Rows[i]["MILESTONE_ID"].ToString(), dt.Rows[i]["WBS_ID"].ToString(), dt.Rows[i]["ACTIVITY_ID"].ToString(), dt.Rows[i]["RES_EXP_ROWID"].ToString(), dt.Rows[i]["EXPENSE_REQ_STATUS"].ToString());
                    }
                    else if (dt.Rows[i]["EXPENSE_REQ_STATUS"].ToString() == "")
                    {

                        lblFlagExpense.Text = "NEW";
                        lblExpenseID.Text = "0";
                        txtPurpose.Text = "";
                        ddlMode.SelectedValue = "Owned";
                        txtKM.Text = "";
                        txtAmount.Text = "";
                        ddlBillable.SelectedValue = "Yes";
                        ddlPaidBy.SelectedValue = "Client";

                        GridView gvEvidance = (GridView)e.Row.FindControl("gvEvidance");
                        BindGvEvidance(gvEvidance, ProjectCode, MilestoneId, WBSID, ActivityID, e.Row.RowIndex.ToString(), "NEW");

                    }

                    else
                    {
                        lblFlagExpense.Text = "VIEW";
                        lblExpenseID.Text = dt.Rows[i]["RES_EXP_ROWID"].ToString();
                        txtPurpose.Text = dt.Rows[i]["EXPENSE_PURPOSE"].ToString();
                        ddlMode.SelectedValue = dt.Rows[i]["EXPENSE_MODE"].ToString();
                        txtKM.Text = dt.Rows[i]["EXPENSE_KMSIFOWNED"].ToString();
                        txtAmount.Text = dt.Rows[i]["EXPENSE_AMOUNT"].ToString();
                        ddlBillable.SelectedValue = (dt.Rows[i]["EXPENSE_BILLABLE"].ToString() == "0") ? "Yes" : "No";
                        ddlPaidBy.SelectedValue = (dt.Rows[i]["EXPENSE_PAIDBYWHOM"].ToString() == "0") ? "Client" : "Self";

                        GridView gvEvidance = (GridView)e.Row.FindControl("gvEvidance");
                        BindGvEvidance(gvEvidance, dt.Rows[i]["PROJECT_CODE"].ToString(), dt.Rows[i]["MILESTONE_ID"].ToString(), dt.Rows[i]["WBS_ID"].ToString(), dt.Rows[i]["ACTIVITY_ID"].ToString(), dt.Rows[i]["RES_EXP_ROWID"].ToString(), dt.Rows[i]["EXPENSE_REQ_STATUS"].ToString());

                        txtPurpose.Enabled = false;
                        ddlMode.Enabled = false;
                        txtKM.Enabled = false;
                        txtAmount.Enabled = false;
                        ddlBillable.Enabled = false;
                        ddlPaidBy.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvEvidance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int i = e.Row.RowIndex;

                    DataTable dt = (DataTable)((GridView)sender).DataSource;
                    Label lblFlag = (Label)e.Row.FindControl("lblFlag");
                    TextBox txtExpenseID = (TextBox)e.Row.FindControl("txtExpenseID");
                    TextBox txtBillNo = (TextBox)e.Row.FindControl("txtBillNo");
                    TextBox txtDescription = (TextBox)e.Row.FindControl("txtDescription");
                    txtExpenseID.Text = dt.Rows[i]["RES_EXP_ROWID"].ToString();
                    txtBillNo.Text = dt.Rows[i]["EVIDENCE_BILLNO"].ToString();
                    txtDescription.Text = dt.Rows[i]["EVIDENCE_DESCRIPTION"].ToString();
                    if (txtExpenseID.Text == "")
                    {
                        lblFlag.Text = "NEW";
                    }
                    else
                    {
                        lblFlag.Text = "VIEW";
                    }
                    if (lblFlag.Text == "VIEW")
                    {
                        txtExpenseID.Enabled = false;
                        txtBillNo.Enabled = false;
                        txtDescription.Enabled = false;
                    }
                    else if (lblFlag.Text == "NEW")
                    {
                        txtExpenseID.Enabled = true;
                        txtBillNo.Enabled = true;
                        txtDescription.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void BindGvExpense()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from TE_RESOURCE_EXPENSE_DETAIL where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneId + "' and WBS_ID = '" + WBSID + "' and ACTIVITY_ID = '" + ActivityID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and EXPENSE_DATE = convert(datetime, '" + Date + "', 103)", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvExpense.DataSource = dt;
                gvExpense.DataBind();
                ViewState["Expense"] = dt;
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
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void BindGvEvidance(GridView gvEvidance, string ProjectCode, string MilestoneID, string WBSID, string ActivityID, string ExpenseID, string Status)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (Status == "NEW")
                {
                    SqlCommand cmd = new SqlCommand("Select * from dbo.TE_RESOURCE_EXP_EVIDENCE_DETAIL where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and ACTIVITY_ID = '" + ActivityID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and EXPENSE_ID = '" + "0" + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvEvidance.DataSource = dt;
                    gvEvidance.DataBind();
                    if (ViewState["Evidance_NEW_" + ((GridViewRow)gvEvidance.NamingContainer).RowIndex] == null)
                    {
                        ViewState["Evidance_NEW_" + ((GridViewRow)gvEvidance.NamingContainer).RowIndex] = dt;
                    }

                }
                else if (Status == "REQUESTED")
                {
                    SqlCommand cmd = new SqlCommand("Select * from dbo.TE_RESOURCE_EXP_EVIDENCE_DETAIL where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and ACTIVITY_ID = '" + ActivityID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and EXPENSE_ID = '" + ExpenseID + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvEvidance.DataSource = dt;
                    gvEvidance.DataBind();
                    if (ViewState["Evidance_OLD_" + ExpenseID] == null)
                    {
                        ViewState["Evidance_OLD_" + ExpenseID] = dt;
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
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {

                List<Expense> expense = new List<Expense>();
                for (int i = 0; i < gvExpense.Rows.Count; i++)
                {
                    expense.Add(new Expense());
                    expense[i].Purpose = ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtPurpose")).Text;
                    expense[i].Mode = ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlMode")).SelectedValue;
                    expense[i].KM = ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtKM")).Text;
                    expense[i].Amount = ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtAmount")).Text;
                    expense[i].Billable = ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlBillable")).SelectedValue;
                    expense[i].PaidBy = ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlPaidBy")).SelectedValue;
                    GridView gv = (GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance");
                    for (int j = 0; j < gv.Rows.Count; j++)
                    {
                        expense[i].evidance.Add(new Evidance());
                        expense[i].evidance[j].BillNo = ((TextBox)gv.Rows[j].Cells[2].FindControl("txtBillNo")).Text;
                        expense[i].evidance[j].Description = ((TextBox)gv.Rows[j].Cells[3].FindControl("txtDescription")).Text;
                    }
                }


                DataTable dt = (DataTable)ViewState["Expense"];
                dt.Rows.InsertAt(dt.NewRow(), dt.Rows.Count);
                gvExpense.DataSource = dt;
                gvExpense.DataBind();
                ViewState["Expense"] = dt;

                for (int i = 0; i < expense.Count; i++)
                {
                    ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtPurpose")).Text = expense[i].Purpose;
                    ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlMode")).SelectedValue = expense[i].Mode;
                    ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtKM")).Text = expense[i].KM;
                    ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtAmount")).Text = expense[i].Amount;
                    ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlBillable")).SelectedValue = expense[i].Billable;
                    ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlPaidBy")).SelectedValue = expense[i].PaidBy;
                    if (((Label)gvExpense.Rows[i].Cells[0].FindControl("lblFlagExpense")).Text == "NEW")
                    {
                        ((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).DataSource = (DataTable)ViewState["Evidance_NEW_" + i.ToString()];
                    }
                    else if (((Label)gvExpense.Rows[i].Cells[0].FindControl("lblFlagExpense")).Text != "NEW")
                    {
                        ((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).DataSource = ViewState["Evidance_OLD_" + ((Label)gvExpense.Rows[i].Cells[0].FindControl("lblExpenseID")).Text];
                    }
                    ((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).DataBind();

                    for (int j = 0; j < expense[i].evidance.Count; j++)
                    {
                        ((TextBox)((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).Rows[j].Cells[2].FindControl("txtBillNo")).Text = expense[i].evidance[j].BillNo;
                        ((TextBox)((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).Rows[j].Cells[3].FindControl("txtDescription")).Text = expense[i].evidance[j].Description;
                    }

                }


                //gvExpense.EditIndex = dt.Rows.Count;

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddEvidance")
            {
                string ExpenseID = e.CommandArgument.ToString();
                DataTable dt = new DataTable();
                if (ExpenseID == "")
                {
                    dt = (DataTable)ViewState["Evidance_NEW_" + ((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex];
                }
                else if (ExpenseID != "")
                {
                    dt = (DataTable)ViewState["Evidance_OLD_" + ExpenseID];
                }

                List<Evidance> evidance = new List<Evidance>();
                GridViewRow gvRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                GridView gvEvidance = (GridView)gvRow.FindControl("gvEvidance");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    evidance.Add(new Evidance());
                    evidance[i].BillNo = ((TextBox)gvEvidance.Rows[i].Cells[2].FindControl("txtBillNo")).Text;
                    evidance[i].Description = ((TextBox)gvEvidance.Rows[i].Cells[3].FindControl("txtDescription")).Text;
                }
                dt.Rows.InsertAt(dt.NewRow(), dt.Rows.Count);


                gvEvidance.DataSource = dt;
                if (ExpenseID == "")
                {
                    ViewState["Evidance_NEW_" + ((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex] = dt;
                }
                else if (ExpenseID != "")
                {
                    ViewState["Evidance_OLD_" + ExpenseID] = dt;
                }


                gvEvidance.DataBind();
                ((Label)gvEvidance.Rows[dt.Rows.Count - 1].Cells[0].FindControl("lblFlag")).Text = "NEW";
                for (int i = 0; i < evidance.Count; i++)
                {
                    ((TextBox)gvEvidance.Rows[i].Cells[2].FindControl("txtBillNo")).Text = evidance[i].BillNo;
                    ((TextBox)gvEvidance.Rows[i].Cells[3].FindControl("txtDescription")).Text = evidance[i].Description;
                }

            }

            if (e.CommandName == "DeleteExpense")
            {
                string ExpenseID = e.CommandArgument.ToString();

                GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblFlagExpense = (Label)gvRow.Cells[0].FindControl("lblFlagExpense");
                Label lblExpenseID = (Label)gvRow.Cells[0].FindControl("lblExpenseID");
                TextBox txtPurpose = (TextBox)gvRow.Cells[0].FindControl("txtPurpose");
                DropDownList ddlMode = (DropDownList)gvRow.Cells[0].FindControl("ddlMode");
                TextBox txtKM = (TextBox)gvRow.Cells[0].FindControl("txtKM");
                TextBox txtAmount = (TextBox)gvRow.Cells[0].FindControl("txtAmount");
                DropDownList ddlBillable = (DropDownList)gvRow.Cells[0].FindControl("ddlBillable");
                DropDownList ddlPaidBy = (DropDownList)gvRow.Cells[0].FindControl("ddlPaidBy");
                GridView gvEvidance = (GridView)gvRow.Cells[0].FindControl("gvEvidance");

                List<Expense> expense = new List<Expense>();
                for (int i = 0; i < gvExpense.Rows.Count; i++)
                {
                    expense.Add(new Expense());
                    expense[i].Purpose = ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtPurpose")).Text;
                    expense[i].Mode = ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlMode")).SelectedValue;
                    expense[i].KM = ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtKM")).Text;
                    expense[i].Amount = ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtAmount")).Text;
                    expense[i].Billable = ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlBillable")).SelectedValue;
                    expense[i].PaidBy = ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlPaidBy")).SelectedValue;
                    GridView gv = (GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance");
                    for (int j = 0; j < gv.Rows.Count; j++)
                    {
                        expense[i].evidance.Add(new Evidance());
                        expense[i].evidance[j].BillNo = ((TextBox)gv.Rows[j].Cells[2].FindControl("txtBillNo")).Text;
                        expense[i].evidance[j].Description = ((TextBox)gv.Rows[j].Cells[3].FindControl("txtDescription")).Text;
                    }
                }



                if (lblFlagExpense.Text == "NEW")
                {
                    DataTable dt = (DataTable)ViewState["Expense"];
                    dt.Rows.RemoveAt(gvRow.RowIndex);
                    expense.RemoveAt(gvRow.RowIndex);
                    ViewState["Expense"] = dt;
                    DataTable dt1 = (DataTable)ViewState["Evidance_NEW_" + ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex];
                    dt1.Rows.Clear();
                    ViewState["Evidance_NEW_" + ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex] = dt1;
                    gvExpense.DataSource = dt;
                    gvExpense.DataBind();
                    //gvExpense.DeleteRow(gvRow.RowIndex);
                }
                else if (lblFlagExpense.Text == "EDIT")
                {
                    DataTable dt = (DataTable)ViewState["Expense"];
                    dt.Rows.RemoveAt(gvRow.RowIndex);
                    expense.RemoveAt(gvRow.RowIndex);
                    ViewState["Expense"] = dt;
                    DataTable dt1 = (DataTable)ViewState["Evidance_OLD_" + ExpenseID];
                    dt1.Rows.Clear();
                    ViewState["Evidance_OLD_" + ExpenseID] = dt1;
                    gvExpense.DataSource = dt;
                    gvExpense.DataBind();

                    //gvExpense.DeleteRow(gvRow.RowIndex);

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Delete from TE_RESOURCE_EXPENSE_DETAIL where RES_EXP_ROWID = '" + ExpenseID + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                else if (lblFlagExpense.Text == "VIEW")
                {

                }

                for (int i = 0; i < expense.Count; i++)
                {
                    ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtPurpose")).Text = expense[i].Purpose;
                    ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlMode")).SelectedValue = expense[i].Mode;
                    ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtKM")).Text = expense[i].KM;
                    ((TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtAmount")).Text = expense[i].Amount;
                    ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlBillable")).SelectedValue = expense[i].Billable;
                    ((DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlPaidBy")).SelectedValue = expense[i].PaidBy;
                    if (((Label)gvExpense.Rows[i].Cells[0].FindControl("lblFlagExpense")).Text == "NEW")
                    {
                        ((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).DataSource = (DataTable)ViewState["Evidance_NEW_" + i.ToString()];
                    }
                    else if (((Label)gvExpense.Rows[i].Cells[0].FindControl("lblFlagExpense")).Text != "NEW")
                    {
                        ((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).DataSource = ViewState["Evidance_OLD_" + ((Label)gvExpense.Rows[i].Cells[0].FindControl("lblExpenseID")).Text];
                    }
                    ((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).DataBind();
                    for (int j = 0; j < expense[i].evidance.Count; j++)
                    {
                        ((TextBox)((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).Rows[j].Cells[2].FindControl("txtBillNo")).Text = expense[i].evidance[j].BillNo;
                        ((TextBox)((GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance")).Rows[j].Cells[3].FindControl("txtDescription")).Text = expense[i].evidance[j].Description;
                    }
                }
            }
        }

        protected void gvEvidance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteEvidance")
            {

                GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridView gvEvidance = (GridView)gvRow.NamingContainer;
                GridViewRow gvRow1 = (GridViewRow)gvEvidance.NamingContainer;
                string expenseID = ((Label)gvRow1.FindControl("lblExpenseID")).Text;
                Label lblFlag = (Label)gvRow.FindControl("lblFlag");
                TextBox txtEvidanceID = (TextBox)gvRow.FindControl("txtExpenseID");
                List<Evidance> evidance = new List<Evidance>();
                for (int i = 0; i < gvEvidance.Rows.Count; i++)
                {
                    evidance.Add(new Evidance());
                    evidance[i].BillNo = ((TextBox)gvEvidance.Rows[i].Cells[2].FindControl("txtBillNo")).Text;
                    evidance[i].Description = ((TextBox)gvEvidance.Rows[i].Cells[3].FindControl("txtDescription")).Text;
                }
                if (lblFlag.Text == "VIEW")
                {
                    DataTable dt = (DataTable)ViewState["Evidance_OLD_" + expenseID];
                    dt.Rows.RemoveAt(gvRow.RowIndex);
                    gvEvidance.DataSource = dt;
                    gvEvidance.DataBind();
                    ViewState["Evidance_OLD_" + expenseID] = dt;
                    evidance.RemoveAt(gvRow.RowIndex);
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("Delete from TE_RESOURCE_EXP_EVIDENCE_DETAIL where RES_EXP_ROWID = '" + ((TextBox)gvRow.Cells[1].FindControl("txtExpenseID")).Text + "'", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
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
                        lblError.Text = ex.Message;
                        lblError.Visible = true;
                    }
                }
                else if (lblFlag.Text == "NEW")
                {
                    DataTable dt = (DataTable)ViewState["Evidance_NEW_" + gvRow1.RowIndex];
                    dt.Rows.RemoveAt(gvRow.RowIndex);
                    gvEvidance.DataSource = dt;
                    gvEvidance.DataBind();
                    ViewState["Evidance_NEW_" + gvRow1.RowIndex] = dt;
                    evidance.RemoveAt(gvRow.RowIndex);
                }

                for (int i = 0; i < evidance.Count; i++)
                {
                    ((TextBox)gvEvidance.Rows[i].Cells[2].FindControl("txtBillNo")).Text = evidance[i].BillNo;
                    ((TextBox)gvEvidance.Rows[i].Cells[3].FindControl("txtDescription")).Text = evidance[i].Description;
                }
                //BindGvExpense();
            }



        }

        protected void btnSubmitExpense_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                try
                {
                    for (int i = 0; i < gvExpense.Rows.Count; i++)
                    {
                        Label lblFlagExpense = (Label)gvExpense.Rows[i].Cells[0].FindControl("lblFlagExpense");
                        Label lblExpenseID = (Label)gvExpense.Rows[i].Cells[0].FindControl("lblExpenseID");
                        TextBox txtPurpose = (TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtPurpose");
                        DropDownList ddlMode = (DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlMode");
                        TextBox txtKM = (TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtKM");
                        TextBox txtAmount = (TextBox)gvExpense.Rows[i].Cells[0].FindControl("txtAmount");
                        DropDownList ddlBillable = (DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlBillable");
                        DropDownList ddlPaidBy = (DropDownList)gvExpense.Rows[i].Cells[0].FindControl("ddlPaidBy");
                        string ExpenseID = "";
                        if (lblFlagExpense.Text == "NEW")
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            SqlCommand cmd;
                            if (txtKM.Enabled == true)
                            {
                                cmd = new SqlCommand("insert into TE_RESOURCE_EXPENSE_DETAIL (PROJECT_CODE, MILESTONE_ID, WBS_ID, ACTIVITY_ID, RESOURCE_ID, EXPENSE_DATE, EXPENSE_PURPOSE, EXPENSE_MODE, EXPENSE_KMSIFOWNED, EXPENSE_AMOUNT, EXPENSE_BILLABLE, EXPENSE_PAIDBYWHOM, MODIFIEDBY, MODIFIED_DATE, EXPENSE_REQ_STATUS) VALUES ('" + ProjectCode + "','" + MilestoneId + "','" + WBSID + "','" + ActivityID + "','" + Session["uid"].ToString() + "',convert(datetime, '" + Date + "', 103),'" + txtPurpose.Text + "','" + ddlMode.SelectedValue + "'," + ((txtKM.Text == "") ? "null" : txtKM.Text) + ",'" + txtAmount.Text + "','" + ((ddlBillable.SelectedValue == "Yes") ? "1" : "0") + "','" + ((ddlPaidBy.SelectedValue == "Client") ? "1" : "0") + "','" + Session["uid"].ToString() + "',getdate(),'REQUESTED'); Select ident_current('TE_RESOURCE_EXPENSE_DETAIL');", conn);

                            }
                            else
                            {
                                cmd = new SqlCommand("insert into TE_RESOURCE_EXPENSE_DETAIL (PROJECT_CODE, MILESTONE_ID, WBS_ID, ACTIVITY_ID, RESOURCE_ID, EXPENSE_DATE, EXPENSE_PURPOSE, EXPENSE_MODE, EXPENSE_AMOUNT, EXPENSE_BILLABLE, EXPENSE_PAIDBYWHOM, MODIFIEDBY, MODIFIED_DATE, EXPENSE_REQ_STATUS) VALUES ('" + ProjectCode + "','" + MilestoneId + "','" + WBSID + "','" + ActivityID + "','" + Session["uid"].ToString() + "',convert(datetime, '" + Date + "', 103),'" + txtPurpose.Text + "','" + ddlMode.SelectedValue + "','" + txtAmount.Text + "','" + ((ddlBillable.SelectedValue == "Yes") ? "1" : "0") + "','" + ((ddlPaidBy.SelectedValue == "Client") ? "1" : "0") + "','" + Session["uid"].ToString() + "',getdate(),'REQUESTED'); Select ident_current('TE_RESOURCE_EXPENSE_DETAIL');", conn);

                            }
                            cmd.CommandType = CommandType.Text;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            ExpenseID = dt.Rows[0][0].ToString();
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                        else if (lblFlagExpense.Text == "EDIT")
                        {
                            ExpenseID = lblExpenseID.Text;
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            if (ddlMode.SelectedValue == "Owned")
                            {
                                SqlCommand cmd = new SqlCommand("update TE_RESOURCE_EXPENSE_DETAIL set EXPENSE_PURPOSE = '" + txtPurpose.Text + "',EXPENSE_MODE = '" + ddlMode.SelectedValue + "',EXPENSE_KMSIFOWNED = " + ((txtKM.Text == "") ? "0" : txtKM.Text) + ",EXPENSE_AMOUNT = '" + txtAmount.Text + "',EXPENSE_BILLABLE = '" + ((ddlBillable.SelectedValue == "Yes") ? "1" : "0") + "',EXPENSE_PAIDBYWHOM = '" + ((ddlPaidBy.SelectedValue == "Client") ? "1" : "0") + "',MODIFIEDBY = '" + Session["uid"].ToString() + "',MODIFIED_DATE = GETDATE() WHERE RES_EXP_ROWID = '" + ExpenseID + "'", conn);
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmd = new SqlCommand("update TE_RESOURCE_EXPENSE_DETAIL set EXPENSE_PURPOSE = '" + txtPurpose.Text + "',EXPENSE_MODE = '" + ddlMode.SelectedValue + "', EXPENSE_AMOUNT = '" + txtAmount.Text + "',EXPENSE_BILLABLE = '" + ((ddlBillable.SelectedValue == "Yes") ? "1" : "0") + "',EXPENSE_PAIDBYWHOM = '" + ((ddlPaidBy.SelectedValue == "Client") ? "1" : "0") + "',MODIFIEDBY = '" + Session["uid"].ToString() + "',MODIFIED_DATE = GETDATE() WHERE RES_EXP_ROWID = '" + ExpenseID + "'", conn);
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                            }
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                        else if (lblFlagExpense.Text == "VIEW")
                        {
                            ExpenseID = lblExpenseID.Text;
                        }


                        GridView gvEvidance = (GridView)gvExpense.Rows[i].Cells[0].FindControl("gvEvidance");
                        for (int j = 0; j < gvEvidance.Rows.Count; j++)
                        {
                            Label lblFlag = (Label)gvEvidance.Rows[j].Cells[0].FindControl("lblFlag");
                            TextBox txtExpenseID = (TextBox)gvEvidance.Rows[j].Cells[1].FindControl("txtExpenseID");
                            TextBox txtBillNo = (TextBox)gvEvidance.Rows[j].Cells[2].FindControl("txtBillNo");
                            TextBox txtDescription = (TextBox)gvEvidance.Rows[j].Cells[3].FindControl("txtDescription");
                            if (lblFlag.Text == "NEW")
                            {
                                try
                                {
                                    if (conn.State == ConnectionState.Closed)
                                    {
                                        conn.Open();
                                    }
                                    SqlCommand cmd = new SqlCommand("insert into TE_RESOURCE_EXP_EVIDENCE_DETAIL (EXPENSE_ID, PROJECT_CODE, MILESTONE_ID, WBS_ID, ACTIVITY_ID, RESOURCE_ID, EVIDENCE_BILLNO, EVIDENCE_DESCRIPTION) values ('" + ExpenseID + "', '" + ProjectCode + "', '" + MilestoneId + "', '" + WBSID + "', '" + ActivityID + "', '" + Session["uid"].ToString() + "', '" + txtBillNo.Text + "', '" + txtDescription.Text + "')", conn);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
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
                                    lblError.Text = ex.Message;
                                    lblError.Visible = true;
                                }
                            }
                        }
                    }
                    Response.Redirect("ExpenseEntry.aspx?ProjectCode=" + ProjectCode + "&MilestoneId=" + MilestoneId + "&WBSID=" + WBSID + "&ActivityID=" + ActivityID + "&Date=" + Date + "", true);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
            }
        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMode = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)ddlMode.NamingContainer;
            if (ddlMode.SelectedValue == "Owned")
            {
                TextBox txtKM = (TextBox)gvRow.Cells[0].FindControl("txtKM");
                txtKM.Enabled = true;
            }
            else
            {
                TextBox txtKM = (TextBox)gvRow.Cells[0].FindControl("txtKM");
                txtKM.Text = "";
                txtKM.Enabled = false;
            }
        }

        protected void cvtxtKM_OnServerValidate(object sender, ServerValidateEventArgs args)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)((CustomValidator)sender).NamingContainer;
                DropDownList ddlMode = (DropDownList)gvRow.Cells[0].FindControl("ddlMode");
                TextBox txtKM = (TextBox)gvRow.Cells[0].FindControl("txtKM");
                if (ddlMode.SelectedValue == "Owned")
                {
                    if (txtKM.Text == "")
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        int distance;
                        if (!int.TryParse(txtKM.Text, out distance))
                        {
                            args.IsValid = false;
                        }
                        else
                        {
                            args.IsValid = true;
                        }
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

    }

    public class Evidance
    {
        public string BillNo = "";
        public string Description = "";

        public Evidance()
        {
            BillNo = "";
            Description = "";
        }
    }

    public class Expense
    {
        public string Purpose = "";
        public string Mode = "";
        public string KM = "";
        public string Amount = "";
        public string Billable = "";
        public string PaidBy = "";
        public List<Evidance> evidance = new List<Evidance>();
        public Expense()
        {
            Purpose = "";
            Mode = "";
            KM = "";
            Amount = "";
            Billable = "";
            PaidBy = "";
            evidance = new List<Evidance>();
        }
    }


}