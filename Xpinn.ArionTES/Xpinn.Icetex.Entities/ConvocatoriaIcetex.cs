using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class ConvocatoriaIcetex
    {
        [DataMember]
        public int cod_convocatoria { get; set; }
        [DataMember]
        public DateTime fecha_convocatoria { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public DateTime? fecha_inicio { get; set; }
        [DataMember]
        public DateTime? fecha_final { get; set; }
        [DataMember]
        public string mensaje_solicitud { get; set; }
        [DataMember]
        public int? numero_creditos { get; set; }
        [DataMember]
        public DateTime? fec_ini_inscripcion { get; set; }
        [DataMember]
        public DateTime? fec_fin_inscripcion { get; set; }
    }
}
