using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class AmortizacionNIF
    {
        #region Grabado de CostoAmortizacion
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public int tipo_tasa { get; set; }
        [DataMember]
        public string nomtipo_tasa { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public decimal saldo_total { get; set; }
        [DataMember]
        public int plazo_faltante { get; set; }
        [DataMember]
        public decimal tasa_mercado { get; set; }
        [DataMember]
        public decimal tir { get; set; }
        [DataMember]
        public decimal valor_presente { get; set; }
        [DataMember]
        public decimal valor_ajuste { get; set; }
        [DataMember]
        public int estado { get; set; }
        #endregion
             
    }
}