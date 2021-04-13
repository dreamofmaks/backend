using System;
using System.Collections.Generic;
using System.Text;

namespace User.Data.DTO
{
    public class QueryParamsDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SortBy { get; set; }
        public string Order { get; set; }

    }
}
