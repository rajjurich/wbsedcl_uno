using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsHolidayHandler
    {
        public static void InsertUpdateHolidayDetails(clsHoliday objHoliday, string strCommand, string strXML, string strOldXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_ID", DbType.String, objHoliday.HOLIDAY_ID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_DESCRIPTION", DbType.String, objHoliday.HOLIDAY_DESCRIPTION, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_DATE", DbType.String, objHoliday.HOLIDAY_DATE, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_SWAP", DbType.String, objHoliday.HOLIDAY_SWAP, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_LOCATION", DbType.String, objHoliday.HOLIDAY_LOCATION, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_TYPE", DbType.String, objHoliday.HOLIDAY_TYPE, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objHoliday.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strOldXML", DbType.String, strOldXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_insertUpdateHolidays", CommandType.StoredProcedure, paramColl);

                strError = paramColl[8].value.ToString();
                strSuccess = paramColl[9].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Holiday");
            }
        }
        public static DataTable GetHolidayLocationDetails(string strHolidayID, string strCommand, ref clsHoliday objHoliday)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                objHoliday = new clsHoliday();
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@HOLIDAY_ID", DbType.String, strHolidayID, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetHolidaysDetails", CommandType.StoredProcedure, paramColl);
                if (objDataSet.Tables.Count > 0)
                {
                    if (objDataSet.Tables[0].Rows.Count > 0)
                    {
                        objHoliday.HOLIDAY_ID = objDataSet.Tables[0].Rows[0]["holiday_id"].ToString();
                        objHoliday.HOLIDAY_DESCRIPTION = objDataSet.Tables[0].Rows[0]["HOLIDAY_DESCRIPTION"].ToString();
                        objHoliday.HOLIDAY_TYPE = objDataSet.Tables[0].Rows[0]["HOLIDAY_TYPE"].ToString();
                        objHoliday.HOLIDAY_DATE = objDataSet.Tables[0].Rows[0]["HOLIDAY_DATE"].ToString();
                        objHoliday.HOLIDAY_SWAP = objDataSet.Tables[0].Rows[0]["HOLIDAY_SWAP"].ToString();
                        objHoliday.HOLIDAY_LOCATION = objDataSet.Tables[0].Rows[0]["HOLIDAY_LOCATION"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Holiday");
            }
            return objDataSet.Tables[1];
        }
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
                objDataSet = dataAccessObject.ExecDataSet("sp_GetHolidaysDetails", CommandType.StoredProcedure, paramColl);
               

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Holiday");
            }
            return objDataSet.Tables[0];
        }
    }
    public class clsHoliday
    {
        public string HOLIDAY_ID { get; set; }
        public string HOLIDAY_DESCRIPTION { get; set; }
        public string HOLIDAY_TYPE { get; set; }
        public string HOLIDAY_DATE { get; set; }
        public string HOLIDAY_SWAP { get; set; }
        public string HOLIDAY_LOCATION { get; set; }
        public string CreatedBy { get; set; }

    }
    public class clsHolidayLoc : clsHoliday
    {
        public string HOLIDAY_LOC_ID { get; set; }
        public Boolean IS_OPTIONAL { get; set; }
    }
}
