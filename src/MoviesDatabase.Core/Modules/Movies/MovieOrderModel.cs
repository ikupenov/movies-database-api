namespace MoviesDatabase.Core.Modules.Movies
{
    public class MovieOrderModel
    {
        public SortProperty Sort { get; set; } = SortProperty.Title;

        public bool IsAscending { get; set; } = true;

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = int.MaxValue;
    }

    public enum SortProperty
    {
        Title = 1,
        Rating = 2,
        YearOfRelease = 3
    }
}