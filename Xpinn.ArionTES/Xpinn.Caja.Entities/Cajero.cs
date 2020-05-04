using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Cajero
    {
        [DataMember]
        public Int64 IdCajero { get; set; }
        [DataMember]
        public string cod_cajero { get; set; }
        [DataMember]
        public long cod_caja_des { get; set; }
        [DataMember]
        public String nom_cajero { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public String nom_caja { get; set; }
        [DataMember]
        public DateTime fecha_ingreso { get; set; }
        [DataMember]
        public DateTime fecha_retiro { get; set; }
        [DataMember]
        public string fecharetiro { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public Int64 conteo { get; set; }
        [DataMember]
        public Int64 cod_cajero_ppal { get; set; }
        [DataMember]
        public Int64 cod_caja_ppal { get; set; }
        [DataMember]
        public String nom_cajero_ppal { get; set; }
        [DataMember]
        public String cod_usuario { get; set; }
        [DataMember]
        public Int64 estado_ofi { get; set; }
        [DataMember]
        public Int64 estado_caja { get; set; }

        [DataMember]
        public string identificacion { get; set; }

    }
}
