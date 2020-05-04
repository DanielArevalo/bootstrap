using System;
using System.Runtime.Serialization;


namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Beneficiarios
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_poliza { get; set; }
        [DataMember]
        public String nombres { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String parentesco { get; set; }
        [DataMember]
        public DateTime fecha_nacimiento { get; set; }
        [DataMember]
        public Int64 porcentaje { get; set; }
    }
}