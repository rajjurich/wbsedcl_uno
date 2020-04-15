using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using Morpho.MorphoAcquisition;
using System.Text;
using System.Configuration;
using System.Web.Services;

namespace UNO
{
    public partial class FingerEnrollment : System.Web.UI.Page
    {
        string FormatType = "";
        //public ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();

        bool _isConfirmNeeded = true;
        string _confirmMessage = string.Empty;
        public bool IsConfirmNeeded
        {
            get { return _isConfirmNeeded; }
            set { _isConfirmNeeded = value; }
        }

        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { _confirmMessage = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IsConfirmNeeded = true;
            ConfirmMessage = "Place User Card on reader";

            // Insure that the __doPostBack() JavaScript is added to the page...
            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (IsPostBack)
            {
                string eventTarget = Request["__EVENTTARGET"] ?? string.Empty;
                string eventArgument = Request["__EVENTARGUMENT"] ?? string.Empty;


                switch (eventTarget)
                {
                    case "UserConfirmationPostBack":
                        if (Convert.ToBoolean(eventArgument))
                        {
                            // User said yes do it...
                            WriteOnCard();
                            btnWrite.Enabled = false;

                        }
                        else
                        {
                            // User said NOT to do it...
                            //this.lblMsg.Text = "No";

                        }
                        break;
                }
            }

            if (!Page.IsPostBack)
            {
                //  RootPath.Value = this.Server.MapPath("~//Enrollment//NativeTemplate.exe");
                RootPath.Value = @"C:\Program Files\CMS\Enrollment\NativeTemplate.exe";
                clsCardRW objcard = new clsCardRW();
                objcard.CardSettings();
                FillData();
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        [WebMethod]
        public static string GetISOTemplate(string EmployeeCode)
        {
            string json = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("spGetISOTemplate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EmployeeCode;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                json = "False";
            }
            else
            {
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("ISOTemplate1", typeof(string));
                dt.Columns.Add("ISOTemplate2", typeof(string));
                dt.Columns.Add("ActivationDate", typeof(int));
                dt.Columns.Add("ExpiryDate", typeof(int));
                dt.Columns.Add("AadharNo", typeof(string));
                dt.Columns.Add("CenterCode", typeof(string));
                dt.Columns.Add("LocationCode", typeof(string));
                DataRow dr = dt.NewRow();
                dr["ISOTemplate1"] = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["ISOTemplate1"]);
                dr["ISOTemplate2"] = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["ISOTemplate2"]);
                dr["ActivationDate"] = ds.Tables[0].Rows[0]["ActivationDate"];
                dr["ExpiryDate"] = ds.Tables[0].Rows[0]["ExpiryDate"];
                dr["AadharNo"] = ds.Tables[0].Rows[0]["AadharNo"];
                dr["CenterCode"] = ds.Tables[0].Rows[0]["CenterCode"];
                dr["LocationCode"] = ds.Tables[0].Rows[0]["LocationCode"];
                dt.Rows.Add(dr);
                ds1.Tables.Add(dt);
                ds1.AcceptChanges();
                json = ds1.GetXml();
            }
            return json;
        }

        [WebMethod]
        public static string GetTemplate(string txtEmpCd)
        {
            DataSet ds = new DataSet();
            string dtResult = "";
            byte[] data = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // string strsql = "select top 1 * from ENT_User";
                string strsql = "EXEC USP_Finger_Template @EmpCode = '" + txtEmpCd + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.Fill(ds);
                //  dtResult = ds.GetXml();
                //   dtResult =Convert.ToString(ds.Tables[0].Rows[0]["TempMin2"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //data = (byte[])ds.Tables[0].Rows[0]["TempMin1"];
                    //data = (byte[])ds.Tables[0].Rows[0]["TempMin2"];
                    //data = (byte[])ds.Tables[0].Rows[0]["Format_Type"];

                    //data = (byte[])ds.Tables[0].Rows[0]["ISOFingerImg1"];
                    //data = (byte[])ds.Tables[0].Rows[0]["ISOFingerImg2"];
                    DataSet ds1 = new DataSet();
                    DataTable dt;
                    DataRow dr;
                    int i = 0;
                    dt = new DataTable();


                    dt.Columns.Add("TempMin1", typeof(string));
                    dt.Columns.Add("TempMin2", typeof(string));
                    dt.Columns.Add("ISOFingerImg1", typeof(string));
                    dt.Columns.Add("ISOFingerImg2", typeof(string));
                    dt.Columns.Add("Format_Type", typeof(string));
                    string TempMin1 = null;
                    string TempMin2 = null;
                    string ISOFingerImg1 = null;
                    string ISOFingerImg2 = null;

                    if (Convert.ToString(ds.Tables[0].Rows[0]["TempMin1"]) != "")
                    {
                        TempMin1 = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["TempMin1"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["TempMin2"]) != "")
                    {
                        TempMin2 = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["TempMin2"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["ISOFingerImg1"]) != "")
                    {
                        ISOFingerImg1 = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["ISOFingerImg1"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["ISOFingerImg2"]) != "")
                    {
                        ISOFingerImg2 = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["ISOFingerImg2"]);
                    }

