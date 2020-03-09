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
    public class BoxService : IBoxService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private bool _disposed;

        public BoxService(ISafesRepositoryWrapper repositoryWrapper,
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
        public async Task<ServiceResponse<List<Box>>> GetBoxes(int? start, int? end)
        {
            var Boxes = _repositoryWrapper.BoxRepository.FindAllTakeSkip(start, end).ToList();
            return (Boxes.Any())
                ? new ServiceResponse<List<Box>>(Boxes)
                : new ServiceResponse<List<Box>>(default)
                {
                    Error = new ResponseError("No Boxes Found")
                };
        }
        public async Task<ServiceResponse<Box>> CreateBox(BoxCreateDto form)
        {
            var Box = _mapper.Map<Box>(form);
             _repositoryWrapper.BoxRepository.Insert(Box);
            return new ServiceResponse<Box>(Box);
                //? 
                //: new ServiceResponse<Box>(null)
                //{
                //    Error = new ResponseError("No Boxes Found")
                //};
        }
    }
}
