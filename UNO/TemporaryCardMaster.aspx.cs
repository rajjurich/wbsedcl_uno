using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient ;
using System.Data;
using System.Configuration;
using System.IO;



namespace UNO
{
    public partial class TemporaryCardMaster : System.Web.UI.Page
   {
       public static string Mstr_Con = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
       string Entity_Mode = "";
       string emplyeeid = "";
       bool Flag = false;
      
       protected void Page_Load(object sender, EventArgs e)
        {
            emplyeeid = Convert.ToString(Request.QueryString["id"]);
          
            if (Session["Mode"] != null)
            {
                Entity_Mode = Session["Mode"].ToString();
            }
            else
            {
                Entity_Mode = "Add";
            }
            
           
            if (!Page.IsPostBack)
            {


                FillReason();
                if (emplyeeid != null && Entity_Mode == "Modify")
                {
                    Flag = false;
                    Modify_Data(emplyeeid);
                }

                else
                {

                }
            }
            else
            {
                if (Entity_Mode == "Modify" && Flag == true)
                {
                    Modify_Data(emplyeeid);
                }
                else
                {

                }
            }
            
        }

        protected void LnkBack_Click(object sender, EventArgs e)
        {
            if (txtTempCardID.Text != "" && txtTempCardID.Enabled == false)
            {
                Label1.Text = "Save the Records or Click Cancel";
                LoadJScript();
                return;

            }
            else
            {
                Response.Redirect("TemporaryCardViewMaster.aspx");
            }
        }
        private void FillReason()
        {
            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type";
                   strSql = strSql + " and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'TC'";
            SqlDataAdapter da = new SqlDataAdapter(strSql, Mstr_Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlReason.DataValueField = "Reason_ID";
            ddlReason.DataTextField = "Reason_Description";
            ddlReason.DataSource = dt;
            ddlReason.DataBind();
            
            ddlReason.Items.Insert(0, "Select One");

            //ddlReason.DataValueField = "Reason_ID";
            //ddlReason.DataTextField = "Reason_Description";
            //ddlReason.DataSource = dt;
            //ddlReason.DataBind();
            //ddlReason.Items.Insert(0, "Select One");

        }



        protected void Modify_Data(string Employeeid)
        {

            string strsql = "SELECT TC_EMPLOYEE_ID,TC_ORI_CARD_ID,TC_TMP_CARD_ID,TC_ISSUEDT,TC_RETURNDT,TC_REASON_ID FROM TA_TEMPCARD WHERE TC_TMP_CARD_ID ='"+ Employeeid +"' AND TC_ISDELETED=0";
           
            SqlDataAdapter da = new SqlDataAdapter(strsql, Mstr_Con);
            DataTable dt = new DataTable();
            dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                txtTempCardID.Text = dt.Rows[i]["TC_TMP_CARD_ID"].ToString();
                txtEmpCd.Text = dt.Rows[i]["TC_EMPLOYEE_ID"].ToString();
                txtIssueDate.Text = Convert.ToDateTime(dt.Rows[i]["TC_ISSUEDT"].ToString()).ToString("dd/MM/yyyy");
                txtReturnDate.Text = Convert.ToDateTime(dt.Rows[i]["TC_RETURNDT"].ToString()).ToString("dd/MM/yyyy");
                ddlReason.SelectedValue = dt.Rows[i]["TC_REASON_ID"].ToString();
            }
            TblInfo.Visible = true;
            TblBtn.Visible = true;
            DoorConfig.Visible = true;
            txtTempCardID.Enabled = false;
        
        
        }

       
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
             

                SqlConnection conn = new SqlConnection(Mstr_Con );
                conn.Open();
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;

            //SELECT TC_REC_ID,TC_EMPLOYEE_ID,TC_ORI_CARD_ID,TC_TMP_CARD_ID,TC_ISSUEDT,TC_RETURNDT,TC_REASON_ID FROM TA_TEMPCARD

                objcmd.CommandText = "SELECT TC_TMP_CARD_ID FROM TA_TEMPCARD Where  TC_TMP_CARD_ID='" + txtTempCardID.Text.Trim() + "' AND TC_ISDELETED=0 ";
                string rows =Convert.ToString(objcmd.ExecuteScalar());

               
                   
                    try
                    {
                        if (Entity_Mode == "Add")
                        {
                            if (rows != "")
                            {
                                Label1.Text = "Work Day Master File Code Already Exists.";
                                string someScript = "";
                                someScript = "<script language='javascript'>clearFunction();</script>";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                                LoadJScript();
                                return;

                            }
                            if (txtTempCardID.Text != "")
                            {

                                string InsertValue = "INSERT INTO TA_TEMPCARD VALUES('" + txtEmpCd.Text.ToUpper() + "','" + txtTempCardID.Text.ToUpper() + "','" + txtTempCardID.Text.ToUpper() + "',convert(datetime,'" + txtIssueDate.Text.Trim() + "',103),convert(datetime,'" + txtReturnDate.Text + "',103),'" + ddlReason.SelectedValue + "',0,Null)";

                                objcmd = new SqlCommand(InsertValue, conn);
                                objcmd.ExecuteNonQuery();
                                Label1.Text = "Record Saved Successfully.";
                                txtTempCardID.Text = "";
                                txtEmpCd.Text = "";
                                txtIssueDate.Text = "";
                                txtReturnDate.Text = "";
                                ddlReason.SelectedIndex = 0;

                                LoadJScript();


                            }
                        }
                        else
                        {



                            string InsertValue = "UPDATE TA_TEMPCARD SET TC_ISSUEDT =convert(datetime,'" + txtIssueDate.Text + "',103) , TC_RETURNDT=convert(datetime,'" + txtReturnDate.Text + "',103) , TC_REASON_ID='" + ddlReason.SelectedValue + "' where TC_TMP_CARD_ID='" + txtTempCardID.Text.Trim() + "'";

                            objcmd = new SqlCommand(InsertValue, conn);
                            objcmd.ExecuteNonQuery();

                            Label1.Text = "Record Saved Successfully";
                            txtTempCardID.Text = "";
                            txtEmpCd.Text = "";
                            txtIssueDate.Text = "";
                            txtReturnDate.Text = "";
                            ddlReason.SelectedIndex = 0;
                            LoadJScript();

                            Session.Remove("Mode");
                        }
                        
                    }


                    catch (Exception ex)
                    {
                       
                        throw ex;
                      

                    }
                   
                    conn.Close();
                 
        }


            internal void LoadJScript()
            {
                ClientScriptManager script = Page.ClientScript;
                //prevent duplicate script
                if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
                {
                    script.RegisterStartupScript(this.GetType(), "HideLabel",
                    "<script type='text/javascript'>HideLabel('" + Label1.ClientID + "')</script>");
                }
            } 
         protected void BtnCancel_Click(object sender, EventArgs e)
        {
           
            if (Entity_Mode == "Modify" && emplyeeid !=null )
            {
                Modify_Data(emplyeeid);
            }
            else
            {
                txtTempCardID.Text = "";
                txtEmpCd.Text = "";
                txtIssueDate.Text = "";
                txtReturnDate.Text = "";
                ddlReason.SelectedIndex = 0;
                
                Label1.Text = "";

              
               
                Session.Remove("Mode");
                return;
              
            }
         
        }
       

       

       
        private void FillDDL()
        {
           
            string CODE = (string)(Session["ID"]);
            string DESC = (string)(Session["Description"]);

            txtTempCardID.Text = CODE;
            txtEmpCd.Text =DESC;

        }
        
    }
}
