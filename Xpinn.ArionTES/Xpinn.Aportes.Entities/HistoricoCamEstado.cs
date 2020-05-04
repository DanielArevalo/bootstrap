using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class HistoricoCamEstado
    {
        [DataMember]
        public Int64 idconsecutivo { get; set; }
        [DataMember]
        public Int64 idafiliacion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string estado_anterior { get; set; }
        [DataMember]
        public Int64? cod_motivo_anterior { get; set; }
        [DataMember]
        public DateTime fecha_cambio { get; set; }
        [DataMember]
        public string estado_nuevo { get; set; }
        [DataMember]
        public Int64 cod_motivo_nuevo { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
    }
}
