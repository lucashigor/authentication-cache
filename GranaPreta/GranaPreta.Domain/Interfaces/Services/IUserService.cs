using System;

namespace GranaPreta.Domain
{
    public interface IUserService: IServiceBase<User>
    {
        User GetByEmail(string email);
    }
}
