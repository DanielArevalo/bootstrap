using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class DetallePagos
    {
        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Int64 NumCuota { get; set; }
        [DataMember]
        public DateTime FechaCuota { get; set; }
        [DataMember]      
        public Decimal Capital { get; set; }
        [DataMember]
        public Decimal IntCte { get; set; }
        [DataMember]
        public Decimal IntMora { get; set; }
        [DataMember]
        public Decimal LeyMiPyme { get; set; }
        [DataMember]
        public Decimal ivaLeyMiPyme { get; set; }

        [DataMember]
        public Decimal Otros { get; set; }
        [DataMember]
        public Decimal Poliza { get; set; }
        [DataMember]
        public Decimal Total { get; set; }
        [DataMember]    
        public Decimal Saldo { get; set; }
        [DataMember]
        public Decimal Cobranzas { get; set; }
        [DataMember]
        public Decimal Garantias_Comunitarias { get; set; }
        [DataMember]
        public string NumeroProducto { get; set; }
        [DataMember]
        public long CodigoCliente { get; set; }
        [DataMember]
        public long CodigoOperacion { get; set; }
        [DataMember]
        public DateTime FechaPago { get; set; }
        [DataMember]
        public decimal ValorPago { get; set; }
        [DataMember]
        public long TipoTran { get; set; }
        [DataMember]
        public long CodigoUsuarioRealizoTransaccion { get; set; }
        [DataMember]
        public string Documento { get; set; }
        [DataMember]
        public string Sobrante { get; set; }
        [DataMember]
        public string Error { get; set; }
    }
}
