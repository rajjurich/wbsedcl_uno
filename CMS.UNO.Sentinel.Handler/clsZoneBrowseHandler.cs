
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CMS.Framework.Common;
using CMS.UNO.Framework.DataAccess;

namespace CMS.UNO.Sentinel.Handler
{
   public static class clsZoneBrowseHandler
    {
       public static DataTable GetAllDetails(string strCommand,string LevelID)
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

               paramStruct = new ParamStruct("@level", DbType.String,LevelID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
               objDataSet = dataAccessObject.ExecDataSet("sp_ZoneBrowse", CommandType.StoredProcedure, paramColl);


           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ZoneBrowse");
           }
           return objDataSet.Tables[0];
       }
       public static DataSet GetAllDetails(string strCommand, string LevelID, string ZoneID)
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

               paramStruct = new ParamStruct("@level", DbType.String, LevelID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@ZoneID", DbType.String, ZoneID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
               objDataSet = dataAccessObject.ExecDataSet("sp_ZoneBrowse", CommandType.StoredProcedure, paramColl);


           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ZoneBrowse");
           }
           return objDataSet;
       }
       public static int GetCount()
       {
           DataAccess dataAccessObject = null;
           dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
           return Convert.ToInt16(dataAccessObject.ExecScalar("select ISNULL(max(zone_id)+1,1) as id from zone", CommandType.Text));
       }
       public static void InsertZoneBrowseDetails(clsZoneBrowse objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
       {
           clsParameterCollection paramColl = null;
           ParamStruct paramStruct;
           DataAccess dataAccessObject = null;

           try
           {
               paramColl = new clsParameterCollection();

               paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@ZoneID", DbType.String, objData.Zone_ID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@ZONE_DESCRIPTION", DbType.String, objData.ZONE_DESCRIPTION, ParameterDirection.Input);
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
               dataAccessObject.ExecNonQuery("sp_ZoneBrowse", CommandType.StoredProcedure, paramColl);

               strError = paramColl[3].value.ToString();
               strSuccess = paramColl[4].value.ToString();

           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ZoneBrowse");
           }
       }

       public static void UpdateZoneBrowseDetails(clsZoneBrowse objData, string strXML, ref string strError, ref string strSuccess, string PageName)
       {
           clsParameterCollection paramColl = null;
           ParamStruct paramStruct;
           DataAccess dataAccessObject = null;

           try
           {
               paramColl = new clsParameterCollection();



               paramStruct = new ParamStruct("@zoneID", DbType.String, objData.Zone_ID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@ZONE_DESCRIPTION", DbType.String, objData.ZONE_DESCRIPTION, ParameterDirection.Input);
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
               dataAccessObject.ExecNonQuery("zoneEdit", CommandType.StoredProcedure, paramColl);

               strError = paramColl[2].value.ToString();
               strSuccess = paramColl[3].value.ToString();

           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "zoneBrowse");
           }
       }

    }

   public class clsZoneBrowse
   {
       public string Zone_ID { get; set; }
       public string ZONE_DESCRIPTION { get; set; }
       public string CreatedBy { get; set; }
   }
}
