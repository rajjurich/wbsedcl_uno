using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Data;
using System.Xml.Serialization;
using System;
namespace CMS.UNO.Framework.DataAccess
{
    public enum EnumProviders
    {
        ODBC,
        SQLClient,
        OLEDB
    }

    public class ParamStruct
    {
        public string ParamName;
        public DbType DataType;
        public object value;
        public ParameterDirection direction;
        public string sourceColumn;
        public Int32 size;

        public ParamStruct()
        {

        }

        public ParamStruct(string pi_strParamName, DbType pi_DataType, object pi_objValue, ParameterDirection pi_ParamDirection)
        {
            ParamName = pi_strParamName;
            DataType = pi_DataType;
            value = pi_objValue;
            direction = pi_ParamDirection;
        }

        public ParamStruct(string pi_strParamName, DbType pi_DataType, object pi_objValue,
            ParameterDirection pi_ParamDirection, Int32 pi_intSize)
        {
            ParamName = pi_strParamName;
            DataType = pi_DataType;
            value = pi_objValue;
            direction = pi_ParamDirection;
            size = pi_intSize;
        }

        public ParamStruct(string pi_strParamName, DbType pi_DataType, object pi_objValue,
            ParameterDirection pi_ParamDirection, Int32 pi_intSize, string pi_strSourceColumn)
        {
            ParamName = pi_strParamName;
            DataType = pi_DataType;
            value = pi_objValue;
            direction = pi_ParamDirection;
            size = pi_intSize;
            sourceColumn = pi_strSourceColumn;
        }

    }

    internal class ProviderFactory
    {

        private ProviderFactory()
        {
        }

        public static IDbCommand GetCommand(EnumProviders provider)
        {
            if (provider == EnumProviders.ODBC)
            {
                return new OdbcCommand();
            }
            else if (provider == EnumProviders.SQLClient)
            {
                return new SqlCommand();
            }
            else if (provider == EnumProviders.OLEDB)
            {
                return new OleDbCommand();
            }
            else
            {
                return null;
            }
        }

        public static IDbCommand GetCommand(string strCmdText, CommandType cmdType, int cmdTimeout, clsParameterCollection ParameterArray, EnumProviders provider)
        {
            IDbCommand cmd = GetCommand(provider);
            int i;
            if (!(ParameterArray == null))
            {
                for (i = 0; i <= ParameterArray.Count - 1; i++)
                {
                    ParamStruct ps = ParameterArray[i];
                    IDbDataParameter pm = GetParameter(ps.ParamName, ps.direction, ps.value, ps.DataType, ps.sourceColumn, ps.size, provider);
                    cmd.Parameters.Add(pm);
                }
            }
            cmd.CommandType = cmdType;
            cmd.CommandText = strCmdText;
            return cmd;
        }

        public static IDbConnection GetConnection(EnumProviders provider)
        {
            if (provider == EnumProviders.ODBC)
            {
                return new OdbcConnection();
            }
            else if (provider == EnumProviders.SQLClient)
            {
                return new SqlConnection();
            }
            else if (provider == EnumProviders.OLEDB)
            {
                return new OleDbConnection();
            }
            else
            {
                return null;
            }
        }
 
        public static IDbConnection GetConnection(string strConnStringName, EnumProviders provider)
        {
            IDbConnection con = GetConnection(GetProvider(strConnStringName));
            StringBuilder strConnString = new StringBuilder(300);
            strConnString.Append(GetConnectionString(strConnStringName));
            strConnString.Append(";App=");
            strConnString.Append(provider.ToString());
            strConnString.Append(" Provider");
            con.ConnectionString = strConnString.ToString();
            return con;
        }

        public static IDbDataAdapter GetAdapter(EnumProviders provider)
        {
            if (provider == EnumProviders.ODBC)
            {
                return new OdbcDataAdapter();
            }
            else if (provider == EnumProviders.SQLClient)
            {
                return new SqlDataAdapter();
            }
            else if (provider == EnumProviders.OLEDB)
            {
                return new OleDbDataAdapter();
            }
            else
            {
                return null;
            }
        }

