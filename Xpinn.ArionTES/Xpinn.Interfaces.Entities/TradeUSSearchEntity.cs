using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Xpinn.Interfaces.Entities
{
    [Serializable]
    public class TradeUSSearchEntity
    {
        public string id { get; set; }
        public List<TradeUSSearchEntityAddress> addresses { get; set; }
        public List<string> alt_names { get; set; }
        public List<string> citizenships { get; set; }
        public List<string> dates_of_birth { get; set; }
        public string entity_number { get; set; }
        public List<TradeUSSearchEntityIds> ids { get; set; }
        public string name { get; set; }
        public List<string> nationalities { get; set; }
        public List<string> places_of_birth { get; set; }
        public List<string> programs { get; set; }
        public string remarks { get; set; }
        public string source { get; set; }
        public string source_information_url { get; set; }
        public string source_list_url { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    public class TradeUSSearchInd     
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string alt_name { get; set; }
        public string citizenship { get; set; }
        public string date_of_birth { get; set; }
        public string source { get; set; }
        public string program { get; set; }
        public string nationality { get; set; }
        public string place_of_birth { get; set; }
        public bool coincidencia { get; set; }

        //agregado
        public string nombre_persona { get; set; }
        public string cod_persona { get; set; }
        public string id_persona { get; set; }
    }

    [Serializable]
    [XmlRoot("INDIVIDUAL")]
    public class Individual
    {
        [XmlElement("DATAID")]
        public string dataid { get; set; }

        [XmlElement(ElementName = "FIRST_NAME")]
        public string first_name { get; set; }

        [XmlElement(ElementName = "SECOND_NAME")]
        public string second_name { get; set; }

        [XmlElement(ElementName = "THIRD_NAME")]
        public string third_name { get; set; }

        [XmlElement(ElementName = "UN_LIST_TYPE")]
        public string un_list_type { get; set; }

        [XmlElement(ElementName = "LISTED_ON")]
        public string listed_on { get; set; }

        [XmlElement(ElementName = "COMMENTS1")]
        public string comments1 { get; set; }

        [XmlElement(ElementName = "DESIGNATION")]
        public List<Value> lstDesignation { get; set; }

        [XmlElement(ElementName = "NATIONALITY")]
        public List<Value> lstnationality { get; set; }

        [XmlElement(ElementName = "LIST_TYPE")]
        public List<Value> lstType { get; set; }

        [XmlElement(ElementName = "INDIVIDUAL_DOCUMENT")]
        public List<IndividualDocument> individual_document { get; set; }

        public string designation { get; set; }
        public string nationality { get; set; }
        public string list_type { get; set; }
        public bool coincidencia { get; set; }

        //agregado
        public string nombre_persona { get; set; }
        public string id_persona { get; set; }

    }

    [Serializable()]
    [XmlRoot("ENTITY")]
    public class Entity
    {
        [XmlElement("DATAID")]
        public string dataid { get; set; }

        [XmlElement(ElementName = "FIRST_NAME")]
        public string first_name { get; set; }

        [XmlElement(ElementName = "UN_LIST_TYPE")]
        public string un_list_type { get; set; }

        [XmlElement(ElementName = "LISTED_ON")]
        public string listed_on { get; set; }

        [XmlElement(ElementName = "COMMENTS1")]
        public string comments1 { get; set; }

        [XmlElement(ElementName = "COUNTRY")]
        public string country { get; set; }

        [XmlElement(ElementName = "CITY")]
        public string city { get; set; }

        public bool coincidencia { get; set; }

        //agregado
        public string nombre { get; set; }
        public string identificacion { get; set; }
    }
}
