using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Repositories
{
    public interface IBoxRepository : IRepositoryBase<Box>
    {
        Task<BoxDetailsDto> GetBoxDetails(int SearchId, bool IsBoxId = true);
        Task<Box> FindBoxId(int BoxId);
        Task<BoxCountDto> BoxCount(int? Year = 0, bool? JustThisYear = false, bool? FromStartUntilYear = false);

    }
}
