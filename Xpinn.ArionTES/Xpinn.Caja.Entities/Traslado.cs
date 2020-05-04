using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Traslado
    {
        [DataMember]
        public Int64 IdTraslado { get; set; }
        [DataMember]
        public Int64 cod_traslado { get; set; }
        [DataMember]
        public DateTime fecha_traslado { get; set; }
        [DataMember]
        public DateTime fecha_recibe { get; set; }
        [DataMember]
        public Int64 cod_oficina_ori { get; set; }
        [DataMember]
        public string nomoficina_ori { get; set; }
        [DataMember]
        public Int64 cod_caja_ori { get; set; }
        [DataMember]
        public string nomcaja_ori { get; set; }
        [DataMember]
        public Int64 cod_cajero_ori { get; set; }
        [DataMember]
        public string nomcajero_ori { get; set; }
        [DataMember]
        public Int64 cod_caja_des { get; set; }
        [DataMember]
        public Int64 cod_cajero_des { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public Int64 tipo_traslado { get; set; }
        [DataMember]
        public string tipo_movimiento { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public string ip { get; set; }
    }
}
