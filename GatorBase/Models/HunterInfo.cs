using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GatorBase.Models
{
    public class HunterInfo
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "email is required")]
        public string email { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        //Type
        public string addressType { get; set; }
        //Phone
        public string number { get; set; }
        //Type
        public string phoneType { get; set; }
    }
}