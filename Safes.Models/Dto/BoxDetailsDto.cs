﻿using System;

namespace Safes.Models.Dto
{
    public class BoxDetailsDto
    {
        public int BoxId { get; set; }
        public string MeditorName { get; set; }
        public string OwnerName { get; set; }
        public int BoxStatus { get; set; }
        public string EventName { get; set; }
        public DateTime? DateDeliverdToMeditor { get; set; }
        public DateTime? DateDeliverdToOwner { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateReceived { get; set; }
        public uint? Amount { get; set; }
        public string Note { get; set; }
    }
}
