using System;

namespace MoviesDatabase.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
