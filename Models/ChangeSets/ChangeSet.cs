using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduDataAPI.Models
{
    public class ChangeSet
    {
        [Key]
        [JsonIgnore]
        public string ID {get; set;}
        
        [JsonProperty("UpdateDescription")]
        public string UpdateDescription { get; set; }

        [JsonProperty("UpdatedDomain")]
        public string UpdatedDomain { get; set; }

        [JsonProperty("UpdatedIdentifiers", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> UpdatedIdentifiers { 
            
            get{
                try{
                    if (string.IsNullOrWhiteSpace(iUpdatedIdentifiers)){iUpdatedIdentifiers="";}
                    return iUpdatedIdentifiers.Split(@"###").ToList();
                }catch{
                    return new List<string>();
                }
            }
            set{
                string s = "";
                foreach (string x in value)
                {s = s + x + "###";}
                iUpdatedIdentifiers = s;
            }
            
         }

         [JsonIgnore]
        public string iUpdatedIdentifiers {get;set;}  

        [JsonProperty("Changes")]
        public List<Change> Changes {get; set;}

        [JsonProperty("PayloadType")]
        public string PayloadType {get; set;}
        
        [JsonProperty("Payload")]
        public string Payload {get; set;}

        [JsonIgnore]
        public string Status {get; set;}

        
        
    }
}
