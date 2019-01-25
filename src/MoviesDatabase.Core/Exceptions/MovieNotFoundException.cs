using System;

namespace MoviesDatabase.Core.Exceptions
{
    [Serializable]
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException()
        {
        }

        public MovieNotFoundException(string message)
            : base(message)
        {
        }

        public MovieNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
