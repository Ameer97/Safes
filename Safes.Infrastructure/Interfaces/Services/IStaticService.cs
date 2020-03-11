using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IStaticService : IDisposable
    {
        Task<ServiceResponse<List<StaticBoxReuse>>> GetStaticBoxes(int? start, int? end);
        Task<ServiceResponse<StaticBoxAllReuseDto>> GetStaticBoxDetails(int SBoxId);
        Task<ServiceResponse<StaticBoxAllReuseDto>> CreateStaticBox(StaticBoxCreateDto form);
    }
}
