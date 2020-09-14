using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IThankService : IDisposable
    {
        Task<ServiceResponse<Thank>> Create(ThankCreateDto input);
        Task<ServiceResponse<List<ThankCreateDto>>> GetThanks(int? start, int? end);
        Task<ServiceResponse<List<Thank>>> CreateList(ThankCreateListDto input);
        Task<ServiceResponse<DateTime?>> GetLastThankDateToPerson(int ReferenceId, int ReferenceTypeId);
    }
}
