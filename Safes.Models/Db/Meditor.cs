using Safes.Models.Db.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Safes.Models.Db
{
    public class Meditor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [DefaultValue(false)]
        public bool IsStopped { get; set; }
        public string Note { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
