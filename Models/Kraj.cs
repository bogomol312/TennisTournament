﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class Kraj
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public List<Kraj> Countries { get; } = new List<Kraj>
        {
   
        };

    }
}
