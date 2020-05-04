using System;
using System.Runtime.Serialization;

namespace Xpinn.Sincronizacion.Entities
{
    [DataContract]
    [Serializable]
    public class Oficina
    {
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime? fecha_creacion { get; set; }
        [DataMember]
        public Int64? codciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string responsable { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int? estado_caja { get; set; }
        [DataMember]
        public Int64? cod_super { get; set; }
        [DataMember]
        public int? sede_propia { get; set; }
        [DataMember]
        public int? indicador_corresponsal { get; set; }
        [DataMember]
        public int? tipo_negocio { get; set; }
    }
}
