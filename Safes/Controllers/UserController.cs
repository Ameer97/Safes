using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enjaz.Isp.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Safes.Infrastructure.Interfaces.Services;
using Safes.Models.Db;
using Safes.Models.Dto;

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

        public UserController(IBoxService boxService,
                              IUserService userService,
                              IStaticService staticService,
                              IOwnerService ownerService,
                              IMeditorService meditorService,
                              IStatisticsService statisticsService,
                              IEventService eventService)
        {
            _eventService = eventService;
            _meditorService = meditorService;
            _ownerService = ownerService;
            _userService = userService;
            _boxService = boxService;
            _staticService = staticService;
            _statisticsService = statisticsService;
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
        public async Task<IActionResult> CreateBox(BoxCreateDto box)
        {
            var serviceResponse = await _boxService.CreateBox(box);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<Box>(serviceResponse.Value));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<BoxDetailsDto>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> BoxDetails(int SearchId, bool IsBoxId = true)
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
        [ProducesResponseType(typeof(ClientResponse<List<Box>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetOwners(int? start, int? end)
        {
            var serviceResponse = await _ownerService.GetOwners(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<Owner>>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<Box>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateOwner(PersonCreateDto Owner)
        {
            var serviceResponse = await _ownerService.CreateOwner(Owner);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<Owner>(serviceResponse.Value));
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
    }
}