using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using CMS.UNO.Core.Handler;
using System.Text;

namespace UNO
{
    public partial class ReasonFileView : System.Web.UI.Page
    {      
        static string strSuccMsg, strErrMsg = string.Empty;      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                FillddlReasonType(cmbReason_Type);
                cmbReason_Type.Items.Insert(0, new ListItem("Select One", "0"));
                FillddlReasonType(cmbModifyReason_Type);
                cmbModifyReason_Type.Items.Insert(0, new ListItem("Select One", "0"));
                bindDataGrid();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvReason.ClientID + "');");
            }
        }
        private void FillddlReasonType(DropDownList ddlreasonType)
        {

            try
            {
                DataTable dt = clsCommonHandler.GetEntParameterValues("REASONTYPE", "ENT");
                if (dt.Rows.Count > 0)
                {
                    ddlreasonType.DataValueField = "Code";
                    ddlreasonType.DataTextField = "value";
                    ddlreasonType.DataSource = dt;
                    ddlreasonType.DataBind();

                }


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
                clsReasonView objReason = new clsReasonView();
                objReason.RecID = "";
                DataTable dt = clsReasonViewHandler.GetAllReason("AllReason", ref objReason);

                gvReason.DataSource = dt;
                gvReason.DataBind();


                if (dt.Rows.Count != 0)
                {
                    DropDownList ddl = (DropDownList)gvReason.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvReason.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvReason.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvReason.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvReason.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvReason.PageCount == 0)
                    {
                        ((Button)gvReason.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvReason.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvReason.PageIndex + 1 == gvReason.PageCount)
                    {
                        ((Button)gvReason.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvReason.PageIndex == 0)
                    {
                        ((Button)gvReason.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvReason.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvReason.PageSize * gvReason.PageIndex) + 1) + " to " + (((gvReason.PageSize * (gvReason.PageIndex + 1)) - 10) + gvReason.Rows.Count);

                    gvReason.BottomPagerRow.Visible = true;
                }

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
        protected void gvReason_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvReason.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReasonMasterView");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvReason.PageIndex = Convert.ToInt32(((DropDownList)gvReason.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReasonMasterView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvReason.PageIndex = gvReason.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReasonMasterView");
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvReason.PageIndex = gvReason.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReasonMasterView");
            }

        }
        protected void gvReason_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit3")
            {
                txtModifyReasonDesc.Focus();
                string Rowid = e.CommandArgument.ToString();

                fillModifyReason(Rowid);

                txtModifyReasonId.ReadOnly = true;
                mpEditReason.Show();
            }
        }       
        public void fillModifyReason(string Reasonid)
        {
            try
            {
                clsReasonView objReason = new clsReasonView();
                objReason.RecID = Reasonid;
                clsReasonViewHandler.GetAllReason("AllReason", ref objReason);
                if (objReason != null)
                {
                    cmbModifyReason_Type.SelectedValue = objReason.ReasonType;
                    txtModifyReasonId.Text = objReason.ReasonID;
                    txtModifyReasonDesc.Text = objReason.ReasonDescr;
                }
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace,clsCommonHandler.PageName());
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            clearAdd();
            cmbReason_Type.Focus();
            mpeAddreason.Show();
            ScriptManager manager = ScriptManager.GetCurrent(this);
            manager.SetFocus("cmbReason_Type");
        }
        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddreason.Hide();
            clearAdd();
        }
        private void clearAdd()
        {
            txtReason_ID.Text = "";
            txtReason_Description.Text = "";
            cmbReason_Type.SelectedIndex = 0;
            lblMessages.Text = "";
            lblAddmsg.Text = string.Empty;
        }
        private void clearEdit()
        {
            lblMessages.Text = "";

        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsReasonView objReason = new clsReasonView();
                objReason.ReasonID = txtReason_ID.Text.Trim();
                objReason.ReasonDescr = txtReason_Description.Text.Trim();
                objReason.ReasonType = cmbReason_Type.SelectedValue.Trim();
                objReason.CreatedBy = Session["uid"].ToString();
                clsReasonViewHandler.InsertReasonDetails(objReason, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Length >= 1)
                {
                    lblAddmsg.Visible = true;
                    lblAddmsg.Text = strErrMsg;
                    mpeAddreason.Show();
                    ScriptManager.RegisterClientScriptBlock(updatepanel3, updatepanel3.GetType(), "Script", "validateChosen();", true);
                    return;
                }
                else
                {
                    lblAddmsg.Text = strSuccMsg;
                    txtReason_ID.Text = "";
                    txtReason_Description.Text = "";
                    cmbReason_Type.SelectedValue = "0";
                    lblAddmsg.Visible = true;
                    cmbReason_Type.Focus();
                    bindDataGrid();
                    mpeAddreason.Show();
                    ScriptManager.RegisterClientScriptBlock(updatepanel3, updatepanel3.GetType(), "Script", "validateChosen();", true);
                }

            }


            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnModifySave_Click(object sender, EventArgs e)
        {
            try
            {
                clsReasonView objReason = new clsReasonView();
                objReason.ReasonID = txtModifyReasonId.Text.Trim();
                objReason.ReasonDescr = txtModifyReasonDesc.Text.Trim();
                objReason.ReasonType = cmbModifyReason_Type.SelectedValue.Trim();
                objReason.CreatedBy = Session["uid"].ToString();
                clsReasonViewHandler.InsertReasonDetails(objReason, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Length >= 1)
                {
                    lblEditMsg.Text = strErrMsg;
                    lblEditMsg.Visible = true;
                    mpEditReason.Show();
                    return;
                }
                else
                {
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    txtModifyReasonDesc.Text = "";
                    txtModifyReasonId.Text = "";
                    cmbModifyReason_Type.SelectedValue = "0";
                    bindDataGrid();
                    mpEditReason.Hide();
                }

            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());

            }
        }
        protected void btnModifyCancel_Click(object sender, EventArgs e)
        {
            mpEditReason.Hide();
            clearEdit();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<ent_reason>");

                int cnt = 0;
                for (int i = 0; i < gvReason.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvReason.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        cnt++;
                        strXML.Append("<Reason>");
                        strXML.Append("<reason_id>" + gvReason.Rows[i].Cells[2].Text + "</reason_id>");
                        lblMessages.Text = "Record(s) Deleted Successfully.";
                        lblMessages.Visible = true;
                        strXML.Append("</Reason>");
                    }
                }
                strXML.Append("</ent_reason>");
                if (cnt >= 1)
                {
                    clsReasonView objReason = new clsReasonView();
                    objReason.CreatedBy = Session["uid"].ToString();
                    clsReasonViewHandler.InsertReasonDetails(objReason, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Length >= 1)
                    {
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg;
                        lblMessages.Visible = true;
                    }
                }
                bindDataGrid();
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
                clsReasonView objReason = new clsReasonView();
                objReason.RecID = "";
                DataTable dt = clsReasonViewHandler.GetAllReason("AllReason", ref objReason);
                if (textreasonid.Text.ToString() == "" && textreasonname.Text.ToString() == "")
                {
                    gvReason.DataSource = dt;
                    gvReason.DataBind();

                }
                else
                {
                    String[,] values = { 
                {"Reason_ID~" +textreasonid.Text.Trim(), "S" },
                {"Reason_Description~" +textreasonname.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvReason.DataSource = _tempDT;
                    gvReason.DataBind();

                }
                DropDownList ddl = (DropDownList)gvReason.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvReason.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvReason.PageIndex + 1).ToString();
                Label lblcount = (Label)gvReason.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvReason.DataSource).Rows.Count.ToString() + " Records.";
                if (gvReason.PageCount == 0)
                {
                    ((Button)gvReason.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvReason.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvReason.PageIndex + 1 == gvReason.PageCount)
                {
                    ((Button)gvReason.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvReason.PageIndex == 0)
                {
                    ((Button)gvReason.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvReason.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvReason.PageSize * gvReason.PageIndex) + 1) + " to " + (((gvReason.PageSize * (gvReason.PageIndex + 1)) - 10) + gvReason.Rows.Count);

                gvReason.BottomPagerRow.Visible = true;
            }


            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}