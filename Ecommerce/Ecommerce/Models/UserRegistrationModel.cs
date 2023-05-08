using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class UserRegistrationModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string image { get; set; }
        public string Role { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }
    }
}