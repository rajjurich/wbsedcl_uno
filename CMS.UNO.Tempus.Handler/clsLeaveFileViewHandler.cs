using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Tempus.Handler
{
   public static class clsLeaveFileViewHandler
    {
       public static DataTable GetLeaveDetails(string strCommand)
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
               objDataSet = dataAccessObject.ExecDataSet("sp_LeaveFileView", CommandType.StoredProcedure, paramColl);


           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveFileView");
           }
           return objDataSet.Tables[0];
       }
       public static void UpdateLeaveDetails(clsLeaveFileView objData, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
       {
           clsParameterCollection paramColl = null;
           ParamStruct paramStruct;
           DataAccess dataAccessObject = null;

           try
           {
               paramColl = new clsParameterCollection();

               paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@LeaveID", DbType.String, objData.LeaveID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@LeaveDescription", DbType.String, objData.LeaveDescr, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@IsPaid", DbType.Boolean, objData.IsPaid, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@LeaveGroup", DbType.String, objData.LeaveGroup, ParameterDirection.Input);
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
               dataAccessObject.ExecNonQuery("sp_LeaveFileView", CommandType.StoredProcedure, paramColl);

               strError = paramColl[5].value.ToString();
               strSuccess = paramColl[6].value.ToString();

           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveFile");
           }
       }
       public static clsLeaveFileView GetLeaveDetails(string strCommand,Int32 RecID)
       {
           clsParameterCollection paramColl = null;
           ParamStruct paramStruct;
           DataAccess dataAccessObject = null;
           DataSet objDataSet = null;
           clsLeaveFileView objdata = null;
           try
           {

               paramColl = new clsParameterCollection();
               paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
               paramColl.Add(paramStruct);
               paramStruct = new ParamStruct("@RecID", DbType.Int32, RecID, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
               objDataSet = dataAccessObject.ExecDataSet("sp_LeaveFileView", CommandType.StoredProcedure, paramColl);
               if (objDataSet != null)
               {
                   if (objDataSet.Tables.Count > 0)
                   {
                       if (objDataSet.Tables[0].Rows.Count > 0)
                       {
                           objdata = new clsLeaveFileView();
                           objdata.LeaveID = objDataSet.Tables[0].Rows[0]["leave_id"].ToString();
                           objdata.LeaveDescr = objDataSet.Tables[0].Rows[0]["leave_description"].ToString();
                           objdata.IsPaid = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["leave_ispaid"].ToString());
                           objdata.LeaveGroup = objDataSet.Tables[0].Rows[0]["Leave_Group"].ToString();
                       }
                   }
               }

           }
           catch (Exception ex)
           {
               clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveFileView");
           }
           return objdata;
       }
    }

   public class clsLeaveFileView
   {
       public string LeaveID { get; set;}
       public string LeaveDescr { get; set; }
       public Boolean IsPaid { get; set; }
       public string LeaveGroup { get; set; }
       public string CreatedBy { get; set; }
   }
}
