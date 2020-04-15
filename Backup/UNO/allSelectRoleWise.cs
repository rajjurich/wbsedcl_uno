using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;


namespace UNO
{
    public class allSelectRoleWise
    {

        public HiddenField allSelect(DataSet ds, String strVal)
        {
            DataTable dt = new DataTable(); ;
            if (strVal == "COM")
                dt = ds.Tables[4];
            else if (strVal == "DIV")
                dt = ds.Tables[3];
            else if (strVal == "DEPT")
                dt = ds.Tables[2];
            else if (strVal == "GRP")
                dt = ds.Tables[7];
            else if (strVal == "GRD")
                dt = ds.Tables[6];
            else if (strVal == "LOC")
                dt = ds.Tables[1];
            else if (strVal == "SFT")
                dt = ds.Tables[9];

            HiddenField hdn = new HiddenField();
            hdn.Value = "";
            String str = "";
            if (dt.Rows.Count > 0)
            {
                str = "(";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str = str + "'" + dt.Rows[i][0].ToString() + "',";
                }
                str = str.Remove(str.Length - 1);
                str = str + ")";
            }

            hdn.Value = str;
            return hdn;
        }


        public HiddenField empAllSelect(ListBox lst)
        {
            HiddenField hdn = new HiddenField();
            hdn.Value = "";
            String str = "";
            if (lst.Items.Count > 0)
            {
                str = "(";
                for (int i = 0; i < lst.Items.Count; i++)
                {
                    str = str + "'" + lst.Items[i].Value.ToString() + "',";
                }
                str = str.Remove(str.Length - 1);
                str = str + ")";
            }

            hdn.Value = str;
            return hdn;
        }
    }
}