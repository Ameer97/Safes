using System;

namespace Safes.Models.Dto
{
    public class StaticBoxReuseForViewDto : WithOwnerMeditorIdNameDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? Amount { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
    }
}
