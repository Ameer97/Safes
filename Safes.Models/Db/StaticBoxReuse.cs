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
        public StaticBox StaticBoxes { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        [DefaultValue(0)]
        public decimal Amount { get; set; }

        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

        public int MeditorId { get; set; }
        [ForeignKey(nameof(MeditorId))]
        public Meditor Meditor { get; set; }

        public string Note { get; set; }
        public string Address { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
