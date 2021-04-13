using System;
using User.Data.Models;

namespace User.Data.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? AddressId { get; set; }
        public string Email { get; set; }
        public virtual AddressDTO Address { get; set; }
    }
}
