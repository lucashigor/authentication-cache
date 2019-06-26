using System;
using GranaPreta.Domain;

namespace GranaPreta.Application.Interfaces
{
    public interface IUserAppService : IAppServiceBase<User>
    {
        User GetByEmail(string email);
    }
}
