using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class TipoCupo
    {
        [DataMember]
        public Int64? tipo_cupo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? resta_creditos { get; set; }
        [DataMember]
        public List<DetTipoCupo> LstVariables { get; set; }
    }
}