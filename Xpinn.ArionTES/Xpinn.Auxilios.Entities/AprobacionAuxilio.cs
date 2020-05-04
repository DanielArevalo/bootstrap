using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class AprobacionAuxilio
    {
        [DataMember]
        public Int64 idcontrolaux { get; set; }
        [DataMember]
        public Int64 numero_auxilios { get; set; }
        [DataMember]
        public int codtipo_proceso { get; set; }
        [DataMember]
        public DateTime fecha_proceso { get; set; }
        [DataMember]
        public int codusuario { get; set; }
        [DataMember]
        public string observaciones { get; set; }

        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }
        //ENTIDADES DE APROBACION
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public decimal valor_aprobado { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
    }
}
