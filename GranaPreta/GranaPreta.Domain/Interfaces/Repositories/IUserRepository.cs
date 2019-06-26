using System;

namespace GranaPreta.Domain
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetByEmail(string email);
    }
}
