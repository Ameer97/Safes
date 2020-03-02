using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Safes.Models.Db
{
    public class PlaceEvent
    {
        [Key]
        public int Id { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
