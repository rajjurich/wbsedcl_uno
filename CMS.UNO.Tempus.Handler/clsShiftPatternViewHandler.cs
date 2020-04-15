using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;

namespace CMS.UNO.Tempus.Handler
{
    public static class clsShiftPatternViewHandler
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
                objDataSet = dataAccessObject.ExecDataSet("sp_ShiftPatternView", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ShiftPattern");
            }
            return objDataSet.Tables[0];
        }

        public static DataTable GetShiftDetails(string strCommand,string PatternID)
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
                paramStruct = new ParamStruct("@ShiftPatternID", DbType.String, PatternID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_ShiftPatternView", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ShiftPattern");
            }
            return objDataSet.Tables[0];
        }

        public static void UpdateShiftDetails(clsShiftPatternView objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ShiftPatternID", DbType.String, objData.PatternID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ShiftPatternDescr", DbType.String, objData.PatternDesr, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ShiftPatternType", DbType.String, objData.PatternType, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ShiftPattern", DbType.String, objData.Pattern, ParameterDirection.Input);
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
                dataAccessObject.ExecNonQuery("sp_ShiftPatternView", CommandType.StoredProcedure, paramColl);

                strError = paramColl[5].value.ToString();
                strSuccess = paramColl[6].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ShiftPattern");
            }
        }


    }

    public class clsShiftPatternView
    {
        public string PatternID { get; set; }
        public string PatternDesr { get; set; }
        public string PatternType { get; set; }
        public string Pattern { get; set; }
        public string CreatedBy { get; set; }
    }
}
