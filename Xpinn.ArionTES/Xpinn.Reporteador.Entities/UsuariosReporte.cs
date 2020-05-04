using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    /// <summary>
    /// Entidad UsuariosReporte
    /// </summary>
    [DataContract]
    [Serializable]
    public class UsuariosReporte
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idusuario { get; set; }
        [DataMember]
        public Int64 codusuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Boolean autorizar { get; set; }
    }
}
