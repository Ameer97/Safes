using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User,SafesDbContext>, IUserRepository
    {
        private readonly SafesDbContext _context;
        public UserRepository(SafesDbContext repositoryContext) 
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }
    }
}
