using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace UNO
{
    public partial class Attendance_Summary_Report : System.Web.UI.Page
    {
        SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        void Page_PreInit(Object sender, EventArgs e)
        {
            this.Title = "Attendance Summary Report";
        }
        string strDate = "", strToDate = "";         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {

                    fillEmplist();
                    Filllocationdrp();
                    Filldivisiondrp();
                    
                }
                else
                {
                    fillEntities();
                }
                BindMonth();
                BindYear();

            }
            else
            {
                //if (Page.Request.Form["txtCalendarFrom"] != null)
                //{
                //strDate = Page.Request.Form["txtCalendarFrom"].ToString();
                //strToDate = Page.Request.Form["txtCalendarTo"].ToString();

                strDate = ddlFromMonth.SelectedItem.ToString();
                strToDate = ddlFromYear.SelectedItem.ToString();
                //}

            }
            ShowTable.Attributes.Add("style", "display:block");
        }
        private void fillEntities()
        {
            string mgrId = Session["uid"].ToString();

            string levelId = Session["levelId"].ToString();
            //SqlConnection con = new SqlCOConnection(m_connectons);

            if (conString.State == ConnectionState.Closed)
            {
                conString.Open();
            }

            SqlCommand cmd = new SqlCommand("spFillEntities2", conString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@levelid", levelId);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            if (conString.State == ConnectionState.Open)
            {
                conString.Close();
            }
            ListBox1.DataValueField = "EOD_EMPID";
            ListBox1.DataTextField = "EmployeeName";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();


            ListBox7.DataValueField = "EOD_EMPID";
            ListBox7.DataTextField = "EmployeeName";
            ListBox7.DataSource = thisDataSet.Tables[0];
            ListBox7.DataBind();



            DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

            ListBox4.DataValueField = "DivisionID";
            ListBox4.DataTextField = "Division_NAME";

            ListBox4.DataSource = dtDivision;

            ListBox4.DataBind();

            ListBox10.DataValueField = "DivisionID";
            ListBox10.DataTextField = "Division_NAME";
            ListBox10.DataSource = thisDataSet.Tables[3];
            ListBox10.DataBind();



            DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

            ListBox3.DataValueField = "LocationID";
            ListBox3.DataTextField = "Location_NAME";

            ListBox3.DataSource = dtLocation;

            ListBox3.DataBind();

            ListBox9.DataValueField = "LocationID";
            ListBox9.DataTextField = "Location_NAME";
            ListBox9.DataSource = thisDataSet.Tables[1];
            ListBox9.DataBind();

        }   
        protected void View_Click(object sender, EventArgs e)
        {
            try
            {

                viewer.Visible = true;
                ReportViewer1.Visible = true;
                btnClose.Visible = true;
                DivSearch.Visible = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/AttendanceSummary_Report.rdlc");
                String dataSetName = "DataSet1";
                String tableName = "AttendanceSummary_Report";
                string loc = LocationHdn.Value;
                string Div = DivisionHdn.Value;
                string EmpCode = EmployeeHdn.Value;
                string month = ddlFromMonth.SelectedValue;
                string year = ddlFromYear.SelectedValue;
                string query = "EXEC AttendanceSummary_Report @Location='" + loc + "' ,@Division ='" + Div + "',"
                              + " @EmpCode='" + EmpCode + "',@pMonth='" + month + "',@pYear='" + year + "'";
                            
                DataSet thisDataSet = ExecuteQuery(query, dataSetName, tableName);
                int a = thisDataSet.Tables[tableName].Rows.Count;


                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = m_connectons;

                if (conString.State == ConnectionState.Closed)
                {
                    conString.Open();
                }



                SqlCommand cmd = new SqlCommand("select * from ent_params where module='ENT' and identifier='GLOBALREPORTPARAM'", conString);
                cmd.CommandType = CommandType.Text;
             

                //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
              

                string comp = "", compAdd = "";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    comp = dt.Rows[0]["value"].ToString();
                    compAdd = dt.Rows[1]["value"].ToString();
                }

                if (conString.State == ConnectionState.Open)
                {
                    conString.Close();
                }


                ReportParameter header = new ReportParameter("header", comp);
                ReportParameter address = new ReportParameter("address", compAdd);

                       

                string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for " + strDate + " of " + strToDate;
                ReportParameter para = new ReportParameter("myParameter1", strReportHeader);
              
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { header, address });  
               
            


                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = thisDataSet.Tables[tableName];
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {

            }
        }
        private DataSet ExecuteQuery(string commandText, string dataSetName, string tableName)
        {
            DataSet dataSet = new DataSet(dataSetName);
            try
            {
                if (conString.State == ConnectionState.Closed)
                {
                    conString.Open();
                }
                SqlDataAdapter adptr = new SqlDataAdapter(commandText, conString);
                adptr.SelectCommand.CommandTimeout = 0;
                adptr.Fill(dataSet, tableName);
            }
            catch (Exception ex)
            {
            }
            return dataSet;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Attendance_Summary_Report.aspx");
        }
        protected void btnClosePage_Click(object sender, EventArgs e)
        {
            //Response.Redirect("TADashboard.aspx");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Attendance_Summary_Report.aspx");
        }
        private void BindMonth()
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            ddlFromMonth.Items.Add(new ListItem("-Select Month-", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlFromMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }

        }
        private void BindYear()
        {
            //ddlFromYear.Items.Add(new ListItem("-Select Year-", "0"));
            //for (int i = 2012; i < 2020; i++)
            //{
            //    ddlFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //}


            ddlFromYear.Items.Add(new ListItem("-Select Year-", "0"));

            int year = DateTime.Now.Year;

            for (int i = 2012; i <= year; i++)
            {
                ddlFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }
        public void fillEmplist()
        {
            DataSet thisDataSet = clsCommonHandler.GetEmployeesDetails("NEMP", "");

            ListBox1.DataValueField = "EPD_EMPID";
            ListBox1.DataTextField = "EPD_FIRST_NAME";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();

            ListBox7.DataValueField = "EPD_EMPID";
            ListBox7.DataTextField = "EPD_FIRST_NAME";
            ListBox7.DataSource = thisDataSet.Tables[0];
            ListBox7.DataBind();


        }
        private void Filllocationdrp()
        {
            string strSql = "select OCE_ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  ))+OCE_ID,' ',' ' )  as LOCATIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='LOC'";
            SqlCommand cmd = new SqlCommand(strSql, conString);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox3.DataValueField = "oce_id";
            ListBox3.DataTextField = "LOCATIONNAME";
            ListBox3.DataSource = thisDataSet.Tables[0];
            ListBox3.DataBind();

            ListBox9.DataValueField = "oce_id";
            ListBox9.DataTextField = "LOCATIONNAME";
            ListBox9.DataSource = thisDataSet.Tables[0];
            ListBox9.DataBind();
        }
        private void Filldivisiondrp()
        {
            string strSql = "select OCE_ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  ))+OCE_ID,' ',' ' )  as DIVISIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='DIV'";
            SqlCommand cmd = new SqlCommand(strSql, conString);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox4.DataValueField = "oce_id";
            ListBox4.DataTextField = "DIVISIONNAME";
            ListBox4.DataSource = thisDataSet.Tables[0];
            ListBox4.DataBind();

            ListBox10.DataValueField = "oce_id";
            ListBox10.DataTextField = "DIVISIONNAME";
            ListBox10.DataSource = thisDataSet.Tables[0];
            ListBox10.DataBind();
        }
    }
}