using System.Data.Common;
using MySql.Data.MySqlClient;

namespace PSM_Libary.Connectors
{
    public abstract class IDBConnector
    {
        private MySqlConnection _connection;
        string DbHost { get; set; }
        string DbUser { get; set; }
        string DbPassword { get; set; }
        string DbName { get; set; }
        
        public abstract void CloseConnection();
        
        public abstract bool TestConnection();

        public abstract int ExecuteNonQuery(string dml);

        public abstract DbDataReader ExecuteQuery(string sql);
    }
}