using System;
using System.Collections.Generic;

#nullable disable

namespace User.Data.Model
{
    public partial class Person : IEntity
    {
        public Person()
        {
            Passwords = new HashSet<Password>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? AddressId { get; set; }
        public string Email { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Password> Passwords { get; set; }
    }
}
