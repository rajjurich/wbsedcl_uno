using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;
using System.Data;

namespace CMS.UNO.Core.Handler
{
    public static class clsCompanyHandler
    {


        public static void InsertUpdateCompanyDetails(clsCompany objCompany, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_ID", DbType.String, objCompany.CompId, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_NAME", DbType.String, objCompany.CompName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_ADDRESS", DbType.String, objCompany.CompAddress, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_CITY", DbType.String, objCompany.CompCity, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_PIN", DbType.String, objCompany.CompPin, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_PHONE1", DbType.String, objCompany.CompPhone1, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@CreatedBy", DbType.String, objCompany.CreatedBy, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_PHONE2", DbType.String, objCompany.CompPhone2, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_STATE", DbType.String, objCompany.CompState, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_RO_ADDRESS", DbType.String, objCompany.CompROAddress, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_RO_CITY", DbType.String, objCompany.CompROCity, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_RO_PIN", DbType.String, objCompany.CompROPin, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_RO_STATE", DbType.String, objCompany.CompROState, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_RO_PHONE1", DbType.String, objCompany.CompROPhone1, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@COMPANY_RO_PHONE2", DbType.String, objCompany.CompROPhone2, ParameterDirection.Input);              
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_GetUpdateCompanyDetails", CommandType.StoredProcedure, paramColl);

                strError = paramColl[8].value.ToString();
                strSuccess = paramColl[9].value.ToString();


            }
            catch (Exception ex)
            {
                strError = "You Cannot create Company With "+objCompany.CompId;
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Company");
            }
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
                objDataSet = dataAccessObject.ExecDataSet("sp_GetUpdateCompanyDetails", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Comapny");
            }
            return objDataSet.Tables[0];
        }
        public static clsCompany GetCompanyDetails(string strCommand,string companyID)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsCompany objCompany = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@COMPANY_ID", DbType.String, companyID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetUpdateCompanyDetails", CommandType.StoredProcedure, paramColl);

                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            objCompany = new clsCompany();
                            objCompany.CompId = objDataSet.Tables[0].Rows[0]["COMPANY_ID"].ToString();
                            objCompany.CompName = objDataSet.Tables[0].Rows[0]["COMPANY_NAME"].ToString();
                            objCompany.CompAddress = objDataSet.Tables[0].Rows[0]["COMPANY_ADDRESS"].ToString();
                            objCompany.CompROAddress = objDataSet.Tables[0].Rows[0]["COMPANY_RO_ADDRESS1"].ToString();
                            objCompany.CompPhone1 = objDataSet.Tables[0].Rows[0]["COMPANY_PHONE1"].ToString();
                            objCompany.CompPhone2 = objDataSet.Tables[0].Rows[0]["COMPANY_PHONE2"].ToString();
                            objCompany.CompPin = objDataSet.Tables[0].Rows[0]["COMPANY_PIN"].ToString();
                            objCompany.CompROPin = objDataSet.Tables[0].Rows[0]["COMPANY_RO_PIN"].ToString();
                            objCompany.CompROPhone1 = objDataSet.Tables[0].Rows[0]["COMPANY_RO_PHONE1"].ToString();
                            objCompany.CompROPhone2 = objDataSet.Tables[0].Rows[0]["COMPANY_RO_PHONE2"].ToString();
                            objCompany.CompCity = objDataSet.Tables[0].Rows[0]["COMPANY_CITY"].ToString();
                            objCompany.CompROCity = objDataSet.Tables[0].Rows[0]["COMPANY_RO_CITY"].ToString();
                            objCompany.CompState = objDataSet.Tables[0].Rows[0]["COMPANY_STATE"].ToString();
                            objCompany.CompROState = objDataSet.Tables[0].Rows[0]["COMPANY_RO_STATE"].ToString();

                        }
                    }
                }


            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Comapny");
            }
            return objCompany;
        }
    }

    public class clsCompany
    {
        public string CompId { get; set; }
        public string CompName { get; set; }
        public string CompAddress { get; set; }
        public string CompCity { get; set; }
        public string CompPin { get; set; }
        public string CompPhone1 { get; set; }
        public string CompPhone2 { get; set; }
        public string CompState { get; set; }
        public string CompROAddress { get; set; }
        public string CompROCity { get; set; }
        public string CompROPin { get; set; }
        public string CompROState { get; set; }
        public string CompROPhone1 { get; set; }
        public string CompROPhone2 { get; set; }
        public string CreatedBy { get; set; }


    }
}
