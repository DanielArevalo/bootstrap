using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Auditoria
    /// </summary>
    [DataContract]
    [Serializable]
    public class Auditoria
    {
        [DataMember] 
        public Int64 cod_auditoria { get; set; }
        [DataMember] 
        public Int64 codusuario { get; set; }
        [DataMember] 
        public Int64 codopcion { get; set; }
        [DataMember] 
        public DateTime fecha { get; set; }
        [DataMember] 
        public string ip { get; set; }
        [DataMember] 
        public string navegador { get; set; }
        [DataMember] 
        public string accion { get; set; }
        [DataMember] 
        public string tabla { get; set; }
        [DataMember] 
        public string detalle { get; set; }
        [DataMember]
        public string nombre_usuario { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public int? tipo_auditoria { get; set; }
        [DataMember]
        public string informacionanterior { get; set; }
        [DataMember]
        public string nombre_opcion { get; set; }
    }
}