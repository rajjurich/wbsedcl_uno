using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Sentinel.Handler
{
    public static class clsTimeZoneViewHandler
    {
        public static DataTable GetAllDetails(string strCommand)
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
                objDataSet = dataAccessObject.ExecDataSet("sp_TimezonView", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TimeZone");
            }
            return objDataSet.Tables[0];
        }

        public static int GetCount()
        {
                DataAccess dataAccessObject = null;
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                return Convert.ToInt16(dataAccessObject.ExecScalar("Select COUNT(*) from ACS_TIMEZONE with (nolock) where isnull(TZ_ISDELETED,0)=0", CommandType.Text));
        }
        public static DataSet GetAllDetails(string strCommand,Int32 Code)
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

                paramStruct = new ParamStruct("@TimeZoneCode", DbType.Int16, Code, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_TimezonView", CommandType.StoredProcedure, paramColl);
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TimeZone");
            }
            return objDataSet;
        }
        public static void UpdateTimeZoneDetails(clsTimeZone objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@TimeZoneCode", DbType.String, objData.TZCode, ParameterDirection.Input);
                paramColl.Add(paramStruct);
              
                paramStruct = new ParamStruct("@Description", DbType.String, objData.TZDescription, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objData.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_TimezonView", CommandType.StoredProcedure, paramColl);

                strError = paramColl[3].value.ToString();
                strSuccess = paramColl[4].value.ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "TimeZone");
            }
        }
    }
    public class clsTimeZone
    {
        public Int32 TZCode { get; set; }
        public string TZDescription { get; set; }
        public string CreatedBy { get; set; }
    }
}
