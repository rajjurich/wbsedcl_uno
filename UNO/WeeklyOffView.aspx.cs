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
using CMS.UNO.Tempus.Handler;
using System.Text;

namespace UNO
{
    public partial class WeeklyOffView : System.Web.UI.Page
    {
       
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                bindDataGrid();
                rdoUserDefSelction.Style.Add("display", "none");
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvReason.ClientID + "');");
                btnSubmitAdd.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtWeekOffCode.ClientID + "','" + lblAddError.ClientID + "','" + rdoSelectionWay.ClientID + "','" + rdoUserDefSelction.ClientID + "');");
                btnModifySave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtWeekOffCodeEdit.ClientID + "','" + lblEditError.ClientID + "','" + rdoSelectionWayEdit.ClientID + "','" + rdoUserDefSelctionEdit.ClientID + "');");
            }
        }

        void bindDataGrid()
        {
            try
            {

                DataTable dt = clsWeeklyOffHandler.GetWeekendWeekoffDetails("All");

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

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void gvReason_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit3")
            {
                rdoUserDefSelctionEdit.Style.Add("display", "none");
                txtWeekOffCodeEdit.Style.Add("ReadOnly", "true");
                string Rowid = e.CommandArgument.ToString();
                fillModifyReason(Rowid);
                mpEditReason.Show();
                lblEditError.Text = "";
            }
        }

        public void fillModifyReason(string Code)
        {
            try
            {

                clsWeekendWeekOff objdata = clsWeeklyOffHandler.GetWeekendWeekoffDetails("FillEditData", Code);

                if (objdata != null)
                {
                    txtWeekOffCodeEdit.Text = objdata.Code;

                    if (objdata.WeekOff.ToString() == "0")
                    {
                        rdoWOOrWENDEdit.SelectedValue = "1";
                    }
                    else
                    {
                        rdoWOOrWENDEdit.SelectedValue = "0";
                    }

                    ddlDayEdit.SelectedValue = objdata.Day.ToString();

                    string[] strWOSequence = null;

                    strWOSequence = objdata.Pattern.ToString().Split(',');

                    if (objdata.Pattern.ToString() == "1,2,3,4,5")
                    {
                        rdoSelectionWayEdit.SelectedIndex = 0;
                    }
                    else if (objdata.Pattern.ToString() == "2,4")
                    {
                        rdoSelectionWayEdit.SelectedIndex = 1;
                    }
                    else if (objdata.Pattern.ToString() == "1,3,5")
                    {
                        rdoSelectionWayEdit.SelectedIndex = 2;
                    }
                    else
                    {
                        rdoUserDefSelctionEdit.Style.Add("display", "inline");

                        foreach (ListItem lst in rdoUserDefSelctionEdit.Items)
                        {
                            lst.Selected = false;
                        }

                        foreach (string str in strWOSequence)
                        {
                            rdoUserDefSelctionEdit.Items[Convert.ToInt32(str) - 1].Selected = true;
                        }
                        rdoSelectionWayEdit.SelectedIndex = 3;

                    }

                }
                else
                {
                    lblMessages.Visible = true;
                    lblMessages.Text = "Records not found";
                }


            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblAddError.Text = "";
            mpeAddreason.Show();
            clearAdd();
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddreason.Hide();
            clearAdd();
        }

        private void clearAdd()
        {
            rdoUserDefSelction.SelectedIndex = -1;
            txtWeekOffCode.Text = "";
            rdoWOOrWEND.SelectedIndex = 0;
            rdoSelectionWay.SelectedIndex = 0;
            lblMessages.Text = "";
            lblMessages.Visible = false;
        }

        private void clearEdit()
        {
            lblMessages.Text = "";
            lblMessages.Visible = false;

        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsWeekendWeekOff objData = new clsWeekendWeekOff();

                string strWeekList = string.Empty;

                if (rdoSelectionWay.SelectedIndex == 0)
                {
                    strWeekList = "1,2,3,4,5";
                }
                else if (rdoSelectionWay.SelectedIndex == 1)
                {
                    strWeekList = "2,4";
                }

                else if (rdoSelectionWay.SelectedIndex == 2)
                {
                    strWeekList = "1,3,5";
                }
                else if (rdoSelectionWay.SelectedIndex == 3)
                {
                    foreach (ListItem item in rdoUserDefSelction.Items)
                    {
                        if (item.Selected)
                        {
                            strWeekList = strWeekList + (strWeekList.Length > 0 ? "," : "") + item.Value.ToString();
                        }
                    }
                }

                objData.Code = txtWeekOffCode.Text.Trim();
                objData.Day = Convert.ToInt16(ddlDay.SelectedValue.ToString());
                objData.WeekOff = rdoWOOrWEND.SelectedIndex == 0 ? 1 : 0;
                objData.Pattern = strWeekList;
                objData.CreatedBy = Session["uid"].ToString();
                clsWeeklyOffHandler.UpdateWeekOffDetails(objData, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblAddError.Text = strErrMsg;
                    mpeAddreason.Show();
                    if (rdoSelectionWay.SelectedIndex == 3)
                    {
                        rdoUserDefSelction.Style.Add("display", "inline");
                    }
                    return;
                }
                else
                {
                    lblAddError.Text = strSuccMsg;
                    bindDataGrid();
                    lblMessages.Visible = true;
                    mpeAddreason.Show();
                    clearAdd();
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
                clsWeekendWeekOff objData = new clsWeekendWeekOff();
                string strWeekList = string.Empty;

                if (rdoSelectionWayEdit.SelectedIndex == 0)
                {
                    strWeekList = "1,2,3,4,5";
                }
                else if (rdoSelectionWayEdit.SelectedIndex == 1)
                {
                    strWeekList = "2,4";
                }

                else if (rdoSelectionWayEdit.SelectedIndex == 2)
                {
                    strWeekList = "1,3,5";
                }
                else if (rdoSelectionWayEdit.SelectedIndex == 3)
                {
                    foreach (ListItem item in rdoUserDefSelctionEdit.Items)
                    {
                        if (item.Selected)
                        {
                            strWeekList = strWeekList + (strWeekList.Length > 0 ? "," : "") + item.Value.ToString();
                        }
                    }
                }


                objData.Code = txtWeekOffCodeEdit.Text.Trim();
                objData.Day = Convert.ToInt16(ddlDayEdit.SelectedValue.ToString());
                objData.WeekOff = rdoWOOrWENDEdit.SelectedIndex == 0 ? 1 : 0;
                objData.Pattern = strWeekList;
                objData.CreatedBy = Session["uid"].ToString();
                clsWeeklyOffHandler.UpdateWeekOffDetails(objData, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblEditError.Text = strErrMsg;
                    mpeAddreason.Show();
                    return;
                }
                else
                {
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
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
                int count = 0;
                strXML.Append("<TA_WKLYOFF>");
                for (int i = 0; i < gvReason.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvReason.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        count++;
                        strXML.Append("<WeekOff>");
                        strXML.Append("<MWK_CD>" + gvReason.Rows[i].Cells[2].Text + "</MWK_CD>");
                        strXML.Append("</WeekOff>");                       
                    }
                }
                strXML.Append("</TA_WKLYOFF>");
               
                if (count > 0)
                {
                    clsWeekendWeekOff objData = new clsWeekendWeekOff();
                    objData.CreatedBy = Session["uid"].ToString();
                    clsWeeklyOffHandler.UpdateWeekOffDetails(objData, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length>= 1)
                    {
                        lblMessages.Text = strErrMsg;
                        lblMessages.Visible = true;
                        return;
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg;
                        lblMessages.Visible = true;
                        bindDataGrid();
                    }
                }
                else
                {
                    lblMessages.Text = "Please select record to delete.";
                    lblMessages.Visible = true;
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
                lblMessages.Text = "";
                DataTable dt = clsWeeklyOffHandler.GetWeekendWeekoffDetails("All");
                if (textreasonid.Text.ToString() == "" && textreasonname.Text.ToString() == "")
                {
                    gvReason.DataSource = dt;
                    gvReason.DataBind();

                }
                else
                {
                    String[,] values = { 
                {"MWK_CD~" +textreasonid.Text.Trim(), "S" },
                {"MWK_CD~" +textreasonname.Text.Trim(), "S" }			
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

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}