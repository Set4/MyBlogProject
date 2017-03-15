using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.DataAccess
{
    /// <summary>
    /// Provide methods for quering to databases
    /// </summary>
    public class DatabaseHelper : IDatabaseHelper, IDisposable
    {
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;

        private bool useTransaction;

        /// <summary>
        /// If it's true, all execute cmd's to SQL server will be written only to file
        /// </summary>
        public static bool WriteUpdatesToFile = false;
        private static object _locker = new object();

        public DatabaseHelper(bool UseTransaction)
        {
            useTransaction = UseTransaction;
        }

        public SqlConnection GetConnection()
        {

            if (useTransaction)
            {
                if (sqlConnection == null)
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = DataAccess.Config.DBSource;
                    builder.Password = DataAccess.Config.DBPassword;
                    builder.UserID = DataAccess.Config.DBUserName;
                    builder.MultipleActiveResultSets = true;
                    builder.MaxPoolSize = 100000;
                    builder.ConnectTimeout = 60;
                    builder.InitialCatalog = DataAccess.Config.DBInitialCatalog;

                    sqlConnection = new SqlConnection(builder.ToString());
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction(DataAccess.Config.DataIsolationLevel);

                }
                return sqlConnection;
            }
            else
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = DataAccess.Config.DBSource;
                builder.Password = DataAccess.Config.DBPassword;
                builder.UserID = DataAccess.Config.DBUserName;
                builder.MultipleActiveResultSets = true;
                builder.MaxPoolSize = 100000;

                var connection = new SqlConnection(builder.ToString());
                connection.Open();
                return connection;
            }
        }
        public void CloseConnection(SqlConnection sqlConnection)
        {
            if (!useTransaction)
                sqlConnection.Close();
        }
        public IEnumerable<T> Query<T>(string sqlCommand, object parameters)
        {
            var connection = GetConnection();
            var cmd = GetCMD(sqlCommand, parameters);
            var result = connection.Query<T>(cmd);
            CloseConnection(connection);
            return result;
        }
        public IEnumerable<object> Query(string sqlCommand, object parameters, Type elementsType)
        {
            var connection = GetConnection();
            var cmd = GetCMD(sqlCommand, parameters);
            var result = connection.Query(elementsType, sql: cmd.CommandText, param: parameters, transaction: cmd.Transaction);
            CloseConnection(connection);
            return result;
        }
        public int Execute(string sqlCommand, object parameters)
        {
            if (!WriteUpdatesToFile)
            {
                var connection = GetConnection();
                var cmd = GetCMD(sqlCommand, parameters);
                var result = connection.Execute(cmd);
                CloseConnection(connection);
                return result;
            }
            else // write into the file
            {
                lock (_locker)
                {
                    System.IO.File.AppendAllLines("sql_executed.txt",
                        new string[] { $"{DateTime.Now.ToString()}\t{sqlCommand}\t{Newtonsoft.Json.JsonConvert.SerializeObject(parameters)}" });
                }
                return -1;
            }
        }

        public void Commit()
        {
            if (this.sqlTransaction != null)
                this.sqlTransaction.Commit();

            this.sqlConnection.Close();
            this.sqlConnection.Dispose();
        }

        public void Rollback()
        {
            if (this.sqlTransaction != null)
                this.sqlTransaction.Rollback();

            this.sqlConnection.Close();
            this.sqlConnection.Dispose();
        }

        private CommandDefinition GetCMD(string sql, object inputObj)
        {
            return new CommandDefinition(sql, inputObj, sqlTransaction);
        }

        public void Dispose()
        {
            if (useTransaction)
            {
                this.sqlConnection.Close();
            }
        }


        public static string GetFilds(Type classType)
        {
            StringBuilder srtBilder = new StringBuilder();
            FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.GetProperty);
            foreach (var field in fields)
                srtBilder.Append(" ["+field.Name+"] ");

           return srtBilder.ToString();
        }
    }

}
