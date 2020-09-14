using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Safes.Models.Db
{
    public class Thank
    {
        public int Id { get; set; }
        public int ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public DateTime DateDeliverd { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
