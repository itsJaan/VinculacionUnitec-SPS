﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.ProjectHours
{
    public class CreateProjectHourDto
    {
        public int Hours { get; set; }
        public int TableState { get; set; }
        public int Student { get; set; }
        public int Section { get; set; }
        public int Project { get; set; }
    }
}
