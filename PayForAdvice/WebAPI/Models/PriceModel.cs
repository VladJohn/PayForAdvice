using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class PriceModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public string Order { get; set; }

        //foreign key
        public int UserId { get; set; }
    }
}