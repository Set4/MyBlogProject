using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess
{
    public static class DataAccess
    {
        public static DataAccessConfiguration Config = new DataAccessConfiguration();

        private static IDatabaseHelper _dbHelper;

        public static IDatabaseHelper DatabaseHelper
        {
            get
            {
                if (_dbHelper == null)
                    _dbHelper = new DatabaseHelper(false);
                return _dbHelper;
            }
        }
    }

    public class DataAccessConfiguration
    {
        /// <summary>
        /// server host IP:PORT (PORT is optional)
        /// </summary>
        public string DBSource { get; set; }

        public string DBUserName { get; set; }

        public string DBPassword { get; set; }

        public string DBInitialCatalog { get; set; }
        
        /// <summary>
        /// Do you want to read commited data only? Default value is "ReadCommitted"
        /// </summary>
        public IsolationLevel DataIsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
    }
}
