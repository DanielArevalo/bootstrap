using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities
{
    [DataContract]
    [Serializable]
    public class ConsecutivoOficinas
    {
        [DataMember]
        public int idconsecutivo { get; set; }
        [DataMember]
        public string tabla { get; set; }
        [DataMember]
        public string columna { get; set; }
        [DataMember]
        public Int64? cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public Int64? tipo_consecutivo { get; set; }
        [DataMember]
        public Int64? rango_inicial { get; set; }
        [DataMember]
        public Int64? rango_final { get; set; }
        [DataMember]
        public DateTime? fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime? fecultmod { get; set; }
        [DataMember]
        public string usuarioultmod { get; set; }
    }
}
