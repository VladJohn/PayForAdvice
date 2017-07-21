using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class UserModelForCategoryView
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public double Rating { get; set; }
    }
}