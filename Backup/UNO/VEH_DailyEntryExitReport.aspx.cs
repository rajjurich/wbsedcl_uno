using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace UNO.UNO_Log
{
    public partial class VEH_DailyEntryExitReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            ShowReport();
            }
        }
        private void ShowReport()
        {
            try
            {

                ReportViewer1.LocalReport.ReportPath = "RDLC\\VEH_EntryExitReport.rdlc";
                ReportViewer1.Visible = true;

             

                String commandText = "  select * from [dbo].[veh_vehicleEntries]";
           
                string strCondition = "", strSort = "";


                //strCondition = " ENT_EMPLOYEE_PERSONAL_DTLS.EPD_ISDELETED=0 ";

                //if (RadioButtonList1.SelectedIndex == 1)
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " TDAY_EMPCDE in " + this.EmployeeHdn.Value;
                //}

                //if (RadioButtonList2.SelectedIndex == 1)
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " COMPANY_ID in " + this.ComapnyHdn.Value;
                //}

                //if (RadioButtonList3.SelectedIndex == 1)
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " LOC.OCE_ID in " + this.LocationHdn.Value;
                //}

                //if (RadioButtonList4.SelectedIndex == 1)
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " DIV.OCE_ID in " + this.DivisionHdn.Value;
                //}

                //if (RadioButtonList5.SelectedIndex == 1)
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " DEP.OCE_ID in " + this.DepartmentHdn.Value;
                //}

                //if (RadioButtonList6.SelectedIndex == 1)
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " TDAY_SFTREPO in " + this.ShiftHdn.Value;
                //}



                //if (strDate != "")
                //{
                //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " CONVERT(DATETIME,CONVERT(VARCHAR(50),TDAY_DATE,103),103) between  CONVERT(DATETIME, '" + strDate + "', 103)  AND CONVERT(DATETIME, '" + strToDate + "', 103)";

                //}



                //if (strCondition.Length > 0)
                //{
                //    strCondition = " WHERE " + strCondition;
                //}

                //if (TypeCheckBox.Checked == true)
                //{
                //    strSort = " ORDER BY TDAY_EMPCDE,TDAY_DATE ";
                //}


                commandText = commandText + strCondition + strSort;



                String commandType = "Text";


                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME




                DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);



                //string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for " + strDate + " To" + strToDate;
                //ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
                //ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });




                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = thisDataSet.Tables[tableName];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();

            }

            catch (Exception ex)
            {

            }
        }
        private DataSet ExecuteQuery(string strQuery, string dataSetName, string tableName)
        {
            DataSet ds = new DataSet();

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = strQuery;
                DataSet dataSet = new DataSet(dataSetName);
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dataSet, tableName);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return dataSet;

            }

            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleDailyEntryExitReport");
            }
            return ds;
        }

    }
}