﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskPhoto
    {

        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; } // Add Date property
        public TimeSpan Time { get; set; } // Add Time property
    }
}
