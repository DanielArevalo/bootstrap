using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionCDAT
    {
        //DATOS 

        [DataMember]
        public int cod_interes { get; set; }
        
        [DataMember]
        public DateTime fecha_liquidacion { get; set; }
        [DataMember]
        public Int64 codigo_cdat { get; set; }
        [DataMember]
        public string numero_cdat { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }

        [DataMember]
        public DateTime fecha_cierre { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public DateTime fecha_int { get; set; }
        [DataMember]
        public decimal interes { get; set; }
        [DataMember]
        public decimal retencion { get; set; }
        [DataMember]
        public decimal dias_liquida { get; set; }
        [DataMember]
        public decimal interes_causado { get; set; }
        [DataMember]
        public decimal retencion_causado { get; set; }
        [DataMember]
        public decimal valor_gmf { get; set; }
        [DataMember]
        public decimal interes_neto { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string cta_ahorros { get; set; }
        [DataMember]
        public decimal valor_pagar { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public List<LiquidacionCDAT> lstLista { get; set; }
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public decimal totalinteres { get; set; }

        [DataMember]
        public int?  capitalizar_int { get; set; }
        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }

        [DataMember]
        public int origen { get; set; }

        [DataMember]
        public string estadocierre { get; set; }
    }


    //DATOS DE OPERACION
    [DataContract]
    [Serializable]
    public class OPERACION
    {
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public int tipo_ope { get; set; }
        [DataMember]
        public int cod_usu { get; set; }
        [DataMember]
        public int cod_ofi { get; set; }
        [DataMember]
        public int cod_caja { get; set; }
        [DataMember]
        public int cod_cajero { get; set; }
        [DataMember]
        public int num_recibo { get; set; }
        [DataMember]
        public DateTime fecha_real { get; set; }
        [DataMember]
        public DateTime hora { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }
        [DataMember]
        public DateTime fecha_calc { get; set; }
        [DataMember]
        public int num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int num_lista { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public int cod_proceso { get; set; }
        [DataMember]
        public string ip { get; set; }
    }

}
