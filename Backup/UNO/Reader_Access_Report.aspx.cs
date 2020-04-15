using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

using Microsoft.Reporting.WebForms;

namespace MMWebReports
{
    public partial class Reader_Access_Report : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "",strToDate="";
        void Page_PreInit(Object sender, EventArgs e)
        {
            this.Title = "Reader_Access_Report.aspx";
        }
        String m_messageString = "";
        String m_role = "";
        //Common common = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login_username"] == null)
            {
                //m_role = roleCookie.Value;
                //Session.Clear();
                //Response.Redirect("../../BaseModule/UI/SessionExpire.htm");
            }
            //checkPageAuthorization();
            if (!IsPostBack)
            {



            
                fillEmplist();
                FillControllerDrp();
                FillReaderDrp();
                Filldivisiondrp();
                Filldepartmentdrp();
                Fillshiftdrp();
                //initializeControls();
                //string someScript1 = "";
                //someScript1 = "<script language='javascript'>HideExcel_Export_Button()</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript1);

            }

            else
            {
                if (Page.Request.Form["From_Date"] != null)
                {
                    strDate = Page.Request.Form["From_Date"].ToString();
                    strToDate = Page.Request.Form["To_Date"].ToString();
                }
            }

           // fillEmplist();
            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
            if (userAgent.Contains("MSIE 7.0"))
                //ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");
                DateHdn.Value = DateTime.Now.ToString("dd/MM/yyyy");

