using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Safes.DAL.Repositories
{
    public class StaticBoxReuseRepository : RepositoryBase<StaticBoxReuse, SafesDbContext>, IStaticBoxReuseRepository
    {
        private SafesDbContext _context;
        private IMapper _mapper;

        public StaticBoxReuseRepository(SafesDbContext RepositoryContext)
            : base(RepositoryContext)
        {
            _context = RepositoryContext;

        }
        public async Task CreateStaticBox(StaticBoxCreateDto form)
        {
            var StaticBox = new StaticBox
            {
                SBoxId = form.StaticBoxId,
                DateCreated = DateTime.Now
            };
            _context.StaticBoxes.Add(StaticBox);
            _context.SaveChanges();

            form.StaticBoxId = StaticBox.Id;
            await CreateStaticBoxReuse(form);
        }
        public async Task CreateStaticBoxReuse(StaticBoxCreateDto form)
        {
            var StaticBoxReuse = new StaticBoxReuse
            {
                StaticBoxId = form.StaticBoxId,
                DateFrom = form.DateFrom,
                MeditorId = form.MeditorId,
                OwnerId = form.OwnerId,
                Address = form.Address,
                Note = form.Note,
                DateCreated = DateTime.Now
            };
            _context.StaticBoxReuses.Add(StaticBoxReuse);
            _context.SaveChanges();

        }
        public async Task<StaticBoxReuse> FindSBox(int SBoxId)
        {
            return await _context.StaticBoxReuses.Where(b => b.StaticBoxId == SBoxId).OrderByDescending(s => s.DateFrom).FirstOrDefaultAsync();
        }
    }
}
