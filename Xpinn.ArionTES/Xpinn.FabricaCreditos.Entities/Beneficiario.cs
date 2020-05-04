using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Beneficiario
    {
        [DataMember]
        public Int64 idbeneficiario { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public String numero_cuenta { get; set; }
        [DataMember]
        public String numero_programado{ get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string identificacion_ben { get; set; }
        [DataMember]
        public int? tipo_identificacion_ben { get; set; }
        [DataMember]
        public string nombre_ben { get; set; }
        [DataMember]
        public DateTime? fecha_nacimiento_ben { get; set; }
        [DataMember]
        public int? parentesco { get; set; }
        [DataMember]
        public decimal? porcentaje_ben { get; set; }
        [DataMember]
        public int codparentesco { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public int? edad { get; set; }

    }
}
