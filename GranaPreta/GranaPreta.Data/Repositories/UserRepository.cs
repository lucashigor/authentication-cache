using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using GranaPreta.Domain;
using GranaPreta.Domain.Interfaces.Factory;

namespace GranaPreta.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IContextFactory factory) : base(factory)
        {
        }

        public User GetByEmail(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EMAIL", email);

            using (var db = new SqlConnection(ConnectionString))
            {
                var user = db.Query<User>("Proc_SelectUserByEmail",
                param: parameters,
                commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                return user;
            }
        }
    }
}
