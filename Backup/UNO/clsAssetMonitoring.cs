using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace UNO
{
    public class generalCls
    {
        /*properties used for Asset Master*/
        public Int64 AssetId
        { get; set; }
        public string AssetCode
        { get; set; }
        public string AssetDesc
        { get; set; }
        public string AssetType
        { get; set; }
        public string AssetSrno
        { get; set; }
        public string AssetMake
        { get; set; }
        public string AssetModel
        { get; set; }

        /*properties used for Controller Master*/
        public Int64 CtlrId
        { get; set; }
        public string CtlrDesc
        { get; set; }
        public string CtlrIP
        { get; set; }
        public string LocationID
        { get; set; }

        /*properties used for Tag Master*/
        public Int64 TagId
        { get; set; }
        public string TagCode
        { get; set; }

        /*properties used for Mapping*/
        public static Int16 MappingId
        { get; set; }
        public static string SaveEditFlag
        { get; set; }
    }

    public class ExecuteSQL
    {
        public static DataSet ExecuteDataSet(string cmdText, StringDictionary nameVC)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["connection_string"].ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = cmdText;
            cmd.Parameters.Clear();

            if (nameVC != null)
            {
                if (nameVC.Count > 0)
                {
                    int i = 0;
                    DbParameter[] param = new DbParameter[nameVC.Count];
                    IEnumerator enumList = nameVC.GetEnumerator();
                    DictionaryEntry de;
                    while (enumList.MoveNext())
                    {
                        param[i] = factory.CreateParameter();

                        de = (DictionaryEntry) enumList.Current;

                        param[i].ParameterName = de.Key.ToString();
                        
                        if (de.Value == null)
                        {
                            param[i].Value = DBNull.Value;
                        }
                        else
                        {
                            param[i].Value = Convert.ToString(de.Value);
                        }
                        cmd.Parameters.Add(param[i]);
                        i++;
                    }
                }
            }

            DbDataAdapter da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            cmd.Dispose();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Dispose();

            return ds;
        }
        public static DataSet ExecuteDataSetHashTable(string cmdText, Hashtable ht)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["connection_string"].ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = cmdText;
            cmd.Parameters.Clear();

            if (ht != null)
            {
                if (ht.Count > 0)
                {
                    int i = 0;
                    DbParameter[] param = new DbParameter[ht.Count];
                    IDictionaryEnumerator enumList = ht.GetEnumerator();
                    while (enumList.MoveNext())
                    {
                        param[i] = factory.CreateParameter();
                        param[i].ParameterName = enumList.Key.ToString();
                        if (enumList.Value == null)
                        {
                            param[i].Value = DBNull.Value;
                        }
                        else
                        {
                            param[i].Value = Convert.ToString(enumList.Value);
                        }
                        cmd.Parameters.Add(param[i]);
                        i++;
                    }
                }
            }

            DbDataAdapter da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            cmd.Dispose();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Dispose();

            return ds;
        }
    }
}