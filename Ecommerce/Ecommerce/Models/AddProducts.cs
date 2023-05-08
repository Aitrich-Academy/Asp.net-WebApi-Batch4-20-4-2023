using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class AddProducts
    {
        public int id { get; set; }
        public string name { get; set; }
        public int categoryId { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdByDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }
        public int price { get; set; }
    }
}