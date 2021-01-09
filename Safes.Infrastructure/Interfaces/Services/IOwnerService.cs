using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IOwnerService : IDisposable
    {
        Task<ServiceResponse<Owner>> CreateOwner(OwnerDto form);
        Task<ServiceResponse<List<OwnerDto>>> GetOwners(int? start, int? end);
    }
}
