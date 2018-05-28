using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EduDataAPI.Models
{
    public class HomeViewModel
    {
        //Data Element Section
        [DisplayName("Data identifier")]
        public string DataID { get; set; }
        public DataElement ElemtnInFocus {get; set;}

        [DisplayName("Element Name:")]
        public string name { get; set; }
        [DisplayName("Element Domain:")]
        public string domain { get; set; }
        [DisplayName("Element Status:")]
        public string status { get; set; }
        [DisplayName("Element Definition:")]
        public string definition { get; set; }
        [DisplayName("Element Guidance:")]
        public string guidance { get; set; }
        [DisplayName("Element Identifier:")]
        public string identifier { get; set; }
        [DisplayName("Element Usage:")]
        public List<string> usage {get; set;}
       
       //datatype
        public string dataTypeFacetPattern { get; set; }
        public string dataTypeFacetMaxLength { get; set; }
        public string dataTypeFacetMinInclusive { get; set; }
        public string dataTypeFacetMinLength { get; set; }
        
        public string dataTypeType { get; set; }

        public List<string> dataTypeValues { get; set; }
        
        [DisplayName("Element Values:")]
        public List<string> values {get; set;}

        [DisplayName("Element Source URL:")]
        public string sourceURL { get; set; }

        [DisplayName("Element Version:")]
        public string version { get; set; }

        [DisplayName("Change Action:")]
        public string changeAction { get; set; }



        //ChangeSetSection
        [DisplayName("Change Set ID:")]
        public string changeSetID { get; set; }
        
        [DisplayName("Description:")]
        public string changeUpdateDescription { get; set; }

        [DisplayName("Domain:")]
        public string changeUpdatedDomain { get; set; }

        [DisplayName("# of changes:")]
        public string changeNumber { get; set; }

        [DisplayName("ChangeSet Action:")]
        public string changeSetAction { get; set; }




    }
}
