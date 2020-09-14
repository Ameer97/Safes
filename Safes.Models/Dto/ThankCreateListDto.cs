using System;
using System.Collections.Generic;

namespace Safes.Models.Dto
{
    public class ThankCreateListDto
    {
        public List<int> ReferenceIds { get; set; }
        public int ReferenceTypeId { get; set; }
        public DateTime DateDeliverd { get; set; }
        public string Note { get; set; }
    }
}
