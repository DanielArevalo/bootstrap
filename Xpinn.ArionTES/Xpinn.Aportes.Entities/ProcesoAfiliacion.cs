using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class ProcesoAfiliacion
    {
        //Entidades para registrar el proceso
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public Int64 numero_acta { get; set; }
        [DataMember]
        public int concepto { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public int tipo_proceso { get; set; }

        //Entidades para listar procesos realizados
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public Int64 digito_verificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string lugar_proceso { get; set; }
        [DataMember]
        public string nombre_usuario { get; set; }
        [DataMember]
        public string tipo_concepto { get; set; }
    }

}
