using System;

namespace Safes.Models.Dto
{
    public class ReceivedReuseStaticDto
    {
        public int StaticBoxId { get; set; }
        public int Amount { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
    }
}
