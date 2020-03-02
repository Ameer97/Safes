using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;

namespace Safes.DAL.Repositories
{
    public class EventRepository : RepositoryBase<PlaceEvent, SafesDbContext>, IEventRepository
    {
        private SafesDbContext _context;
        public EventRepository(SafesDbContext RepositoryContext)
            : base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
    }
}
