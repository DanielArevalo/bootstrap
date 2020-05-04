using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivcategoria
    {
        [DataMember]
        public Int64? id_categoria { get; set; }
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64? id_padre { get; set; }
        [DataMember]
        public string nombre_categoria { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string cod_cuenta_ingreso { get; set; }
        [DataMember]
        public string cod_cuenta_gasto { get; set; }
    }
}