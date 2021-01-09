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
        public async Task<ServiceResponse<List<OwnerDto>>> GetOwners(int? start, int? end)
        {
            var Owners = _repositoryWrapper.OwnerRepository.FindAllTakeSkip(start, end).ToList();
            return (Owners.Any())
                ? new ServiceResponse<List<OwnerDto>>(_mapper.Map<List<OwnerDto>>(Owners))
                : new ServiceResponse<List<OwnerDto>>(null)
                {
                    Error = new ResponseError("No Owners Found")
                };
        }
        public async Task<ServiceResponse<Owner>> CreateOwner(OwnerDto form)
        {
            var Owner = _mapper.Map<Owner>(form);
            Owner.DateCreated = DateTime.Now;

            _repositoryWrapper.OwnerRepository.Insert(Owner);
            return new ServiceResponse<Owner>(Owner);
        }
    }
}
