using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace UNO
{
    public partial class RawCard : System.Web.UI.Page
    {
        //public ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();
        string[] Rnd = new string[8];
        string MasterKey = "";
        string MasterCompCd = "";
        string MasterSiteId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Timeout = 5;
            if (!IsPostBack)
            {
                string Message = "";
                string someScript = "";

                //if (Session["MasterKey"] == null)
                //{

                Message = "Place Master Card on reader";
                someScript = "";
                someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                pnlCardDetails.Visible = true;
                pnlRawCard.Visible = false;
                //}
                //else
                //{
                //    hdnMasterKey.Value = Session["MasterKey"].ToString();
                //    Message = "Place User Card on reader";
                //    someScript = "";
                //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                //    pnlCardDetails.Visible = false;
                //    pnlRawCard.Visible = true;                   
                //}
            }               

        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            //string DataIntial = "";
            //string Message = "";
            //string someScript = "";

            //MasterKey = Session["MasterKey"].ToString();
            //MasterCompCd = Session["CmpCD"].ToString();
            //MasterSiteId = Session["SiteID"].ToString();

            //DataIntial = obj_Card.Initialise().Trim();
            //if (DataIntial != "")
            //{
            //    Message = "Omnikey reader not connected or Error in card Initialization";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //Thread.Sleep(1000);
            //string Data = "";            

            ////*****************Read User Card Company Code & site id & Match with Master Card************

            //Data = obj_Card.ConnectToCard();
            //if (Data != "")
            //{
            //    Message = "Error in connecting";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript,false);
            //    return;
            //}

            //string UserCardDetails = "";
            //UserCardDetails = obj_Card.ReadCMP(txtuid.Text.Trim(), txtpass.Text.Trim());

            //string UserCompCd = UserCardDetails.Substring(0, 4);
            //string UserSiteId = UserCardDetails.Substring(4, 3);

            ////*********end of reading User Card*******************************************************

            //if (MasterCompCd != UserCompCd && MasterSiteId != UserSiteId)
            //{
            //    Message = "Company code or site id does not match.Please Contact CMS Computers LTD";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
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
            //    string OldDataBlock1 = obj_Card.ReadBlock(1);
            //    string OldDataBlock2 = obj_Card.ReadBlock(2);

            //}
            ////****************end of reading app code***********************************************************

            ////****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

            ////Authenticate sec 14 with Master Key
            //Data = obj_Card.Authenticate(59, MasterKey, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 14 Block 59";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    string ReadData = obj_Card.ReadBlock(56);
            //    string Stemp = "";

            //    for (int i = 0; i < ReadData.Length; i++)
            //    {
            //        if (ReadData.Substring(i, 1) != " ")
            //        {
            //            Stemp += ReadData.Substring(i, 1);
            //        }
            //    }

            //    Stemp = obj_Card.Convert(Stemp);
            //    Rnd[3] = Stemp.Substring(Stemp.Length - 6, 6);
            //    Rnd[2] = Stemp.Substring(4, 6);
             
            //}
            ////****************End*************************************************************************

            ////***********below code for making sector 0 block 1,2 raw*********************************
            //Data = obj_Card.Authenticate(2, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 0 Block 2";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    string WriteData = "00000000000000000000020002000200";
            //    Data = obj_Card.WriteBlock(2, WriteData);
            //    if (Data != "")
            //    {
            //        Message = "Making Sector Raw.Card Error,Sector 0 Block 2";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //    string WriteData1 = "FF0F0000000000000000000000000000";
            //    Data = obj_Card.WriteBlock(1, WriteData1);
            //    if (Data != "")
            //    {
            //        Message = "Making Sector Raw.Card Error,Sector 0 Block 1";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //}
            ////**********End***************************************************************
            ////***********below code for making sector 1 raw*********************************

            //Data = obj_Card.Authenticate(6, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Message = "Authentication Error Sector 1 Block 6";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}
            //else
            //{
            //    string WriteData = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
            //    if (obj_Card.WriteBlock(6, WriteData) != "")
            //    {
            //        Message = "Error in writting data to Sector 1 Block 6";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}

            //Data = obj_Card.Authenticate(7, obj_Card.Cardrawkeybuf, 96);
            //if (Data != "")
            //{
            //    Data = obj_Card.Authenticate(7, "FFFFFFFFFFFF", 96);

            //    if (Data != "")
            //    {
            //        Message = "Authentication Error Sector 1 Block 7";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //}
            //else
            //{
            //    Data = obj_Card.MakeSectorRaw("7", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
            //    if (Data != "")
            //    {
            //        Message = "Making Sector Raw.Card Error,Sector 1 Block 7";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //}

            ////**********************End************************************************          
          
            //    //**************************Raw Sector 2******************************************
            //    string RandomNo = Rnd[2];
            //    if (obj_Card.RndSecRaw(RandomNo, "08", "02") != true)
            //    {
            //        Message = "Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //    if(obj_Card.RndSecRaw(RandomNo,"11","02") != true)
            //    {
            //        Message = "Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 11";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            //    string RandomNo1 = Rnd[3];
            //    if (obj_Card.RndSecRaw(RandomNo1, "12", "03") != true)
            //    {
            //        Message = "Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }
            //    if (obj_Card.RndSecRaw(RandomNo1,"15", "03") != true)
            //    {
            //        Message = "Making Sector Raw for Access Control.Card Error,Sector 3 Block 15";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;
            //    }

            
            ////****************************End************************************************

            //    //***********below code for making sector 13 raw*********************************
            //    Data = obj_Card.Authenticate(55, MasterKey, 96);
            //    if (Data != "")
            //    {
            //        Data = obj_Card.Authenticate(55, "FFFFFFFFFFFF", 96);

            //        if (Data != "")
            //        {
            //            Message = "Authentication Error Sector 13 Block 37";
            //            someScript = "";
            //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        Data = obj_Card.MakeSectorRaw("37", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
            //        if (Data != "")
            //        {
            //            Message = "Making Sector Raw.Card Error,Sector 13 Block 55";
            //            someScript = "";
            //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //            return;
            //        }

            //    }

            //    //**********************End************************************************

            //   //***********below code for making sector 14 raw*********************************
            //    Data = obj_Card.Authenticate(59, MasterKey , 96);
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
            //        Data = obj_Card.MakeSectorRaw("3B", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
            //        if (Data != "")
            //        {
            //            Message = "Making Sector Raw.Card Error,Sector 14 Block 59";
            //            someScript = "";
            //            someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //            return;
            //        }

            //    }

            //   //**********************End************************************************          
           
            ////**********************Raw Sector 15 block 60********************************
            //    Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);
            // if (Data != "")
            // {
            //     Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

            //     if (Data != "")
            //     {
            //         Message = "Authentication Error Sector 15 Block 60";
            //         someScript = "";
            //         someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //         return;
            //     }
            // }
            // else
            // {
            //     string ReadData = obj_Card.ReadBlock(60);
            //     string Stemp = "";

            //     for (int i = 0; i < ReadData.Length; i++)
            //     {
            //         if (ReadData.Substring(i, 1) != " ")
            //         {
            //             Stemp += ReadData.Substring(i, 1);
            //         }
            //     }

            //    // ReadData = obj_Card.Convert(Stemp);
            //     ReadData = Stemp;

            //     string Writedata = "000000000000000000000000000000" + ReadData.Substring(ReadData.Length - 2, 2);
            //     Data = obj_Card.WriteBlock(60, Writedata);
            //     if (Data != "")
            //     {
            //         Message = "Making Sector Raw.Card Error,Sector 15 Block 60";
            //         someScript = "";
            //         someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //         return;
            //     }

            //     Message = "Card Reinitialized Successfully.";
            //     someScript = "";
            //     someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);

            // }

            //*************end************************************************************
        }     

        protected void btnAccept_Click(object sender, EventArgs e)
        {
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

            //Thread.Sleep(1000);
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
          
               // Session["MasterKey"] = hdnMasterKey.Value;

                //MasterCompCd = hdnCompSiteCd.Value.Substring(0, 4);
                //MasterSiteId = hdnCompSiteCd.Value.Substring(4, 3);
                //Session["CmpCD"] = MasterCompCd;
                //Session["SiteID"] = MasterSiteId;

            pnlCardDetails.Visible = false;
            pnlRawCard.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtpass.Text = "";
            txtuid.Text = "";
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Uno_Dashboard.aspx");
        }
    }
}