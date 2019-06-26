using System;
using GranaPreta.Application.Interfaces;
using GranaPreta.Domain;

namespace GranaPreta.Application
{
    public class UserAppService : AppServiceBase<User>, IUserAppService
    {
        private readonly IUserService _appService;

        public UserAppService(IUserService appService) : base(appService)
        {
            _appService = appService;
        }

        public User GetByEmail(string email)
        {
            return _appService.GetByEmail(email);
        }
    }
}
