using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Interfaces.Entities
{
    [DataContract]
    [Serializable]
    public class WorkFlowPagoProveedores
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long numerocomprobante { get; set; }
        [DataMember]
        public long tipocomprobante { get; set; }
        [DataMember]
        public long codigobeneficiario { get; set; }
        [DataMember]
        public string numerofactura { get; set; }
        [DataMember]
        public string barcoderadicado { get; set; }
        [DataMember]
        public int estado { get; set; }
    }
}