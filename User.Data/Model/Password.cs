using System;
using System.Collections.Generic;

#nullable disable

namespace User.Data.Model
{
    public partial class Password : IEntity
    {
        public int Id { get; set; }
        public string Password1 { get; set; }
        public string Salt { get; set; }
        public int? UserId { get; set; }

        public virtual Person User { get; set; }
    }
}
