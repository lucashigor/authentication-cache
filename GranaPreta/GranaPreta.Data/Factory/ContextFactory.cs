using System;
using GranaPreta.Domain.Interfaces.Factory;
using Microsoft.Extensions.Configuration;

namespace GranaPreta.Data.Factory
{
    public class ContextFactory: IContextFactory
    {
        private readonly IConfiguration _configuration;

        public ContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string _context;

        public string GetContext()
        {
            return _context ?? (_context = _configuration.GetConnectionString("Restaurants"));
        }
    }
}
