using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Text;

namespace UNO
{
    public partial class ViewCard : System.Web.UI.Page
    {
      //  public ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();
        bool TimeAttendance = false;
        bool AccessControl = false;
        string[] Rnd = new string[8];
        string MasterKey = "";
        string MasterCompCd = "";
        string MasterSiteId = "";
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
            if (!IsPostBack)
            {
                //string Message = "";
                //string someScript = "";

                //if (Session["MasterKey"] == null)
                //{

                //    Message = "Place Master Card on reader";
                //    someScript = "";
                //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                //    //pnlCardDetails.Visible = true;
                //    //pnlViewCard.Visible = false;
                //}
                //else
                //{
                //    //Message = "Place User Card on reader";
                //    //someScript = "";
                //    //someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                //    pnlCardDetails.Visible = false;                   
                //    if (IsConfirmNeeded)
                //    {
                //        ConfirmMessage = "Place User Card on reader";
                        
                //        StringBuilder javaScript = new StringBuilder();

                //        string scriptKey = "ConfirmationScript";

                //        javaScript.AppendFormat("var userConfirmation = window.confirm('{0}');\n", ConfirmMessage);

                //        javaScript.Append("__doPostBack('UserConfirmationPostBack', userConfirmation);\n");

                //        ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript.ToString(), true);

                //    }
                //    //ViewCardDetails();
                //}
            }
            //else
            //{

            //    IsConfirmNeeded = true;
            //    ConfirmMessage = "Place User Card on reader";

            //    // Insure that the __doPostBack() JavaScript is added to the page...
            //    ClientScript.GetPostBackEventReference(this, string.Empty);
                
            //    string eventTarget = Request["__EVENTTARGET"] ?? string.Empty;
            //    string eventArgument = Request["__EVENTARGUMENT"] ?? string.Empty;
                
            //        switch (eventTarget)
            //        {
            //            case "UserConfirmationPostBack":
            //                if (Convert.ToBoolean(eventArgument))
            //                {
            //                   // ViewCardDetails();
            //                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "ViewCard()", false);
            //                }
            //                else
            //                {
            //                    pnlCardDetails.Visible = true;
            //                    pnlViewCard.Visible = false;
            //                }
            //                break;
            //        }                
            //}                                 
        }

        //private void ViewCardDetails()
        //{
        //    string DataIntial = "";
        //    string Message = "";
        //    string someScript = "";


        //    MasterKey = Session["MasterKey"].ToString();
        //    MasterCompCd = Session["CmpCD"].ToString();
        //    MasterSiteId = Session["SiteID"].ToString();

        //    DataIntial = obj_Card.Initialise().Trim();
        //    if (DataIntial != "")
        //    {
        //        Message = "Omnikey reader not connected or Error in card Initialization";
        //        someScript = "";
        //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //        return;
        //    }

        //    string Data = "";
        //    //Data = obj_Card.ConnectToCard();
        //    //if (Data != "")
        //    //{
        //    //    Message = "Error in connecting";
        //    //    someScript = "";
        //    //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //    //    return;
        //    //}

        //    ////************************Read Master Card key & Master Card Company code & Site Id*******************
        //    //string MasterKey = "";
        //    //MasterKey = obj_Card.ReadMasterKey("111111", "111111");

        //    //string MasterCardDetails = "";
        //    //MasterCardDetails = obj_Card.ReadCMP("111111", "111111");

        //    //string MasterCompCd = MasterCardDetails.Substring(0, 4);
        //    //string MasterSiteId = MasterCardDetails.Substring(4, 3);

        //    ////**************end of Master Card Readering*******************************************

        //    //*****************Read User Card Company Code & site id & Match with Master Card************

        //    Data = obj_Card.ConnectToCard();
        //    if (Data != "")
        //    {
        //        Message = "Error in connecting";
        //        someScript = "";
        //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //        return;
        //    }

        //    string UserCardDetails = "";
        //    UserCardDetails = obj_Card.ReadCMP(txtuid.Text.Trim(), txtpass.Text.Trim());

        //    string UserCompCd = UserCardDetails.Substring(0, 4);
        //    string UserSiteId = UserCardDetails.Substring(4, 3);

        //    //*********end of reading User Card*******************************************************

        //    if (MasterCompCd != UserCompCd && MasterSiteId != UserSiteId)
        //    {
        //        Message = "Company code or site id does not match.Please Contact CMS Computers LTD";
        //        someScript = "";
        //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //        return;
        //    }

        //    //***********************Reading Expiry Date & Time from Sector 15 Block 60************************

        //    Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);

