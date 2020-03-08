using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IEventService : IDisposable
    {
        Task<ServiceResponse<List<PlaceEvent>>> GetEvents(int start, int end);
        Task<ServiceResponse<PlaceEvent>> CreateEvent(EventCreateDto form);
    }
}
