using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GatorBase.Models
{
    public class Gator
    {
        public int id { get; set; }
        public string lastName { get; set; }
        public string  firstName { get; set; }
        public decimal length { get; set; }
        public decimal mass { get; set; }
    }
}