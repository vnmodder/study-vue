using System.Text.Json.Serialization;

namespace TestAPI
{
    public class Lop
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
