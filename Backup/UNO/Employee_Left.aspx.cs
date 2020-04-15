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
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;
using CMS.UNO.Core.Handler;

namespace UNO
{
    public partial class Employee_Left : System.Web.UI.Page
    {
        static string strSuccMsg, strErrMsg = string.Empty;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string userid, Rowid1 = "", LeaveAvaibleOnEdit = "";
        DataTable dtCheck, DtLVCutUpload, dtLVCutUploadError;
        DataSet dsLVCutUpload;
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
                bindDataGrid();
                FillEmployeeEntity();
                FillLeaveCodeEntity();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvLeaveData.ClientID + "');");
            }

        }
        void updateLeavecut(string empid, DateTime leaveid, int Left_DATE, string Reason, ref string ErrMsg)
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand("PROC_UPDATE_EMP_LEFT_DETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EL_EMPID", empid);
                cmd.Parameters.AddWithValue("@EL_LEFTDATE", leaveid);
                cmd.Parameters.AddWithValue("@EL_ISDELETED", Left_DATE);
                cmd.Parameters.AddWithValue("@EL_Reason", Reason);
                cmd.Parameters.AddWithValue("@ErrorMsg", '0').Direction = ParameterDirection.Output;
                cmd.Parameters["@ErrorMsg"].Size = 1000;
                cmd.ExecuteNonQuery();
                ErrMsg = cmd.Parameters["@ErrorMsg"].Value.ToString();
            }

            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                throw ex;
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        void bindDataGrid()
        {
            try
            {

                DataTable dt = clsEmployeeLeftHandler.GetAllLeftEmployees();
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

                ((Label)gvLeaveData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeaveData.PageSize * gvLeaveData.PageIndex) + 1) + " to " + (((gvLeaveData.PageSize * (gvLeaveData.PageIndex + 1)) - gvLeaveData.PageSize) + gvLeaveData.Rows.Count);

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
        private DataTable Get_PROC_GET_LEAVEDETAILS_BYROWID(string rowid)
        {
            DataTable dt = null;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int _result = 0;

            SqlCommand cmd = new SqlCommand("PROC_GET_EMPLOYEE_LEFT_DETAILS_BYROWID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LV_REC_ID", rowid);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveDataUpload");
            }
        }
        protected void gvLeaveData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string Rowid = e.CommandArgument.ToString();
                lblEditError.Text = string.Empty;
                ViewState["Row"] = Rowid;
                DataTable dt = null;
                dt = Get_PROC_GET_LEAVEDETAILS_BYROWID(Rowid);
                txtEditEmployeeName.Text = dt.Rows[0]["EL_EMP_ID"].ToString();
                TextBox1.Text = dt.Rows[0]["EL_LEFT_DATE"].ToString();
                ddlReasonEdit.SelectedValue = dt.Rows[0]["EL_REASONID"].ToString();
                mpeModifyZone.Show();
                ScriptManager.RegisterClientScriptBlock(updatepnlEditLeaveData, updatepnlEditLeaveData.GetType(), "Script", "validateChosen();", true);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblErrorSingleEntry.Text = string.Empty;
            lblMessages.Text = "";
            txtCalendarFrom.Text = "";
            mpeAddNewEntry.Show();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel4, UpdatePanel4.GetType(), "Script", "validateChosen();", true);
        }
        protected void btnSubmitNewEntry_Onclick(object sender, EventArgs e)
        {
            if (lstEmployees.SelectedValue != null && txtCalendarFrom.Text != string.Empty && ddlReasonAdd.SelectedItem.ToString() != "Select")
            {
                string Rec_ID = "0";
                SaveLeft(Rec_ID);
            }
        }
        protected void btnModifySaveLeave_Click(object sender, EventArgs e)
        {
            SaveLeft(ViewState["Row"].ToString());
        }
        private void SaveLeft(string RecID)
        {
            string employeeID = string.Empty;
            string Emp_Left_date = "";
            string Reason = RecID == "0" ? ddlReasonAdd.SelectedValue.ToString() : ddlReasonEdit.SelectedValue.ToString();
            if (RecID == "0")
            {
                employeeID = lstEmployees.SelectedValue;
                Emp_Left_date = txtCalendarFrom.Text;
            }
            else
            {
                employeeID = txtEditEmployeeName.Text;
                if (TextBox1.Text != "")
                    Emp_Left_date = TextBox1.Text;
            }

            try
            {
                clsEmployeeLeft objEmp = new clsEmployeeLeft();
                objEmp.RecID = Convert.ToInt32(RecID);
                objEmp.EmpID = employeeID;
                objEmp.CreatedBy = Session["uid"].ToString();
                objEmp.LeftDate = Emp_Left_date;
                objEmp.Reason = Reason;
                clsEmployeeLeftHandler.InsertUpdateEmpLeftDetails(objEmp, "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    if (RecID == "0")
                    {
                        mpeAddNewEntry.Show();
                        lblErrorSingleEntry.Text = strErrMsg;
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel4, UpdatePanel4.GetType(), "Script", "validateChosen();", true);
                    }
                    else
                    {
                        lblEditError.Visible = true;
                        lblEditError.Text = strErrMsg;
                        ScriptManager.RegisterClientScriptBlock(updatepnlEditLeaveData, updatepnlEditLeaveData.GetType(), "Script", "validateChosen();", true);
                    }
                }
                else
                {
                    if (RecID == "0")
                    {
                        lblMessages.Text = strSuccMsg;
                        mpeAddNewEntry.Hide();
                        bindDataGrid();
                        ResellAll_NewEntry();
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg;
                        mpeModifyZone.Hide();
                        this.bindDataGrid();
                        ResellAll_NewEntry();
                    }
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void btnAddCancelEntry_Onclick(object sender, EventArgs e)
        {
            ResellAll_NewEntry();
            mpeAddNewEntry.Hide();
            txtCalendarFrom.Text = "";
            lblMessages.Text = "";

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                int cnt = 0;
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<ENT_EMPLOYEE_LEFT>");
                for (int i = 0; i < gvLeaveData.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox delrows = (CheckBox)gvLeaveData.Rows[i].FindControl("DeleteRows");
                        if (delrows.Checked == true)
                        {

                            cnt++;
                            strXML.Append("<EMPLOYEE>");
                            strXML.Append("<EL_ColumnID>" + ((HiddenField)gvLeaveData.Rows[i].FindControl("hdnColId")).Value + "</EL_ColumnID>");
                            strXML.Append("<EL_EMP_ID>" + ((HiddenField)gvLeaveData.Rows[i].FindControl("hdnEmpID")).Value + "</EL_EMP_ID>");
                            strXML.Append("</EMPLOYEE>");
                        }
                    }
                    catch (Exception ex)
                    {
                        UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                    }
                }
                strXML.Append("</ENT_EMPLOYEE_LEFT>");
                if (cnt > 0)
                {
                    clsEmployeeLeft objEmp = new clsEmployeeLeft();
                    objEmp.CreatedBy = Session["uid"].ToString();
                    clsEmployeeLeftHandler.DeleteEmpLeftDetails(objEmp, strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblMessages.Text = strErrMsg.Trim();
                        lblMessages.Visible = true;
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg.Trim();
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                lblMessages.Visible = false;
                ReportViewer1.Visible = false;
                DataTable dt = clsEmployeeLeftHandler.GetAllLeftEmployees();
                if (txtSearchEmployeeid.Text.ToString() == "")
                {

                    gvLeaveData.DataSource = dt;
                    gvLeaveData.DataBind();

                }
                else
                {
                    String[,] values = { 
                {"EL_EMP_ID~" +txtSearchEmployeeid.Text.Trim(), "S" },
               		
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvLeaveData.DataSource = _tempDT;
                    gvLeaveData.DataBind();

                }

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
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnBulkUpload_Click(object sender, EventArgs e)
        {
            mpeAddZone.Show();
            pnlgvLeaveData.Visible = false;
            lblMessages.Text = "";
            lblBulkErrorMessage.Text = "";
            lblMessages.Text = string.Empty;

        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    string extension = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string file = Path.GetFileNameWithoutExtension(fileuploadExcel.FileName).ToLower();
                    string path = string.Concat(Server.MapPath("~/Uploaded Folder/" + fileuploadExcel.FileName));
                    string excelConnectionString = "";
                    fileuploadExcel.SaveAs(path);

                    if (extension == ".xls")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }
                    else if (extension == ".xlsx")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.12.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }

                    else
                    {
                        lblBulkErrorMessage.Text = "Please upload valid file.";
                        lblBulkErrorMessage.Visible = true;
                        mpeAddZone.Show();
                        return;
                    }

                    OleDbConnection connection = new OleDbConnection();

                    connection.ConnectionString = excelConnectionString;

                    string CONSTANT_LV_REC_ID = string.Empty;

                    string[] sheetName = GetExcelSheetNames(file, path);

                    OleDbCommand command = new OleDbCommand("select * from [" + sheetName[0].ToString() + "]", connection);

                    OleDbDataAdapter da = new OleDbDataAdapter(command);

                    dsLVCutUpload = new DataSet();

                    da.Fill(dsLVCutUpload);

                    DtLVCutUpload = dsLVCutUpload.Tables[0];
                    int int_cnt = 0;

                    StringBuilder strXML = new StringBuilder();
                    strXML.Append("<Employees>");
                    for (int i = 0; i < DtLVCutUpload.Rows.Count; i++)
                    {

                        int count = 0;
                        try
                        {
                            if (DtLVCutUpload.Rows[i]["EL_EMP_ID"].ToString() != "" && DtLVCutUpload.Rows[i]["EL_RETIREMENT_DATE"].ToString() != "" && DtLVCutUpload.Rows[i]["EL_REASON"].ToString() != "")
                            {
                                count = count + 1;
                            }

                        }
                        catch (Exception ex)
                        {
                        }

                        dtLVCutUploadError = new DataTable();

                        DataTable dt1 = dsLVCutUpload.Tables[0];

                        dtLVCutUploadError.Columns.Add("EL_EMP_ID", typeof(string));
                        dtLVCutUploadError.Columns.Add("EL_RETIREMENT_DATE", typeof(string));
                        dtLVCutUploadError.Columns.Add("EL_REASON", typeof(string));


                        if (DtLVCutUpload.Rows[i]["EL_EMP_ID"].ToString() == "" || DtLVCutUpload.Rows[i]["EL_RETIREMENT_DATE"].ToString() == "" || DtLVCutUpload.Rows[i]["EL_REASON"].ToString() == "")
                        {
                            dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                        }
                        else
                        {
                            count = count + 1;
                        }
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand objcmd = new SqlCommand();
                        objcmd.Connection = conn;
                        objcmd.CommandText = "SELECT COUNT(*) FROM ENT_EMPLOYEE_PERSONAL_DTLS  WHERE EPD_EMPID ='" + DtLVCutUpload.Rows[i]["EL_EMP_ID"].ToString() + "'";
                        int cnt = Convert.ToInt32(objcmd.ExecuteScalar());
                        if (cnt == 0)
                        {
                            dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                        }
                        else
                        {
                            count = count + 1;
                        }

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlCommand objcmd1 = new SqlCommand();
                        objcmd1.Connection = conn;
                        objcmd1.CommandText = "select COUNT(*) from [ENT_Reason] where Reason_Type='EL'  and isnull(Reason_IsDeleted,0)=0 and  Reason_ID='" + DtLVCutUpload.Rows[i]["EL_REASON"].ToString() + "' ";
                        int cnt1 = Convert.ToInt32(objcmd1.ExecuteScalar());
                        if (cnt1 == 0)
                        {
                            dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            lblMessages.Text = "Please rectify the Reason ID and upload again.";
                            lblMessages.Visible = true;
                            return;
                        }
                        else
                        {
                            count = count + 1;
                        }

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        try
                        {
                           
                            count = count + 1;
                        }
                        catch (Exception ex)
                        {
                            dtLVCutUploadError.ImportRow(dt1.Rows[i]);
                            lblMessages.Text = "Please rectify the Date format and upload again.";
                            lblMessages.Visible = true;
                            return;
                        }
                        if (count == 5)
                        {
                            int_cnt++;
                            string strErrorMsg = string.Empty;
                            strXML.Append("<Employee>");
                            strXML.Append("<EL_EMP_ID>" + DtLVCutUpload.Rows[i]["EL_EMP_ID"].ToString() + "</EL_EMP_ID>");
                            strXML.Append("<EL_REASON>" + DtLVCutUpload.Rows[i]["EL_REASON"].ToString() + "</EL_REASON>");
                            strXML.Append("<EL_RETIREMENT_DATE>" + DtLVCutUpload.Rows[i]["EL_RETIREMENT_DATE"].ToString() + "</EL_RETIREMENT_DATE>");
                            strXML.Append("</Employee>");
                           // updateLeavecut(DtLVCutUpload.Rows[i]["EL_EMP_ID"].ToString(), Convert.ToDateTime(DtLVCutUpload.Rows[i]["EL_RETIREMENT_DATE"].ToString()), 0, DtLVCutUpload.Rows[i]["EL_REASON"].ToString(), ref strErrorMsg);
                            if (strErrorMsg.Trim().Length > 1)
                            {
                                lblMessages.Text = strErrorMsg;
                                lblMessages.Visible = true;
                                return;
                            }
                            else
                                lblMessages.Text = string.Empty;
                        }

                    }
                    strXML.Append("</Employees>");

                    if (dtLVCutUploadError.Rows.Count > 0)
                    {
                        ShowReport();
                        ReportViewer1.Visible = true;
                        lblMessages.Text = "Error report generated, please rectify and upload again.";
                        lblMessages.Visible = true;
                        bindDataGrid();
                    }
                    else
                    {
                        if (int_cnt > 1)
                        {
                            clsEmployeeLeft objEmp=new clsEmployeeLeft();
                            objEmp.CreatedBy=Session["uid"].ToString();
                            clsEmployeeLeftHandler.LetfEmployees(objEmp, strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                            if (strErrMsg.Trim().Length >= 1)
                            {
                                ReportViewer1.Visible = false;
                                lblMessages.Text = strErrMsg;
                                lblMessages.Visible = true;
                                return;
                            }
                            else
                            {
                                ReportViewer1.Visible = false;
                                lblMessages.Text = strSuccMsg;
                                lblMessages.Visible = true;
                                bindDataGrid();
                            }
                        }

                      
                    }
                }
                catch (Exception ex)
                {
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                }
            }
        }       

        //RETRIEVE EXCEL SHEETNAMES111
        private String[] GetExcelSheetNames(string excelFile, string path)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Connection String. Change the excel file to the file you will search.
                String connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;;Data Source={0};Extended Properties=Excel 8.0", path);

                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);

                // Open connection with the database.
                objConn.Open();

                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                return excelSheets;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                return null;
            }
        }
        private void ShowReport()
        {
            try
            {
                ReportViewer1.LocalReport.ReportPath = "RDLC\\Employee_Left.rdlc";
                ReportViewer1.Visible = true;

                String commandText = "SELECT [EL_ColumnID],[EL_EMP_ID],[EL_LEFT_DATE],[EL_ISDELETED],[EL_DELETEDDATE] FROM [ENT_EMPLOYEE_LEFT] ";

                String commandType = "Text";

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME

                DataSet dsLvcut = new DataSet();

                dsLvcut.Tables.Add(dtLVCutUploadError);

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dsLvcut.Tables[0];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
            }
        }
        private void FillEmployeeEntity()
        {
            try
            {
                DataTable dt = clsEmployeeLeftHandler.EmpLeftEntities("EMP");

                lstEmployees.DataValueField = "ID";
                lstEmployees.DataTextField = "NAME";
                lstEmployees.DataSource = dt;
                lstEmployees.DataBind();
                lstEmployees.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private void FillLeaveCodeEntity()
        {

            DataTable dt = clsEmployeeLeftHandler.EmpLeftEntities("Reasons");

            ddlReasonAdd.DataSource = dt;
            ddlReasonAdd.DataTextField = "Reason_Description";
            ddlReasonAdd.DataValueField = "Reason_ID";
            ddlReasonAdd.DataBind();
            ddlReasonAdd.Items.Insert(0, "Select");

            ddlReasonEdit.DataSource = dt;
            ddlReasonEdit.DataTextField = "Reason_Description";
            ddlReasonEdit.DataValueField = "Reason_ID";
            ddlReasonEdit.DataBind();
        }
        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string XlsPath = Server.MapPath(@"~/Uploaded Folder/upload_Employee_Left_format.xls");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(fileDet.FullName);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        private void ResellAll_NewEntry()
        {
            FillEmployeeEntity();
            FillLeaveCodeEntity();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

    }

}