using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Safes.Models.Db
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public Permission()
        {
            if (this.DateCreated.Year <= 2000 || this.DateCreated == null)
                this.DateCreated = DateTime.Now;
            else
                this.DateUpdated = DateTime.Now;
        }
    }
}
