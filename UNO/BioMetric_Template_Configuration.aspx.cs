using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UNO.Core.Handler;

namespace UNO
{
    public partial class BioMetric_Template_Configuration : System.Web.UI.Page
    {
        static string strSuccMsg, strErrMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["radiocount"] = 0;
                GetRecord();
            }
        }

        protected void LHC1Changed(object sender, EventArgs e)
        {
            if (LHC1.Checked == true)
            {
                LHR1.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                LHR1.Enabled = false;
                if (LHR1.Checked != false)
                {
                    LHR1.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void LHC2Changed(object sender, EventArgs e)
        {
            if (LHC2.Checked == true)
            {
                LHR2.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                LHR2.Enabled = false;
                if (LHR2.Checked != false)
                {
                    LHR2.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void LHC3Changed(object sender, EventArgs e)
        {
            if (LHC3.Checked == true)
            {
                LHR3.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                LHR3.Enabled = false;
                if (LHR3.Checked != false)
                {
                    LHR3.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void LHC4Changed(object sender, EventArgs e)
        {
            if (LHC4.Checked == true)
            {
                LHR4.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                LHR4.Enabled = false;
                if (LHR4.Checked != false)
                {
                    LHR4.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void LHC5Changed(object sender, EventArgs e)
        {
            if (LHC5.Checked == true)
            {
                LHR5.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                LHR5.Enabled = false;
                if (LHR5.Checked != false)
                {
                    LHR5.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void RHC1Changed(object sender, EventArgs e)
        {
            if (RHC1.Checked == true)
            {
                RHR1.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                RHR1.Enabled = false;
                if (RHR1.Checked != false)
                {
                    RHR1.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void RHC2Changed(object sender, EventArgs e)
        {
            if (RHC2.Checked == true)
            {
                RHR2.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                RHR2.Enabled = false;
                if (RHR2.Checked != false)
                {
                    RHR2.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void RHC3Changed(object sender, EventArgs e)
        {
            if (RHC3.Checked == true)
            {
                RHR3.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                RHR3.Enabled = false;
                if (RHR3.Checked != false)
                {
                    RHR3.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void RHC4Changed(object sender, EventArgs e)
        {
            if (RHC4.Checked == true)
            {
                RHR4.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                RHR4.Enabled = false;
                if (RHR4.Checked != false)
                {
                    RHR4.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void RHC5Changed(object sender, EventArgs e)
        {
            if (RHC5.Checked == true)
            {
                RHR5.Enabled = true;
                //Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
            else
            {
                RHR5.Enabled = false;
                if (RHR5.Checked != false)
                {
                    RHR5.Checked = false;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
                }
            }
        }

        protected void LHR1Changed(object sender, EventArgs e)
        {
            if (LHR1.Checked == false)
            {
                LHR1.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR1.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR2Changed(object sender, EventArgs e)
        {
            if (LHR2.Checked == false)
            {
                LHR2.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR2.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR3Changed(object sender, EventArgs e)
        {
            if (LHR3.Checked == false)
            {
                LHR3.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR3.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR4Changed(object sender, EventArgs e)
        {
            if (LHR4.Checked == false)
            {
                LHR4.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR4.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"])+ 1;
            }
        }

        protected void LHR5Changed(object sender, EventArgs e)
        {
            if (LHR5.Checked == false)
            {
                LHR5.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR5.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR1Changed(object sender, EventArgs e)
        {
            if (RHR1.Checked == false)
            {
                RHR1.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR1.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR2Changed(object sender, EventArgs e)
        {
            if (RHR2.Checked == false)
            {
                RHR2.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR2.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR3Changed(object sender, EventArgs e)
        {
            if (RHR3.Checked == false)
            {
                RHR3.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR3.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR4Changed(object sender, EventArgs e)
        {
            if (RHR4.Checked == false)
            {
                RHR4.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR4.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR5Changed(object sender, EventArgs e)
        {
            if (RHR5.Checked == false)
            {
                RHR5.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR5.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        void GetRecord()
        {
            string[] ArrFingureCount;
            string[] ArrFingureForTA;
            try
            {
                List<BioMatricTemp> lstCommonView = GetcommonList();
                if (lstCommonView.Count > 0)
                {
                    btnBioMetric.Text = "Update";
                    ddlNoofFig.SelectedIndex = lstCommonView[0].NoOfFingers;
                    ArrFingureCount = lstCommonView[0].FingureCount.Split(',');
                    ArrFingureForTA = lstCommonView[0].FingureForTA.Split(',');

                    for (int i = 0; i < ArrFingureCount.Length; i++)
                    {
                        switch (ArrFingureCount[i])
                        {
                            case "1":
                                LHC1.Checked = true;
                                LHR1.Enabled = true;
                                break;
                            case "2":
                                LHC2.Checked = true;
                                LHR2.Enabled = true;
                                break;
                            case "3":
                                LHC3.Checked = true;
                                LHR3.Enabled = true;
                                break;
                            case "4":
                                LHC4.Checked = true;
                                LHR4.Enabled = true;
                                break;
                            case "5":
                                LHC5.Checked = true;
                                LHR5.Enabled = true;
                                break;
                            case "6":
                                RHC1.Checked = true;
                                RHR1.Enabled = true;
                                break;
                            case "7":
                                RHC2.Checked = true;
                                RHR2.Enabled = true;
                                break;
                            case "8":
                                RHC3.Checked = true;
                                RHR3.Enabled = true;
                                break;
                            case "9":
                                RHC4.Checked = true;
                                RHR4.Enabled = true;
                                break;
                            case "10":
                                RHC5.Checked = true;
                                RHR5.Enabled = true;
                                break;
                        }
                    }
                    for (int i = 0; i < ArrFingureForTA.Length; i++)
                    {
                        switch (ArrFingureForTA[i])
                        {
                            case "1":
                                LHR1.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "2":
                                LHR2.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "3":
                                LHR3.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "4":
                                LHR4.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "5":
                                LHR5.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "6":
                                RHR1.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "7":
                                RHR2.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "8":
                                RHR3.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "9":
                                RHR4.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                            case "10":
                                RHR5.Checked = true;
                                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                                break;
                        }
                    }
                }
                else
                {
                    btnBioMetric.Text = "Save";
                    LHR4.Checked = true;
                    RHR2.Checked = true;
                    Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 2;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        private List<BioMatricTemp> GetcommonList()
        {
            List<BioMatricTemp> lstCommonView;
            lstCommonView = CMS.UNO.Core.Handler.clsBioMetrictemplateconfigurationHandler.GetCommonData("Common");
            Session["lstCommon"] = lstCommonView;
            return lstCommonView;
        }

        protected void btnBioMetric_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt16(Session["radiocount"]) !=2)
                lblMessages.Text = "Select at least two finger for time attendance";
            else
            {
            int count = Convert.ToInt32(ddlNoofFig.SelectedValue);
            string nooffig = string.Empty;
            string FingureTA = string.Empty;
            if (LHC1.Checked == true)
                nooffig = nooffig + "1,";
            if (LHC2.Checked == true)
                nooffig = nooffig + "2,";
            if (LHC3.Checked == true)
                nooffig = nooffig + "3,";
            if (LHC4.Checked == true)
                nooffig = nooffig + "4,";
            if (LHC5.Checked == true)
                nooffig = nooffig + "5,";
            if (RHC1.Checked == true)
                nooffig = nooffig + "6,";
            if (RHC2.Checked == true)
                nooffig = nooffig + "7,";
            if (RHC3.Checked == true)
                nooffig = nooffig + "8,";
            if (RHC4.Checked == true)
                nooffig = nooffig + "9,";
            if (RHC5.Checked == true)
                nooffig = nooffig + "10,";

            if (LHR1.Checked == true)
                FingureTA = FingureTA + "1,";
            if (LHR2.Checked == true)
                FingureTA = FingureTA + "2,";
            if (LHR3.Checked == true)
                FingureTA = FingureTA + "3,";
            if (LHR4.Checked == true)
                FingureTA = FingureTA + "4,";
            if (LHR5.Checked == true)
                FingureTA = FingureTA + "5,";
            if (RHR1.Checked == true)
                FingureTA = FingureTA + "6,";
            if (RHR2.Checked == true)
                FingureTA = FingureTA + "7,";
            if (RHR3.Checked == true)
                FingureTA = FingureTA + "8,";
            if (RHR4.Checked == true)
                FingureTA = FingureTA + "9,";
            if (RHR5.Checked == true)
                FingureTA = FingureTA + "10,";


            nooffig = nooffig.Substring(0, nooffig.Length - 1);
            FingureTA = FingureTA.Substring(0, FingureTA.Length - 1);


            string[] bow = nooffig.Split(',');
            string nooffiglen = string.Empty;
            foreach (var item in bow)
            {
                nooffiglen = nooffiglen + item;
            }
            string[] FTA = FingureTA.Split(',');
            string FingureTAlen = string.Empty;
            foreach (var item in FTA)
            {
                FingureTAlen = FingureTAlen + item;
            }

            if (bow.Length == count)
            {
                if (FTA.Length == 2)
                {
                    if (btnBioMetric.Text == "Update")
                    {
                        BioMatricTemp objCommon = new BioMatricTemp();
                        objCommon.NoOfFingers = Convert.ToInt32(ddlNoofFig.SelectedValue);
                        objCommon.FingureCount = nooffig;
                        objCommon.FingureForTA = FingureTA;
                        clsBioMetrictemplateconfigurationHandler.InsertCommonDetails(objCommon, "Update", ref strErrMsg, ref strSuccMsg);

                        if (strErrMsg.Trim().Length >= 1)
                        {
                            lblMessages.Text = strErrMsg;
                        }
                        else
                        {
                            lblMessages.Text = strSuccMsg;
                        }
                        GetRecord();
                    }
                    else if (btnBioMetric.Text == "Save")
                    {
                        BioMatricTemp objCommon = new BioMatricTemp();
                        objCommon.NoOfFingers = Convert.ToInt32(ddlNoofFig.SelectedValue);
                        objCommon.FingureCount = nooffig;
                        objCommon.FingureForTA = FingureTA;
                        clsBioMetrictemplateconfigurationHandler.InsertCommonDetails(objCommon, "Insert", ref strErrMsg, ref strSuccMsg);

                        if (strErrMsg.Trim().Length >= 1)
                        {
                            lblMessages.Text = strErrMsg;
                        }
                        else
                        {
                            lblMessages.Text = strSuccMsg;
                        }
                        GetRecord();
                    }
                }
                else
                    lblMessages.Text = "Please Select at least two finger for time attendance";
            }
            else
                lblMessages.Text = "Selected Number Of finger and Enroll finger are not matched";
            }
        }
        
    }
}