using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;


namespace UNO
{
    public partial class SaveTemplate : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        DataSet ds = new DataSet();
        string EmpId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindDropDown();
               // lstEmplyee.Items.Clear();
            }
            if (Session["uid"] == null || Convert.ToString(Session["uid"]) == "")
            {
                Response.Redirect("Login.aspx", true);
            }
            EmpId = Convert.ToString(Session["uid"]);
        
            hfconnection.Value = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ToString().Replace(' ', ',');
        }
        protected void BindDropDown()
        {
            //if (ds.Tables.Count == 0)
            //{
            //    getDataset();
            //}
            //ddlTemplateName.DataSource = ds.Tables[0];
            //ddlTemplateName.DataTextField = "OCE_DESCRIPTION";
            //ddlTemplateName.DataValueField = "varCategory";
            //ddlTemplateName.DataBind();
            //ddlTemplateName.Items.Insert(0, new ListItem("--Select Template--", "0"));
        }
       
        protected void ddlTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
           // BindGridView(Convert.ToString(ddlTemplateName.SelectedValue));
            lstEmplyee.Items.Clear();
           // Session["categoryId"] = ddlTemplateName.SelectedValue;
            empDetail.Visible = true;


        }
        protected void getDataset()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EXEC USP_CardTemplate  @strCommand ='SelectedCategory'", cn);
                da.Fill(ds);
                cn.Close();
            }
            catch(Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "SaveTemplate");
            }

        }
     

        protected void rd1_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1.Checked == true)
            {
                txtEmpName.Enabled = true;
                txtEmpSearch.Enabled = false;
            }
            else
            {
                txtEmpName.Enabled = false;
                txtEmpSearch.Enabled = true;
            }
            
        }

        protected void rd2_CheckedChanged(object sender, EventArgs e)
        {
            if (rd2.Checked == true)
            {
                txtEmpName.Enabled = false;
                txtEmpSearch.Enabled = true;
            }
            else
            {
                txtEmpName.Enabled = true;
                txtEmpSearch.Enabled = false;
            }
         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rd1.Checked == true)
            {
                string[] getEmpId = null;
                if (txtEmpName.Text.Trim() != "")
                {
                    if (txtEmpName.Text.Trim().IndexOf("|") > 1)
                    {
                        getEmpId = txtEmpName.Text.Trim().Split('|');
                        lstEmplyee.Items.Add(getEmpId[1].Trim() + " | " + getEmpId[0].Trim());
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select employee from list');", false);
                        //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/jscript'>alert('Please select employee from list')</script>", true);
                        //getEmpId[0] = "";
                        // getEmpId[0] = txtEmpName.Text.Trim();

                    }

                    txtEmpName.Text = "";
                    // lstEmplyee.Attributes.Add(getEmpId[1].Trim() + " | " + getEmpId[0].Trim(), Convert.ToString(lstEmplyee.Attributes.Count + 1));
                }

            }
            else
            {
                if (txtEmpSearch.Text.Trim() != "")
                {
                    lstEmplyee.Items.Add(txtEmpSearch.Text.Trim());
                    txtEmpSearch.Text = "";
                }

            }
            
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            while (lstEmplyee.GetSelectedIndices().Length > 0)
            {
                lstEmplyee.Items.Remove(lstEmplyee.SelectedItem);
            }
        }
       
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            lstEmplyee.Items.Clear();
        }

      
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetdataById(string prefixText, int count, string contextKey)
        {
            DataTable dt = new DataTable();
            SqlConnection cn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
         
            //Get data in datatable 
            if (dt != null)
            {
                cn1.Open();
                string strSQL = "EXEC USP_CardTemplate @strCommand='GetDataByID', @prefixText='" + prefixText + "',@nvarUserId='" + Convert.ToString(HttpContext.Current.Session["uid"]) + "',@EOD_CATEGORY_ID=''";
                SqlDataAdapter da = new SqlDataAdapter(strSQL, cn1);
                da.Fill(dt);
                cn1.Close();
            }
            List<String> list = new List<String>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr["EPD_EMPID"].ToString());
            }
            return list;
        }
       
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetdataByName(string prefixText, int count, string contextKey)
        {
            DataTable dt = new DataTable();
            SqlConnection cn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
            //Make your database connection here
            //    string strSQL = "select    EPD_FIRST_NAME+ ' ' + isnull (EPD_MIDDLE_NAME,'') + ' '+ isnull(EPD_LAST_NAME,'') +'      |      '+EPD_EMPID as EPD_EMPID from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_FIRST_NAME like '" + prefixText + "%'   order by EPD_EMPID,EPD_FIRST_NAME";
            //Get data in datatable 
            if (dt != null)
            {
                cn1.Open();
                string strSQL = "EXEC USP_CardTemplate @strCommand='GetDataByName', @prefixText='" + prefixText + "',@nvarUserId='" + Convert.ToString(HttpContext.Current.Session["uid"]) + "',@EOD_CATEGORY_ID=''";
                SqlDataAdapter da = new SqlDataAdapter(strSQL, cn1);
                da.Fill(dt);
                cn1.Close();
            }
            List<String> list = new List<String>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr["EPD_EMPID"].ToString());
            }
            return list;
        }

       
       
        protected void rdoBtnCate_CheckedChanged(object sender, EventArgs e)
        {
            lstEmplyee.Items.Clear();
          //  ddlTemplateName.Visible = true;
          //  ddlTemplateName.SelectedValue = "0";
            empDetail.Visible = false;
           // Session["categoryId"] = "";
            
          
        }

        protected void rdoBtnEmp_CheckedChanged(object sender, EventArgs e)
        {
            lstEmplyee.Items.Clear();
           // ddlTemplateName.Visible = false;
            empDetail.Visible = true;
          //  Session["categoryId"] = "";
        }
    }
}