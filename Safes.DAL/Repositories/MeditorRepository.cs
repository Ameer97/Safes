using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;

namespace Safes.DAL.Repositories
{
    public class MeditorRepository : RepositoryBase<Meditor, SafesDbContext>, IMeditorRepository
    {
        private SafesDbContext _context;
        public MeditorRepository(SafesDbContext RepositoryContext)
            : base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
    }
}
