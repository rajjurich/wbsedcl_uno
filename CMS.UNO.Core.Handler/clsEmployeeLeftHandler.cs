using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsEmployeeLeftHandler
    {
        public static DataTable EmpLeftEntities(string strCommand,string levelID)
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

                paramStruct = new ParamStruct("@LevelID", DbType.String, levelID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetEmpLeftEntitites", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeLeft");
            }
            return objDataSet.Tables[0];
        }

        public static DataTable GetAllLeftEmployees()
        {           
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {  
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("PROC_GET_EMPLOYEE_LEFT_DETAILS", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeLeft");
            }
            return objDataSet.Tables[0];
        }

        public static void InsertUpdateEmpLeftDetails(clsEmployeeLeft objEmp, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@Left_REC_ID", DbType.Int32, objEmp.RecID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Left_EMP_ID", DbType.String, objEmp.EmpID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Left_DATE", DbType.String, objEmp.LeftDate, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Left_Reason", DbType.String, objEmp.Reason, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objEmp.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("PROC_SAVE_Employee_Left_Configuration", CommandType.StoredProcedure, paramColl);

                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Reason");
            }
        }

        public static void DeleteEmpLeftDetails(clsEmployeeLeft objEmp, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

              
                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objEmp.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("PROC_DELETE_EMPLOYEE_LEFT_DETAILS_BYROWID", CommandType.StoredProcedure, paramColl);

                strError = paramColl[0].value.ToString();
                strSuccess = paramColl[1].value.ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Reason");
            }
        }

        public static void LetfEmployees(clsEmployeeLeft objEmp, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@ErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SuccMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objEmp.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("PROC_UPDATE_EMP_LEFT_DETAILS", CommandType.StoredProcedure, paramColl);

                strError = paramColl[0].value.ToString();
                strSuccess = paramColl[1].value.ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Reason");
            }
        }
    }

    public class clsEmployeeLeft
    {
        public Int32 RecID { get; set; }
        public string EmpID { get; set; }
        public string LeftDate { get; set; }
        public string Reason { get; set; }
        public string CreatedBy { get; set; }
    }
}
