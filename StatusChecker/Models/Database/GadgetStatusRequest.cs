﻿using System;

namespace StatusChecker.Models.Database
{
    public class GadgetStatusRequest : DbBaseModel
    {
        public int GadgetId { get; set; }
        public DateTime RequestDateTime { get; set; }
        public bool IsStatusRequestValid { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
    }
}
