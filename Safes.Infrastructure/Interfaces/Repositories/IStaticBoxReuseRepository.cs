using Safes.Models.Db;
using Safes.Models.Dto;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Repositories
{
    public interface IStaticBoxReuseRepository : IRepositoryBase<StaticBoxReuse>
    {
        Task CreateStaticBox(StaticBoxCreateDto form);
    }
}
