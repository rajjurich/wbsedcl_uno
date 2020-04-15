using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using CMS.UNO.Tempus.Handler;





namespace UNO
{
    public partial class LeaveRuleNew : System.Web.UI.Page
    {
     
        static string strSuccMsg, strErrMsg = string.Empty;
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        string userid;
        string Rowid1 = "";
        string LeaveAvaibleOnEdit = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }


            if (ViewState["Row"] != null)
            {
                Rowid1 = ViewState["Row"].ToString();
            }
            else
            {
                Rowid1 = "0";
            }


            if (ViewState["LeaveAvaibleOnEdit"] != null)
            {
                LeaveAvaibleOnEdit = ViewState["LeaveAvaibleOnEdit"].ToString();
            }


            if (Page.IsPostBack == false)
            {
                FillddlCategory();
                FillLeaveCodeCombo();
                bindDataGrid();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvLeaveData.ClientID + "');");

            }

        }
        private void FillddlCategory()
        {

            try
            {


                DataTable dt = clsCommonHandler.GetCommonTableDetails("CAT").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlCategory.DataValueField = "ID";
                    ddlCategory.DataTextField = "Value";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "Select One");

                }


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        public void FillLeaveCodeCombo()
        {


            try
            {
                DataTable dt = clsLeaveRuleNewHandler.GetLeaveCode();

                ddlLeaveCode.DataSource = dt;
                ddlLeaveCode.DataValueField = "Leave_ID";
                ddlLeaveCode.DataTextField = "Leave_Description";
                ddlLeaveCode.DataBind();
                ddlLeaveCode.Items.Insert(0, "Select One");
                ddlLeaveCode.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        void bindDataGrid()
        {
            try
            {


                DataTable dt = clsLeaveRuleNewHandler.GetAllDetails();
                gvLeaveData.DataSource = dt;
                gvLeaveData.DataBind();
                DropDownList ddl = (DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeaveData.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeaveData.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeaveData.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeaveData.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeaveData.PageCount == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex + 1 == gvLeaveData.PageCount)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeaveData.PageIndex == 0)
                {
                    ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvLeaveData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeaveData.PageSize * gvLeaveData.PageIndex) + 1) + " to " + (gvLeaveData.PageSize * (gvLeaveData.PageIndex + 1));

                gvLeaveData.BottomPagerRow.Visible = true;


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
        private DataTable Get_PROC_GET_LEAVE_RULE_DETAILS_BYROWID(string rowid)
        {
            DataTable dt = clsLeaveRuleNewHandler.Get_PROC_GET_LEAVE_RULE_DETAILS_BYROWID(rowid);
            return dt;
        }
        protected void gvLeaveData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = e.NewPageIndex;
                bindDataGrid();

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
                gvLeaveData.PageIndex = Convert.ToInt32(((DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = gvLeaveData.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvLeaveData.PageIndex = gvLeaveData.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void gvLeaveData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Modify")
                {
                    string Rowid = e.CommandArgument.ToString();
                    ddlEditRule.Visible = false;
                    rblEditValue.Visible = false;

                    ViewState["Row"] = Rowid;

                    DataTable dt = null;

                    dt = Get_PROC_GET_LEAVE_RULE_DETAILS_BYROWID(Rowid);


                    txtEditLRCODE.Text = dt.Rows[0]["LR_CODE"].ToString();

                    txtEditCategory.Text = dt.Rows[0]["LR_CATEGORYID"].ToString();

                    txtEditLeaveCode.Text = dt.Rows[0]["LeaveID"].ToString();

                    txtEditAccumulation.Text = dt.Rows[0]["LR_ACCUMULATION"].ToString();


                    txtEditLeaveAllotmentAmount.Text = dt.Rows[0]["LR_ALLOTMENT"].ToString();

                    rdlEditDays.SelectedValue = dt.Rows[0]["LR_DAYS"].ToString();

                    txtEditMinDaysAllow.Text = dt.Rows[0]["LR_MinDaysAllowed"].ToString();

                    txtEditMaxDaysAllow.Text = dt.Rows[0]["LR_MaxDaysAllowed"].ToString();

                    ddlEditAllotmentType.SelectedValue = dt.Rows[0]["LR_AllotmentType"].ToString();

                    if (dt.Rows[0]["LR_AllotmentType"].ToString() == "Y")
                    {
                        rblDdlEditAllotmentType.Visible = true;
                        rblDdlEditAllotmentType.SelectedValue = dt.Rows[0]["LR_AllotmentType_YE_PR"].ToString();
                    }

                    if (dt.Rows[0]["LR_DAYS"].ToString() == "R")
                    {
                        ddlEditRule.Visible = true;
                        ddlEditRule.SelectedValue = dt.Rows[0]["LEAVE_RULE"].ToString();
                        if (dt.Rows[0]["LEAVE_RULE"].ToString() == "O")
                        {
                            rblEditValue.Visible = true;
                            rblEditValue.SelectedValue = dt.Rows[0]["LR_GreaterOrLesser"].ToString();
                        }
                    }

                    mpeModifyZone.Show();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResellAll_NewEntry();
            lblMessages.Visible = false;
            lblMessages.Text = "";


        }
        protected void btnSubmitNewEntry_Onclick(object sender, EventArgs e)
        {

            string lvRuleID = "0";

            if (SaveLeave(lvRuleID))
            {
                mpeAddNewEntry.Hide();

                bindDataGrid();

                ResellAll_NewEntry();
            }
            else
            {
                return;
            }


        }
        protected void btnModifySaveLeave_Click(object sender, EventArgs e)
        {

            SaveLeave(Rowid1);

            this.bindDataGrid();

            mpeModifyZone.Hide();

        }
        private bool SaveLeave(string lvRuleID)
        {

            clsLeaveRuleNew objData = new clsLeaveRuleNew();
            if (lvRuleID == "0")
            {
                objData.lrrecid = "0";
                objData.lrCode = txtLRCODE.Text;
                objData.lrAllotment = txtLeaveAllotment.Text;
                objData.lrAccumulation = txtLeaveAccumulation.Text;
                objData.lrCategory = ddlCategory.SelectedValue.ToString();
                objData.lrLeaveid = ddlLeaveCode.SelectedValue.ToString();
                objData.strDays = rdbDays.SelectedValue;
                objData.strLeaveRule = ddlDays.SelectedValue;
                objData.Value = rblValue.SelectedValue;
                objData.strMinDays = txtMinDaysAllow.Text.Trim();
                objData.strMaxDays = txtMaxDaysAllowed.Text.Trim();
                objData.strAllotmentType = ddlAllotmentType.SelectedValue;
                objData.strAllotmentType_YE_PR = rblDdlAllotmentType.SelectedValue;

            }
            else
            {
                objData.lrrecid = lvRuleID;
                objData.lrCode = txtEditLRCODE.Text;
                objData.lrLeaveid = txtEditLeaveCode.Text;
                objData.lrAllotment = txtEditLeaveAllotmentAmount.Text;
                objData.lrAccumulation = txtEditAccumulation.Text;
                objData.lrCategory = txtEditCategory.Text;
                objData.strDays = rdlEditDays.SelectedValue;
                objData.strLeaveRule = ddlEditRule.SelectedValue;
                objData.Value = rblEditValue.SelectedValue;
                objData.strMinDays = txtEditMinDaysAllow.Text.Trim();
                objData.strMaxDays = txtEditMaxDaysAllow.Text.Trim();
                objData.strAllotmentType = ddlEditAllotmentType.SelectedValue;
                objData.strAllotmentType_YE_PR = rblDdlEditAllotmentType.SelectedValue;

            }

            try
            {
                objData.CreatedBy = Session["uid"].ToString();
                clsLeaveRuleNewHandler.UpdateLeaveDetails(objData, "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                if (strErrMsg.Length >= 1)
                {
                    lblErrorSingleEntry.Visible = true;
                    lblErrorSingleEntry.Text = strErrMsg;
                }
                else
                {
                    lblMessages.Visible = true;
                    lblMessages.Text = strSuccMsg;

                }
                return true;



            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                return false;
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool Check = false;
            bool marked = false;           
            lblMessages.Text = "";
            StringBuilder strXML = new StringBuilder();
            strXML.Append("<TA_LEAVE_RULE_NEW>");
            for (int i = 0; i < gvLeaveData.Rows.Count; i++)
            {
                try
                {
                    CheckBox delrows = (CheckBox)gvLeaveData.Rows[i].FindControl("DeleteRows");
                    Label lblLeaveID = (Label)gvLeaveData.Rows[i].FindControl("lblLeaveID");

                    if (delrows.Checked == true)
                    {
                        if (marked == false)
                        {
                            marked = true;
                        }

                       
                        Label lblRowId = (Label)gvLeaveData.Rows[i].FindControl("lblSrNo");                      
                        strXML.Append("<Leave>");
                        strXML.Append("<LR_REC_ID>" + lblRowId.Text + "</LR_REC_ID>");
                        strXML.Append("</Leave>");
                        Check = true;                        

                    }

                }



                catch (Exception ex)
                {
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                }

            }
            strXML.Append("</TA_LEAVE_RULE_NEW>");
            if (Check == true)
            {
                clsLeaveRuleNew objData=new clsLeaveRuleNew();
                objData.CreatedBy=Session["uid"].ToString();
                clsLeaveRuleNewHandler.DeleteLeave(objData, strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
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
            else if (marked == false)
            {
                lblMessages.Text = "Please select record to Delete";
                lblMessages.Visible = true;
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsLeaveRuleNewHandler.GetAllDetails();

                if (txtSearchLeaveCode.Text.ToString() == "" && txtSearchCategoryID.Text.ToString() == "")
                {
                    bindDataGrid();
                }
                else
                {
                    String[,] values = { 
                {"LR_CATEGORYID~" +txtSearchCategoryID.Text.Trim(), "S" },
                {"LR_CODE~" +txtSearchLeaveCode.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvLeaveData.DataSource = _tempDT;
                    gvLeaveData.DataBind();
                    _tempDT = _sc.searchTable(values, dt);
                    gvLeaveData.DataSource = _tempDT;
                    gvLeaveData.DataBind();

                    DropDownList ddl = (DropDownList)gvLeaveData.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvLeaveData.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvLeaveData.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvLeaveData.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvLeaveData.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvLeaveData.PageCount == 0)
                    {
                        ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvLeaveData.PageIndex + 1 == gvLeaveData.PageCount)
                    {
                        ((Button)gvLeaveData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvLeaveData.PageIndex == 0)
                    {
                        ((Button)gvLeaveData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvLeaveData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeaveData.PageSize * gvLeaveData.PageIndex) + 1) + " to " + (gvLeaveData.PageSize * (gvLeaveData.PageIndex + 1));

                    gvLeaveData.BottomPagerRow.Visible = true;


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
        private void ResellAll_NewEntry()
        {

            ddlLeaveCode.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            lblMessages.Text = "";
            txtLeaveAllotment.Text = string.Empty;
            txtLeaveAccumulation.Text = string.Empty;
            txtLRCODE.Text = string.Empty;
            lblErrorSingleEntry.Visible = false;
            lblErrorSingleEntry.Text = string.Empty;
            rdbDays.SelectedValue = "W";
            ddlDays.SelectedValue = "N";
            ddlDays.Visible = false;
            rblValue.Visible = false;
            txtMinDaysAllow.Text = "";
            ddlAllotmentType.SelectedValue = "0";

        }
        protected void rdbDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDays = rdbDays.SelectedValue;
            if (strDays == "R")
            {
                ddlDays.Visible = true;
            }
            else
            {
                ddlDays.Visible = false;
                rblValue.Visible = false;
                ddlDays.SelectedValue = "N";
            }
        }
        protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectedRule = ddlDays.SelectedValue;
            if (strSelectedRule == "O")
            {
                rblValue.Visible = true;
            }
            else
            {
                rblValue.Visible = false;
            }
        }
        protected void rdlEditDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDays = rdlEditDays.SelectedValue;
            if (strDays == "R")
            {
                ddlEditRule.Visible = true;
            }
            else
            {
                ddlEditRule.Visible = false;
                rblEditValue.Visible = false;
                ddlEditRule.SelectedValue = "N";
            }
        }
        protected void ddlEditRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectedRule = ddlEditRule.SelectedValue;
            if (strSelectedRule == "O")
            {
                rblEditValue.Visible = true;
            }
            else
            {
                rblEditValue.Visible = false;
            }
        }
        protected void ddlEditAllotmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEditAllotmentType.SelectedValue == "Y")
            {
                rblDdlEditAllotmentType.Visible = true;
            }
            else
            {
                rblDdlEditAllotmentType.Visible = false;
            }
        }
        protected void ddlAllotmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAllotmentType.SelectedValue == "Y")
            {
                rblDdlAllotmentType.Visible = true;
            }
            else
            {
                rblDdlAllotmentType.Visible = false;
            }
        }
        protected void gvLeaveData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("DeleteRows");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (string.Equals(hdnFlag.Value, "A", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChkDelete.Enabled = true;
                }
                else
                {
                    ChkDelete.Enabled = false;
                }
            }
        }

    }

}