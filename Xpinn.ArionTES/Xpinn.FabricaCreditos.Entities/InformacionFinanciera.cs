using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad InformacionFinanciera
    /// </summary>
    [DataContract]
    [Serializable]
    public class InformacionFinanciera
    {
        [DataMember] 
        public Int64 cod_inffin { get; set; }
        [DataMember] 
        public DateTime fecha { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }


        // REPORTE 
        [DataMember]
        public Int64 cuenta { get; set; }
          [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
          




    }
}