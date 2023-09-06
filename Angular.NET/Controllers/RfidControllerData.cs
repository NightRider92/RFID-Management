using Angular.NET.Database.Entities;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using JsonRequiredAttribute = System.Text.Json.Serialization.JsonRequiredAttribute;

namespace Angular.NET.Controllers
{
    /// <summary>
    /// Rfid data
    /// </summary>
    public class RfidControllerData
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("username")]
        [JsonRequired]
        public string? Username { get; set; }

        [JsonPropertyName("value")]
        [JsonRequired]
        public string? Value { get; set; }

        [JsonPropertyName("createdTime")]
        [JsonRequired]
        public string? CreatedTime { get; set; }
    }
}
