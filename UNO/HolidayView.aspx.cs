using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text;
using CMS.UNO.Core.Handler;
namespace UNO
{
    public partial class HolidayView : System.Web.UI.Page
    {
      
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                ddlHolidayType();
                FillLocation();
                calHolidayDateAdd.StartDate = DateTime.Now.Date;
                calSwapDateAdd.StartDate = DateTime.Now.Date;
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvHoliday.ClientID + "');");
                btnSubmitAdd.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtHolidayIDAdd.ClientID + "','" + txtDescriptionAdd.ClientID + "','" + ddlHolidayTypeAdd.ClientID + "','" + txtHolidayDateAdd.ClientID + "','" + txtSwapDateAdd.ClientID + "','" + chkSelectLocation.ClientID + "','" + gvHolidayLocationsAdd.ClientID + "','" + lblErrorAdd.ClientID + "');");
                btnSubmitEdit.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtHolidayIDEdit.ClientID + "','" + txtDescriptionEdit.ClientID + "','" + ddlHolidayTypeEdit.ClientID + "','" + txtHolidayDateEdit.ClientID + "','" + txtSwapDateEdit.ClientID + "','" + chkSelectLocationEdit.ClientID + "','" + gvHolidayLocationsEdit.ClientID + "','" + lblErrorEdit.ClientID + "');");
                ddlHolidayTypeAdd.Attributes.Add("onchange", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeAdd.ClientID + "','" + chkSelectLocation.ClientID + "','" + gvHolidayLocationsAdd.ClientID + "','A');");
                ddlHolidayTypeEdit.Attributes.Add("onchange", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeEdit.ClientID + "','" + chkSelectLocationEdit.ClientID + "','" + gvHolidayLocationsEdit.ClientID + "','E');");
                ddlHolidayTypeAdd.Attributes.Add("onkeyup", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeAdd.ClientID + "','" + chkSelectLocation.ClientID + "','" + gvHolidayLocationsAdd.ClientID + "','A');");
                ddlHolidayTypeEdit.Attributes.Add("onkeyup", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeEdit.ClientID + "','" + chkSelectLocationEdit.ClientID + "','" + gvHolidayLocationsEdit.ClientID + "','E');");
                ddlHolidayTypeAdd.Attributes.Add("onkeydown", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeAdd.ClientID + "','" + chkSelectLocation.ClientID + "','" + gvHolidayLocationsAdd.ClientID + "','A');");
                ddlHolidayTypeEdit.Attributes.Add("onkeydown", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeEdit.ClientID + "','" + chkSelectLocationEdit.ClientID + "','" + gvHolidayLocationsEdit.ClientID + "','E');");
                chkSelectLocation.Attributes.Add("onchange", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeAdd.ClientID + "','" + chkSelectLocation.ClientID + "','" + gvHolidayLocationsAdd.ClientID + "','A');");
                chkSelectLocationEdit.Attributes.Add("onchange", "javascript:return ddlHolidayTypeOnChange('" + ddlHolidayTypeEdit.ClientID + "','" + chkSelectLocationEdit.ClientID + "','" + gvHolidayLocationsEdit.ClientID + "','E');");
            }

        }
        void bindDataGrid()
        {
            try
            {
                DataTable dt = clsHolidayHandler.GetAllDetails("All");

                gvHoliday.DataSource = dt;
                gvHoliday.DataBind();

                if (dt.Rows.Count != 0)
                {

                    DropDownList ddl = (DropDownList)gvHoliday.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvHoliday.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvHoliday.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvHoliday.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvHoliday.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvHoliday.PageCount == 0)
                    {
                        ((Button)gvHoliday.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvHoliday.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvHoliday.PageIndex + 1 == gvHoliday.PageCount)
                    {
                        ((Button)gvHoliday.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvHoliday.PageIndex == 0)
                    {
                        ((Button)gvHoliday.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvHoliday.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvHoliday.PageSize * gvHoliday.PageIndex) + 1) + " to " + (((gvHoliday.PageSize * (gvHoliday.PageIndex + 1)) - 10) + gvHoliday.Rows.Count);
                    gvHoliday.BottomPagerRow.Visible = true;
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
        private void ddlHolidayType()
        {
            try
            {
                
                DataTable dt = clsCommonHandler.GetEntParameterValues("HOLIDAY", "ENT");
              
                ddlHolidayTypeAdd.DataValueField = "CODE";
                ddlHolidayTypeAdd.DataTextField = "VALUE";
                ddlHolidayTypeAdd.DataSource = dt;
                ddlHolidayTypeAdd.DataBind();
                ddlHolidayTypeAdd.Items.Insert(0, new ListItem("Select One", "0"));

                ddlHolidayTypeEdit.DataValueField = "CODE";
                ddlHolidayTypeEdit.DataTextField = "VALUE";
                ddlHolidayTypeEdit.DataSource = dt;
                ddlHolidayTypeEdit.DataBind();
                ddlHolidayTypeEdit.Items.Insert(0, new ListItem("Select One", "0"));


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private void FillLocation()
        {
            try
            {
                DataTable dt = clsCommonHandler.GetCommonEntitiesValues("LOC");
                if (dt.Rows.Count > 0)
                {
                    gvHolidayLocationsEdit.DataSource = dt;
                    gvHolidayLocationsEdit.DataBind();
                    gvHolidayLocationsAdd.DataSource = dt;
                    gvHolidayLocationsAdd.DataBind();

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
                gvHoliday.PageIndex = Convert.ToInt32(((DropDownList)gvHoliday.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvHoliday.PageIndex = gvHoliday.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvHoliday.PageIndex = gvHoliday.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder strXML = new StringBuilder();
            try
            {
                clsHoliday objHoliday = new clsHoliday();
                objHoliday.CreatedBy = Session["uid"].ToString();
                strXML.Append("<ENT_HOLIDAY>");

                for (int i = 0; i <= gvHoliday.Rows.Count - 1; i++)
                {
                    CheckBox chk = (CheckBox)gvHoliday.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked)
                    {
                        strXML.Append("<HOLIDAY>");
                        strXML.Append("<HOLIDAY_ID>" + gvHoliday.Rows[i].Cells[2].Text + "</HOLIDAY_ID>");
                        strXML.Append("</HOLIDAY>");

                    }
                   
                  
                }
                strXML.Append("</ENT_HOLIDAY>");
                clsHolidayHandler.InsertUpdateHolidayDetails(objHoliday, "Delete", strXML.ToString(), "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
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
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = clsHolidayHandler.GetAllDetails("All");
                if (txtHolidayID.Text.ToString() == "" && txtHolidayName.Text.ToString() == "")
                {
                    gvHoliday.DataSource = dt;
                    gvHoliday.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"HOLIDAY_ID~" +txtHolidayID.Text.Trim(), "S" },
				{"HOLIDAY_DESCRIPTION~" +txtHolidayName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvHoliday.DataSource = _tempDT;
                    gvHoliday.DataBind();
                }

                DropDownList ddl = (DropDownList)gvHoliday.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvHoliday.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvHoliday.PageIndex + 1).ToString();
                Label lblcount = (Label)gvHoliday.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvHoliday.DataSource).Rows.Count.ToString() + " Records.";
                if (gvHoliday.PageCount == 0)
                {
                    ((Button)gvHoliday.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvHoliday.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvHoliday.PageIndex + 1 == gvHoliday.PageCount)
                {
                    ((Button)gvHoliday.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvHoliday.PageIndex == 0)
                {
                    ((Button)gvHoliday.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvHoliday.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvHoliday.PageSize * gvHoliday.PageIndex) + 1) + " to " + (((gvHoliday.PageSize * (gvHoliday.PageIndex + 1)) - 10) + gvHoliday.Rows.Count);
                gvHoliday.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        public StringBuilder HolidayLocation(GridView gvAddHolidayLocations, string HolidayID)
        {

            StringBuilder strXML = new StringBuilder();
            strXML.Append("<ENT_HOLIDAYLOC>");
            for (int i = 0; i < gvAddHolidayLocations.Rows.Count; i++)
            {
                try
                {
                    int IsTrue = 0;
                    CheckBox selrows = (CheckBox)gvAddHolidayLocations.Rows[i].FindControl("DeleteRows");
                    CheckBox optChk = (CheckBox)gvAddHolidayLocations.Rows[i].FindControl("optionalChk");
                    if (optChk.Checked == true)
                    {

                        IsTrue = 1;
                    }
                    if (selrows.Checked == true)
                    {
                        strXML.Append("<Location>");
                        strXML.Append("<HOLIDAY_ID>" + HolidayID + "</HOLIDAY_ID>");
                        strXML.Append("<HOLIDAY_LOC_ID>" + gvAddHolidayLocations.Rows[i].Cells[1].Text + "</HOLIDAY_LOC_ID>");
                        strXML.Append("<IS_OPTIONAL>" + IsTrue + "</IS_OPTIONAL>");
                        strXML.Append("</Location>");
                    }
                }
                catch (Exception ex)
                {

                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                }
            }
            strXML.Append("</ENT_HOLIDAYLOC>");
            return strXML;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mpeAddHolidays.Show();
            txtHolidayIDAdd.Focus();
            lblErrorAdd.Text = "";
            lblMessages.Text = "";
            lblMessages.Visible = true;
            txtHolidayIDAdd.Text = "";
            txtDescriptionAdd.Text = "";
            txtHolidayDateAdd.Text = "";
            txtSwapDateAdd.Text = "";
            ddlHolidayTypeAdd.SelectedValue = "0";
            chkSelectLocation.SelectedIndex = 0;
            divgvHolidayLocationsAdd.Attributes.Add("class", "divHide");
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            lblErrorAdd.Text = "";
            try
            {
                clsHoliday objHoliday = new clsHoliday();
                objHoliday.HOLIDAY_ID = txtHolidayIDAdd.Text.Trim();
                objHoliday.HOLIDAY_DESCRIPTION = txtDescriptionAdd.Text.Trim();
                objHoliday.HOLIDAY_TYPE = ddlHolidayTypeAdd.SelectedValue;
                objHoliday.HOLIDAY_DATE = txtHolidayDateAdd.Text.Trim();
                objHoliday.HOLIDAY_SWAP = txtSwapDateAdd.Text.Trim();
                objHoliday.HOLIDAY_LOCATION = chkSelectLocation.SelectedValue;
                objHoliday.CreatedBy = Session["uid"].ToString();
                clsHolidayHandler.InsertUpdateHolidayDetails(objHoliday, "Insert", HolidayLocation(gvHolidayLocationsAdd, txtHolidayIDAdd.Text.Trim()).ToString(), "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblErrorAdd.Text = strErrMsg;
                    lblErrorAdd.Visible = true;
                    mpeAddHolidays.Show();
                }
                else
                {
                    lblErrorAdd.Text = strSuccMsg;
                    lblErrorAdd.Visible = true;
                    txtHolidayIDAdd.Text = "";
                    txtDescriptionAdd.Text = "";
                    txtHolidayDateAdd.Text = "";
                    txtSwapDateAdd.Text = "";
                    ddlHolidayTypeAdd.SelectedValue = "0";
                    chkSelectLocation.SelectedIndex = 0;
                    bindDataGrid();
                    divgvHolidayLocationsAdd.Attributes.Add("class", "divHide");
                    mpeAddHolidays.Show();
                    txtHolidayIDAdd.Focus();
                }


            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void gvHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblErrorEdit.Text = "";
                if (e.CommandName == "Modify")
                {
                    clsHoliday objHoliday = new clsHoliday();
                    string HolidayID = e.CommandArgument.ToString();
                    DataTable dtLocation = clsHolidayHandler.GetHolidayLocationDetails(HolidayID, "HolidayLoc", ref objHoliday);

                    if (dtLocation.Rows.Count != 0)
                    {
                        chkSelectLocationEdit.SelectedIndex = 1;
                    }
                    else
                    {
                        chkSelectLocationEdit.SelectedIndex = 0;
                    }

                    if (chkSelectLocationEdit.SelectedIndex == 1)
                    {
                        divgvHolidayLocationsEdit.Attributes.Add("class", "divOverflow");
                    }
                    else
                    {
                        divgvHolidayLocationsEdit.Attributes.Add("class", "divHide");
                    }

                    for (int i = 0; i < gvHolidayLocationsEdit.Rows.Count; i++)
                    {
                        CheckBox chkDeletebox = (CheckBox)gvHolidayLocationsEdit.Rows[i].FindControl("DeleteRows");
                        CheckBox EditOptChk = (CheckBox)gvHolidayLocationsEdit.Rows[i].FindControl("optionalChk");
                        string strHolidayLocID = gvHolidayLocationsEdit.Rows[i].Cells[1].Text;
                        string strfilter = " holiday_loc_id= '" + strHolidayLocID + "'";
                        DataRow[] drholiday = dtLocation.Select(strfilter);
                        if (drholiday.Length > 0)
                        {
                            chkDeletebox.Checked = true;
                            if (drholiday[0][2].ToString().ToLower() == "true")
                            {
                                EditOptChk.Checked = true;
                            }
                            else
                                EditOptChk.Checked = false;
                        }
                        else
                        {
                            chkDeletebox.Checked = false;
                        }
                    }


                    txtHolidayIDEdit.Text = objHoliday.HOLIDAY_ID;
                    txtDescriptionEdit.Text = objHoliday.HOLIDAY_DESCRIPTION;
                    ddlHolidayTypeEdit.SelectedValue = objHoliday.HOLIDAY_TYPE;
                    txtHolidayDateEdit.Text = objHoliday.HOLIDAY_DATE;
                    txtSwapDateEdit.Text = objHoliday.HOLIDAY_SWAP;
                    chkSelectLocationEdit.SelectedValue = objHoliday.HOLIDAY_LOCATION;
                    ViewState["OldLocation"] = HolidayLocation(gvHolidayLocationsEdit, txtHolidayIDEdit.Text.Trim()).ToString();

                }

                if (ddlHolidayTypeEdit.SelectedValue.ToLower() == "n")
                {
                    chkSelectLocationEdit.Enabled = false;
                }
                else
                {
                    chkSelectLocationEdit.Enabled = true;
                }
                txtHolidayIDEdit.Enabled = false;
                mpeEditHolidays.Show();
            }

            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            lblErrorEdit.Text = "";
            try
            {
                clsHoliday objHoliday = new clsHoliday();
                objHoliday.HOLIDAY_ID = txtHolidayIDEdit.Text.Trim();
                objHoliday.HOLIDAY_DESCRIPTION = txtDescriptionEdit.Text.Trim();
                objHoliday.HOLIDAY_TYPE = ddlHolidayTypeEdit.SelectedValue;
                objHoliday.HOLIDAY_DATE = txtHolidayDateEdit.Text.Trim();
                objHoliday.HOLIDAY_SWAP = txtSwapDateEdit.Text.Trim();
                objHoliday.HOLIDAY_LOCATION = chkSelectLocationEdit.SelectedValue;
                objHoliday.CreatedBy = Session["uid"].ToString();
                clsHolidayHandler.InsertUpdateHolidayDetails(objHoliday, "Update", HolidayLocation(gvHolidayLocationsEdit, txtHolidayIDEdit.Text.Trim()).ToString(), ViewState["OldLocation"].ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblErrorEdit.Text = strErrMsg;
                    lblErrorEdit.Visible = true;
                    mpeEditHolidays.Show();
                    return;
                }
                else
                {
                    lblMessages.Text = strSuccMsg;
                    bindDataGrid();
                    mpeEditHolidays.Hide();
                }


            }
            catch (Exception ex)
            {
                lblErrorEdit.Text = ex.Message;
                lblErrorEdit.Visible = true;
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }


        }
        protected void gvHoliday_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Added by Pooja Yadav
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnTdayFlag = (HiddenField)e.Row.FindControl("hdnTdayFlag");
                CheckBox chkDeleteRows = (CheckBox)e.Row.FindControl("DeleteRows");
                if ((hdnTdayFlag.Value.Trim().Equals("1", StringComparison.CurrentCultureIgnoreCase)))
                    chkDeleteRows.Enabled = false;

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

    }
}