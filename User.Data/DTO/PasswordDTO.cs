using System;
using System.Collections.Generic;
using System.Text;

namespace User.Data.DTO
{
    public class PasswordDTO
    {
        public int? Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int? UserId { get; set; }
        public PersonDTO User { get; set; }
    }
}
