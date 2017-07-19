using System.Collections.Generic;

namespace Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //foreign key
        public ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}