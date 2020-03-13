using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Dto;
using System;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IStatisticsService : IDisposable
    {
        Task<ServiceResponse<BoxCountDto>> BoxesCount(int? Year, bool? JustThisYear, bool? FromStartUntilYear);
    }
}
