using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Safes.Models.Db
{
    public class StaticBox
    {
        [Key]
        public int Id { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }        // (Active : Inactive)
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
