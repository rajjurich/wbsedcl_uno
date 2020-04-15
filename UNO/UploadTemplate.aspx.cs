using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UNO
{
    public partial class UploadTemplate : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillController();
                FillEmployee();
            }
        }

        private void FillController()
        {
            lstACntrl.Items.Clear();
            lstSCntrl.Items.Clear();
            try
            {
                string _strsql = "select CTLR_ID,CTLR_DESCRIPTION from dbo.ACS_CONTROLLER where CTLR_ISDELETED = 'False'";

                DataTable _dtCtrl = new DataTable();
                _dtCtrl = getDataTable(_strsql);
                if (_dtCtrl != null)      
                {
                    for (int i = 0; i <= _dtCtrl.Rows.Count - 1; i++)
                    {
                        lstACntrl.Items.Add(new ListItem(_dtCtrl.Rows[i][0].ToString().PadRight(10,' ') + _dtCtrl.Rows[i][1].ToString(), _dtCtrl.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void FillEmployee()
        {
            lstAEmp.Items.Clear();
            lstSEmp.Items.Clear();
            try
            {
                string _strsql = " select EPD_EMPID,EPD_FIRST_NAME + ' ' + EPD_LAST_NAME as Name "+
                                 "  from dbo.ENT_EMPLOYEE_PERSONAL_DTLS INNER JOIN Finger_Template  ON EPD_EMPID = EmployeeCD "+
                                 "  inner join ENT_EMPLOYEE_OFFICIAL_DTLS  on EPD_EMPID=EOD_EMPID "+
                                 "  where EPD_ISDELETED = '0' AND IsDeleted = '0' and EOD_ACTIVE='1' " ;

                DataTable _dtEmp = new DataTable();
                _dtEmp = getDataTable(_strsql);
                if (_dtEmp != null)
                {
                    for (int i = 0; i <= _dtEmp.Rows.Count - 1; i++)
                    {
                        lstAEmp.Items.Add(new ListItem(_dtEmp.Rows[i][0].ToString().PadRight(20, ' ') + _dtEmp.Rows[i][1].ToString(), _dtEmp.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable getDataTable(string _strQuery)
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();

                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _da = new SqlDataAdapter(_strQuery, _sqlConnection);
                _result = _da.Fill(_ds);

                return _ds.Tables[0];
            }
            catch (Exception ex)
            { return null; }
        }

        protected void btnCntrlRight_Click(object sender, EventArgs e)
        {
            if (lstACntrl.SelectedItem != null)
            {
                //lstSReader.Items.Add(lstAReader.SelectedItem);
                //lstAReader.Items.Remove(lstAReader.SelectedItem);  
                for (int i = lstACntrl.Items.Count - 1; i >= 0; i--)
                {
                    if (lstACntrl.Items[i].Selected == true)
                    {
                        lstSCntrl.Items.Add(lstACntrl.Items[i]);
                        ListItem li = lstACntrl.Items[i];
                        lstACntrl.Items.Remove(li);
                    }
                }
                lstSCntrl.SelectedValue = null;
            }
        }

        protected void btnCntrlLeft_Click(object sender, EventArgs e)
        {
            if (lstSCntrl.SelectedItem != null)
            {
                //lstAReader.Items.Add(lstSReader.SelectedItem);
                //lstSReader.Items.Remove(lstSReader.SelectedItem);
                for (int i = lstSCntrl.Items.Count - 1; i >= 0; i--)
                {
                    if (lstSCntrl.Items[i].Selected == true)
                    {
                        lstACntrl.Items.Add(lstSCntrl.Items[i]);
                        ListItem li = lstSCntrl.Items[i];
                        lstSCntrl.Items.Remove(li);
                    }
                }
                lstACntrl.SelectedValue = null;
            }
        }

        protected void btnEmpRight_Click(object sender, EventArgs e)
        {           
            if (lstAEmp.SelectedItem != null)
            {               
                for (int i = lstAEmp.Items.Count - 1; i >= 0; i--)
                {
                    if (lstAEmp.Items[i].Selected == true)
                    {
                        lstSEmp.Items.Add(lstAEmp.Items[i]);
                        ListItem li = lstAEmp.Items[i];
                        lstAEmp.Items.Remove(li);
                    }
                }
                lstSEmp.SelectedValue = null;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "Mykey", "AllCtlrSelected();", true);
        }

        protected void btnEmpLeft_Click(object sender, EventArgs e)
        {

            if (lstSEmp.SelectedItem != null)
            {
                //lstAReader.Items.Add(lstSReader.SelectedItem);
                //lstSReader.Items.Remove(lstSReader.SelectedItem);
                for (int i = lstSEmp.Items.Count - 1; i >= 0; i--)
                {
                    if (lstSEmp.Items[i].Selected == true)
                    {
                        lstAEmp.Items.Add(lstSEmp.Items[i]);
                        ListItem li = lstSEmp.Items[i];
                        lstSEmp.Items.Remove(li);
                    }
                }
                lstAEmp.SelectedValue = null;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                string strsql = "";
                int rows = 0;
                if (_sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _sqlConnection;

                if (rbSelect.SelectedValue.ToString() == "U")
                {
                    if (chkAllCtlr.Checked)
                    {
                        if (chkAllEmp.Checked)
                        {
                            for (int i = 0; i < lstACntrl.Items.Count; i++)
                            {
                                strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                               "VALUES('','" + lstACntrl.Items[i].Text.Substring(0, 10).Trim() + "','UA','False') ";
                                cmd.CommandText = strsql;
                                rows = cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lstACntrl.Items.Count; i++)
                            {
                                for (int j = 0; j < lstSEmp.Items.Count; j++)
                                {
                                    strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                                   "VALUES('" + lstSEmp.Items[j].Text.Substring(0, 20).Trim() + "','" + lstACntrl.Items[i].Text.Substring(0, 10).Trim() + "','U','False') ";
                                    cmd.CommandText = strsql;
                                    rows = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (chkAllEmp.Checked)
                        {
                            for (int i = 0; i < lstSCntrl.Items.Count; i++)
                            {
                                strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                         "VALUES('','" + lstSCntrl.Items[i].Text.Substring(0, 10).Trim() + "','UA','False') ";
                                cmd.CommandText = strsql;
                                rows = cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lstSCntrl.Items.Count; i++)
                            {
                                for (int j = 0; j < lstSEmp.Items.Count; j++)
                                {
                                    strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                                   "VALUES('" + lstSEmp.Items[j].Text.Substring(0, 20).Trim() + "'," +
                                                   "'" + lstSCntrl.Items[i].Text.Substring(0, 10).Trim() + "','U','False') ";
                                    cmd.CommandText = strsql;
                                    rows = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                else if (rbSelect.SelectedValue.ToString() == "D")
                {
                    if (chkAllCtlr.Checked)
                    {
                        if (chkAllEmp.Checked)
                        {
                            for (int i = 0; i < lstACntrl.Items.Count; i++)
                            {
                                strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                               "VALUES('','" + lstACntrl.Items[i].Text.Substring(0, 10).Trim() + "','DA','False') ";
                                cmd.CommandText = strsql;
                                rows = cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lstACntrl.Items.Count; i++)
                            {
                                for (int j = 0; j < lstSEmp.Items.Count; j++)
                                {
                                    strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                                   "VALUES('" + lstSEmp.Items[j].Text.Substring(0, 20).Trim() + "','" + lstACntrl.Items[i].Text.Substring(0, 10).Trim() + "','D','False') ";
                                    cmd.CommandText = strsql;
                                    rows = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (chkAllEmp.Checked)
                        {
                            for (int i = 0; i < lstSCntrl.Items.Count; i++)
                            {
                                strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                         "VALUES('','" + lstSCntrl.Items[i].Text.Substring(0, 10).Trim() + "','DA','False') ";
                                cmd.CommandText = strsql;
                                rows = cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lstSCntrl.Items.Count; i++)
                            {
                                for (int j = 0; j < lstSEmp.Items.Count; j++)
                                {
                                    strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                                   "VALUES('" + lstSEmp.Items[j].Text.Substring(0, 20).Trim() + "'," +
                                                   "'" + lstSCntrl.Items[i].Text.Substring(0, 10).Trim() + "','D','False') ";
                                    cmd.CommandText = strsql;
                                    rows = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                else if (rbSelect.SelectedValue.ToString() == "R")
                {
                    if (chkAllCtlr.Checked)
                    {
                        for (int i = 0; i < lstACntrl.Items.Count; i++)
                        {
                            strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                           "VALUES('','" + lstACntrl.Items[i].Text.Substring(0, 10).Trim() + "','R','False') ";
                            cmd.CommandText = strsql;
                            rows = cmd.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < lstSCntrl.Items.Count; i++)
                        {
                            strsql = "INSERT INTO FINGER_UPLOAD(EMP_CD,CTLR_ID,PERFORM_ACTION,SEND_STATUS) " +
                                           "VALUES('','" + lstSCntrl.Items[i].Text.Substring(0, 10).Trim() + "','R','False') ";
                            cmd.CommandText = strsql;
                            rows = cmd.ExecuteNonQuery();
                        }
                    }

                }

                if (rows >= 1)
                    this.messageDiv.InnerHtml = "Record Saved Successfully";
                else
                    this.messageDiv.InnerHtml = "Failed to save record";

                string someScript2 = "";
                someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",2000);</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

                Reset();
            }

        }

        public Boolean validation()
        {
            try
            {
                string script = "";               
                if (chkAllCtlr.Checked == false && rbSelect.SelectedValue == "R" && lstSCntrl.Items.Count == 0)
                { 
                    script = "<script language='javascript'>alert('Please select Controller');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (chkAllCtlr.Checked == false && rbSelect.SelectedValue == "U" && lstSCntrl.Items.Count == 0)
                {
                    script = "<script language='javascript'>alert('Please select Controller.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (chkAllEmp.Checked == false && rbSelect.SelectedValue == "U" && lstSEmp.Items.Count == 0)
                { 
                    script = "<script language='javascript'>alert('Please select Employee.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (chkAllCtlr.Checked == false && rbSelect.SelectedValue == "D" && lstSCntrl.Items.Count == 0)
                { //this.messageDiv.InnerHtml = "Please select Time Zone."; return false;
                    script = "<script language='javascript'>PushAlert('Please select Controller.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (chkAllEmp.Checked == false && rbSelect.SelectedValue == "D" && lstSEmp.Items.Count == 0)
                {
                    script = "<script language='javascript'>alert('Please select Employee.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void chkAllCtlr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCtlr.Checked)
            {
                lstACntrl.Enabled = false;
                lstSCntrl.Enabled = false;
                btnCntrlLeft.Enabled = false;
                btnCntrlRight.Enabled = false;
            }
            else
            {
                lstACntrl.Enabled = true;
                lstSCntrl.Enabled = true;
                btnCntrlLeft.Enabled = true;
                btnCntrlRight.Enabled = true;
            }

        }

        protected void chkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllEmp.Checked)
            {
                lstAEmp.Enabled = false;
                lstSEmp.Enabled = false;
                btnEmpLeft.Enabled = false;
                btnEmpRight.Enabled = false;
            }
            else
            {
                lstAEmp.Enabled = true;
                lstSEmp.Enabled = true;
                btnEmpLeft.Enabled = true;
                btnEmpRight.Enabled = true;
            }

        }

        protected void rbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbSelect.SelectedValue == "R")
            {
                chkAllEmp.Enabled = false;
                lstAEmp.Enabled = false;
                lstSEmp.Enabled = false;
                btnEmpLeft.Enabled = false;
                btnEmpRight.Enabled = false;
            }
            else
            {
                chkAllEmp.Enabled = true;
                lstAEmp.Enabled = true;
                lstSEmp.Enabled = true;
                btnEmpLeft.Enabled = true;
                btnEmpRight.Enabled = true;
            }
        }

        private void Reset()
        {
            lstACntrl.Items.Clear();
            lstSCntrl.Items.Clear();
            lstAEmp.Items.Clear();
            lstSEmp.Items.Clear();           
            FillController();
            FillEmployee();

            chkAllCtlr.Checked = false;
            chkAllCtlr.Enabled = true;
            lstACntrl.Enabled = true;
            lstSCntrl.Enabled = true;
            btnCntrlLeft.Enabled = true;
            btnCntrlRight.Enabled = true;
            
            chkAllEmp.Checked = false;
            chkAllEmp.Enabled = true;
            lstAEmp.Enabled = true;
            lstSEmp.Enabled = true;
            btnEmpLeft.Enabled = true;
            btnEmpRight.Enabled = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }        

    }
}