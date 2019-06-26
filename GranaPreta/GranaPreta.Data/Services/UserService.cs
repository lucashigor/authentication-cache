using System;
using GranaPreta.Domain;

namespace GranaPreta.Data.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _serviceRepository;

        public UserService(IUserRepository serviceRepository) : base(serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public User GetByEmail(string email)
        {
            return _serviceRepository.GetByEmail(email);
        }
    }
}
