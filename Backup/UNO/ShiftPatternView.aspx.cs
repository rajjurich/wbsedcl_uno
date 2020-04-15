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
    public partial class ShiftPatternView : System.Web.UI.Page
    {
        static string strSuccMsg, strErrMsg = string.Empty;
      
        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                bindDataGrid();
                ButtonDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvTempCard.ClientID + "');");
            }
        }
        public void bindDataGrid()
        {
            try
            {

                DataTable _dt = clsShiftPatternViewHandler.GetShiftDetails("All");
                if (_dt.Rows.Count != 0)
                {
                    gvTempCard.DataSource = _dt;
                    gvTempCard.DataBind();
                    DropDownList ddl = (DropDownList)gvTempCard.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvTempCard.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvTempCard.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvTempCard.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvTempCard.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvTempCard.PageCount == 0)
                    {
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvTempCard.PageIndex + 1 == gvTempCard.PageCount)
                    {
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvTempCard.PageIndex == 0)
                    {
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvTempCard.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTempCard.PageSize * gvTempCard.PageIndex) + 1) + " to " + (((gvTempCard.PageSize * (gvTempCard.PageIndex + 1)) - 10) + gvTempCard.Rows.Count);

                    gvTempCard.BottomPagerRow.Visible = true;
                }
                else
                {
                    ButtonDelete.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        public DataTable getDataTable(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();


                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);


                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();

                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                return null;
            }
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StringCollection idCollection = new StringCollection();
            String index;
         
            try
            {

                for (int i = 0; i < gvTempCard.Rows.Count; i++)
                {
                    CheckBox delrows = (CheckBox)gvTempCard.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        index = gvTempCard.DataKeys[i].Value.ToString();
                        idCollection.Add(index.ToString());
                    }
                }

                if (idCollection.Count != 0)
                {
                    string strErr="", strSucc = "";
                    Boolean _result = DeleteRecords(idCollection,ref strErr,ref strSucc);
                    if (strErr.Trim().Length >= 1)
                    {
                        bindDataGrid();
                        lblMessages.Visible = true;
                        lblMessages.Text = strErr;
                    }
                    else
                    {
                        bindDataGrid();
                        lblMessages.Visible = true;
                        lblMessages.Text = strSucc;
                    }
                }
                else
                {
                    lblMessages.Visible = true;
                    lblMessages.Text = "Please Select Record to delete";
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        public Boolean DeleteRecords(StringCollection _idCollection, ref string strErr, ref string strSucc)
        {
            try
            {
                int cnt = 0;
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<TA_SHIFT_PATTERN>");
               
                for (int i = 0; _idCollection.Count > i; i++)
                {
                    try
                    {
                        cnt++;
                        strXML.Append("<Shift>");
                        strXML.Append("<SHIFT_PATTERN_ID>" + _idCollection[i].ToString().Trim() + "</SHIFT_PATTERN_ID>");
                        strXML.Append("</Shift>");
                       

                    }
                    catch (Exception ex)
                    {
                        UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                    }
                }
                strXML.Append("</TA_SHIFT_PATTERN>");
                if (cnt > 0)
                {
                    clsShiftPatternView objData = new clsShiftPatternView();                  
                    objData.CreatedBy = Session["uid"].ToString();
                    clsShiftPatternViewHandler.UpdateShiftDetails(objData, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        strErr = strErrMsg;
                        return false;
                    }
                    else
                    {
                        strSucc = strSuccMsg;
                        return true;
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsShiftPatternViewHandler.GetShiftDetails("All");

                if (txtCompanyID.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
                {
                    gvTempCard.DataSource = dt;
                    gvTempCard.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"SHIFT_PATTERN_ID~" +txtCompanyID.Text.Trim(), "S" },
				{"SHIFT_PATTERN_DESCRIPTION~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvTempCard.DataSource = _tempDT;
                    gvTempCard.DataBind();
                }
                if (gvTempCard.Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)gvTempCard.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvTempCard.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvTempCard.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvTempCard.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvTempCard.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvTempCard.PageCount == 0)
                    {
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvTempCard.PageIndex + 1 == gvTempCard.PageCount)
                    {
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvTempCard.PageIndex == 0)
                    {
                        ((Button)gvTempCard.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvTempCard.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvTempCard.PageSize * gvTempCard.PageIndex) + 1) + " to " + (((gvTempCard.PageSize * (gvTempCard.PageIndex + 1)) - 10) + gvTempCard.Rows.Count);

                    gvTempCard.BottomPagerRow.Visible = true;
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblErrorAdd.Text = "";
            lblMessages.Text = "";
            cmbShiftPatternTypeEdit.Enabled = true;
            mpeAddTC.Show();
        }
        protected void btnSaveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSpidAdd.Text != string.Empty && txtdescriptionAdd.Text != string.Empty && cmbShiftPatternTypeAdd.SelectedItem.Text != "SELECT ONE")
                {
                    clsShiftPatternView objData = new clsShiftPatternView();
                    objData.PatternID = txtSpidAdd.Text.Trim();
                    objData.PatternDesr = txtdescriptionAdd.Text.Trim();
                    objData.PatternType = cmbShiftPatternTypeAdd.SelectedValue.ToString();
                    objData.Pattern = HDNSelectedShiftAdd.Value.ToString();
                    objData.CreatedBy = Session["uid"].ToString();
                    clsShiftPatternViewHandler.UpdateShiftDetails(objData, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblErrorAdd.Text = strErrMsg;
                        lblErrorAdd.Visible = true;
                        mpeAddTC.Show();
                        return;
                    }
                    else
                    {
                        mpeAddTC.Show();
                        lblErrorAdd.Text = strSuccMsg.Trim();
                        lblErrorAdd.Visible = true;
                        txtSpidAdd.Text = "";
                        txtdescriptionAdd.Text = "";
                        lstSShiftAdd.Items.Clear();
                        cmbShiftPatternTypeAdd.SelectedIndex = 0;
                        txtshiftAdd.Text = "";
                        bindDataGrid();
                    }

                }
                else
                {
                    lblErrorAdd.Text = "Fill the Require Details to Save.";
                    lblErrorAdd.Visible = true;
                }


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSpidEdit.Text != string.Empty && txtdescriptionEdit.Text != string.Empty && cmbShiftPatternTypeEdit.SelectedItem.Text != "SELECT ONE")
                {

                    clsShiftPatternView objData = new clsShiftPatternView();
                    objData.PatternID = txtSpidEdit.Text.Trim();
                    objData.PatternDesr = txtdescriptionEdit.Text.Trim();
                    objData.PatternType = cmbShiftPatternTypeEdit.SelectedValue.ToString();
                    objData.Pattern = HDNSelectedShiftEdit.Value.ToString();
                    objData.CreatedBy = Session["uid"].ToString();
                    clsShiftPatternViewHandler.UpdateShiftDetails(objData, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblErrorEdit.Text = strErrMsg;
                        lblErrorEdit.Visible = true;
                        mpeEditTC.Show();
                        return;
                    }
                    else
                    {
                        mpeEditTC.Hide();
                        lblMessages.Text = strSuccMsg;
                        lblMessages.Visible = true;
                        txtSpidEdit.Text = "";
                        txtdescriptionEdit.Text = "";
                        lstSShiftEdit.Items.Clear();
                        cmbShiftPatternTypeEdit.SelectedIndex = 0;
                        txtshiftEdit.Text = "";
                        bindDataGrid();
                    }
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
                gvTempCard.PageIndex = Convert.ToInt32(((DropDownList)gvTempCard.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
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
                gvTempCard.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        public void intializeControlAdd()
        {

            try
            {

                DataTable _dt = clsShiftPatternViewHandler.GetShiftDetails("GetShift");
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAShiftAdd.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));

                    }
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        public void intializeControlEdit()
        {
            try
            {
                DataTable _dt = clsShiftPatternViewHandler.GetShiftDetails("GetShift");              
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAShiftEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));                       
                    }
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "Modify")
                {
                    lblErrorEdit.Text = "";
                    lstSShiftEdit.Items.Clear();
                    string CompanyId = e.CommandArgument.ToString();                  
                    DataTable dt = clsShiftPatternViewHandler.GetShiftDetails("GetEditData", CompanyId);
                   
                    lstAShiftEdit.Items.Clear();
                    intializeControlEdit();
                    if (dt.Rows.Count > 0)
                    {

                        txtSpidEdit.Text = dt.Rows[0]["SHIFT_PATTERN_ID"].ToString();
                        txtdescriptionEdit.Text = dt.Rows[0]["SHIFT_PATTERN_DESCRIPTION"].ToString();
                        cmbShiftPatternTypeEdit.SelectedValue = ((dt.Rows[0]["SHIFT_PATTERN_TYPE"].ToString() == "") ? "0" : dt.Rows[0]["SHIFT_PATTERN_TYPE"].ToString());
                        txtshiftEdit.Text = dt.Rows[0]["SHIFT_PATTERN"].ToString();
                        txtSpidEdit.Enabled = false;
                        cmbShiftPatternTypeEdit.Enabled = false;




                        if (cmbShiftPatternTypeEdit.SelectedItem.Text == "DAILY")
                        {
                            int index = lstAShiftEdit.Items.IndexOf(lstAShiftEdit.Items.FindByText("OFF"));
                            if (index >= 0)
                            {
                                lstAShiftEdit.Items.RemoveAt(index);
                            }
                            lstAShiftEdit.Items.Clear();

                            lstAShiftEdit.Items.Add(new ListItem("OFF", "OFF"));
                            intializeControlEdit();
                        }
                        else if (cmbShiftPatternTypeEdit.SelectedItem.Text != "SELECT ONE")
                        {
                            int index = lstAShiftEdit.Items.IndexOf(lstAShiftEdit.Items.FindByText("OFF"));
                            if (index >= 0)
                            {
                                lstAShiftEdit.Items.RemoveAt(index);
                            }
                            lstAShiftEdit.Items.Clear();

                            intializeControlEdit();
                        }

                        if (txtshiftEdit.Text != string.Empty)
                        {
                            string[] list = txtshiftEdit.Text.Split('-');
                            for (int count = 0; count < list.Length; count++)
                            {
                                int index = lstAShiftEdit.Items.IndexOf(lstAShiftEdit.Items.FindByValue(list[count]));
                                if (index == -1)
                                {
                                    lstSShiftEdit.Items.Add(new ListItem("OFF", "OFF"));
                                }
                                else
                                {

                                    lstAShiftEdit.SelectedIndex = index;
                                    lstSShiftEdit.Items.Add(new ListItem(lstAShiftEdit.SelectedItem.Text, lstAShiftEdit.SelectedItem.Value.ToString()));

                                }
                            }
                            lstAShiftEdit.SelectedIndex = 0;
                            lstSShiftEdit.SelectedIndex = 0;
                        }
                        else
                        {
                            lstAShiftEdit.SelectedIndex = 0;
                        }
                        mpeEditTC.Show();

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
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvTempCard.PageIndex = gvTempCard.PageIndex - 1;
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
                gvTempCard.PageIndex = gvTempCard.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void ShiftPatternTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbShiftPatternTypeEdit.SelectedItem.Text == "DAILY")
                {
                    int index = lstAShiftEdit.Items.IndexOf(lstAShiftEdit.Items.FindByText("OFF"));
                    if (index >= 0)
                    {
                        lstAShiftEdit.Items.RemoveAt(index);
                    }
                    lstAShiftEdit.Items.Clear();
                    lstSShiftEdit.Items.Clear();
                    lstAShiftEdit.Items.Add(new ListItem("OFF", "OFF"));
                    intializeControlEdit();
                    txtshiftEdit.Text = "";
                }
                else if (cmbShiftPatternTypeEdit.SelectedItem.Text != "SELECT ONE")
                {
                    int index = lstAShiftEdit.Items.IndexOf(lstAShiftEdit.Items.FindByText("OFF"));
                    if (index >= 0)
                    {
                        lstAShiftEdit.Items.RemoveAt(index);
                    }
                    lstAShiftEdit.Items.Clear();
                    lstSShiftEdit.Items.Clear();
                    intializeControlEdit();
                    txtshiftEdit.Text = "";
                }
                else
                {
                    lstAShiftEdit.Items.Clear();
                    lstSShiftEdit.Items.Clear();
                    txtshiftEdit.Text = "";
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        protected void cmbShiftPatternTypeAdd_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                if (cmbShiftPatternTypeAdd.SelectedItem.Text == "DAILY")
                {
                    int index = lstAShiftAdd.Items.IndexOf(lstAShiftAdd.Items.FindByText("OFF"));
                    if (index >= 0)
                    {
                        lstAShiftAdd.Items.RemoveAt(index);
                    }
                    lstAShiftAdd.Items.Clear();
                    lstSShiftAdd.Items.Clear();
                    lstAShiftAdd.Items.Add(new ListItem("OFF", "OFF"));
                    intializeControlAdd();
                    txtshiftAdd.Text = "";
                }
                else if (cmbShiftPatternTypeAdd.SelectedItem.Text != "SELECT ONE")
                {
                    int index = lstAShiftAdd.Items.IndexOf(lstAShiftAdd.Items.FindByText("OFF"));
                    if (index >= 0)
                    {
                        lstAShiftAdd.Items.RemoveAt(index);
                    }
                    lstAShiftAdd.Items.Clear();
                    lstSShiftAdd.Items.Clear();
                    intializeControlAdd();
                    txtshiftAdd.Text = "";
                }
                else
                {
                    lstAShiftAdd.Items.Clear();
                    txtshiftAdd.Text = "";
                    lstSShiftAdd.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

            mpeAddTC.Show();


        }
        protected void lstAShiftAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstAShiftAdd.SelectedIndex;
            lstAShiftAdd.SelectedIndex = index;
        }
        protected void lstSShiftAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstSShiftAdd.SelectedIndex;
            lstSShiftAdd.SelectedIndex = index;
        }
        protected void lstAShiftEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstAShiftEdit.SelectedIndex;
            lstAShiftEdit.SelectedIndex = index;
        }
        protected void lstSShiftEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstSShiftEdit.SelectedIndex;
            lstSShiftEdit.SelectedIndex = index;

        }
        protected void btnCancelAd_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            lblMessages.Visible = false;
            lstSShiftAdd.Items.Clear();
            lstAShiftAdd.Items.Clear();
            txtSpidAdd.Text = "";
            txtdescriptionAdd.Text = "";
            cmbShiftPatternTypeAdd.SelectedIndex = 0;
            txtshiftAdd.Text = "";
            lblErrorAdd.Text = "";
            lblErrorAdd.Visible = false;
            mpeAddTC.Hide();
        }
        protected void btnCancelEdi_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            lblMessages.Visible = false;
            txtSpidEdit.Text = "";
            txtdescriptionEdit.Text = "";
            cmbShiftPatternTypeEdit.SelectedIndex = 0;
            lstSShiftEdit.Items.Clear();
            lstAShiftEdit.Items.Clear();
            txtshiftEdit.Text = "";
            lblErrorEdit.Text = "";
            lblErrorEdit.Visible = false;
            mpeEditTC.Hide();
        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyID.Text = "";
            txtCompanyName.Text = "";
            bindDataGrid();
        }


    }
}







