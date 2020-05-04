using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    public class RangoPerfil
    {
        [DataMember]
        public Int64 cod_rango_perfil { get; set; }
        [DataMember]
        public Int64 calificacion { get; set; }
        [DataMember]
        public Int32 rango_minimo { get; set; }
        [DataMember]
        public Int32 rango_maximo { get; set; }
        [DataMember]
        public Int64 cod_monitoreo { get; set; }
        [DataMember]
        public List<RangoPerfil> lstDetalle { get; set; }
    }
}
