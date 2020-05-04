using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class GiroDistribucion
    {
        [DataMember]
        public Int64 iddetgiro { get; set; }
        [DataMember]
        public Int64 idgiro { get; set; }
        [DataMember]
        public DateTime fecha_distribucion { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public Int64? estado { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public int distribucion { get; set; }

        /// <summary>
        /// Agregados para la Gridvw
        /// </summary>
        [DataMember]
        public String Descripcion { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public Int64 cod_Operacion { get; set; }
        [DataMember]
        public Int64 numero_Com { get; set; }
        [DataMember]
        public String tipo_Comp { get; set; }
        [DataMember]
        public String generado { get; set; }
        [DataMember]
        public String forma_Pago { get; set; }
        [DataMember]
        public String banco { get; set; }
        [DataMember]
        public String cuenta { get; set; }
        [DataMember]
        public Int64 banco2 { get; set; }
        [DataMember]
        public String cuenta_Bancaria { get; set; }
        [DataMember]
        public Int16 IdTipoActo { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }

        public string banc { get; set; }
        public string Numcue_des { get; set; }
    }
}
