using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Safes.Models.Dto
{
    public class AssignSelectedBoxesToMeditorDto
    {
        [Required]
        public List<int> SelectedBoxes { get; set; }
        [Required]
        public int MeditorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
