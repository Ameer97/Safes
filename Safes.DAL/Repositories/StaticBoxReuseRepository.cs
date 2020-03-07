using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Safes.DAL.Abstraction;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Models.Db;
using Safes.Models.Dto;
using System.Collections.Generic;
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
        public async Task<StaticBoxReuse> CreateStaticBox(StaticBoxCreateDto form)
        {
            var StaticBox = new StaticBox();
            await _context.StaticBoxes.AddAsync(StaticBox);
            var StaticBoxReuse = await CreateStaticBoxReuse(form, StaticBox.Id);

            return StaticBoxReuse;
        }
        public async Task<StaticBoxReuse> CreateStaticBoxReuse(StaticBoxCreateDto form, int StaticBoxId)
        {
            var StaticBoxReuse = _mapper.Map<StaticBoxReuse>(form);
            StaticBoxReuse.StaticBoxId = StaticBoxId;
            await _context.StaticBoxReuses.AddAsync(StaticBoxReuse);

            return StaticBoxReuse;
        }
    }
}
