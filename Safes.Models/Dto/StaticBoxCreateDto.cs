using System;

namespace Safes.Models.Dto
{
    public class StaticBoxCreateDto
    {
        public int StaticBoxId { get; set; }
        public int MeditorId { get; set; }
        public int OwnerId { get; set; }
        public string? Note { get; set; }
        public string? Address { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
