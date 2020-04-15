using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsAutoScheduleBackupHandler
    {
        public static DataTable GetBackupDetails(string strCommand)
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
                objDataSet = dataAccessObject.ExecDataSet("USP_AutoScheduledBackupConfig", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Backup");
            }
            return objDataSet.Tables[0];
        }
        public static void insertBackupDetails(string strCommand, clsAutoScheduleBackup objData, string PageName,ref string SuccMsg)
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

                paramStruct = new ParamStruct("@Sqlservername", DbType.String, objData.ServerName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Username", DbType.String, objData.UserName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Password", DbType.String, objData.Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@DatabaseName", DbType.String, objData.DatabaseName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Days", DbType.String, objData.Days, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@StartTime", DbType.String, objData.StartTime, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EmailId", DbType.String, objData.EmailID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@BackupPath", DbType.String, objData.BackupPath, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

      
                paramStruct = new ParamStruct("@strSuccess", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("USP_AutoScheduledBackupConfig", CommandType.StoredProcedure, paramColl);

                SuccMsg = paramColl[10].value.ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Backup");
            }
           
        }
    }

    public class clsAutoScheduleBackup
    {
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string Days { get; set; }
        public string StartTime { get; set; }
        public string EmailID { get; set; }
        public string BackupPath { get; set; }
      

    }
}
