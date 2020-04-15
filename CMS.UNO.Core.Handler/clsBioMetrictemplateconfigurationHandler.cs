using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public class BioMatricTemp
    {
        public int NoOfFingers { get; set; }
        public string FingureCount { get; set; }
        public string FingureForTA { get; set; }
    }
    public class clsBioMetrictemplateconfigurationHandler
    {
        public static List<BioMatricTemp> GetCommonData(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            BioMatricTemp objCommon = null;
            List<BioMatricTemp> lstCommon = new List<BioMatricTemp>();
            try
            {
                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input, 100);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@NoOfFingers", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureCount", DbType.String, "", ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureForTA", DbType.String, "", ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("Sp_BioMetricTemplateConfiguration", CommandType.StoredProcedure, paramColl);

                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                objCommon = new BioMatricTemp();
                                objCommon.NoOfFingers = Convert.ToInt32(objDataSet.Tables[0].Rows[i]["NoOfFingers"]);
                                objCommon.FingureCount = objDataSet.Tables[0].Rows[i]["FingureCount"].ToString();
                                objCommon.FingureForTA = objDataSet.Tables[0].Rows[i]["FingureForTA"].ToString();
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

        public static void InsertCommonDetails(BioMatricTemp objCommon, string strCommand, ref string strError, ref string strSuccess)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@NoOfFingers", DbType.Int32, objCommon.NoOfFingers, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureCount", DbType.String, objCommon.FingureCount, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureForTA", DbType.String, objCommon.FingureForTA, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("Sp_BioMetricTemplateConfiguration", CommandType.StoredProcedure, paramColl);

                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
        }        
    }
}
