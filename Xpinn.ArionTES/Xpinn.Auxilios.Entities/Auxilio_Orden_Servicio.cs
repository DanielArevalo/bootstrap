using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class Auxilio_Orden_Servicio
    {
        [DataMember]
        public int idordenservicio { get; set; }
        [DataMember]
        public int numero_auxilio { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string idproveedor { get; set; }
        [DataMember]
        public string nomproveedor { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public Int64? numero_preimpreso { get; set; }
    }
}
