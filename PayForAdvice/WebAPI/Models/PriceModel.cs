using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPI.Validators;

namespace WebAPI.Models
{
    [Validator(typeof(PriceValidator))]
    public class PriceModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public int Order { get; set; }

        //foreign key
        public int UserId { get; set; }
    }
}