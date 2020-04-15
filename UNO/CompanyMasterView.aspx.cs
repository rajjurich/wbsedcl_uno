using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using CMS.UNO.Core.Handler;
using System.Text;

namespace UNO
{
    public partial class CompanyMasterView : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                FillStates();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvCompany.ClientID + "');");
            }
        }

        private void FillStates()
        {
            try
            {

                DataTable dt = clsCommonHandler.GetEntParameterValues("STATES", "STATES");

                ddlRegisteredStateAdd.DataValueField = "CODE";
                ddlRegisteredStateAdd.DataTextField = "Value";
                ddlRegisteredStateAdd.DataSource = dt;
                ddlRegisteredStateAdd.DataBind();
                ddlRegisteredStateAdd.Items.Insert(0, new ListItem("Select One", "0"));

                ddlHOStateAdd.DataValueField = "CODE";
                ddlHOStateAdd.DataTextField = "Value";
                ddlHOStateAdd.DataSource = dt;
                ddlHOStateAdd.DataBind();
                ddlHOStateAdd.Items.Insert(0, new ListItem("Select One", "0"));

                ddlRegisteredStateEdit.DataValueField = "CODE";
                ddlRegisteredStateEdit.DataTextField = "Value";
                ddlRegisteredStateEdit.DataSource = dt;
                ddlRegisteredStateEdit.DataBind();
                ddlRegisteredStateEdit.Items.Insert(0, new ListItem("Select One", "0"));

                ddlHOStateEdit.DataValueField = "CODE";
                ddlHOStateEdit.DataTextField = "Value";
                ddlHOStateEdit.DataSource = dt;
                ddlHOStateEdit.DataBind();
                ddlHOStateEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }

        }

        void bindDataGrid()
        {
            try
            {


                DataTable dt = clsCompanyHandler.GetAllDetails("All");

                gvCompany.DataSource = dt;
                gvCompany.DataBind();

                DropDownList ddl = (DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCompany.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCompany.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCompany.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCompany.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCompany.PageCount == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex + 1 == gvCompany.PageCount)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvCompany.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCompany.PageSize * gvCompany.PageIndex) + 1) + " to " + (((gvCompany.PageSize * (gvCompany.PageIndex + 1)) - 10) + gvCompany.Rows.Count);

                gvCompany.BottomPagerRow.Visible = true;


                if (dt.Rows.Count != 0)
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCompany.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = Convert.ToInt32(((DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = gvCompany.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = gvCompany.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddCompany.Hide();
            lblError.Text = string.Empty;
            lblError.Visible = false;
        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyIDAdd.Text.Trim() != null && txtDescriptionAdd.Text.Trim() != null)
                {
                    clsCompany objCompany = new clsCompany();
                    objCompany.CreatedBy = Session["uid"].ToString();
                    objCompany.CompId = txtCompanyIDAdd.Text.Trim().ToUpper();
                    objCompany.CompName = txtDescriptionAdd.Text.Trim();
                    objCompany.CompAddress = txtRegisteredAddressAdd.Text.Trim();
                    objCompany.CompROAddress = txtHOAddressAdd.Text.Trim();
                    objCompany.CompCity = txtRegisteredCityAdd.Text.Trim();
                    objCompany.CompROCity = txtHOCityAdd.Text.Trim();
                    objCompany.CompPin = txtRegisteredPinAdd.Text.Trim();
                    objCompany.CompROPin = txtHOPinAdd.Text.Trim();
                    objCompany.CompPhone1 = txtRegisteredPhone1Add.Text.Trim();
                    objCompany.CompPhone2 = txtRegisteredPhone2Add.Text.Trim();
                    objCompany.CompROPhone1 = txtHOPhone1Add.Text.Trim();
                    objCompany.CompROPhone2 = txtHOPhone2Add.Text.Trim();
                    objCompany.CompState = ((ddlRegisteredStateAdd.SelectedIndex == 0) ? "" : ddlRegisteredStateAdd.SelectedValue);
                    objCompany.CompROState = ((ddlHOStateAdd.SelectedIndex == 0) ? "" : ddlHOStateAdd.SelectedValue);

                    clsCompanyHandler.InsertUpdateCompanyDetails(objCompany, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblError.Visible = true;
                        // lblError.Style.Add("display", "inline");
                        lblError.Text = strErrMsg;
                        mpeAddCompany.Show();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Script", "validateChosen();", true);
                        return;
                    }
                    else
                    {
                        txtCompanyIDAdd.Text = "";
                        txtDescriptionAdd.Text = "";
                        txtRegisteredAddressAdd.Text = "";
                        txtRegisteredCityAdd.Text = "";
                        txtRegisteredPinAdd.Text = "";
                        ddlRegisteredStateAdd.SelectedIndex = 0;
                        txtRegisteredPhone1Add.Text = "";
                        txtRegisteredPhone2Add.Text = "";
                        ChkAddressAdd.Checked = false;
                        txtHOAddressAdd.Text = "";
                        txtHOCityAdd.Text = "";
                        txtHOPinAdd.Text = "";
                        ddlHOStateAdd.SelectedIndex = 0;
                        txtHOPhone1Add.Text = "";
                        txtHOPhone2Add.Text = "";
                        lblError.Text = strSuccMsg;
                        lblError.Visible = true;
                        bindDataGrid();
                        mpeAddCompany.Show();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Script", "validateChosen();", true);
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Company ID should not be null or empty.";
                    mpeAddCompany.Show();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Script", "validateChosen();", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            try
            {
                clsCompany objCompany = new clsCompany();
                objCompany.CreatedBy = Session["uid"].ToString();
                objCompany.CompId = txtCompanyIDEdit.Text.Trim().ToUpper();
                objCompany.CompName = txtDesciptionEdit.Text.Trim();
                objCompany.CompAddress = txtRegisteredAddressEdit.Text.Trim();
                objCompany.CompROAddress = txtHOAddressEdit.Text.Trim();
                objCompany.CompCity = txtRegisteredCityEdit.Text.Trim();
                objCompany.CompROCity = txtHOCityEdit.Text.Trim();
                objCompany.CompPin = txtRegisteredPinEdit.Text.Trim();
                objCompany.CompROPin = txtHOPinEdit.Text.Trim();
                objCompany.CompPhone1 = txtRegisteredPhone1Edit.Text.Trim();
                objCompany.CompPhone2 = txtRegisteredPhone2Edit.Text.Trim();
                objCompany.CompROPhone1 = txtHOPhone1Edit.Text.Trim();
                objCompany.CompROPhone2 = txtHOPhone2Edit.Text.Trim();
                objCompany.CompState = ((ddlRegisteredStateEdit.SelectedIndex == 0) ? "" : ddlRegisteredStateEdit.SelectedValue);
                objCompany.CompROState = ((ddlHOStateEdit.SelectedIndex == 0) ? "" : ddlHOStateEdit.SelectedValue);

                clsCompanyHandler.InsertUpdateCompanyDetails(objCompany, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                if (strErrMsg.Trim().Length >= 1)
                {

                    lblErrorEdit.Text = strErrMsg;
                    lblErrorEdit.Visible = true;
                    mpeEditCompany.Show();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
                    return;
                }
                else
                {
                    mpeEditCompany.Hide();
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    bindDataGrid();
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEditCompany.Hide();
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    lblErrorEdit.Text = "";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
                    txtDesciptionEdit.Focus();
                    string CompanyId = e.CommandArgument.ToString();
                    clsCompany objCompany = clsCompanyHandler.GetCompanyDetails("Company", CompanyId);

                    if (objCompany != null)
                    {
                        txtCompanyIDEdit.Text = CompanyId;
                        txtDesciptionEdit.Text = objCompany.CompName;
                        txtRegisteredAddressEdit.Text = objCompany.CompAddress;
                        txtRegisteredCityEdit.Text = objCompany.CompCity;
                        txtRegisteredPinEdit.Text = objCompany.CompPin;
                        ddlRegisteredStateEdit.SelectedValue = ((objCompany.CompState == "") ? "0" : objCompany.CompState);
                        txtRegisteredPhone1Edit.Text = objCompany.CompPhone1;
                        txtRegisteredPhone2Edit.Text = objCompany.CompPhone2;
                        if (objCompany.CompAddress.Trim() == objCompany.CompROAddress.Trim() && objCompany.CompCity.Trim() == objCompany.CompROCity.Trim() && objCompany.CompPhone1.Trim() == objCompany.CompROPhone1.Trim() && objCompany.CompPhone2.Trim() == objCompany.CompROPhone2.Trim() && objCompany.CompState.Trim() == objCompany.CompROState.Trim() )
                        {
                            ChkAddressEdit.Checked = true;
                            txtHOAddressEdit.Enabled = false;
                            txtHOCityEdit.Enabled = false;
                            txtHOPinEdit.Enabled = false;
                            ddlHOStateEdit.Enabled = false;
                            txtHOPhone1Edit.Enabled = false;
                            txtHOPhone2Edit.Enabled = false;
                        }
                        else
                        {
                            ChkAddressEdit.Checked = false;
                            txtHOAddressEdit.Enabled = true;
                            txtHOCityEdit.Enabled = true;
                            txtHOPinEdit.Enabled = true;
                            ddlHOStateEdit.Enabled = true;
                            txtHOPhone1Edit.Enabled = true;
                            txtHOPhone2Edit.Enabled = true;
                        }
                        txtHOAddressEdit.Text = objCompany.CompROAddress.Trim();
                        txtHOCityEdit.Text = objCompany.CompROCity.Trim();
                        txtHOPinEdit.Text = objCompany.CompROPin.Trim();
                        ddlHOStateEdit.SelectedValue = ((objCompany.CompROState.Trim() == "") ? "0" : objCompany.CompROState.Trim());
                        txtHOPhone1Edit.Text = objCompany.CompROPhone1.Trim();
                        txtHOPhone2Edit.Text = objCompany.CompROPhone2.Trim();
                        txtCompanyIDEdit.Enabled = false;

                        ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
                        mpeEditCompany.Show();
                    }
                    else
                    {
                        lblMessages.Text = "Records not found";
                        lblMessages.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            string strSql = "", str = "";
            try
            {
                for (int i = 0; i < gvCompany.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvCompany.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        strSql = "Delete ent_company where company_id ='" + gvCompany.Rows[i].Cells[2].Text + "'";

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand(strSql, conn);
                        cmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
                lblMessages.Text = "Company Deleted Sucessfully";
                lblMessages.Visible = true;
                bindDataGrid();                
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            //clsCompany objCompany = new clsCompany();
            //objCompany.CreatedBy = Session["uid"].ToString();
            //StringBuilder strXML = new StringBuilder();
            //try
            //{
            //    strXML.Append("<ENT_COMPANY>");
            //    for (int i = 0; i < gvCompany.Rows.Count; i++)
            //    {

            //        CheckBox chk = (CheckBox)gvCompany.Rows[i].FindControl("DeleteRows");
            //        if (chk.Checked)
            //        {
            //            strXML.Append("<Company>");
            //            strXML.Append("<COMPANY_ID>" + gvCompany.Rows[i].Cells[2].Text + "</COMPANY_ID>");
            //            strXML.Append("</Company>");
            //        }
            //    }
            //    strXML.Append("</ENT_COMPANY>");
            //    clsCompanyHandler.InsertUpdateCompanyDetails(objCompany, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
            //    if (strSuccMsg.Trim().Length >= 1)
            //    {
            //        lblMessages.Text = strSuccMsg;
            //        lblMessages.Visible = true;
            //        bindDataGrid();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsCompanyHandler.GetAllDetails("All");


                if (txtCompanyID.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
                {
                    gvCompany.DataSource = dt;
                    gvCompany.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"COMPANY_ID ~" +txtCompanyID.Text.Trim(), "S" },
				{"COMPANY_NAME ~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search(); 
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvCompany.DataSource = _tempDT;
                    gvCompany.DataBind();
                }

                DropDownList ddl = (DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCompany.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCompany.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCompany.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCompany.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCompany.PageCount == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex + 1 == gvCompany.PageCount)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }


                ((Label)gvCompany.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCompany.PageSize * gvCompany.PageIndex) + 1) + " to " + (((gvCompany.PageSize * (gvCompany.PageIndex + 1)) - 10) + gvCompany.Rows.Count);

                gvCompany.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            txtCompanyIDAdd.Text = "";
            txtDescriptionAdd.Text = "";
            txtRegisteredAddressAdd.Text = "";
            txtRegisteredCityAdd.Text = "";
            txtRegisteredPinAdd.Text = "";
            ddlRegisteredStateAdd.SelectedIndex = 0;
            txtRegisteredPhone1Add.Text = "";
            txtRegisteredPhone2Add.Text = "";
            ChkAddressAdd.Checked = false;
            txtHOAddressAdd.Text = "";
            txtHOCityAdd.Text = "";
            txtHOPinAdd.Text = "";
            ddlHOStateAdd.SelectedIndex = 0;
            txtHOPhone1Add.Text = "";
            txtHOPhone2Add.Text = "";
            txtCompanyIDAdd.Focus();
            mpeAddCompany.Show();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Script", "validateChosen();", true);
        }

        protected void gvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Added by Pooja Yadav
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
                CheckBox chkDeleteRows = (CheckBox)e.Row.FindControl("DeleteRows");
                if ((hdnFlag.Value.Equals("1", StringComparison.CurrentCultureIgnoreCase)))
                    chkDeleteRows.Enabled = false;
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }



    }
}