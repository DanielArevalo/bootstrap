using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class ELibretas
    {
        [DataMember]
        public Int64 id_Libreta { get; set; }
        [DataMember]
        public Int64 numero_libreta { get; set; }
        [DataMember]
        public Int64 desde { get; set; }
        [DataMember]
        public decimal valor_libreta { get; set; }
        [DataMember]
        public Int64 hasta { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public DateTime fecha_asignacion { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public String numero_cuenta { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public String cod_nomina { get; set; }
        [DataMember]
        public String nombreoficina { get; set; }
        [DataMember]
        public decimal saldo_total { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public String Linea { get; set; }
        [DataMember]
        public String nom_estado { get; set; }
        [DataMember]
        public String nom_estado_libreta { get; set; }
        [DataMember]
        public String TipoIdentific { get; set; }
        [DataMember]
        public Int64 Num_Desprendible { get; set; }
    }
}
