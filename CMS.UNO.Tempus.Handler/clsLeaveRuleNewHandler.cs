using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;

namespace CMS.UNO.Tempus.Handler
{
    public static class clsLeaveRuleNewHandler
    {
        public static DataTable GetAllDetails()
        {
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("PROC_GET_LEAVE_RULE_NEW_DETAILS", CommandType.StoredProcedure);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveRuleNew");
            }
            return objDataSet.Tables[0];
        }
        public static DataTable GetLeaveCode()
        {
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("SELECT Leave_ID,replace(convert(char(22),ltrim(Leave_Description))+Leave_ID,' ',' ' )  as  Leave_Description FROM TA_LEAVE_FILE where leave_ISDELETED='0'", CommandType.Text);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveRuleNew");
            }
            return objDataSet.Tables[0];
        }
        public static DataTable Get_PROC_GET_LEAVE_RULE_DETAILS_BYROWID(string rowid)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            try
            {
                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@LR_REC_ID", DbType.String, rowid, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("PROC_GET_LEAVE_RULE_DETAILS_BYROWID", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveRuleNew");
            }
            return objDataSet.Tables[0];
        }
        public static void UpdateLeaveDetails(clsLeaveRuleNew objData, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

              
                paramStruct = new ParamStruct("@LR_REC_ID", DbType.String, objData.lrrecid, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_CODE", DbType.String, objData.lrCategory + objData.lrLeaveid, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_CATEGORYID", DbType.String, objData.lrCategory, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_ALLOTMENT", DbType.String, objData.lrAllotment, ParameterDirection.Input);
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

                paramStruct = new ParamStruct("@LR_ACCUMULATION", DbType.String, objData.lrAccumulation, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LeaveID", DbType.String, objData.lrLeaveid, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_MinDaysAllowed", DbType.String, objData.strMinDays, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_MaxDaysAllowed", DbType.String, objData.strMaxDays, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_AllotmentType", DbType.String, objData.strAllotmentType, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LR_DAYS", DbType.String, objData.strDays, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                if (objData.strAllotmentType == "Y")
                {
                    paramStruct = new ParamStruct("@LR_AllotmentType_YE_PR", DbType.String, objData.strAllotmentType_YE_PR, ParameterDirection.Input);
                    paramColl.Add(paramStruct);
                }

                if (objData.strDays == "R")
                {

                    paramStruct = new ParamStruct("@LEAVE_RULE", DbType.String, objData.strLeaveRule, ParameterDirection.Input);
                    paramColl.Add(paramStruct);

                    if (objData.strLeaveRule == "O")
                    {
                        paramStruct = new ParamStruct("@LR_GreaterOrLesser", DbType.String, objData.Value, ParameterDirection.Input);
                        paramColl.Add(paramStruct);
                    }
                }

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("PROC_SAVE_Leave_Rule_New", CommandType.StoredProcedure, paramColl);

                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();       

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveFile");
            }
        }

        public static void DeleteLeave(clsLeaveRuleNew objData, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

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
                dataAccessObject.ExecNonQuery("PROC_DELETE_LEAVE_RULE_DETAILS_BYROWID", CommandType.StoredProcedure, paramColl);

                strError = paramColl[0].value.ToString();
                strSuccess = paramColl[1].value.ToString();

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveFile");
            }
        }
    }

    public class clsLeaveRuleNew
    {

        public string lrCode { get; set; }
        public string lrAllotment { get; set; }
        public string lrAccumulation { get; set; }
        public string lrCategory { get; set; }
        public string lrrecid { get; set; }
        public string lrLeaveid { get; set; }
        public string strDays { get; set; }
        public string strLeaveRule { get; set; }
        public string Value { get; set; }
        public string strMinDays { get; set; }
        public string strMaxDays { get; set; }
        public string strAllotmentType { get; set; }
        public string strAllotmentType_YE_PR { get; set; }
        public string CreatedBy { get; set; }

    }


}
