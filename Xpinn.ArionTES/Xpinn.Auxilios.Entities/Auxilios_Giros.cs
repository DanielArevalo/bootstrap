using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class Auxilios_Giros
    {
        [DataMember]
        public Int64 idgiro { get; set; }
        [DataMember]
        public int numero_auxilio { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string nom_tipo { get; set; }
    }
}
