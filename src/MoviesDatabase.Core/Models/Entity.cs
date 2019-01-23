using System;

namespace MoviesDatabase.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
