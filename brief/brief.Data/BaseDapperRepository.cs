namespace brief.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Library.Helpers;

    public class BaseDapperRepository
    {
        public static string ConnectionString;
        protected SqlConnection Connection => _connection ?? (_connection = GetOpenConnection());
        private SqlConnection _connection;

        public BaseDapperRepository(string connectionString)
        {
            Guard.AssertNotNull(connectionString, nameof(connectionString));

            ConnectionString = connectionString;
        }

        public static SqlConnection GetOpenConnection(bool mars = false)
        {
            var cs = ConnectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            if (conn.State != ConnectionState.Closed)
            {
                throw new InvalidOperationException("Connection should be closed!");
            }

            return conn;
        }
    }
}
