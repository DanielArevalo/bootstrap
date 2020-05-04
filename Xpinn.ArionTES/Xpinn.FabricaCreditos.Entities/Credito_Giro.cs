using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{   
    [DataContract]
    [Serializable]
    public class Credito_Giro
    {
        [DataMember]
        public Int64 idgiro { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public string nom_tipo { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]        
        public Int32 id_tipo_desembolso { get; set; }
        [DataMember]
        public CuentasBancarias cuenta { get; set; }
    }
}
