using System;

namespace Safes.Models.Dto
{
    public class StaticBoxReuseDto
    {
        public int StaticBoxId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Amount { get; set; }
        public int MeditorId { get; set; }
        public int OwnerId { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
    }
}
