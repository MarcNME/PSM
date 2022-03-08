using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace PSM_Libary.Connectors
{
    public class MySQLConnector : IDBConnector
    {
        private MySqlConnection _connection;
        string DbHost { get; set; }
        string DbUser { get; set; }
        string DbPassword { get; set; }
        string DbName { get; set; }

        private string ConnectionString
        {
            get
            {
                var stringBuilder = new MySqlConnectionStringBuilder
                {
                    Server = DbHost,
                    UserID = DbUser,
                    Password = DbPassword,
                    Database = DbName
                };

                return stringBuilder.ConnectionString;
            }
        }

        public MySQLConnector(string dbName)
        {
            DbName = dbName;
            DbHost = "localhost";
            DbUser = "root";
            DbPassword = "";
        }

        public MySQLConnector(string dbName, string dbHost, string dbUser, string dbPassword)
        {
            DbName = dbName;
            DbHost = dbHost;
            DbUser = dbUser;
            DbPassword = dbPassword;
        }

        private MySqlCommand OpenConnection()
        {
            _connection = new MySqlConnection(this.ConnectionString);
            _connection.Open();

            var mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = _connection;

            return mySqlCommand;
        }

        public override void CloseConnection()
        {
            _connection.Close();
        }

        public override bool TestConnection()
        {
            const string sql = "show tables;";
            try
            {
                var cmd = this.OpenConnection();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                this.CloseConnection();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override int ExecuteNonQuery(string dml)
        {
            var cmd = this.OpenConnection();
            cmd.CommandText = dml;
            var returnValue = cmd.ExecuteNonQuery();
            
            this.CloseConnection();

            return returnValue;
        }

        public override DbDataReader ExecuteQuery(string sql)
        {
            var cmd = this.OpenConnection();
            cmd.CommandText = sql;
            var reader = cmd.ExecuteReader();
            
            return reader;
        }
    }
}