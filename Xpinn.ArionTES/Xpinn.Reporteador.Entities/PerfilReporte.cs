using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    /// <summary>
    /// Entidad PerfilReporte
    /// </summary>
    [DataContract]
    [Serializable]
    public class PerfilReporte
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idperfil { get; set; }
        [DataMember]
        public Int64 codperfil { get; set; }
        [DataMember]
        public string nombreperfil { get; set; }
        [DataMember]
        public Boolean autorizar { get; set; }
    }
}
