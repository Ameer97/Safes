using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;

namespace Safes.DAL.Repositories
{
    public class StaticBoxReuseRepository : RepositoryBase<StaticBoxReuse, SafesDbContext>, IStaticBoxReuseRepository
    {
        private SafesDbContext _context;
        public StaticBoxReuseRepository(SafesDbContext RepositoryContext)
            : base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
    }
}
