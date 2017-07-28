using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class TokenModel
    {
        public DateTime Expiration { get; set; }
        public string Ip { get; set; }
        public string TokenText { get; set; }

        public int UserId { get; set; }
    }
}