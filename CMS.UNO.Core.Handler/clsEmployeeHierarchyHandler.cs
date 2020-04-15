using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsEmployeeHierarchyHandler
    {

        public static DataTable GetEmployeeManagerDetails(string strCommand, clsEmployeeHierarchy objData)
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

                paramStruct = new ParamStruct("@EMPID", DbType.String, objData.EmpID, ParameterDirection.Input);
                paramColl.Add(paramStruct);               

                paramStruct = new ParamStruct("@Cmpcde", DbType.String, objData.Company, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Loccde", DbType.String, objData.Location, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@divcde", DbType.String, objData.Division, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@depcde", DbType.String, objData.Department, ParameterDirection.Input);
                paramColl.Add(paramStruct);
               
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetManagerHierarchyDetails", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Hierarchy");
            }
            return objDataSet.Tables[0];
        }
        public static void UpdateEmployeeManagerDetails(clsEmployeeHierarchy objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@MgrID", DbType.String, objData.MngrID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@RowID", DbType.String, objData.RowID, ParameterDirection.Input);
                paramColl.Add(paramStruct);             

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objData.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

              
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_UpdateEmployeeHierDetails", CommandType.StoredProcedure, paramColl);

                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Hierarchy");
            }
        }

        public static void UploadHierarchy(clsEmployeeHierarchy objData, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objData.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("PROC_INSERT_EMPLOYEE_HIRARCHY_UPLOAD", CommandType.StoredProcedure, paramColl);

                strError = paramColl[1].value.ToString();
                strSuccess = paramColl[2].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Hierarchy");
            }
        }

        public static void InsertEmployeeMgr(clsEmployeeHierarchy objData, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objData.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);
              
                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@MGR_ID", DbType.String, objData.MngrID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EmpCode", DbType.String, objData.EmpID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Cmpcde", DbType.String, objData.Company, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Loccde", DbType.String, objData.Location, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@divcde", DbType.String, objData.Division, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@depcde", DbType.String, objData.Department, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("PROC_Insert_Emp_Mgr", CommandType.StoredProcedure, paramColl);

                strError = paramColl[2].value.ToString();
                strSuccess = paramColl[3].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Hierarchy");
            }
        }

    }

    public class clsEmployeeHierarchy
    {
        public string MngrID { get; set; }
        public string EmpID { get; set; }
        public string RowID { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }       
        public string CreatedBy { get; set; }
    }
}
