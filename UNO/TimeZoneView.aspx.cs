using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using CMS.UNO.Sentinel.Handler;
namespace UNO
{
    public partial class TimeZoneView1 : System.Web.UI.Page
    {
    
        static string path = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                bindDataGrid();
                SetTimezoneData();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvTimezone.ClientID + "');");

            }
        }
        void bindDataGrid()
        {
            try
            {

                DataTable dt = clsTimeZoneViewHandler.GetAllDetails("All");

                gvTimezone.DataSource = dt;
                gvTimezone.DataBind();

                DropDownList ddl = (DropDownList)gvTimezone.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvTimezone.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvTimezone.PageIndex + 1).ToString();
                Label lblcount = (Label)gvTimezone.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvTimezone.DataSource).Rows.Count.ToString() + " Records.";
                if (gvTimezone.PageCount == 0)
                {
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimezone.PageIndex + 1 == gvTimezone.PageCount)
                {
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimezone.PageIndex == 0)
                {
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }


                ((Label)gvTimezone.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTimezone.PageSize * gvTimezone.PageIndex) + 1) + " to " + (((gvTimezone.PageSize * (gvTimezone.PageIndex + 1)) - gvTimezone.PageSize) + gvTimezone.Rows.Count);

                gvTimezone.BottomPagerRow.Visible = true;


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
        private void SetTimezoneData()
        {
            DataTable dtTZ = new DataTable();
            DataRow drType = null;

            dtTZ.Columns.Add(new DataColumn("DELETE_PERIOD", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("PERIOD_ID", typeof(int)));
            dtTZ.Columns.Add(new DataColumn("START_TIME", typeof(string)));
            dtTZ.Columns.Add(new DataColumn("END_TIME", typeof(string)));
            dtTZ.Columns.Add(new DataColumn("SUNDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("MONDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("TUESDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("WEDNESDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("THURSDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("FRIDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("SATURDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("HOLIDAY1", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("HOLIDAY2", typeof(bool)));


            for (int count = 0; count < 12; count++)
            {
                drType = dtTZ.NewRow();
                drType["DELETE_PERIOD"] = false;
                drType["PERIOD_ID"] = count + 1;
                drType["START_TIME"] = false;
                drType["END_TIME"] = false;
                drType["SUNDAY"] = false;
                drType["MONDAY"] = false;
                drType["TUESDAY"] = false;
                drType["WEDNESDAY"] = false;
                drType["THURSDAY"] = false;
                drType["FRIDAY"] = false;
                drType["SATURDAY"] = false;
                drType["HOLIDAY1"] = false;
                drType["HOLIDAY2"] = false;
                dtTZ.Rows.Add(drType);
                gvPeriods.DataSource = dtTZ;
                gvPeriods.DataBind();

            }

            for (int i = 0; i < dtTZ.Rows.Count; i++)
            {
                TextBox PeriodId = (TextBox)gvPeriods.Rows[i].Cells[0].FindControl("txtPeriod");
                PeriodId.Text = (i + 1).ToString();
            }
        }
        private void SetEditTimezoneData()
        {
            DataTable dtTZ = new DataTable();
            DataRow drType = null;

            dtTZ.Columns.Add(new DataColumn("DELETE_PERIOD", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("PERIOD_ID", typeof(int)));
            dtTZ.Columns.Add(new DataColumn("START_TIME", typeof(string)));
            dtTZ.Columns.Add(new DataColumn("END_TIME", typeof(string)));
            dtTZ.Columns.Add(new DataColumn("SUNDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("MONDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("TUESDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("WEDNESDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("THURSDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("FRIDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("SATURDAY", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("HOLIDAY1", typeof(bool)));
            dtTZ.Columns.Add(new DataColumn("HOLIDAY2", typeof(bool)));


            for (int count = 0; count < 12; count++)
            {
                drType = dtTZ.NewRow();
                drType["DELETE_PERIOD"] = false;
                drType["PERIOD_ID"] = count + 1;
                drType["START_TIME"] = false;
                drType["END_TIME"] = false;
                drType["SUNDAY"] = false;
                drType["MONDAY"] = false;
                drType["TUESDAY"] = false;
                drType["WEDNESDAY"] = false;
                drType["THURSDAY"] = false;
                drType["FRIDAY"] = false;
                drType["SATURDAY"] = false;
                drType["HOLIDAY1"] = false;
                drType["HOLIDAY2"] = false;
                dtTZ.Rows.Add(drType);
                gvEditPeriod.DataSource = dtTZ;
                gvEditPeriod.DataBind();

            }

            for (int i = 0; i < dtTZ.Rows.Count; i++)
            {
                TextBox PeriodId = (TextBox)gvEditPeriod.Rows[i].Cells[0].FindControl("txtPeriod");
                PeriodId.Text = (i + 1).ToString();
            }
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            int count = clsTimeZoneViewHandler.GetCount();

            if (count >= 32)
            {
                lblError.Text = "Can not create more than 32 timezones";
                lblError.Visible = true;
                mpeAddTZ.Hide();
                return;
            }

            lblError.Text = "";
            string strMsg = string.Empty;
            StringBuilder strXML = new StringBuilder();
            StringBuilder strRelationXML = new StringBuilder();
            try
            {

                if (IsTimeOverlap(ref strMsg) == true)
                {
                    if (strMsg.Trim().Length > 1)
                    {
                        btnSubmitAdd.Enabled = true;
                        lblError.Text = strMsg;
                        return;
                    }
                    else
                    {
                        btnSubmitAdd.Enabled = true;
                        lblError.Text = "Timezone periods ovelapping";
                        return;
                    }
                }

                string strDescription = txtTZDesc.Text.Trim().Length > 0 ? (txtTZDesc.Text.Trim().First().ToString().ToUpper() + String.Join("", txtTZDesc.Text.Trim().Skip(1))) : "";

                strXML.Append("<TimeZoneDetails>");
                for (int i = 0; i < gvPeriods.Rows.Count; i++)
                {
                    TextBox PeriodId = (TextBox)gvPeriods.Rows[i].Cells[0].FindControl("txtPeriod");
                    TextBox StartTime = (TextBox)gvPeriods.Rows[i].Cells[1].FindControl("txtStartTime");
                    TextBox EndTime = (TextBox)gvPeriods.Rows[i].Cells[2].FindControl("txtEndTime");
                    CheckBox Sunday = (CheckBox)gvPeriods.Rows[i].Cells[3].FindControl("chkSun");
                    CheckBox Monday = (CheckBox)gvPeriods.Rows[i].Cells[4].FindControl("chkMon");
                    CheckBox Tuesday = (CheckBox)gvPeriods.Rows[i].Cells[5].FindControl("chkTue");
                    CheckBox Wednesday = (CheckBox)gvPeriods.Rows[i].Cells[6].FindControl("chkWed");
                    CheckBox Thurday = (CheckBox)gvPeriods.Rows[i].Cells[7].FindControl("chkThr");
                    CheckBox Friday = (CheckBox)gvPeriods.Rows[i].Cells[8].FindControl("chkFri");
                    CheckBox Saturday = (CheckBox)gvPeriods.Rows[i].Cells[9].FindControl("chkSat");
                    CheckBox Holiday1 = (CheckBox)gvPeriods.Rows[i].Cells[10].FindControl("chkH1");
                    CheckBox Holiday2 = (CheckBox)gvPeriods.Rows[i].Cells[11].FindControl("chkH2");

                    if (Sunday.Checked || Monday.Checked || Tuesday.Checked || Wednesday.Checked || Thurday.Checked || Friday.Checked || Saturday.Checked || Holiday1.Checked || Holiday2.Checked)


                        if (Sunday.Checked)
                        {
                            strXML.Append("<Periods>");
                            strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                            strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                            strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                            strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                            strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                            strXML.Append("<TZ_DAYOFWEEK>SUN</TZ_DAYOFWEEK>");
                            strXML.Append("</Periods>");
                        }
                    if (Monday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>MON</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Tuesday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>TUE</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Wednesday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>WED</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");

                    }
                    if (Thurday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>THR</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Friday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>FRI</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Saturday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>SAT</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Holiday1.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>HO</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>H1</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Holiday2.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTZID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>HO</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>H2</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }


                }

                strXML.Append("</TimeZoneDetails>");
                clsTimeZone objData = new clsTimeZone();
                objData.TZCode = Convert.ToInt32(txtTZID.Text.Trim());
                objData.TZDescription = strDescription;
                objData.CreatedBy = Session["uid"].ToString();
                clsTimeZoneViewHandler.UpdateTimeZoneDetails(objData, "Insert", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Length >= 1)
                {
                    lblError.Text = strErrMsg;
                    btnSubmitAdd.Enabled = true;
                    mpeAddTZ.Show();
                    return;
                }
                else
                {
                    txtTZID.Text = "";
                    txtTZDesc.Text = "";
                    gvPeriods.DataSource = null;
                    btnSubmitAdd.Enabled = true;
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    SetTimezoneData();
                    mpeAddTZ.Hide();
                    bindDataGrid();
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                lblError.Text = "";
            }
        }
        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            #region "UpdateData"
            lblMessages.Text = string.Empty;
            lblMsg.Text = string.Empty;
            StringBuilder strXML = new StringBuilder();
            StringBuilder strRelationXML = new StringBuilder();
            string strMsg = string.Empty;         

            try
            {
                if (IsTimeOverlap1(ref strMsg) == true)
                {
                    if (strMsg.Trim().Length > 1)
                    {
                        btnSubmitEdit.Enabled = true;
                        lblMsg.Text = strMsg;
                        return;
                    }
                    else
                    {
                        btnSubmitEdit.Enabled = true;
                        lblMsg.Text = "Timezone periods ovelapping";
                        return;
                    }
                }
                string strDescription = txtTDesc.Text.Trim().Length > 0 ? (txtTDesc.Text.Trim().First().ToString().ToUpper() + String.Join("", txtTDesc.Text.Trim().Skip(1))) : "";

                strXML.Append("<TimeZoneDetails>");

                for (int i = 0; i < gvEditPeriod.Rows.Count; i++)
                {
                    TextBox PeriodId = (TextBox)gvEditPeriod.Rows[i].Cells[0].FindControl("txtPeriod");
                    TextBox StartTime = (TextBox)gvEditPeriod.Rows[i].Cells[1].FindControl("txtStartTimeE");
                    TextBox EndTime = (TextBox)gvEditPeriod.Rows[i].Cells[2].FindControl("txtEndTimeE");
                    CheckBox Sunday = (CheckBox)gvEditPeriod.Rows[i].Cells[3].FindControl("chkSun");
                    CheckBox Monday = (CheckBox)gvEditPeriod.Rows[i].Cells[4].FindControl("chkMon");
                    CheckBox Tuesday = (CheckBox)gvEditPeriod.Rows[i].Cells[5].FindControl("chkTue");
                    CheckBox Wednesday = (CheckBox)gvEditPeriod.Rows[i].Cells[6].FindControl("chkWed");
                    CheckBox Thurday = (CheckBox)gvEditPeriod.Rows[i].Cells[7].FindControl("chkThr");
                    CheckBox Friday = (CheckBox)gvEditPeriod.Rows[i].Cells[8].FindControl("chkFri");
                    CheckBox Saturday = (CheckBox)gvEditPeriod.Rows[i].Cells[9].FindControl("chkSat");
                    CheckBox Holiday1 = (CheckBox)gvEditPeriod.Rows[i].Cells[10].FindControl("chkH1");
                    CheckBox Holiday2 = (CheckBox)gvEditPeriod.Rows[i].Cells[11].FindControl("chkH2");

                  
                    if (Sunday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>SUN</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");

                    }
                    if (Monday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>MON</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Tuesday.Checked)
                    { 
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>TUE</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");

                    }
                    if (Wednesday.Checked)
                    {                       
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>WED</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");

                    }
                    if (Thurday.Checked)
                    {                       
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>THR</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Friday.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>FRI</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Saturday.Checked)
                    {                        
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>Dow</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>SAT</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Holiday1.Checked)
                    {                      
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>HO</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>H1</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    if (Holiday2.Checked)
                    {
                        strXML.Append("<Periods>");
                        strXML.Append("<TZ_CODE>" + txtTID.Text.Trim() + "</TZ_CODE>");
                        strXML.Append("<TZ_FROMTIME>" + StartTime.Text + "</TZ_FROMTIME>");
                        strXML.Append("<TZ_TOTIME>" + EndTime.Text + "</TZ_TOTIME>");
                        strXML.Append("<TZ_TYPE>HO</TZ_TYPE>");
                        strXML.Append("<period_id>" + PeriodId.Text + "</period_id>");
                        strXML.Append("<TZ_DAYOFWEEK>H2</TZ_DAYOFWEEK>");
                        strXML.Append("</Periods>");
                    }
                    
                }

                strXML.Append("</TimeZoneDetails>");
                clsTimeZone objData = new clsTimeZone();

                objData.TZCode = Convert.ToInt32(txtTID.Text.Trim());
                objData.TZDescription = strDescription;
                objData.CreatedBy = Session["uid"].ToString();
                clsTimeZoneViewHandler.UpdateTimeZoneDetails(objData, "Update", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Length >= 1)
                {
                    lblMsg.Text = strErrMsg;
                    btnSubmitEdit.Enabled = true;
                    mpeEditTZ.Show();
                    return;
                }
                else
                {                   
                    txtTID.Text = "";
                    txtTDesc.Text = "";
                    gvEditPeriod.DataSource = null;
                    btnSubmitEdit.Enabled = true;
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    mpeEditTZ.Hide();
                }
            #endregion

            }
            catch (Exception ex)
            {            
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void gvTimezone_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {

                    mpeEditTZ.Show();
                    string TimezoneID = e.CommandArgument.ToString();
                    DataSet ds = clsTimeZoneViewHandler.GetAllDetails("FillonEdit", Convert.ToInt32(TimezoneID));
                    DataTable dt = ds.Tables[0];
                    DataTable dtTimezone = ds.Tables[1];

                    if (dt.Rows.Count > 0)
                    {

                        txtTID.Text = dt.Rows[0]["TZ_CODE"].ToString();
                        txtTDesc.Text = dt.Rows[0]["TZ_DESCRIPTION"].ToString();
                        SetEditTimezoneData();

                        for (int i = 0; i < dtTimezone.Rows.Count; i++)
                        {

                            TextBox StartTime = (TextBox)gvEditPeriod.Rows[i].Cells[1].FindControl("txtStartTimeE");
                            TextBox EndTime = (TextBox)gvEditPeriod.Rows[i].Cells[2].FindControl("txtEndTimeE");
                            CheckBox Sunday = (CheckBox)gvEditPeriod.Rows[i].Cells[3].FindControl("chkSun");
                            CheckBox Monday = (CheckBox)gvEditPeriod.Rows[i].Cells[4].FindControl("chkMon");
                            CheckBox Tuesday = (CheckBox)gvEditPeriod.Rows[i].Cells[5].FindControl("chkTue");
                            CheckBox Wednesday = (CheckBox)gvEditPeriod.Rows[i].Cells[6].FindControl("chkWed");
                            CheckBox Thurday = (CheckBox)gvEditPeriod.Rows[i].Cells[7].FindControl("chkThr");
                            CheckBox Friday = (CheckBox)gvEditPeriod.Rows[i].Cells[8].FindControl("chkFri");
                            CheckBox Saturday = (CheckBox)gvEditPeriod.Rows[i].Cells[9].FindControl("chkSat");
                            CheckBox Holiday1 = (CheckBox)gvEditPeriod.Rows[i].Cells[10].FindControl("chkH1");
                            CheckBox Holiday2 = (CheckBox)gvEditPeriod.Rows[i].Cells[11].FindControl("chkH2");


                            StartTime.Text = dtTimezone.Rows[i]["StartTime"].ToString();
                            EndTime.Text = dtTimezone.Rows[i]["EndTime"].ToString();


                            Sunday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["SUN"]);
                            Monday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["MON"]);
                            Tuesday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["TUE"]);
                            Wednesday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["WED"]);
                            Thurday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["THR"]);
                            Friday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["FRI"]);
                            Saturday.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["SAT"]);
                            Holiday1.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["H1"]);
                            Holiday2.Checked = Convert.ToBoolean(dtTimezone.Rows[i]["H2"]);

                            mpeEditTZ.Show();
                        }
                    }
                    else
                    {
                        lblMessages.Text = "Records not found";
                    }
                }




            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            gvPeriods.DataSource = null;
            gvPeriods.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = string.Empty;
                lblMsg.Text = string.Empty;
                lblError.Text = string.Empty;
                DataTable dt = clsTimeZoneViewHandler.GetAllDetails("All");

                if (txtTimezoneDesc.Text.ToString() == "" && txtTimezoneId.Text.ToString() == "")
                {

                    gvTimezone.DataSource = dt;
                    gvTimezone.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"TZ_CODE~" +txtTimezoneId.Text.Trim(), "S" },
				{"TZ_DESCRIPTION~" +txtTimezoneDesc.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvTimezone.DataSource = _tempDT;
                    gvTimezone.DataBind();
                }

                DropDownList ddl = (DropDownList)gvTimezone.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvTimezone.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvTimezone.PageIndex + 1).ToString();
                Label lblcount = (Label)gvTimezone.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvTimezone.DataSource).Rows.Count.ToString() + " Records.";
                if (gvTimezone.PageCount == 0)
                {
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimezone.PageIndex + 1 == gvTimezone.PageCount)
                {
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvTimezone.PageIndex == 0)
                {
                    ((Button)gvTimezone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvTimezone.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTimezone.PageSize * gvTimezone.PageIndex) + 1) + " to " + (((gvTimezone.PageSize * (gvTimezone.PageIndex + 1)) - gvTimezone.PageSize) + gvTimezone.Rows.Count);

                gvTimezone.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void gvTimezone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dr = e.Row.DataItem as DataRowView;
                if (dr["TZ_DESCRIPTION"].ToString().ToUpper() == "ALWAYS" || dr["TZ_DESCRIPTION"].ToString().ToUpper() == "NEVER")
                {
                    e.Row.Enabled = false;
                }
            }
        }
        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            SetTimezoneData();
            lblError.Text = "";
            lblMessages.Text = "";
            txtTZID.Text = "";
            txtTZDesc.Text = "";
            mpeAddTZ.Hide();
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {

            int count = clsTimeZoneViewHandler.GetCount();

            if (count >= 32)
            {
                lblMessages.Text = "Can not create more than 32 timezones";
                lblMessages.Visible = true;
                mpeAddTZ.Hide();
                return;
            }
            else
            {
                lblMessages.Text = "";
                lblError.Text = "";
                mpeAddTZ.Show();
            }

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                delete();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        private void delete()
        {
        
            StringBuilder strXML = null;
            StringBuilder strXMLRelation = null;
            string ZoneID = string.Empty;
            strXMLRelation = new StringBuilder();
            strXML = new StringBuilder();
            strXML.Append("<ACS_TIMEZONE >");
         
            for (int i = 0; i < gvTimezone.Rows.Count; i++)
            {
                try
                {

                 
                    CheckBox delrows = (CheckBox)gvTimezone.Rows[i].FindControl("DeleteRows");
                    LinkButton lnkEdit = (LinkButton)gvTimezone.Rows[i].FindControl("lnkEdit");
                    HiddenField hdnRowID = (HiddenField)gvTimezone.Rows[i].FindControl("hdnRowID");
                    if (delrows.Checked)
                    {
                        strXML.Append("<TIMEZONE>");
                        strXML.Append("<ID>" + hdnRowID.Value + "</ID>");
                        strXML.Append("<Code>" + gvTimezone.Rows[i].Cells[2].Text + "</Code>");
                        strXML.Append("</TIMEZONE>");
                    }
                   
                }
               
                catch (Exception ex)
                {
                 
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                }
            }

         
            strXML.Append("</ACS_TIMEZONE>");

            clsTimeZone objData = new clsTimeZone();
            objData.CreatedBy = Session["uid"].ToString();
            clsTimeZoneViewHandler.UpdateTimeZoneDetails(objData, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
            if (strErrMsg.Length >= 1)
            {
                lblMessages.Text = strErrMsg;
                lblMessages.Visible = true;
                bindDataGrid();
                return;
            }
            else
            {
                lblMessages.Text = strSuccMsg;
                lblMessages.Visible = true;                
                bindDataGrid();
                return;
            }
        }
        private bool IsTimeOverlap(ref string strMsg)
        {
            try
            {
                string[] StartPeriods = new string[12];
                string[] EndPeriods = new string[12];


                for (int i = 0; i < gvPeriods.Rows.Count; i++)
                {
                    TextBox StartTime = (TextBox)gvPeriods.Rows[i].Cells[1].FindControl("txtStartTime");
                    TextBox EndTime = (TextBox)gvPeriods.Rows[i].Cells[2].FindControl("txtEndTime");
                    CheckBox Sunday = (CheckBox)gvPeriods.Rows[i].Cells[3].FindControl("chkSun");
                    CheckBox Monday = (CheckBox)gvPeriods.Rows[i].Cells[4].FindControl("chkMon");
                    CheckBox Tuesday = (CheckBox)gvPeriods.Rows[i].Cells[5].FindControl("chkTue");
                    CheckBox Wednesday = (CheckBox)gvPeriods.Rows[i].Cells[6].FindControl("chkWed");
                    CheckBox Thurday = (CheckBox)gvPeriods.Rows[i].Cells[7].FindControl("chkThr");
                    CheckBox Friday = (CheckBox)gvPeriods.Rows[i].Cells[8].FindControl("chkFri");
                    CheckBox Saturday = (CheckBox)gvPeriods.Rows[i].Cells[9].FindControl("chkSat");
                    CheckBox Holiday1 = (CheckBox)gvPeriods.Rows[i].Cells[10].FindControl("chkH1");
                    CheckBox Holiday2 = (CheckBox)gvPeriods.Rows[i].Cells[11].FindControl("chkH2");

                    StartPeriods[i] = StartTime.Text;
                    EndPeriods[i] = EndTime.Text;

                }

                for (int cnt = 0; cnt < gvPeriods.Rows.Count; cnt++)
                {

                    int row = cnt + 1;
                    while (row < gvPeriods.Rows.Count)
                    {

                        if (StartPeriods[row].ToString().Trim() != string.Empty && StartPeriods[row].ToString().Trim() != ""
                            || EndPeriods[row].ToString().Trim() != string.Empty && EndPeriods[row].ToString().Trim() != "")
                        {
                            if (StartPeriods[cnt] != "" && StartPeriods[row] != "" && EndPeriods[cnt] != "" && EndPeriods[row] != "")
                            {
                                DateTime StartA = DateTime.ParseExact(StartPeriods[cnt], "HH:mm", null);
                                DateTime EndA = DateTime.ParseExact(EndPeriods[cnt], "HH:mm", null);
                                DateTime StartB = DateTime.ParseExact(StartPeriods[row], "HH:mm", null);
                                DateTime EndB = DateTime.ParseExact(EndPeriods[row], "HH:mm", null);


                                if (StartA >= EndA)
                                {
                                    return true;
                                }

                                if ((StartA <= EndB) && (EndA >= StartB))
                                {
                                    return true;
                                }
                                row += 1;
                            }
                        }
                        else
                        {
                            row += 1;
                        }

                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                return false;
            }

        }
        private bool IsTimeOverlap1(ref string strMsg)
        {
            try
            {
                string[] StartPeriods = new string[12];
                string[] EndPeriods = new string[12];

                for (int i = 0; i < gvEditPeriod.Rows.Count; i++)
                {
                    TextBox StartTime = (TextBox)gvEditPeriod.Rows[i].Cells[1].FindControl("txtStartTimeE");
                    TextBox EndTime = (TextBox)gvEditPeriod.Rows[i].Cells[2].FindControl("txtEndTimeE");
                    CheckBox Sunday = (CheckBox)gvEditPeriod.Rows[i].Cells[3].FindControl("chkSun");
                    CheckBox Monday = (CheckBox)gvEditPeriod.Rows[i].Cells[4].FindControl("chkMon");
                    CheckBox Tuesday = (CheckBox)gvEditPeriod.Rows[i].Cells[5].FindControl("chkTue");
                    CheckBox Wednesday = (CheckBox)gvEditPeriod.Rows[i].Cells[6].FindControl("chkWed");
                    CheckBox Thurday = (CheckBox)gvEditPeriod.Rows[i].Cells[7].FindControl("chkThr");
                    CheckBox Friday = (CheckBox)gvEditPeriod.Rows[i].Cells[8].FindControl("chkFri");
                    CheckBox Saturday = (CheckBox)gvEditPeriod.Rows[i].Cells[9].FindControl("chkSat");
                    CheckBox Holiday1 = (CheckBox)gvEditPeriod.Rows[i].Cells[10].FindControl("chkH1");
                    CheckBox Holiday2 = (CheckBox)gvEditPeriod.Rows[i].Cells[11].FindControl("chkH2");

                    StartPeriods[i] = StartTime.Text;
                    EndPeriods[i] = EndTime.Text;

                }

                for (int cnt = 0; cnt < gvEditPeriod.Rows.Count; cnt++)
                {

                    int row = cnt + 1;
                    while (row < gvEditPeriod.Rows.Count)
                    {

                        if (StartPeriods[row].ToString().Trim() != string.Empty && StartPeriods[row].ToString().Trim() != ""
                            && EndPeriods[row].ToString().Trim() != string.Empty && EndPeriods[row].ToString().Trim() != "")
                        {
                            if (StartPeriods[cnt] != "" && StartPeriods[row] != "" && EndPeriods[cnt] != "" && EndPeriods[row] != "")
                            {
                                DateTime StartA = DateTime.ParseExact(StartPeriods[cnt], "HH:mm", null);
                                DateTime EndA = DateTime.ParseExact(EndPeriods[cnt], "HH:mm", null);
                                DateTime StartB = DateTime.ParseExact(StartPeriods[row], "HH:mm", null);
                                DateTime EndB = DateTime.ParseExact(EndPeriods[row], "HH:mm", null);


                                if (StartA >= EndA)
                                {
                                    return true;
                                }

                                if ((StartA <= EndB) && (EndA >= StartB))
                                {
                                    return true;
                                }
                                row += 1;
                            }
                        }
                        else
                        {
                            row += 1;
                        }

                    }
                }

                return false;


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                return false;
            }

        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvTimezone.PageIndex = gvTimezone.PageIndex - 1;
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
                gvTimezone.PageIndex = gvTimezone.PageIndex + 1;
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
                gvTimezone.PageIndex = Convert.ToInt32(((DropDownList)gvTimezone.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }


    }
}