using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Text;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace UNO
{
    public partial class SACPersoDataCapture : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string default_path = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindddlReason();
                default_path = Server.MapPath(@"~/EmpImage/");
                BindgvEmpDetails();
                FillCompanydrp();
                Filllocationdrp();
                Filldivisiondrp();
                Filldepartmentdrp();
                Filldesignationdrp();
                FillCategorydrp();
                FillGroupdrp();
                FillGraddrp();
                FillEmpStatus();
                FillReason();
                FillStates();
                FillReligion();
                FillStatus();
                BindManager();
            }
            RegisterPostBackControl();
            hdConn.Value= ConfigurationManager.ConnectionStrings["connection_string"].ToString().Replace(' ', ',');
            grid_columns_show();
        }

        public void grid_columns_show()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd_gv = new SqlCommand("Select value from ENT_PARAMS WHERE IDENTIFIER='CardPrintingModules' AND MODULE='Perso'", conn);
                cmd_gv.CommandType = CommandType.Text;
                cmd_gv.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd_gv);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["value"].ToString() == "Photo")
                    {
                        gvEmpDetails.Columns[2].Visible = true;
                    }

                    if (dt.Rows[i]["value"].ToString() == "Signature")
                    {
                        gvEmpDetails.Columns[3].Visible = true;
                    }

                    if (dt.Rows[i]["value"].ToString() == "Finger")
                    {
                        gvEmpDetails.Columns[4].Visible = true;
                    }

                    if (dt.Rows[i]["value"].ToString() == "Print_Card")
                    {
                        gvEmpDetails.Columns[5].Visible = true;
                    }

                    if (dt.Rows[i]["value"].ToString() == "Personalize_Card")
                    {
                        gvEmpDetails.Columns[6].Visible = true;
                    }
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                //throw ex;

            }
        }

        private void BindddlReason()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from ENT_REASON where Reason_Type = 'LC' and Reason_IsDeleted = 0", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count > 0)
                {
                    ddlReason.DataSource = dt;
                    ddlReason.DataTextField = "Reason_Description";
                    ddlReason.DataValueField = "Reason_Description";
                    ddlReason.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void BindgvEmpDetails()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("getEmployeePersoDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("empType", SqlDbType.VarChar).Value = ddlEmployeeType.SelectedValue;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (txtEmpID.Text.ToString() == "" && txtEmpName.Text.ToString() == "")
                {
                    gvEmpDetails.DataSource = dt;
                    gvEmpDetails.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"EPD_EMPID~" +txtEmpID.Text.Trim(), "S" },
				{"Emp_Name~" +txtEmpName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEmpDetails.DataSource = _tempDT;
                    gvEmpDetails.DataBind();
                }
                if (dt.Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)gvEmpDetails.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvEmpDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvEmpDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvEmpDetails.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvEmpDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvEmpDetails.PageCount == 0)
                    {
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmpDetails.PageIndex + 1 == gvEmpDetails.PageCount)
                    {
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmpDetails.PageIndex == 0)
                    {
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvEmpDetails.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpDetails.PageSize * gvEmpDetails.PageIndex) + 1) + " to " + (gvEmpDetails.PageSize * (gvEmpDetails.PageIndex + 1));

                    gvEmpDetails.BottomPagerRow.Visible = true;
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersoDataCapture");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmpDetails.PageIndex = gvEmpDetails.PageIndex - 1;
                BindgvEmpDetails();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersoDataCapture");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmpDetails.PageIndex = gvEmpDetails.PageIndex + 1;
                BindgvEmpDetails();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersoDataCapture");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmpDetails.PageIndex = Convert.ToInt32(((DropDownList)gvEmpDetails.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindgvEmpDetails();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SACPersoDataCapture");
            }
        }

        protected void EmployeeType_OnSelectIndexChange(object sender, EventArgs e)
        {
            BindgvEmpDetails();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetAll();

            BindgvEmpDetails();
        }


        private void resetAll()
        {
            txtEmpName.Text = string.Empty;

            txtEmpID.Text = string.Empty;

            ddlEmployeeType.SelectedIndex = 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindgvEmpDetails();
        }

        protected void gvEmpDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    //string employeeid = e.CommandArgument.ToString();
                    string employeeid = e.CommandArgument.ToString();
                    Modify_Data(employeeid);
                    mpeAddEmployee.Show();

                }

                BindgvEmpDetails();
            }
            catch (Exception ex)
            {

            }
        }
        protected void Modify_Data(string Employeeid)
        {
            string ead_addtype = "";

            string EmpDob;
            string EmpJndt;
            string Confrmdt;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strsql = "Select * from ENT_EMPLOYEE_PERSONAL_DTLS  with(nolock) where EPD_EMPID= '" + Employeeid + "' and isnull(EPD_ISDELETED,0) = 0";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string strsql1 = "Select * from ENT_EMPLOYEE_OFFICIAL_DTLS  with(nolock) where EOD_EMPID = '" + Employeeid + "' AND isnull(EOD_ISDELETED,0) = 0";
                SqlDataAdapter da1 = new SqlDataAdapter(strsql1, conn);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                string strsql2 = "Select * from ENT_EMPLOYEE_ADDRESS  with(nolock) where EAD_EMPID = '" + Employeeid + "' AND isnull(EAD_ISDELETED,0) = 0";
                SqlDataAdapter da2 = new SqlDataAdapter(strsql2, conn);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                string strGetMgr = "SELECT Hier_Mgr_ID As ID FROM dbo.ENT_HierarchyDef  with(nolock) WHERE  isnull(Hier_IsDeleted,0)=0 and Hier_Emp_ID ='" + Employeeid + "' order by RowID desc";
                SqlDataAdapter da3 = new SqlDataAdapter(strGetMgr, conn);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                string strTempCardNo = "  select  TC_TMP_CARD_ID as 'TempId'  from TA_TEMPCARD with(nolock)   where    TC_EMPLOYEE_ID ='" + Employeeid + "' and   isnull(tc_isdeleted,0)=0 and (getdate() between TC_ISSUEDT and TC_RETURNDT)";
                SqlDataAdapter da4 = new SqlDataAdapter(strTempCardNo, conn);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (dt4.Rows.Count > 0)
                {
                    // EPD_TEMP_CARD_NO.Text = dt4.Rows[0]["TempId"].ToString();
                }

                // this.EPD_EMPLOYEEID.Enabled =false;

                string gender = dt.Rows[0]["EPD_GENDER"].ToString();
                // string default_path = "D:\\Project\\UNO_23sept2014(02)\\UNO\\EmpImage\\";
                if (dt.Rows[0]["EPD_PHOTOURL"].ToString() != "")
                {
                    Session["ServerImagePath_U"] = dt.Rows[0]["EPD_PHOTOURL"].ToString();
                    //DisplayEmployeeImage(dt.Rows[0]["EPD_PHOTOURL"].ToString());
                    // EPD_MARITAL_STATUS.SelectedValue = dt.Rows[0][10].ToString();
                }
                else if (gender == "F")
                {
                    string default_male = default_path + "Default_Preview.jpg";

                    // DisplayEmployeeImage(default_male);

                }
                else if (gender == "M")
                {
                    string default_female = default_path + "Default_Preview.jpg";

                    //  DisplayEmployeeImage(default_female);
                }



                EPD_EMPLOYEEID.Text = dt.Rows[0]["EPD_EMPID"].ToString();
                EPD_CARD_NO.Text = dt.Rows[0]["EPD_CARD_ID"].ToString();
                EPD_SALUTATION.Text = dt.Rows[0]["EPD_SALUTATION"].ToString();
                EPD_FIRST_NAME.Text = dt.Rows[0]["EPD_FIRST_NAME"].ToString();
                //   EPD_TEMP_CARD_NO.Text = dt.Rows[0]["EPD_TEMP_CARD_ID"].ToString();
                //EPD_MIDDLE_NAME.Text = dt.Rows[0]["EPD_MIDDLE_NAME"].ToString();
                //EPD_PREVIOUS_CODE.Text = dt.Rows[0]["EPD_PREVIOUS_ID"].ToString();
                // EPD_LAST_NAME.Text = dt.Rows[0]["EPD_LAST_NAME"].ToString();
                EPD_GENDER.SelectedValue = dt.Rows[0]["EPD_GENDER"].ToString();
                lblGender.Text = EPD_GENDER.SelectedItem.Text;



                //EPD_TEMP_CARD_NO.Text = dt.Rows[0]["EPD_TEMP_CARD_ID"].ToString();
                // EPD_MIDDLE_NAME.Text = dt.Rows[0]["EPD_MIDDLE_NAME"].ToString();
                //EPD_PREVIOUS_CODE.Text = dt.Rows[0]["EPD_PREVIOUS_ID"].ToString();
                //EPD_LAST_NAME.Text = dt.Rows[0]["EPD_LAST_NAME"].ToString();
                EPD_GENDER.SelectedValue = dt.Rows[0]["EPD_GENDER"].ToString();

                EmpDob = Convert.ToDateTime(dt.Rows[0]["EPD_DOB"]).ToString("dd/MM/yyyy");
                //EmpDob2 = DateTime.ParseExact(EmpDob, "MM/dd/yyyy", null);

                DOB.Text = EmpDob;

                //EPD_MARITAL_STATUS.SelectedValue = dt.Rows[0][10].ToString();
                if (dt.Rows[0]["EPD_MARITAL_STATUS"].ToString() == "")
                {
                    EPD_MARITAL_STATUS.SelectedIndex = 0;
                    lblMartialStatus.Text = "";
                }
                else
                {
                    EPD_MARITAL_STATUS.SelectedValue = dt.Rows[0]["EPD_MARITAL_STATUS"].ToString();
                    lblMartialStatus.Text = EPD_MARITAL_STATUS.SelectedItem.Text;
                }

                //EPD_NICKNAME.Text = dt.Rows[0]["EPD_NICKNAME"].ToString();

                if (dt.Rows[0]["EPD_RELIGION"].ToString() == "")
                {
                    EPD_RELIGION.SelectedIndex = 0;
                    lblReligion.Text = "";
                }
                else
                {
                    EPD_RELIGION.SelectedValue = dt.Rows[0]["EPD_RELIGION"].ToString();
                    lblReligion.Text = EPD_RELIGION.SelectedItem.Text;
                }

                //EPD_RELIGION.SelectedValue = dt.Rows[0][12].ToString();
                //EPD_REFERENCE_ONE.Text = dt.Rows[0]["EPD_REFERENCE_ONE"].ToString();
                //EPD_REFERENCE_TWO.Text = dt.Rows[0]["EPD_REFERENCE_TWO"].ToString();
                // EPD_NICKNAME.Text = dt.Rows[0]["EPD_NICKNAME"].ToString();
                EPD_DOMICILE.Text = dt.Rows[0]["EPD_DOMICILE"].ToString();
                EPD_BLOODGROUP.Text = dt.Rows[0]["EPD_BLOODGROUP"].ToString();
                EPD_EMAIL.Text = dt.Rows[0]["EPD_EMAIL"].ToString();
                EPD_PAN.Text = dt.Rows[0]["EPD_PAN"].ToString();
                EPD_DOCTOR.Text = dt.Rows[0]["EPD_DOCTOR"].ToString();
                txtAadhar.Text = dt.Rows[0]["EPD_AADHARCARD"].ToString();




                EmpJndt = Convert.ToDateTime(dt1.Rows[0]["EOD_JOINING_DATE"]).ToString("dd/MM/yyyy");
                //EmpDob2 = DateTime.ParseExact(EmpDob, "MM/dd/yyyy", null);

                joindt.Text = EmpJndt;

                Confrmdt = Convert.ToDateTime(dt1.Rows[0]["EOD_CONFIRM_DATE"]).ToString("dd/MM/yyyy");
                if (Confrmdt == "01/01/1900" || Confrmdt == "")
                {
                    Confdt1.Text = "";
                }
                else
                {
                    Confdt1.Text = Confrmdt;
                }
                if (dt1.Rows[0]["EOD_RETIREMENT_DATE"].ToString() != "")
                {
                    //  Retdt1.Text = Convert.ToDateTime(dt1.Rows[0]["EOD_RETIREMENT_DATE"]).ToString("dd/MM/yyyy");
                }
                ////Added by Pooja Yadav
                //if (ddlreason.Items.FindByValue(dt1.Rows[0]["EOD_RETIREMENT_REASON_ID"].ToString()) == null)
                //{
                //    ddlreason.SelectedIndex = 0;
                //}
                //else
                //{
                //    ddlreason.SelectedValue = dt1.Rows[0]["EOD_RETIREMENT_REASON_ID"].ToString();
                //}




                ddlcompany.SelectedValue = dt1.Rows[0]["EOD_COMPANY_ID"].ToString();
                if (ddlcompany.SelectedIndex != 0)
                    lblcompany.Text = ddlcompany.SelectedItem.Text;

                ddllocation.SelectedValue = dt1.Rows[0]["EOD_LOCATION_ID"].ToString();
                if (ddllocation.SelectedIndex != 0)
                    lbllocation.Text = ddllocation.SelectedItem.Text;


                ddldivision.SelectedValue = dt1.Rows[0]["EOD_DIVISION_ID"].ToString();
                if (ddldivision.SelectedIndex != 0)
                    lbldivision.Text = ddldivision.SelectedItem.Text;

                ddldepartment.SelectedValue = dt1.Rows[0]["EOD_DEPARTMENT_ID"].ToString();
                if (ddldepartment.SelectedIndex != 0)
                    lbldepartment.Text = ddldepartment.SelectedItem.Text;


                ddldesignation.SelectedValue = dt1.Rows[0]["EOD_DESIGNATION_ID"].ToString();
                if (ddldesignation.SelectedIndex != 0)
                    lbldesignation.Text = ddldesignation.SelectedItem.Text;

                ddlcategory.SelectedValue = dt1.Rows[0]["EOD_CATEGORY_ID"].ToString();
                if (ddlcategory.SelectedIndex != 0)
                    lblCategory.Text = ddlcategory.SelectedItem.Text;

                ddlgroup.SelectedValue = dt1.Rows[0]["EOD_GROUP_ID"].ToString();
                if (ddlgroup.SelectedIndex != 0)
                    lblgroup.Text = ddlgroup.SelectedItem.Text;

                ddlgrade.SelectedValue = dt1.Rows[0]["EOD_GRADE_ID"].ToString();
                if (ddlgrade.SelectedIndex != 0)
                    lblgrade.Text = ddlgrade.SelectedItem.Text;


                ddlstatus.SelectedValue = dt1.Rows[0]["EOD_STATUS"].ToString();
                if (ddlstatus.SelectedIndex != 0)
                    lblstatus.Text = ddlstatus.SelectedItem.Text;

                Rbtnchecked.SelectedValue = dt1.Rows[0]["EOD_ACTIVE"].ToString();
                //vaibhav
                // ddlManager.SelectedValue = dt3.Rows[0]["ID"].ToString();
                //lblManager.Text = ddlManager.SelectedItem.Text;

                if (dt1.Rows[0]["EOD_ACTIVE"] != "")
                {
                    //chkEssEnable.Checked = true;
                }
                else
                {
                    //chkEssEnable.Checked = false;
                }
                //EAD_PIN1.Text = dt2.Rows[0]["EAD_PIN"].ToString();



            }


            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");

            }
        }
        protected void Btnclear_Click(object sender, EventArgs e)
        {
            mpeAddEmployee.Hide();
        }

        

        protected void gvEmpDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string EmpCode = ((LinkButton)e.Row.FindControl("lnkEmp")).Text;
                    string EmpName = ((Label)e.Row.FindControl("lblName")).Text;

                    Label lblImage = (Label)e.Row.FindControl("lblImage");
                    Label lblSign = (Label)e.Row.FindControl("lblSign");
                    Label lblBio1 = (Label)e.Row.FindControl("lblBio1");
                    Label lblBio2 = (Label)e.Row.FindControl("lblBio2");
                    Label lblName = (Label)e.Row.FindControl("lblName");

                    LinkButton lnkImageCapture = (LinkButton)e.Row.FindControl("lnkImageCapture");
                    lnkImageCapture.Attributes.Add("onclick", string.Format("AddImage('{0}')", EmpCode));

                    LinkButton lnkImageEdit = (LinkButton)e.Row.FindControl("lnkImageEdit");
                    lnkImageEdit.Attributes.Add("onclick", string.Format("AddImage('{0}')", EmpCode));

                    LinkButton lnkSignCapture = (LinkButton)e.Row.FindControl("lnkSignCapture");
                    lnkSignCapture.Attributes.Add("onclick", string.Format("Sign('{0}')", EmpCode));

                    LinkButton lnkSignEdit = (LinkButton)e.Row.FindControl("lnkSignEdit");
                    lnkSignEdit.Attributes.Add("onclick", string.Format("Sign('{0}')", EmpCode));

                    LinkButton lnkBioCapture = (LinkButton)e.Row.FindControl("lnkBioCapture");
                    lnkBioCapture.Attributes.Add("onclick", string.Format("Bio('{0}')", EmpCode));

                    LinkButton lnkBioEdit = (LinkButton)e.Row.FindControl("lnkBioEdit");
                    lnkBioEdit.Attributes.Add("onclick", string.Format("Bio('{0}')", EmpCode));

                  //  LinkButton lnkWriteOnCard = (LinkButton)e.Row.FindControl("lnkWriteOnCard");
                  //  lnkWriteOnCard.Attributes.Add("onclick", string.Format("WriteTemplateOnCard('{0}', '{1}')", EmpCode, EmpName));
                    LinkButton lnkPerso = (LinkButton)e.Row.FindControl("lnkPerso");

                    LinkButton lnkCardPrinting = (LinkButton)e.Row.FindControl("lnkCardPrinting");
                
                    lnkCardPrinting.Attributes.Add("onclick", string.Format("Print('{0}', '{1}')", EmpCode, EmpName));

                    if (lblImage.Text == "")
                    {
                        lnkImageCapture.Visible = true;
                        lnkImageEdit.Visible = false;
                    }
                    else
                    {
                        lnkImageCapture.Visible = false;
                        lnkImageEdit.Visible = true;
                    }

                    if (lblSign.Text == "")
                    {
                        lnkSignCapture.Visible = true;
                        lnkSignEdit.Visible = false;
                    }
                    else
                    {
                        lnkSignCapture.Visible = false;
                        lnkSignEdit.Visible = true;
                    }

                    if (lblBio1.Text == "" && lblBio2.Text == "")
                    {
                        lnkBioCapture.Visible = true;
                        lnkBioEdit.Visible = false;
                        //lnkWriteOnCard.Visible = false;
                    }
                    else
                    {
                        lnkBioCapture.Visible = false;
                        lnkBioEdit.Visible = true;
                       // lnkWriteOnCard.Visible = true;
                    }

                    string flag = CheckLostCard(EmpCode);
                    if (lblImage.Text != "" && lblSign.Text != "" && lblBio1.Text != "" && lblBio2.Text != "")
                    {
                        if (flag == "True")
                        {
                            lnkPerso.Enabled = true;
                            lnkPerso.ForeColor = System.Drawing.Color.Red;
                            lnkPerso.Text = "Personalize Card";
                        }
                        else
                        {
                            lnkPerso.Enabled = true;
                            lnkPerso.ForeColor = System.Drawing.Color.Green;
                            lnkPerso.Text = "Personalize Card";
                        }

                        //added by vaibhav on 3-19-2015
                        lnkCardPrinting.ForeColor = System.Drawing.Color.Green;
                        lnkCardPrinting.Enabled = true;

                        lnkPerso.Attributes.Add("onclick", string.Format("return Personalisation('{0}');", EmpCode));


                    }
                    else
                    {
                        if (flag == "True")
                        {
                            lnkPerso.Enabled = true;
                            lnkPerso.ForeColor = System.Drawing.Color.Red;
                            lnkPerso.Text = "Personalize Card";
                        }
                        else
                        {
                            lnkPerso.Enabled = true;
                            lnkPerso.ForeColor = System.Drawing.Color.Green;
                            lnkPerso.Text = "Personalize Card";
                        }

                       
                        //added by vaibhav on 3-19-2015
                        lnkCardPrinting.ForeColor = System.Drawing.Color.Red;
                        lnkCardPrinting.Enabled = false;
                        lnkPerso.Attributes.Add("onclick", string.Format("return Personalisation('{0}');", EmpCode));
                    }
                    
                 
                }
                //BindgvEmpDetails();
            }
            catch (Exception ex)
            {
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
        public static string GetISOTemplate(string EmpCode)
        {
            string json = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("spGetISOTemplate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EmpCode;
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

                dt.Columns.Add("EmployeeCode", typeof(string));
                dt.Columns.Add("FingerQualityAvg", typeof(string));
                dt.Columns.Add("Template1FingerNo", typeof(string));
                dt.Columns.Add("Template1Quality", typeof(string));
                dt.Columns.Add("Template1", typeof(string));
                dt.Columns.Add("Template2FingerNo", typeof(string));
                dt.Columns.Add("Template2Quality", typeof(string));
                dt.Columns.Add("Template2", typeof(string));


                //dt.Columns.Add("ISOTemplate1", typeof(string));
                //dt.Columns.Add("ISOTemplate2", typeof(string));
                //dt.Columns.Add("ActivationDate", typeof(int));
                //dt.Columns.Add("ExpiryDate", typeof(int));
                //dt.Columns.Add("AadharNo", typeof(string));
                //dt.Columns.Add("CenterCode", typeof(string));
                //dt.Columns.Add("LocationCode", typeof(string));
                DataRow dr = dt.NewRow();
                dr["EmployeeCode"] = ds.Tables[0].Rows[0]["EmployeeCode"];
                dr["FingerQualityAvg"] = ds.Tables[0].Rows[0]["FingerQualityAvg"];
                dr["Template1FingerNo"] = ds.Tables[0].Rows[0]["Template1FingerNo"];
                dr["Template1Quality"] = ds.Tables[0].Rows[0]["Template1Quality"];
                dr["Template1"] = ByteArrayToString(Convert.FromBase64String(ds.Tables[0].Rows[0]["Template1"].ToString()));
                //dr["Template1"] = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["Template1"]);
                dr["Template2FingerNo"] = ds.Tables[0].Rows[0]["Template2FingerNo"];
                dr["Template2Quality"] = ds.Tables[0].Rows[0]["Template2Quality"];
                dr["Template2"] = ByteArrayToString(Convert.FromBase64String(ds.Tables[0].Rows[0]["Template2"].ToString()));
                //dr["Template2"] = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["Template2"]);
                dt.Rows.Add(dr);
                ds1.Tables.Add(dt);
                ds1.AcceptChanges();
                json = ds1.GetXml();
            }
            return json;
        }
        [WebMethod]
        public static string CheckLostCard(string EmployeeCode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EPD_EMPID = '" + EmployeeCode + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows[0]["EPD_PERSO_FLAG"].ToString() != "P")
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                return "False";
            }
        }

        [WebMethod]
        public static string SaveLostCard(string EmpCode, string Reason, string AdditionalInfo, string LostDate)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spSaveLostCardReason", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EmpCode;
                cmd.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = HttpContext.Current.Session["uid"].ToString(); ;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = Reason;
                cmd.Parameters.Add("@AdditionalInfo", SqlDbType.VarChar).Value = AdditionalInfo;
                cmd.Parameters.Add("@LostDate", SqlDbType.VarChar).Value = LostDate;
                SqlParameter LogId = new SqlParameter("@LostCardLogID", SqlDbType.BigInt);
                LogId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(LogId);
                cmd.ExecuteNonQuery();
                string ID = cmd.Parameters["@LostCardLogID"].Value.ToString();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return ID;
            }
            catch (Exception ex)
            {
                return "False";
            }
        }

        [WebMethod]
        public static string GetEmployeeDetails(string EmployeeCode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
              
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strsql = " SELECT EPD_FIRST_NAME + ' ' + EPD_LAST_NAME AS EMP_NAME,convert(varchar(10), EPD_DOB, 103) as  EPD_DOB,EPD_GENDER,EPD_CARD_ID, " +
                            " (CASE WHEN EPD_NUMCARDS is NULL THEN 0 ELSE EPD_NUMCARDS END) as Card_Num,EPD_PERDATE, convert(varchar(10), EPD_CARD_EXPIRY_DATE, 103) as EPD_CARD_EXPIRY_DATE " +
                            " FROM ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID = '" + EmployeeCode.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (dt.Rows.Count > 0)
                {
                  //  string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + ' ' + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                    //DateTime DOB = DateTime.ParseExact(dt.Rows[0]["EPD_DOB"].ToString(), "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));

                    strsql = "select VALUE from ENT_PARAMS WHERE MODULE = 'CM' AND CODE = 'ED'";
                    SqlDataAdapter da1 = new SqlDataAdapter(strsql, conn);
                    DataTable dt2 = new DataTable();
                    da1.Fill(dt2);

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("EmpCode", typeof(string));
                    dt1.Columns.Add("EmpName", typeof(string));
                    dt1.Columns.Add("DOB", typeof(string));
                    dt1.Columns.Add("Gender", typeof(string));
                    dt1.Columns.Add("ExpiryDate", typeof(string));
                    dt1.Columns.Add("ExpiryTime", typeof(string));
                    DataRow dr = dt1.NewRow();
                    dr["EmpCode"] = EmployeeCode;
                    dr["EmpName"] = dt.Rows[0]["EMP_NAME"].ToString();
                   // dr["DOB"] = DOB.ToString("dd/MM/yyyy");
                    dr["DOB"]=dt.Rows[0]["EPD_DOB"].ToString();
                    dr["Gender"] = dt.Rows[0]["EPD_GENDER"].ToString();
                    dr["ExpiryDate"] = dt.Rows[0]["EPD_CARD_EXPIRY_DATE"].ToString() != "" ? dt.Rows[0]["EPD_CARD_EXPIRY_DATE"].ToString() : dt2.Rows[0][0].ToString();
                    dr["ExpiryTime"] = "23:59";
                    dt1.Rows.Add(dr);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt1);
                    ds.AcceptChanges();
                    return ds.GetXml();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }


        }

        [WebMethod]
        public static string GetMasterKey()
        {
            try
            {
                string MasterKey = HttpContext.Current.Session["MasterKey"].ToString();
                return MasterKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
           [WebMethod]
        public static string GetCompanyCode()
        {
            try
            {
                string CompanyCode = HttpContext.Current.Session["CompanyCode"].ToString();
               // return "0015";
                return CompanyCode;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        [WebMethod]
        public static string GetDOSKey()
        {
            try
            {
                string DosKey = HttpContext.Current.Session["DosKey"].ToString();
                return DosKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public static string CheckCSNR(string CSNR)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spValidateCSNR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count == 1)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }

        [WebMethod]
        public static string CompletePersonalisation(string EmpCode, string pin, string CSNR, string Password, string ExpiryDate, string LogId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spSACCardPersonalisationComplete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EmpCode;
                cmd.Parameters.Add("@CSNR ", SqlDbType.VarChar).Value = CSNR;
                cmd.Parameters.Add("@PIN ", SqlDbType.VarChar).Value = pin;
                cmd.Parameters.Add("@Session ", SqlDbType.VarChar).Value = HttpContext.Current.Session["uid"].ToString();
                cmd.Parameters.Add("@pwd ", SqlDbType.VarChar).Value = Encryption.EncryptDecrypt.Encrypt(Password.Trim(), true);
              //  cmd.Parameters.Add("@pwd ", SqlDbType.VarChar).Value = Password;
                cmd.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = ExpiryDate;
                cmd.Parameters.Add("@LogID", SqlDbType.VarChar).Value = LogId;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "True";
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }

        private void FillCompanydrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "SELECT COMPANY_ID,COMPANY_NAME AS COMPANYNAME FROM ENT_COMPANY where COMPANY_ISDELETED='0'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlcompany.DataValueField = "COMPANY_ID";
                ddlcompany.DataTextField = "COMPANYNAME";
                ddlcompany.DataSource = dt;

                ddlcompany.DataBind();


                ddlcompany.Items.Insert(0, "Select");
                //ddlcompany.SelectedValue = null;
                ddlcompany.SelectedIndex = 0;

            }

            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");

            }
        }
        private void Filllocationdrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "select OCE_ID,OCE_DESCRIPTION as LOCATIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='LOC'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddllocation.DataValueField = "oce_id";
                ddllocation.DataTextField = "LOCATIONNAME";
                ddllocation.DataSource = dt;
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }

        }
        private void Filldivisiondrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select OCE_ID,OCE_DESCRIPTION  as DIVISIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='DIV'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddldivision.DataValueField = "oce_id";
                ddldivision.DataTextField = "DIVISIONNAME";
                ddldivision.DataSource = dt;
                ddldivision.DataBind();
                ddldivision.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }

        }
        private void Filldepartmentdrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select OCE_ID,OCE_DESCRIPTION  as DEPARTMENTNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='DEP'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddldepartment.DataValueField = "oce_id";
                ddldepartment.DataTextField = "DEPARTMENTNAME";
                ddldepartment.DataSource = dt;
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");

            }
        }
        private void Filldesignationdrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select OCE_ID,OCE_DESCRIPTION  as DESIGNATIONNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='DES'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddldesignation.DataValueField = "oce_id";
                ddldesignation.DataTextField = "DESIGNATIONNAME";
                ddldesignation.DataSource = dt;
                ddldesignation.DataBind();
                ddldesignation.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }

        }
        private void FillCategorydrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select OCE_ID,OCE_DESCRIPTION  as CATEGORYNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='CAT'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlcategory.DataValueField = "OCE_ID";
                ddlcategory.DataTextField = "CATEGORYNAME";
                ddlcategory.DataSource = dt;
                ddlcategory.DataBind();
                ddlcategory.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillGroupdrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select OCE_ID,OCE_DESCRIPTION  as GROUPNAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='GRP'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlgroup.DataValueField = "oce_id";
                ddlgroup.DataTextField = "GROUPNAME";
                ddlgroup.DataSource = dt;
                ddlgroup.DataBind();
                ddlgroup.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillGraddrp()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select OCE_ID,OCE_DESCRIPTION  as GRADENAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='GRD'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlgrade.DataValueField = "oce_id";
                ddlgrade.DataTextField = "GRADENAME";
                ddlgrade.DataSource = dt;
                ddlgrade.DataBind();
                ddlgrade.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillEmpStatus()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "SELECT CODE,[VALUE] AS EMPSTATUS FROM ENT_PARAMS WHERE IDENTIFIER = 'EMPSTATUS'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlstatus.DataValueField = "CODE";
                ddlstatus.DataTextField = "EMPSTATUS";
                ddlstatus.DataSource = dt;
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillReason()
        {
            //try
            //{
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();
            //    }
            //    string strSql = "select Reason_ID,Reason_Description as Reason_Description from [ENT_Reason] where Reason_Type='el'  and isnull(reason_isdeleted,0)=0 ";
            //    SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    ddlreason.DataValueField = "Reason_Description";
            //    ddlreason.DataTextField = "Reason_ID";
            //    ddlreason.DataSource = dt;
            //    ddlreason.DataBind();
            //    ddlreason.Items.Insert(0, new ListItem("Select", "Select"));

            //}
            //catch (Exception ex)
            //{
            //    if (conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            //}
        }
        private void FillStates()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "SELECT CODE,[VALUE] AS STATENAME FROM ENT_PARAMS WHERE IDENTIFIER = 'STATES' order by Value";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //EAD_STATE1.DataValueField = "CODE";
                //EAD_STATE1.DataTextField = "STATENAME";
                //EAD_STATE1.DataSource = dt;
                //EAD_STATE1.DataBind();
                ////EAD_STATE1.SelectedIndex = 0;

                //EAD_STATE1.Items.Insert(0, "Select");

                //EAD_STATE2.DataValueField = "CODE";
                //EAD_STATE2.DataTextField = "STATENAME";
                //EAD_STATE2.DataSource = dt;
                //EAD_STATE2.DataBind();
                //EAD_STATE2.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }
        }
        private void FillStatus()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "SELECT CODE,[VALUE] AS MARITALSTATUS FROM ENT_PARAMS WHERE IDENTIFIER = 'MARITALSTATUS'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                EPD_MARITAL_STATUS.DataValueField = "CODE";
                EPD_MARITAL_STATUS.DataTextField = "MARITALSTATUS";
                EPD_MARITAL_STATUS.DataSource = dt;
                EPD_MARITAL_STATUS.DataBind();
                EPD_MARITAL_STATUS.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeMasterView");
            }


        }

        private void BindManager()
        {

        }

        private void FillReligion()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "SELECT CODE,[VALUE] AS RELIGION FROM ENT_PARAMS WHERE IDENTIFIER = 'RELIGION'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                EPD_RELIGION.DataValueField = "CODE";
                EPD_RELIGION.DataTextField = "RELIGION";
                EPD_RELIGION.DataSource = dt;
                EPD_RELIGION.DataBind();
                EPD_RELIGION.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }


        [WebMethod]
        public static string SendMail(string EmpCode, string pin, string CSNR, string Password)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                string strMailOption = "";
                string strMailServer = "";
                string strMailUserName = "";
                string strMailPassword = "";
                int strMailPort = 0;
                string UserMailId = "";
                String strSql = "select Identifier,value FROM ENT_PARAMS WHERE  MODULE='ESS'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataAdapter da1 = new SqlDataAdapter("USP_DashBoard @strCommand='getEmailId', @userId=" + EmpCode + "", conn);
                DataSet ds = new DataSet();
                da1.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    UserMailId = Convert.ToString(ds.Tables[0].Rows[0]["epd_email"]);
                    //UserMailId = "shraddha_parihar@cms.co.in";
                }
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["Identifier"].ToString() == "WITHMAIL")
                    {
                        strMailOption = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVER")
                    {
                        strMailServer = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERUSERNAME")
                    {
                        strMailUserName = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERPASSWORD")
                    {
                        strMailPassword = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILPORT")
                    {
                        strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "Dear Employee," + System.Environment.NewLine + "    The PIN to be used for authentication at the Access Control Readers is " + pin + "." +
                                  System.Environment.NewLine + "Your Login ID is " + EmpCode + " and Password is " + Password + "." +
                                  System.Environment.NewLine + "Kindly keep this mail safe for future reference." +
                                 System.Environment.NewLine + "Thanks," +
                                 System.Environment.NewLine + "UNO" +
                                 System.Environment.NewLine + "--This is an auto-generated email. Kindly donot reply to it.";
                objMailMessage.From = new MailAddress("uno@cms.co.in");
                objMailMessage.To.Add(UserMailId.Trim());
                objMailMessage.Subject = "Credentials for Authentication";
                objMailMessage.Body = message.Trim();
                objMailMessage.Priority = MailPriority.High;
                objSMTPCLIENT.Port = strMailPort;
                objSMTPCLIENT.Host = strMailServer;

                if (strMailUserName != "")
                {
                    CredentialCache.DefaultNetworkCredentials.UserName = strMailUserName;
                    CredentialCache.DefaultNetworkCredentials.Password = strMailPassword;

                    objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;

                }
                objSMTPCLIENT.Send(objMailMessage);
                return "True";
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }

       //vaibhav
        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in gvEmpDetails.Rows)
            {
                LinkButton lnkFull = row.FindControl("lnkImageCapture") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }
        }

        protected void FullPostBack(object sender, EventArgs e)
        {
            string fruitName = ((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text;
            string message = "alert('Full PostBack: You clicked " + fruitName + "');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", message, true);
        }



    }
}