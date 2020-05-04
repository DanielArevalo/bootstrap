using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PagoOrdenes
    {
        [DataMember]
        public Int64 idordenservicio { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string idproveedor { get; set; }
        [DataMember]
        public string nomproveedor { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public int estados { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public decimal monto_solicitado { get; set; }
        [DataMember]
        public decimal monto_aprobado { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string numero_preimpreso { get; set; }
        [DataMember]
        public decimal valor_auxilio { get; set; }
    }
}
