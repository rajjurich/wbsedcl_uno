using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

namespace UNO
{
    public partial class EnglishToHindiView : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string EmpId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null || Convert.ToString(Session["uid"]) == "")
            {
                Response.Redirect("Login.aspx", true);
            }
            EmpId = Convert.ToString(Session["uid"]);
            if (!Page.IsPostBack)
            {
                bindDataGrid();
            }
        }
        private void bindDataGrid()
        {
            try
            {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter("EXEC Sp_InsertUnicode @strCommand ='View'", conn);
            DataSet dsTemplate = new DataSet();
            da.Fill(dsTemplate);
            conn.Close();
            gvView.DataSource = dsTemplate.Tables[0];
            gvView.DataBind();
            
                DropDownList ddl = (DropDownList)gvView.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvView.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvView.PageIndex + 1).ToString();
                Label lblcount = (Label)gvView.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvView.DataSource).Rows.Count.ToString() + " Records.";
                if (gvView.PageCount == 0)
                {
                    ((Button)gvView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvView.PageIndex + 1 == gvView.PageCount)
                {
                    ((Button)gvView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvView.PageIndex == 0)
                {
                    ((Button)gvView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvView.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvView.PageSize * gvView.PageIndex) + 1) + " to " + (gvView.PageSize * (gvView.PageIndex + 1));

                gvView.BottomPagerRow.Visible = true;


                if (dsTemplate.Tables[0].Rows.Count != 0)
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                strsql = "EXEC Sp_InsertUnicode @strCommand ='View'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


                if (txtCtlrID.Text.ToString() == "" && txtCtlrDesc.Text.ToString() == "")
                {
                    gvView.DataSource = dt;
                    gvView.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"EMP_NAME~" +txtCtlrID.Text.Trim(), "S" },
				{"EPD_EMPID~" +txtCtlrDesc.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvView.DataSource = _tempDT;
                    gvView.DataBind();
                }

                DropDownList ddl = (DropDownList)gvView.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvView.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvView.PageIndex + 1).ToString();
                Label lblcount = (Label)gvView.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvView.DataSource).Rows.Count.ToString() + " Records.";
                if (gvView.PageCount == 0)
                {
                    ((Button)gvView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvView.PageIndex + 1 == gvView.PageCount)
                {
                    ((Button)gvView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvView.PageIndex == 0)
                {
                    ((Button)gvView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvView.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvView.PageSize * gvView.PageIndex) + 1) + " to " + (gvView.PageSize * (gvView.PageIndex + 1));

                gvView.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Check = false;
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<UNICODE>");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                for (int i = 0; i < gvView.Rows.Count; i++)
                {
                    try
                    {

                        CheckBox delrows = (CheckBox)gvView.Rows[i].FindControl("DeleteRows");
                        if (delrows.Checked == true)
                        {
                            strXML.Append("<Hindi>");
                            strXML.Append("<id>" + gvView.Rows[i].Cells[2].Text + "</id>");
                            strXML.Append("</Hindi>");



                            Check = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
                    }

                }
                strXML.Append("</UNICODE>");
                SqlCommand cmd = new SqlCommand("Sp_InsertUnicode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "Delete";
                cmd.Parameters.Add("@strXML", SqlDbType.NText).Value = strXML.ToString();
                cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = "EnglishToHindiView.aspx";
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = EmpId;
                cmd.Parameters.Add("@strSuccOut", SqlDbType.VarChar,50);
                cmd.Parameters["@strSuccOut"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (Check == true)
                {
                    lblMessages.Visible = true;
                    lblMessages.Text = cmd.Parameters["@strSuccOut"].Value.ToString();

                }

                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
            }
        }

        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvView.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
            }
        }

        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    string EMPID = e.CommandArgument.ToString();
                    Response.Redirect("EnglishToHindi.aspx?id=" + EMPID + "", true);
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvView.PageIndex = gvView.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvView.PageIndex = gvView.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvView.PageIndex = Convert.ToInt32(((DropDownList)gvView.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnglishToHindiView");
            }
        }

       
    }
}