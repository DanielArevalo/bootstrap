using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
    public class ELiquidacionInteres
    {
        [DataMember]
        public String NumeroCuenta { get; set; }

        [DataMember]
        public Int64 Linea { get; set; }

        [DataMember]
        public String Identificacion { get; set; }

        [DataMember]
        public Int64 Cod_Usuario { get; set; }

        [DataMember]
        public String Nombre { get; set; }

        [DataMember]
        public String Oficina { get; set; }

        [DataMember]
        public DateTime Fecha_Apertura { get; set; }

        [DataMember]
        public Decimal Saldo { get; set; }

        [DataMember]
        public Decimal Saldo_Base { get; set; }

        [DataMember]
        public Decimal Tasa_interes { get; set; }

        [DataMember]
        public int dias { get; set; }

        [DataMember]
        public Decimal Interes { get; set; }

        [DataMember]
        public Decimal Retefuente { get; set; }

        [DataMember]
        public Decimal valor_Neto { get; set; }

        [DataMember]
        public List<ELiquidacionInteres> lstLista { get; set; }

        [DataMember]
        public DateTime fecha_liquidacion { get; set; }

        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public Decimal valor_gmf { get; set; }
        [DataMember]
        public Decimal valor_pagar { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string cta_ahorros { get; set; }
        [DataMember]
        public int cod_interes { get; set; }
        [DataMember]
        public DateTime fecha_int { get; set; }
        [DataMember]
        public Decimal interes_capitalizado { get; set; }
        [DataMember]
        public Decimal retencion_causado { get; set; }
    }

}
