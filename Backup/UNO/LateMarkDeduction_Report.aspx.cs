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
    public partial class LateMarkDeduction_Report1 : System.Web.UI.Page
    {
        SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "", strToDate = "";
        void Page_PreInit(Object sender, EventArgs e)
        {
            this.Title = "Late Mark Deduction Report";
        }
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
                //if (Page.Request.Form[ddlFromMonth.SelectedItem.ToString()] != null)
                //{
                strDate = ddlFromMonth.SelectedItem.ToString();
                strToDate = ddlFromYear.SelectedItem.ToString();
                //    strDate = Page.Request.Form[ddlFromMonth.SelectedItem.ToString()].ToString();
                //    strToDate = Page.Request.Form[ddlFromYear.SelectedItem.ToString()].ToString();
                //}
            }
            ShowTable.Attributes.Add("style", "display:block");
        }



        //private void fillEntities()
        //{
        //    string mgrId = Session["uid"].ToString();

        //    string levelId = Session["levelId"].ToString();
        //    SqlConnection con = new SqlConnection(m_connectons);

        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    SqlCommand cmd = new SqlCommand("spFillEntities2", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@levelid", levelId);

        //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    DataSet thisDataSet = new DataSet();
        //    adpt.Fill(thisDataSet);
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    ListBox1.DataValueField = "EOD_EMPID";
        //    ListBox1.DataTextField = "EmployeeName";

        //    ListBox1.DataSource = thisDataSet.Tables[0];

        //    ListBox1.DataBind();

        //    //DataTable dtCompany = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

        //    //ListBox2.DataValueField = "CompanyID";
        //    //ListBox2.DataTextField = "COMPANY_NAME";

        //    //ListBox2.DataSource = dtCompany;

        //    //ListBox2.DataBind();

        //    //DataTable dtCategory = thisDataSet.Tables[0].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

        //    //LstCategory.DataValueField = "CategoryID";
        //    //LstCategory.DataTextField = "Category_NAME";

        //    //LstCategory.DataSource = dtCategory;

        //    //LstCategory.DataBind();


        //    //DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

        //    //ListBox5.DataValueField = "DepartmentID";
        //    //ListBox5.DataTextField = "Department_NAME";

        //    //ListBox5.DataSource = dtDepartment;

        //    //ListBox5.DataBind();


        //    DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

        //    ListBox4.DataValueField = "DivisionID";
        //    ListBox4.DataTextField = "Division_NAME";

        //    ListBox4.DataSource = dtDivision;

        //    ListBox4.DataBind();




        //    DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

        //    ListBox3.DataValueField = "LocationID";
        //    ListBox3.DataTextField = "Location_NAME";

        //    ListBox3.DataSource = dtLocation;

        //    ListBox3.DataBind();

        //}

        private void fillEntities()
        {
            string mgrId = Session["uid"].ToString();

            string levelId = Session["levelId"].ToString();
            SqlConnection con = new SqlConnection(m_connectons);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("spFillEntities2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@levelid", levelId);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            ListBox1.DataValueField = "EOD_EMPID";
            ListBox1.DataTextField = "EmployeeName";

            ListBox1.DataSource = thisDataSet.Tables[0];

            ListBox1.DataBind();


            ListBox7.DataValueField = "EOD_EMPID";
            ListBox7.DataTextField = "EmployeeName";
            ListBox7.DataSource = thisDataSet.Tables[0];
            ListBox7.DataBind();


            //DataTable dtCompany = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

            //ListBox2.DataValueField = "CompanyID";
            //ListBox2.DataTextField = "COMPANY_NAME";

            //ListBox2.DataSource = dtCompany;

            //ListBox2.DataBind();



            //ListBox8.DataValueField = "CompanyID";
            //ListBox8.DataTextField = "COMPANY_NAME";
            //ListBox8.DataSource = thisDataSet.Tables[4];
            //ListBox8.DataBind();

            //DataTable dtCategory = thisDataSet.Tables[0].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField = "Category_NAME";

            //LstCategory.DataSource = dtCategory;

            //LstCategory.DataBind();


            DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

            //ListBox5.DataValueField = "DepartmentID";
            //ListBox5.DataTextField = "Department_NAME";

            //ListBox5.DataSource = dtDepartment;

            //ListBox5.DataBind();



            //ListBox11.DataValueField = "DepartmentID";
            //ListBox11.DataTextField = "Department_NAME";
            //ListBox11.DataSource = thisDataSet.Tables[2];
            //ListBox11.DataBind();



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
                string MinInTime = System.Configuration.ConfigurationManager.AppSettings["MinInTime"].ToString();
                string MaxInTime = System.Configuration.ConfigurationManager.AppSettings["MaxInTime"].ToString();
                string MinOutTime = System.Configuration.ConfigurationManager.AppSettings["MinOutTime"].ToString();
                string MaxOutTime = System.Configuration.ConfigurationManager.AppSettings["MaxOutTime"].ToString();

                ReportViewer1.Visible = true;
                btnClose.Visible = true;
                DivSearch.Visible = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/LateMarkDeduction_Report.rdlc");
                String dataSetName = "DataSet1";
                String tableName = "LateMarkDeduction_Report";
                string loc = LocationHdn.Value;
                string Div = DivisionHdn.Value;
                string EmpCode = EmployeeHdn.Value;
                string month = ddlFromMonth.SelectedValue;
                string year = ddlFromYear.SelectedValue;
                string query = "EXEC LateMarkDeduction_Report @Location='" + loc + "' ,@Division ='" + Div + "',"
                              +" @EmpCode='" + EmpCode + "',@Month=" + month + ",@Year=" + year + ","
                              + " @MinInTime='" + MinInTime + "',@MaxInTime='" + MaxInTime + "',@MinOutTime='" + MinOutTime + "',@MaxOutTime='" + MaxOutTime + "'";
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
            catch(Exception ex)
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
            Response.Redirect("LateMarkDeduction_Report.aspx");
        }

        protected void btnClosePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("LateMarkDeduction_Report.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("LateMarkDeduction_Report.aspx");

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
            ddlFromYear.Items.Add(new ListItem("-Select Year-", "0"));

            int year = DateTime.Now.Year;

            for (int i = 2012; i <= year; i++)
            {
                ddlFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }
        public void fillEmplist()
        {
            string strsql;
            strsql = "SELECT EPD_EMPID,replace(convert(char(40),ltrim(EPD_FIRST_NAME))+EPD_EMPID,' ',' ' )  as  EPD_FIRST_NAME"
                     + " FROM ENT_EMPLOYEE_PERSONAL_DTLS "
                     + " inner join  ENT_EMPLOYEE_OFFICIAL_DTLS on  EPD_EMPID=EOD_EMPID  WHERE EPD_ISDELETED=0 and EOD_ACTIVE=1 ";
                      //  + " inner join  ENT_EMPLOYEE_OFFICIAL_DTLS on  EPD_EMPID=EOD_EMPID  WHERE EPD_ISDELETED=0";
            SqlCommand cmd = new SqlCommand(strsql, conString);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
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