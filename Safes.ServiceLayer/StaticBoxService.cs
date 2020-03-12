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
        public async Task<ServiceResponse<StaticBoxAllReuseDto>> CreateStaticBox(StaticBoxCreateDto form)
        {
            await _repositoryWrapper.StaticBoxReuseRepository.CreateStaticBox(form);
            return await GetStaticBoxDetails(form.StaticBoxId);
        }
        public async Task<ServiceResponse<StaticBoxAllReuseDto>> GetStaticBoxDetails(int SBoxId)
        {
            if (SBoxId <= 0)
                return new ServiceResponse<StaticBoxAllReuseDto>(default)
                {
                    Error = new ResponseError("invalid SBoxId")
                };

            var StaticBox = await _repositoryWrapper.StaticRepository.StaticBoxLog(SBoxId, false);
            return (StaticBox != null)
                ? new ServiceResponse<StaticBoxAllReuseDto>(StaticBox)
                : new ServiceResponse<StaticBoxAllReuseDto>(default)
                {
                    Error = new ResponseError("No Static Boxes Found")
                };
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
        public async Task<ServiceResponse<StaticBoxAllReuseDto>> CreateReuseStatic(StaticBoxCreateDto form)
        {
            if (form.StaticBoxId <= 0)
                return new ServiceResponse<StaticBoxAllReuseDto>(default)
                {
                    Error = new ResponseError("Invalid StaticBox")
                };
            var StaticBox = await _repositoryWrapper.StaticRepository.FindSBox(form.StaticBoxId);
            if (StaticBox == null)
                return new ServiceResponse<StaticBoxAllReuseDto>(default)
                {
                    Error = new ResponseError("StaticBox Not Found")
                };
            var SBoxId = form.StaticBoxId;
            form.StaticBoxId = StaticBox.Id;
            var StaticBoxReuse = _mapper.Map<StaticBoxCreateDto, StaticBoxReuse>(form);
            _repositoryWrapper.StaticBoxReuseRepository.Insert(StaticBoxReuse);

            var MyStaticBox = await _repositoryWrapper.StaticRepository.StaticBoxLog(SBoxId, true);
            return new ServiceResponse<StaticBoxAllReuseDto>(MyStaticBox);
        }
        public async Task<ServiceResponse<StaticBoxAllReuseDto>> UpdateReceivedReuseStatic(ReceivedReuseStaticDto ReceivedReuse)
        {
            if (ReceivedReuse.StaticBoxId <= 0)
                return new ServiceResponse<StaticBoxAllReuseDto>(default)
                {
                    Error = new ResponseError("Invalid StaticBoxId")
                };
            var StaticBox = await _repositoryWrapper.StaticBoxReuseRepository.FindSBox(ReceivedReuse.StaticBoxId);
            if (StaticBox == null)
                return new ServiceResponse<StaticBoxAllReuseDto>(default)
                {
                    Error = new ResponseError("Invalid StaticBox")
                };
            _mapper.Map(ReceivedReuse, StaticBox);
            StaticBox.DateUpdated = DateTime.Now;
            _repositoryWrapper.StaticBoxReuseRepository.Update(StaticBox);
            var MyStaticBox = await _repositoryWrapper.StaticRepository.StaticBoxLog(StaticBox.StaticBoxId, true);
            return new ServiceResponse<StaticBoxAllReuseDto>(MyStaticBox);
        }
    }
}
