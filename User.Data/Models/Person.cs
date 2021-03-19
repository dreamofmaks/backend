using System;
using System.Collections.Generic;

#nullable disable

namespace User.Data.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Addressid { get; set; }

        public virtual Address Address { get; set; }
    }
}
