using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduDataAPI.Models
{
    public class DictElement
    {
        [Key]
        [JsonIgnore]
        public string ID {get; set;}

        [JsonProperty("elementName")]
        public string elementName { get; set; }
        
        [JsonProperty("domainAcronym")]
        public string domainAcronym { get; set; }

        public static List<DictElement> ReadDictElementsFromCSV(string csv)
        {
            List<DictElement> output = new List<DictElement>();
            string[] data = csv.Split(',');
            for (int i = 0; i < data.Length; i = i+2)
            {
                output.Add(new DictElement(){ elementName = data[i], domainAcronym = data[i+1]});
            }
            return output;
        }
    }
}