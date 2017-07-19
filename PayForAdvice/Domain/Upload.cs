using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Upload
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UploadURL { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
