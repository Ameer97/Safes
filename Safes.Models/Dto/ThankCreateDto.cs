using System;

namespace Safes.Models.Dto
{
    public class ThankCreateDto
    {
        public int ReferenceId { get; set; }
        public int ReferenceTypeId { get; set; }
        public string ReferenceType { get; set; }
        public DateTime DateDeliverd { get; set; }
        public string Note { get; set; }
    }
}
