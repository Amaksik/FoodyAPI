﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodyAPI.Models
{
    public class Query
    {
        public Query(string q)
        {
            query = q;
        }
        public string query { get; set; }
        public string timezone = "US/Eastern";
    }
}
