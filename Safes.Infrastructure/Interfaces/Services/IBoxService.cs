using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IBoxService : IDisposable
    {
        Task<ServiceResponse<List<Box>>> GetBoxes(int? start, int? end);
        Task<ServiceResponse<Box>> CreateBox(BoxCreateDto form);
    }
}
