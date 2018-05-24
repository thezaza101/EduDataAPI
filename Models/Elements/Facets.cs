using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EduDataAPI.Models
{
    public class Facets
    {
        [Key]
        [JsonIgnore]
        public string ID {get; set;}
        
        [JsonProperty("pattern")]
        public string pattern { get; set; }

        [JsonProperty("maxLength")]
        public string maxLength { get; set; }

        [JsonProperty("minInclusive")]
        public string minInclusive { get; set; }

        [JsonProperty("minLength")]
        public string minLength { get; set; }
    }
}
