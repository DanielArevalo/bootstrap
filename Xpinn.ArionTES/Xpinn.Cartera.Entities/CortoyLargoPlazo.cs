using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad para reporte de corto y largo plazo
    /// </summary>
    [DataContract]
    [Serializable]
    public class CortoyLargoPlazo
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public double saldo_capital { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public Int64 cod_asesor { get; set; }
        [DataMember]
        public string nom_asesor { get; set; }
        [DataMember]
        public double monto_aprobado { get; set; }
        [DataMember]
        public double cuota { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 cod_periodicidad { get; set; }
        [DataMember]
        public double tasa_interes { get; set; }
        [DataMember]
        public double corto_plazo { get; set; }
        [DataMember]
        public double largo_plazo { get; set; }
        [DataMember]
        public double numero_dias { get; set; }
        [DataMember]
        public double numero_meses { get; set; }
    }

}