        public static IDbDataParameter GetParameter(EnumProviders provider)
        {
            if (provider == EnumProviders.ODBC)
            {
                return new OdbcParameter();
            }
            else if (provider == EnumProviders.SQLClient)
            {
                return new SqlParameter();
            }
            else if (provider == EnumProviders.OLEDB)
            {
                return new OleDbParameter();
            }
            else
            {
                return null;
            }
        }

        public static IDbDataParameter GetParameter(string paramName, ParameterDirection paramDirection, object paramValue, DbType paramtype, string sourceColumn, Int32 size, EnumProviders provider)
        {
            IDbDataParameter param = GetParameter(provider);
            param.ParameterName = paramName;
            param.DbType = paramtype;
            if (size > 0)
            {
                param.Size = size;
            }
            if (!(paramValue == null))
            {
                param.Value = paramValue;
            }
            param.Direction = paramDirection;
            if (!(sourceColumn == ""))
            {
                param.SourceColumn = sourceColumn;
            }
            return param;
        }

        public static IDbTransaction GetTransaction(IDbConnection conn, IsolationLevel transisolationLevel)
        {
            return conn.BeginTransaction(transisolationLevel);
        }

        public static object GetCommandBuilder(EnumProviders provider)
        {
            if (provider == EnumProviders.ODBC)
            {
                return new OdbcCommandBuilder();
            }
            else if (provider == EnumProviders.SQLClient)
            {
                return new SqlCommandBuilder();
            }
            else if (provider == EnumProviders.OLEDB)
            {
                return new OleDbCommandBuilder();
            }
            else
            {
                return null;
            }
        }

        public static string GetConnectionString(string pi_strConnectionName)
        {

          
            return ConfigurationManager.ConnectionStrings[pi_strConnectionName].ConnectionString;

        }

