using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace EduDataAPI.Models
{
    public class Datatype
    {
        [Key]
        [JsonIgnore]
        public string ID {get; set;}
        
        [JsonProperty("facets")]
        public Facets facets { get; set; }
        
        [JsonProperty("type")]
        public string type { get; set; }
        
        [JsonProperty("values", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> values { 
            get{
                try{
                    return iValues.Split(@"###").ToList();
                }catch{
                    return new List<string>();
                }
            }
            set{
                string s = "";
                foreach (string x in value)
                {
                    s = s + x + "###";
                }
                iValues = s;
            }
         }

        private string iValues {get;set;}
    }
}
