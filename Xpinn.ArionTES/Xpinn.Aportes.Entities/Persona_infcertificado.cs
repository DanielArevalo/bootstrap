using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Persona_infcertificado
    {
        [DataMember]
        public decimal idconsecutivo { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public decimal? valor_aportes { get; set; }
        [DataMember]
        public decimal? valor_cartera { get; set; }
        [DataMember]
        public decimal? valor_intereses { get; set; }
        [DataMember]
        public decimal? otros_ingresos { get; set; }
        [DataMember]
        public decimal? retefuente { get; set; }
        [DataMember]
        public decimal? total { get; set; }
        [DataMember]
        public int? cod_carga { get; set; }
    }
}