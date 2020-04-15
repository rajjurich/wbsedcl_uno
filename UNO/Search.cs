using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace UNO
{
    public class Search
    {
        public DataTable searchTable(string[,] values, DataTable _BaseDT)
        {
            try
            {
                DataTable cloneTable = new DataTable();
                cloneTable = _BaseDT.Clone();
                DataTable dtLinq = new DataTable();
                dtLinq = null;
                string[] _decodeField;
                int length = values.GetLength(0);
                String strRowFilter = "";
              
                string FromDate = "";
                string columnName = "";
                string ToDate = "";
                for (int i = 0; i < length; i++)
                {

                    //if (values[i, 1].ToString() !="")
                    //{
                    _decodeField = values[i, 0].ToString().Split('~');
                    if (_decodeField[1].ToString() != "")
                    {
                        if (values[i, 1].ToString() == "S")
                        {
                            string strFinalFilteValue = EscapeLikeValue(_decodeField[1].ToString());

                            //strRowFilter = strRowFilter + (strRowFilter.Length > 0 ? " AND " : "") + _decodeField[0].ToString() + " =  '" + strFinalFilteValue + "'";
                            strRowFilter = strRowFilter + (strRowFilter.Length > 0 ? " AND " : "") + _decodeField[0].ToString() + " like  '%" + strFinalFilteValue + "%'";
                        }
                        else if (values[i, 1].ToString() == "D")
                        {
                            if (_decodeField[1].ToString() != "")
                            {
                                if (FromDate == "")
                                {
                                    FromDate = _decodeField[1].ToString();
                                    columnName = _decodeField[0].ToString();
                                    strRowFilter = strRowFilter + (strRowFilter.Length > 0 ? " AND " : "") + _decodeField[0].ToString() + " = '" + _decodeField[1].ToString() + "'";
                                }
                                else
                                {
                                
                                    strRowFilter = "";
                                    ToDate = _decodeField[1].ToString();
                                 
                                        _BaseDT = (from DataRow dr in _BaseDT.Rows
                                                   where (DateTime.ParseExact(dr[columnName].ToString(), "dd/MM/yyyy", null) >= DateTime.ParseExact(FromDate, "dd/MM/yyyy", null)
                                                   && DateTime.ParseExact(dr[columnName].ToString(), "dd/MM/yyyy", null) <= DateTime.ParseExact(ToDate, "dd/MM/yyyy", null) ||
                                                   DateTime.ParseExact(dr[_decodeField[0].ToString()].ToString(), "dd/MM/yyyy", null) >= DateTime.ParseExact(FromDate, "dd/MM/yyyy", null)
                                                   && DateTime.ParseExact(dr[_decodeField[0].ToString()].ToString(), "dd/MM/yyyy", null) <= DateTime.ParseExact(ToDate, "dd/MM/yyyy", null))
                                                   select dr).CopyToDataTable();
                                    
                                 
                                }
                            }
                          
                        }
                        else
                        {
                            string strFinalFilteValue = EscapeLikeValue(_decodeField[1].ToString());

                            strRowFilter = strRowFilter + (strRowFilter.Length > 0 ? " AND " : "") + _decodeField[0].ToString() + " like  '%" + strFinalFilteValue + "%'";
                    
                            //strRowFilter = strRowFilter + (strRowFilter.Length > 0 ? " OR " : "") + _decodeField[0].ToString() + " = " + _decodeField[1].ToString() + "";
                        }
                    }

                 
                }
             
                    DataRow[] result = _BaseDT.Select(strRowFilter);

                    foreach (DataRow row in result)
                    {
                        cloneTable.ImportRow(row);
                    }

               
                return cloneTable;


            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private string EscapeLikeValue(string value)
        {
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case ']':
                    case '[':
                    case '%':
                    case '*':
                        sb.Append("[").Append(c).Append("]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}