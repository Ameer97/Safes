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

        public UserController(IBoxService boxService,
                              IUserService userService,
                              IStaticService staticService,
                              IOwnerService ownerService,
                              IMeditorService meditorService,
                              IEventService eventService)
        {
            _eventService = eventService;
            _meditorService = meditorService;
            _ownerService = ownerService;
            _userService = userService;
            _boxService = boxService;
            _staticService = staticService;
        }
        #region Box
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<Box>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetBoxes(int start, int end)
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
        #endregion
        #region Static

        #endregion
        #region Event

        #endregion
        #region User

        #endregion
        #region Owner
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<Box>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> GetOwners(int start, int end)
        {
            var serviceResponse = await _ownerService.GetOwners(start, end);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<List<Owner>>(serviceResponse.Value));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<Box>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        public async Task<IActionResult> CreateOwner(OwnerCreateDto Owner)
        {
            var serviceResponse = await _ownerService.CreateOwner(Owner);
            if (serviceResponse.Error != null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.Error.Message));
            return Ok(new ClientResponse<Owner>(serviceResponse.Value));
        }
        #endregion
        #region Meditor

        #endregion
    }
}