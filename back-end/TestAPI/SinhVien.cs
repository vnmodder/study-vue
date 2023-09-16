using System.Text.Json.Serialization;

namespace TestAPI
{
    public class SinhVien
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("class_id")]
        public int ClassId { get; set; }
    }
}
