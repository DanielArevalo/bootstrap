using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad Castigo
    /// </summary>
    [DataContract]
    [Serializable]
    public class Castigo
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public DateTime fecha_castigo { get; set; }
        [DataMember]
        public string cod_linea_castigo { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
    }
}