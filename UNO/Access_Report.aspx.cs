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
    public partial class Access_Report : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "",strToDate="";
        void Page_PreInit(Object sender, EventArgs e)
        {
            this.Title = "Access_Report.aspx";
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
               // FillControllerDrp();
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
                     " FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  " +
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
           // string strSql = "SELECT READER_ID ,replace(convert(char(31),ltrim(READER_DESCRIPTION))+Convert(varchar(100),READER_ID),' ',' ' ) as READER_DESCRIPTION  FROM ACS_READER where READER_ISDELETED='0'";


            string strSql = "SELECT READER_ID ,replace(convert(char(31),ltrim(READER_DESCRIPTION))+Convert(varchar(100),READER_ID)+'('+Convert(varchar(100),CTLR_ID)+')',' ',' ' ) as READER_DESCRIPTION  FROM ACS_READER where READER_ISDELETED='0'";

            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox2.DataValueField = "READER_ID";
            ListBox2.DataTextField = "READER_DESCRIPTION";
            ListBox2.DataSource = thisDataSet.Tables[0];
            ListBox2.DataBind();

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

                ReportViewer1.LocalReport.ReportPath = "RDLC\\Access_Report.rdlc";
              
                ReportViewer1.Visible = true;
                

                String commandText = "";
                commandText =  " ";

                commandText += " SELECT CONVERT(VARCHAR(20),Event_Datetime,103) AS EVENT_DATE ";
                commandText += ",CONVERT(VARCHAR(20),Event_Datetime,108) AS EVENT_TIME";
                commandText += " ,ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID";
                commandText += " ,ENT_EMPLOYEE_PERSONAL_DTLS.EPD_FIRST_NAME +' '+ ENT_EMPLOYEE_PERSONAL_DTLS.EPD_LAST_NAME AS NAME";
                commandText += " ,ACS_EVENTS.Event_Controller_ID,ACS_CONTROLLER.CTLR_DESCRIPTION,";
                commandText += " ACS_EVENTS.Event_Reader_ID,ACS_READER.READER_DESCRIPTION ";

                commandText += ",CASE Event_Status";
                commandText += " WHEN 0 THEN 'Not Used'";
                commandText += " WHEN 1 THEN 'Access Granted' ";
                commandText += " WHEN 2 THEN 'Card No not in Employee Profile List' ";
                commandText += " WHEN 3 THEN 'Card has expired' ";
                commandText += " WHEN 4 THEN 'Reader is Disabled'";
                commandText += " WHEN 5 THEN 'Invalid Access Time'";
                commandText += " END AS EVENT_STATUS";


                commandText += " FROM ACS_EVENTS";

                commandText += " INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS ON ACS_EVENTS.Event_Employee_Code=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID";
                commandText += " INNER JOIN ACS_CONTROLLER ON   ACS_EVENTS.Event_Controller_ID=ACS_CONTROLLER.CTLR_ID ";
                commandText += " INNER JOIN ACS_READER ON ACS_EVENTS.Event_Reader_ID=ACS_READER.READER_ID  ";
                commandText += " AND ACS_EVENTS.Event_Controller_ID=ACS_READER.CTLR_ID";


                string strCondition = "",strSort="";


                strCondition = " ENT_EMPLOYEE_PERSONAL_DTLS.EPD_ISDELETED=0 ";

                if (RadioButtonList1.SelectedIndex == 1)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " Event_Employee_Code  in " + this.EmployeeHdn.Value;
                }

                if (RadioButtonList2.SelectedIndex == 1)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " ACS_EVENTS.Event_Reader_ID in " + this.ComapnyHdn.Value;
                }


           
                if(chkGrantAccess.Checked ==true && chkDeniedAccess.Checked==true)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " Event_Status in('0','1','2','3','4','5') ";
                }

                else  if(chkGrantAccess.Checked ==true && chkDeniedAccess.Checked==false)
                {
                    strCondition = strCondition+(strCondition.Length > 0 ? " AND " : "") + " Event_Status in('0','1') ";
                }

                else if (chkGrantAccess.Checked == false && chkDeniedAccess.Checked ==true)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " Event_Status in('2','3','4','5') ";
                }

                else if (chkGrantAccess.Checked == false && chkDeniedAccess.Checked == false)
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " Event_Status in('0','1','2','3','4','5') ";
                }




                if (strDate != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") +
                    " CONVERT(DATETIME,CONVERT(VARCHAR(50),Event_Datetime,103),103) between  CONVERT(DATETIME, '" + strDate + "', 103)  AND CONVERT(DATETIME, '" + strToDate + "', 103)";

                   



                }



                if(strCondition.Length > 0)
                {
                    strCondition =" WHERE "+ strCondition;
                }

               
                commandText = commandText + strCondition+strSort;

               

                String commandType = "Text";
                               

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
                String tableName = "DataTable1";//USER DEFINED NAME




                DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);



                string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for " + strDate + " To" + strToDate;
                ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
              //  ReportParameter para3 = new ReportParameter("count", thisDataSet.Tables[0].Rows.Count.ToString());

                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para});
            

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
            Response.Redirect("Access_Report.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessDashboard.aspx");
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
           
            RadioButtonList1.SelectedIndex = 0;
            RadioButtonList2.SelectedIndex = 0;
           // RadioButtonList3.SelectedIndex = 0;
            //RadioButtonList4.SelectedIndex = 0;
            //RadioButtonList5.SelectedIndex = 0;
            //RadioButtonList6.SelectedIndex = 0;
        }

    }
}