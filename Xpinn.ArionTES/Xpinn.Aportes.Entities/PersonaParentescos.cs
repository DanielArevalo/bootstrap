using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaParentescos
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public long codigoparentesco { get; set; }
        [DataMember]
        public long codigotipoidentificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombresapellidos { get; set; }
        [DataMember]
        public string codigoactividadnegocio { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string cargo { get; set; }
        [DataMember]
        public decimal? ingresomensual { get; set; }

        //Estatus de la persona
        [DataMember]
        public int? empleado_entidad { get; set; }
        [DataMember]
        public int? miembro_administracion { get; set; }
        [DataMember]
        public int? miembro_control { get; set; }
        [DataMember]
        public int? es_pep { get; set; }

    }
}