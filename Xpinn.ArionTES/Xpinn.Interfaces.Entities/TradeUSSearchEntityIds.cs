using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Xpinn.Interfaces.Entities
{
    public class TradeUSSearchEntityIds
    {
        public string country { get; set; }
        public string expiration_date { get; set; }
        public string issue_date { get; set; }
        public string number { get; set; }
        public string type { get; set; }
    }

    [XmlRoot("INDIVIDUAL_DOCUMENT")]
    public class IndividualDocument
    {
        [XmlElement(ElementName = "TYPE_OF_DOCUMENT")]
        public string type_of_document { get; set; }

        [XmlElement(ElementName = "NUMBER")]
        public string number { get; set; }
    }
        
    [XmlRoot("VALUE")]
    public class Value
    {
        [XmlElement(ElementName = "VALUE")]
        public string value { get; set; }
    }
}
