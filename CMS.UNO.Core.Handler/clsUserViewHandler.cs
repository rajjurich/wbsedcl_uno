using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsUserViewHandler
    {
        public static DataTable GetUserEmployeeDetails(string strCommand)
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
                objDataSet = dataAccessObject.ExecDataSet("sp_UserView", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "User");
            }
            return objDataSet.Tables[0];
        }
        public static clsUserView GetUserDetails(string strCommand,string UserID)
        {
            clsUserView objUser = null;
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
              
                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@UserID", DbType.String, UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_UserView", CommandType.StoredProcedure, paramColl);
                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            objUser = new clsUserView();
                            objUser.UserID = objDataSet.Tables[0].Rows[0]["UserID"].ToString();
                            objUser.Password = objDataSet.Tables[0].Rows[0]["Password"].ToString();
                            objUser.LevelID = objDataSet.Tables[0].Rows[0]["LevelID"].ToString();
                            objUser.EmpID = objDataSet.Tables[0].Rows[0]["EmployeeId"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "User");
            }
            return objUser;
        }
        public static void UpdateUserDetails(clsUserView objUser, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EmpId", DbType.String, objUser.EmpID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@UserID", DbType.String, objUser.UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Password", DbType.String, objUser.Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LevelID", DbType.String, objUser.LevelID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objUser.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_UserView", CommandType.StoredProcedure, paramColl);

                strError = paramColl[5].value.ToString();
                strSuccess = paramColl[6].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "User");
            }
        }
    }


    public class clsUserView
    {
        public string EmpID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string LevelID { get; set; }
        public string CreatedBy { get; set; }
    }
}
