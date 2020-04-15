using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CMS.UNO.Tempus.Handler;
using System.Text;
namespace UNO
{
    public partial class CategaryMasterView : System.Web.UI.Page
    {       
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Form.DefaultButton = btnSearch.UniqueID;
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                ViewState["PageMode"] = "Add";
                ErlD.Enabled = false;
                LtD.Enabled = false;
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvCategory.ClientID + "');");
            }

        }
        public void FillddlCategoryEdit()
        {

            DataTable dt = clsCategoryMasterViewHandler.GetCategoryDetails("FillEditCat");
            if (dt.Rows.Count > 0)
            {
                ddlCategory.DataValueField = "CAT_CATEGORY_ID";
                ddlCategory.DataTextField = "CAT_CATEGORY_ID";
                ddlCategory.DataSource = dt;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, "Select One");
            }
        }
        private void FillddlCategory()
        {

            try
            {

                DataTable dt = clsCategoryMasterViewHandler.GetCategoryDetails("FillRemainCat");

                if (dt.Rows.Count > 0)
                {
                    ddlCategory.DataValueField = "oce_id";
                    ddlCategory.DataTextField = "oce_desc";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "Select One");

                }
                else
                {
                    ddlCategory.DataValueField = "oce_id";
                    ddlCategory.DataTextField = "oce_desc";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();
                    ddlCategory.Enabled = false;
                    ddlCategory.Items.Insert(0, "Already Configured");
                }


            }
            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReasonMasterView");
            }


        }
        void bindDataGrid()
        {
            try
            {

                DataTable dt = clsCategoryMasterViewHandler.GetCategoryDetails("All");

                gvCategory.DataSource = dt;
                gvCategory.DataBind();
                DropDownList ddl = (DropDownList)gvCategory.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCategory.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCategory.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCategory.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCategory.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCategory.PageCount == 0)
                {
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCategory.PageIndex + 1 == gvCategory.PageCount)
                {
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCategory.PageIndex == 0)
                {
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvCategory.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCategory.PageSize * gvCategory.PageIndex) + 1) + " to " + (((gvCategory.PageSize * (gvCategory.PageIndex + 1)) - 10) + gvCategory.Rows.Count);
                gvCategory.BottomPagerRow.Visible = true;


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
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCategory.PageIndex = Convert.ToInt32(((DropDownList)gvCategory.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CategoryMasterView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCategory.PageIndex = gvCategory.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CategoryMasterView");
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCategory.PageIndex = gvCategory.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CategoryMasterView");
            }

        }
        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCategory.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CategoryMasterView");
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(updatepanel4, updatepanel4.GetType(), "Script", "validateChosen();", true);
            ViewState["PageMode"] = "Add";

            Erl_Alwd.Text = "";
            Lt_Alwd.Text = "";
            Bef_Shft.Text = "";
            Aft_shft.Text = "";
            Comp_Sts.Text = "";
            ChkAllowed.Checked = false;
            ChkW1.Checked = false;
            ChkW2.Checked = false;
            ChkHO.Checked = false;
            ChkWD.Checked = false;
            ErlD.Checked = false;
            LtD.Checked = false;
            ddlCategory.Visible = true;
            ddlCategory.Enabled = true;
            Bef_Shft.Enabled = false;
            Aft_shft.Enabled = false;
            lblMessageAdd.Text = "";
            FillddlCategory();
            ddlCategory.SelectedIndex = 0;
            if (ddlCategory.SelectedItem.Text == "Already Configured")
            {
                mpeMessage.Show();
            }
            else
                mpeAddCategory.Show();


        }
        protected void Modify_Data(string CATID)
        {
            try
            {


                clsCategory objData = clsCategoryMasterViewHandler.GetCategoryDetails("All", Convert.ToInt16(CATID));

                if (objData != null)
                {
                    ddlCategory.SelectedValue = objData.CatID;
                    ddlCategory.Visible = true;
                    ddlCategory.Enabled = false;

                    if (objData.EarlyGoing == "" || objData.EarlyGoing == "00:00")
                    {
                        Erl_Alwd.Text = "";
                    }
                    else
                    {
                        Erl_Alwd.Text = objData.EarlyGoing.ToString();
                    }
                    if (objData.LateComing == "" || objData.LateComing == "00:00")
                    {
                        Lt_Alwd.Text = "";
                    }
                    else
                    {
                        Lt_Alwd.Text = objData.LateComing.ToString();
                    }

                    if (objData.CatExtraCheck == true)
                    {
                        ChkAllowed.Checked = true;
                        Bef_Shft.Enabled = true;
                        Aft_shft.Enabled = true;
                        ErlD.Enabled = true;
                        LtD.Enabled = true;
                    }
                    else
                    {
                        ChkAllowed.Checked = false;
                        Bef_Shft.Enabled = false;
                        Aft_shft.Enabled = false;
                        ErlD.Enabled = false;
                        LtD.Enabled = false;
                    }
                    if (objData.ExtraHrsBeforeShiftHrs == "" || objData.EarlyGoing == "00:00")
                    {
                        Bef_Shft.Text = "";

                    }
                    else
                    {
                        Bef_Shft.Text = objData.ExtraHrsBeforeShiftHrs;
                    }
                    if (objData.ExtraHrsAfterShiftHrs == null || objData.EarlyGoing == "00:00")
                    {
                        Aft_shft.Text = "";
                    }
                    else
                    {
                        Aft_shft.Text = objData.ExtraHrsAfterShiftHrs;
                    }
                    Comp_Sts.Text = objData.CompensatoryCode;
                    if (objData.CompensatoryCode != "")
                    {
                        string Str = objData.CompensatoryCode;
                        if (Str.Contains("W1"))
                        {
                            ChkW1.Checked = true;
                        }
                        if (Str.Contains("W2"))
                        {
                            ChkW2.Checked = true;
                        }
                        if (Str.Contains("HO"))
                        {
                            ChkHO.Checked = true;
                        }
                        if (Str.Contains("WD"))
                        {
                            ChkWD.Checked = true;
                        }
                    }

                    if (objData.CatDeductedFromExtHrsEarlyGng == true)
                    {
                        ErlD.Checked = true;
                    }
                    else
                    {
                        ErlD.Checked = false;

                    }
                    if (objData.CatDeductedFromExtHrsLateCmg == true)
                    {
                        LtD.Checked = true;
                    }
                    else
                    {
                        LtD.Checked = false;

                    }
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string Bef_sh = "";
                string Aftr_Sh = "";
                string Erl = "";
                string Lt = "";

                if (ChkAllowed.Checked == true)
                {
                    Bef_sh = Bef_Shft.Text;
                    Aftr_Sh = Aft_shft.Text;

                }
                else
                {
                    Bef_sh = null;
                    Aftr_Sh = null;

                }

                if (Erl_Alwd.Text == "")
                {
                    Erl = null;
                }
                else
                {
                    Erl = Erl_Alwd.Text;
                }

                if (Lt_Alwd.Text == "")
                {
                    Lt = null;
                }
                else
                {

                    Lt = Lt_Alwd.Text;
                }

                clsCategory objData = new clsCategory();
                objData.CatID = ddlCategory.SelectedValue;
                objData.EarlyGoing = Erl;
                objData.LateComing = Lt;
                objData.CatExtraCheck = ChkAllowed.Checked == true ? true : false;
                objData.ExtraHrsBeforeShiftHrs = Bef_sh;
                objData.ExtraHrsAfterShiftHrs = Aftr_Sh;
                objData.CompensatoryCode = Comp_Sts.Text.Trim();
                objData.CatDeductedFromExtHrsEarlyGng = ErlD.Checked == true ? true : false;
                objData.CatDeductedFromExtHrsLateCmg = LtD.Checked == true ? true : false;
                objData.CreatedBy = Session["uid"].ToString();
                if (ViewState["PageMode"].ToString() != "Modify")
                {

                    clsCategoryMasterViewHandler.UpdateUserDetails(objData, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Length >= 1)
                    {
                        lblMessageAdd.Text = strErrMsg;
                        lblMessageAdd.Visible = true;
                        return;
                    }
                    else
                    {
                        lblMessageAdd.Text = strSuccMsg;
                        ddlCategory.SelectedIndex = 0;
                        Erl_Alwd.Text = "";
                        Lt_Alwd.Text = "";
                        Bef_Shft.Text = "";
                        Aft_shft.Text = "";
                        Comp_Sts.Text = "";
                        ChkAllowed.Checked = false;
                        ChkW1.Checked = false;
                        ChkW2.Checked = false;
                        ChkHO.Checked = false;
                        ChkWD.Checked = false;
                        ErlD.Checked = false;
                        LtD.Checked = false;
                        ScriptManager.RegisterClientScriptBlock(updatepanel4, updatepanel4.GetType(), "Script", "validateChosen();", true);
                        mpeAddCategory.Show();
                    }
                }
                else
                {
                    clsCategoryMasterViewHandler.UpdateUserDetails(objData, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblMessages.Text = strErrMsg;

                    }
                    else
                    {
                        ddlCategory.SelectedIndex = 0;
                        Erl_Alwd.Text = "";
                        Lt_Alwd.Text = "";
                        Bef_Shft.Text = "";
                        Aft_shft.Text = "";
                        Comp_Sts.Text = "";
                        ChkAllowed.Checked = false;
                        ChkW1.Checked = false;
                        ChkW2.Checked = false;
                        ChkHO.Checked = false;
                        ChkWD.Checked = false;
                        ErlD.Checked = false;
                        LtD.Checked = false;
                        mpeAddCategory.Hide();
                        lblMessages.Text = strSuccMsg;

                    }

                }


                bindDataGrid();

            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit3")
                {
                    lblMessageAdd.Text = "";
                    FillddlCategoryEdit();
                    string CategoryID = e.CommandArgument.ToString();
                    Modify_Data(CategoryID);
                    ViewState["PageMode"] = "Modify";
                    mpeAddCategory.Show();

                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsCategoryMasterViewHandler.GetCategoryDetails("All");

                if (textCategory.Text.ToString() == "")
                {
                    gvCategory.DataSource = dt;
                    gvCategory.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"CAT_CATEGORY_ID~" +textCategory.Text.Trim(), "S" }
                //{"LevelID-" +txtDescription.Text.Trim(), "I" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvCategory.DataSource = _tempDT;
                    gvCategory.DataBind();
                }

                DropDownList ddl = (DropDownList)gvCategory.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCategory.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCategory.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCategory.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCategory.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCategory.PageCount == 0)
                {
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCategory.PageIndex + 1 == gvCategory.PageCount)
                {
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCategory.PageIndex == 0)
                {
                    ((Button)gvCategory.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvCategory.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCategory.PageSize * gvCategory.PageIndex) + 1) + " to " + (((gvCategory.PageSize * (gvCategory.PageIndex + 1)) - 10) + gvCategory.Rows.Count);
                gvCategory.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpeAddCategory.Hide();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                StringBuilder strXML = new StringBuilder();
                bool check = false;
                int cnt = 0;
                strXML.Append("<ent_category>");
                for (int i = 0; i < gvCategory.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvCategory.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        cnt++;
                        check = true;
                        string categoryRowID = gvCategory.Rows[i].Cells[2].Text;
                        strXML.Append("<Category>");
                        strXML.Append("<RowID>" + categoryRowID + "</RowID>");
                        strXML.Append("</Category>");
                    }
                }
                strXML.Append("</ent_category>");
                if (check == false)
                {
                    lblMessages.Text = "Please select record to delete.";
                    lblMessages.Visible = true;
                }
                if (cnt > 0)
                {
                    clsCategory objData = new clsCategory();
                    objData.CreatedBy = Session["uid"].ToString();
                    clsCategoryMasterViewHandler.UpdateUserDetails(objData, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Length >= 1)
                    {
                        lblMessages.Text = strErrMsg;
                        lblMessages.Visible = true;
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg;
                        lblMessages.Visible = true;
                        bindDataGrid();
                    }
                }


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("DeleteRows");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (string.Equals(hdnFlag.Value, "A", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChkDelete.Enabled = true;
                    // lnkEdit.Enabled = true;
                }
                else
                {
                    ChkDelete.Enabled = false;
                    //lnkEdit.Enabled = false;
                }
            }
        }

    }
}
