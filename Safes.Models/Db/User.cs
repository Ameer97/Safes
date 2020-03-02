using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Safes.Models.Db.Helper;

namespace Safes.Models.Db
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public bool IsMale { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [DefaultValue(false)]
        public bool IsStopped { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
        public string Note { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
