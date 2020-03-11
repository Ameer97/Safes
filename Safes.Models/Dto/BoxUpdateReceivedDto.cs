using System;

namespace Safes.Models.Dto
{
    public class BoxUpdateReceivedDto
    {
        public int BoxId { get; set; }
        public DateTime DateReceived { get; set; }
        public string Amount { get; set; }
        public string Note { get; set; }
    }
}
