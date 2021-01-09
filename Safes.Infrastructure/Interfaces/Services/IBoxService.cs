using Enjaz.Isp.Infrastructure.Helpers;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Services
{
    public interface IBoxService : IDisposable
    {
        Task<ServiceResponse<List<Box>>> GetBoxes(int? start, int? end);
        //Task<ServiceResponse<Box>> CreateBox(BoxCreateDto form);
        Task<ServiceResponse<BoxDetailsDto>> BoxDetails(int SearchId, bool IsBoxId = true);
        Task<ServiceResponse<Box>> UpdateReceivedBox(BoxUpdateReceivedDto form);
        Task<ServiceResponse<bool>> IsReceived(int BoxId);
        Task<ServiceResponse<string>> AssignBoxesMeditor(AssignBoxesToPersonDto Boxes);
        Task<ServiceResponse<string>> CreateBoxRange(int number);
        Task<ServiceResponse<string>> AssignBoxToMeditor(BoxToPersonDto form);
        Task<ServiceResponse<string>> AssignBoxToOwner(BoxToPersonDto form);
        Task<ServiceResponse<int>> LastBoxId();
    }
}
