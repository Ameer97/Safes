using System;
using System.ComponentModel.DataAnnotations;

namespace Safes.Models.Dto
{
    public class ReceiveBoxDto
    {
        [Required]
        public int BoxId { get; set; }
        [Required]
        public uint Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
