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
    public class ThankService : IThankService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private bool _disposed;

        public ThankService(ISafesRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ServiceResponse<Thank>> Create(ThankCreateDto input)
        {
            var Thank = new Thank
            {
                ReferenceId = input.ReferenceId,
                ReferenceType = input.ReferenceTypeId,
                DateDeliverd = input.DateDeliverd,
                Note = input.Note,
                DateCreated = DateTime.Now,
            };
            _repositoryWrapper.ThankRepository.Insert(Thank);
            return new ServiceResponse<Thank>(Thank);
        }
        public async Task<ServiceResponse<List<Thank>>> CreateList(ThankCreateListDto input)
        {
            var Thanks = new List<Thank>();
            foreach (var Id in input.ReferenceIds)
            {
                Thanks.Add(new Thank
                {
                    ReferenceId = Id,
                    ReferenceType = input.ReferenceTypeId,
                    DateDeliverd = input.DateDeliverd,
                    Note = input.Note,
                    DateCreated = DateTime.Now,
                });
                    
            }
            
            _repositoryWrapper.ThankRepository.InsertRange(Thanks);
            return new ServiceResponse<List<Thank>>(Thanks);
        }
        public async Task<ServiceResponse<List<ThankCreateDto>>> GetThanks(int? start, int? end)
        {
            var Thanks = _repositoryWrapper.ThankRepository.FindAllTakeSkip(start, end).ToList()
                .Select(t => new ThankCreateDto
                {
                    DateDeliverd = t.DateDeliverd,
                    Note = t.Note,
                    ReferenceId = t.ReferenceId,
                    ReferenceTypeId = t.ReferenceType,
                    ReferenceType = t.ReferenceType.ToString()
                }).ToList();
            return (Thanks.Any())
                ? new ServiceResponse<List<ThankCreateDto>>(Thanks)
                : new ServiceResponse<List<ThankCreateDto>>(null)
                {
                    Error = new ResponseError("No Thank Exist")
                };
        }
        public async Task<ServiceResponse<DateTime?>> GetLastThankDateToPerson(int ReferenceId, int ReferenceTypeId)
        {
            var DateTime = _repositoryWrapper.ThankRepository.FindAll()
                .Where(t => t.ReferenceId == ReferenceId && t.ReferenceType == ReferenceTypeId)
                .Select(t => t.DateDeliverd).ToList();



            return (DateTime.Any())
                ? new ServiceResponse<DateTime?>(DateTime.Max())
                : new ServiceResponse<DateTime?>(null)
                {
                    Error = new ResponseError("No Thank Exist to this Person")
                };
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
    }
}
