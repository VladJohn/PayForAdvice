using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPI.Validators;

namespace WebAPI.Models
{
    
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public int Status { get; set; }

        //foreign key
        public int RoleId { get; set; }
    }
}