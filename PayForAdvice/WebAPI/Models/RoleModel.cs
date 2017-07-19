
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}