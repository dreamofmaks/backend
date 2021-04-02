using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace User.Data.DTO
{
    public class PersonDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public RegistrationPasswordDTO Password { get; set; }
        public string Token { get; set; }
        public int? AddressId { get; set; }
        public AddressDTO Address { get; set; }
        public ICollection<PasswordDTO> UserPasswords { get; set; }
    }
}
