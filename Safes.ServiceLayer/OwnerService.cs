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
    public class OwnerService : IOwnerService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private bool _disposed;

        public OwnerService(ISafesRepositoryWrapper repositoryWrapper,
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
        public async Task<ServiceResponse<List<Owner>>> GetOwners(int? start, int? end)
        {
            var Owners = _repositoryWrapper.OwnerRepository.FindAllTakeSkip(start, end).ToList();
            return (Owners.Any())
                ? new ServiceResponse<List<Owner>>(Owners)
                : new ServiceResponse<List<Owner>>(null)
                {
                    Error = new ResponseError("No Owners Found")
                };
        }
        public async Task<ServiceResponse<Owner>> CreateOwner(OwnerCreateDto form)
        {
            var Owner = _mapper.Map<Owner>(form);
            _repositoryWrapper.OwnerRepository.Insert(Owner);
            return new ServiceResponse<Owner>(Owner);
        }
    }
}
