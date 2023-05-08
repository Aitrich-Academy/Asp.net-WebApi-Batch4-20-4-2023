using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ResponsDataDTO
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponsDataDTO()
        {

        }
        public ResponsDataDTO(bool result, string message, object data)
        {
            Result = result;
            Message = message;
            Data = data;
        }
    }
}