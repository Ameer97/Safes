using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Safes.Models.Db
{
    public class Box
    {
        [Key]
        public int Id { get; set; }
        public int BoxId { get; set; }
        public int MeditorId { get; set; }
        [ForeignKey(nameof(MeditorId))]
        public Meditor Meditor { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }
        public int? EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public PlaceEvent Event { get; set; }
        public DateTime DateDeliverd { get; set; }
        public DateTime? DateReceived { get; set; }
        public int? Amount { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
