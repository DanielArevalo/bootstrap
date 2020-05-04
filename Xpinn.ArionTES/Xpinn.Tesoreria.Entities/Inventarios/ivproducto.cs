using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivproducto
    {
        [DataMember]
        public Int64? productoid { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string productocode { get; set; }
        [DataMember]
        public string productoname { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public Int64? id_categoria { get; set; }
        [DataMember]
        public string nombre_categoria { get; set; }
    }
}