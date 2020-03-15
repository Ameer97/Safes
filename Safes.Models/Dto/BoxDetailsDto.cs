using System;

namespace Safes.Models.Dto
{
    public class BoxDetailsDto
    {
        public int BoxId { get; set; }
        public string MeditorName { get; set; }
        public string OwnerName { get; set; }
        public int? OwnerId { get; set; }
        public string EventName { get; set; }
        public DateTime DateDeliverd { get; set; }
        public bool IsReceived { get; set; }
        public DateTime? DateReceived { get; set; }
        public int? Amount { get; set; }
        public string Note { get; set; }
    }
}
