using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Status { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public User()
        {
            Prices = new HashSet<Price>();
            Categories = new HashSet<Category>();
            Questions = new HashSet<Question>();
            Answers = new HashSet<Answer>();
        }
    }
}
