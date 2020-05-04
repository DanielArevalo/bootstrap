using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class CuentaHabientes
    {
        [DataMember]
        public Int64 idcuenta_habiente { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? tipo_firma { get; set; }
        [DataMember]
        public Int64? cod_usuario_ahorro { get; set; }


        [DataMember]
        public int? principal { get; set; }
        [DataMember]
        public string conjuncion { get; set; }


        //Detalle de la persona
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64 cod_usuario_cdat { get; set; }
        
    }

}