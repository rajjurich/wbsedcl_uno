using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
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
using System.Reflection;
using Microsoft.Reporting.WebForms;
using CMS.UNO.Core.Handler;

namespace UNO
{
    public partial class HierarchyManagerView : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string HierDeletedDate;
        DataSet dsLVCutUpload;
        DataTable DtLVCutUpload;
        static DataTable dtLVCutUploadError;
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtCompanyId.Focus();
                if (!Page.IsPostBack)
                {
                    bindDataGrid();
                    FillEmployeeEntity();

                    btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvEmpMgrHier.ClientID + "');");
                }

                HierDeletedDate = DateTime.Now.ToString("dd/MM/yyyy");

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchy");

            }
        }
        private void FillEmployeeEntity()
        {
            try
            {
                DataSet ds = clsCommonHandler.GetEmployeesDetails("AEMP");
                lstManager.DataValueField = "ID";
                lstManager.DataTextField = "NAME";
                lstManager.DataSource = ds.Tables[0];
                lstManager.DataBind();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Remove("AppMode");
            Response.Redirect("HierarchyManagerLinkAdd.aspx");


        }
        public void bindDataGrid()
        {

            try
            {

                clsEmployeeHierarchy objData = new clsEmployeeHierarchy();
                DataTable dt = clsEmployeeHierarchyHandler.GetEmployeeManagerDetails("MngrHierarchy", objData);

                gvEmpMgrHier.DataSource = dt;
                gvEmpMgrHier.DataBind();

                DropDownList ddl = (DropDownList)gvEmpMgrHier.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmpMgrHier.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmpMgrHier.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmpMgrHier.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmpMgrHier.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmpMgrHier.PageCount == 0)
                {
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrHier.PageIndex + 1 == gvEmpMgrHier.PageCount)
                {
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrHier.PageIndex == 0)
                {
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }


                ((Label)gvEmpMgrHier.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpMgrHier.PageSize * gvEmpMgrHier.PageIndex) + 1) + " to " + (((gvEmpMgrHier.PageSize * (gvEmpMgrHier.PageIndex + 1)) - 10) + gvEmpMgrHier.Rows.Count);

                gvEmpMgrHier.BottomPagerRow.Visible = true;

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
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddpagesize = sender as DropDownList;
                gvEmpMgrHier.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
                ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
                bindDataGrid();
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        public void delete()
        {
            try
            {
                StringBuilder strXML = new StringBuilder();

                clsEmployeeHierarchy objHierarchy = new clsEmployeeHierarchy();
                objHierarchy.CreatedBy = Session["uid"].ToString();
                strXML.Append("<ENT_HierarchyDef>");

                for (int i = 0; i <= gvEmpMgrHier.Rows.Count - 1; i++)
                {
                    CheckBox chk = (CheckBox)gvEmpMgrHier.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked)
                    {
                        strXML.Append("<Hierarchy>");
                        strXML.Append("<Hier_Emp_ID>" + gvEmpMgrHier.Rows[i].Cells[2].Text.Trim() + "</Hier_Emp_ID>");
                        strXML.Append("</Hierarchy>");

                    }


                }
                strXML.Append("</ENT_HierarchyDef>");

                clsEmployeeHierarchyHandler.UpdateEmployeeManagerDetails(objHierarchy, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lbltext.Text = strErrMsg;
                    lbltext.Visible = true;
                }
                else
                {
                    lbltext.Text = strSuccMsg;
                    lbltext.Visible = true;
                    bindDataGrid();
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblErrorEdit.Text = string.Empty;
                clsEmployeeHierarchy objData = new clsEmployeeHierarchy();
                DataTable dt = clsEmployeeHierarchyHandler.GetEmployeeManagerDetails("MngrHierarchy", objData);
                if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
                {
                    gvEmpMgrHier.DataSource = dt;
                    gvEmpMgrHier.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"empid~" +txtCompanyId.Text.Trim(), "S" },
				{"EmpName~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEmpMgrHier.DataSource = _tempDT;
                    gvEmpMgrHier.DataBind();
                }

                DropDownList ddl = (DropDownList)gvEmpMgrHier.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmpMgrHier.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmpMgrHier.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmpMgrHier.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmpMgrHier.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmpMgrHier.PageCount == 0)
                {
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrHier.PageIndex + 1 == gvEmpMgrHier.PageCount)
                {
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrHier.PageIndex == 0)
                {
                    ((Button)gvEmpMgrHier.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }


                ((Label)gvEmpMgrHier.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpMgrHier.PageSize * gvEmpMgrHier.PageIndex) + 1) + " to " + (((gvEmpMgrHier.PageSize * (gvEmpMgrHier.PageIndex + 1)) - 10) + gvEmpMgrHier.Rows.Count);

                gvEmpMgrHier.BottomPagerRow.Visible = true;

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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchy");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrHier.PageIndex = Convert.ToInt32(((DropDownList)gvEmpMgrHier.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchy");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrHier.PageIndex = gvEmpMgrHier.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchy");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrHier.PageIndex = gvEmpMgrHier.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchy");
            }
        }
        protected void gvEmpMgrHier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Modify")
                {
                    lblError.Text = "";
                    string rowID = e.CommandArgument.ToString();
                    ViewState["rowID"] = rowID;
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    string empid = row.Cells[2].Text;
                    string empName = row.Cells[3].Text;
                    txtEmpID.Text = empid;
                    txtEmployeeName.Text = empName;
                    txtEmployeeName.Text = row.Cells[3].Text + " " + row.Cells[2].Text;
                    lstManager.SelectedValue = row.Cells[4].Text.Trim();
                    txtManagerName.Text = row.Cells[5].Text + " " + row.Cells[4].Text;
                    mpeEmpMgrHierarchy.Show();
                    ScriptManager.RegisterClientScriptBlock(up1, up1.GetType(), "Script", "validateChosen();", true);
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchy");
            }
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsEmployeeHierarchy objHierarchy = new clsEmployeeHierarchy();
                objHierarchy.CreatedBy = Session["uid"].ToString();
                objHierarchy.MngrID = lstManager.SelectedValue;
                objHierarchy.RowID = ViewState["rowID"].ToString();
                objHierarchy.EmpID = txtEmpID.Text;
                clsEmployeeHierarchyHandler.UpdateEmployeeManagerDetails(objHierarchy, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                if (strErrMsg.Trim().Length > 0)
                {
                    lblError.Text = strErrMsg;
                    mpeEmpMgrHierarchy.Show();
                    return;
                }
                else
                {
                    mpeEmpMgrHierarchy.Hide();
                    bindDataGrid();
                    lblErrorEdit.Text = strSuccMsg;
                }

            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
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
                return null;
            }
        }
        void InsertEmployeeHierarchy(string strXML)
        {
            clsEmployeeHierarchy objHier = new clsEmployeeHierarchy();
            objHier.CreatedBy = Session["uid"].ToString();
            clsEmployeeHierarchyHandler.UploadHierarchy(objHier, strXML, ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

        }
        protected void btnImportLeaveCut_Click(object sender, EventArgs e)
        {

            if (fileUpload.HasFile)
            {

                try
                {
                    StringBuilder strXML = new StringBuilder();

                    string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                    string file = Path.GetFileNameWithoutExtension(fileUpload.FileName).ToLower();
                    string path = string.Concat(Server.MapPath("~/Uploaded Folder/" + fileUpload.FileName));
                    string excelConnectionString = "";
                    fileUpload.SaveAs(path);

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
                        lblUploadCutError.Text = "Please upload valid file.";
                        lblUploadCutError.Visible = true;
                        mpeUpload.Show();
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

                    DataTable dt1 = dsLVCutUpload.Tables[0];
                    int i;
                    dtLVCutUploadError = new DataTable();
                    dtLVCutUploadError.Columns.Add("Hier_Emp_ID", typeof(string));
                    dtLVCutUploadError.Columns.Add("Hier_Mgr_ID", typeof(string));

                    strXML.Append("<ENT_HierarchyDef>");
                    int chkcnt = 0;

                    for (i = 0; i < DtLVCutUpload.Rows.Count; i++)
                    {
                        int count = 0;
                        DataRow row = dtLVCutUploadError.NewRow();
                        dtLVCutUploadError.Rows.Add(row);
                        try
                        {
                            if (DtLVCutUpload.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Emp_ID"].ToString() != ""
                                && DtLVCutUpload.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Mgr_ID"].ToString() != "")
                            {
                                count = count + 1;
                            }

                        }
                        catch (Exception ex)
                        {


                        }

                        if (DtLVCutUpload.Rows[i]["Hier_Emp_ID"].ToString() == "" || DtLVCutUpload.Rows[i]["Hier_Mgr_ID"].ToString() == "")
                        {
                            dtLVCutUploadError.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Emp_ID"] = dt1.Rows[i]["Hier_Emp_ID"];
                            dtLVCutUploadError.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Mgr_ID"] = dt1.Rows[i]["Hier_Mgr_ID"];

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
                        objcmd.CommandText = "SELECT COUNT(*) FROM ENT_EMPLOYEE_PERSONAL_DTLS  WHERE EPD_EMPID ='" + DtLVCutUpload.Rows[i]["Hier_Emp_ID"].ToString() + "' and EPD_ISDELETED ='false' ";
                        int cnt = Convert.ToInt32(objcmd.ExecuteScalar());
                        if (cnt == 0)
                        {
                            dtLVCutUploadError.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Emp_ID"] = dt1.Rows[i]["Hier_Emp_ID"];
                            dtLVCutUploadError.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Mgr_ID"] = dt1.Rows[i]["Hier_Mgr_ID"];


                        }
                        else
                        {
                            count = count + 1;
                        }


                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlCommand objcmd1 = new SqlCommand();
                        objcmd1.Connection = conn;
                        objcmd1.CommandText = "SELECT COUNT(*) FROM ENT_EMPLOYEE_PERSONAL_DTLS  WHERE EPD_EMPID ='" + DtLVCutUpload.Rows[i]["Hier_Mgr_ID"].ToString() + "' and EPD_ISDELETED ='false' ";
                        int cnt1 = Convert.ToInt32(objcmd.ExecuteScalar());
                        if (cnt1 == 0)
                        {

                            dtLVCutUploadError.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Emp_ID"] = dt1.Rows[i]["Hier_Emp_ID"];
                            dtLVCutUploadError.Rows[dtLVCutUploadError.Rows.Count - 1]["Hier_Mgr_ID"] = dt1.Rows[i]["Hier_Mgr_ID"];

                        }
                        else
                        {
                            count = count + 1;
                        }

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }

                        if (count == 4)
                        {
                            dtLVCutUploadError.Rows.RemoveAt(dtLVCutUploadError.Rows.Count - 1);
                            strXML.Append("<Hierarchy>");
                            strXML.Append("<Hier_Emp_ID>" + DtLVCutUpload.Rows[i]["Hier_Emp_ID"].ToString() + "</Hier_Emp_ID>");
                            strXML.Append("<Hier_Mgr_ID>" + DtLVCutUpload.Rows[i]["Hier_Mgr_ID"].ToString() + "</Hier_Mgr_ID>");
                            // InsertEmployeeHierarchy(DtLVCutUpload.Rows[i]["Hier_Emp_ID"].ToString(), DtLVCutUpload.Rows[i]["Hier_Mgr_ID"].ToString());
                            strXML.Append("</Hierarchy>");
                            chkcnt++;
                        }

                    }
                    strXML.Append("</ENT_HierarchyDef>");

                    if (chkcnt > 0)
                    {
                        InsertEmployeeHierarchy(strXML.ToString());
                    }

                    if (dtLVCutUploadError.Rows.Count > 0)
                    {
                        ShowReport();
                        ReportViewer1.Visible = true;
                        lbltext.Text = "Error report generated, please rectify and upload again.";
                        lbltext.Visible = true;
                        bindDataGrid();

                    }
                    else
                    {
                        ReportViewer1.Visible = false;
                        lbltext.Text = "Record(s) saved sucessfully.";
                        lbltext.Visible = true;
                        bindDataGrid();

                    }



                }
                catch (Exception ex)
                {

                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HierarchyManagerView.aspx");

                }

            }
            else
            {
                lblUploadCutError.Text = "No file selected.";
                lblUploadCutError.Visible = true;
                mpeUpload.Show();
                return;
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        protected void btnCancelLeaveCut_Click(object sender, EventArgs e)
        {
            mpeUpload.Hide();
            lblUploadCutError.Text = "";
            lblUploadCutError.Visible = false;

        }
        private void ShowReport()
        {
            try
            {

                ReportViewer1.LocalReport.ReportPath = "RDLC\\EmpHierReport.rdlc";
                ReportViewer1.Visible = true;
                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                DataSet dsLvcut = new DataSet();
                dsLvcut.Tables.Add(dtLVCutUploadError.Copy());
                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dsLvcut.Tables[0];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            mpeUpload.Show();
            lblUploadCutError.Text = "";
            lblUploadCutError.Visible = false;

        }
        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string XlsPath = Server.MapPath(@"~/Uploaded Folder/uploadEmployeeHierarchyformat.xls");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(fileDet.FullName);
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }


    }
}
