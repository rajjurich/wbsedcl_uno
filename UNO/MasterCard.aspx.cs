using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UNO
{

    public partial class MasterCard : System.Web.UI.Page
    {
        public ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //obj_Card.WriteBlock(0, "");
            
            //obj_Card.ReadBlock
            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //string DataIntial = "";
            //string Message = "";
            //string someScript = "";
            //string[] Compdetails = new string[2];
            //string[] MasterCarddetails = new string[3];

            //Compdetails[0] = txtCompanyCode.Text;
            //Compdetails[1] = txtSiteId.Text;

            //MasterCarddetails[0] = txtUserId.Text;
            //MasterCarddetails[1] = txtPassword.Text;
            //MasterCarddetails[2] = txtMasterKey.Text;

            //DataIntial = obj_Card.Initialise().Trim();
            //if (DataIntial != "")
            //{
            //    Message = "Omnikey reader not connected or Error in card Initialization";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //string Data = "";
            //Data = obj_Card.ConnectToCard();
            //if (Data != "")
            //{
            //    Message = "Error in connecting";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}

            //Data = obj_Card.WriteMasterCard(Compdetails, MasterCarddetails);
            //if (DataIntial != "")
            //{
            //    Message = "Error in creating master card";
            //    someScript = "";
            //    someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //    return;
            //}
            //else
            //{                
            //        Message = "Master card created successfully";
            //        someScript = "";
            //        someScript = "<script language='javascript'>alert('" + Message + "');</script>";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", someScript, false);
            //        return;             
            //}

        }
    }
}