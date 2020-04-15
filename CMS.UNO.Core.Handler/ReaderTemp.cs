using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public class ReaderTemp
    {
        public int ReaderID { get; set; }
        public int ControllerID { get; set; }
        public int EventID { get; set; }
        public string EventMessage { get; set; }
        public string EventName { get; set; }
        public string ControllerName { get; set; }
    }

    public class RederTemplate
    {
        public static List<ReaderTemp> GetCommonData(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            ReaderTemp objCommon = null;
            List<ReaderTemp> lstCommon = new List<ReaderTemp>();
            try
            {
                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input, 100);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ReaderID", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ControllerID", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EventID", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EventMessage", DbType.String, "", ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("Sp_ReaderTemplate", CommandType.StoredProcedure, paramColl);

                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                objCommon = new ReaderTemp();
                                objCommon.ReaderID = Convert.ToInt32(objDataSet.Tables[0].Rows[i]["ReaderID"]);
                                objCommon.ControllerID = Convert.ToInt32(objDataSet.Tables[0].Rows[i]["ControllerID"]);
                                objCommon.ControllerName = objDataSet.Tables[0].Rows[i]["CTLR_DESCRIPTION"].ToString();
                                objCommon.EventID = Convert.ToInt32(objDataSet.Tables[0].Rows[i]["EventID"]);
                                objCommon.EventName = objDataSet.Tables[0].Rows[i]["EventName"].ToString();
                                objCommon.EventMessage = objDataSet.Tables[0].Rows[i]["EventMessage"].ToString();
                                lstCommon.Add(objCommon);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
                return null;
            }
            return lstCommon;
        }

        public static void InsertCommonDetails(ReaderTemp objCommon, string strCommand, ref string strError, ref string strSuccess)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ReaderID", DbType.Int32, objCommon.ReaderID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@ControllerID", DbType.Int32, objCommon.ControllerID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EventID", DbType.Int32, objCommon.EventID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EventMessage", DbType.String, objCommon.EventMessage, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("Sp_ReaderTemplate", CommandType.StoredProcedure, paramColl);

                strError = paramColl[5].value.ToString();
                strSuccess = paramColl[6].value.ToString();
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
        }
    }
}
