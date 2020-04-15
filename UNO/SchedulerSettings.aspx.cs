using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace UNO
{
    public partial class SchedularSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                 
                 string SchTaskType = Convert.ToString(Request.QueryString["id"]);
                 if (SchTaskType != null)
                 {
                     Session["AppMode"] = "EDIT";
                     Modify_Data(SchTaskType);
                 }
                 else
                 {
                     Session["AppMode"] = "ADD";
                     FillScheduleType();
                     FillScheduleFrequency();
                 }
            }
        }

        private void FillScheduleType()
        {
            try
            {
                string strSql = "";
                if (Session["AppMode"] == "ADD")
                {
                    strSql = "SELECT CODE,[VALUE] AS SCHEDULE FROM ENT_PARAMS WHERE IDENTIFIER = 'SCHEDULETYPE' " +
                                 "AND CODE NOT IN (SELECT SCHEDULER_TASK_TYPE FROM SCHEDULER WHERE SCHEDULER_ISDELETED = 0) ";
                }
                else
                {
                    strSql = "SELECT CODE,[VALUE] AS SCHEDULE FROM ENT_PARAMS WHERE IDENTIFIER = 'SCHEDULETYPE' ";
                }
                SqlDataAdapter da = new SqlDataAdapter(strSql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlScheduleType.DataValueField = "CODE";
                ddlScheduleType.DataTextField = "SCHEDULE";
                ddlScheduleType.DataSource = dt;
                ddlScheduleType.DataBind();
                ddlScheduleType.Items.Insert(0, "Select One");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void FillScheduleFrequency()
        {
            try
            {
                string strSql = "SELECT CODE,[VALUE] AS FREQUENCY FROM ENT_PARAMS WHERE IDENTIFIER = 'FREQUENCY'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlFrequency.DataValueField = "CODE";
                ddlFrequency.DataTextField = "FREQUENCY";
                ddlFrequency.DataSource = dt;
                ddlFrequency.DataBind();
                ddlFrequency.Items.Insert(0, "Select One");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
          //  Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "handleAdd();", true);
            
                SqlConnection conn = new SqlConnection(AccessController.m_connecton);
                conn.Open();
                SqlTransaction trans;
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;
                trans = conn.BeginTransaction();
                objcmd.Transaction = trans;
                try
                {
                    objcmd.CommandText = "SELECT COUNT(*) FROM SCHEDULER WHERE SCHEDULER_TASK_TYPE = '" + ddlScheduleType.SelectedValue + "' AND SCHEDULER_ISDELETED = '0' ";
                    int rowcount = Convert.ToInt16(objcmd.ExecuteScalar());

                    if (rowcount >= 1)
                    {                        
                        objcmd.CommandText = "UPDATE SCHEDULER SET SCHEDULER_DESCRIPTION = '" + txtScheduleDesc.Text.Trim() + "' ,SCHEDULER_FREQUENCY = '" + ddlFrequency.SelectedValue + "', SCHEDULER_TIME = '" + txtScheduleTime.Text.Trim() + "' ,SCHEDULER_ISDELETED = '0' " +
                                             "WHERE SCHEDULER_TASK_TYPE = '" + ddlScheduleType.SelectedValue + "'";
                        objcmd.ExecuteNonQuery();
                        this.messageDiv.InnerHtml = "Record Updated Successfully";
                    }
                    else
                    {
                        objcmd.CommandText = "INSERT INTO SCHEDULER(SCHEDULER_TASK_TYPE,SCHEDULER_DESCRIPTION,SCHEDULER_FREQUENCY,SCHEDULER_TIME,SCHEDULER_ISDELETED) VALUES('" + ddlScheduleType.SelectedValue + "','" + txtScheduleDesc.Text.Trim() + "','" + ddlFrequency.SelectedValue + "','" + txtScheduleTime.Text.Trim() + "','0')";
                        objcmd.ExecuteNonQuery();
                        this.messageDiv.InnerHtml = "Record Saved Successfully.";
                    }
                    trans.Commit();
                    FillScheduleType();
                    resetall();                   
                   
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                string someScript = "";
                someScript = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                
        }

        private void resetall()
        {
            txtScheduleDesc.Text = "";
            txtScheduleTime.Text = "";
            //ddlScheduleType.SelectedIndex = 0;
            ddlFrequency.SelectedIndex = 0;
            FillScheduleType();
        }

        protected void Modify_Data(string SchedulerTaskType)
        {
            try
            {
                string strsql = "SELECT SCHEDULER_DESCRIPTION, SCHEDULER_TASK_TYPE, SCHEDULER_FREQUENCY, SUBSTRING(CONVERT(VARCHAR,SCHEDULER_TIME,114),0,6) " +
                                "FROM SCHEDULER WHERE SCHEDULER_TASK_TYPE = '" + SchedulerTaskType + "' AND SCHEDULER_ISDELETED = '0' ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);

                FillScheduleType();
                FillScheduleFrequency();
                               
                txtScheduleDesc.Text = dt.Rows[0][0].ToString();
                ddlScheduleType.SelectedValue = dt.Rows[0][1].ToString();
                ddlFrequency.SelectedValue = dt.Rows[0][2].ToString();
                txtScheduleTime.Text = dt.Rows[0][3].ToString();
                ddlScheduleType.Enabled = false;
            }
            catch(Exception ex)
            {
                this.messageDiv.InnerHtml = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["AppMode"] == "ADD")
            {
                resetall();
                this.messageDiv.InnerHtml = "";
            }
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {

        }

        //protected void ddlScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //     SqlConnection conn = new SqlConnection(AccessController.m_connecton);
        //     conn.Open();            
        //     SqlCommand objcmd = new SqlCommand();
        //     objcmd.Connection = conn;
        //     try
        //     {
        //         objcmd.CommandText = "SELECT COUNT(*) FROM SCHEDULER WHERE SCHEDULER_TASK_TYPE = '" + ddlScheduleType.SelectedValue + "' AND SCHEDULER_ISDELETED = '0' ";
        //         int rowcount = Convert.ToInt16(objcmd.ExecuteScalar());

        //         if (rowcount >= 1)
        //         {
        //             this.messageDiv.InnerHtml = " Scheduler Task Type already exist.";
        //             ddlScheduleType.SelectedIndex = 0;
        //         }
        //         else
        //         {
        //             this.messageDiv.InnerHtml = "";
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         this.messageDiv.InnerHtml = ex.Message ;
        //     }
        //     string someScript = "";
        //     someScript = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
        //     Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
        //}
    }
}