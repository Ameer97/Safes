using System;
using System.Collections.Generic;

namespace Safes.Models.Dto
{
    public class StaticBoxAllReuseDto
    {
        public int SBoxId { get; set; }
        public bool IsDisabled { get; set; }        // (Active : Inactive)
        public DateTime DateCreated { get; set; }
        public List<StaticBoxReuseForViewDto> StaticBoxesReuse { get; set; }
    }
}
