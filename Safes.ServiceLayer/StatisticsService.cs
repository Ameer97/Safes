using AutoMapper;
using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Infrastructure.Interfaces.Services;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Threading.Tasks;

namespace Safes.ServiceLayer
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private bool _disposed;

        public StatisticsService(ISafesRepositoryWrapper repositoryWrapper,
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
        public async Task<ServiceResponse<BoxCountDto>> BoxesCount(int? Year, bool? JustThisYear, bool? FromStartUntilYear)
        {
            var Result = await _repositoryWrapper.BoxRepository.BoxCount(Year, JustThisYear, FromStartUntilYear);
            return (Result != null)
                ? new ServiceResponse<BoxCountDto>(Result)
                : new ServiceResponse<BoxCountDto>(default)
                {
                    Error = new ResponseError("No Data Found")
                };
        }


    }
}
