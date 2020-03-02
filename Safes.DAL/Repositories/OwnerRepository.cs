using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;

namespace Safes.DAL.Repositories
{
    public class OwnerRepository : RepositoryBase<Owner, SafesDbContext>, IOwnerRepository
    {
        private SafesDbContext _context;
        public OwnerRepository(SafesDbContext RepositoryContext)
            : base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
    }
}
