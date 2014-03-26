using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class HunterAddressDM
    {
        public int id { get; set; }
        public int hunterId { get; set; }
        public int addressId { get; set; }
        public int typeId { get; set; }
    }
}