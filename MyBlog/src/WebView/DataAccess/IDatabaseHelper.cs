using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.DataAccess
{
    public interface IDatabaseHelper
    {
        IEnumerable<T> Query<T>(string sqlCommand, object parameters);
        IEnumerable<object> Query(string sqlCommand, object parameters, Type elementsType);
        int Execute(string sqlCommand, object parameters);
        void Commit();
        void Rollback();

        SqlConnection GetConnection();
        void CloseConnection(SqlConnection sqlConnection);
    }
}