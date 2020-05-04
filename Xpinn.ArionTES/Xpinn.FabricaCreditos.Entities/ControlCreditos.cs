using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ControlCreditos
    /// </summary>
    [DataContract]
    [Serializable]
    public class ControlCreditos
    {
        [DataMember] 
        public Int64 idcontrol { get; set; }
        [DataMember] 
        public Int64 numero_radicacion { get; set; }
        [DataMember] 
        public string codtipoproceso { get; set; }
        [DataMember] 
        public DateTime fechaproceso { get; set; }
        [DataMember]
        public DateTime fechaconsulta_dat { get; set; }
        [DataMember]
        public String fechadata { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 cod_motivo { get; set; }
        [DataMember] 
        public string observaciones { get; set; }
        [DataMember] 
        public string anexos { get; set; }
        [DataMember] 
        public Int64 nivel { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string motivo { get; set; }

    }
}