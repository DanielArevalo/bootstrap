using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Inversiones
    {
        [DataMember]
        public Int64 cod_inversion { get; set; }
        [DataMember]
        public string numero_titulo { get; set; }
        [DataMember]
        public decimal? valor_capital { get; set; }
        [DataMember]
        public decimal? valor_interes { get; set; }
        [DataMember]
        public int? plazo { get; set; }
        [DataMember]
        public DateTime? fecha_emision { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public decimal? tasa_interes { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public int? cod_tipo { get; set; }
        [DataMember]
        public string nom_tipo { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public int? tipo_calendario { get; set; }
    }
}
