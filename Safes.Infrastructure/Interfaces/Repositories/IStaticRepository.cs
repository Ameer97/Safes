using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Repositories
{
    public interface IStaticRepository : IRepositoryBase<StaticBox>
    {
        Task<StaticBoxAllReuseDto> StaticBoxLog(int SBoxId);
    }
}
