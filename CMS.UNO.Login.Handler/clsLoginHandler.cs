using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.UNO.Framework.DataAccess;
using System.Data;
using CMS.Framework.Common;

namespace CMS.UNO.Login.Handler
{
    public class clsLoginHandler
    {
        public static clsLogin GetLoginDetails(string strCommand, string UserID,string Password)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsLogin objLogin = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@User", DbType.String, UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@password", DbType.String, Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);


                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_LoginView", CommandType.StoredProcedure, paramColl);


                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                objLogin = new clsLogin();

                                if (objDataSet.Tables[0].Columns.Contains("UserID") || objDataSet.Tables[0].Columns["UserID"] != null)
                                    objLogin.UserID = objDataSet.Tables[0].Rows[0]["UserID"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("EmployeeID") || objDataSet.Tables[0].Columns["EmployeeID"] != null)
                                    objLogin.EmployeeID = objDataSet.Tables[0].Rows[0]["EmployeeID"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("DateofCreation") || objDataSet.Tables[0].Columns["DateofCreation"] != null)
                                    objLogin.DateofCreation = objDataSet.Tables[0].Rows[0]["DateofCreation"].ToString();


                                if (objDataSet.Tables[0].Columns.Contains("Password") || objDataSet.Tables[0].Columns["Password"] != null)
                                    objLogin.Password = objDataSet.Tables[0].Rows[0]["Password"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("LevelID") || objDataSet.Tables[0].Columns["LevelID"] != null)
                                    objLogin.LevelID = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["LevelID"].ToString());

                                if (objDataSet.Tables[0].Columns.Contains("EssEnabled") || objDataSet.Tables[0].Columns["EssEnabled"] != null)
                                    objLogin.EssEnabled = Convert.ToString(objDataSet.Tables[0].Rows[0]["EssEnabled"].ToString());

                                if (objDataSet.Tables[0].Columns.Contains("Active") || objDataSet.Tables[0].Columns["Active"] != null)
                                    objLogin.Active = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["Active"].ToString());

                                if (objDataSet.Tables[0].Columns.Contains("IsDeleted") || objDataSet.Tables[0].Columns["IsDeleted"] != null)
                                    objLogin.Isdeleted = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["IsDeleted"].ToString());

                                if (objDataSet.Tables[0].Columns.Contains("InitialLogin") || objDataSet.Tables[0].Columns["InitialLogin"] != null)
                                    objLogin.IsFirstLogin = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["InitialLogin"].ToString());

                                if (objDataSet.Tables[0].Columns.Contains("EmpName") || objDataSet.Tables[0].Columns["EmpName"] != null)
                                    objLogin.EmpName = objDataSet.Tables[0].Rows[0]["EmpName"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("DeletedDate") || objDataSet.Tables[0].Columns["DeletedDate"] != null)
                                    objLogin.DeletedDate = objDataSet.Tables[0].Rows[0]["DeletedDate"].ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
            return objLogin;
        }

        public static string GetLicenceKey(string strCommand)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            string LicenceKey = "";
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_LoginView", CommandType.StoredProcedure, paramColl);


                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            LicenceKey = objDataSet.Tables[0].Rows[0]["LicenseKey"].ToString();
                        }
                        else
                            LicenceKey = "";
                    }
                }
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
            return LicenceKey;
        }

        public static void InsertLog(string strCommand,clsLogin objData)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@User", DbType.String, objData.UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@password", DbType.String, objData.Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@Message", DbType.String, objData.Message, ParameterDirection.Input);
                paramColl.Add(paramStruct);
               
                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_LoginView", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
        }

        public static clsLogin GetPassword(string strCommand, clsLogin objData)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsLogin objLogin = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@EmployeeID", DbType.String, objData.EmployeeID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@uniqueCode", DbType.String, objData.UniqueCode, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@User", DbType.String, objData.UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_LoginView", CommandType.StoredProcedure, paramColl);


                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                objLogin = new clsLogin();


                                if (objDataSet.Tables[0].Columns.Contains("UserID") || objDataSet.Tables[0].Columns["UserID"] != null)
                                    objLogin.UserID = objDataSet.Tables[0].Rows[0]["UserID"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("EmployeeID") || objDataSet.Tables[0].Columns["EmployeeID"] != null)
                                    objLogin.EmployeeID = objDataSet.Tables[0].Rows[0]["EmployeeID"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("Hours") || objDataSet.Tables[0].Columns["Hours"] != null)
                                    objLogin.Hours =Convert.ToInt16(objDataSet.Tables[0].Rows[0]["Hours"].ToString());


                                if (objDataSet.Tables[0].Columns.Contains("UniqueCode") || objDataSet.Tables[0].Columns["UniqueCode"] != null)
                                    objLogin.UniqueCode = objDataSet.Tables[0].Rows[0]["UniqueCode"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("Password") || objDataSet.Tables[0].Columns["Password"] != null)
                                    objLogin.Password = objDataSet.Tables[0].Rows[0]["Password"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("epd_Email") || objDataSet.Tables[0].Columns["epd_Email"] != null)
                                    objLogin.EmailID = objDataSet.Tables[0].Rows[0]["epd_Email"].ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
            return objLogin;
        }

        public static void UpdateUniqueID(string strCommand, clsLogin objData)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@User", DbType.String, objData.UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@password", DbType.String, objData.Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@uniqueCode", DbType.String, objData.UniqueCode, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_LoginView", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
        }
        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        /// <summary>
        /// Function is used to Decrypt the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        public static clsLogin GetLoginDetails(string strCommand, string UserID)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;
            DataSet objDataSet = null;
            clsLogin objLogin = null;
            try
            {

                paramColl = new clsParameterCollection();
                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@User", DbType.String, UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                objDataSet = dataAccessObject.ExecDataSet("sp_ChangePassword", CommandType.StoredProcedure, paramColl);


                if (objDataSet != null)
                {
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                objLogin = new clsLogin();

                                if (objDataSet.Tables[0].Columns.Contains("UserID") || objDataSet.Tables[0].Columns["UserID"] != null)
                                    objLogin.UserID = objDataSet.Tables[0].Rows[0]["UserID"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("EmployeeID") || objDataSet.Tables[0].Columns["EmployeeID"] != null)
                                    objLogin.EmployeeID = objDataSet.Tables[0].Rows[0]["EmployeeID"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("epd_Email") || objDataSet.Tables[0].Columns["epd_Email"] != null)
                                    objLogin.EmailID = objDataSet.Tables[0].Rows[0]["epd_Email"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("Password") || objDataSet.Tables[0].Columns["Password"] != null)
                                    objLogin.Password = objDataSet.Tables[0].Rows[0]["Password"].ToString();

                                if (objDataSet.Tables[0].Columns.Contains("LevelCode") || objDataSet.Tables[0].Columns["LevelCode"] != null)
                                    objLogin.LevelCode = (objDataSet.Tables[0].Rows[0]["LevelCode"].ToString());

                                if (objDataSet.Tables[0].Columns.Contains("InitialLogin") || objDataSet.Tables[0].Columns["InitialLogin"] != null)
                                    objLogin.IsFirstLogin = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["InitialLogin"].ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
            return objLogin;
        }

        public static void UpdatePassword(string strCommand, clsLogin objData)
        {
            clsParameterCollection paramColl = null;
            ParamStruct paramStruct;
            DataAccess dataAccessObject = null;

            try
            {
                paramColl = new clsParameterCollection();

                paramStruct = new ParamStruct("@strCommand", DbType.String, strCommand, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@User", DbType.String, objData.UserID, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                paramStruct = new ParamStruct("@password", DbType.String, objData.Password, ParameterDirection.Input);
                paramColl.Add(paramStruct);

                dataAccessObject = new DataAccess(clsUtility.ConnectionStringName());
                dataAccessObject.ExecNonQuery("sp_ChangePassword", CommandType.StoredProcedure, paramColl);


            }
            catch (Exception ex)
            {
                clsException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Login");
            }
        }


    }

    public class clsLogin
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
        public string DateofCreation { get; set; }
        public string EssEnabled { get; set; }
        public Boolean Active { get; set; }
        public Boolean Isdeleted { get; set; }
        public Boolean IsFirstLogin { get; set; }
        public string DeletedDate { get; set; }
        public string EmpName { get; set; }
        public Int32 LevelID { get; set; }
        public string Message { get; set; }
        public string EmailID { get; set; }
        public string UniqueCode { get; set; }
        public int Hours { get; set; }
        public string LevelCode { get; set; }
    }
    public class clsLoginView
    {
        public string UserID { get; set; }
        public string EmailID { get; set; }
        public string UniqueCode { get; set; }
    }


}