           // this.From_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //ManageLinkVisibility();

        }
        public void fillEmplist()
        {
            string strsql;
            strsql = "SELECT EPD_EMPID ,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as EPD_FIRST_NAME " +
                     " FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  "+
                     " on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1' ";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strsql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox1.DataValueField = "EPD_EMPID";
            ListBox1.DataTextField = "EPD_FIRST_NAME";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();
  
        }



        private void FillControllerDrp()
        {





            string strSql = " SELECT CTLR_ID ,replace(convert(char(31),ltrim(CTLR_DESCRIPTION))+Convert(varchar(100),CTLR_ID),' ',' ' ) as CTLR_DESCRIPTION  FROM ACS_CONTROLLER where CTLR_ISDELETED='0' ";

            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox2.DataValueField = "CTLR_ID";
            ListBox2.DataTextField = "CTLR_DESCRIPTION";
            ListBox2.DataSource = thisDataSet.Tables[0];

            ListBox2.DataBind();



            //ddlcompany.SelectedValue = null;


        }

        private void FillCompanydrp()
        {



            

            string strSql = "SELECT COMPANY_ID,replace(convert(char(31),ltrim(COMPANY_NAME  ))+COMPANY_ID,' ',' ' ) as COMPANYNAME FROM ENT_COMPANY where COMPANY_ISDELETED='0'";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox2.DataValueField = "COMPANY_ID";
            ListBox2.DataTextField = "COMPANYNAME";
            ListBox2.DataSource = thisDataSet.Tables[0];

            ListBox2.DataBind();


          
            //ddlcompany.SelectedValue = null;
           

        }

        private void FillReaderDrp()
        {
            string strSql = "SELECT READER_ID ,replace(convert(char(31),ltrim(READER_DESCRIPTION))+Convert(varchar(100),READER_ID)+'('+Convert(varchar(100),CTLR_ID)+')',' ',' ' ) as READER_DESCRIPTION ,Convert(varchar(100),READER_ID)+'|'+Convert(varchar(100),CTLR_ID)as CompleteDesc   FROM ACS_READER where READER_ISDELETED='0'";

            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            //CHANGE TO FETCH CONTROLLER ID
            ListBox3.DataValueField = "READER_ID";
            ListBox3.DataTextField = "READER_DESCRIPTION";

            //CHANGE TO FETCH CONTROLLER ID

            ListBox3.DataSource = thisDataSet.Tables[0];
            ListBox3.DataBind();

        }


        private void Filllocationdrp()
        {
            string strSql = "select OCE_ID,replace(convert(char(31),ltrim(OCE_DESCRIPTION  ))+OCE_ID,' ',' ' )  as LOCATIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='LOC'";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox3.DataValueField = "oce_id";
            ListBox3.DataTextField = "LOCATIONNAME";
            ListBox3.DataSource = thisDataSet.Tables[0];
            ListBox3.DataBind();
          

        }
        private void Filldivisiondrp()
        {
            string strSql = "select OCE_ID,replace(convert(char(31),ltrim(OCE_DESCRIPTION  ))+OCE_ID,' ',' ' )  as DIVISIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='DIV'";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox4.DataValueField = "oce_id";
            ListBox4.DataTextField = "DIVISIONNAME";
            ListBox4.DataSource = thisDataSet.Tables[0];
            ListBox4.DataBind();
           
        }

        private void Filldepartmentdrp()
        {
            string strSql = "select OCE_ID, replace(convert(char(31),ltrim(OCE_DESCRIPTION  ))+OCE_ID,' ',' ' )  as DEPARTMENTNAME  from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='DEP'";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox5.DataValueField = "oce_id";
            ListBox5.DataTextField = "DEPARTMENTNAME";
            ListBox5.DataSource = thisDataSet.Tables[0];
            ListBox5.DataBind();
           

        }
        private void Fillshiftdrp()
        {
            string strSql = "select SHIFT_ID,replace(convert(char(31),ltrim(SHIFT_ID))+SHIFT_DESCRIPTION,' ',' ' ) as  SHIFTNAME from ta_shift  where SHIFT_ISDELETED='0'";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox6.DataValueField = "SHIFT_ID";
            ListBox6.DataTextField = "SHIFTNAME";
            ListBox6.DataSource = thisDataSet.Tables[0];
            ListBox6.DataBind();
        

        }

        void initializeControls()
        {
            fillEmplist();
        }

        protected void View_Click(object sender, EventArgs e)
        {
            btnClose.Visible = true;
            ReportViewer1.Visible = true;
           ShowReport();
         }



        private void ShowReport()
        {
            try 
            {
                String commandText = "",strOrder="";

                if(RadioButtonList4.SelectedIndex==0)
                {
                    ReportViewer1.LocalReport.ReportPath = "RDLC\\Reader_Acess.rdlc";
                    strOrder = " ORDER BY ACS_ACCESSLEVEL_RELATION.CONTROLLER_ID, RD_ZN_ID,EMPLOYEE_CODE";
                }
                else if (RadioButtonList4.SelectedIndex == 1)
                {
                    ReportViewer1.LocalReport.ReportPath = "RDLC\\EmployeeWise_Acess.rdlc";
                    strOrder = " ORDER BY EMPLOYEE_CODE,CONTROLLER_ID,RD_ZN_ID";
                }
              
                ReportViewer1.Visible = true;



            

               
                commandText =  " SELECT	EMPLOYEE_CODE,";
                commandText += " ENT_EMPLOYEE_PERSONAL_DTLS.EPD_FIRST_NAME +' '+ENT_EMPLOYEE_PERSONAL_DTLS.EPD_LAST_NAME AS NAME,";
                commandText += " DEP.OCE_ID AS DEP_ID, DEP.OCE_DESCRIPTION AS DEP_DESC, DESG.OCE_ID AS DESG_ID,";
                commandText += " DESG.OCE_DESCRIPTION AS DESG_DESC ";

                //commandText += " ,ACS_TIMEZONE.TZ_ID";
                //commandText += " ,ACS_TIMEZONE.TZ_DESCRIPTION";

                commandText += " ,ACS_CARD_CONFIG.ACTIVATION_DATE";
                commandText += " ,ACS_CARD_CONFIG.EXPIRY_DATE ";
                commandText += "   ,ACS_ACCESSLEVEL_RELATION.RD_ZN_ID";
                commandText += " ,ACS_READER.READER_DESCRIPTION";
                commandText += " ,ACS_ACCESSLEVEL_RELATION.CONTROLLER_ID";
                commandText += " ,ACS_CONTROLLER.CTLR_DESCRIPTION";

                //commandText += " ,ACS_TIMEZONE.TZ_TYPE ";
                //commandText += " ,ACS_TIMEZONE.TZ_DAYOFWEEK ";

                commandText += " FROM EAL_CONFIG ";
                commandText += " INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS ON EAL_CONFIG.EMPLOYEE_CODE= ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID";
                commandText += " INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_EMPID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID ";
                commandText += " INNER JOIN ENT_ORG_COMMON_ENTITIES AS DEP ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_DEPARTMENT_ID=DEP.OCE_ID AND DEP.CEM_ENTITY_ID='DEP'";
                commandText += " INNER JOIN ENT_ORG_COMMON_ENTITIES AS DESG ON ENT_EMPLOYEE_OFFICIAL_DTLS.EOD_DESIGNATION_ID=DESG.OCE_ID AND DESG.CEM_ENTITY_ID='DES'";

                commandText += " INNER JOIN ACS_ACCESSLEVEL ON EAL_CONFIG.AL_ID=ACS_ACCESSLEVEL.AL_ID ";
                //commandText += " INNER JOIN ACS_TIMEZONE ON    ACS_ACCESSLEVEL.AL_TIMEZONE_ID=ACS_TIMEZONE.TZ_ID ";
                commandText += " INNER JOIN ACS_ACCESSLEVEL_RELATION ON ACS_ACCESSLEVEL.AL_ID= ACS_ACCESSLEVEL_RELATION.AL_ID AND ACS_ACCESSLEVEL_RELATION.AL_ENTITY_TYPE='R' ";
                commandText += " INNER JOIN ACS_CARD_CONFIG ON  EAL_CONFIG.EMPLOYEE_CODE=ACS_CARD_CONFIG.CC_EMP_ID";

               commandText += "  INNER JOIN ACS_READER ON ACS_READER.READER_ID=ACS_ACCESSLEVEL_RELATION.RD_ZN_ID ";
                 commandText += "   AND CTLR_ID=ACS_ACCESSLEVEL_RELATION.CONTROLLER_ID ";
                 commandText += " INNER JOIN ACS_CONTROLLER ON ACS_CONTROLLER.CTLR_ID=ACS_ACCESSLEVEL_RELATION.CONTROLLER_ID";
              

                string strCondition = "",strSort="";


                strCondition = " ENT_EMPLOYEE_PERSONAL_DTLS.EPD_ISDELETED=0 ";

                if (RadioButtonList1.SelectedIndex == 1)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " EMPLOYEE_CODE in " + this.EmployeeHdn.Value;
                }

                if (RadioButtonList2.SelectedIndex == 1)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " ACS_ACCESSLEVEL_RELATION.CONTROLLER_ID  in " + this.ComapnyHdn.Value;
                }

                if (RadioButtonList3.SelectedIndex == 1)
                {
                    /*
                    string strReaderID = "", strControllerID = "",strTemp="";

                    strTemp = this.LocationHdn.Value.ToString();

                   strTemp= strTemp.Remove(0,1);
                   strTemp = strTemp.Remove(strTemp.Length-1,1);

                    string[] arrAllReader = strTemp.Split(',');

                    strReaderID = "(";
                    strControllerID = "(";

                    foreach (string OneReader in arrAllReader)
                    {

                        string[] arrReader = OneReader.Split('|');

                        strReaderID = strReaderID + (strReaderID.Length > 0? ",":" ")+arrReader[0];
                        strControllerID = strControllerID + (strControllerID.Length > 0 ? "," : " ") + arrReader[1];
                    }

                    strReaderID =strReaderID+ ")";
                    strControllerID =strControllerID+ ")";

                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " ACS_ACCESSLEVEL_RELATION.RD_ZN_ID in " + strReaderID + " AND CONTROLLER_ID in "+strControllerID;
                    */

                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " ACS_ACCESSLEVEL_RELATION.RD_ZN_ID in " + this.LocationHdn.Value;


                }

               



               // if (strDate != "")
               // {

               //    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "");

               //    strCondition = strCondition + "  CONVERT(DATETIME,CONVERT(VARCHAR(50),ACTIVATION_DATE,103),103) BETWEEN ";
               //    strCondition = strCondition + " CONVERT(DATETIME,CONVERT(VARCHAR(50),'"+strDate+"',103),103) ";
               //    strCondition = strCondition + "  AND  CONVERT(DATETIME,CONVERT(VARCHAR(50),'"+strToDate+"',103),103)";
               //}



                if(strCondition.Length > 0)
                {
                    strCondition =" WHERE "+ strCondition;
                }


                commandText = commandText + strCondition + strOrder;

               

                String commandType = "Text";
                               

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME




                DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);



                string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";
                ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });
               




                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = thisDataSet.Tables[tableName];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();

                HeadPnl.Visible = false;
                //Panel1.Visible = false;
                HeadPanel.Visible = false;
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
        protected void Close_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reader_Access_Report.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessDashboard.aspx");
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
           
            RadioButtonList1.SelectedIndex = 0;
            RadioButtonList2.SelectedIndex = 0;
            RadioButtonList3.SelectedIndex = 0;
            //RadioButtonList4.SelectedIndex = 0;
            //RadioButtonList5.SelectedIndex = 0;
            //RadioButtonList6.SelectedIndex = 0;
        }

    }
}