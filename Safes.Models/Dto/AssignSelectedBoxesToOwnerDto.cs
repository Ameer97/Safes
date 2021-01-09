using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Safes.Models.Dto
{
    public class AssignSelectedBoxesToOwnerDto
    {
        [Required]
        public List<int> SelectedBoxes { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
