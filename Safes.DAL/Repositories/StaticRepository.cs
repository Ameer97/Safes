using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.DAL.Repositories
{
    public class StaticRepository : RepositoryBase<StaticBox,SafesDbContext>, IStaticRepository
    {
        private SafesDbContext _context;
        public StaticRepository(SafesDbContext RepositoryContext)
            :base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
    }
}
