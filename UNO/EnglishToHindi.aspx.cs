using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace UNO
{
    public partial class EnglishToHindi : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        string EmpCode;
        protected void Page_Load(object sender, EventArgs e)
        {
         

            EmpCode = Convert.ToString(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                BindLbl();
            }
            if (EmpCode != "")
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EXEC Sp_InsertUnicode @strCommand='View',@EmpCode='" + EmpCode +"'", cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();
                var field = "";
                foreach (DataRow dtRow in dt.Rows)
                {

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (field == "")
                        {
                            field = dtRow[dc].ToString();
                        }
                        else
                        {
                            field = field + '^' + dtRow[dc].ToString();
                        }
                    }
                }
                hdfValues.Value = field;
           
            }
        }
        protected void BindLbl()
        {

          
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string query = "EXEC Sp_InsertUnicode @strCommand='GetUserInfo',@EmpCode='" + EmpCode +"'";
            SqlDataAdapter da = new SqlDataAdapter(query,cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                lblId.Text = EmpCode;
                lblName.Text =Convert.ToString(dt.Rows[0]["EMPLOYEE NAME"]);
                lblSALUTATION.Text = Convert.ToString(dt.Rows[0]["SALUTATION"]);
                lblJoin_Date.Text = Convert.ToString(dt.Rows[0]["JOININGDATE"]);
                lblCompanyName.Text = Convert.ToString(dt.Rows[0]["COMPANYNAME"]);
                lblLocationName.Text = Convert.ToString(dt.Rows[0]["LOCATIONNAME"]);
                lblDiviName.Text = Convert.ToString(dt.Rows[0]["DIVISIONNAME"]);
                lblDepaName.Text = Convert.ToString(dt.Rows[0]["DEPARTMENTNAME"]);
                lblDesigName.Text = Convert.ToString(dt.Rows[0]["DESIGNATIONNAME"]);
                lblCateName.Text = Convert.ToString(dt.Rows[0]["CATEGORYNAME"]);
                lblGroupName.Text = Convert.ToString(dt.Rows[0]["GROUPNAME"]);
                lblGradeName.Text = Convert.ToString(dt.Rows[0]["GRADENAME"]);
                lblEmpNickName.Text = Convert.ToString(dt.Rows[0]["EPD_NICKNAME"]);
                lblCardExDate.Text = Convert.ToString(dt.Rows[0]["CARD EXPIRY DATE"]);
                lblGender.Text = Convert.ToString(dt.Rows[0]["EPD_GENDER"]);
                lblDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                lblDomi.Text = Convert.ToString(dt.Rows[0]["EPD_DOMICILE"]);
                lblBloodGrp.Text = Convert.ToString(dt.Rows[0]["EPD_BLOODGROUP"]);

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }
        [WebMethod]
        public static string InsertData(string EmpCode, string EmpId, string txtEmpName, string txtSalutation, string txtJoinDate, string txtCompanyName, string txtLocName, string txtDiviName, string txtDepaName, string txtDesigName, string txtCateName, string txtGroupName, string txtGradeName, string txtEmpNickName, string txtCardExDate, string txtGender, string txtDOB, string txtDomi, string txtBloodGrp)
        {
            string Result = "";
            try
            {

                SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                SqlCommand cmd = new SqlCommand("Sp_InsertUnicode", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "Insert";
                cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
                cmd.Parameters.Add("@EMPLOYEE_ID", SqlDbType.NVarChar).Value = EmpId ;
                cmd.Parameters.Add("@EMPLOYEE_NAME", SqlDbType.NVarChar).Value = txtEmpName;
                cmd.Parameters.Add("@SALUTATION", SqlDbType.NVarChar).Value = txtSalutation ;
                cmd.Parameters.Add("@JOINING_DATE", SqlDbType.NVarChar).Value =  txtJoinDate ;
                cmd.Parameters.Add("@COMPANY_NAME", SqlDbType.NVarChar).Value =  txtCompanyName ;
                cmd.Parameters.Add("@LOCATION_NAME", SqlDbType.NVarChar).Value =  txtLocName ;
                cmd.Parameters.Add("@DIVISION_NAME", SqlDbType.NVarChar).Value =  txtDiviName ;
                cmd.Parameters.Add("@DEPARTMENT_NAME", SqlDbType.NVarChar).Value =  txtDepaName ;
                cmd.Parameters.Add("@DESIGNATION_NAME", SqlDbType.NVarChar).Value =  txtDiviName ;
                cmd.Parameters.Add("@CATEGORY_NAME", SqlDbType.NVarChar).Value =  txtDesigName ;
                cmd.Parameters.Add("@GROUP_NAME", SqlDbType.NVarChar).Value =  txtGroupName ;
                cmd.Parameters.Add("@GRADE_NAME", SqlDbType.NVarChar).Value =  txtGradeName ;
                cmd.Parameters.Add("@EMPLOYEE_NICKNAME", SqlDbType.NVarChar).Value =  txtEmpNickName ;
                cmd.Parameters.Add("@CARD_EXPIRY_DATE", SqlDbType.NVarChar).Value =  txtCardExDate ;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value =  txtGender ;
                cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value =  txtDOB ;
                cmd.Parameters.Add("@DOMICILE", SqlDbType.NVarChar).Value =  txtDomi ;
                cmd.Parameters.Add("@BLOOD_GROUP", SqlDbType.NVarChar).Value =  txtBloodGrp ;


                cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = "EnglishToHindi.aspx";
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["uid"]);
                cmd.Parameters.Add("@strSuccOut", SqlDbType.VarChar,50);
                cmd.Parameters["@strSuccOut"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Result = cmd.Parameters["@strSuccOut"].Value.ToString();
                cn.Close();
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
            return Result;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnglishToHindiView.aspx");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

        }
    
    }
}