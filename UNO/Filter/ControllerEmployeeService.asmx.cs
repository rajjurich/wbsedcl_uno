using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Data;
using System.Configuration;

namespace UNO.Filter
{
    /// <summary>
    /// Summary description for ControllerEmployeeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ControllerEmployeeService : System.Web.Services.WebService
    {
        public static string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        [WebMethod]
        public void GetControllerEmployeeMapping(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            List<ControllerEmployeeMappingModel> controllerEmployeeMappings = new List<ControllerEmployeeMappingModel>();
            int filteredCount = 0;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                using (SqlConnection con = new SqlConnection(m_connections))
                {
                    SqlCommand cmd = new SqlCommand("uspGetControllerEmployeeMapping", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramDisplayLength = new SqlParameter() { ParameterName = "@DisplayLength", Value = displayLength };
                    cmd.Parameters.Add(paramDisplayLength);
                    SqlParameter paramDisplayStart = new SqlParameter() { ParameterName = "@DisplayStart", Value = displayStart };
                    cmd.Parameters.Add(paramDisplayStart);
                    SqlParameter paramSortCol = new SqlParameter() { ParameterName = "@SortCol", Value = sortCol };
                    cmd.Parameters.Add(paramSortCol);
                    SqlParameter paramSortDir = new SqlParameter() { ParameterName = "@SortDir", Value = sortDir };
                    cmd.Parameters.Add(paramSortDir);
                    SqlParameter paramSearchString = new SqlParameter() { ParameterName = "@Search", Value = string.IsNullOrEmpty(search) ? null : search };
                    cmd.Parameters.Add(paramSearchString);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        ControllerEmployeeMappingModel controllerEmployeeMapping = new ControllerEmployeeMappingModel();
                        controllerEmployeeMapping.RowNum = Convert.ToInt32(rdr["RowNum"]);
                        filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                        controllerEmployeeMapping.EmpCode = rdr["EmpCode"].ToString();
                        controllerEmployeeMapping.EmpName = rdr["EmpName"].ToString();
                        controllerEmployeeMapping.ControllerId = rdr["ControllerId"].ToString();
                        controllerEmployeeMapping.ControllerDescription = rdr["ControllerDescription"].ToString();
                        controllerEmployeeMappings.Add(controllerEmployeeMapping);
                    }
                }

                var result = new
                {
                    draw = 0,
                    recordsTotal = GetTotalCostCenterCount(),
                    recordsFiltered = filteredCount,
                    data = controllerEmployeeMappings
                };


                Context.Response.Write(js.Serialize(result));
            }
            catch (Exception ex)
            {
                Context.Response.Write(js.Serialize(ex.Message));
            }
        }
        private int GetTotalCostCenterCount()
        {
            int totalCostCenterCount = 0;
            using (SqlConnection con = new SqlConnection(m_connections))
            {
                SqlCommand cmd = new SqlCommand("select count(*) from Eal_Config where flag=0 and TemplateFlag=0 and DELETEDDATE is null", con);
                con.Open();
                totalCostCenterCount = (int)cmd.ExecuteScalar();
            }
            return totalCostCenterCount;
        }
    }
}
