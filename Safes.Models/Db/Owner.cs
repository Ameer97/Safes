using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Safes.Models.Db.Helper;

namespace Safes.Models.Db
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
