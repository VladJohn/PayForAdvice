using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class PriceModelForPublicProfile
    {
        public double Base { get; set; }
        public double Normal { get; set; }
        public double Premium { get; set; }
        public string DetailBase { get; set; }
        public string DetailNormal { get; set; }
        public string DetailPremium { get; set; }

        //foreign key
        public int UserId { get; set; }
    }
}