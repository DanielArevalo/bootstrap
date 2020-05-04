using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class LibroTerceros
    {
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public Int64? codigo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int32? tipo_iden { get; set; }
        [DataMember]
        public string nom_tipo_iden { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string regimen { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public string tipo_comp { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? saldo { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public string tipo_benef { get; set; }
        [DataMember]
        public string num_sop { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }

        [DataMember]
        public string depende_de { get; set; }
    }
}

