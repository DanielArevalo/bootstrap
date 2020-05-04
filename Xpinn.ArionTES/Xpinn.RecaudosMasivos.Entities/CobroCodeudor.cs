using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CobroCodeudor
    {
        [DataMember]
        public int idcobrocodeud { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public string identificacion_deudor { get; set; }
        [DataMember]
        public string nombre_deudor { get; set; }
        [DataMember]
        public string nombreYApellidoDeudor { get; set; }
        [DataMember]
        public Int64 cod_codeudor { get; set; }
        [DataMember]
        public string identificacion_codeudor { get; set; }
        [DataMember]
        public string nombre_codeudor { get; set; }
        [DataMember]
        public string nombreYApellidoCodeudor { get; set; }
        [DataMember]
        public DateTime fecha_cobro { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public decimal? porcentaje { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int? cod_empresa { get; set; }
        [DataMember]
        public DateTime? fechacrea { get; set; }
        [DataMember]
        public decimal? cod_usuario { get; set; }
        [DataMember]
        public int estado { get; set; }
    }
}