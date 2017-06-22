using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Learner.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;

namespace Learner.Services
{
    
    public class UserService : IUserService
    {
        private DbConnection _dbCon;
        public UserService()
        {
            _dbCon = new DbConnection();
        }
        public void Delete(User user)
        {
           
            
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(_dbCon.ConnectionString))
            {
                CommandType cmd = CommandType.Text;
                string commandName = "SELECT TOP 10 WITH TIES p.[FirstName] + ' ' + p.[LastName] as FullName, e.[EmailAddress], p.[BusinessEntityID] as Id" +
                    " FROM[Person].[Person] p INNER JOIN[Person].[EmailAddress] e ON p.[BusinessEntityID] = e.[BusinessEntityID] ORDER BY[LastName]";

                DataTable table = await _dbCon.ExecuteSelectedCommand(connection, cmd, commandName);


                IEnumerable<DataRow>dataTable = table.AsEnumerable();
                var users = dataTable
                    .Select(x => new User() { Name = x["FullName"].ToString(), Email = (string)x["EmailAddress"], Id = (int)x["Id"] });


                return users;

            }
        }

        public User GetUser(int id)
        {
            using(SqlConnection connection = new SqlConnection(_dbCon.ConnectionString))
            {
                CommandType cmd = CommandType.Text;
                string commandName = @"SELECT p.[FirstName] + ' ' + p.[LastName] as FullName, p.[BusinessEntityID], e.EmailAddress
                                       FROM [Person].[Person] p LEFT JOIN [Person].[EmailAddress] e ON p.BusinessEntityID = e.BusinessEntityID
                                       WHERE p.BusinessEntityID = @id";
                SqlParameter[] paramList = new SqlParameter[1];
                SqlParameter param = new SqlParameter("@id", id);
                paramList[0] = param;
                DataTable table = _dbCon.ExecuteCommandWithParameters(connection, cmd, commandName, paramList);
                // var rawDataUser = table.AsEnumerable().FirstOrDefault(u => (int)u["FullName"] == id);
                var dataTable = table.AsEnumerable();
                var user = dataTable
                    .Select(x => new User() { Name = x["FullName"].ToString(), Id= (int)x["BusinessEntityID"], Email = (string)x["EmailAddress"] }).FirstOrDefault(u => u.Id == id);

                return user;
            }
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}