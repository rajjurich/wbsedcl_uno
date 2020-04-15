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

namespace UNO
{
    public partial class Reader_Access_Point_Report : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                ShowReport();
                ReportViewer1.Visible = true;
            }
        }

        private void ShowReport()
        {
            try
            {

                ReportViewer1.LocalReport.ReportPath = "RDLC\\Reader_Access_Point.rdlc";
                ReportViewer1.Visible = true;




                String commandText = "";
                commandText += " SELECT '' AS SR_NO, ACS_READER.READER_ID, READER_DESCRIPTION,";
                commandText += " ACS_READER.CTLR_ID, ACS_CONTROLLER.CTLR_DESCRIPTION ";

                commandText += ",ACS_ACCESSPOINT.AP_ID ,ACS_ACCESSPOINT.AP_DESCRIPTION ";
                commandText += " FROM ACS_READER";
                commandText += " INNER JOIN ACS_CONTROLLER ON ACS_READER.CTLR_ID=ACS_CONTROLLER.CTLR_ID";

                commandText += " INNER JOIN ACS_ACCESSPOINT_RELATION ON ACS_READER.READER_ID=ACS_ACCESSPOINT_RELATION.READER_ID ";
                commandText += " AND ACS_READER.CTLR_ID=ACS_ACCESSPOINT_RELATION.AP_CONTROLLER_ID";
                commandText += " INNER JOIN ACS_ACCESSPOINT ON ACS_ACCESSPOINT_RELATION.AP_ID =ACS_ACCESSPOINT.AP_ID ";


               
                String commandType = "Text";

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME
                DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);

                string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";
               
                ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
                ReportParameter para3 = new ReportParameter("count", thisDataSet.Tables[0].Rows.Count.ToString());

                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, para3 });


               
                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = thisDataSet.Tables[tableName];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }
            catch(Exception ex)
            {

            }
        }

        private DataSet ExecuteQuery(string strQuery, string dataSetName, string tableName)
        {

            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = m_connectons;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = strQuery;
                DataSet dataSet = new DataSet(dataSetName);
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dataSet, tableName);
                return dataSet;
            }

            catch (Exception ex)
            {
                return null;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        protected void Close_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessDashboard.aspx");
        }

    }
}