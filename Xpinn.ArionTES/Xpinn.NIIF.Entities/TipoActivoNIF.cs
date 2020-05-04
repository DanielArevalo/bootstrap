using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class TipoActivoNIF
    {
        [DataMember]
        public int tipo_activo_nif { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int codclasificacion_nif { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_deterioro { get; set; }
        [DataMember]
        public string cod_cuenta_revaluacion { get; set; }
        [DataMember]
        public string cod_cuenta_gastodet { get; set; }

        //Agregado
        [DataMember]
        public string nomclasificacion_nif { get; set; }

    }
}