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
    public partial class Controller_Master : System.Web.UI.Page
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
                ReportViewer1.LocalReport.ReportPath = "RDLC\\Controllers.rdlc";
                ReportViewer1.Visible = true;

                String commandText = "";
                commandText += "  SELECT '' AS SR_NO, ";
                commandText += "  CTLR_ID, 	CTLR_DESCRIPTION,CTLR_IP,CTLR_TYPE,	ENT_PARAMS.VALUE";
                
                commandText += "  FROM ACS_CONTROLLER ";
                commandText += "  INNER JOIN  ENT_PARAMS ON ACS_CONTROLLER.CTLR_TYPE=ENT_PARAMS.CODE";
                commandText += "  AND  IDENTIFIER='CONTROLLERTYPE'";
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
            return null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        protected void Close_Click(object sender, EventArgs e)
        {
            Response.Redirect("Uno_Dashboard.aspx");
        }

    }
}