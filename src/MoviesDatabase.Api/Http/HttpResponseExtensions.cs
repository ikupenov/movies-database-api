using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MoviesDatabase.Api.Http
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteJsonAsync(this HttpResponse @this, object value)
        {
            @this.ContentType = ContentType.JSON;
            var serializedValue = JsonConvert.SerializeObject(value);

            await @this.WriteAsync(serializedValue);
        }
    }
}