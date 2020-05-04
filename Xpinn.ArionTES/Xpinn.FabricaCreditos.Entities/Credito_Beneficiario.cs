using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{   
    [DataContract]
    [Serializable]
    public class Credito_Beneficiario
    {
        [DataMember]
        public Int64 codbeneficiariocre { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? cod_parentesco { get; set; }
        [DataMember]
        public decimal? porcentaje_beneficiario { get; set; }
    }
}
