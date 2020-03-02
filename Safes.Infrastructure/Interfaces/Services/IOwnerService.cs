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
        Task<ServiceResponse<Owner>> CreateOwner(PersonCreateDto form);
        Task<ServiceResponse<List<Owner>>> GetOwners(int start, int end);
    }
}
