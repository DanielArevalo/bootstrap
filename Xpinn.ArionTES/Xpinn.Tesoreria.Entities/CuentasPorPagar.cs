using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasPorPagar
    {
        [DataMember]
        public int codigo_factura { get; set; }
        [DataMember]
        public string numero_factura { get; set; }
        [DataMember]
        public DateTime fecha_ingreso { get; set; }
        [DataMember]
        public DateTime? fecha_factura { get; set; }
        [DataMember]
        public DateTime? fecha_radicacion { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public int idtipo_cta_por_pagar { get; set; }
        [DataMember]
        public decimal? doc_equivalente { get; set; }
        [DataMember]
        public string num_contrato { get; set; }
        [DataMember]
        public string poliza { get; set; }
        [DataMember]
        public DateTime vence_contrato { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string cta_ahorros { get; set; }

        [DataMember]
        public int maneja_descuentos { get; set; }
        [DataMember]
        public int maneja_anticipos { get; set; }
        [DataMember]
        public decimal valor_anticipo { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public int estado { get; set; }

        [DataMember]
        public String nomestado { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
        [DataMember]
        public DateTime fecha_crea { get; set; }


        //AGREGADO
        [DataMember]
        public string tiponom { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public List<CuentaXpagar_Detalle> lstDetalle { get; set; }

        [DataMember]
        public List<CuentaXpagar_Pago> lstFormaPago { get; set; }


        [DataMember]
        public decimal valortotal { get; set; }
        [DataMember]
        public decimal valorneto { get; set; }
        [DataMember]
        public decimal valordescuento { get; set; }
        [DataMember]
        public string manejadscto { get; set; }
        [DataMember]
        public string manejaanti { get; set; }

        [DataMember]
        public DateTime fec_fact { get; set; }
        [DataMember]
        public DateTime fec_radi { get; set; }

        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public decimal porc_descuento { get; set; }
        [DataMember]
        public decimal porc_iva { get; set; }
        [DataMember]
        public decimal porc_retencion { get; set; }
        [DataMember]
        public decimal porc_reteiva { get; set; }
        [DataMember]
        public decimal porc_timbre { get; set; }
        [DataMember]
        public decimal valor_neto { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public string num_cuenta_destino { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public int cod_bancodestino { get; set; }
        [DataMember]
        public int idctabancaria { get; set; }
        [DataMember]
        public int formapago { get; set; }
        [DataMember]
        public int saldo { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }

        [DataMember]
        public int forma_pago { get; set; }



    }

    [DataContract]
    [Serializable]
    public class CuentaXpagar_Detalle
    {
        [DataMember]
        public int coddetallefac { get; set; }
        [DataMember]
        public int codigo_factura { get; set; }
        [DataMember]
        public int? cod_concepto_fac { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public int? centro_costo { get; set; }
        [DataMember]
        public int? cantidad { get; set; }
        [DataMember]
        public decimal? valor_unitario { get; set; }
        [DataMember]
        public decimal? valor_total { get; set; }
        [DataMember]
        public decimal? porc_descuento { get; set; }
        
        /*
        [DataMember]
        public decimal? porc_iva { get; set; }
        [DataMember]
        public decimal? porc_retencion { get; set; }
        [DataMember]
        public decimal? porc_reteiva { get; set; }
        [DataMember]
        public decimal? porc_timbre { get; set; } 
         */

        [DataMember]
        public decimal? valor_neto { get; set; }
        [DataMember]
        public decimal? valor_impuesto { get; set; }

        [DataMember]
        public List<Concepto_CuentasXpagarImp> lstImpuesto { get; set; }
        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }
        [DataMember]
        public int coddetalleimp { get; set; }


    }

    [DataContract]
    [Serializable]
    public class CuentaXpagar_Pago
    {
        [DataMember]
        public int codpagofac { get; set; }
        [DataMember]
        public int codigo_factura { get; set; }
        [DataMember]
        public int? numero { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public decimal? porcentaje { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? porc_descuento { get; set; }
        [DataMember]
        public decimal? valor_descuento { get; set; }
        [DataMember]
        public DateTime? fecha_descuento { get; set; }
        [DataMember]
        public decimal? vr_ConDescuento { get; set; }
        [DataMember]
        public int estado { get; set; }
    }
}