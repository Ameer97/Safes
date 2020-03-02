using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Owner>> GetOwners(int start, int end)
        {
            return await _context.Owners.Skip(start).Take(end).ToListAsync();
        }
        //public async Task<Owner> GetOwnerDetails(int Id)
        //{
        //    return await _context.Owners.Where(o ).FirstOrDefaultAsync();
        //}
    }
}
