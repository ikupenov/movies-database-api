using Microsoft.Extensions.Configuration;

namespace MoviesDatabase.Api.Configuration
{
    internal static class ConfigurationExtensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration @this)
        {
            var sectionName = typeof(TSettings).Name;
            var section = @this.GetSection(sectionName);

            return section.Get<TSettings>();
        }
    }
}
