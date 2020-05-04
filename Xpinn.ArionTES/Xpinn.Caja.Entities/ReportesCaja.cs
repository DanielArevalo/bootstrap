using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class ReportesCaja
    {
       
        [DataMember]
        public Int64 COD_OFICINA { get; set; }
        [DataMember]
        public string NOMBRE_OFICINA { get; set; }
        [DataMember]
        public Int64 COD_CAJERO { get; set; }
        [DataMember]
        public string NOMBRE_CAJERO { get; set; }
        [DataMember]
        public string FECHA_MOVIMIENTO { get; set; }
        [DataMember]
        public Int64 VALOR_PAGO { get; set; }
        [DataMember]
        public Int64 CANTIDAD_PAGOS { get; set; }
        [DataMember]
        public Int64 EFECTIVO { get; set; }
        [DataMember]
        public Int64 EGRESOS_EFECTIVO { get; set; }
        [DataMember]
        public Int64 saldocheque { get; set; }
        [DataMember]
        public Int64 saldoefectivo { get; set; }
        [DataMember]
        public Int64 consignaciones { get; set; }
        [DataMember]
        public Int64 totalcheque { get; set; }
        [DataMember]
        public Int64 totalefectivo{ get; set; }
        [DataMember]
        public Int64 CHEQUE { get; set; }
       
        //////////reporte movimiento diario de caja//////
      
    }
}

