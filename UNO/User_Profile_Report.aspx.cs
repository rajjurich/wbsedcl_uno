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
    public partial class User_Profile_Report : System.Web.UI.Page
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

                ReportViewer1.LocalReport.ReportPath = "RDLC\\User_Profile.rdlc";
                ReportViewer1.Visible = true;




                String commandText = "";
                commandText = " SELECT  '' AS SR_NO,";
                commandText +=" EPD_CARD_ID,EPD_EMPID, 	EPD_FIRST_NAME +' '+EPD_LAST_NAME AS NAME,";
                commandText +=" DEP.OCE_ID AS DEP_ID,DEP.OCE_DESCRIPTION AS DEP_DESC,DESG.OCE_ID AS DESG_ID,";
                commandText +=" DESG.OCE_DESCRIPTION AS DESG_DESC ";
                
                commandText +=" FROM ENT_EMPLOYEE_PERSONAL_DTLS";
                commandText +=" INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_EMPID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID";
                commandText += " INNER JOIN ENT_ORG_COMMON_ENTITIES AS DEP ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_DEPARTMENT_ID=DEP.OCE_ID AND DEP.CEM_ENTITY_ID='DEP' ";
                commandText +=" INNER JOIN ENT_ORG_COMMON_ENTITIES AS DESG ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_DESIGNATION_ID=DESG.OCE_ID AND DESG.CEM_ENTITY_ID='DES'";//QUERY TO EXECUTE

                String commandType = "Text";

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME



                string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";

                ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
                ReportParameter para2 = new ReportParameter("user", Session["uid"].ToString());
                DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);

                ReportParameter para3 = new ReportParameter("count", thisDataSet.Tables[0].Rows.Count.ToString());
                DataTable dtReport = clsCommonHandler.GetReportHeader();
                ReportParameter header = new ReportParameter("header", dtReport.Rows[0]["value"].ToString());
                ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, para2, para3,header,address });

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