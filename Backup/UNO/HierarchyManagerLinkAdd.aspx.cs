using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Sockets;
using CMS.UNO.Core.Handler;
namespace UNO
{
    public partial class HierarchyManagerLinkAdd : System.Web.UI.Page
    {
       
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uid"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                if (!Page.IsPostBack)
                {
                    RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdCompany.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('COM','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdLocation.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('LOC', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdDivision.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DIV', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdDepartment.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DEP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");


                    txtSearchEmp.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtSearchEmp.ClientID + "','" + LstEmployee.ClientID + "','" + lstEmployDummy.ClientID + "','"+LstEntitySelected.ClientID+"');");
                    txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtCompany.ClientID + "','" + LstCompany.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                    txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtLocation.ClientID + "','" + LstLocation.ClientID + "','" + LstLocationDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                    txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDivision.ClientID + "','" + LstDivision.ClientID + "','" + LstDivisionDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                    txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDepartment.ClientID + "','" + LstDepartment.ClientID + "','" + LstDepartmentDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                    

                    btnOK.Attributes.Add("onclick", "javascript:return okclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + btnSubmitAdd.ClientID + "');");
                    //Close
                    Button4.Attributes.Add("onclick", "javascript:return closeclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + btnSubmitAdd.ClientID + "');");

                    Button2.Attributes.Add("onclick", "javascript:return removeEntitySelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    cmdEntityAllRight.Attributes.Add("onclick", "javascript:return AllSelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    Button1.Attributes.Add("onclick", "javascript:return FillEntitySeletedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    Button3.Attributes.Add("onclick", "javascript:return ReturnFillEntityAvailable('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    FillManagerEntity();
                    //FillEmployeeEntity();
                    //FillCompanyEntity();
                    //FillLocationEntity();
                    //FillDivisionEntity();
                    //FillDepartmentEntity();
                    Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");
                    //FillEntitydrp();
                    //FillManagerdrp();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }
        public void bindData()
        {
            try
            {

                clsEmployeeHierarchy objData = new clsEmployeeHierarchy();
                objData.EmpID = EmployeeHdn.Value;
                objData.Company = ComapnyHdn.Value;
                objData.Location = LocationHdn.Value;
                objData.Division = DivisionHdn.Value;
                objData.Department = DepartmentHdn.Value;

                DataTable dt = clsEmployeeHierarchyHandler.GetEmployeeManagerDetails("EMPFilter", objData);              
                gvEmpMgrAdd.DataSource = dt;
                gvEmpMgrAdd.DataBind();


                DropDownList ddl = (DropDownList)gvEmpMgrAdd.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmpMgrAdd.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmpMgrAdd.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmpMgrAdd.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmpMgrAdd.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmpMgrAdd.PageCount == 0)
                {
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrAdd.PageIndex + 1 == gvEmpMgrAdd.PageCount)
                {
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrAdd.PageIndex == 0)
                {
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvEmpMgrAdd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpMgrAdd.PageSize * gvEmpMgrAdd.PageIndex) + 1) + " to " + (gvEmpMgrAdd.PageSize * (gvEmpMgrAdd.PageIndex + 1));

                gvEmpMgrAdd.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        private void FillManagerEntity()
        {
            try
            {
                DataSet ds = clsCommonHandler.GetEmployeesDetails("AEMP");
                lstManager.DataValueField = "ID";
                lstManager.DataTextField = "NAME";
                lstManager.DataSource = ds.Tables[0];
                lstManager.DataBind();        
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private void FillEmployeeEntity(string empId)
        {
            try
            {
                clsEmployeeHierarchy objHierarchy=new clsEmployeeHierarchy();
                objHierarchy.EmpID=empId;
                DataTable dt=clsEmployeeHierarchyHandler.GetEmployeeManagerDetails("EMP", objHierarchy);
              
                LstEmployee.DataValueField = "ID";
                LstEmployee.DataTextField = "NAME";
                LstEmployee.DataSource = dt;
                LstEmployee.DataBind();

                lstEmployDummy.DataValueField = "ID";
                lstEmployDummy.DataTextField = "NAME";
                lstEmployDummy.DataSource = dt;
                lstEmployDummy.DataBind();

               
            }
            catch (Exception ex)
            {                
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }           
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            //this.messageDiv.InnerHtml = "";
            //this.ddlMngrcode.SelectedIndex = 0;
            //this.cmbEntity.SelectedIndex = 0;
            ////this.ddlMngrcode.SelectedItem.Text = "Select One";
            ////this.cmbEntity.SelectedItem.Text = "Select One";
            //this.lstAEntity.Items.Clear();
            //this.lstSEntity.Items.Clear();
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrAdd.PageIndex = Convert.ToInt32(((DropDownList)gvEmpMgrAdd.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindData();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrAdd.PageIndex = gvEmpMgrAdd.PageIndex - 1;
                bindData();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrAdd.PageIndex = gvEmpMgrAdd.PageIndex + 1;
                bindData();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            bindData();
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            
            try
            {           
                clsEmployeeHierarchy objHier = new clsEmployeeHierarchy();
                objHier.EmpID = EmployeeHdn.Value;
                objHier.MngrID = lstManager.SelectedValue;
                objHier.Company = ComapnyHdn.Value;
                objHier.Location = LocationHdn.Value;
                objHier.Division = DivisionHdn.Value;
                objHier.Department = DepartmentHdn.Value;
                objHier.CreatedBy = Session["uid"].ToString();
                clsEmployeeHierarchyHandler.InsertEmployeeMgr(objHier,ref  strErrMsg, ref strSuccMsg,"HierarchyManagerView.aspx");
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblMessages.Text = strErrMsg;
                    lblMessages.Visible = true;
                    return;
                }
                else
                {
                    reset();
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                }
            }
            catch (Exception ex)
            {                
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            
            }
        }
        protected void lstManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = lstManager.SelectedItem.Text;
            FillEmployeeEntity(lstManager.SelectedItem.Value.ToString());

        }
        protected void btnCancelAdd_Click(object sender, EventArgs e) //Changes by Shrinith 23/Sept/2014
        {

            Response.Redirect("HierarchyManagerView.aspx");
        }
        protected void reset()
        {
            EmployeeHdn.Value = "";
            ComapnyHdn.Value = "";
            LocationHdn.Value = "";
            DivisionHdn.Value = "";
            DepartmentHdn.Value = "";
            lstManager.SelectedIndex = -1;
            RbdEmp.Enabled = true;
            RbdCompany.Enabled = true;
            RbdLocation.Enabled = true;
            RbdDivision.Enabled = true;
            RbdDepartment.Enabled = true;
            RbdEmp.SelectedIndex = 0;
            RbdCompany.SelectedIndex = 0;
            RbdLocation.SelectedIndex = 0;
            RbdDivision.SelectedIndex = 0;
            RbdDepartment.SelectedIndex = 0;
            gvEmpMgrAdd.DataSource = null;
            gvEmpMgrAdd.DataBind();
        }

    }
}