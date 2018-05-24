using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EduDataAPI.Models
{
    public class DataElement
    {
        [Key]
        [JsonIgnore]
        public string ID {get; set;}

        [JsonProperty("name")]
        public string name { get; set; }
        
        [JsonProperty("domain")]
        public string domain { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("definition")]
        public string definition { get; set; }
        
        [JsonProperty("guidance")]
        public string guidance { get; set; }
        
        [JsonProperty("identifier")]
        public string identifier { get; set; }
       
        [JsonProperty("usage", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> usage { 
            get{
                try{
                    return iUseage.Split(@"###").ToList();
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
                iUseage = s;
            }
         }
        
        [JsonProperty("datatype")]
        public Datatype datatype { get; set; }
        
        [JsonProperty("values", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> values { 
            get{
                try{
                    return iValue.Split(@"###").ToList();
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
                iValue = s;
            }
         }
        
        [JsonProperty("sourceURL")]
        public string sourceURL { get; set; }

        [JsonProperty("version")]
        public string version { get; set; }

        [JsonProperty("lastUpdateDate")]
        public string lastUpdateDate { get; set; }

        private string iUseage {get;set;}
        private string iValue {get;set;}
    }
}
