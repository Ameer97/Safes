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
            var boxes = await _repositoryWrapper.BoxRepository.SpecialStatusBoxes(form.BoxIds, BoxStatus.Created);

            if (boxes.Count != form.BoxIds.Count)
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("Invalid boxes")
                };
            foreach (var box in boxes)
            {
                box.MeditorId = meditor.Id;
                box.Status = (int)BoxStatus.DeliverdToMeditor;
                box.DateDeliverdToMeditor = new DateTime();
                box.DateUpdated = new DateTime();
            }

            return new ServiceResponse<string>("successfully Assigned");
        }

        public async Task<ServiceResponse<string>> AssignBoxToOwner(BoxToPersonDto form)
        {
            var owner = await _repositoryWrapper.OwnerRepository.FindItemByCondition(m => m.Id == form.PersonId);
            var boxes = await _repositoryWrapper.BoxRepository.SpecialStatusBoxes(form.BoxIds, BoxStatus.DeliverdToMeditor);

            if (boxes.Count != form.BoxIds.Count)
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("Invalid boxes")
                };
            foreach (var box in boxes)
            {
                box.OwnerId = owner.Id;
                box.Status = (int)BoxStatus.DeliverdToMeditor;
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
        public async Task<ServiceResponse<string>> AssignBoxesoMeditor(AssignBoxesToMeditorDto Boxes)
        {
            if (!Boxes.End.HasValue)
                Boxes.End = Boxes.Begain;

            if (Boxes.End < Boxes.Begain)
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("end must be greater than begain")
                };
            var Meditor = _repositoryWrapper.MeditorRepository.FindItemByCondition(m => m.Id == Boxes.MeditorId);
            if (Meditor.Result == null)
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("No Meditor Found")
                };
            var BoxesRangeInDb = _repositoryWrapper.BoxRepository.FindByCondition(b => b.BoxId >= Boxes.Begain && b.BoxId <= Boxes.End).Select(b => b.BoxId).ToList();
            if (BoxesRangeInDb.Any())
                return new ServiceResponse<string>(default)
                {
                    Error = new ResponseError("You Already have these box(es) in Db : " + string.Join(" | ", BoxesRangeInDb))
                };
            var BoxesListRange = new List<Box>();
            for (int BoxId = Boxes.Begain; BoxId <= Boxes.End; BoxId++)
            {
                BoxesListRange.Add(
                    new Box
                    {
                        BoxId = BoxId,
                        MeditorId = Boxes.MeditorId,
                        DateCreated = DateTime.Now,
                        DateDeliverdToMeditor = DateTime.Now,
                        EventId = Boxes.EventId
                    }
                );

            }
            _repositoryWrapper.BoxRepository.InsertRange(BoxesListRange);
            return new ServiceResponse<string>((Boxes.End - Boxes.Begain + 1) + " box(es) had been added to meditor "
                + Meditor.Result.FirstName + " from " + Boxes.Begain + " until " + Boxes.End);
        }

    }
}
