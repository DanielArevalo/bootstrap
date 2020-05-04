using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class MotivoExcepcion
    {
        [DataMember]
        public int cod_motivo_excepcion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}