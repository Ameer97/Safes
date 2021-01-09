using System;

namespace Safes.Models.Dto
{
    public class MeditorDetails
    {
        public int Count { get; set; }
        public string Type { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public string Note { get; set; }
    }
}
