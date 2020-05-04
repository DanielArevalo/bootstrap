using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Xpinn.Interfaces.Entities
{
    public class TradeUSSearchResults
    {
        public string search_performed_at { get; set; }
        public List<TradeUSSearchEntity> results { get; set; }
    }
    
    [XmlRoot("CONSOLIDATED_LIST")]
    public class Individuals
    {
        [XmlArray("INDIVIDUALS")]
        [XmlArrayItem("INDIVIDUAL")]
        public List<Individual> individual { get; set; }

        [XmlArray("ENTITIES")]
        [XmlArrayItem("ENTITY")]
        public List<Entity> entity { get; set; }
    }
}
