using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Safes.Models.Db.Helper
{
    public class Person
    {
        //[Timestamp]
        //[Key]
        //public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string SecondName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual bool IsMale { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual bool IsStopped { get; set; }
        [DefaultValue(false)]
        public virtual bool IsDeleted { get; set; }

    }
}
