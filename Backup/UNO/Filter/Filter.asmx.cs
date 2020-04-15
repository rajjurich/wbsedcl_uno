/*
 Added by Pooja Yadav 
 Purpose:To filter the entitites 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
namespace UNO.Filter
{
    /// <summary>
    /// Summary description for Filter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Filter : System.Web.Services.WebService
    {
        public static string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        [WebMethod(EnableSession = true)]      
        public string FillEntitySpecificDetails(string strcom, string strloc, string strdiv, string strdept, string strcat, string strchecked, string strshift, string strSec, string strGRD)
        {

            SqlConnection con = new SqlConnection(m_connections);
            SqlDataAdapter da;
            DataTable dt = null;
            string strCompanyCode, strLocation, strDivision, strDepartment, strCategory, strShift, strSection, strGrade;
            string mgrId = HttpContext.Current.Session["uid"].ToString();
            strCompanyCode = HttpUtility.UrlDecode(strcom) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strcom);
            strLocation = HttpUtility.UrlDecode(strloc) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strloc);
            strDivision = HttpUtility.UrlDecode(strdiv) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strdiv);
            strDepartment = HttpUtility.UrlDecode(strdept) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strdept);
            strCategory = HttpUtility.UrlDecode(strcat) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strcat);
            strShift = HttpUtility.UrlDecode(strshift) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strshift);
            strSection = HttpUtility.UrlDecode(strSec) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strSec);
            strGrade = HttpUtility.UrlDecode(strGRD) == "\'N\'" ? "N" : HttpUtility.UrlDecode(strGRD);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_EMPDETAILS_ROLEWISE";
                cmd.Parameters.AddWithValue("@strchecked", strchecked);
                cmd.Parameters.AddWithValue("@EmployeeCode", mgrId);
                cmd.Parameters.AddWithValue("@SelCompanyCode", strCompanyCode);
                cmd.Parameters.AddWithValue("@SelLocationCode", strLocation);
                cmd.Parameters.AddWithValue("@SelDivisionCode", strDivision);
                cmd.Parameters.AddWithValue("@SelDepartCode", strDepartment);
                cmd.Parameters.AddWithValue("@SelCategoryCode", strCategory);
                cmd.Parameters.AddWithValue("@SelShiftCode", strShift);
                cmd.Parameters.AddWithValue("@SelSectionCode", strSection);
                cmd.Parameters.AddWithValue("@SelGradeCode", strGrade);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EntityFilter");
            }
            return DataTableToJSON(dt);

        }
        public static string DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();
            string json;
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            //string Regex=@"[\@\#\$\%\&\*\(\)\-\_\+\]\[\'\;\:\?\.\,\!]+$]";
            // ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";","_", "(", ")", ":", "|", "[", "]" 
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // json = System.Text.RegularExpressions.Regex.Replace((serializer.Serialize(list)), "[$|&@#$%,~`&*.()-+;!?{}[]_:]", "");
            return serializer.Serialize(list);



        }

    }

}

