using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class HunterInfoVM
    {
        public int id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string addressType { get; set; }
        public string number { get; set; }
        public string phoneType { get; set; }
    }
}