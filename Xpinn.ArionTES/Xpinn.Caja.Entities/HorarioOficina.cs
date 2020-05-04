using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class HorarioOficina
    {
        [DataMember]
        public Int64 IdHorario { get; set; }
        [DataMember]
        public Int64 cod_horario { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 nom_oficina { get; set; }
        [DataMember]
        public Int64 tipo_horario { get; set; }
        [DataMember]
        public Int64 dia { get; set; }
        [DataMember]
        public string nom_dia { get; set; }
        [DataMember]
        public DateTime hora_inicial { get; set; }
        [DataMember]
        public DateTime hora_final { get; set; }
        [DataMember]
        public DateTime fecha_hoy { get; set; }
        [DataMember]
        public Int64 conteo { get; set; }
    }
}
