using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using System.Threading.Tasks;

namespace Safes.DAL.Repositories
{
    public class ThankRepository : RepositoryBase<Thank, SafesDbContext>, IThankRepository
    {
        private readonly SafesDbContext _context;
        public ThankRepository(SafesDbContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }
    }
}
