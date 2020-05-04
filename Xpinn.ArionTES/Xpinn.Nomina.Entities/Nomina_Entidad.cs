using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Nomina_Entidad
    { 
        [DataMember] 
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string nom_persona { get; set; }
        [DataMember]
        public string nit { get; set; }
        public string email { get; set; }
        [DataMember]
        public string clase { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public Int64? ciudad { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string responsable { get; set; }
        [DataMember] 
        public string codigociiu { get; set; }
        [DataMember]
        public string codigopila { get; set; }


        [DataMember]
        public Int64 dias_liquidar { get; set; }

        [DataMember]
        public DateTime Fecha_ingreso { get; set; }

        [DataMember]
        public Int64 codigo_persona { get; set; }

        [DataMember]
        public Int64 codigo_empleado { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }

        [DataMember]
        public string fondosalud { get; set; }

        [DataMember]
        public string fondopension { get; set; }

        [DataMember]
        public string cajacompensacion { get; set; }

        [DataMember]
        public string arl { get; set; }

        [DataMember]
        public string cesantias { get; set; }

        [DataMember]
        public string formapago { get; set; }


        [DataMember]
        public string entidadpago { get; set; }


        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_contra { get; set; }

        [DataMember]
        public string CENTRO_COSTO_NOM { get; set; }

    }
}


