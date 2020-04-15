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
    public partial class User_Master_Report : System.Web.UI.Page
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

                ReportViewer1.LocalReport.ReportPath = "RDLC\\User.rdlc";
                ReportViewer1.Visible = true;




                String commandText = "";
                commandText = " SELECT '' AS SR_NO,";
                commandText+=" EPD_FIRST_NAME +' '+EPD_MIDDLE_NAME+' '+EPD_LAST_NAME AS NAME,";
                commandText+=" UserID,LevelID,ACS_ACCESSLEVEL.AL_DESCRIPTION";

                commandText+=" FROM ENT_User";
                commandText += " INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS ON ENT_User.EmployeeID= ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID";
                commandText += " INNER JOIN ACS_ACCESSLEVEL ON ENT_User.LevelID= ACS_ACCESSLEVEL.AL_ID";


                String commandType = "Text";

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME


                DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);

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