using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<StaticBoxAllReuseDto> StaticBoxLog(int SBoxId)
        {
            return await _context.StaticBoxes.Where(s => s.SBoxId == SBoxId).Include(s => s.StaticBoxReuses)
                .Select(s => new StaticBoxAllReuseDto
                {
                    SBoxId = s.SBoxId,
                    DateCreated = s.DateCreated,
                    IsActive = s.IsActive,
                    StaticBoxesReuse = s.StaticBoxReuses.Select(r => new StaticBoxReuseForViewDto
                    {
                        Amount = r.Amount,
                        DateFrom = r.DateFrom,
                        DateTo = r.DateTo,
                        Note = r.Note,
                        Address = r.Address,
                        OwnerId = r.OwnerId,
                    }).ToList()
                }).FirstOrDefaultAsync();
        }
    }
}
