using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.Models.Dto
{
    public class BoxCreateDto
    {
        public int BoxId { get; set; }
        public int MeditorId { get; set; }
        public int OwnerId { get; set; }
        public int EventId { get; set; }
        public DateTime DateDeliverd { get; set; }
        public string Note { get; set; }
    }
}
