using System;
using System.Collections.Generic;

#nullable disable

namespace User.Data.Models
{
    public partial class UserPassword : IEntity
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int UserId { get; set; }
        public virtual Person User { get; set; }
    }
}
