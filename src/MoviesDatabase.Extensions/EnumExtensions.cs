using System;

namespace MoviesDatabase.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum @this)
        {
            return @this.GetType().GetEnumName(@this);
        }
    }
}
