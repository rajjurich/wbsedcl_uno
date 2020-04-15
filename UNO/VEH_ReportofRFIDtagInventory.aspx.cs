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
using System.Collections;

namespace UNO
{
    public partial class VEH_ReportofRFIDtagInventory : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               ShowReport();
               // FillEmployeeEntity();
            }
           // Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");
        }
        //private void FillEmployeeEntity()
        //{

        //    string strSql = "";

        //    strSql = "SELECT  epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME )) + epd_empid,' ',' ')   as NAME FROM [UNO_DEC].[dbo].[ENT_EMPLOYEE_PERSONAL_DTLS]  where epd_isdeleted='0'";
        //    //strSql = " select Left(EPD_FIRST_NAME + space(28),30) + epd_empid as Name ,epd_empid as ID from ENT_EMPLOYEE_PERSONAL_DTLS where epd_isdeleted='0' ";


        //    //strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+ oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0'";

        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    SqlCommand cmd = new SqlCommand(strSql, conn);

        //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    DataSet thisDataSet = new DataSet();
        //    adpt.Fill(thisDataSet);

        //    LstEmployee.DataValueField = "ID";
        //    LstEmployee.DataTextField = "NAME";

        //    LstEmployee.DataSource = thisDataSet.Tables[0];

        //    LstEmployee.DataBind();


        //}
        private void ShowReport()
        {
            try
            {

                ReportViewer1.LocalReport.ReportPath = "RDLC\\VEH_RFIDtagInventoryReport.rdlc";
                ReportViewer1.Visible = true;

                String commandText = "select * from [dbo].[Veh_RFIDstock]";

                //String commandText = "select * from [dbo].[Veh_RFIDstock] where TagStatus='" + ddlStatus.SelectedValue.ToString()+"'";

                string strCondition = "", strSort = "";


                //strCondition = " ENT_EMPLOYEE_PERSONAL_DTLS.EPD_ISDELETED=0 ";

                string strVehicleId = "";

                //int uu = LstEmployee.Items.Count;
                //if(LstEntitySelected.Items.Count > 0 )
                //{  
                //    for(int iSelectedID=0;iSelectedID < LstEntitySelected.Items.Count;iSelectedID++)
                //    {
                //        strVehicleId = strVehicleId + (strVehicleId.Length > 0 ? "," : "") +"'"+ LstEntitySelected.Items[iSelectedID].Value.ToString()+"'";
                //    }
                    
                //}


                //if (strVehicleId.Length > 0)
                //{
                //    strCondition = " WHERE VehicleRegistrationNumber in ( " + strVehicleId + ")";
                //}
                               

                //commandText = commandText + strCondition ;



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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowReport();
        }



    }
}