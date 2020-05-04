using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class Solicitud_cruce_ahorro
    {
        [DataMember]
        public Int64 idcruceahorro { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public string num_producto { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public DateTime? fecha_pago { get; set; }
        [DataMember]
        public decimal valor_pago { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public int? codusuario { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string ip { get; set; }

        //Adicionados
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string mensaje_error { get; set; }
        [DataMember]
        public string otro_atributo { get; set; }
    }

}
