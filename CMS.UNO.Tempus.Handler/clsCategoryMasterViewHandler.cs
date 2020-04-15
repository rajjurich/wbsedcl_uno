using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Tempus.Handler
{
    public static class clsCategoryMasterViewHandler
    {
        public static DataTable GetCategoryDetails(string strCommand)
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
                objDataSet = dataAccessObject.ExecDataSet("sp_Category", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Category");
            }
            return objDataSet.Tables[0];
        }
        public static clsCategory GetCategoryDetails(string strCommand, Int16 CatRowID)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsCategory objCategory = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@CatRowId", DbType.Int16, CatRowID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_Category", CommandType.StoredProcedure, paramColl);

                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            objCategory = new clsCategory();
                            objCategory.CatRowId = Convert.ToInt16(objDataSet.Tables[0].Rows[0]["CAT_ROW_ID"]);
                            objCategory.CatID = objDataSet.Tables[0].Rows[0]["CAT_CATEGORY_ID"].ToString();
                            objCategory.EarlyGoing = objDataSet.Tables[0].Rows[0]["CAT_EARLY_GOING"].ToString();
                            objCategory.LateComing = objDataSet.Tables[0].Rows[0]["CAT_LATE_COMING"].ToString();
                            objCategory.ExtraHrsBeforeShiftHrs = objDataSet.Tables[0].Rows[0]["CAT_EXHRS_BEFORE_SHIFT_HRS"].ToString();
                            objCategory.ExtraHrsAfterShiftHrs = objDataSet.Tables[0].Rows[0]["CAT_EXHRS_AFTER_SHIFT_HRS"].ToString();
                            objCategory.Flag = objDataSet.Tables[0].Rows[0]["Flag"].ToString();
                            objCategory.CatDeductedFromExtHrsEarlyGng =Convert.ToBoolean( objDataSet.Tables[0].Rows[0]["CAT_DED_FROM_EXHRS_EARLY_GOING"]);
                            objCategory.CatDeductedFromExtHrsLateCmg = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["CAT_DED_FROM_EXHRS_LATE_COMING"]);
                            objCategory.CatExtraCheck = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["CAT_EXTRA_CHECK"]);
                            objCategory.CompensatoryCode = Convert.ToString(objDataSet.Tables[0].Rows[0]["CAT_COMPENSATORYOFF_CODE"]);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Category");
            }
            return objCategory;
        }
        public static void UpdateUserDetails(clsCategory objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CatID", DbType.String, objData.CatID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EarlyGoing", DbType.String, objData.EarlyGoing, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@LateComing", DbType.String, objData.LateComing, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ExtraHrsBeforeShiftHrs", DbType.String, objData.ExtraHrsBeforeShiftHrs, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ExtraHrsAfterShiftHrs", DbType.String, objData.ExtraHrsAfterShiftHrs, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CompensatoryCode", DbType.String, objData.CompensatoryCode, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CatExtraCheck", DbType.Boolean, objData.CatExtraCheck, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CatDeductedFromExtHrsEarlyGng", DbType.Boolean, objData.CatDeductedFromExtHrsEarlyGng, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CatDeductedFromExtHrsLateCmg", DbType.Boolean, objData.CatDeductedFromExtHrsLateCmg, ParameterDirection.Input);
                paramColl.Add(paramStruct);            

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objData.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_UpdateCategory", CommandType.StoredProcedure, paramColl);

                strError = paramColl[5].value.ToString();
                strSuccess = paramColl[6].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "User");
            }
        }
    }

    public class clsCategory
    {
        public Int16 CatRowId { get; set; }
        public string CatID { get; set; }
        public string EarlyGoing { get; set; }
        public string LateComing { get; set; }
        public string ExtraHrsBeforeShiftHrs { get; set; }
        public string ExtraHrsAfterShiftHrs { get; set; }
        public string CompensatoryCode { get; set; }
        public string Flag { get; set; }
        public Boolean CatDeductedFromExtHrsEarlyGng { get; set; }
        public Boolean CatDeductedFromExtHrsLateCmg { get; set; }
        public Boolean CatExtraCheck { get; set; }
        public string CreatedBy { get; set; }
    }
}
