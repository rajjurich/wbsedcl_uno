using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CMS.UNO.Core.Handler
{
   public static class clsCommonViewHandler
    {

       public static List<clsCommon> GetCommonData(string strCommand)
       {
           clsParameterCollection paramColl = null;
           ParamStruct paramStruct;
           DataAccess dataAccessObject = null;
           DataSet objDataSet = null;
           clsCommon objCommon = null;
           List<clsCommon> lstCommon = new List<clsCommon>();
           try
           {
                
               paramColl = new clsParameterCollection();
               paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input, 100);
               paramColl.Add(paramStruct);
               dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
               objDataSet = dataAccessObject.ExecDataSet("Sp_ViewUpdateCommonDetails", CommandType.StoredProcedure, paramColl);

               if (objDataSet != null)
               {
                   if (objDataSet.Tables.Count > 0)
                   {
                       if (objDataSet.Tables[0].Rows.Count > 0)
                       {
                           for(int i=0;i<=objDataSet.Tables[0].Rows.Count-1;i++)
                           {
                           objCommon = new clsCommon();
                           objCommon.ID = objDataSet.Tables[0].Rows[i]["ID"].ToString();
                           objCommon.ENTID = objDataSet.Tables[0].Rows[i]["ENTID"].ToString();
                           objCommon.Description = objDataSet.Tables[0].Rows[i]["Descr"].ToString();
                           objCommon.CompanyID = objDataSet.Tables[0].Rows[i]["CompanyID"].ToString();
                           lstCommon.Add(objCommon);
                           }
                       }
                   }
               }
              
           }
           catch (Exception ex)
           {
            clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace,"Common");
            return null;
           }
           return lstCommon;
       }



       public static DataSet GetCommonData1(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
           try
           {                
               paramColl = new clsParameterCollection();
               paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input, 100);
               paramColl.Add(paramStruct);

               dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
               objDataSet = dataAccessObject.ExecDataSet("Sp_ViewUpdateCommonDetails", CommandType.StoredProcedure, paramColl);              
           }
           catch (Exception ex)
           {
            clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace,"Common");
            return null;
           }
           return objDataSet;
       }





       public static void InsertCommonDetails(clsCommon objCommon, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
       {
           clsParameterCollection paramColl = null;
           ParamStruct paramStruct;
           DataAccess dataAccessObject = null;
        
           try
           {
               paramColl = new clsParameterCollection();

              
               paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@oce_id", DbType.String, objCommon.ID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@Cem_entity_ID", DbType.String, objCommon.ENTID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@oce_description", DbType.String, objCommon.Description, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@strErrorMsg", DbType.String, "", ParameterDirection.Output,300);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@strSuccessMsg", DbType.String, "", ParameterDirection.Output,300);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@CreatedBy", DbType.String, objCommon.CreatedBy, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@CompanyID", DbType.String, objCommon.CompanyID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
               dataAccessObject.ExecNonQuery("Sp_ViewUpdateCommonDetails", CommandType.StoredProcedure, paramColl);

               strError = paramColl[4].value.ToString();
               strSuccess = paramColl[5].value.ToString();
              

           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
           }
       }

    }
   public  class clsCommon
   {
       public string CompanyID { get; set; }
       public string ENTID { get; set; }
       public string ID { get; set; }
       public string Description { get; set; }
       public string CreatedBy { get; set; }
   }
}
