using System.Collections.Generic;

namespace MoviesDatabase.Core.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
