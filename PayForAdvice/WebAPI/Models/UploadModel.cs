using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class UploadModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UploadURL { get; set; }

        public int AnswerId { get; set; }
    }
}