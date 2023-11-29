using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcomDemo.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string returnUrl { get; set; }
    }
}