        //    string ReadData = obj_Card.ReadBlock(60);
        //    string temp = "";
        //    for (int i = 0; i < ReadData.Length; i++)
        //    {
        //        if (ReadData.Substring(i, 1) != " ")
        //        {
        //            temp += ReadData.Substring(i, 1);
        //        }
        //    }
        //    ReadData = temp;
        //    txtCardExpiryDate.Text = ReadData.Substring(12, 2) + "/" + ReadData.Substring(14, 2) + "/" + ReadData.Substring(16, 4);
        //    txtCardExpiryTime.Text = ReadData.Substring(20, 2) + ":" + ReadData.Substring(22, 2);


        //    //***********************End*******************************************************************

        //    //****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

        //    //Authenticate sec 14 with Master Key
        //    Data = obj_Card.Authenticate(59, MasterKey, 96);
        //    if (Data != "")
        //    {
        //        Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

        //        if (Data != "")
        //        {
        //            Message = "Authentication Error Sector 14 Block 59";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        string ReadData1 = obj_Card.ReadBlock(56);
        //        string Stemp = "";

        //        for (int i = 0; i < ReadData1.Length; i++)
        //        {
        //            if (ReadData1.Substring(i, 1) != " ")
        //            {
        //                Stemp += ReadData1.Substring(i, 1);
        //            }
        //        }

        //        ReadData1 = obj_Card.Convert(Stemp);
        //        Rnd[3] = ReadData1.Substring(ReadData1.Length - 6, 6);
        //        Rnd[2] = ReadData1.Substring(4, 6);

        //        //****************End*************************************************************************

        //        //***********Reading Sector 1 block 4 ********************************************************
        //        Data = obj_Card.Authenticate(7, obj_Card.Cardrawkeybuf, 96);
        //        if (Data != "")
        //        {
        //            Data = obj_Card.Authenticate(7, "FFFFFFFFFFFF", 96);

        //            if (Data != "")
        //            {
        //                Message = "Authentication Error Sector 1 Block 4";
        //                someScript = "";
        //                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            string ReadData2 = obj_Card.ReadBlock(4);
        //            string Stemp1 = "";

        //            for (int i = 0; i < ReadData2.Length; i++)
        //            {
        //                if (ReadData2.Substring(i, 1) != " ")
        //                {
        //                    Stemp1 += ReadData2.Substring(i, 1);
        //                }
        //            }

        //            ReadData2 = obj_Card.Convert(Stemp1);
        //            txtEmpName.Text  = ReadData2.Trim();

        //            ReadData2 = "";
        //            ReadData2 = obj_Card.ReadBlock(5);
        //            Stemp1 = "";

        //            for (int i = 0; i < ReadData2.Length; i++)
        //            {
        //                if (ReadData2.Substring(i, 1) != " ")
        //                {
        //                    Stemp1 += ReadData2.Substring(i, 1);
        //                }
        //            }

        //            ReadData2 = obj_Card.Convert(Stemp1);
        //            txtEmpName.Text = txtEmpName.Text +  ReadData2.Trim();

        //            ReadData2 = "";
        //            ReadData2 = obj_Card.ReadBlock(6);
        //            Stemp1 = "";

        //            for (int i = 0; i < ReadData2.Length; i++)
        //            {
        //                if (ReadData2.Substring(i, 1) != " ")
        //                {
        //                    Stemp1 += ReadData2.Substring(i, 1);
        //                }
        //            }

        //            ReadData2 = Stemp1;

        //            string Gender = ReadData2.Substring(8, 2);                    

        //            if (Gender == "01")
        //            {
        //                txtGender.Text = "Male";
        //            }
        //            else
        //            {
        //                txtGender.Text = "Female";
        //            }

        //            string DOB = ReadData2.Substring(0, 8);                

        //            txtDOB.Text = DOB.Substring(0, 2) + "/" + DOB.Substring(2, 2) + "/" + DOB.Substring(4,4);

        //        }

        //        //**********End******************************************************************************


        //        //*****************Read App Code from sec 0********************************************************
        //        Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
        //        if (Data != "")
        //        {
        //            Message = "Authentication Error Sector 0";
        //            someScript = "";
        //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //            return;
        //        }
        //        else
        //        {
        //            string OldDataBlock1 = obj_Card.ReadBlock(1);
        //            string OldDataBlock2 = obj_Card.ReadBlock(2);

        //            string Stemp1 = "";

        //            for (int i = 0; i < OldDataBlock1.Length; i++)
        //            {
        //                if (OldDataBlock1.Substring(i, 1) != " ")
        //                {
        //                    Stemp1 += OldDataBlock1.Substring(i, 1);
        //                }
        //            }

