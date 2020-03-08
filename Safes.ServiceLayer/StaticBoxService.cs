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
    public class StaticBoxService : IStaticService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private bool _disposed;

        public StaticBoxService(ISafesRepositoryWrapper repositoryWrapper,
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
        //public async Task<ServiceResponse<List<Meditor>>> GetMeditors(int start, int end)
        //{
        //    var Meditors = _repositoryWrapper.MeditorRepository.FindAll().Skip(start).Take(end).ToList();
        //    return (Meditors.Any())
        //        ? new ServiceResponse<List<Meditor>>(Meditors)
        //        : new ServiceResponse<List<Meditor>>(null)
        //        {
        //            Error = new ResponseError("No Owners Found")
        //        };
        //}
        public async Task<ServiceResponse<StaticBoxReuse>> CreateStaticBox(StaticBoxCreateDto form)
        {
            var StaticBox = await _repositoryWrapper.StaticBoxReuseRepository.CreateStaticBox(form);
            return new ServiceResponse<StaticBoxReuse>(StaticBox);
        }
        public async Task<ServiceResponse<List<StaticBoxReuse>>> GetStaticBoxes(int? start, int? end)
        {
            var StaticBox = _repositoryWrapper.StaticBoxReuseRepository.FindAllTakeSkip(start, end).ToList();
            return (StaticBox.Any())
                ? new ServiceResponse<List<StaticBoxReuse>>(StaticBox)
                : new ServiceResponse<List<StaticBoxReuse>>(default)
                {
                    Error = new ResponseError("No Static Boxes Found")
                };
        }
    }
}
