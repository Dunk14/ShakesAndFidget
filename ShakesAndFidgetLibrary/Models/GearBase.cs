﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public abstract class GearBase : ModelBase
    {
        public String Name { get; set; }
        public String ImageSource { get; set; }
    }
}