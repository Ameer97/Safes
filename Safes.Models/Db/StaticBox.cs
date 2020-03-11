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
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public StaticBox()
        {
            if (this.DateCreated.Year <= 2000 || this.DateCreated == null)
                this.DateCreated = DateTime.Now;
            else
                this.DateUpdated = DateTime.Now;
        }
    }
}
