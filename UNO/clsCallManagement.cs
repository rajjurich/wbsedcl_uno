using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace UNO
{
    public class CallObjects
    {
        public Int32 CallId
        { get; set; }
        public String CallNo
        { get; set; }
        public String CreatedBy
        { get; set; }
        public DateTime CreatedOn
        { get; set; }
        public String CallType
        { get; set; }
        public String Remarks
        { get; set; }
        public String OldStatus
        { get; set; }
        public String CurrStatus
        { get; set; }
        public DateTime CloseDate
        { get; set; }
        public string CallTypeCode
        { get; set; }
        public string MainStatus
        { get; set; }
        public string strWhere
        { get; set; }
        public string Location
        { get; set; }

        public string LocDescription
        { get; set; }

        public Boolean IsClosed
        { get; set; }
        public Boolean IsAttended
        { get; set; }
        public Boolean IsReopened
        { get; set; }
        public Boolean IsCritical
        { get; set; }
        public string XML
        { get; set; }
        public int PageSize
        { get; set; }
        public Int32 PageIndex
        { get; set; }
        public string Problem
        { get; set; }
        public string Severity
        { get; set; }
        public string Category
        { get; set; }
        public string FaultType
        { get; set; }
        public string CategoryId
        { get; set; }
        public string ComplaintGroup { get; set; }
        public string PageName { get; set; }
    }

    public class ComplaintCategory
    {
        private Int64 _CategoryId;
        private string _CategoryName;
        private string _CreatedBy;
        private DateTime  _CreatedOn;
        private List<FaultType> _lstFaultType;
        public Int64 CatgoryId
        {
            get
            { return _CategoryId; }
            set
            { _CategoryId = value; }
        }
        public string CategoryName
        {
            get
            { return _CategoryName; }
            set
            { _CategoryName = value; }
        }
        public string CreatedBy
        {
            get
            { return _CreatedBy; }
            set
            { _CreatedBy = value; }
        }
        public DateTime  CreatedOn
        {
            get
            { return _CreatedOn; }
            set
            { _CreatedOn = value; }
        }
        public List<FaultType> lstFaultType
        {
            get { return _lstFaultType; }
            set { _lstFaultType = value; }
        }
        public string PageName { get; set; }

    }
    public class FaultType
    {
        private Int64 _FaultId;
        private Int64 _CategoryId;
        private string _FaultName;
        private string _CreatedBy;
        private DateTime _CreatedOn;
        public Int64 FaultId
        {
            get
            { return _FaultId; }
            set
            { _FaultId = value; }
        }
        public string FaultName
        {
            get
            { return _FaultName; }
            set
            { _FaultName = value; }
        }
        public string CreatedBy
        {
            get
            { return _CreatedBy; }
            set
            { _CreatedBy = value; }
        }
        public DateTime  CreatedOn
        {
            get
            { return _CreatedOn; }
            set
            { _CreatedOn = value; }
        }
        public Int64 CatgoryId
        {
            get
            { return _CategoryId; }
            set
            { _CategoryId = value; }
        }
        public string PageName { get; set; }
    }
    public static class CallManagementHandler
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public static string connString = conn.ConnectionString;

        public static string GetLevelCode(int Levelid)
        {
            try
            {
                DataTable dt = ExecuteQuery("select levelcode from levelmaster where id= " + Levelid + "", connString);
                return dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CallManagement");
                return null;
            }
        }
        private static DataTable ExecuteQuery(string strQuery, String con)
        {

            try
            {
               // SqlDataAdapter adptr = new SqlDataAdapter(strQuery, conn);
                SqlCommand cmd = new SqlCommand(strQuery, conn);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;


                DataTable dt = new DataTable();
                adptr.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CallManagement");
                return null;
            }

        }
        public static DataTable TicketStatusAndCallType(string Identifier, string Module)
        {
            if (Identifier == "tblCategory" && Module == "")

                return ExecuteQuery("select ID as Code,Category as Value,CreatedBy,CreatedOn  from CLM_Complaintcategory where isnull(isdeleted,0)=0 order by Category", connString);
            else if (Identifier == "tblFault" && Module == "")
                return ExecuteQuery("select ID as Code,FaultType as Value,CreatedBy,CreatedOn,CategoryId  from CLM_FaultType where isnull(isdeleted,0)=0 order by FaultType ", connString);
            else if(Identifier=="tblOrgCommon")
                return ExecuteQuery("select distinct Code,Value from ENT_PARAMS where MODULE= 'ACS' and IDENTIFIER='LOCATION' ", connString);
            else

                return ExecuteQuery("Select Code,Value from ENT_PARAMS where identifier= '" + Identifier + "' and module='" + Module + "'", connString);

        }
        public static void ManageCallDetails(CallObjects objCallObj, string CommandText, ref string ErrMsg, ref string strSucMsg)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string QueryString = string.Empty;

                if (CommandText == "Insert")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_ManageEmployeeCallDetails";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@CallType", objCallObj.CallType);
                    cmd.Parameters.AddWithValue("@ComplaintCat", objCallObj.Category);
                    cmd.Parameters.AddWithValue("@Remarks", objCallObj.Remarks);
                    cmd.Parameters.AddWithValue("@Problem", objCallObj.Problem);
                    cmd.Parameters.AddWithValue("@strLocation", objCallObj.Location);
                    cmd.Parameters.AddWithValue("@CurrStatus", objCallObj.CurrStatus);
                    cmd.Parameters.AddWithValue("@strSeverity", objCallObj.Severity);
                    cmd.Parameters.AddWithValue("@ComplaintGroup", objCallObj.ComplaintGroup);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();

                }
                else if (CommandText == "Update")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_ManageEmployeeCallDetails";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@CallType", objCallObj.CallType);
                    cmd.Parameters.AddWithValue("@ComplaintCat", objCallObj.Category);
                    cmd.Parameters.AddWithValue("@Remarks", objCallObj.Remarks);
                    cmd.Parameters.AddWithValue("@Problem", objCallObj.Problem);
                    cmd.Parameters.AddWithValue("@CallNo", objCallObj.CallNo);
                    cmd.Parameters.AddWithValue("@strLocation", objCallObj.Location);
                    cmd.Parameters.AddWithValue("@btClose", objCallObj.IsClosed);
                    cmd.Parameters.AddWithValue("@btReopen", objCallObj.IsReopened);
                    cmd.Parameters.AddWithValue("@strSeverity ", objCallObj.Severity);
                    cmd.Parameters.AddWithValue("@ComplaintGroup ", objCallObj.ComplaintGroup);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();
                }
                else if (CommandText == "Delete")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_ManageEmployeeCallDetails";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@strXML", objCallObj.XML);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();
                }

                else if (CommandText == "InsertFaultType")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_ManageEmployeeCallDetails";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@ComplaintCat", objCallObj.Category);
                    cmd.Parameters.AddWithValue("@FaultType", objCallObj.FaultType);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
            }

        }

        public static DataTable GetCallDetails(CallObjects objCallObj, ref CallObjects objrefCallObj, string CommandText, ref Int32 RecordCount)
        {
            DataTable dt = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string QueryString = string.Empty;

                if (CommandText == "Search")
                {

                    dt = FillData(objCallObj, ref objrefCallObj, CommandText, "USP_ManageEmployeeCallDetails", ref RecordCount);

                }

                else if (CommandText == "OnStatus")
                {

                    dt = FillData(objCallObj, ref objrefCallObj, CommandText, "USP_ManageEmployeeCallDetails", ref RecordCount);

                }
                else if (CommandText == "View")
                {

                    dt = FillData(objCallObj, ref objrefCallObj, CommandText, "USP_ManageEmployeeCallDetails", ref RecordCount);


                }


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
            }

            return dt;
        }

        public static DataTable FillData(CallObjects objCallObj, ref CallObjects objrefCallObj, string cmdText, string Spname, ref Int32 RecordCount)
        {
            SqlDataAdapter da;
            DataTable dt;
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Spname;
            cmd.Parameters.AddWithValue("@strCommand", cmdText);
            cmd.Parameters.AddWithValue("@strWhere", objCallObj.strWhere);
            cmd.Parameters.AddWithValue("@MainStatus", objCallObj.MainStatus);
            cmd.Parameters.AddWithValue("@CurrStatus", objCallObj.CurrStatus);
            cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
            cmd.Parameters.AddWithValue("@PageSize", objCallObj.PageSize);
            cmd.Parameters.AddWithValue("@PageIndex", objCallObj.PageIndex);
            cmd.Parameters.AddWithValue("@RecordCount", '0').Direction = ParameterDirection.Output;
            cmd.Parameters["@RecordCount"].Size = 1000;
            da = new SqlDataAdapter(cmd);

            dt = new DataTable();
            da.Fill(dt);
            RecordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
            if (dt.Rows.Count > 0)
            {
                objrefCallObj = new CallObjects();
                if (dt.Columns.Contains("CallNo") || dt.Columns["CallNo"] != null)
                    objrefCallObj.CallNo = dt.Rows[0]["CallNo"].ToString();

                if (dt.Columns.Contains("CallType") || dt.Columns["CallType"] != null)
                    objrefCallObj.CallType = dt.Rows[0]["CallType"].ToString();

                if (dt.Columns.Contains("CreatedOn") || dt.Columns["CreatedOn"] != null)
                    objrefCallObj.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"].ToString());

                if (dt.Columns.Contains("CreatedBy") || dt.Columns["CreatedBy"] != null)
                    objrefCallObj.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();

                if (dt.Columns.Contains("Remarks") || dt.Columns["Remarks"] != null)
                    objrefCallObj.Remarks = dt.Rows[0]["Remarks"].ToString();

                if (dt.Columns.Contains("CurrStatus") || dt.Columns["CurrStatus"] != null)
                    objrefCallObj.CurrStatus = dt.Rows[0]["CurrStatus"].ToString();

                if (dt.Columns.Contains("Code") || dt.Columns["Code"] != null)
                    objrefCallObj.CallTypeCode = dt.Rows[0]["Code"].ToString();

                if (dt.Columns.Contains("location") || dt.Columns["location"] != null)
                    objrefCallObj.Location = dt.Rows[0]["location"].ToString();

                if (dt.Columns.Contains("LocDescription") || dt.Columns["LocDescription"] != null)
                    objrefCallObj.LocDescription = dt.Rows[0]["LocDescription"].ToString();
                

                if (dt.Columns.Contains("IsClosed") || dt.Columns["IsClosed"] != null)
                    objrefCallObj.IsClosed = Convert.ToBoolean(dt.Rows[0]["IsClosed"]);

                if (dt.Columns.Contains("IsAttended") || dt.Columns["IsAttended"] != null)
                    objrefCallObj.IsAttended = Convert.ToBoolean(dt.Rows[0]["IsAttended"]);

                if (dt.Columns.Contains("IsReopened") || dt.Columns["IsReopened"] != null)
                    objrefCallObj.IsReopened = Convert.ToBoolean(dt.Rows[0]["IsReopened"]);

                if (dt.Columns.Contains("Problem") || dt.Columns["Problem"] != null)
                    objrefCallObj.Problem = dt.Rows[0]["Problem"].ToString();

                if (dt.Columns.Contains("Severity") || dt.Columns["Severity"] != null)
                    objrefCallObj.Severity = dt.Rows[0]["Severity"].ToString();

                if (dt.Columns.Contains("IsCritical") || dt.Columns["IsCritical"] != null)
                    objrefCallObj.IsCritical = Convert.ToBoolean(dt.Rows[0]["IsCritical"]);

                if (dt.Columns.Contains("CatgoryName") || dt.Columns["CatgoryName"] != null)
                    objrefCallObj.Category = dt.Rows[0]["CatgoryName"].ToString();

                if (dt.Columns.Contains("CategoryID") || dt.Columns["CategoryID"] != null)
                    objrefCallObj.CategoryId = dt.Rows[0]["CategoryID"].ToString();
                if (dt.Columns.Contains("ComplaintGroup") || dt.Columns["ComplaintGroup"] != null)
                    objrefCallObj.ComplaintGroup = dt.Rows[0]["ComplaintGroup"].ToString();
            }
            return dt;
        }

        public static DataTable FillFaultType(string CategoryId)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlDataAdapter da;
            DataTable dt = null;
            try
            {
                da = new SqlDataAdapter("EXEC USP_ManageEmployeeCallDetails @strCommand='GetFaultType', @CategoryID= " + Convert.ToInt64(CategoryId) + " ", con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
                return dt;
            }
        }

        public static List<ComplaintCategory> RetriveComplaintCategory()
        {
            DataTable dtCategory, dtFault = null;
            ComplaintCategory objCatgory;
            FaultType objFault;
            List<ComplaintCategory> lstCatgory = new List<ComplaintCategory>();
            List<FaultType> lstFault = null;
            dtCategory = TicketStatusAndCallType("tblCategory", "");
            dtFault = TicketStatusAndCallType("tblFault", "");
            if (dtCategory.Rows.Count > 0)
            {
                for (int i = 0; i <= dtCategory.Rows.Count - 1; i++)
                {
                    lstFault = new List<FaultType>();
                    if (dtFault.Rows.Count > 0)
                    {
                        for (int j = 0; j <= dtFault.Rows.Count - 1; j++)
                        {

                            if (Convert.ToInt64(dtCategory.Rows[i]["Code"]) == Convert.ToInt64(dtFault.Rows[j]["CategoryId"]))
                            {
                                objFault = new FaultType();
                                objFault.FaultId = Convert.ToInt64(dtFault.Rows[j]["Code"]);
                                objFault.FaultName = dtFault.Rows[j]["Value"].ToString();
                                objFault.CreatedBy = dtFault.Rows[j]["CreatedBy"].ToString();
                                objFault.CreatedOn = Convert.ToDateTime(dtFault.Rows[j]["CreatedOn"]);
                                objFault.CatgoryId = Convert.ToInt64(dtFault.Rows[j]["CategoryId"]);
                                lstFault.Add(objFault);
                            }

                        }
                    }
                    objCatgory = new ComplaintCategory();
                    objCatgory.CatgoryId = Convert.ToInt64(dtCategory.Rows[i]["Code"]);
                    objCatgory.CategoryName = dtCategory.Rows[i]["Value"].ToString();
                    objCatgory.CreatedBy = dtCategory.Rows[i]["CreatedBy"].ToString();
                    objCatgory.CreatedOn =Convert.ToDateTime( dtCategory.Rows[i]["CreatedOn"].ToString());
                    objCatgory.lstFaultType = lstFault;
                    lstCatgory.Add(objCatgory);
                   
                }
            }
            return lstCatgory;
        }
        public static void UpdateCategory(ComplaintCategory objCallObj, string CommandText, ref string ErrMsg, ref string strSucMsg)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string QueryString = string.Empty;

                if (CommandText == "UpdateCategory")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_Call_UpdateCategoryFault";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CategoryID", objCallObj.CatgoryId);
                    cmd.Parameters.AddWithValue("@ComplaintCat", objCallObj.CategoryName);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();

                }
                else if (CommandText == "RemoveCategory")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_Call_UpdateCategoryFault";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CategoryID", objCallObj.CatgoryId);
                    cmd.Parameters.AddWithValue("@ComplaintCat", objCallObj.CategoryName);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString(); ;
                }
              

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
            }

        }

        public static void UpdateFault(FaultType objCallObj, string CommandText, ref string ErrMsg, ref string strSucMsg)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string QueryString = string.Empty;

                if (CommandText == "UpdateFault")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_Call_UpdateCategoryFault";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CategoryID", objCallObj.CatgoryId);
                    cmd.Parameters.AddWithValue("@FaultID", objCallObj.FaultId);
                    cmd.Parameters.AddWithValue("@FaultType", objCallObj.FaultName);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();

                }
                else if (CommandText == "RemoveFault")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_Call_UpdateCategoryFault";                 
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CategoryID", objCallObj.CatgoryId);
                    cmd.Parameters.AddWithValue("@FaultID", objCallObj.FaultId);               
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString(); ;
                }


                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
            }

        }

        #region "ManageCallLog"
        public static void ManageLogDetails(CallObjects objCallObj, string CommandText, ref string ErrMsg, ref string strSucMsg)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string QueryString = string.Empty;

                if (CommandText == "Insert")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_ManageEngCallDetails";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CallNo", objCallObj.CallNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@Remarks", objCallObj.Remarks);
                    cmd.Parameters.AddWithValue("@CurrStatus", objCallObj.CurrStatus);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();


                }
                if (CommandText == "InsertInProgress")
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_ManageEngCallDetails";
                    cmd.Parameters.AddWithValue("@strCommand", CommandText);
                    cmd.Parameters.AddWithValue("@CallNo", objCallObj.CallNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", objCallObj.CreatedBy);
                    cmd.Parameters.AddWithValue("@PageName", objCallObj.PageName);
                    cmd.Parameters.AddWithValue("@strErrMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@strErrMsg"].Size = 1000;
                    cmd.Parameters.AddWithValue("@stSuccessMsg", '0').Direction = ParameterDirection.Output;
                    cmd.Parameters["@stSuccessMsg"].Size = 1000;
                    cmd.ExecuteNonQuery();
                    ErrMsg = cmd.Parameters["@strErrMsg"].Value.ToString();
                    strSucMsg = cmd.Parameters["@stSuccessMsg"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
            }

        }

        public static DataTable GetCallLogDetails(CallObjects objCallObj, ref CallObjects objrefCallObj, string CommandText, ref Int32 RecordCount)
        {
            DataTable dt = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string QueryString = string.Empty;

                if (CommandText == "Search")
                {
                    dt = FillData(objCallObj, ref objrefCallObj, CommandText, "USP_ManageEngCallDetails", ref RecordCount);


                }
                if (CommandText == "View")
                {
                    dt = FillData(objCallObj, ref objrefCallObj, CommandText, "USP_ManageEngCallDetails", ref  RecordCount);

                }


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Call_List");
            }
            return dt;

        }
        #endregion
    }

}