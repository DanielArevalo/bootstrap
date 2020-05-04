using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaAutorizacion
    {
        [DataMember]
        public Int64 idautorizacion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public string ip { get; set; }
        [DataMember]
        public int? cod_usuario { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public DateTime? fecha_excepcion { get; set; }
        [DataMember]
        public int? cod_motivo_excepcion { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public int? cod_usuario_excepcion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string estados { get; set; }
    }
}