        public static EnumProviders GetProvider(string pi_strConnectionName)
        {

            {
                string strProviderName;
                strProviderName = ConfigurationManager.ConnectionStrings[pi_strConnectionName].ProviderName;
                if (strProviderName == "System.Data.SqlClient")
                {
                    return EnumProviders.SQLClient;
                }
                else if (strProviderName == "ODBC")
                {
                    return EnumProviders.ODBC;
                }
                else if (strProviderName == "OleDB")
                {
                    return EnumProviders.OLEDB;
                }
                else
                    throw new Exception("Invalid Provider type specified");
            }
        }
    }
    public class DataAccess : IDisposable
    {
        private IDbTransaction _trans;
        private IsolationLevel _isolationLevel;
        private IDbConnection _conn;
        private int _cmdTimeout;
        private string _connStringName;
        private EnumProviders _provider;
        private const int COMMAND_TIMEOUT = 100;
        private CommandBehavior _commandBehavior;
        //clsDbLayerSection objDbLayerSection;
        bool blndisposed = false;

      
        public DataAccess(string strConnectionStringName)
        {
            _isolationLevel = IsolationLevel.ReadCommitted;
            _commandBehavior = CommandBehavior.CloseConnection;
            _connStringName = strConnectionStringName;
            _provider = ProviderFactory.GetProvider(_connStringName);

        }

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        ~DataAccess()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }


        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.blndisposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                CloseConnection();
                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.
            }
            blndisposed = true;
        }



        private void PrepareAll(ref IDbCommand cmd, ref IDbConnection conn, string strSQL, CommandType cmdType, clsParameterCollection parameterArray)
        {
            if (!(IsInTransaction()))
            {
                conn = ProviderFactory.GetConnection(ConnectionStringName, Provider);
                cmd = ProviderFactory.GetCommand(strSQL, cmdType, CmdTimeout, parameterArray, Provider);
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                conn.Open();
            }
            else
            {
                cmd = ProviderFactory.GetCommand(strSQL, cmdType, CmdTimeout, parameterArray, Provider);
                cmd.Transaction = _trans;
                cmd.Connection = _conn;
                cmd.CommandTimeout = 0;
            }
        }

        public EnumProviders Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

        public string ConnectionStringName
        {
            get
            {
                return _connStringName;
            }
            set
            {
                _connStringName = value;
            }
        }

        public IsolationLevel TransIsolationLevel
        {
            get
            {
                return _isolationLevel;
            }
            set
            {
                _isolationLevel = value;
            }
        }

        public int CmdTimeout
        {
            get
            {
                if (_cmdTimeout == 0)
                {
                    return COMMAND_TIMEOUT;
                }
                return _cmdTimeout;
            }
            set
            {
                _cmdTimeout = value;
            }
        }

        public CommandBehavior ReaderCommandBehavior
        {
            get
            {
                return _commandBehavior;
            }
            set
            {
                _commandBehavior = value;
            }
        }

        public void BeginTrans(string connStringName, IsolationLevel transisolationLevel)
        {
            _conn = ProviderFactory.GetConnection(connStringName, Provider);
            _conn.Open();
            _trans = ProviderFactory.GetTransaction(_conn, transisolationLevel);
        }

        public void BeginTrans(IsolationLevel transisolationLevel)
        {
            _conn = ProviderFactory.GetConnection(_connStringName, Provider);
            _conn.Open();
            _trans = ProviderFactory.GetTransaction(_conn, transisolationLevel);
        }

        public void CommitTrans()
        {
            CommitTrans(true);
        }

        public void CommitTrans(bool CloseConnection)
        {
            _trans.Commit();
            DisposeTrans(CloseConnection);
        }

        public void AbortTrans()
        {
            if (IsInTransaction())
            {
                _trans.Rollback();
                DisposeTrans(true);
            }
        }

        public Boolean CloseConnection()
        {

            if (!(_conn == null))
            {
                _conn.Close();
                _conn.Dispose();
                _conn = null;
            }
            return true;
        }
        private void DisposeTrans(bool CloseConnection)
        {
            if (CloseConnection)
            {
                if (!(_conn == null))
                {
                    _conn.Close();
                    _conn.Dispose();
                    _conn = null;
                }
            }
            _trans.Dispose();
            _trans = null;
        }

        public bool IsInTransaction()
        {
            return (!(_trans == null));
        }

        public void ExecDataSet(DataSet ds, string strSQL, CommandType cmdtype)
        {
            ExecDataSet(ds, strSQL, cmdtype, null);
        }

        public DataSet ExecDataSet(string strSQL, CommandType cmdtype)
        {
            return ExecDataSet(strSQL, cmdtype, null);
        }

        public DataSet ExecDataSet(string strSQL, CommandType cmdtype, clsParameterCollection parameterArray)
        {
            DataSet ds = new DataSet("DataSet");
            ExecDataSet(ds, strSQL, cmdtype, parameterArray);
            return ds;

        }

        public void ExecDataSet(DataSet ds, string strSQL, CommandType cmdtype, clsParameterCollection parameterArray)
        {
            IDbDataAdapter da = null;
            IDbCommand cmd = null;
            IDbConnection conn = null;
            try
            {
                da = ProviderFactory.GetAdapter(Provider);
                //PrepareAll(ref IDbCommand cmd, ref IDbConnection conn, string strSQL, CommandType cmdType, clsParameterCollection parameterArray)
                PrepareAll(ref cmd, ref conn, strSQL, cmdtype, parameterArray);
                da.SelectCommand = cmd;
                da.Fill(ds);
                ParameterValues(cmd.Parameters, parameterArray);
            }
            catch (Exception ex)
            {
                GenericExceptionHandler(ex);
            }
            finally
            {
                if (!(IsInTransaction()))
                {
                    conn.Close();
                    conn.Dispose();
                }
                cmd.Dispose();
                ((IDisposable)(da)).Dispose();
            }
        }

        public IDataReader ExecDataReader(string strSQL, CommandType cmdtype, clsParameterCollection parameterArray)
        {
            IDbConnection conn = null;
            IDbCommand cmd = null;
            IDataReader IDataReader = null;
            try
            {
                PrepareAll(ref cmd, ref conn, strSQL, cmdtype, parameterArray);
                IDataReader = cmd.ExecuteReader(ReaderCommandBehavior);
                //ParameterValues(cmd.Parameters, parameterArray);
                ParameterValues(cmd.Parameters, parameterArray);
                return IDataReader;
            }
            catch (Exception ex)
            {
                if (!(IsInTransaction()))
                {
                    conn.Close();
                    conn.Dispose();
                }
                GenericExceptionHandler(ex);
            }
            finally
            {
                cmd.Dispose();
            }
            return null;
        }

        public IDataReader ExecDataReader(string strSQL, CommandType cmdtype)
        {
            return ExecDataReader(strSQL, cmdtype, null);
        }

        public int ExecNonQuery(string strSQL, CommandType cmdType)
        {
            return ExecNonQuery(strSQL, cmdType, null);
        }

        public int ExecNonQuery(string strSQL, CommandType cmdType, clsParameterCollection parameterArray)
        {
            IDbCommand cmd = null;
            IDbConnection conn = null;
            int alParams;
            try
            {
                PrepareAll(ref cmd, ref conn, strSQL, cmdType, parameterArray);
                cmd.CommandTimeout = 0;
                alParams = cmd.ExecuteNonQuery();
                ParameterValues(cmd.Parameters, parameterArray);
                return alParams;
            }
            catch (Exception ex)
            {
                GenericExceptionHandler(ex);
            }
            finally
            {
                if (!(IsInTransaction()))
                {
                    conn.Close();
                    conn.Dispose();
                }
                cmd.Dispose();
            }
            return -2;
        }

        public void SaveDataSet(DataSet ds, string insertSQL, string deleteSQL, string updateSQL, clsParameterCollection InsertparameterArray, clsParameterCollection DeleteparameterArray, clsParameterCollection UpdateparameterArray)
        {
            IDbConnection cn = null;
            IDbDataAdapter da = null;
            try
            {
                da = ProviderFactory.GetAdapter(Provider);
                if (!(IsInTransaction()))
                {
                    cn = ProviderFactory.GetConnection(ConnectionStringName, Provider);
                    if (insertSQL != "")
                    {
                        da.InsertCommand = ProviderFactory.GetCommand(insertSQL, CommandType.StoredProcedure, CmdTimeout, InsertparameterArray, Provider);
                        da.InsertCommand.Connection = cn;
                    }
                    if (updateSQL != "")
                    {
                        da.UpdateCommand = ProviderFactory.GetCommand(updateSQL, CommandType.StoredProcedure, CmdTimeout, UpdateparameterArray, Provider);
                        da.UpdateCommand.Connection = cn;
                    }
                    if (deleteSQL != "")
                    {
                        da.DeleteCommand = ProviderFactory.GetCommand(deleteSQL, CommandType.StoredProcedure, CmdTimeout, DeleteparameterArray, Provider);
                        da.DeleteCommand.Connection = cn;
                    }
                    cn.Open();
                }
                else
                {
                    if (insertSQL != "")
                    {
                        da.InsertCommand = ProviderFactory.GetCommand(insertSQL, CommandType.StoredProcedure, CmdTimeout, InsertparameterArray, Provider);
                        da.InsertCommand.Connection = _conn;
                        da.InsertCommand.Transaction = _trans;
                    }
                    if (updateSQL != "")
                    {
                        da.UpdateCommand = ProviderFactory.GetCommand(updateSQL, CommandType.StoredProcedure, CmdTimeout, UpdateparameterArray, Provider);
                        da.UpdateCommand.Connection = _conn;
                        da.UpdateCommand.Transaction = _trans;
                    }
                    if (deleteSQL != "")
                    {
                        da.DeleteCommand = ProviderFactory.GetCommand(deleteSQL, CommandType.StoredProcedure, CmdTimeout, DeleteparameterArray, Provider);
                        da.DeleteCommand.Connection = _conn;
                        da.DeleteCommand.Transaction = _trans;
                    }
                }
                da.Update(ds);
            }
            catch (Exception ex)
            {
                GenericExceptionHandler(ex);
            }
            finally
            {
                if (!(IsInTransaction()))
                {
                    cn.Close();
                    cn.Dispose();
                }
                if (insertSQL != "")
                {
                    da.InsertCommand.Parameters.Clear();
                    da.InsertCommand.Dispose();
                }
                if (updateSQL != "")
                {
                    da.UpdateCommand.Parameters.Clear();
                    da.UpdateCommand.Dispose();
                }
                if (deleteSQL != "")
                {
                    da.DeleteCommand.Parameters.Clear();
                    da.DeleteCommand.Dispose();
                }
                ((IDisposable)(da)).Dispose();
            }
        }

        public object ExecScalar(string strSQL, CommandType cmdtype, clsParameterCollection parameterArray)
        {
            IDbConnection conn = null;
            IDbCommand cmd = null;
            object objReturn = null;
            try
            {
                PrepareAll(ref cmd, ref conn, strSQL, cmdtype, parameterArray);
                cmd.CommandTimeout = 0;
                objReturn = cmd.ExecuteScalar();
                ParameterValues(cmd.Parameters, parameterArray);
            }
            catch (Exception ex)
            {
                GenericExceptionHandler(ex);
            }
            finally
            {
                if (!(IsInTransaction()))
                {
                    conn.Close();
                    conn.Dispose();
                }
                cmd.Dispose();
            }
            return objReturn;
        }

        public object ExecScalar(string strSQL, CommandType cmdtype)
        {
            return ExecScalar(strSQL, cmdtype, null);
        }

        public ArrayList ExecPreparedSQL(string strSQL, CommandType cmdtype, clsParameterCollection parameterArray)
        {
            IDbCommand cmd = null;
            IDbConnection conn = null;
            ArrayList alParams = new ArrayList();

            try
            {
                PrepareAll(ref cmd, ref conn, strSQL, cmdtype, parameterArray);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                ParameterValues(cmd.Parameters, parameterArray);

            }
            catch (Exception ex)
            {
                GenericExceptionHandler(ex);
            }
            finally
            {
                if (!(IsInTransaction()))
                {
                    conn.Close();
                    conn.Dispose();
                }
                cmd.Dispose();
            }
            return alParams;
        }

        private void GenericExceptionHandler(System.Exception ex)
        {
            if (ex is SqlException)
            {
                SQLExceptionHandler((SqlException)ex);
            }
            else if (ex is OleDbException)
            {
                OLEDBExceptionHandler((OleDbException)ex);
            }
            else if (ex is OdbcException)
            {
                ODBCExceptionHandler((OdbcException)ex);
            }
            else
            {
                throw ex;
            }
        }

        private void ParameterValues(System.Data.IDataParameterCollection objColParams, clsParameterCollection parameterArray)
        //
        {
            IDbDataParameter iParam;
            for (int iCount = 0; iCount < objColParams.Count; iCount++)
            {
                iParam = (IDbDataParameter)objColParams[iCount];
                if (iParam.Direction != ParameterDirection.Input)
                {
                    parameterArray[iCount].value = iParam.Value;
                }
            }
        }

        private ParamStruct cstr(IDbDataParameter iParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void SQLExceptionHandler(SqlException ex)
        {
            SqlError sqlerr;
            StringBuilder sb = new StringBuilder();
            int intCount;
            for (intCount = 0; intCount < ex.Errors.Count; intCount++)
            {
                sqlerr = ex.Errors[intCount];
                sb.AppendFormat("Error: {0}{1}", sqlerr.Message, Environment.NewLine);
                sb.AppendFormat("Server: {0}{1}", sqlerr.Server, Environment.NewLine);
                sb.AppendFormat("Source: {0}{1}", sqlerr.Source, Environment.NewLine);
                sb.Append("-----------------------------------------------");
            }
            throw new Exception(sb.ToString(), ex);
        }

        private void OLEDBExceptionHandler(OleDbException ex)
        {
            OleDbError oledberr;
            StringBuilder sb = new StringBuilder();
            int intCount;
            for (intCount = 0; intCount < ex.Errors.Count; intCount++)
            {
                oledberr = ex.Errors[intCount];
                sb.AppendFormat("Error: {0}{1}", oledberr.Message, Environment.NewLine);
                sb.AppendFormat("Source: {0}{1}", oledberr.Source, Environment.NewLine);
                sb.Append("-----------------------------------------------");
            }
            throw new Exception(sb.ToString(), ex);
        }

        private void ODBCExceptionHandler(OdbcException ex)
        {
            OdbcError odbcerr;
            StringBuilder sb = new StringBuilder();
            int intCount;
            for (intCount = 0; intCount < ex.Errors.Count; intCount++)
            {
                odbcerr = ex.Errors[intCount];
                sb.AppendFormat("Error: {0}{1}", odbcerr.Message, "\n");
                sb.AppendFormat("Source: {0}{1}", odbcerr.Source, "\n");
                sb.Append("-----------------------------------------------");
            }
            throw new Exception(sb.ToString(), ex);
        }
    }
}
