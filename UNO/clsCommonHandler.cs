using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;
using System.Net.Mail;
using System.Net;


namespace UNO
{
    public class clsCommonHandler
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public static string connString = conn.ConnectionString;
        public static DataTable GetReportHeader()
        {
            try
            {
                DataTable dt = ExecuteQuery("select * from ent_params where module='ENT' and identifier='GLOBALREPORTPARAM'", connString);
                return dt;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
                return null;
            }
        }
        private static DataTable ExecuteQuery(string strQuery, String con)
        {

            try
            {

                SqlCommand cmd = new SqlCommand(strQuery, conn);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
                return null;
            }

        }
        public static DataTable GetEntParameterValues(string Identifier, string Module)
        {
            return ExecuteQuery("Select Code,Value from ENT_PARAMS where identifier= '" + Identifier + "' and module='" + Module + "' order by Value", connString);
        }
		public static DataTable GetControllerList(string Identifier, string Module)
        {
            return ExecuteQuery("Select CTLR_ID,CTLR_DESCRIPTION from ACS_CONTROLLER where CTLR_ISDELETED =0 order by CTLR_DESCRIPTION", connString);
        }
        public static string GetLevelCode(int Levelid)
        {
            try
            {
                DataTable dt = ExecuteQuery("select levelcode from levelmaster where id= " + Levelid + "", connString);
                return dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
                return null;
            }
        }
        public static DataTable GetCommonEntitiesValues(string strEntityID)
        {
            return ExecuteQuery("select oce_id,oce_description from ENT_ORG_COMMON_ENTITIES where CEM_ENTITY_ID='" + strEntityID + "' AND isnull(OCE_ISDELETED,0)=0 ", connString);
        }
        public static string DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

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
            return serializer.Serialize(list);

        }
        public static DataSet GetCommonTableDetails(string strTable, string comID)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@Table", DbType.String, strTable, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@comID", DbType.String, comID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("Sp_GetCommonTableDetails", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }
        public static DataSet GetEmployeesDetails(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetEmployees", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }
        public static DataSet GetReaderZoneDetails(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_ReaderZoneDetails", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }
        public static DataSet GetEmployeesDetails(string strCommand, string strType)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Type", DbType.String, strType, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetEmployees", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }


        //aniket
        public static DataSet GetManagerDetails()
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetManager", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }

        public static DataSet GetDesignationDetails()
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetDesignation", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }


        public static DataSet GetControllerDetails()
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetController", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
            return objDataSet;
        }

        //aniket




        public static DataTable GetReasonDetails()
        {
            return ExecuteQuery("select Reason_ID,Reason_Description as Reason_Description from [ENT_Reason] where Reason_Type='el'  and isnull(reason_isdeleted,0)=0", connString);
        }
        public static string PageName()
        {
            return HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        }
        public string GetIpAddress()//Get IP Address
        {
            

            // Get UserHostAddress property.
            string address = HttpContext.Current.Request.UserHostAddress;            
            return address;


        }

        

        public static clsCommonEntity GetClientName()
        {
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsCommonEntity objData = null;

            try
            {

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("select code,value from ent_params where module='ENT' and identifier='GLOBALREPORTPARAM'", CommandType.Text);
                objData = new clsCommonEntity();
                if (objDataSet.Tables.Count > 0)
                {
                    if (objDataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                        {
                          

                            if (objDataSet.Tables[0].Rows[i]["CODE"].ToString() == "COMP")
                                objData.CompanyName = objDataSet.Tables[0].Rows[i]["VALUE"].ToString();

                            if (objDataSet.Tables[0].Rows[i]["CODE"].ToString() == "COMPADD")
                                objData.CompanyAdd = objDataSet.Tables[0].Rows[i]["VALUE"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
                return objData;
            }


            return objData;
        }



    }
    public class clsCommonEntity
    {
        public string CompanyName { get; set; }
        public string CompanyAdd { get; set; }
    }
}