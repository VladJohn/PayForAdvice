using System.Collections.Generic;

namespace Domain
{
    public class Category : Idable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        //foreign key
        public virtual ICollection<User> Users { get; set; }

        //constructor
        public Category()
        {
            Users = new HashSet<User>();
        }



    }
}