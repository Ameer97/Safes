using AutoMapper;
using Enjaz.Isp.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Safes.DAL.Contexts;
using Safes.Infrastructure.Enums;
using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Infrastructure.Interfaces.Services;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safes.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IEventService _eventService;
        private IMeditorService _meditorService;
        private IOwnerService _ownerService;
        private IUserService _userService;
        private IBoxService _boxService;
        private IStaticService _staticService;
        private IStatisticsService _statisticsService;
        private IThankService _thankService;
        private SafesDbContext _context;
        private IMapper _mapper;
        private readonly ISafesRepositoryWrapper _repositoryWrapper;

        public UserController(IBoxService boxService,
                              IUserService userService,
                              IStaticService staticService,
                              IOwnerService ownerService,
                              IMeditorService meditorService,
                              IThankService thankService,
                              IStatisticsService statisticsService,
                              IEventService eventService,
                              SafesDbContext context,
                              IMapper mapper,
                              ISafesRepositoryWrapper repositoryWrapper)
        {
            _eventService = eventService;
            _meditorService = meditorService;
            _ownerService = ownerService;
            _userService = userService;
            _boxService = boxService;
            _staticService = staticService;
            _statisticsService = statisticsService;
            _thankService = thankService;
            _context = context;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        #region Box
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<Box>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetBoxes(int? start, int? end)
        {
            var serviceResponse = await _boxService.GetBoxes(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<Box>>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<Box>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateBox([FromForm] int number)
        {
            var serviceResponse = await _boxService.CreateBoxRange(number);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<string>(serviceResponse.Value));
        }

        [HttpGet]
        public async Task<IActionResult> GetLastBoxId()
        {
            var Response = new { lastBoxId = await _GetLastBoxId() };
            return Ok(Response);
        }


        [HttpGet]
        public async Task<IActionResult> GetBoxesOf(int boxStatus)
        {
            var Response = new { BoxIds = await _GetBoxesOf(boxStatus) };
            return Ok(Response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<BoxDetailsDto>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetBox(int SearchId, bool IsBoxId = true)
        {
            var serviceResponse = await _boxService.BoxDetails(SearchId, IsBoxId);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<BoxDetailsDto>(serviceResponse.Value));
        }
        [HttpPut]
        [ProducesResponseType(typeof(ClientResponse<Box>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> UpdateReceivedBox(BoxUpdateReceivedDto box)
        {
            var serviceResponse = await _boxService.UpdateReceivedBox(box);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<Box>(serviceResponse.Value));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetIsReceivedOrNot(int BoxId)
        {
            var serviceResponse = await _boxService.IsReceived(BoxId);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<bool>(serviceResponse.Value));
        }

        #endregion
        #region StaticBox
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<StaticBoxReuse>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetStaticBoxes(int? start, int? end)
        {
            var serviceResponse = await _staticService.GetStaticBoxes(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<StaticBoxReuse>>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<StaticBoxAllReuseDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateStaticBox(StaticBoxCreateDto StaticBox)
        {
            var serviceResponse = await _staticService.CreateStaticBox(StaticBox);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<StaticBoxAllReuseDto>(serviceResponse.Value));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<StaticBoxAllReuseDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetStaticBoxDetails(int SBoxId)
        {
            var serviceResponse = await _staticService.GetStaticBoxDetails(SBoxId);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<StaticBoxAllReuseDto>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<StaticBoxAllReuseDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateReuseStatic(StaticBoxCreateDto StaticBox)
        {
            var serviceResponse = await _staticService.CreateReuseStatic(StaticBox);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<StaticBoxAllReuseDto>(serviceResponse.Value));
        }
        [HttpPut]
        [ProducesResponseType(typeof(ClientResponse<StaticBoxAllReuseDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> UpdateReceivedStaticBox(ReceivedReuseStaticDto ReceivedReuse)
        {
            var serviceResponse = await _staticService.UpdateReceivedReuseStatic(ReceivedReuse);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<StaticBoxAllReuseDto>(serviceResponse.Value));
        }
        #endregion
        #region Event
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<PlaceEvent>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetEvents(int? start, int? end)
        {
            var serviceResponse = await _eventService.GetEvents(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<PlaceEvent>>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<PlaceEvent>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateEvent(EventCreateDto Event)
        {
            var serviceResponse = await _eventService.CreateEvent(Event);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<PlaceEvent>(serviceResponse.Value));
        }
        #endregion
        #region User

        #endregion
        #region Owner
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<OwnerDto>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetOwners(int? start, int? end)
        {
            var serviceResponse = await _ownerService.GetOwners(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<OwnerDto>>(serviceResponse.Value));
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner(OwnerDto form)
        {
            var Owner = _mapper.Map<Owner>(form);
            Owner.DateCreated = DateTime.Now;

            var AlreadyOwner = await _context.Owners
                .Where(o => (o.Phone == form.Phone
                          || o.Name == form.Name)
                          && o.IsDeleted == false).FirstOrDefaultAsync();
            if (AlreadyOwner != null)
                return Ok(new ClientResponse<Owner>(AlreadyOwner, AlreadyExist("Owner"), true));

            _repositoryWrapper.OwnerRepository.Insert(Owner);
            return Ok(_SuccessfulResponse());
        }
        #endregion
        #region Meditor
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<Meditor>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetMeditors(int? start, int? end)
        {
            var serviceResponse = await _meditorService.GetMeditors(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<Meditor>>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<Meditor>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateMeditor(PersonCreateDto Meditor)
        {
            var serviceResponse = await _meditorService.CreateOwner(Meditor);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<Meditor>(serviceResponse.Value));
        }
        [HttpGet]
        public async Task<IActionResult> GetMeditorDetails(int meditorId)
        {
            var meditor = await _context.Meditors.Where(m => m.Id == meditorId).FirstOrDefaultAsync();

            var boxes = await _context.Boxes.Where(b => b.MeditorId == meditor.Id).ToListAsync();

            var count = boxes.Count();
            var groups = boxes.GroupBy(b => b.Status);
            var ret = groups.Select(g => new MeditorDetails
            {
                Type = ((BoxStatusEnum)g.Key).ToString(),
                Count = g.Count(),
                FirstDate = GetMinDate((BoxStatusEnum)g.Key, boxes.Where(b => b.Status == g.Key).ToList()),
                LastDate = GetMinDate((BoxStatusEnum)g.Key, boxes.Where(b => b.Status == g.Key).ToList(), false),
                Note = ((BoxStatusEnum)g.Key == BoxStatusEnum.Received)? boxes.Where(b => b.Status == g.Key).Select(b => (decimal)b.Amount).Sum().ToString() : null
            }).ToList();

            var MeditorDetails = new MeditorDetailsDto
            {
                TotalCount = count,
                DetailsList = ret
            };
            return Ok(new ClientResponse<MeditorDetailsDto>(MeditorDetails));
        }
        #endregion
        #region Statistics
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<BoxCountDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> BoxesCount(int? Year, bool? JustThisYear, bool? FromStartUntilYear)
        {
            var serviceResponse = await _statisticsService.BoxesCount(Year, JustThisYear, FromStartUntilYear);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<BoxCountDto>(serviceResponse.Value));
        }
        #endregion
        #region Thank
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<Thank>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateThank(ThankCreateDto input)
        {
            var serviceResponse = await _thankService.Create(input);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<Thank>(serviceResponse.Value));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<ThankCreateDto>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> Gethanks(int? start, int? end)
        {
            var serviceResponse = await _thankService.GetThanks(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<ThankCreateDto>>(serviceResponse.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<List<Thank>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateThanks(ThankCreateListDto input)
        {
            var serviceResponse = await _thankService.CreateList(input);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<Thank>>(serviceResponse.Value));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<DateTime?>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetLastThankDateToPerson(int ReferenceId, int ReferenceTypeId)
        {
            var serviceResponse = await _thankService.GetLastThankDateToPerson(ReferenceId, ReferenceTypeId);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<DateTime?>(serviceResponse.Value));
        }
        #endregion

        #region simple Pattren
        [HttpPost]
        public async Task<IActionResult> CreateBoxRange(CreateBoxRangeForm form)
        {
            var FirstNewBox = await _GetLastBoxId() + 1;
            var LastNewBox = FirstNewBox + form.NumberOfBoxes - 1;
            var BoxesListRange = new List<Box>();
            for (int BoxId = FirstNewBox; BoxId <= LastNewBox; BoxId++)
                BoxesListRange.Add(new Box
                {
                    BoxId = BoxId,
                    DateCreated = DateTime.Now,
                    Status = (int)BoxStatusEnum.Created
                });

            _repositoryWrapper.BoxRepository.InsertRange(BoxesListRange);

            var Response = form.NumberOfBoxes + " box(es) had been created from " + FirstNewBox + " to " + LastNewBox;
            //var service = new ServiceResponse<int>(form.NumberOfBoxes, Response);
            return Ok(new ClientResponse<int>(form.NumberOfBoxes, Response));
        }

        [HttpGet]
        public async Task<IActionResult> GetFromTo(int boxStatus)
        {
            var boxIds = await _context.Boxes.Where(b => b.Status == boxStatus).Select(b => b.BoxId).ToListAsync();

            if (boxIds.Count == 0)
                return Ok(_ErrorInvalidData(_empty));

            var Response = new FirstLastDto
            {
                From = boxIds.Min(),
                To = boxIds.Max()
            };
            return Ok(new ClientResponse<FirstLastDto>(Response));
        }


        [HttpPost]
        public async Task<IActionResult> AssignBoxesToMeditor(AssignBoxesToPersonDto form)
        {
            var CreatedBoxes = await _context.Boxes
                .Where(b => b.BoxId >= form.From
                && b.BoxId <= form.To
                && b.Status == (int)BoxStatusEnum.Created).ToListAsync();

            var meditor = await _repositoryWrapper.MeditorRepository.FindItemByCondition(m => m.Id == form.MeditorId);

            var RequiredCount = form.To - form.From + 1;
            if (CreatedBoxes.Count() < RequiredCount)
                return BadRequest(_ErrorInvalidData("some of boxes in this range can't Delivered to Meditor"));

            if (meditor == null)
                return BadRequest(_ErrorInvalidData("Meditor Not Exist"));

            foreach (var box in CreatedBoxes)
            {
                box.MeditorId = meditor.Id;
                box.Status = (int)BoxStatusEnum.DeliverdToMeditor;
                box.DateDeliverdToMeditor = form.Date;
                box.DateUpdated = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            var Response = form.From + " To " + form.To + " box(es) had been Assigned to " + _MeditorName(meditor);
            return Ok(_SuccessfulResponse(Response));
        }


        [HttpPost]
        public async Task<IActionResult> AssignSelectedBoxesToMeditor(AssignSelectedBoxesToMeditorDto form)
        {
            var CreatedBoxes = await _context.Boxes
                .Where(b => b.Status == (int)BoxStatusEnum.Created)
                .Where(b => form.SelectedBoxes.Contains(b.BoxId)).ToListAsync();

            var meditor = await _repositoryWrapper.MeditorRepository.FindItemByCondition(m => m.Id == form.MeditorId);

            var RequiredCount = form.SelectedBoxes.Count();
            if (CreatedBoxes.Count() < RequiredCount)
                return BadRequest(_ErrorInvalidData("some of boxes in this range can't Delivered to Meditor"));

            if (meditor == null)
                return BadRequest(_ErrorInvalidData("Meditor Not Exist"));

            foreach (var box in CreatedBoxes)
            {
                box.MeditorId = meditor.Id;
                box.Status = (int)BoxStatusEnum.DeliverdToMeditor;
                box.DateDeliverdToMeditor = form.Date;
                box.DateUpdated = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            var Response = RequiredCount + " box(es) had been Assigned to " + _MeditorName(meditor);
            return Ok(new ClientResponse<List<int>>(await _GetBoxesOf((int)BoxStatusEnum.Created), Response));
        }


        [HttpPost]
        public async Task<IActionResult> AssignSelectedBoxesToOwner(AssignSelectedBoxesToOwnerDto form)
        {
            var DeliveredToMeditorBoxes = await _context.Boxes
                .Where(b => b.Status == (int)BoxStatusEnum.DeliverdToMeditor)
                .Where(b => form.SelectedBoxes.Contains(b.BoxId)).ToListAsync();

            var owner = await _repositoryWrapper.OwnerRepository.FindItemByCondition(m => m.Id == form.OwnerId);

            var RequiredCount = form.SelectedBoxes.Count();
            if (DeliveredToMeditorBoxes.Count() < RequiredCount)
                return Ok(_ErrorInvalidData("some of boxes can't Delivered to Owner"));

            if (owner == null)
                return BadRequest(_ErrorInvalidData("Owner Not Exist"));

            foreach (var box in DeliveredToMeditorBoxes)
            {
                box.OwnerId = owner.Id;
                box.Status = (int)BoxStatusEnum.DeliverdToOwner;
                box.DateDeliverdToOwner = form.Date;
                box.DateUpdated = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            var Response = RequiredCount + " box(es) had been Assigned to " + owner.Name;
            return Ok(new ClientResponse<List<int>>(await _GetBoxesOf((int)BoxStatusEnum.DeliverdToMeditor), Response));
        }


        [HttpPost]
        public async Task<IActionResult> ReceiveBox(ReceiveBoxDto form)
        {
            var DeliveredToOwnerBox = await _context.Boxes
                .Where(b => b.BoxId == form.BoxId)
                .Where(b => b.Status == (int)BoxStatusEnum.DeliverdToOwner)
                .FirstOrDefaultAsync();

            if (DeliveredToOwnerBox == null)
                return Ok(_ErrorInvalidData("Can't Recive this Box, not exist, or not in this state"));

            DeliveredToOwnerBox.Note = form.Note;
            DeliveredToOwnerBox.Amount = form.Amount;
            DeliveredToOwnerBox.DateReceived = form.Date;
            DeliveredToOwnerBox.DateUpdated = DateTime.Now;
            DeliveredToOwnerBox.Status = (int)BoxStatusEnum.Received;

            await _context.SaveChangesAsync();
            var Response = "Box " + form.BoxId + " Recived with Amount: " + form.Amount + "IQD";
            return Ok(new ClientResponse<List<int>>(await _GetBoxesOf((int)BoxStatusEnum.DeliverdToOwner), Response));
        }

        #endregion


        #region functions
        private async Task<int> _GetLastBoxId()
        {
            var boxList = await _context.Boxes.Select(b => b.BoxId).ToListAsync();
            return (boxList.Count() <= 0) ? 0 : boxList.Max();
        }
        private DateTime GetMinDate(BoxStatusEnum boxStatus, List<Box> boxes, bool min = true)
        {
            var theBoxes = boxes.Where(b => b.Status == (int)boxStatus);
            if (min)
                switch (boxStatus)
                {
                    case BoxStatusEnum.Created: return theBoxes.Select(b => b.DateCreated).Min();
                    case BoxStatusEnum.DeliverdToMeditor: return theBoxes.Select(b => (DateTime)b.DateDeliverdToMeditor).Min();
                    case BoxStatusEnum.DeliverdToOwner: return theBoxes.Select(b => (DateTime)b.DateDeliverdToOwner).Min();
                    case BoxStatusEnum.Received: return theBoxes.Select(b => (DateTime)b.DateReceived).Min();
                }
            else
                switch (boxStatus)
                {
                    case BoxStatusEnum.Created: return theBoxes.Select(b => b.DateCreated).Max();
                    case BoxStatusEnum.DeliverdToMeditor: return theBoxes.Select(b => (DateTime)b.DateDeliverdToMeditor).Max();
                    case BoxStatusEnum.DeliverdToOwner: return theBoxes.Select(b => (DateTime)b.DateDeliverdToOwner).Max();
                    case BoxStatusEnum.Received: return theBoxes.Select(b => (DateTime)b.DateReceived).Max();
                }
            return DateTime.Now;
        }
        private async Task<List<int>> _GetBoxesOf(int boxStatus)
        {
            return await _context.Boxes
                .Where(b => b.Status == boxStatus)
                .Select(b => b.BoxId)
                .OrderBy(b => b).ToListAsync();
        }

        private string _MeditorName(Meditor meditor)
        {
            return meditor.FirstName + " " + meditor.SecondName + " " + meditor.LastName;
        }

        #endregion

        #region ResponsesMessage

        private ClientResponse<string> _ErrorInvalidData(string response = "Invalid Data")
        {
            return new ClientResponse<string>(null, response, true);
        }

        private ClientResponse<string> _SuccessfulResponse(string response = "Done")
        {
            return new ClientResponse<string>(null, response);
        }

        private string _empty = "Empty list";
        private string AlreadyExist(string something = "")
        {
            return something + " Already Exist";
        }
        #endregion
    }
}