using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Acceso
    /// </summary>
    [DataContract]
    [Serializable]
    public class Ingresos
    {
        [DataMember]
        public int cod_ingreso { get; set; }
        [DataMember]
        public DateTime fecha_horaingreso { get; set; }
        [DataMember]
        public DateTime? fecha_horasalida { get; set; }
        [DataMember]
        public string direccionip { get; set; }
        [DataMember]
        public int? codusuario { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}