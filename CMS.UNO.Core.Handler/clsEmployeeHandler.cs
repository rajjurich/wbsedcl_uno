using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Core.Handler
{
    public static class clsEmployeeHandler
    {
        public static void InsertUpdateEmployeeDetails(clsEmployee objEmployee, string strCommand, string strXML, ref string strError, ref string strSuccess, string PageName)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();


                paramStruct = new ParamStruct("@CommandText", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@employeeID", DbType.String, objEmployee.EMPLOYEEID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@SALUTATION", DbType.String, objEmployee.SALUTATION, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@FName", DbType.String, objEmployee.FName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strErrOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strSuccOut", DbType.String, "", ParameterDirection.Output, 300);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@MName", DbType.String, objEmployee.MName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Lname", DbType.String, objEmployee.Lname, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@NickName", DbType.String, objEmployee.NickName, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@PreviousId", DbType.String, objEmployee.PreviousId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@tempCardId", DbType.String, objEmployee.tempCardId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Gender", DbType.String, objEmployee.Gender, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@MaritalStatus", DbType.String, objEmployee.MaritalStatus, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@DOB", DbType.String, objEmployee.DOB, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Religion", DbType.String, objEmployee.Religion, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Ref1", DbType.String, objEmployee.Ref1, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@ref2", DbType.String, objEmployee.Ref2, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@bldGroup", DbType.String, objEmployee.bldGroup, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@email", DbType.String, objEmployee.email, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Domicile", DbType.String, objEmployee.Domicile, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Pan", DbType.String, objEmployee.Pan, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@Doctor", DbType.String, objEmployee.Doctor, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PreCardStatus", DbType.String, objEmployee.PreCardStatus, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@cardID", DbType.String, objEmployee.cardID, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PhotoUrl", DbType.String, objEmployee.PhotoUrl, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@AdharCardNo", DbType.String, objEmployee.AdharCardNo, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PAddress", DbType.String, objEmployee.PAddress, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PCity", DbType.String, objEmployee.PCity, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PPin", DbType.String, objEmployee.PPin, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PState", DbType.String, objEmployee.PState, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PCOUNTRY", DbType.String, objEmployee.PCOUNTRY, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PPhone1", DbType.String, objEmployee.PPhone1, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@PPhone2", DbType.String, objEmployee.PPhone2, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@TAddress", DbType.String, objEmployee.TAddress, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@TCity", DbType.String, objEmployee.TCity, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@TPin", DbType.String, objEmployee.TPin, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@TState", DbType.String, objEmployee.TState, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@TCOUNTRY", DbType.String, objEmployee.TCOUNTRY, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                paramStruct = new ParamStruct("@TPhone1", DbType.String, objEmployee.TPhone1, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@TPhone2", DbType.String, objEmployee.TPhone2, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@joiningDate", DbType.String, objEmployee.joiningDate, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@confirmDate", DbType.String, objEmployee.confirmDate, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@retirementDate", DbType.String, objEmployee.retirementDate, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@RetirementReasonId", DbType.String, objEmployee.RetirementReasonId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@companyId", DbType.String, objEmployee.companyId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@LocationId", DbType.String, objEmployee.LocationId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@DivisionId", DbType.String, objEmployee.DivisionId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@DepartmentId", DbType.String, objEmployee.DepartmentId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@designationId", DbType.String, objEmployee.designationId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@CategoryId", DbType.String, objEmployee.CategoryId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@GroupID", DbType.String, objEmployee.GroupID, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                paramStruct = new ParamStruct("@GradeId", DbType.String, objEmployee.GradeId, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EodStaus", DbType.String, objEmployee.EodStaus, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Password", DbType.String, objEmployee.Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@IsESSEnabled", DbType.String, objEmployee.IsESSEnabled, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@MgrId", DbType.String, objEmployee.MgrId, ParameterDirection.Input);
                paramColl.Add(paramStruct);
              

              paramStruct = new ParamStruct("@PageName", DbType.String, PageName, ParameterDirection.Input);
               paramColl.Add(paramStruct);

               paramStruct = new ParamStruct("@CreatedBy", DbType.String, objEmployee.CreatedBy, ParameterDirection.Input);
               paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@strXML", DbType.String, strXML, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("USP_Insert_EmployeeDetails", CommandType.StoredProcedure, paramColl);

                strError = paramColl[4].value.ToString();
                strSuccess = paramColl[5].value.ToString();


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Common");
            }
        }

        public static DataTable GetEmployeeManagerDetails(string EmpStatus, string strCommand, string EmployeeID,string UserID)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
           
            try
            {
                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@EmpStatus", DbType.String, EmpStatus, ParameterDirection.Input);
                paramColl.Add(paramStruct);

              
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

               
                paramStruct = new ParamStruct("@EmpId", DbType.String, EmployeeID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@USeriD", DbType.String, UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_GetEmployeeManagerDetails", CommandType.StoredProcedure, paramColl);

            }
            catch (Exception ex)
            {

                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Holiday");
            }
            return objDataSet.Tables[0];
        }

    }
    public class clsEmployee
    {
        public string EMPLOYEEID { get; set; }
        public string SALUTATION { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string Lname { get; set; }
        public string NickName { get; set; }
        public string PreviousId { get; set; }
        public string tempCardId { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string DOB { get; set; }
        public string Religion { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string bldGroup { get; set; }
        public string email { get; set; }
        public string Domicile { get; set; }
        public string Pan { get; set; }
        public string Doctor { get; set; }
        public string PreCardStatus { get; set; }
        public string cardID { get; set; }
        public string PhotoUrl { get; set; }
        public string AdharCardNo { get; set; }
        public string TAddress { get; set; }
        public string TCity { get; set; }
        public string TPin { get; set; }
        public string TState { get; set; }
        public string TCOUNTRY { get; set; }
        public string TPhone1 { get; set; }
        public string TPhone2 { get; set; }
        public string PAddress { get; set; }
        public string PCity { get; set; }
        public string PPin { get; set; }
        public string PState { get; set; }
        public string PCOUNTRY { get; set; }
        public string PPhone1 { get; set; }
        public string PPhone2 { get; set; }
        public string confirmDate { get; set; }
        public string joiningDate { get; set; }
        public string retirementDate { get; set; }
        public string RetirementReasonId { get; set; }
        public string companyId { get; set; }
        public string LocationId { get; set; }
        public string DivisionId { get; set; }
        public string DepartmentId { get; set; }
        public string designationId { get; set; }
        public string CategoryId { get; set; }
        public string GroupID { get; set; }
        public string GradeId { get; set; }
        public string EodStaus { get; set; }
        public string Password { get; set; }
        public string MgrId { get; set; }
        public string CreatedBy { get; set; }
        public bool IsESSEnabled { get; set; }
        
    }
}
