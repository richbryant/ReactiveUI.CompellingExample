using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Refit;

namespace CompellingExample.Blazor.Client
{
    public class JsonContentSerializer : IContentSerializer
    {
        private readonly JsonSerializerOptions _serializerOptions;
 
        public JsonContentSerializer(JsonSerializerOptions serializerOptions = null)
        {
            _serializerOptions = serializerOptions ?? new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
 
        public async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            await using var utf8Json = await content.ReadAsStreamAsync()
                .ConfigureAwait(false);
 
            return await JsonSerializer.DeserializeAsync<T>(utf8Json,
                _serializerOptions).ConfigureAwait(false);
        }
 
        public Task<HttpContent> SerializeAsync<T>(T item)
        {
            var json = JsonSerializer.Serialize(item, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
 
            return Task.FromResult((HttpContent)content);
        }
    }
}