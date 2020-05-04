using System;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    [DataContract]
    [Serializable]
    public class ObPlanPagos
    {
        //parametros de entrada del PL
        [DataMember]
        public Int64 Idobligacion { get; set; }
        [DataMember]
        public Int64 tasa_efectiva { get; set; }
        [DataMember]
        public Int64 tasa_per { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }

        [DataMember]
        public Int64 cod_obligacion { get; set; }
        [DataMember]
        public Int64 nrocuota { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public decimal amort_cap { get; set; }
        [DataMember]
        public decimal interes_corriente { get; set; }
        [DataMember]
        public decimal interes_mora { get; set; }
        [DataMember]
        public decimal seguro { get; set; }
        [DataMember]
        public decimal otros { get; set; }
        [DataMember]
        public decimal cuotaextra { get; set; }
        [DataMember]
        public decimal cuotanormal { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal cuotatotal { get; set; }

        // datos de la tabla  OBCOMPONENTECREDITO 

        [DataMember]
        public Int64 componente { get; set; }
        [DataMember]
        public Int64 calculocomponente { get; set; }
        [DataMember]
        public Int64 tipo_historico { get; set; }
        [DataMember]
        public String tipo_tasa { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public Int64 spread { get; set; }

        // datos de la tabla  obcomponentesadicionales 

        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public String nombre { get; set; }
      
        
    }
}
