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
    public class MeditorService : IMeditorService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private bool _disposed;

        public MeditorService(ISafesRepositoryWrapper repositoryWrapper,
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
        public async Task<ServiceResponse<List<Meditor>>> GetMeditors(int? start, int? end)
        {
            var Meditors = _repositoryWrapper.MeditorRepository.FindAllTakeSkip(start,end).ToList();
            return (Meditors.Any())
                ? new ServiceResponse<List<Meditor>>(Meditors)
                : new ServiceResponse<List<Meditor>>(null)
                {
                    Error = new ResponseError("No Owners Found")
                };
        }
        public async Task<ServiceResponse<Meditor>> CreateOwner(PersonCreateDto form)
        {
            var Meditor = _mapper.Map<Meditor>(form);
            _repositoryWrapper.MeditorRepository.Insert(Meditor);
            return new ServiceResponse<Meditor>(Meditor);
        }
    }
}
