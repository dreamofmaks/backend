using System;
using System.Collections.Generic;

#nullable disable

namespace User.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int? Cityid { get; set; }
        public int? Countryid { get; set; }

        public virtual City City { get; set; }
        public virtual DCountry Country { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
