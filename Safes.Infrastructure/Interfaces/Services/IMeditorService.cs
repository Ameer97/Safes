using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IMeditorService : IDisposable
    {
        Task<ServiceResponse<List<Meditor>>> GetMeditors(int start, int end);
        Task<ServiceResponse<Meditor>> CreateOwner(PersonCreateDto form);
    }
}
