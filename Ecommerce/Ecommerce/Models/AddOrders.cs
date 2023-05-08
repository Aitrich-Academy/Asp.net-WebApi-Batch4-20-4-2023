using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class AddOrders
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }

        public int totalprice { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdByDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }
    }
}