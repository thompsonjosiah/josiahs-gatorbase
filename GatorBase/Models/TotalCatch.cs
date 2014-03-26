using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GatorBase.Models
{
    public class TotalCatch
    {
        public int id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public decimal totalLength { get; set; }
        public decimal totalMass { get; set; }
    }
}