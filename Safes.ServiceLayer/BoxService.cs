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
            Box.DateCreated = DateTime.Now;
            _repositoryWrapper.BoxRepository.Insert(Box);
            return new ServiceResponse<Box>(Box);
        }
        public async Task<ServiceResponse<BoxDetailsDto>> BoxDetails(int SearchId, bool IsBoxId = true)
        {
            if (SearchId <= 0)
                return new ServiceResponse<BoxDetailsDto>(default)
                {
                    Error = new ResponseError("Invalid SearchId")
                };
            var BoxDetails = _repositoryWrapper.BoxRepository.GetBoxDetails(SearchId, IsBoxId).Result;
            return (BoxDetails != null)
                ? new ServiceResponse<BoxDetailsDto>(BoxDetails)
                : new ServiceResponse<BoxDetailsDto>(default)
                {
                    Error = new ResponseError("No Boxes Found")
                };
        }
        public async Task<ServiceResponse<Box>> UpdateReceivedBox(BoxUpdateReceivedDto form)
        {
            var Box = await _repositoryWrapper.BoxRepository.FindBoxId(form.BoxId);
            if (Box == null)
                return new ServiceResponse<Box>(default)
                {
                    Error = new ResponseError("Invalid SearchId")
                };
            _mapper.Map(form, Box);
            Box.DateUpdated = DateTime.Now;
            _repositoryWrapper.BoxRepository.Update(Box);
            return new ServiceResponse<Box>(Box);
        }
    }
}
