using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public class BioMatricPersonalizeTemp
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string FingureForTA { get; set; }
    }

    public class clsBioMetricPersonalizetemplateconfigurationHandler
    {
        public static List<BioMatricPersonalizeTemp> GetCommonData(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            BioMatricPersonalizeTemp objCommon = null;
            List<BioMatricPersonalizeTemp> lstCommon = new List<BioMatricPersonalizeTemp>();
            try
            {
                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input, 100);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Id", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EmpId", DbType.String, "", ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureForTA", DbType.String, "", ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("Sp_BioMetricPersonalizeTemplateConfiguration", CommandType.StoredProcedure, paramColl);

                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                objCommon = new BioMatricPersonalizeTemp();
                                objCommon.Id = Convert.ToInt32(objDataSet.Tables[0].Rows[i]["Id"]);
                                objCommon.EmpId = objDataSet.Tables[0].Rows[i]["EmpId"].ToString();
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

        public static void InsertCommonDetails(BioMatricPersonalizeTemp objCommon, string strCommand, ref string strError, ref string strSuccess)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            clsParameterCollection paramColl1 = null;
            ParamStruct paramStruct1;
            DataAccess dataAccessObject1 = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Id", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);
              
                paramStruct = new ParamStruct("@EmpId", DbType.String, objCommon.EmpId, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureForTA", DbType.String, objCommon.FingureForTA, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("Sp_BioMetricPersonalizeTemplateConfiguration", CommandType.StoredProcedure, paramColl);
                
                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();


                paramColl1 = new clsParameterCollection();
                paramStruct1 = new ParamStruct("@EMPCODE", DbType.String, objCommon.EmpId, ParameterDirection.Input);
                paramColl1.Add(paramStruct1);

                dataAccessObject1 = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject1.ExecNonQuery("PROC_Update_Personalise_EAL_CONFIG", CommandType.StoredProcedure, paramColl1);

            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
        }

        public static void DeleteCommonDetails(string EMPIDS, string strCommand, ref string strError, ref string strSuccess)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Id", DbType.Int32, 0, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EmpId", DbType.String, EMPIDS, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FingureForTA", DbType.String, "", ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("Sp_BioMetricPersonalizeTemplateConfiguration", CommandType.StoredProcedure, paramColl);

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
