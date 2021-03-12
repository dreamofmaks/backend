using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using User.Data.Model;

namespace User.Data.DTO
{
    public class AddressDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    }
}
