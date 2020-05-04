using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Familiares
    /// </summary>
    [DataContract]
    [Serializable]
    public class AutorizaConsulta
    {
        [DataMember]
        public Int32 id_autoriza { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int32 cod_persona { get; set; }
        [DataMember]
        public Int32 autoriza { get; set; }
        [DataMember]
        public DateTime fecha_autoriza { get; set; }
    }
}