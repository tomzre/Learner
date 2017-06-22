using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Learner.Services
{
    public class DbConnection
    {
        public string ConnectionString { get { return GetConnectionString(); } }

        public async Task<DataTable> ExecuteSelectedCommand(SqlConnection connection, CommandType cmdType, string cmdName)
        {
            SqlCommand cmd = null;
            DataTable table = new DataTable();
            if (connection.State == ConnectionState.Closed)
                await connection.OpenAsync();
            cmd = connection.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdName;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                connection.Close();
            }
            return table;
            }

        public DataTable ExecuteCommandWithParameters(SqlConnection connection, CommandType cmdType, string cmdName, SqlParameter[] parameters)
        {
            SqlCommand cmd = null;
            DataTable table = new DataTable();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdName;
            cmd.Parameters.AddRange(parameters);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                connection.Close();
            }
            return table;

        }
        
        

        private string GetConnectionString()
        {
            var connectionSB = new SqlConnectionStringBuilder();

            connectionSB.DataSource = "DESKTOP-RD4TJGS\\SQLEXPRESS";
            connectionSB.InitialCatalog = "AdventureWorks2012";
            connectionSB.IntegratedSecurity = true;
            connectionSB.Pooling = false;
            

            return connectionSB.ToString();
        }
    }
}