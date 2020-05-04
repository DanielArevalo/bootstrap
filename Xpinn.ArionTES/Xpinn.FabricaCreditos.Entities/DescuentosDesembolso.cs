using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Descuentos Credito
    /// </summary>
    [DataContract]
    [Serializable]
    public class DescuentosDesembolso
    {
        [DataMember]
        public string numero_obligacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public string descuento { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public Int64 tipo_movimiento { get; set; }
    }

}