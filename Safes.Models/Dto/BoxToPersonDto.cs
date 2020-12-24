using System.Collections.Generic;

namespace Safes.Models.Dto
{
    public class BoxToPersonDto
    {
        public List<int> BoxIds { get; set; }
        public int PersonId { get; set; }
    }
}
