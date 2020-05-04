using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad para la Proyección de Cartera
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProyeccionCartera
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 oficina { get; set; }
        [DataMember]
        public string pagare { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public DateTime fecha_terminacion { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public Int64 dias_mora { get; set; }
        [DataMember]
        public double monto { get; set; }
        [DataMember]
        public double saldo { get; set; }
        [DataMember]
        public double cuota { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime fecha_cuota { get; set; }
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public double valor { get; set; }
    }
}
