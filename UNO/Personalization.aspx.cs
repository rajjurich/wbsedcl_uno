using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
namespace UNO
{
    [Guid("E421E41E-100C-48AB-8CC7-5B6B78DC1C76")]
    [ComVisible(true)]
    public partial class Personalization : System.Web.UI.Page
    {
        //System.Type oType = System.Type.GetTypeFromProgID("ContactlessCardRW.Card");
         
         //public ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();        
         string m_connecton = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
         string[] Rnd = new string[8];
         string[] AppId = new string[3];
         string MasterKey = "";
         string MasterCompCd = "";
         string MasterSiteId = "";
         bool NewCard = false;
         bool DuplicateCard = false;
         int Count;

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
            
            //object o = System.Activator.CreateInstance(oType);
            //object r = oType.InvokeMember("InvokeMethod", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, this, null);
            if (!IsPostBack) 
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "testcomm();", true);
                
                string Message = "";
                string someScript = "";
                
                //if (Session["MasterKey"] == null)
                //{

                Message = "Place Master Card on reader";
                someScript = "";
                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                pnlCardDetails.Visible = true;
                pnlPersoCard.Visible = false;
                //}
                //else
                //{
                //    Message = "Place User Card on reader";
                //    someScript = "";
                //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                //    pnlCardDetails.Visible = false;
                //    pnlPersoCard.Visible = true;
                //}
            }
            else
            {

                IsConfirmNeeded = true;
                ConfirmMessage = "A card has already been issued for this employee.Do you Wish to issue a Duplicate card ?";

                // Insure that the __doPostBack() JavaScript is added to the page...
                ClientScript.GetPostBackEventReference(this, string.Empty);

                string eventTarget = Request["__EVENTTARGET"] ?? string.Empty;
                string eventArgument = Request["__EVENTARGUMENT"] ?? string.Empty;

                switch (eventTarget)
                {
                    case "UserConfirmationPostBack":
                        if (Convert.ToBoolean(eventArgument))
                        {
                           //ModalPopupExtender.Show();
                        }
                        else
                        {

                        }
                        break;
                }
            } 
            
            if (Request.Form["__EVENTTARGET"] == "Button2")
            {
                // Fire event
                Button2_Click(this, new EventArgs());
            }
            else if (Request.Form["__EVENTTARGET"] == "btnActicveX")
            {
                //btnActicveX_Click(this, new EventArgs());
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            if (hdnData.Value != "")
                return;

            Session["MasterKey"] = hdnMasterKey.Value;

            //MasterCompCd = hdnCompSiteCd.Value.Substring(0,4);
            //MasterSiteId = hdnCompSiteCd.Value.Substring(4,3);
            //Session["CmpCD"] = MasterCompCd;
            //Session["SiteID"] = MasterSiteId;
            //string DataIntial = "";
            //string Message = "";
            //string someScript = "";

            //DataIntial = obj_Card.Initialise().Trim();
            //if (DataIntial != "")
            //{
            //    Message = "Omnikey reader not connected or Error in card Initialization";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
            //    return;
            //}

            //string Data = "";
            //Data = obj_Card.ConnectToCard();
            //if (Data != "")
            //{
            //    Message = "Error in connecting";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
            //    return;
            //}

            ////************************Read Master Card key & Master Card Company code & Site Id*******************

            //MasterKey = obj_Card.ReadMasterKey(txtuid.Text.Trim(), txtpass.Text.Trim());
            //if (MasterKey != "" && MasterKey.Length == 12 && MasterKey != "000000000000")
            //{
            //    Session["MasterKey"] = MasterKey;
            //}
            //else
            //{
            //    Message = "Authentication Failed";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
            //    return;
            //}

            //string MasterCardDetails = "";
            //MasterCardDetails = obj_Card.ReadCMP(txtuid.Text.Trim(), txtpass.Text.Trim());

            // MasterCompCd = MasterCardDetails.Substring(0, 4);
            // MasterSiteId = MasterCardDetails.Substring(4, 3);

            // Session["CmpCD"] = MasterCompCd;
            // Session["SiteID"] = MasterSiteId;

            ////**************end of Master Card Readering*******************************************

            // Message = "Place User Card on reader";
            // someScript = "";
            // someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
            pnlCardDetails.Visible = false;
            pnlPersoCard.Visible = true;              

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string ApplicationCode = "";
            //ModalPopupExtender.Hide();
            //string EmployeeCode = "";
            //txtEmpCd.Text = txtEmpCd.Text.PadLeft(10, '0');

            //EmployeeCode = txtEmpCd.Text;

            //SqlConnection conn = new SqlConnection(m_connecton);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "Select count(*) from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID = '" + EmployeeCode + "' AND EPD_PREVIOUS_CARD_STATUS = '1' ";
            //cmd.Connection = conn;
            //Count = Convert.ToInt16(cmd.ExecuteScalar());
           
            //if (Count > 0)
            //{
            //    ModalPopupExtender.Show();               
            //}
            //else
            //{
            //    Personalize();
            //}
            try
            {
                string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + ' ' + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                string strsql = " SELECT EPD_CARD_ID, (CASE WHEN EPD_NUMCARDS is NULL THEN 0 ELSE EPD_NUMCARDS END) as Card_Num," +
                                " EPD_PERDATE FROM ENT_EMPLOYEE_PERSONAL_DTLS E WHERE EPD_EMPID = '" + txtEmpCd.Text.Trim() + "' ";

                SqlDataAdapter da = new SqlDataAdapter(strsql, m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    hdnCSNR.Value = dt.Rows[0][0].ToString();
                    hdnCardNum.Value = dt.Rows[0][1].ToString();
                    // hdnPerDate.Value = dt.Rows[0][2].ToString();
                    if (dt.Rows[0]["EPD_PERDATE"].ToString() != "")
                    {
                        DateTime PersoDate = DateTime.ParseExact(dt.Rows[0]["EPD_PERDATE"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));
                        hdnPerDate.Value = PersoDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else
                    {
                        hdnPerDate.Value = "";
                    }
                }

                if (Count == 0 || hdnDuplicate.Value == "Duplicate")
                {
                    NewCard = true;
                }
                else
                {
                    NewCard = false;
                }

                SqlConnection conn = new SqlConnection(m_connecton);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                if (hdnCardNum.Value == "")
                    hdnCardNum.Value = "0";

                int CardNum = Convert.ToInt32(hdnCardNum.Value) + 1;

                string CSNR = "";

                //for (int i = 0; i < hdnNewCSNR.Value.Length; i++)
                //{
                //    if (hdnNewCSNR.Value.Substring(i, 1) != " ")
                //    {
                //        CSNR += hdnNewCSNR.Value.Substring(i, 1);

                //    }
                //}

                string[] NewCSNR = hdnNewCSNR.Value.Split(' ');
                for (int i = NewCSNR.Length - 1; i > -1; i--)
                {
                    CSNR += NewCSNR[i].ToString();
                }

                if (chkApplication.Items[0].Selected)
                {
                    ApplicationCode = "T";
                }
                if (chkApplication.Items[1].Selected)
                {
                    ApplicationCode += "A";
                }

                //if (Count == 0)
                //{
                //    cmd.CommandText = " Update ACS_CARD_CONFIG set CARD_CODE = '" + CSNR + "', EXPIRY_DATE = convert(datetime,'" + txtCardExpiryDate.Text.Trim() + "',103) ," +
                //                       " EXPIRY_TIME = '" + txtCardExpiryTime.Text + "'  Where CC_EMP_ID = '" + txtEmpCd.Text + "' ";
                //    cmd.ExecuteNonQuery();
                //    cmd.CommandText = " Update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_CARD_ID = '" + CSNR + "', EPD_PREVIOUS_CARD_STATUS = '1',EPD_APPLN = '" + ApplicationCode + "',EPD_PERDATE = getdate(), " +
                //                       " EPD_NUMCARDS = '" + CardNum + "' Where EPD_EMPID = '" + txtEmpCd.Text + "' ";
                //    cmd.ExecuteNonQuery();
                //}

                if (hdnDuplicate.Value == "Duplicate")
                {
                    //string someScript = "";
                    //someScript = "<script language='javascript'>alert('Duplicate');</script>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", someScript);


                    cmd.CommandText = " Update ACS_CARD_CONFIG set CARD_CODE = '" + CSNR + "', EXPIRY_DATE = convert(datetime,'" + txtCardExpiryDate.Text.Trim() + "',103) ," +
                                       " EXPIRY_TIME = '" + txtCardExpiryTime.Text + "'  Where CC_EMP_ID = '" + txtEmpCd.Text + "' ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = " Update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_CARD_ID = '" + CSNR + "', EPD_PREVIOUS_CARD_STATUS = '1',EPD_APPLN = '" + ApplicationCode + "',EPD_PERDATE = getdate(), " +
                                       " EPD_NUMCARDS = '" + CardNum + "' Where EPD_EMPID = '" + txtEmpCd.Text + "' ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Update ENT_PARAMS set Value = 1 where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE = 'UAC'";
                    cmd.ExecuteNonQuery();

                    //someScript = "<script language='javascript'>alert('Executed');</script>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", someScript);

                    cmd.CommandText = "Insert into LOST_CARD_DETAILS (EmpNo,CardSno,Status,PerDate,AuthId) " +
                                     "Values('" + txtEmpCd.Text + "','" + hdnCSNR.Value + "','" + ddlCardStatus.SelectedValue.ToString() + "', " +
                                     "convert(varchar,'" + hdnPerDate.Value + "',103),'') ";

                    //someScript = "<script language='javascript'>alert('" + cmd.CommandText + "');</script>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", someScript);

                    cmd.ExecuteNonQuery();
                    
                }
                else
                {
                    cmd.CommandText = " Update ACS_CARD_CONFIG set CARD_CODE = '" + CSNR + "', EXPIRY_DATE = convert(datetime,'" + txtCardExpiryDate.Text.Trim() + "',103) ," +
                                      " EXPIRY_TIME = '" + txtCardExpiryTime.Text + "'  Where CC_EMP_ID = '" + txtEmpCd.Text + "' ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = " Update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_CARD_ID = '" + CSNR + "', EPD_PREVIOUS_CARD_STATUS = '1',EPD_APPLN = '" + ApplicationCode + "',EPD_PERDATE = getdate(), " +
                                       " EPD_NUMCARDS = '" + CardNum + "' Where EPD_EMPID = '" + txtEmpCd.Text + "' ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Update ENT_PARAMS set Value = 1 where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE = 'UAC'";
                    cmd.ExecuteNonQuery();
                }
                hdnDuplicate.Value = "";
                btnExit_Click(null, null);
            }
            catch (Exception ex)
            {
                hdnDuplicate.Value = "";
                throw ex;
                //string message = "Insert into LOST_CARD_DETAILS (EmpNo,CardSno,Status,PerDate,AuthId) " +
                //                     "Values('" + txtEmpCd.Text + "','" + hdnCSNR.Value + "','" + ddlCardStatus.SelectedValue.ToString() + "', " +
                //                     "convert(varchar,'" + hdnPerDate.Value + "',103),'') ";
                //string someScript = "<script language='javascript'>alert('" + message + "');</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", someScript);

            }
        }

        public void Personalize()
        {
            //string DataIntial = "";
            string Message = "";
            string someScript = "";
            string WriteData = "";
            //string ReadData = "";
            //string OldDataBlock1 = "";
            //string OldDataBlock2 = "";
            //string ApplicationCode = "";
            //int intLRC;

            if (Count == 0 || DuplicateCard == true)
            {
                NewCard = true;
            }
            else
            {
                NewCard = false;
            }

            int selectedCount = chkApplication.Items.Cast<ListItem>().Count(li => li.Selected);

            if (selectedCount == 0)
            {
                Message = "Select atleast one application";
                someScript = "";
                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
                return;
            }

            MasterKey = Session["MasterKey"].ToString();
            MasterCompCd = Session["CmpCD"].ToString();
            MasterSiteId = Session["SiteID"].ToString();           

            string Data = "";
            btndll_Click(null, null);
            ////***********************'Writing Company Code Site id********************************************************               
       //     Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "CardAuthenticate();", true);
            if (hdnData.Value != "")
            {
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this,GetType(), "MyKey", "WritingRawKey('3F',96);", true);
                if (hdnData.Value != "")
                {
                    return;
                }

                WriteData = "";
                int SiteId = Convert.ToInt32(MasterSiteId.PadLeft(3, '0'));
                string Adddata = "";
                Adddata = Adddata.PadRight(26, '0');
                ScriptManager.RegisterClientScriptBlock(this,GetType(), "MyKey", "ConvertData('" + MasterCompCd.PadLeft(4, '0') + "');", true);
                WriteData = hdnData.Value;
                ScriptManager.RegisterClientScriptBlock(this,GetType(), "MyKey", "ConvertData('" + SiteId.ToString("X") + "');", true);
                WriteData += hdnData.Value;
                ScriptManager.RegisterClientScriptBlock(this,GetType(), "MyKey", "ConvertData('" + Adddata + "');", true);
                WriteData += hdnData.Value;

                ScriptManager.RegisterClientScriptBlock(this,GetType(), "MyKey", "WriteDataInCard('" + WriteData + "','3C',15);", true);
                if (hdnData.Value != "")
                {
                    return;
                }
            }
            //Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(63, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 15 Block 63";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    //WriteData = obj_Card.Convert(obj_Card.rawkeybuf) + obj_Card.Convert("FF078069") + obj_Card.Convert("FFFFFFFFFFFF");
            //    //Data = obj_Card.WriteData(WriteData,"3F");
            //    Data = obj_Card.WriteRawKey("3F");

            //    if (Data != "")
            //    {
            //        Message = "Card Error: Unable to write in the card.Sector 15 Block 63";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //    WriteData = "";
            //    int SiteId = Convert.ToInt32(MasterSiteId.PadLeft(3, '0'));
            //    string Adddata = "";
            //    Adddata = Adddata.PadRight(26, '0');
            //    WriteData = obj_Card.Convert(MasterCompCd.PadLeft(4, '0')) + obj_Card.Convert(SiteId.ToString("X")) + obj_Card.Convert(Adddata);
            //    //WriteData = WriteData.Replace('\0','0');

            //    Data = obj_Card.WriteData(WriteData, "3C");

            //    if (Data != "")
            //    {
            //        Message = "Card Error: Unable to write in the card.Sector 15 Block 60";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}

            ////*************************End********************************************************************************

            //string EmployeeCode = "";
            //txtEmpCd.Text = txtEmpCd.Text.PadLeft(10, '0');

            //EmployeeCode = txtEmpCd.Text;

            ////commented for Aplphanumeric Numbers
            ////converting 10 bytes of empcode to 16 bytes by appending each digit with zero
            ////for (int i = 0; i < txtEmpCd.Text.Length; i++)
            ////{
            ////    EmployeeCode += 0 + txtEmpCd.Text.Substring(i, 1);
            ////}

            ////SqlConnection conn = new SqlConnection(m_connecton);
            ////conn.Open();
            ////SqlCommand cmd = new SqlCommand();
            ////cmd.CommandText = "Select count(*) from ACS_CARD_CONFIG where CC_EMP_ID = '" + txtEmpCd.Text + "' AND CARD_STATUS = '1' ";
            ////cmd.Connection = conn;
            ////int Count = Convert.ToInt16(cmd.ExecuteScalar());
            ////if (Count > 0)
            ////{
            ////    Message = "Card already issued for this employee.";
            ////    someScript = "";
            ////    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            ////    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            ////    return;
            ////}

            ////if (Count == 0 || DuplicateCard == true)
            ////{
            ////    NewCard = true;
            ////}

            ////*******************Checking for personalized card************************************************************

            //Data = obj_Card.Authenticate(8, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Message = "Authentication Error Sector 2 Block 8";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //ReadData = obj_Card.ReadTAEMP(MasterKey);
            //ReadData = obj_Card.ReConvert(ReadData).Substring(0, 10);
            //if (ReadData != "" && ReadData != "0000000000")
            //{
            //    Message = "Card Is Already personalized or Card Error";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //ReadData = "";
            //ReadData = obj_Card.ReadACEMP(MasterKey);
            //ReadData = obj_Card.ReConvert(ReadData).Substring(0, 10);
            //if (ReadData != "" && ReadData != "0000000000")
            //{
            //    Message = "Card Is Already personalized or Card Error";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //WriteData = "";
            //Data = obj_Card.Authenticate(60, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(60, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 15 Block 60";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    ReadData = obj_Card.ReadBlock(60);
            //    string Stemp = "";

            //    for (int i = 0; i < ReadData.Length; i++)
            //    {
            //        if (ReadData.Substring(i, 1) != " ")
            //        {
            //            Stemp += ReadData.Substring(i, 1);
            //        }
            //    }

            //    ReadData = Stemp;

            //    DateTime ExpiryDt = DateTime.ParseExact(txtCardExpiryDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
            //    WriteData = ExpiryDt.ToString("ddMMyyyy");

            //    DateTime ExpiryTime = DateTime.ParseExact(txtCardExpiryTime.Text, "HH:mm", new System.Globalization.CultureInfo("en-US"));
            //    WriteData += ExpiryTime.ToString("HHmm");

            //    string strBCDDate = "";
            //    string strLRC = "";

            //    strBCDDate = obj_Card.ConvertBCD(WriteData);

            //    intLRC = 0;

            //    for (int i = 0; i < strBCDDate.Length; i++)
            //    {
            //        byte[] asciiByte = ASCIIEncoding.ASCII.GetBytes(strBCDDate.Substring(i, 1));
            //        string LRC = asciiByte.GetValue(0).ToString();
            //        intLRC = intLRC + Convert.ToInt32(LRC);
            //    }

            //    strLRC = intLRC.ToString("X");
            //    strLRC = obj_Card.Convert(strLRC.Substring(strLRC.Length - 2, 2));

            //    WriteData = obj_Card.Convert(ReadData.Substring(0, 12)) + strBCDDate + strLRC + obj_Card.Convert(ReadData.Substring(ReadData.Length - 6, 6));
            //    // WriteData = WriteData.Replace('\0', ' ');

            //    Data = obj_Card.WriteData(WriteData, "3C");

            //    if (Data != "")
            //    {
            //        Message = "Card Error: Unable to write in the card.Sector 15 Block 60";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}

            ////*****************Read App Code from sec 0********************************************************
            //Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Message = "Authentication Error Sector 0";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}
            //else
            //{
            //    OldDataBlock1 = obj_Card.ReadBlock(1);
            //    OldDataBlock2 = obj_Card.ReadBlock(2);

            //    string Stemp = "";

            //    for (int i = 0; i < OldDataBlock1.Length; i++)
            //    {
            //        if (OldDataBlock1.Substring(i, 1) != " ")
            //        {
            //            Stemp += OldDataBlock1.Substring(i, 1);
            //        }
            //    }

            //    OldDataBlock1 = Stemp;

            //}
            ////****************end of reading app code***********************************************************
            ////***********Locking Sector 0 with cms key ********************************************************
            //WriteData = "";
            ////WriteData = obj_Card.Convert(obj_Card.rawkeybuf) + obj_Card.Convert("FF078069") + obj_Card.Convert("FFFFFFFFFFFF");
            ////Data = obj_Card.WriteBlock(3, WriteData);
            //Data = obj_Card.WriteRawKey("3");
            //if (Data != "")
            //{
            //    Message = "Card Error: Unable to write in the card.Sector 0 Block 3";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}


            ////*********************end***********************************************************************
            ////*****'Following code writes the sector 1 (reallocated) with the card holders data and the random no***********
            //Data = obj_Card.Authenticate(6, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(6, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 1 Block 6";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    //Data = obj_Card.WriteBlock(7, WriteData);
            //    Data = obj_Card.WriteRawKey("7");
            //    if (Data != "")
            //    {
            //        Message = "Card Error: Unable to write in the card.Sector 1 Block 7";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //}

            //string EmpName = txtEmpName.Text.PadRight(32, ' ');
            //WriteData = "";
            //WriteData = EmpName.Substring(0, 16);
            //Data = obj_Card.WriteData(WriteData, "04");
            //if (Data != "")
            //{
            //    Message = "Card Error: Unable to write in the card.Sector 1 Block 4";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //WriteData = EmpName.Substring(16, 16);
            //Data = obj_Card.WriteData(WriteData, "05");
            //if (Data != "")
            //{
            //    Message = "Card Error: Unable to write in the card.Sector 1 Block 5";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //string Gender = ddlGender.SelectedItem.ToString();
            //if (Gender == "Male")
            //{
            //    Gender = obj_Card.Convert("01");
            //}
            //else
            //{
            //    Gender = obj_Card.Convert("02");
            //}

            //DateTime DOB = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));

            //WriteData = "";
            //string AddString = "";
            //AddString = AddString.PadRight(22, 'F');
            //WriteData = obj_Card.Convert(DOB.ToString("ddMMyyyy")) + Gender + obj_Card.Convert(AddString);

            //Data = obj_Card.WriteData(WriteData, "6");
            //if (Data != "")
            //{
            //    Message = "Card Error: Unable to write in the card.Sector 1 Block 6";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            ////*****************End*******************************************************************************************

            ////*********************'below code to lock sector 14 with master key**************************************************

            //Data = obj_Card.Authenticate(59, MasterKey, 96);
            //if (Data != "")
            //{
            //    Message = "Authentication Error Sector 14 Block 59";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            ////WriteData = obj_Card.Convert(MasterKey.Substring(0, 6)) + obj_Card.Convert("FF078069");
            ////WriteData = WriteData.PadRight(12, 'F');

            ////Data = obj_Card.WriteBlock(59, WriteData);
            //Data = obj_Card.WriteMasterKey(MasterKey, "3B");
            //if (Data != "")
            //{
            //    Message = "Card Error: Unable to write in the card.Sector 14 Block 59";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}
            ////***************************************end********************************************************************
            ////*******below code to lock sector 13 with master key**********************************************
            //Data = obj_Card.Authenticate(55, MasterKey, 96);
            //if (Data != "")
            //{
            //    Message = "Authentication Error Sector 13 Block 55";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            ////WriteData = obj_Card.Convert(MasterKey.Substring(0, 6)) + obj_Card.Convert("FF078069");
            ////WriteData = WriteData.PadRight(12, 'F');

            ////Data = obj_Card.WriteBlock(55, WriteData);
            //Data = obj_Card.WriteMasterKey(MasterKey, "37");
            //if (Data != "")
            //{
            //    Message = "Card Error: Unable to write in the card.Sector 13 Block 55";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}
            ////***************end***********************************************************************

            ////*********below code to write Application code in sector 0*********************************

            //AppId[0] = "0400";
            //AppId[1] = "0148";
            //AppId[2] = "0248";

            //Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(3, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 0 Block 3";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    WriteData = "";
            //    WriteData = OldDataBlock1.Substring(0, 4) + AppId[0] + AppId[1] + AppId[2];
            //    WriteData = WriteData.PadRight(32, '0');
            //    WriteData = obj_Card.Convert(WriteData);

            //    Data = obj_Card.WriteData(WriteData, "1");
            //    if (Data != "")
            //    {
            //        Message = "Card Error: Unable to write in the card.Sector 0 Block 1";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            ////***********End***************************************************************************

            ////*******************below code to write Access AppCode in block 0 sector 15**************
            //Data = obj_Card.Authenticate(60, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(60, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 15 Block 60";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    ReadData = obj_Card.ReadBlock(60);
            //    string Stemp = "";

            //    for (int i = 0; i < ReadData.Length; i++)
            //    {
            //        if (ReadData.Substring(i, 1) != " ")
            //        {
            //            Stemp += ReadData.Substring(i, 1);
            //        }
            //    }

            //    ReadData = Stemp;

            //    WriteData = "";
            //    WriteData = ReadData.Substring(0, 26) + AppId[2] + ReadData.Substring(ReadData.Length - 2, 2);
            //    WriteData = obj_Card.Convert(WriteData);

            //    Data = obj_Card.WriteData(WriteData, "3C");
            //    if (Data != "")
            //    {
            //        Message = "Card Error: Unable to write in the card.Sector 15 Block 60";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}


            ////******************************End****************************************

            ////*******************writting code for Time Attendance & Access Control******************************
            //if (chkApplication.Items[0].Selected)
            //{
            //    if (Time_Attd_Perso(MasterKey, EmployeeCode) != true)
            //    {
            //        return;
            //    }
            //    ApplicationCode = "T";
            //}
            //if (chkApplication.Items[1].Selected)
            //{
            //    if (Access_Sys_Perso(MasterKey, EmployeeCode) != true)
            //    {
            //        return;
            //    }
            //    ApplicationCode += "A";
            //}

            //Message = "Card Personalized Successfully.";
            //someScript = "";
            //someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);

            //SqlConnection conn = new SqlConnection(m_connecton);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //int CardNum = Convert.ToInt32(hdnCardNum.Value) + 1;

            //string CSNR = "";
            //for (int i = 0; i < obj_Card.CSNR.Length; i++)
            //{
            //    if (obj_Card.CSNR.Substring(i, 1) != " ")
            //    {
            //        CSNR += obj_Card.CSNR.Substring(i, 1);
            //    }
            //}
            
            //if (Count == 0)
            //{
            //    cmd.CommandText = " Update ACS_CARD_CONFIG set CARD_CODE = '" + CSNR + "', EXPIRY_DATE = convert(datetime,'" + txtCardExpiryDate.Text.Trim() + "',103) ," +
            //                       " EXPIRY_TIME = '" + txtCardExpiryTime.Text + "'  Where CC_EMP_ID = '" + txtEmpCd.Text + "' ";
            //    cmd.ExecuteNonQuery();
            //    cmd.CommandText = " Update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_CARD_ID = '" + CSNR + "', EPD_PREVIOUS_CARD_STATUS = '1',EPD_APPLN = '" + ApplicationCode + "',EPD_PERDATE = getdate(), " +
            //                       " EPD_NUMCARDS = '" + CardNum + "' Where EPD_EMPID = '" + txtEmpCd.Text + "' ";
            //    cmd.ExecuteNonQuery();
            //}

            //if (DuplicateCard)
            //{
            //    cmd.CommandText = "Insert into LOST_CARD_DETAILS (EmpNo,CardSno,Status,PerDate,AuthId) " +
            //                      "Values('" + txtEmpCd.Text + "','" + hdnCSNR.Value + "','" + ddlCardStatus.SelectedValue.ToString() + "', " +
            //                      "convert(datetime,'" + hdnPerDate.Value + "',103),'') ";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = " Update ACS_CARD_CONFIG set CARD_CODE = '" + CSNR + "', EXPIRY_DATE = convert(datetime,'" + txtCardExpiryDate.Text.Trim() + "',103) ," +
            //                       " EXPIRY_TIME = '" + txtCardExpiryTime.Text + "'  Where CC_EMP_ID = '" + txtEmpCd.Text + "' ";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = " Update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_CARD_ID = '" + CSNR + "', EPD_PREVIOUS_CARD_STATUS = '1',EPD_APPLN = '" + ApplicationCode + "',EPD_PERDATE = getdate(), " +
            //                       " EPD_NUMCARDS = '" + CardNum + "' Where EPD_EMPID = '" + txtEmpCd.Text + "' ";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = "Update ENT_PARAMS set Value = 1 where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE = 'UAC'";
            //    cmd.ExecuteNonQuery();
            //}
            //else
            //{
            //    cmd.CommandText = " Update ACS_CARD_CONFIG set CARD_CODE = '" + CSNR + "', EXPIRY_DATE = convert(datetime,'" + txtCardExpiryDate.Text.Trim() + "',103) ," +
            //                      " EXPIRY_TIME = '" + txtCardExpiryTime.Text + "'  Where CC_EMP_ID = '" + txtEmpCd.Text + "' ";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = " Update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_CARD_ID = '" + CSNR + "', EPD_PREVIOUS_CARD_STATUS = '1',EPD_APPLN = '" + ApplicationCode + "',EPD_PERDATE = getdate(), " +
            //                       " EPD_NUMCARDS = '" + CardNum + "' Where EPD_EMPID = '" + txtEmpCd.Text + "' ";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = "Update ENT_PARAMS set Value = 1 where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE = 'UAC'";
            //    cmd.ExecuteNonQuery();
            //}
            //btnExit_Click(null, null);

        }
        public bool Time_Attd_Perso(string masterkey, string empcode)
        {
            //string Data = "";
            //string Message = "";
            //string someScript = "";
            //string WriteData = "";
            //string ReadData = "";
            //string key = "";
            //string RandomNo = "";
            //string Key_a_Buf = "";

            //RandomNo = obj_Card.CalculateRandomNo().ToString();
            //RandomNo = obj_Card.Convert(RandomNo);

            //ReadData = obj_Card.ProgramRNDs(2, masterkey, RandomNo);
            //if (ReadData == "")
            //{
            //    Message = "Card Error - Presonalizing for Time Attendance.";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return false;
            //}

            //Key_a_Buf = "FFFFFF";

            //key = obj_Card.CalculatetApplKey(Convert.ToInt32(ReadData), RandomNo, Key_a_Buf);

            //if (key == "")
            //{
            //    Message = "Card Error - Presonalizing for Time Attendance.";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return false;
            //}

            ////Data = obj_Card.Authenticate(11, "", 96);
            ////if (Data != "")
            ////{
            //    Data = obj_Card.Authenticate(11, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 2 Block 11";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return false;
            //    }
            ////}
            //else
            //{
            //    WriteData = "";
            //    WriteData = empcode.PadRight(16, '0');
            //    //WriteData =obj_Card.Convert(WriteData);
            //    Data = obj_Card.WriteData(WriteData,"08");
            //    if (Data != "")
            //    {
            //        Message = "Card Error - Presonalizing for Time Attendance.Sector 2 Block 8";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return false;
            //    }

            //     //writting calculated key in 11 block sec 2
            //     WriteData = "";
            //     WriteData = key;               

            //     Data = obj_Card.WriteData(WriteData,"B");
            //     if (Data != "")
            //     {
            //         Message = "Card Error - Presonalizing for Time Attendance.Sector 2 Block 11";
            //         someScript = "";
            //         someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //         return false;
            //     }

            //     WriteData = "";
            //     WriteData = WriteData.PadRight(32, '0');
            //     WriteData = obj_Card.Convert(WriteData);
            //     Data = obj_Card.WriteData(WriteData,"9");
            //     if (Data != "")
            //     {
            //         Message = "Card Error - Presonalizing for Time Attendance.Sector 2 Block 9";
            //         someScript = "";
            //         someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //         return false;
            //     }
            //}
            return true;
        }

        public bool Access_Sys_Perso(string masterkey, string empcode)
        {
        //    string Data = "";
        //    string Message = "";
        //    string someScript = "";
        //    string WriteData = "";
        //    string ReadData = "";
        //    string key = "";
        //    string RandomNo ;
        //    string Key_a_Buf = "";

        //    RandomNo = obj_Card.CalculateRandomNo().ToString();
        //    RandomNo = obj_Card.Convert(RandomNo);

        //    ReadData = obj_Card.ProgramRNDs(3, masterkey, RandomNo);
        //    if (ReadData == "")
        //    {
        //        Message = "Card Error - Presonalizing for Access Control.";
        //        someScript = "";
        //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //        return false;
        //    }

        //    Key_a_Buf = "FFFFFF";

        //    key = obj_Card.CalculatetApplKey(Convert.ToInt32(ReadData), RandomNo, Key_a_Buf);

        //    if (key == "")
        //    {
        //        Message = "Card Error - Presonalizing for Access Control.";
        //        someScript = "";
        //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //        return false;
        //    }            

        //    //Data = obj_Card.Authenticate(15, "", 96);
        //    //if (Data != "")
        //    //{
        //        Data = obj_Card.Authenticate(15, "FFFFFFFFFFFF", 96);

        //        if (Data != "")
        //        {
        //            Message = "Authentication Error Sector 3 Block 15";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return false;
        //        }
        //   // }
        //    else
        //    {
        //        WriteData = "";
        //        WriteData = empcode.PadRight(16, '0');
        //        //WriteData = obj_Card.Convert(WriteData);
        //        Data = obj_Card.WriteData(WriteData,"C");
        //        if (Data != "")
        //        {
        //            Message = "Card Error - Presonalizing for Access Control.Sector 3 Block 12";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return false;
        //        }

        //        WriteData = "";
        //        WriteData = WriteData.PadRight(32, '0');
        //        WriteData = obj_Card.Convert(WriteData);
        //        Data = obj_Card.WriteData(WriteData,"D");
        //        if (Data != "")
        //        {
        //            Message = "Card Error - Presonalizing for Access Control.Sector 3 Block 13";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return false;
        //        }

        //        WriteData = "";
        //        WriteData = WriteData.PadRight(32, '0');
        //        WriteData = obj_Card.Convert(WriteData);
        //        Data = obj_Card.WriteData(WriteData,"E");
        //        if (Data != "")
        //        {
        //            Message = "Card Error - Presonalizing for Access Control.Sector 3 Block 14";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return false;
        //        }

        //        //writting calculated key in 15 block sec 3
        //        WriteData = "";
        //        WriteData = key;

        //        Data = obj_Card.WriteData(WriteData,"F");
        //        if (Data != "")
        //        {
        //            Message = "Card Error - Presonalizing for Time Attendance.Sector 3 Block 15";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return false;
        //        }
        //    }
            return true;
        }

        //protected void txtEmpCd_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtEmpCd.Text.Length == 10)
        //    {
        //        string strsql = " SELECT EPD_FIRST_NAME + ' ' + EPD_LAST_NAME AS EMP_NAME,EPD_DOB,EPD_GENDER,EXPIRY_DATE " +
        //                        " FROM ENT_EMPLOYEE_PERSONAL_DTLS E,ACS_CARD_CONFIG A WHERE EPD_EMPID = CC_EMP_ID " +
        //                        " AND EPD_EMPID = '" + txtEmpCd.Text.Trim() + "' ";

        //        SqlDataAdapter da = new SqlDataAdapter(strsql, m_connecton);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            txtEmpName.Text = dt.Rows[0]["EMP_NAME"].ToString();
        //            txtDOB.Text = dt.Rows[0]["EPD_DOB"].ToString().Substring(0, 10);
        //            ddlGender.SelectedValue = dt.Rows[0]["EPD_GENDER"].ToString();
        //            txtCardExpiryDate.Text = dt.Rows[0]["EXPIRY_DATE"].ToString().Substring(0, 10);
        //        }
        //    }
        //}
        protected void Button2_Click(object sender, EventArgs e)
        {
            ModalPopupExtender.Hide();
            string EmployeeCode = "";
            txtEmpCd.Text = txtEmpCd.Text.PadLeft(10, '0');
            EmployeeCode = txtEmpCd.Text;

            SqlConnection conn = new SqlConnection(m_connecton);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select count(*) from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID = '" + EmployeeCode + "' AND EPD_PREVIOUS_CARD_STATUS = '1' ";
            cmd.Connection = conn;
            Count = Convert.ToInt16(cmd.ExecuteScalar());

            if (Count > 0)
            {
                ModalPopupExtender.Show();
            }
            //else
            //{
            //    Personalize();
            //}
            string strsql = " SELECT EPD_FIRST_NAME + ' ' + EPD_LAST_NAME AS EMP_NAME,EPD_DOB,EPD_GENDER,EPD_CARD_ID, " +
                            " (CASE WHEN EPD_NUMCARDS is NULL THEN 0 ELSE EPD_NUMCARDS END) as Card_Num,EPD_PERDATE " +
                            " FROM ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID = '" + txtEmpCd.Text.Trim() + "' ";

             SqlDataAdapter da = new SqlDataAdapter(strsql, m_connecton);
             DataTable dt = new DataTable();
             da.Fill(dt);
             if (dt.Rows.Count > 0)
             {
                 string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + ' ' + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                 DateTime DOB = DateTime.ParseExact(dt.Rows[0]["EPD_DOB"].ToString(), sysFormat , new System.Globalization.CultureInfo("en-US"));
                // DateTime CardExpirydt = DateTime.ParseExact(dt.Rows[0]["EXPIRY_DATE"].ToString(), sysFormat , new System.Globalization.CultureInfo("en-US"));

                 if (dt.Rows[0]["EPD_PERDATE"].ToString() != "")
                 {
                     DateTime PersoDate = DateTime.ParseExact(dt.Rows[0]["EPD_PERDATE"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));
                     hdnPerDate.Value = PersoDate.ToString();
                 }
                 else
                 {
                     hdnPerDate.Value = "";
                 }

                 txtEmpName.Text = dt.Rows[0]["EMP_NAME"].ToString();
                 txtDOB.Text = DOB.ToString("dd/MM/yyyy");
                 ddlGender.SelectedValue = dt.Rows[0]["EPD_GENDER"].ToString();                
                 txtCardExpiryTime.Text = "23:59";
                 hdnCardNum.Value = dt.Rows[0]["Card_Num"].ToString();
                 hdnCSNR.Value = dt.Rows[0]["EPD_CARD_ID"].ToString();

                 strsql = "select VALUE from ENT_PARAMS WHERE MODULE = 'CM' AND CODE = 'ED'";
                 SqlDataAdapter da1 = new SqlDataAdapter(strsql, m_connecton);
                 DataTable dt1 = new DataTable();
                 da1.Fill(dt1);
                 txtCardExpiryDate.Text = dt1.Rows[0][0].ToString();

                 chkApplication.Items[0].Selected = true;
                 chkApplication.Items[1].Selected = true;
                
             }
             else
             {
                string Message = "Employee Not found.";
                string someScript = "";
                 someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
                 return;
             }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtuid.Text = "";
            txtpass.Text = "";
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            txtEmpCd.Text = "";
            txtEmpName.Text = "";
            txtCardExpiryDate.Text = "";
            txtCardExpiryTime.Text = "";
            ddlGender.SelectedIndex = 0;
            txtDOB.Text = "";
            chkApplication.Items[0].Selected = false;
            chkApplication.Items[1].Selected = false;
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            DuplicateCard = true;
          //  hdnDuplicate.Value = "Duplicate";
            ModalPopupExtender.Hide();
           // Personalize();
        }         
         

         protected void btnActicveX_Click(object sender, EventArgs e)
         {

            string Message = "Serverside code executed";
            string someScript = "";
             someScript = "<script language='javascript'>alert('" + Message + "');</script>";
             Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);

                

         }

         protected void btndll_Click(object sender, EventArgs e)
         {
             //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "CardAuthenticate();", true);
             Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ConvertData();", true);
             string WriteData = hdnData.Value;

         }

    }
}