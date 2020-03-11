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
    public class BoxRepository : RepositoryBase<Box,SafesDbContext>, IBoxRepository
    {
        private SafesDbContext _context;
        public BoxRepository(SafesDbContext RepositoryContext)
            :base(RepositoryContext)
        {
            _context = RepositoryContext;
        }
        public async Task<BoxDetailsDto> GetBoxDetails(int SearchId, bool IsBoxId = true)
        {
            return (IsBoxId)
                ? await _context.Boxes.Where(x => x.BoxId == SearchId)
                .Include(b => b.Event)
                .Include(b => b.Meditor)
                .Include(b => b.Owner)
                .Select(b => new BoxDetailsDto
                {
                    BoxId = b.BoxId,
                    MeditorName = b.Meditor.FirstName + " " + b.Meditor.SecondName + b.Meditor.LastName,
                    OwnerId = b.OwnerId,
                    OwnerName = b.Owner.FirstName + " " + b.Owner.SecondName + b.Owner.LastName,
                    EventName = b.Event.Name,
                    Note = b.Note,
                    DateDeliverd = b.DateDeliverd,
                    IsReceived = (b.DateReceived != null) ? true : false,
                    DateReceived = b.DateReceived,
                    Amount = b.Amount
                })
                .FirstOrDefaultAsync()
                : await _context.Boxes.Where(x => x.Id == SearchId)
                .Include(b => b.Event)
                .Include(b => b.Meditor)
                .Include(b => b.Owner)
                .Select(b => new BoxDetailsDto
                {
                    BoxId = b.BoxId,
                    MeditorName = b.Meditor.FirstName + " " + b.Meditor.SecondName + b.Meditor.LastName,
                    OwnerId = b.OwnerId,
                    OwnerName = b.Owner.FirstName + " " + b.Owner.SecondName + b.Owner.LastName,
                    EventName = b.Event.Name,
                    Note = b.Note,
                    DateDeliverd = b.DateDeliverd,
                    IsReceived = (b.DateReceived != null) ? true : false,
                    DateReceived = b.DateReceived,
                    Amount = b.Amount
                })
                .FirstOrDefaultAsync();

        }
    }
}
