using System;
using System.Collections.Generic;
using System.Text;

namespace User.Data.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressDTO Address { get; set; }

    }
}
