using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Procesos
    /// </summary>
    [DataContract]
    [Serializable]
    public class Procesos
    {
        [DataMember] 
        public Int64 cod_proceso{ get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 tipo_proceso { get; set; }
        [DataMember]
        public string nom_tipo_proceso { get; set; }
        [DataMember]
        public Int64 cod_proceso_antec { get; set; }
        [DataMember]
        public Int64 cod_linea_credito { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string antecede { get; set; }
        [DataMember]
        public string Perror { get; set; }
    }
}