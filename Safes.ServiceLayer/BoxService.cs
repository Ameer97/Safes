using AutoMapper;
using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Infrastructure.Enums;
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

        public async Task<ServiceResponse<string>> AssignBoxToMeditor(BoxToPersonDto form)
        {
            var meditor = await _repositoryWrapper.MeditorRepository.FindItemByCondition(m => m.Id == form.PersonId);
            var boxes = await _repositoryWrapper.BoxRepository.SpecialStatusBoxes(form.BoxIds, BoxStatusEnum.Created);

            if (boxes.Count != form.BoxIds.Count)
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("Invalid boxes")
                };
            foreach (var box in boxes)
            {
                box.MeditorId = meditor.Id;
                box.Status = (int)BoxStatusEnum.DeliverdToMeditor;
                box.DateDeliverdToMeditor = new DateTime();
                box.DateUpdated = new DateTime();
            }

            return new ServiceResponse<string>("successfully Assigned");
        }

        //public async Task<ServiceResponse<string>> AssignBoxesToMeditor(AssignBoxesToMeditorDto form)
        //{
        //    //var meditor = await _repositoryWrapper.MeditorRepository.FindItemByCondition(m => m.Id == form.PersonId);
        //    //var boxes = await _repositoryWrapper.BoxRepository.SpecialStatusBoxes(form.BoxIds, BoxStatusEnum.Created);

        //    //if (boxes.Count != form.BoxIds.Count)
        //    //    return new ServiceResponse<string>(default)
        //    //    {
        //    //        Error = new ResponseError("Invalid boxes")
        //    //    };
        //    //foreach (var box in boxes)
        //    //{
        //    //    box.MeditorId = meditor.Id;
        //    //    box.Status = (int)BoxStatusEnum.DeliverdToMeditor;
        //    //    box.DateDeliverdToMeditor = new DateTime();
        //    //    box.DateUpdated = new DateTime();
        //    //}

        //    return new ServiceResponse<string>("successfully Assigned");
        //}


        public async Task<ServiceResponse<string>> AssignBoxToOwner(BoxToPersonDto form)
        {
            var owner = await _repositoryWrapper.OwnerRepository.FindItemByCondition(m => m.Id == form.PersonId);
            var boxes = await _repositoryWrapper.BoxRepository.SpecialStatusBoxes(form.BoxIds, BoxStatusEnum.DeliverdToMeditor);

            if (boxes.Count != form.BoxIds.Count)
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("Invalid boxes")
                };
            foreach (var box in boxes)
            {
                box.OwnerId = owner.Id;
                box.Status = (int)BoxStatusEnum.DeliverdToMeditor;
                box.DateDeliverdToOwner = new DateTime();
                box.DateUpdated = new DateTime();
            }

            return new ServiceResponse<string>("successfully Assigned");
        }

        public async Task<ServiceResponse<int>> LastBoxId()
        {
            return new ServiceResponse<int>(await _repositoryWrapper.BoxRepository.LastBoxId());
        }

        public async Task<ServiceResponse<string>> CreateBoxRange(int number)
        {
            var lastBoxId = await _repositoryWrapper.BoxRepository.LastBoxId();
            var boxes = new List<Box>();
            for (int i = 1; i <= number; i++)
                boxes.Add(new Box { BoxId = lastBoxId + i });
            _repositoryWrapper.BoxRepository.InsertRange(boxes);
            return new ServiceResponse<string>("successfully adding " + number + " box(es)");
        }
        public async Task<ServiceResponse<BoxDetailsDto>> BoxDetails(int SearchId, bool IsBoxId = true)
        {
            if (SearchId <= 0)
                return new ServiceResponse<BoxDetailsDto>(default)
                {
                    Error = new ResponseError("Invalid SearchId")
                };
            var BoxDetails = await _repositoryWrapper.BoxRepository.GetBoxDetails(SearchId, IsBoxId);
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
        public async Task<ServiceResponse<bool>> IsReceived(int BoxId)
        {
            if (BoxId <= 0)
                return new ServiceResponse<bool>(default)
                {
                    Error = new ResponseError("Invalid BoxId")
                };

            var Box = await _repositoryWrapper.BoxRepository.FindBoxId(BoxId);
            if (Box == null)
                return new ServiceResponse<bool>(default)
                {
                    Error = new ResponseError("Box Not Found")
                };
            return (Box.Amount.HasValue)
                ? new ServiceResponse<bool>(true)
                : new ServiceResponse<bool>(false);
        }
        public async Task<ServiceResponse<string>> AssignBoxesoMeditor(int NumberOfBoxes)
        {
            var FirstNewBox = await _repositoryWrapper.BoxRepository.LastBoxId() + 1;
            var LastNewBox = FirstNewBox + NumberOfBoxes;
            var BoxesListRange = new List<Box>();
            for (int BoxId = FirstNewBox; BoxId <= LastNewBox; BoxId++)
            {
                BoxesListRange.Add(
                    new Box
                    {
                        BoxId = BoxId,
                    }
                );

            }
            _repositoryWrapper.BoxRepository.InsertRange(BoxesListRange);
            return new ServiceResponse<string>(NumberOfBoxes + " box(es) had been created from "
                + FirstNewBox + " to " + LastNewBox);
        }

        public Task<ServiceResponse<string>> AssignBoxesMeditor(AssignBoxesToPersonDto Boxes)
        {
            throw new NotImplementedException();
        }
    }
}
