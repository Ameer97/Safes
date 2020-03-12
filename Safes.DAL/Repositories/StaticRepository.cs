using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using Safes.Models.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace Safes.DAL.Repositories
{
    public class StaticRepository : RepositoryBase<StaticBox, SafesDbContext>, IStaticRepository
    {
        private SafesDbContext _context;
        public StaticRepository(SafesDbContext RepositoryContext)
            : base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
        public async Task<StaticBoxAllReuseDto> StaticBoxLog(int SBoxId, bool OnlyLastReuse = true)
        {
            var staticboxreuse = (OnlyLastReuse)
            ? await _context.StaticBoxReuses.Where(s => s.StaticBoxId == SBoxId)
                .Include(s => s.StaticBoxes).Include(s => s.Owner).Include(s => s.Meditor)
                .OrderByDescending(s => s.DateFrom).Take(1).ToListAsync()

            : await _context.StaticBoxReuses.Where(s => s.StaticBoxId == SBoxId)
                .Include(s => s.StaticBoxes).Include(s => s.Owner).Include(s => s.Meditor)
                .OrderByDescending(s => s.DateFrom).ToListAsync();

            return staticboxreuse.GroupBy(s => s.StaticBoxes).Select(s => new StaticBoxAllReuseDto
            {
                SBoxId = s.Key.SBoxId,
                DateCreated = s.Key.DateCreated,
                IsDisabled = s.Key.IsDisabled,
                StaticBoxesReuse = s.Select(r => new StaticBoxReuseForViewDto
                {
                    Amount = r.Amount,
                    DateFrom = r.DateFrom,
                    DateTo = r.DateTo,
                    Note = r.Note,
                    Address = r.Address,
                    OwnerId = r.OwnerId,
                    OwnerName = r.Owner.FirstName + " " + r.Owner.SecondName + r.Owner.LastName,
                    MeditorId = r.MeditorId,
                    MeditorName = r.Meditor.FirstName + " " + r.Meditor.SecondName + r.Meditor.LastName
                }).ToList()
            }).FirstOrDefault();

        }
        public async Task<StaticBox> FindSBox(int SBoxId)
        {
            return await _context.StaticBoxes.Where(b => b.SBoxId == SBoxId).FirstOrDefaultAsync();
        }
    }
}
