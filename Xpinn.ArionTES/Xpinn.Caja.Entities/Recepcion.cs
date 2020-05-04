using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]  
    public class Recepcion
    {
        [DataMember]
        public Int64 IdRecepcion { get; set; }
        [DataMember]
        public Int64 cod_recepcion { get; set; }
        [DataMember]
        public DateTime fecha_recepcion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public string nomcaja { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string nomcajero { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Int64 tipo_traslado { get; set; }
        [DataMember]
        public string tipo_movimiento { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
    }

}
