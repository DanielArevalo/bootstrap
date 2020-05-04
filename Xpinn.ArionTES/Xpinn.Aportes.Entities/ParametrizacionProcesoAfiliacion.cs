using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]

    public class ParametrizacionProcesoAfiliacion
    {
        [DataMember]
        public int cod_proceso { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int requerido { get; set; }
        [DataMember]
        public int orden { get; set; }
        [DataMember]
        public int asociado { get; set; }
        [DataMember]
        public int asesor { get; set; }
        [DataMember]
        public string otro { get; set; }
        [DataMember]
        public string estado_asociado { get; set; }
        [DataMember]
        public string estado_proceso { get; set; }
        //INFORMACION PARA EL CORREO
        [DataMember]
        public string nombre_asociado { get; set; }
        [DataMember]
        public string email_asociado { get; set; }
        [DataMember]
        public string nombre_asesor { get; set; }
        [DataMember]
        public string email_asesor { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public List<ParametrizacionProcesoAfiliacion> lstParametros { get; set; }
        //REGISTRO AUDITORIA
        [DataMember]
        public int numero_solicitud { get; set; }
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string ip_local { get; set; }
        //DATOS HOJA DE RUTA
        [DataMember]
        public string tipo_iden { get; set; }
        [DataMember]
        public string sigPaso { get; set; }
        [DataMember]
        public string antPaso { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
    }
}
