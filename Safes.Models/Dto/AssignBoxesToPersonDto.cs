using System;
using System.ComponentModel.DataAnnotations;

namespace Safes.Models.Dto
{
    public class AssignBoxesToPersonDto
    {
        [Required]
        public int From { get; set; }
        [Required]
        public int To { get; set; }
        [Required]
        public int MeditorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
