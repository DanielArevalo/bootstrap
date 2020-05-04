using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class MovimientoAporte
    {
        [DataMember]
        public Int64 numero_aporte { get; set; }
        [DataMember]
        public Int64 num_transaccion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_atributo { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal Saldo { get; set; }
        [DataMember]
        public Int64 cod_lista { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public DateTime FechaPago { get; set; }
        [DataMember]
        public DateTime FechaCuota { get; set; }
        [DataMember]
        public Int64 CodOperacion { get; set; }
        [DataMember]
        public DateTime FechaUltimoPago { get; set; }
        [DataMember]
        public DateTime FechaProximoPago { get; set; }
        [DataMember]
        public DateTime FechaAprobacion { get; set; }
        [DataMember]
        public String num_comp { get; set; }
        [DataMember]
        public Double Capital { get; set; }
        [DataMember]
        public String tipo_comp { get; set; }
        [DataMember]
        public String nom_tipo_comp { get; set; }
        [DataMember]
        public Int64 NumCuota { get; set; }
        [DataMember]
        public Int64 DiasMora { get; set; }
        [DataMember]
        public String TipoOperacion { get; set; }
        [DataMember]
        public String TipoMovimiento { get; set; }
        [DataMember]
        public Int32 estado { get; set; }
    }
}
