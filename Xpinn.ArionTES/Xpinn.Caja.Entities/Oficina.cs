using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Oficina
    {
        [DataMember]
        public Int64 IdOficina { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 cod_ciudad { get; set; }
        [DataMember]
        public string nom_ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }
        [DataMember]
        public string nom_centro { get; set; }
        [DataMember]
        public string responsable { get; set; }
        [DataMember]
        public string nom_persona { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public DateTime fecha_creacion { get; set; }
        [DataMember]
        public DateTime fechaproceso { get; set; }
        [DataMember]
        public DateTime fecha_nuevo_proceso { get; set; }
        [DataMember]
        public Int64 tipo_proceso { get; set; }
        [DataMember]
        public Int64 conteo { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int sede_propia { get; set; }
        [DataMember]
        public int indicador_corresponsal { get; set; }
        [DataMember]
        public int? tipo_negocio { get; set; }

        [DataMember]
        public Int64 cod_super{ get; set; }
    }
    
}