                    dt.Rows.Add(TempMin1, TempMin2, ISOFingerImg1, ISOFingerImg2, Convert.ToString(ds.Tables[0].Rows[0]["Format_Type"]));
                    ds1.Tables.Add(dt);
                    ds1.AcceptChanges();
                    dtResult = ds1.GetXml();
                }
            }
            catch (Exception ex)
            {

            }
            return dtResult;
        }
        protected void btnEnroll_Click(object sender, EventArgs e)
        {

            string strsql = " Select * from Finger_Template where IsDeleted = 'false' and " +
                            " Format_Type = '" + ddlFormat.SelectedItem.ToString() + "' and EmployeeCD = '" + txtEmpCd.Text.Trim() + "' ";

            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count >= 1)
            {
                string someScript2 = "";
                someScript2 = "<script language='javascript'>alert('User has already enrolled " + ddlFormat.SelectedItem.ToString() + " Template');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                return;
            }

            if (ddlFormat.SelectedItem.ToString() == Session["FormatType"].ToString())
            {
                string someScript2 = "";
                someScript2 = "<script language='javascript'>alert('User has already enrolled " + ddlFormat.SelectedItem.ToString() + " Template');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                return;
            }
            //btnEnroll.Enabled = false;
            hdnparm.Value = txtEmpCd.Text + " " + "ISO" + " " + "Enrollment";
            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>executeCommands()</script>");
        }

        private void FillData()
        {
            txtEmpCd.Text = Session["EmpCd"].ToString();
            txtCardCd.Text = Session["CardCd"].ToString();
            txtEmpName.Text = Session["EmpName"].ToString();
            txtDept.Text = Session["Department"].ToString();
            txtDesignation.Text = Session["Designation"].ToString();

            string Status = Session["Status"].ToString();
            if (Status == "Not Enrolled")
            {
                btnEnroll.Enabled = true;
                //btnWrite.Enabled = false;
            }
            else if (Status == "Enrolled")
            {
                ddlFormat.Text = Session["FormatType"].ToString();
                if (Session["FormatType"].ToString() == "Both")
                {
                    btnEnroll.Enabled = false;
                    ddlFormat.Enabled = false;
                }
                else
                {
                    btnEnroll.Enabled = true;
                    ddlFormat.Items.Remove("Both");
                }
                //btnWrite.Enabled = true;
            }
            if (Status == "Card Issue")
            {
                ddlFormat.Text = Session["FormatType"].ToString();
                if (Session["FormatType"].ToString() == "Both")
                {
                    ddlFormat.Enabled = false;
                    btnEnroll.Enabled = false;
                    //btnWrite.Enabled = false;
                }
                else
                {
                    btnEnroll.Enabled = true;
                    ddlFormat.Items.Remove("Both");
                    //btnWrite.Enabled = false;
                }
            }

        }

        private void WriteOnCard()
        {
            string strsql = "";
            try
            {
                if (ddlFormat.Text == "Native")
                {
                    strsql = "select " + clsCardRW.finger_val1 + "," + clsCardRW.finger_val2 + ",FingerQuality1,FingerQuality2 from Finger_Template " +
                             "where Format_Type = 'Native' and EmployeeCD = '" + txtEmpCd.Text + "' and Isdeleted = 'false'";

                    SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        byte[] NativeTemplate1 = (byte[])dt.Rows[0][clsCardRW.finger_val1];
                        byte[] NativeTemplate2 = (byte[])dt.Rows[0][clsCardRW.finger_val2];
                        int FingerQty1 = Convert.ToInt32(dt.Rows[0]["FingerQuality1"]);
                        int FingerQty2 = Convert.ToInt32(dt.Rows[0]["FingerQuality2"]);

                        clsCardRW objclsCardRW = new clsCardRW();
                        string strCardWrite = objclsCardRW.WriteCard(NativeTemplate1, NativeTemplate2, FingerQty1, FingerQty2);
                        string Message = "";
                        if (strCardWrite != "")
                        {
                            Message = "Failed to write native template on card. " + strCardWrite;
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                            return;
                        }
                        else
                        {
                            Message = "Successfully written native template on card";
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

                            clsCardRW.Execute = false;
                            SqlConnection conn = new SqlConnection(AccessController.m_connecton);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "Update Finger_Template set CardIssue = 'true' where EmployeeCD = '" + txtEmpCd.Text + "' and IsDeleted = 'false'";
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                else if (ddlFormat.Text == "ISO")
                {
                    strsql = "select " + clsCardRW.ISOfinger_val1 + "," + clsCardRW.ISOfinger_val2 + ",ISOFingerQuality1,ISOFingerQuality2 from Finger_Template " +
                             "where Format_Type = 'ISO' and EmployeeCD = '" + txtEmpCd.Text + "' and Isdeleted = 'false'";

                    SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        byte[] ISOTemplate1 = (byte[])dt.Rows[0][clsCardRW.ISOfinger_val1];
                        byte[] ISOTemplate2 = (byte[])dt.Rows[0][clsCardRW.ISOfinger_val2];
                        int ISOFingerQty1 = Convert.ToInt32(dt.Rows[0]["ISOFingerQuality1"]);
                        int ISOFingerQty2 = Convert.ToInt32(dt.Rows[0]["ISOFingerQuality2"]);

                        clsCardRW objclsCardRW = new clsCardRW();
                        string strCardWrite = objclsCardRW.ISOWriteCard(ISOTemplate1, ISOTemplate2, ISOFingerQty1, ISOFingerQty2);
                        string Message = "";
                        if (strCardWrite != "")
                        {
                            Message = "Failed to write ISO template on card. " + strCardWrite;
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                            return;
                        }
                        else
                        {
                            Message = "Successfully written ISO template on card";
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

                            clsCardRW.Execute = false;
                            SqlConnection conn = new SqlConnection(AccessController.m_connecton);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "Update Finger_Template set CardIssue = 'true' where EmployeeCD = '" + txtEmpCd.Text + "' and IsDeleted = 'false'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (ddlFormat.Text == "Both")
                {
                    strsql = "select " + clsCardRW.finger_val1 + "," + clsCardRW.finger_val2 + "," + clsCardRW.ISOfinger_val1 + "," + clsCardRW.ISOfinger_val2 + ", " +
                             "FingerQuality1,FingerQuality2,ISOFingerQuality1,ISOFingerQuality2 from Finger_Template " +
                             "where Format_Type = 'Both' and EmployeeCD = '" + txtEmpCd.Text + "' and Isdeleted = 'false'";

                    SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        byte[] NativeTemplate1 = (byte[])dt.Rows[0][clsCardRW.finger_val1];
                        byte[] NativeTemplate2 = (byte[])dt.Rows[0][clsCardRW.finger_val2];
                        int FingerQty1 = Convert.ToInt32(dt.Rows[0]["FingerQuality1"]);
                        int FingerQty2 = Convert.ToInt32(dt.Rows[0]["FingerQuality2"]);

                        byte[] ISOTemplate1 = (byte[])dt.Rows[0][clsCardRW.ISOfinger_val1];
                        byte[] ISOTemplate2 = (byte[])dt.Rows[0][clsCardRW.ISOfinger_val2];
                        int ISOFingerQty1 = Convert.ToInt32(dt.Rows[0]["ISOFingerQuality1"]);
                        int ISOFingerQty2 = Convert.ToInt32(dt.Rows[0]["ISOFingerQuality2"]);

                        clsCardRW objclsCardRW = new clsCardRW();
                        string strCardWrite = objclsCardRW.WriteCard(NativeTemplate1, NativeTemplate2, FingerQty1, FingerQty2);
                        string Message = "";
                        SqlConnection conn = new SqlConnection(AccessController.m_connecton);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        if (strCardWrite != "")
                        {
                            Message = "Failed to write native template on card. " + strCardWrite;
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                            cmd.CommandText = "Update Finger_Template set CardIssue = 'false' where EmployeeCD = '" + txtEmpCd.Text + "' and IsDeleted = 'false'";
                            cmd.ExecuteNonQuery();
                            return;
                        }
                        else
                        {
                            Message = "Successfully written native template on card";
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

                            clsCardRW.Execute = false;
                            cmd.CommandText = "Update Finger_Template set CardIssue = 'true' where EmployeeCD = '" + txtEmpCd.Text + "' and IsDeleted = 'false'";
                            cmd.ExecuteNonQuery();
                        }

                        string strCardWrite1 = objclsCardRW.ISOWriteCard(ISOTemplate1, ISOTemplate2, ISOFingerQty1, ISOFingerQty2);
                        string Message1 = "";
                        if (strCardWrite != "")
                        {
                            Message1 = "Failed to write ISO template on card. " + strCardWrite;
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                            cmd.CommandText = "Update Finger_Template set CardIssue = 'false' where EmployeeCD = '" + txtEmpCd.Text + "' and IsDeleted = 'false'";
                            cmd.ExecuteNonQuery();
                            return;
                        }
                        else
                        {
                            Message1 = "Successfully written ISO template on card";
                            string someScript2 = "";
                            someScript2 = "<script language='javascript'>alert('" + Message + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

                            clsCardRW.Execute = false;
                            cmd.CommandText = "Update Finger_Template set CardIssue = 'true' where EmployeeCD = '" + txtEmpCd.Text + "' and IsDeleted = 'false'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();
                //popUpPanel.Visible = false;
                string DataIntial = "";
                DataIntial = obj_Card.Initialise().Trim();
                if (DataIntial != "")
                {
                    string someScript2 = "";
                    someScript2 = "<script language='javascript'>alert('Omnikey reader not connected or Error in card Initialization');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                    return;
                }

                string Data = "";
                Data = obj_Card.ConnectToCard();
                if (Data != "")
                {
                    string someScript2 = "";
                    someScript2 = "<script language='javascript'>alert('Error in connecting');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                    return;
                }

                obj_Card.userid = txtUserID.Text.Trim();
                obj_Card.pwd = txtPwd.Text.Trim();

                clsCardRW.strMasterKey = obj_Card.ReadMasterKey(obj_Card.userid, obj_Card.pwd);
                if (clsCardRW.strMasterKey.Contains(" "))
                {
                    string ErrorMsg = clsCardRW.strMasterKey;
                    clsCardRW.strMasterKey = null;
                    string someScript2 = "";
                    someScript2 = "<script language='javascript'>alert('" + ErrorMsg + "');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                    return;
                }
                else
                {
                    Session["MasterKey"] = clsCardRW.strMasterKey;
                }

                if (IsConfirmNeeded)
                {
                    StringBuilder javaScript = new StringBuilder();

                    string scriptKey = "ConfirmationScript";

                    javaScript.AppendFormat("var userConfirmation = window.confirm('{0}');\n", ConfirmMessage);

                    javaScript.Append("__doPostBack('UserConfirmationPostBack', userConfirmation);\n");


                    ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript.ToString(), true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnWrite_Click(object sender, EventArgs e)
        {
            string strsql = "Select * from Finger_Template where IsDeleted = 'false' and CardIssue= 'false' and EmployeeCD = '" + txtEmpCd.Text + "'";

            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                string someScript = "";
                string Message = "";
                Message = "Can not write on card,employee not enrolled or card is already issued.";
                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                return;
            }
            else
            {
                //ModalPopupExtender.Show();
                if (Session["MasterKey"] == null)
                {
                    ModalPopupExtender.Show();
                }
                else
                {
                    ModalPopupExtender.Hide();
                    if (IsConfirmNeeded)
                    {
                        StringBuilder javaScript = new StringBuilder();

                        string scriptKey = "ConfirmationScript";

                        javaScript.AppendFormat("var userConfirmation = window.confirm('{0}');\n", ConfirmMessage);

                        javaScript.Append("__doPostBack('UserConfirmationPostBack', userConfirmation);\n");


                        ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript.ToString(), true);

                    }
                }
            }

        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string strsql = "Select * from Finger_Template where IsDeleted = 'false' and Format_Type = '" + ddlFormat.SelectedItem.ToString() + "' and EmployeeCD = '" + txtEmpCd.Text + "'";

            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                string someScript = "";
                string Message = "";
                Message = "Can not verify employee not enrolled.";
                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                return;
            }

            hdnparm.Value = txtEmpCd.Text + " " + ddlFormat.SelectedItem.ToString() + " " + "Verify";
            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>executeCommands()</script>");
        }


    }
}