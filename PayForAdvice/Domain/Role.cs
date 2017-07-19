using System.Collections.Generic;

namespace Domain
{
    public class Role : Idable
    {
        public string Name { get; set; }

        //foreign key
        public ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}