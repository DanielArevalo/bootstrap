using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad Obligación Financiera
    /// </summary>
    [DataContract]
    [Serializable]
    public class Obligacion
    {
        [DataMember]
        public Int64 idpresupuesto { get; set; }
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal plazo { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public Int64 cod_periodicidad { get; set; }
        [DataMember]
        public decimal gracia { get; set; }

        [DataMember]
        public Int64 iddetalle { get; set; }
        [DataMember]
        public Int64 numero_periodo { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }
        [DataMember]
        public double monto { get; set; }

        [DataMember]
        public DataTable dtFechas { get; set; }
        [DataMember]
        public DataTable dtObligacionesNuevas { get; set; }
    }
}