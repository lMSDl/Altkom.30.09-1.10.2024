﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public int Id {get; set;}
        public string? Name {get; set;}

        public string? Description { get; set;}

        public Address? Address {get; set;}
    }
}
