﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int Number { get; set; }

        public Coordinates Coordinates { get; set; }
    }
}
