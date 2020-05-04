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
    public class RepEdades
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
        public Int64 dias_mora { get; set; }
        [DataMember]
        public double clasificacion_mora_1 { get; set; }
        [DataMember]
        public double clasificacion_mora_2 { get; set; }
        [DataMember]
        public double clasificacion_mora_3 { get; set; }
        [DataMember]
        public double clasificacion_mora_4 { get; set; }
        [DataMember]
        public double clasificacion_mora_5 { get; set; }
        [DataMember]
        public double clasificacion_mora_6 { get; set; }
        [DataMember]
        public double clasificacion_mora_7 { get; set; }
        [DataMember]
        public double clasificacion_mora_8 { get; set; }
    }

    [DataContract]
    [Serializable]
    public class EdadMora
    {
        [DataMember]
        public Int64 idrango { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 dias_minimo { get; set; }
        [DataMember]
        public Int64 dias_maximo { get; set; }
    }
}
