using System.ComponentModel.DataAnnotations;

namespace Safes.Models.Dto
{
    public class OwnerDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"([+]\d{1,3})?(\d{10,11})")]
        public string Phone { get; set; }
    }
}
