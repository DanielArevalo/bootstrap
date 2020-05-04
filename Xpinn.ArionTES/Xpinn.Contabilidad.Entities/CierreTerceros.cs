using System;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class CierreTerceros
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }
        [DataMember]
        public String estado { get; set; }
    }

}