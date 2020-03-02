using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safes.DAL.Repositories
{
    public class BoxRepository : RepositoryBase<Box,SafesDbContext>, IBoxRepository
    {
        private SafesDbContext _context;
        public BoxRepository(SafesDbContext RepositoryContext)
            :base(RepositoryContext)
        {
            _context = RepositoryContext;
        }

        public async Task<List<Box>> GetBoxes(int start, int end)
        {
            return await _context.Boxes.Skip(start).Take(end).ToListAsync();
        }
        public async Task<Box> GetBoxeDetails(int SearchId, bool IsBoxId = true)
        {
            var SearchIdType = (IsBoxId)
                ? typeof(Box).GetProperty("BoxId")
                : typeof(Box).GetProperty("Id");

            return await _context.Boxes.Where(x => (int)SearchIdType.GetValue(x, null) == SearchId).FirstOrDefaultAsync();
        }
    }
}
