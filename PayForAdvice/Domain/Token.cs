using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Token : Idable
    {
        public DateTime Expiration { get; set; }
        public string Ip { get; set; }
        public string TokenText { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
