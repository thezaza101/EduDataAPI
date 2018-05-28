using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduDataAPI.Models
{
    public class Change
    {
        [Key]
        [JsonIgnore]
        public string ID {get; set;}

        [JsonProperty("ChangeType")]
        public string ChangeType {get; set;}        

        [JsonProperty("UpdatedIdentifiers", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public DataElement UpdatedElement {get; set;}
        
    }
}
