using Newtonsoft.Json;

namespace MultiContactManagement.Domain.EntitiesExternal
{
    public class Country
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("idd")]
        public Idd Idd { get; set; }
        public string CountryCode { get; set; }
    }

    public class Name
    {
        [JsonProperty("common")]
        public string? Country { get; set; }
    }

    public class Idd
    {
        [JsonProperty("root")]
        public string? Root { get; set; }

        [JsonProperty("suffixes")]
        public List<string>? Suffixes { get; set; }
    }
}
