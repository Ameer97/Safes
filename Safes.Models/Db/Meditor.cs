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
        public int BirthYear { get; set; }
        public bool IsMale { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [DefaultValue(false)]
        public bool IsStopped { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public Meditor()
        {
            if (this.DateCreated.Year <= 2000 || this.DateCreated == null)
                this.DateCreated = DateTime.Now;
            else
                this.DateUpdated = DateTime.Now;
        }
    }
}
