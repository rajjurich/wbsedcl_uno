using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using CMS.UNO.Core.Handler;
using System.Text;


namespace UNO
{
    public partial class EPDViewNew : System.Web.UI.Page
    {
        public string ImageUrl;       
        string strAdddeletedDATE;
        string strOfficialDATE;
        string strPersonalDATE;
        string default_path = null;
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                FillDropDowns();
                FillEmpStatus();
                FillReason();
                FillStates();
                FillReligion();
                FillStatus();
                bindDataGrid();
                ViewState["AppMode"] = "Add";
                Rbtnchecked.SelectedIndex = 0;
                EPD_EMPLOYEEID.Focus();
                default_path = Server.MapPath(@"~/EmpImage/");
                BindManager();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvEmployee.ClientID + "');");
                //Added by Pooja Yadav to check the ESS Status
                chkESSStatus();
            }

            default_path = Server.MapPath(@"~/EmpImage/");

            strAdddeletedDATE = DateTime.Now.ToString("dd/MM/yyyy");
            strOfficialDATE = DateTime.Now.ToString("dd/MM/yyyy");
            strPersonalDATE = DateTime.Now.ToString("dd/MM/yyyy");

        }
        public void FillDropDowns()
        {
            DataSet ds = clsCommonHandler.GetCommonTableDetails("");

            ddlcompany.DataValueField = "ID";
            ddlcompany.DataTextField = "Value";
            ddlcompany.DataSource = ds.Tables[0];
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0, "Select");
            ddlcompany.SelectedIndex = 0;

            ddllocation.DataValueField = "ID";
            ddllocation.DataTextField = "Value";
            ddllocation.DataSource = ds.Tables[1];
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, "Select");

            ddldivision.DataValueField = "ID";
            ddldivision.DataTextField = "Value";
            ddldivision.DataSource = ds.Tables[2];
            ddldivision.DataBind();
            ddldivision.Items.Insert(0, "Select");

            ddldepartment.DataValueField = "ID";
            ddldepartment.DataTextField = "Value";
            ddldepartment.DataSource = ds.Tables[3];
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, "Select");

            ddldesignation.DataValueField = "ID";
            ddldesignation.DataTextField = "Value";
            ddldesignation.DataSource = ds.Tables[4];
            ddldesignation.DataBind();
            ddldesignation.Items.Insert(0, "Select");

            ddlcategory.DataValueField = "ID";
            ddlcategory.DataTextField = "Value";
            ddlcategory.DataSource = ds.Tables[5];
            ddlcategory.DataBind();
            ddlcategory.Items.Insert(0, "Select");

            ddlgroup.DataValueField = "ID";
            ddlgroup.DataTextField = "Value";
            ddlgroup.DataSource = ds.Tables[6];
            ddlgroup.DataBind();
            ddlgroup.Items.Insert(0, "Select");


            ddlgrade.DataValueField = "ID";
            ddlgrade.DataTextField = "Value";
            ddlgrade.DataSource = ds.Tables[7];
            ddlgrade.DataBind();
            ddlgrade.Items.Insert(0, "Select");

        }
        private void FillEmpStatus()
        {
            try
            {

                DataTable dt = clsCommonHandler.GetEntParameterValues("EMPSTATUS", "ENT");
                ddlstatus.DataValueField = "CODE";
                ddlstatus.DataTextField = "Value";
                ddlstatus.DataSource = dt;
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillReason()
        {
            try
            {

                DataTable dt = clsCommonHandler.GetReasonDetails();
                ddlreason.DataValueField = "Reason_ID";
                ddlreason.DataTextField = "Reason_Description";
                ddlreason.DataSource = dt;
                ddlreason.DataBind();
                ddlreason.Items.Insert(0, new ListItem("Select", "Select"));

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillStates()
        {
            try
            {

                DataTable dt = clsCommonHandler.GetEntParameterValues("STATES", "STATES");
                EAD_STATE1.DataValueField = "CODE";
                EAD_STATE1.DataTextField = "Value";
                EAD_STATE1.DataSource = dt;
                EAD_STATE1.DataBind();
                EAD_STATE1.Items.Insert(0, "Select");

                EAD_STATE2.DataValueField = "CODE";
                EAD_STATE2.DataTextField = "Value";
                EAD_STATE2.DataSource = dt;
                EAD_STATE2.DataBind();
                EAD_STATE2.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillStatus()
        {
            try
            {

                DataTable dt = clsCommonHandler.GetEntParameterValues("MARITALSTATUS", "ENT");
                EPD_MARITAL_STATUS.DataValueField = "CODE";
                EPD_MARITAL_STATUS.DataTextField = "Value";
                EPD_MARITAL_STATUS.DataSource = dt;
                EPD_MARITAL_STATUS.DataBind();
                EPD_MARITAL_STATUS.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }


        }
        private void FillReligion()
        {
            try
            {

                DataTable dt = clsCommonHandler.GetEntParameterValues("RELIGION", "ENT");

                EPD_RELIGION.DataValueField = "CODE";
                EPD_RELIGION.DataTextField = "Value";
                EPD_RELIGION.DataSource = dt;
                EPD_RELIGION.DataBind();
                EPD_RELIGION.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        void bindDataGrid()
        {
            try
            {

                clsEmployee objEmp = new clsEmployee();
                DataTable dt = clsEmployeeHandler.GetEmployeeManagerDetails(ddlEmpStatus.SelectedValue.ToString(), "AllEmployees","");

                gvEmployee.DataSource = dt;
                gvEmployee.DataBind();

                if (dt.Rows.Count != 0)
                {

                    DropDownList ddl = (DropDownList)gvEmployee.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvEmployee.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvEmployee.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvEmployee.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvEmployee.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvEmployee.PageCount == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex + 1 == gvEmployee.PageCount)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }


                    ((Label)gvEmployee.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployee.PageSize * gvEmployee.PageIndex) + 1) + " to " + (((gvEmployee.PageSize * (gvEmployee.PageIndex + 1)) - 10) + gvEmployee.Rows.Count);

                    gvEmployee.BottomPagerRow.Visible = true;
                }

                if (dt.Rows.Count != 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }


            }
            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = Convert.ToInt32(((DropDownList)gvEmployee.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = gvEmployee.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = gvEmployee.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }

        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {


        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            //txtemp_code.Text = "";
            //txtemp_name.Text = "";

            txtCompanyID.Text = "";
            txtCompanyName.Text = "";
            bindDataGrid();
        }
        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        protected void Modify_Data(string Employeeid)
        {
            lblSaveMessages.Text = string.Empty;
            string ead_addtype = "";
            string EmpDob;
            string EmpJndt;
            string Confrmdt;

            try
            {
                DataTable dtAllData = clsEmployeeHandler.GetEmployeeManagerDetails("", "EmpSpecificData", Employeeid);


                if (dtAllData.Rows[0]["TdayPresence"].ToString() == "P")
                    joindt.Enabled = false;
                else
                    joindt.Enabled = true;


                if (Convert.ToString(dtAllData.Rows[0]["TempID"]) != "")
                {
                    EPD_TEMP_CARD_NO.Text = Convert.ToString(dtAllData.Rows[0]["TempID"]);
                }


                string gender = dtAllData.Rows[0]["EPD_GENDER"].ToString();               
                if (dtAllData.Rows[0]["EPD_PHOTOURL"].ToString() != "")
                {
                    Session["ServerImagePath_U"] = dtAllData.Rows[0]["EPD_PHOTOURL"].ToString();
                    DisplayEmployeeImage(dtAllData.Rows[0]["EPD_PHOTOURL"].ToString());

                }
                else if (gender == "F")
                {
                    string default_male = default_path + "Default_Preview.jpg";

                    DisplayEmployeeImage(default_male);

                }
                else if (gender == "M")
                {
                    string default_female = default_path + "Default_Preview.jpg";

                    DisplayEmployeeImage(default_female);
                }

                EPD_EMPLOYEEID.Text = dtAllData.Rows[0]["EPD_EMPID"].ToString();
                EPD_CARD_NO.Text = dtAllData.Rows[0]["EPD_CARD_ID"].ToString();
                EPD_SALUTATION.Text = dtAllData.Rows[0]["EPD_SALUTATION"].ToString();
                EPD_FIRST_NAME.Text = dtAllData.Rows[0]["EPD_FIRST_NAME"].ToString();
                EPD_MIDDLE_NAME.Text = dtAllData.Rows[0]["EPD_MIDDLE_NAME"].ToString();
                EPD_PREVIOUS_CODE.Text = dtAllData.Rows[0]["EPD_PREVIOUS_ID"].ToString();
                EPD_LAST_NAME.Text = dtAllData.Rows[0]["EPD_LAST_NAME"].ToString();
                EPD_GENDER.SelectedValue = dtAllData.Rows[0]["EPD_GENDER"].ToString();

                EPD_MIDDLE_NAME.Text = dtAllData.Rows[0]["EPD_MIDDLE_NAME"].ToString();
                EPD_PREVIOUS_CODE.Text = dtAllData.Rows[0]["EPD_PREVIOUS_ID"].ToString();
                EPD_LAST_NAME.Text = dtAllData.Rows[0]["EPD_LAST_NAME"].ToString();
                EPD_GENDER.SelectedValue = dtAllData.Rows[0]["EPD_GENDER"].ToString();
                EmpDob = Convert.ToDateTime(dtAllData.Rows[0]["EPD_DOB"]).ToString("dd/MM/yyyy");                
                DOB.Text = EmpDob;
                
                if (dtAllData.Rows[0]["EPD_MARITAL_STATUS"].ToString() == "")
                {
                    EPD_MARITAL_STATUS.SelectedIndex = 0;
                }
                else
                {
                    EPD_MARITAL_STATUS.SelectedValue = dtAllData.Rows[0]["EPD_MARITAL_STATUS"].ToString();
                }

                EPD_NICKNAME.Text = dtAllData.Rows[0]["EPD_NICKNAME"].ToString();

                if (dtAllData.Rows[0]["EPD_RELIGION"].ToString() == "")
                {
                    EPD_RELIGION.SelectedIndex = 0;
                }
                else
                {
                    EPD_RELIGION.SelectedValue = dtAllData.Rows[0]["EPD_RELIGION"].ToString();
                }

                
                EPD_REFERENCE_ONE.Text = dtAllData.Rows[0]["EPD_REFERENCE_ONE"].ToString();
                EPD_REFERENCE_TWO.Text = dtAllData.Rows[0]["EPD_REFERENCE_TWO"].ToString();
                EPD_NICKNAME.Text = dtAllData.Rows[0]["EPD_NICKNAME"].ToString();
                EPD_DOMICILE.Text = dtAllData.Rows[0]["EPD_DOMICILE"].ToString();
                EPD_BLOODGROUP.Text = dtAllData.Rows[0]["EPD_BLOODGROUP"].ToString();
                EPD_EMAIL.Text = dtAllData.Rows[0]["EPD_EMAIL"].ToString();
                EPD_PAN.Text = dtAllData.Rows[0]["EPD_PAN"].ToString();
                EPD_DOCTOR.Text = dtAllData.Rows[0]["EPD_DOCTOR"].ToString();
                txtAadhar.Text = dtAllData.Rows[0]["EPD_AADHARCARD"].ToString();



                ead_addtype = dtAllData.Rows[0]["P_ADDRESS_TYPE"].ToString();

                EAD_ADDRESS1.Text = dtAllData.Rows[0]["P_ADDRESS"].ToString();
                EAD_CITY1.Text = dtAllData.Rows[0]["PCITY"].ToString();
                EAD_PIN1.Text = dtAllData.Rows[0]["PPIN"].ToString();

                if (dtAllData.Rows[0]["PSTATE"].ToString() == "")
                        {
                            EAD_STATE1.SelectedIndex = 0;
                        }
                        else
                        {
                            EAD_STATE1.SelectedValue = dtAllData.Rows[0]["PSTATE"].ToString();
                        }


                EAD_Country1.Text = dtAllData.Rows[0]["PCOUNTRY"].ToString();
                EAD_Phone1.Text = dtAllData.Rows[0]["PPHONE_ONE"].ToString();
                EAD_Phone2.Text = dtAllData.Rows[0]["PPHONE_TWO"].ToString();


                EAD_ADDRESS2.Text = dtAllData.Rows[0]["T_ADDRESS"].ToString();
                EAD_CITY2.Text = dtAllData.Rows[0]["TCITY"].ToString();
                EAD_PIN2.Text = dtAllData.Rows[0]["TPIN"].ToString();

                if (dtAllData.Rows[0]["TSTATE"].ToString() == "")
                        {
                            EAD_STATE2.SelectedIndex = 0;
                        }
                        else
                        {
                            EAD_STATE2.SelectedValue = dtAllData.Rows[0]["TSTATE"].ToString();
                        }

                EAD_COUNTRY2.Text = dtAllData.Rows[0]["TCOUNTRY"].ToString();
                EAD_Phone3.Text = dtAllData.Rows[0]["TPHONE_ONE"].ToString();
                EAD_Phone4.Text = dtAllData.Rows[0]["TPHONE_TWO"].ToString();


                EmpJndt = Convert.ToDateTime(dtAllData.Rows[0]["EOD_JOINING_DATE"]).ToString("dd/MM/yyyy");


                joindt.Text = EmpJndt;

                Confrmdt = Convert.ToDateTime(dtAllData.Rows[0]["EOD_CONFIRM_DATE"]).ToString("dd/MM/yyyy");
                if (Confrmdt == "01/01/1900" || Confrmdt == "")
                {
                    Confdt1.Text = "";
                }
                else
                {
                    Confdt1.Text = Confrmdt;
                }
                if (!(dtAllData.Rows[0]["EOD_RETIREMENT_DATE"].ToString().Contains("01/01/1900")) && dtAllData.Rows[0]["EOD_RETIREMENT_DATE"].ToString() != "")
                    Retdt1.Text = Convert.ToDateTime(dtAllData.Rows[0]["EOD_RETIREMENT_DATE"]).ToString("dd/MM/yyyy");
                else
                    Retdt1.Text = string.Empty;
                if (ddlreason.Items.FindByValue(dtAllData.Rows[0]["EOD_RETIREMENT_REASON_ID"].ToString()) == null)
                {
                    ddlreason.SelectedIndex = 0;
                }
                else
                {
                    ddlreason.SelectedValue = dtAllData.Rows[0]["EOD_RETIREMENT_REASON_ID"].ToString();
                }


                if (ddlcompany.Items.FindByValue(dtAllData.Rows[0]["EOD_COMPANY_ID"].ToString()) == null)

                    ddlcompany.SelectedIndex = 0;
                else
                    ddlcompany.SelectedValue = dtAllData.Rows[0]["EOD_COMPANY_ID"].ToString();

                if (ddllocation.Items.FindByValue(dtAllData.Rows[0]["EOD_LOCATION_ID"].ToString()) == null)

                    ddllocation.SelectedIndex = 0;
                else
                    ddllocation.SelectedValue = dtAllData.Rows[0]["EOD_LOCATION_ID"].ToString();

                if (ddldivision.Items.FindByValue(dtAllData.Rows[0]["EOD_DIVISION_ID"].ToString()) == null)

                    ddldivision.SelectedIndex = 0;
                else
                    ddldivision.SelectedValue = dtAllData.Rows[0]["EOD_DIVISION_ID"].ToString();


                if (ddldepartment.Items.FindByValue(dtAllData.Rows[0]["EOD_DEPARTMENT_ID"].ToString()) == null)

                    ddldepartment.SelectedIndex = 0;
                else
                    ddldepartment.SelectedValue = dtAllData.Rows[0]["EOD_DEPARTMENT_ID"].ToString();


                if (ddldesignation.Items.FindByValue(dtAllData.Rows[0]["EOD_DESIGNATION_ID"].ToString()) == null)

                    ddldesignation.SelectedIndex = 0;
                else
                    ddldesignation.SelectedValue = dtAllData.Rows[0]["EOD_DESIGNATION_ID"].ToString();

               
                if (ddlcategory.Items.FindByValue(dtAllData.Rows[0]["EOD_CATEGORY_ID"].ToString()) == null)
                {
                    ddlcategory.SelectedIndex = 0;
                }
                else
                    ddlcategory.SelectedValue = dtAllData.Rows[0]["EOD_CATEGORY_ID"].ToString();

                if (ddlgroup.Items.FindByValue(dtAllData.Rows[0]["EOD_GROUP_ID"].ToString()) == null)
                {
                    ddlgroup.SelectedIndex = 0;
                }
                else
                    ddlgroup.SelectedValue = dtAllData.Rows[0]["EOD_GROUP_ID"].ToString();


                if (ddlgrade.Items.FindByValue(dtAllData.Rows[0]["EOD_GRADE_ID"].ToString()) == null)

                    ddlgrade.SelectedIndex = 0;
                else
                    ddlgrade.SelectedValue = dtAllData.Rows[0]["EOD_GRADE_ID"].ToString();


                if (ddlstatus.Items.FindByValue(dtAllData.Rows[0]["EOD_STATUS"].ToString()) == null)

                    ddlstatus.SelectedIndex = 0;
                else
                    ddlstatus.SelectedValue = dtAllData.Rows[0]["EOD_STATUS"].ToString();


                

                if (dtAllData.Rows[0]["EOD_STATUS"].ToString() == "C")
                    Confdt1.Enabled = true;
                else
                {
                    Confdt1.Enabled = false;
                    Confdt1.Text = "";
                }
                Rbtnchecked.SelectedValue = dtAllData.Rows[0]["EOD_ACTIVE"].ToString();
             
                if (Convert.ToString(dtAllData.Rows[0]["Hier_Mgr_ID"]) != "")
                {
                    if (ddlManager.Items.FindByValue(dtAllData.Rows[0]["Hier_Mgr_ID"].ToString()) == null)
                    {
                        ddlManager.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlManager.SelectedValue = dtAllData.Rows[0]["Hier_Mgr_ID"].ToString();
                    }

                }
                if (dtAllData.Rows[0]["EOD_ACTIVE"].ToString() != "")
                {
                    if (Convert.ToString(dtAllData.Rows[0]["essenabled"]) != "")
                    {
                        if (Convert.ToString(dtAllData.Rows[0]["essenabled"]) == "1" && dtAllData.Rows[0]["EOD_ACTIVE"].ToString() == "1")
                            chkEssEnable.Checked = true;
                        else
                            chkEssEnable.Checked = false;
                    }
                    else
                        chkEssEnable.Checked = false;

                }
                else
                {
                    chkEssEnable.Checked = false;
                }




            }


            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");

            }
           
        }
        private void DisplayEmployeeImage(string strImageUrl)
        {
            //strImageUrl="";

            imgEmployeeImage.ImageUrl = "";
            imgEmployeeImage.ImageUrl = "~/Handler1.ashx?ImagePath=" + strImageUrl;

        }
        public void chkchecked()
        {

            bool isAddressSame = false, isCitySame = false, isCountrySame = false, isPinCodeSame = false, isPhone1Same = false, isPhone2Same = false;

            if (EAD_ADDRESS1.Text != "" && EAD_ADDRESS2.Text != "")
            {
                if (EAD_ADDRESS1.Text == EAD_ADDRESS2.Text)
                {
                    isAddressSame = true;
                }
                else
                {
                    isAddressSame = false;
                }
            }



            if (EAD_CITY1.Text != "" && EAD_CITY2.Text != "")
            {
                if (EAD_CITY1.Text == EAD_CITY2.Text)
                {
                    isCitySame = true;
                }
                else
                {
                    isCitySame = false;
                }
            }



            if (EAD_Country1.Text != "" && EAD_COUNTRY2.Text != "")
            {
                if (EAD_Country1.Text == EAD_COUNTRY2.Text)
                {
                    isCountrySame = true;
                }
                else
                {
                    isCountrySame = false;
                }
            }


            if (EAD_Phone1.Text != "" && EAD_Phone3.Text != "")
            {
                if (EAD_Phone1.Text == EAD_Phone3.Text)
                {
                    isPhone1Same = true;
                }
                else
                {
                    isPhone1Same = false;
                }
            }


            if (EAD_Phone2.Text != "" && EAD_Phone4.Text != "")
            {
                if (EAD_Phone2.Text == EAD_Phone4.Text)
                {
                    isPhone2Same = true;
                }
                else
                {
                    isPhone2Same = false;
                }
            }

            if (EAD_PIN1.Text != "" && EAD_PIN2.Text != "")
            {
                if (EAD_PIN1.Text == EAD_PIN2.Text)
                {
                    isPinCodeSame = true;
                }
                else
                {
                    isPinCodeSame = false;
                }
            }


            if (isAddressSame == true && isCountrySame == true && isCitySame == true && isPinCodeSame == true && isPhone1Same == true && isPhone2Same == true)
            {
                ChkAddress.Checked = true;
            }
            else
            {
                ChkAddress.Checked = false;
            }


            if (ChkAddress.Checked)
            {
                EAD_ADDRESS2.Enabled = false;
                EAD_CITY2.Enabled = false;
                EAD_COUNTRY2.Enabled = false;
                EAD_Phone4.Enabled = false;
                EAD_Phone3.Enabled = false;
                EAD_PIN2.Enabled = false;
                EAD_STATE2.Enabled = false;
            }
            else
            {
                EAD_ADDRESS2.Enabled = true;
                EAD_CITY2.Enabled = true;
                EAD_COUNTRY2.Enabled = true;
                EAD_Phone4.Enabled = true;
                EAD_Phone3.Enabled = true;
                EAD_PIN2.Enabled = true;
                EAD_STATE2.Enabled = true;
            }

        }
        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Edit3")
                {

                    string employeeid = e.CommandArgument.ToString();
                    
                    Modify_Data(employeeid);
                    ViewState["AppMode"] = "EDIT";
                    EPD_EMPLOYEEID.Enabled = false;

                    chkchecked();
                    mpeAddEmployee.Show();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Script", "validateChosen();", true);
                }





            }
            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }

        }
        protected void btnAddE_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            string strSucMsg = string.Empty;
           
            try
            {

                if (Retdt1.Text.Trim() != string.Empty)
                {
                    if (ddlreason.SelectedValue == "Select")
                    {
                        lblSaveMessages.Text = "Please select retirement reason";
                        lblSaveMessages.Visible = true;
                        mpeAddEmployee.Show();
                        return;
                    }
                }


                if (Confdt1.Text == "")
                {
                    if (ddlstatus.SelectedValue == "C")
                    {
                        this.lblSaveMessages.Text = "Please Enter Confirmation Date";
                        this.lblSaveMessages.Visible = true;
                        Confdt1.Enabled = true;
                        mpeAddEmployee.Show();
                        return;
                    }

                }



                if (!RetrieveEmployeeImage())
                {
                    mpeAddEmployee.Show();
                    return;
                }


                if (ViewState["AppMode"].ToString() == "EDIT")
                {
                    InsertOrUpdate("Update", ref strErrMsg, ref strSucMsg);

                    if (strErrMsg.Trim().Length > 0)
                    {
                        this.lblSaveMessages.Text = strErrMsg;
                        this.lblSaveMessages.Visible = true;
                        mpeAddEmployee.Show();
                        return;
                    }
                    else
                    {
                        ViewState["AppMode"] = "Add";
                        joindt.Enabled = true;
                        this.lblMessages.Text = strSucMsg;
                        this.lblMessages.Visible = true;
                    }
                }


                else
                {
                    InsertOrUpdate("Insert", ref strErrMsg, ref strSucMsg);

                    if (strErrMsg.Trim().Length > 0)
                    {
                        this.lblSaveMessages.Text = strErrMsg;
                        this.lblSaveMessages.Visible = true;

                        mpeAddEmployee.Show();
                        return;
                    }
                    else
                    {
                        ViewState["AppMode"] = "Add";
                        this.lblSaveMessages.Text = strSucMsg;
                        this.lblSaveMessages.Visible = true;
                        mpeAddEmployee.Show();
                    }



                }
            }

            catch (Exception ex)
            {

               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");


            }
           
            bindDataGrid();
            cleartextbox();
            EPD_EMPLOYEEID.Focus();

        }
        private void InsertOrUpdate(string strCommand, ref string ErrMsg, ref string strSucMsg)
        {

            string NickName = EPD_NICKNAME.Text.Trim().Length > 0 ? (EPD_NICKNAME.Text.Trim().First().ToString().ToUpper() + String.Join("", EPD_NICKNAME.Text.Trim().Skip(1))) : "";
            string MiddleName = EPD_MIDDLE_NAME.Text.Trim().Length > 0 ? (EPD_MIDDLE_NAME.Text.Trim().First().ToString().ToUpper() + String.Join("", EPD_MIDDLE_NAME.Text.Trim().Skip(1))) : "";
            clsEmployee objEmp = new clsEmployee();
            objEmp.CreatedBy = Session["uid"].ToString();
            objEmp.EMPLOYEEID = EPD_EMPLOYEEID.Text.Trim().ToUpper();
            objEmp.SALUTATION = EPD_SALUTATION.Text.Trim();
            objEmp.FName = (EPD_FIRST_NAME.Text.Trim().First().ToString().ToUpper() + String.Join("", EPD_FIRST_NAME.Text.Trim().Skip(1)));
            objEmp.MName = MiddleName;
            objEmp.Lname = (EPD_LAST_NAME.Text.Trim().First().ToString().ToUpper() + String.Join("", EPD_LAST_NAME.Text.Trim().Skip(1)));
            objEmp.NickName = NickName;
            objEmp.PreviousId = EPD_PREVIOUS_CODE.Text.Trim();
            objEmp.tempCardId = EPD_TEMP_CARD_NO.Text.Trim().ToUpper();
            objEmp.Gender = EPD_GENDER.SelectedValue.ToString();
            objEmp.MaritalStatus = EPD_MARITAL_STATUS.SelectedIndex == 0 ? "" : EPD_MARITAL_STATUS.SelectedValue.ToString();
            objEmp.DOB = DOB.Text.Trim() == "" ? null : DateTime.ParseExact(DOB.Text.Trim(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            objEmp.Religion = EPD_RELIGION.SelectedIndex == 0 ? "" : EPD_RELIGION.SelectedValue.ToString();
            objEmp.Ref1 = EPD_REFERENCE_ONE.Text.Trim();
            objEmp.Ref2 = EPD_REFERENCE_TWO.Text.Trim();
            objEmp.bldGroup = EPD_BLOODGROUP.Text.Trim();
            objEmp.email = EPD_EMAIL.Text.Trim();
            objEmp.Domicile = EPD_DOMICILE.Text.Trim();
            objEmp.Pan = EPD_PAN.Text.Trim();
            objEmp.Doctor = EPD_DOCTOR.Text.Trim();
            objEmp.PreviousId = EPD_PREVIOUS_CODE.Text.Trim();
            objEmp.cardID = EPD_CARD_NO.Text.Trim();

            if (strCommand.ToLower() == "insert")
                objEmp.PhotoUrl = Session["ServerImagePath"] == null ? "" : Session["ServerImagePath"].ToString();
            else if (strCommand.ToLower() == "update")
                objEmp.PhotoUrl = Session["ServerImagePath_U"] == null ? "" : Session["ServerImagePath_U"].ToString();

            objEmp.AdharCardNo = txtAadhar.Text.Trim();
            objEmp.TAddress = EAD_ADDRESS1.Text.Trim();
            objEmp.TCity = (EAD_CITY1.Text.Trim());
            objEmp.TPin = (EAD_PIN1.Text.Trim());
            objEmp.TState = (EAD_STATE1.SelectedIndex == 0 ? "" : EAD_STATE1.SelectedValue.ToString());
            objEmp.TCOUNTRY = (EAD_Country1.Text.Trim());
            objEmp.TPhone1 = (EAD_Phone1.Text.Trim());
            objEmp.TPhone2 = (EAD_Phone2.Text.Trim());

            if (ChkAddress.Checked)
            {
                objEmp.PAddress = (EAD_ADDRESS1.Text.Trim());
                objEmp.PCity = (EAD_CITY1.Text.Trim());
                objEmp.PPin = (EAD_PIN1.Text.Trim());
                objEmp.PState = (EAD_STATE1.SelectedIndex == 0 ? "" : EAD_STATE1.SelectedValue.ToString());
                objEmp.PCOUNTRY = (EAD_Country1.Text.Trim());
                objEmp.PPhone1 = (EAD_Phone1.Text.Trim());
                objEmp.PPhone2 = (EAD_Phone2.Text.Trim());

            }
            else
            {
                objEmp.PAddress = (EAD_ADDRESS2.Text.Trim());
                objEmp.PCity = (EAD_CITY2.Text.Trim());
                objEmp.PPin = (EAD_PIN2.Text.Trim());
                objEmp.PState = (EAD_STATE2.SelectedIndex == 0 ? "" : EAD_STATE2.SelectedValue.ToString());
                objEmp.PCOUNTRY = (EAD_COUNTRY2.Text.Trim());
                objEmp.PPhone1 = (EAD_Phone3.Text.Trim());
                objEmp.PPhone2 = (EAD_Phone4.Text.Trim());
            }
            objEmp.confirmDate = (Confdt1.Text.Trim() == "" ? "" : DateTime.ParseExact(Confdt1.Text.Trim(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy"));
            objEmp.joiningDate = (joindt.Text.Trim() == "" ? "" : DateTime.ParseExact(joindt.Text.Trim(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy"));
            objEmp.retirementDate = (Retdt1.Text.Trim() == "" ? "" : DateTime.ParseExact(Retdt1.Text.Trim(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy"));
            objEmp.RetirementReasonId = (ddlreason.SelectedIndex == 0 ? "" : ddlreason.SelectedValue.Trim());
            objEmp.companyId = (ddlcompany.SelectedValue.ToString());
            objEmp.LocationId = (ddllocation.SelectedValue.ToString());
            objEmp.DivisionId = (ddldivision.SelectedValue.ToString());
            objEmp.DepartmentId = (ddldepartment.SelectedValue.ToString());
            objEmp.designationId = (ddldesignation.SelectedValue.ToString());
            objEmp.CategoryId = (ddlcategory.SelectedValue.ToString());
            objEmp.GroupID = (ddlgroup.SelectedValue.ToString());
            objEmp.GradeId = (ddlgrade.SelectedValue.ToString());
            objEmp.EodStaus = (ddlstatus.SelectedValue.ToString());
            objEmp.Password = (Encryption.EncryptDecrypt.Encrypt(DateTime.ParseExact(DOB.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy").Replace("/", ""), true));
            objEmp.IsESSEnabled = (chkEssEnable.Checked == true ? true : false);
            objEmp.MgrId = (ddlManager.SelectedIndex == 0 ? "" : ddlManager.SelectedValue.ToString());
            clsEmployeeHandler.InsertUpdateEmployeeDetails(objEmp, strCommand, "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
            ErrMsg = strErrMsg;
            strSucMsg = strSuccMsg;

        }
        public void cleartextbox()
        {
            EPD_EMPLOYEEID.Focus();
            EPD_EMPLOYEEID.Text = "";
            EPD_CARD_NO.Text = "";
            EPD_CARD_NO.Text = "";
            EPD_FIRST_NAME.Text = "";
            EPD_LAST_NAME.Text = "";
            EPD_MIDDLE_NAME.Text = "";
            EPD_PREVIOUS_CODE.Text = "";
            EPD_TEMP_CARD_NO.Text = "";
            EPD_LAST_NAME.Text = "";
            EPD_GENDER.SelectedIndex = 0;
            DOB.Text = "";
            EPD_NICKNAME.Text = "";
            EPD_MARITAL_STATUS.SelectedValue = "Select";
            EPD_SALUTATION.Text = "";
            EPD_REFERENCE_ONE.Text = "";
            EPD_REFERENCE_TWO.Text = "";
            EPD_RELIGION.SelectedValue = "Select";
            EPD_EMAIL.Text = "";
            EPD_DOMICILE.Text = "";
            EPD_PAN.Text = "";
            txtAadhar.Text = "";
            EPD_BLOODGROUP.Text = "";
            EPD_DOCTOR.Text = "";
            EAD_ADDRESS1.Text = "";
            EAD_CITY1.Text = "";
            EAD_PIN1.Text = "";
            EAD_STATE1.SelectedValue = "Select";
            EAD_Country1.Text = "";
            EAD_Phone1.Text = "";
            EAD_Phone2.Text = "";
            EAD_ADDRESS2.Text = "";
            EAD_CITY2.Text = "";
            EAD_PIN2.Text = "";
            EAD_STATE2.SelectedValue = "Select";
            EAD_COUNTRY2.Text = "";
            EAD_Phone3.Text = "";
            EAD_Phone4.Text = "";
            joindt.Text = "";
            Confdt1.Text = "";
            Retdt1.Text = "";
            ddlreason.SelectedValue = "Select";
            ddlcompany.SelectedValue = "Select";
            ddllocation.SelectedValue = "Select";
            ddldivision.SelectedValue = "Select";
            ddldesignation.SelectedValue = "Select";
            ddldepartment.SelectedValue = "Select";
            ddlgroup.SelectedValue = "Select";
            ddlcategory.SelectedValue = "Select";
            ddlgrade.SelectedValue = "Select";
            ddlstatus.SelectedValue = "Select";
            ddlManager.SelectedValue = "Select";
            ChkAddress.Enabled = true;
            ChkAddress.Checked = false;
            EPD_EMPLOYEEID.Enabled = true;
            string default_preview = default_path + "Default_Preview.jpg";
            DisplayEmployeeImage(default_preview);

        }
        public void delete()
        {
            clsEmployee objEMP = new clsEmployee();
            StringBuilder strXML = new StringBuilder();
            try
            {
                strXML.Append("<Employees>");
                for (int i = 0; i < gvEmployee.Rows.Count; i++)
                {

                    CheckBox chk = (CheckBox)gvEmployee.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked)
                    {
                        strXML.Append("<Emp>");
                        strXML.Append("<EMPID>" + gvEmployee.Rows[i].Cells[2].Text + "</EMPID>");
                        strXML.Append("</Emp>");


                    }
                }
                strXML.Append("</Employees>");
                objEMP.CreatedBy = Session["uid"].ToString();
                clsEmployeeHandler.InsertUpdateEmployeeDetails(objEMP, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strSuccMsg.Trim().Length >= 1)
                {
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    bindDataGrid();
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }
        protected void btnImage_Click(object sender, EventArgs e)
        {

            RetrieveEmployeeImage();
            //mpeAddEmployee.Show();

        }
        private bool RetrieveEmployeeImage()
        {

            if (FileUploadImages.HasFile)
            {

                if (FileUploadImages.PostedFile.FileName != "")
                {
                    try
                    {
                        string path = FileUploadImages.PostedFile.FileName;
                        string Filename = FileUploadImages.FileName;
                        string strImageExtension = "";

                        bool isValidExtension = false;

                        strImageExtension = Filename.Substring(Filename.LastIndexOf("."), (Filename.Length - Filename.LastIndexOf(".")));

                        string[] Imageformat = { ".jpg", ".jpeg", ".bmp", ".png" };



                        for (int j = 0; j <= Imageformat.Length - 1; j++)
                        {
                            if (strImageExtension == Imageformat[j].ToString())
                            {
                                isValidExtension = true;
                            }
                        }

                        if (!isValidExtension)
                        {
                            this.lblSaveMessages.Text = "Please Select Proper Image format i.e JPG,PNG,BMP";
                            lblSaveMessages.Visible = true;

                            mpeAddEmployee.Show();
                            return false;

                        }



                        if (EPD_EMPLOYEEID.Text != "")
                        {
                            Filename = EPD_EMPLOYEEID.Text + strImageExtension;
                        }
                        imgEmployeeImage.ImageUrl = FileUploadImages.PostedFile.FileName;
                        FileUploadImages.SaveAs(Server.MapPath("EmpImage/" + Filename));

                        ImageUrl = Server.MapPath("EmpImage/" + Filename).ToString();
                        //imgEmployeeImage.ImageUrl = "~/Handler1.ashx";

                        if (ViewState["AppMode"].ToString() != "EDIT")
                        {
                            Session["ServerImagePath"] = ImageUrl;

                        }
                        else
                        {
                            Session["ServerImagePath_U"] = ImageUrl;
                        }
                        //imgEmployeeImage.ImageUrl = "~/Handler1.ashx?ImagePath=" + ImageUrl;
                        DisplayEmployeeImage(ImageUrl);


                    }
                    catch (Exception ex)
                    {

                        UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
                    }
                }
                else
                {
                    this.lblMessages.Text = "Please Select Proper Image format i.e JPG,PNG,BMP";
                }
            }

            return true;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = string.Empty;
                lblSaveMessages.Text = string.Empty;
                clsEmployee objEmp = new clsEmployee();
                DataTable dt = clsEmployeeHandler.GetEmployeeManagerDetails(ddlEmpStatus.SelectedValue.ToString(), "AllEmployees","");

                if (txtCompanyName.Text.ToString() == "" && txtCompanyID.Text.ToString() == "")
                {
                    cmdReset_Click(sender, e);
                }
                else
                {
                    String[,] values = { 
                {"EPD_empid~" +txtCompanyID.Text.Trim(), "S" },
                {"EPD_Name~" +txtCompanyName.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEmployee.DataSource = _tempDT;
                    gvEmployee.DataBind();
                    _tempDT = _sc.searchTable(values, dt);
                    gvEmployee.DataSource = _tempDT;
                    gvEmployee.DataBind();

                    DropDownList ddl = (DropDownList)gvEmployee.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvEmployee.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvEmployee.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvEmployee.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvEmployee.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvEmployee.PageCount == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex + 1 == gvEmployee.PageCount)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                   

                    ((Label)gvEmployee.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployee.PageSize * gvEmployee.PageIndex) + 1) + " to " + (((gvEmployee.PageSize * (gvEmployee.PageIndex + 1)) - 10) + gvEmployee.Rows.Count);

                    gvEmployee.BottomPagerRow.Visible = true;
                }
            }


            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            cleartextbox();
            lblMessages.Text = "";
            lblSaveMessages.Text = "";
            mpeAddEmployee.Show();
            EPD_EMPLOYEEID.Focus();
        }
        protected void empda_Click(object sender, EventArgs e)
        {

        }
        protected void imgCloseBtn_Click(object sender, ImageClickEventArgs e)
        {
            mpeAddEmployee.Hide();
        }
        private void BindManager()
        {
            try
            {
                
                clsEmployee objEmp = new clsEmployee();
                DataTable dt = clsEmployeeHandler.GetEmployeeManagerDetails(ddlEmpStatus.SelectedValue.ToString(), "Manager","");

                ddlManager.DataValueField = "ID";
                ddlManager.DataTextField = "NAME";
                ddlManager.DataSource = dt;
                ddlManager.DataBind();
                ddlManager.Items.Insert(0, "Select");
                ddlManager.SelectedIndex = 0;
              
            }

            catch (Exception ex)
            {               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        protected void Btnclear_Click(object sender, EventArgs e)
        {
            cleartextbox();
            mpeAddEmployee.Hide();
        }
        protected void ddlEmpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCompanyName.Text = string.Empty;
            txtCompanyID.Text = string.Empty;
            txtCompanyName.Text = "";
            txtCompanyID.Text = "";
            lblSaveMessages.Text = "";
            bindDataGrid();
        }
        private void chkESSStatus()
        {

            DataTable dt = clsCommonHandler.GetEntParameterValues("ESS_ENABLE_FLAG", "ESS");
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   if (dt.Rows[0]["value"].ToString() == "1")
                   {
                       tdESSchkBox.Visible = true;
                       tdESSLabel.Visible = true;
                   }
                   else
                   {
                       tdESSchkBox.Visible = false;
                       tdESSLabel.Visible = false;
                   }
               }
               else
               {
                   tdESSchkBox.Visible = false;
                   tdESSLabel.Visible = false;
               }
           }

        }


    }
}