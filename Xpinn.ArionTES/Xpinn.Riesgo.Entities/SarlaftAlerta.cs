using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class SarlaftAlerta
    {
        [DataMember]
        public int idalerta { get; set; }
        [DataMember]
        public DateTime fecha_alerta { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
        [DataMember]
        public int tipo_alerta { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? tipo_producto { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public DateTime? fechacrea { get; set; }
        [DataMember]
        public DateTime? fechaultmod { get; set; }
        [DataMember]
        public string consulta { get; set; }
        [DataMember]
        public Int64 cod_consulta { get; set; }
        [DataMember]
        public DateTime fecha_consulta { get; set; }
        [DataMember]
        public bool coincidencia { get; set; }
        [DataMember]
        public string tipo_consulta { get; set; }
        [DataMember]
        public bool coincidencia2 { get; set; }
        //Agregado para traer el resultado de un tipo de consulta 2

    }

    [DataContract]
    [Serializable]
    public class Consulta
    {
        [DataMember]
        public Int64 cod_consulta { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string tipo_consulta { get; set; }
        [DataMember]
        public string contenido { get; set; }
        [DataMember]
        public int coincidencia { get; set; }
        [DataMember]
        public DateTime fecha_consulta { get; set; }
    }
}
