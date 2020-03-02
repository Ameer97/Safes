using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Safes.Models.Db
{
    public class StaticBoxReuse
    {
        [Key]
        public int Id { get; set; }
        public int StaticBoxId { get; set; }
        [ForeignKey(nameof(StaticBoxId))]
        public StaticBoxes StaticBoxes { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Amount { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }
        public string Note { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
