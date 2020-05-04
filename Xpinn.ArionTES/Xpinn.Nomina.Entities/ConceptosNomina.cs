using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class ConceptosNomina
    {
        [DataMember]
        public Int64 CONSECUTIVO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DataMember]
        public string NATURALEZA { get; set; }
        [DataMember]
        public int? CLASE { get; set; }
        [DataMember]
        public string CLASEN { get; set; }
        [DataMember]
        public int? TIPO_CONCEPTO { get; set; }
        [DataMember]
        public string TIPO_CONCEPTON { get; set; }
        [DataMember]
        public int? UNIDAD_CONCEPTO { get; set; }
        [DataMember]
        public string UNIDAD_CONCEPTON { get; set; }
        [DataMember]
        public string FORMULA { get; set; }
        [DataMember]
        public decimal PONDERADO { get; set; }
        [DataMember]
        public long tipo { get; set; }
        [DataMember]
        public string desc_clase { get; set; }
        [DataMember]
        public string desc_tipo { get; set; }
        [DataMember]
        public string desc_unidad_concepto { get; set; }
        [DataMember]
        public string desc_tipoconcepto { get; set; }

        [DataMember]
        public String CONSECUTIVOConcepto { get; set; }

        [DataMember]
        public int? provisiona_extralegal { get; set; }

        [DataMember]
        public decimal? porcentajeprovisionextralegal { get; set; }

    }
}
