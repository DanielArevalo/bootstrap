using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ComposicionPasivo
    /// </summary>
    [DataContract]
    [Serializable]
    public class ComposicionPasivo
    {
        [DataMember] 
        public Int64 idpasivo { get; set; }
        [DataMember] 
        public Int64 cod_inffin { get; set; }
        [DataMember] 
        public string acreedor { get; set; }
        [DataMember] 
        public Int64 monto_otorgado { get; set; }
        [DataMember] 
        public Int64 valor_cuota { get; set; }
        [DataMember] 
        public string periodicidad { get; set; }
        [DataMember] 
        public Int64 cuota { get; set; }
        [DataMember] 
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }


        [DataMember]
        public Int64 entidad { get; set; }
        [DataMember]
        public Int64 cupo { get; set; }
        [DataMember]
        public Int64 saldo { get; set; }
  


    }
}