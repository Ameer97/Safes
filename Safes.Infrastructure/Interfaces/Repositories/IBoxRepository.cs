using Safes.Models.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Repositories
{
    public interface IBoxRepository : IRepositoryBase<Box>
    {
        Task<List<Box>> GetBoxes(int start, int end);
    }
}
