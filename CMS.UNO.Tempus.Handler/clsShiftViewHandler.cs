using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Tempus.Handler
{
    public static class clsShiftViewHandler
    {
        public static DataTable GetShiftDetails(string strCommand)
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
                objDataSet = dataAccessObject.ExecDataSet("sp_ShiftView", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Shift");
            }
            return objDataSet.Tables[0];
        }

        public static clsShift GetShiftDetails(string strCommand, string ShiftID)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsShift shift = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ShiftID", DbType.String, ShiftID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_ShiftView", CommandType.StoredProcedure, paramColl);
                clsShift _oldShift = new clsShift();
                shift = setOldDataForShift(objDataSet.Tables[0], _oldShift);

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Shift");
            }
            return shift;
        }

        public static void UpdateShiftDetails(clsShift objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ShiftID", DbType.String, objData.SHIFT_ID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_DESCRIPTION", DbType.String, objData.SHIFT_DESCRIPTION, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_ALLOCATION_TYPE", DbType.String, objData.SHIFT_ALLOCATION_TYPE, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_AUTO_SEARCH_START", DbType.String, objData.SHIFT_AUTO_SEARCH_START, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_AUTO_SEARCH_END", DbType.String, objData.SHIFT_AUTO_SEARCH_END, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_TYPE ", DbType.String, objData.SHIFT_TYPE, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_START", DbType.String, objData.SHIFT_START, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_END", DbType.String, objData.SHIFT_END, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_BREAK_START", DbType.String, objData.SHIFT_BREAK_START, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                paramStruct = new ParamStruct("@SHIFT_BREAK_END", DbType.String, objData.SHIFT_BREAK_END, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                paramStruct = new ParamStruct("@SHIFT_BREAK_HRS", DbType.String, objData.SHIFT_BREAK_HRS, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                paramStruct = new ParamStruct("@SHIFT_WORKHRS", DbType.String, objData.SHIFT_WORKHRS, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                paramStruct = new ParamStruct("@SHIFT_FLAG_ADD_BREAK", DbType.Boolean, objData.SHIFT_FLAG_ADD_BREAK, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_WEEKEND_DIFF_TIME", DbType.Boolean, objData.SHIFT_WEEKEND_DIFF_TIME, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_WEEKEND_START", DbType.String, objData.SHIFT_WEEKEND_START, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_WEEKEND_END", DbType.String, objData.SHIFT_WEEKEND_END, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_WEEKEND_BREAK_START", DbType.String, objData.SHIFT_WEEKEND_BREAK_START, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_WEEKEND_BREAK_END", DbType.String, objData.SHIFT_WEEKEND_BREAK_END, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_EARLY_SEARCH_HRS", DbType.String, objData.SHIFT_EARLY_SEARCH_HRS, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SHIFT_LATE_SEARCH_HRS", DbType.String, objData.SHIFT_LATE_SEARCH_HRS, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objData.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_ShiftView", CommandType.StoredProcedure, paramColl);

                strError = paramColl[5].value.ToString();
                strSuccess = paramColl[6].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Shift");
            }
        }

        public static clsShift setOldDataForShift(DataTable _dt, clsShift _oldshift)
        {
            try
            {
                _oldshift.REC_ID = _dt.Rows[0]["REC_ID"].ToString();
                _oldshift.SHIFT_ID = _dt.Rows[0]["SHIFT_ID"].ToString();
                _oldshift.SHIFT_DESCRIPTION = _dt.Rows[0]["SHIFT_DESCRIPTION"].ToString();
                _oldshift.SHIFT_ALLOCATION_TYPE = _dt.Rows[0]["SHIFT_ALLOCATION_TYPE"].ToString();
                _oldshift.SHIFT_AUTO_SEARCH_START = _dt.Rows[0]["SHIFT_AUTO_SEARCH_START"].ToString();
                _oldshift.SHIFT_AUTO_SEARCH_END = _dt.Rows[0]["SHIFT_AUTO_SEARCH_END"].ToString();
                _oldshift.SHIFT_TYPE = _dt.Rows[0]["SHIFT_TYPE"].ToString();
                _oldshift.SHIFT_START = _dt.Rows[0]["SHIFT_START"].ToString();
                _oldshift.SHIFT_END = _dt.Rows[0]["SHIFT_END"].ToString();
                _oldshift.SHIFT_BREAK_START = _dt.Rows[0]["SHIFT_BREAK_START"].ToString();
                _oldshift.SHIFT_BREAK_END = _dt.Rows[0]["SHIFT_BREAK_END"].ToString();
                _oldshift.SHIFT_BREAK_HRS = _dt.Rows[0]["SHIFT_BREAK_HRS"].ToString();
                _oldshift.SHIFT_WORKHRS = _dt.Rows[0]["SHIFT_WORKHRS"].ToString();
                _oldshift.SHIFT_FLAG_ADD_BREAK = Convert.ToBoolean(_dt.Rows[0]["SHIFT_FLAG_ADD_BREAK"].ToString());
                _oldshift.SHIFT_WEEKEND_DIFF_TIME = Convert.ToBoolean(_dt.Rows[0]["SHIFT_WEEKEND_DIFF_TIME"].ToString());
                _oldshift.SHIFT_WEEKEND_START = _dt.Rows[0]["SHIFT_WEEKEND_START"].ToString();
                _oldshift.SHIFT_WEEKEND_END = _dt.Rows[0]["SHIFT_WEEKEND_END"].ToString();
                _oldshift.SHIFT_WEEKEND_BREAK_START = _dt.Rows[0]["SHIFT_WEEKEND_BREAK_START"].ToString();
                _oldshift.SHIFT_WEEKEND_BREAK_END = _dt.Rows[0]["SHIFT_WEEKEND_BREAK_END"].ToString();
                _oldshift.SHIFT_EARLY_SEARCH_HRS = _dt.Rows[0]["SHIFT_EARLY_SEARCH_HRS"].ToString();
                _oldshift.SHIFT_LATE_SEARCH_HRS = _dt.Rows[0]["SHIFT_LATE_SEARCH_HRS"].ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Shift");
            }
            return _oldshift;
        }
    }
    public class clsShift
    {

        public String REC_ID { get; set; }
        public String ShiftID { get; set; }
        public String SHIFT_ID { get; set; }
        public String SHIFT_DESCRIPTION { get; set; }
        public String SHIFT_ALLOCATION_TYPE { get; set; }
        public String SHIFT_AUTO_SEARCH_START { get; set; }
        public String SHIFT_AUTO_SEARCH_END { get; set; }
        public String SHIFT_TYPE { get; set; }
        public String SHIFT_START { get; set; }
        public String SHIFT_END { get; set; }
        public String SHIFT_BREAK_START { get; set; }
        public String SHIFT_BREAK_END { get; set; }
        public String SHIFT_BREAK_HRS { get; set; }
        public String SHIFT_WORKHRS { get; set; }
        public Boolean SHIFT_FLAG_ADD_BREAK { get; set; }
        public Boolean SHIFT_WEEKEND_DIFF_TIME { get; set; }
        public String SHIFT_WEEKEND_START { get; set; }
        public String SHIFT_WEEKEND_END { get; set; }
        public String SHIFT_WEEKEND_BREAK_START { get; set; }
        public String SHIFT_WEEKEND_BREAK_END { get; set; }
        public String SHIFT_EARLY_SEARCH_HRS { get; set; }
        public String SHIFT_LATE_SEARCH_HRS { get; set; }
        public String CreatedBy { get; set; }


    }
}
