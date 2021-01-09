using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Enums;
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
        public async Task<Box> FindBoxId(int BoxId)
        {
            return await _context.Boxes.Where(b => b.BoxId == BoxId).FirstOrDefaultAsync();
        }
        public async Task<int> LastBoxId()
        {
            return await _context.Boxes.Select(b => b.BoxId).MaxAsync();
        }

        public async Task<List<Box>> SpecialStatusBoxes(List<int> boxes, BoxStatusEnum boxStatus)
        {
            return await _context.Boxes
                .Where(b => b.Status == (int)boxStatus
                         && boxes.Contains(b.BoxId)).ToListAsync();
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
                    MeditorName = b.Meditor.FirstName + " " + b.Meditor.SecondName + " " + b.Meditor.LastName,
                    BoxStatus = b.Status,
                    OwnerName = b.Owner.Name,
                    EventName = b.Event.Name,
                    Note = b.Note,
                    DateDeliverdToMeditor = b.DateDeliverdToMeditor.Value.Date,
                    DateDeliverdToOwner = b.DateDeliverdToOwner.Value.Date,
                    DateCreated = b.DateCreated,
                    DateReceived = b.DateReceived.Value.Date,
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
                    BoxStatus = b.Status,
                    OwnerName = b.Owner.Name,
                    EventName = b.Event.Name,
                    Note = b.Note,
                    DateDeliverdToMeditor = b.DateDeliverdToMeditor.Value.Date,
                    DateDeliverdToOwner = b.DateDeliverdToOwner.Value.Date,
                    DateCreated = b.DateCreated,
                    DateReceived = b.DateReceived.Value.Date,
                    Amount = b.Amount
                })
                .FirstOrDefaultAsync();

        }

        #region Statistics
        public async Task<BoxCountDto> BoxCount(int? Year = 0, bool? JustThisYear = false, bool? FromStartUntilYear = false)
        {                                                                             //FromStartToYear
            // 0 true true = all
            // 
            var ActiveBoxes = ((Year ?? 0) == 0) 
                ? await _context.Boxes.Where(b => b.IsDeleted == false).ToListAsync()
                : (JustThisYear ?? false) 
                    ? await _context.Boxes.Where(b => b.IsDeleted == false && b.DateDeliverdToMeditor.Value.Year == Year).ToListAsync()
                    : (FromStartUntilYear ?? false)
                        ? await _context.Boxes.Where(b => b.IsDeleted == false && b.DateDeliverdToMeditor.Value.Year <= Year).ToListAsync()
                        : await _context.Boxes.Where(b => b.IsDeleted == false && b.DateDeliverdToMeditor.Value.Year >= Year).ToListAsync();

            var Delived = ActiveBoxes.Where(b => b.Amount == null).ToList().Count;
            var Received = ActiveBoxes.Where(b => b.Amount != null).ToList().Count;
            var Late = ActiveBoxes.Where(b => DateTime.Now.Subtract(b.DateDeliverdToMeditor.Value).Days >= 365).ToList().Count;
            var TooLate = ActiveBoxes.Where(b => DateTime.Now.Subtract(b.DateDeliverdToMeditor.Value).Days >= 730).ToList().Count;

            var BoxesCount = new BoxCountDto
            {
                Total = ActiveBoxes.Count,
                Delivered = Delived,
                Received = Received,
                Late = Late - TooLate,
                TooLate = TooLate
            };
            return BoxesCount;
        
        }
        #endregion
    }
}
