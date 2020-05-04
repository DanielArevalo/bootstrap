using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ProductosProceso
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductosProceso
    {
        [DataMember] 
        public Int64 cod_prodproc { get; set; }
        [DataMember] 
        public Int64 cod_balance { get; set; }
        [DataMember] 
        public Int64 cantidad { get; set; }
        [DataMember] 
        public string producto { get; set; }
        [DataMember] 
        public Int64 porcpd { get; set; }
        [DataMember] 
        public Int64 valunitario { get; set; }
        [DataMember] 
        public Int64 valortotal { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }

        

    }
}