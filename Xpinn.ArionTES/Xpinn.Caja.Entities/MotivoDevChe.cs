using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class MotivoDevChe
    {
        [DataMember]
        public int cod_motivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}