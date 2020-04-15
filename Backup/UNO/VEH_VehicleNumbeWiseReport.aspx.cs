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
    public partial class VEH_VehicleNumbeWiseReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // ShowReport();
                FillEmployeeEntity();
            }
            
          //  Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script type='text/javascript' language='javascript'>CacheValues();</script>");
            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");
        }
        private void FillEmployeeEntity()
        {

            string strSql = "";

            strSql = "SELECT VehicleRegistrationNumber FROM [dbo].[veh_vehicleEntries]";
            //strSql = " select Left(EPD_FIRST_NAME + space(28),30) + epd_empid as Name ,epd_empid as ID from ENT_EMPLOYEE_PERSONAL_DTLS where epd_isdeleted='0' ";


            //strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+ oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0'";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(strSql, conn);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            LstEmployee.DataTextField = "VehicleRegistrationNumber";

            LstEmployee.DataSource = thisDataSet.Tables[0];

            LstEmployee.DataBind();


        }
        private void ShowReport()
        {
            try
            {

                ReportViewer1.LocalReport.ReportPath = "RDLC\\VEH_VehicleNumberWiseReport.rdlc";
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.DataSources.Clear();

                String commandText = "  SELECT * FROM [dbo].[veh_vehicleEntries]";

                string strCondition = "", strSort = "";


                //strCondition = " ENT_EMPLOYEE_PERSONAL_DTLS.EPD_ISDELETED=0 ";

                string strVehicleId = "";

                int uu = LstEmployee.Items.Count;
                if(LstEntitySelected.Items.Count > 0 )
                {  
                    for(int iSelectedID=0;iSelectedID < LstEntitySelected.Items.Count;iSelectedID++)
                    {
                        strVehicleId = strVehicleId + (strVehicleId.Length > 0 ? "," : "") +"'"+ LstEntitySelected.Items[iSelectedID].Value.ToString()+"'";
                    }
                }
                if (strVehicleId.Length > 0)
                {
                    strCondition = " WHERE VehicleRegistrationNumber in ( " + strVehicleId + ")";
                }
                 commandText = commandText + strCondition ;
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (LstEntitySelected.Items.Count == 0)
            {
                lblError.Text = "Please Select Record";
            }
            else
            {
                ShowReport();
                lblError.Text = "";
            }
            
        }

        protected void btnMoveAll_Click(object sender, EventArgs e)
        {
            while (LstEmployee.Items.Count != 0)
            {
                for (int i = 0; i < LstEmployee.Items.Count; i++)
                {
                    LstEntitySelected.Items.Add(LstEmployee.Items[i]);
                    LstEmployee.Items.Remove(LstEmployee.Items[i]);
                }
            }

        }

        protected void btnMoveSingle_Click(object sender, EventArgs e)
        {
            ArrayList arraylist1 = new ArrayList();
            ArrayList arraylist2 = new ArrayList();

            if (LstEmployee.SelectedIndex >= 0)
            {
                for (int i = 0; i < LstEmployee.Items.Count; i++)
                {
                    if (LstEmployee.Items[i].Selected)
                    {
                        if (!arraylist1.Contains(LstEmployee.Items[i]))
                        {
                            arraylist1.Add(LstEmployee.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    if (!LstEntitySelected.Items.Contains(((ListItem)arraylist1[i])))
                    {
                        LstEntitySelected.Items.Add(((ListItem)arraylist1[i]));
                    }
                    LstEmployee.Items.Remove(((ListItem)arraylist1[i]));
                }
                LstEntitySelected.SelectedIndex = -1;
            }
        }

        protected void btnReturnSingle_Click(object sender, EventArgs e)
        {
            ArrayList arraylist1 = new ArrayList();
            ArrayList arraylist2 = new ArrayList();
            if (LstEntitySelected.SelectedIndex >= 0)
            {
                for (int i = 0; i < LstEntitySelected.Items.Count; i++)
                {
                    if (LstEntitySelected.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(LstEntitySelected.Items[i]))
                        {
                            arraylist2.Add(LstEntitySelected.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!LstEmployee.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        LstEmployee.Items.Add(((ListItem)arraylist2[i]));
                    }
                    LstEntitySelected.Items.Remove(((ListItem)arraylist2[i]));
                }
                LstEmployee.SelectedIndex = -1;
            }
        }

        protected void btnReturnAll_Click(object sender, EventArgs e)
        {
            while (LstEntitySelected.Items.Count != 0)
            {
                for (int i = 0; i < LstEntitySelected.Items.Count; i++)
                {
                    LstEmployee.Items.Add(LstEntitySelected.Items[i]);
                    LstEntitySelected.Items.Remove(LstEntitySelected.Items[i]);
                }
            }
        }
    }
}