        //            OldDataBlock1 = Stemp1;

        //            string Stemp2 = "";

        //            for (int i = 0; i < OldDataBlock2.Length; i++)
        //            {
        //                if (OldDataBlock2.Substring(i, 1) != " ")
        //                {
        //                    Stemp2 += OldDataBlock2.Substring(i, 1);
        //                }
        //            }

        //            OldDataBlock2 = Stemp2;

        //            if (OldDataBlock1.Substring(8, 4) == "0148")
        //            {
        //                TimeAttendance = true;
        //            }
        //            if (OldDataBlock1.Substring(12, 4) == "0248")
        //            {
        //                AccessControl = true;
        //            }
        //            if (OldDataBlock2.Substring(8, 4) == "0148")
        //            {
        //                TimeAttendance = true;
        //            }
        //            if (OldDataBlock2.Substring(12, 4) == "0248")
        //            {
        //                AccessControl = true;
        //            }

        //            if (TimeAttendance == true)
        //            {
        //                lstApplication.Items.Add("Time Attendance");
        //                txtEmpCd.Text = obj_Card.ReadTAEMP(MasterKey);
        //            }


        //            if (AccessControl == true)
        //            {
        //                lstApplication.Items.Add("Access Control");
        //                txtEmpCd.Text = obj_Card.ReadACEMP(MasterKey);
        //            }
        //            if (txtEmpCd.Text == "")
        //            {
        //                Message = "Card is not personalized";
        //                someScript = "";
        //                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
        //                pnlViewCard.Visible = false;
        //                pnlCardDetails.Visible = true;
        //                return;
        //            }
        //        }
        //        //****************end of reading app code***********************************************************
        //        pnlViewCard.Visible = true;
             
        //    }
        //}

        //protected void btnAccept_Click(object sender, EventArgs e)
        //{
        //    //string DataIntial = "";
        //    //string Message = "";
        //    //string someScript = "";

        //    //DataIntial = obj_Card.Initialise().Trim();
        //    //if (DataIntial != "")
        //    //{
        //    //    Message = "Omnikey reader not connected or Error in card Initialization";
        //    //    someScript = "";
        //    //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
        //    //    return;
        //    //}

        //    //Thread.Sleep(1000);
        //    //string Data = "";
        //    //Data = obj_Card.ConnectToCard();
        //    //if (Data != "")
        //    //{
        //    //    Message = "Error in connecting";
        //    //    someScript = "";
        //    //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
        //    //    return;
        //    //}

        //    ////************************Read Master Card key & Master Card Company code & Site Id*******************

        //    //MasterKey = obj_Card.ReadMasterKey(txtuid.Text.Trim(), txtpass.Text.Trim());
        //    //if (MasterKey != "" && MasterKey.Length == 12 && MasterKey != "000000000000")
        //    //{
        //    //Session["MasterKey"] = hdnMasterKey.Value;
        //    //}
        //    //else
        //    //{
        //    //    Message = "Authentication Failed";
        //    //    someScript = "";
        //    //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
        //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
        //    //    return;
        //    //}

        //    //string MasterCardDetails = "";
        //    //MasterCardDetails = obj_Card.ReadCMP(txtuid.Text.Trim(), txtpass.Text.Trim());

        //    //MasterCompCd = MasterCardDetails.Substring(0, 4);
        //    //MasterSiteId = MasterCardDetails.Substring(4, 3);

        //    //Session["CmpCD"] = MasterCompCd;
        //    //Session["SiteID"] = MasterSiteId;

        //    ////**************end of Master Card Readering*******************************************

        //    //if (IsConfirmNeeded)
        //    //{
        //    //    StringBuilder javaScript = new StringBuilder();

        //    //    string scriptKey = "ConfirmationScript";

        //    //    javaScript.AppendFormat("var userConfirmation = window.confirm('{0}');\n", ConfirmMessage);

        //    //    javaScript.Append("__doPostBack('UserConfirmationPostBack', userConfirmation);\n");


        //    //    ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript.ToString(), true);

        //    //}
        //    //lstApplication.Items.Add("Time Attendance");
        //    //lstApplication.Items.Add("Access Control");
        //    //pnlCardDetails.Visible = false;
        //    //pnlViewCard.Visible = true;
            
        //   //// Message = "Place User Card on reader";
        //   // someScript = "";
        //   // someScript = "<script language='javascript'>Confirm();</script>";

        //   // Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
        //   // string confirmValue = Request.Form["confirm_value"];
        //   // if (confirmValue == "Yes")
        //   // {
        //   //    
        //   //     ViewCardDetails();
        //   // }
            
        //}
    }
}