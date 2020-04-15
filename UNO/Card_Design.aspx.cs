using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace UNO
{
  
    public partial class Card_Design : System.Web.UI.Page
    {
        SqlConnection cn  = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        DataSet ds = new DataSet();
        string EmpId;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["imageName"] = "add_image.png";
            string id = Convert.ToString(Request.QueryString["id"]);
            if (Session["uid"] == null || Convert.ToString(Session["uid"]) == "")
            {
                Response.Redirect("Login.aspx", true);
            }
            EmpId = Convert.ToString(Session["uid"]);
            if (!IsPostBack)
            {
                GetFontFamilies();
                GetFontSize();
                BindTableColumn();
                BindImageFields();
                BindTemplateName();
            }
       
            UserId.Value = Convert.ToString(Session["uid"]);
          
            if (id != "" && id != null)
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EXEC USP_CardTemplate @strCommand ='EditData', @intId=" + id + "", cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                saveDiv.InnerHtml = Convert.ToString(dt.Rows[0]["varFrontTmpHtml"]);
                saveDiv1.InnerHtml = Convert.ToString(dt.Rows[0]["varBackTmpHtml"]);
                cn.Close();
                drpTemplate.SelectedValue = id;
                drpCategory.SelectedValue = Convert.ToString(dt.Rows[0]["varCategory"]);
            }
        }
        protected void BindTemplateName()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EXEC USP_CardTemplate @strCommand ='SelectTemplate', @nvarUserId='" + EmpId + "'", cn);
                DataSet dsTemplate = new DataSet();
                da.Fill(dsTemplate);
                cn.Close();
                drpTemplate.DataSource = dsTemplate.Tables[0];
                drpTemplate.DataTextField = "nvarTemplateName";
                drpTemplate.DataValueField = "intId";
                drpTemplate.DataBind();
                drpTemplate.Items.Insert(0, new ListItem("--Select Template--", "0"));

                //drpEditTemp.DataSource = dsTemplate.Tables[0];
                //drpEditTemp.DataTextField = "nvarTemplateName";
                //drpEditTemp.DataValueField = "intId";
                //drpEditTemp.DataBind();                             
                //drpEditTemp.Items.Insert(0, new ListItem("--Select Template--", "0"));

                drpCategory.DataSource = dsTemplate.Tables[1];
                drpCategory.DataTextField = "OCE_DESCRIPTION";
                drpCategory.DataValueField = "OCE_ID";
                drpCategory.DataBind();
                drpCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }
            catch(Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_Design");
            }

        }

        protected void BindImageFields()
        {

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EXEC USP_CardTemplate @strCommand='GetImageFields'", cn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlImages.DataSource = ds.Tables[0];
                ddlImages.DataTextField = "COLUMN_NAME";
                ddlImages.DataBind();
                ddlImages1.DataSource = ds.Tables[0];
                ddlImages1.DataTextField = "COLUMN_NAME";
                ddlImages1.DataBind();
                cn.Close();
            }
            catch(Exception ex)
            {
                   if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_Design");
            }

        }
        public void GetFontFamilies()
        {
            List<string> fontfamilies = new List<string>();
            // fontfamilies.Add("--select Font Family--");
            LstFont.Items.Insert(0, new ListItem("--Font--", "0"));
            LstFont1.Items.Insert(0, new ListItem("--Font--", "0"));
            lidrpFontFamily.Items.Insert(0, new ListItem("--Font--", "0"));
            int count = 1;
            foreach (FontFamily family in FontFamily.Families)
            {
                LstFont.Items.Insert(count, new ListItem(family.Name, family.Name.ToLower().Trim()));
                LstFont1.Items.Insert(count, new ListItem(family.Name, family.Name.ToLower().Trim()));
                lidrpFontFamily.Items.Insert(count, new ListItem(family.Name, family.Name.ToLower().Trim()));
                count = count + 1;
              
            }
     
        }
        protected void GetFontSize()
        {
            List<string> fontSize = new List<string>();
            fontSize.Add("-Size-");
            for (int i = 8; i < 73; i++)
            {
                fontSize.Add(i.ToString());
            }
            ddlSize.DataSource = fontSize;
            ddlSize.DataBind();
            ddlSize1.DataSource = fontSize;
            ddlSize1.DataBind();
            lidrpFontSize.DataSource = fontSize;
            lidrpFontSize.DataBind();

        }
        [WebMethod]

        public static int SaveToDataBase(string TemplateName, string divFronthtml, string divBackhtml, string empid, string SaveTemplate, string Flag, string Category,string CatFlag)
        {
            SqlConnection cn;
            cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
            cn.Open();
            //top: 183px;"
            //  position: relative;"
            string s = "type=\"text\"";
            string s1 = "type=\"text\"/";
            string img = "data=\"findImg\"";
            string img1 = "/";
            divFronthtml = divFronthtml.Replace(img, img1);
            divFronthtml = divFronthtml.Replace(s, s1);
            divBackhtml = divBackhtml.Replace(s, s1);
            divFronthtml = divFronthtml.Replace("value=\"TextBox\"", "");
            divBackhtml = divBackhtml.Replace("value=\"TextBox\"", "");
            divFronthtml = divFronthtml.Replace("data=", "value=");
            divBackhtml = divBackhtml.Replace("data=", "value=");
            string query = "";

            //if (Flag == "New")
            //{
            //    query = "insert into u_StoreTemplate (varFrontTmpHtml,varBackTmpHtml,nvarTemplateName,nvarUserId,intCategory) values('" + divFronthtml + "','" + divBackhtml + "','" + TemplateName + "','" + empid + "','" + Category + "')";
            //}
            //else
            //{
            //    if (SaveTemplate == "Front")
            //    {
            //        query = "update  u_StoreTemplate set varFrontTmpHtml='" + divFronthtml + "' where intId='" + TemplateName + "' ";
            //    }
            //    else if (SaveTemplate == "Back")
            //    {
            //        query = "update  u_StoreTemplate set varBackTmpHtml='" + divBackhtml + "' where intId='" + TemplateName + "' ";
            //    }
            //    else
            //    {
            //        query = "update  u_StoreTemplate set varFrontTmpHtml='" + divFronthtml + "' , varBackTmpHtml='" + divBackhtml + "' where intId='" + TemplateName + "' ";
            //    }
            //}

            SqlCommand cmd = new SqlCommand("USP_CardTemplate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "InsertUpdateTemplate";
            cmd.Parameters.Add("@SaveTemplate", SqlDbType.NVarChar).Value = SaveTemplate;
            cmd.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = Flag;
            cmd.Parameters.Add("@varFrontTmpHtml", SqlDbType.VarChar).Value = divFronthtml;
            cmd.Parameters.Add("@varBackTmpHtml", SqlDbType.VarChar).Value = divBackhtml;
            cmd.Parameters.Add("@nvarTemplateName", SqlDbType.NVarChar).Value = TemplateName; // this is templateId
            cmd.Parameters.Add("@nvarUserId", SqlDbType.NVarChar).Value = empid;
            cmd.Parameters.Add("@varCategory", SqlDbType.NVarChar).Value = Category;
            cmd.Parameters.Add("@CatFlag", SqlDbType.NVarChar).Value = CatFlag;
            int result = Convert.ToInt16(cmd.ExecuteNonQuery());
            cn.Close();
            return result;

        }
       
        protected void BindTableColumn()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter("EXEC USP_CardTemplate @strCommand ='GetTableColumn'", cn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddltxtValue.DataSource = ds.Tables[0];
                ddltxtValue.DataTextField = "COLUMN_NAME";
                ddltxtValue.DataValueField = "COLUMN_NAME";
                ddltxtValue.DataBind();

                ddltxtValue1.DataSource = ds.Tables[0];
                ddltxtValue1.DataTextField = "COLUMN_NAME";
                ddltxtValue1.DataValueField = "COLUMN_NAME";
                ddltxtValue1.DataBind();
                cn.Close();
            }
            catch( Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_Design");
            }
        }
        [WebMethod]
        public static string GetUserId()
        {

            return Convert.ToString(HttpContext.Current.Session["imageName"]);

        }
     
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Card_Design.aspx?id=" + drpEditTemp.SelectedValue + "", false);
        }
    }
}