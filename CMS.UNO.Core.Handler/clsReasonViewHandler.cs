using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsReasonViewHandler
    {
        public static DataTable GetAllReason(string strCommand, ref clsReasonView objReason)
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

                paramStruct = new ParamStruct("@RowID", DbType.String, objReason.RecID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetUpdateReasonDetails", CommandType.StoredProcedure, paramColl);

                if (objDataSet.Tables.Count > 0)
                {
                    if (objDataSet.Tables[0].Rows.Count > 0 && objReason.RecID != "")
                    {
                        objReason.ReasonDescr = objDataSet.Tables[0].Rows[0]["Reason_Description"].ToString();
                        objReason.ReasonID = objDataSet.Tables[0].Rows[0]["Reason_ID"].ToString();
                        objReason.ReasonType = objDataSet.Tables[0].Rows[0]["Reason_Type"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Reason");
            }
            return objDataSet.Tables[0];
        }
        public static void InsertReasonDetails(clsReasonView objReason, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ReasonID", DbType.String, objReason.ReasonID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ReasonDescr", DbType.String, objReason.ReasonDescr, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ReasonType", DbType.String, objReason.ReasonType, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objReason.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_GetUpdateReasonDetails", CommandType.StoredProcedure, paramColl);

                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Reason");
            }
        }


    }

    public class clsReasonView
    {
        public string ReasonID { get; set; }
        public string ReasonDescr { get; set; }
        public string ReasonType { get; set; }
        public string RecID { get; set; }
        public string CreatedBy { get; set; }
    }
}