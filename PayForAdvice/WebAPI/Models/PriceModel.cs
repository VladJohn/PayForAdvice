using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class PriceModel
    {
        public int Id { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string Order { get; set; }

        //foreign key
        public int UserId { get; set; }
    }
}