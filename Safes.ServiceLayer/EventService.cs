using AutoMapper;
using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Infrastructure.Interfaces.Services;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safes.ServiceLayer
{
    public class EventService : IEventService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private bool _disposed;

        public EventService(ISafesRepositoryWrapper repositoryWrapper,
                            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _repositoryWrapper.Dispose();
            _disposed = true;
        }
        public async Task<ServiceResponse<List<PlaceEvent>>> GetEvents(int? start, int? end)
        {
            var Events = _repositoryWrapper.EventRepository.FindAllTakeSkip(start, end).ToList();
            return (Events.Any())
                ? new ServiceResponse<List<PlaceEvent>>(Events)
                : new ServiceResponse<List<PlaceEvent>>(null)
                {
                    Error = new ResponseError("No Events Found")
                };
        }
        public async Task<ServiceResponse<PlaceEvent>> CreateEvent(EventCreateDto form)
        {
            var Event = _mapper.Map<PlaceEvent>(form);
            _repositoryWrapper.EventRepository.Insert(Event);
            return new ServiceResponse<PlaceEvent>(Event);
        }
    }
}
